
using Microsoft.EntityFrameworkCore;
using NttDataApi.Common;
using NttDataApi.Database;
using NttDataApi.Database.Entitidades;
using NttDataApi.ViewModels.Cuenta;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ViewModel;
using ViewModel.Cuenta;

namespace NttDataApi.Repository
{
    public interface ICuentaRepository
    {
        Task<ResponseGenerico> CrearCuenta(CuentaRequest request);
        Task<ResponseGenerico<Cuenta>> ObtenerCuenta(int cuentaId);

        Task<ResponseGenerico<List<CuentaResponse>>> ObtenerCuentaAll();
        Task<ResponseGenerico<List<CuentaResponse>>> ObtenerCuentaByClienteId(int clienteId);
        Task<ResponseGenerico<List<CuentaResponse>>> ObtenerCuentaByCliente(CuentaFiltroRequest request);
    }
    public class CuentaRepository : ICuentaRepository
    {
        private CooperativaDatabase Database { get; }
        private IClienteRepository ClienteRepository { get; }


        public CuentaRepository(CooperativaDatabase database,
            IClienteRepository clienteRepository)
        {
            Database = database;
            ClienteRepository = clienteRepository;

        }
        public async Task<ResponseGenerico> CrearCuenta(CuentaRequest request)
        {
            
                var validarCLiente = await ClienteRepository.ObtenerCliente(request.ClienteId);
                if (!validarCLiente.Success)
                    return new ResponseGenerico
                    {
                        Success = validarCLiente.Success,
                        Message = validarCLiente.Message

                    };

                using (var ts = await Database.Database.BeginTransactionAsync())
                {
                    var numeroCuenta = Utilidad.CrearCuenta(request.ClienteId);
                    Cuenta cuenta = new Cuenta
                    {
                        NumeroCuenta = numeroCuenta,
                        ClienteId = request.ClienteId,
                        Estado = "C",
                        SaldoInicial = request.SaldoInicial,
                        TipoCuenta = request.TipoCuenta,
                        Creacion = DateTime.Now
                    };
                    await Database.Cuentas.AddAsync(cuenta);
                    await Database.SaveChangesAsync();

                    if (cuenta.CuentaId > 0)
                    {
                        await Database.Movimientos!.AddAsync(new Movimiento
                        {
                            CuentaId = cuenta.CuentaId,
                            Saldo = request.SaldoInicial,
                            Valor = request.SaldoInicial,
                            Fecha = DateTime.Now,
                            TipoMovimiento = "I",
                            Observacion = $"Creacion de cuenta {request.SaldoInicial}"
                        });
                        await Database.SaveChangesAsync();
                        await ts.CommitAsync();

                    }
                    else
                    {
                        await ts.RollbackAsync();
                        return new ResponseGenerico { Success = false, Message = "Ocurrio un error en crear la cuenta" };
                    }

                    return new ResponseGenerico { Success = true, Message = "Cuenta creada exitosamente" };
                }
           


        }

        public async Task<ResponseGenerico<Cuenta>> ObtenerCuenta(int cuentaId)
        {
            var cuenta = await Database.Cuentas!.Include(x => x.Cliente)
                        .Where(x => x.CuentaId == cuentaId).FirstOrDefaultAsync();

            return new ResponseGenerico<Cuenta>
            {
                Message = cuenta != null ? "" : "Cuenta no encontrada",
                Success = cuenta != null,
                Result = cuenta
            };
        }

        public async Task<ResponseGenerico<List<CuentaResponse>>> ObtenerCuentaAll()
        {
            var cuentas = await Database.Cuentas.Include(x => x.Cliente)
                             
                             .Select(x => new CuentaResponse
                             {
                                 Cliente = x.Cliente.Nombre,
                                 CuentaId = x.CuentaId,
                                 NumeroCuenta = x.NumeroCuenta,
                                 FechaApertura = x.Creacion.ToString("yyyy-MM-dd"),
                                 SaldoApertura = x.SaldoInicial
                             }).ToListAsync();

            return new ResponseGenerico<List<CuentaResponse>>
            {
                Success = cuentas.Count > 0,
                Message = cuentas.Count > 0 ? "" : "No hay registros",
                Result = cuentas
            };
        }

        public async Task<ResponseGenerico<List<CuentaResponse>>> ObtenerCuentaByClienteId(int clienteId)
        {
            var cuentas = await Database.Cuentas.Include(x => x.Cliente)
                            .Where(x => x.ClienteId == clienteId)
                            .Select(x => new CuentaResponse
                            {
                               Cliente = x.Cliente.Nombre,
                               CuentaId = x.CuentaId,
                               NumeroCuenta = x.NumeroCuenta,
                               FechaApertura = x.Creacion.ToString("yyyy-MM-dd"),
                               SaldoApertura = x.SaldoInicial
                            }).ToListAsync();

            return new ResponseGenerico<List<CuentaResponse>>
            {
                Success = cuentas.Count > 0,
                Message = cuentas.Count > 0 ? "" : "No hay registros",
                Result = cuentas
            };
        }

        public async Task<ResponseGenerico<List<CuentaResponse>>> ObtenerCuentaByCliente(CuentaFiltroRequest request)
        {
            var cuentas = await Database.Cuentas.Include(x => x.Cliente)
                           .Where(x => (x.Cliente.Identificacion.Contains(request.Identificacion)
                            ||string.IsNullOrWhiteSpace(request.Identificacion)
                            ) &&
                            (x.Cliente.Nombre.Contains(request.Nombre)
                            || string.IsNullOrWhiteSpace(request.Nombre)

                           ))
                           .Select(x => new CuentaResponse
                           {
                               Cliente = x.Cliente.Nombre,
                               CuentaId = x.CuentaId,
                               NumeroCuenta = x.NumeroCuenta,
                               FechaApertura = x.Creacion.ToString("yyyy-MM-dd"),
                               SaldoApertura = x.SaldoInicial
                           }).ToListAsync();

            return new ResponseGenerico<List<CuentaResponse>>
            {
                Success = cuentas.Count > 0,
                Message = cuentas.Count > 0 ? "" : "No hay registros",
                Result = cuentas
            };
        }
    }
}
