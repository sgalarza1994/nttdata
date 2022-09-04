
using Microsoft.EntityFrameworkCore;
using NttDataApi.Common;
using NttDataApi.Database;
using NttDataApi.Database.Entitidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ViewModel;
using ViewModel.Movimiento;
namespace NttDataApi.Repository
{
    public interface IMovimientoRepository
    {
        Task<ResponseGenerico> RetiroCajero(MovimientoRequest request);
        Task<ResponseGenerico> Deposito(MovimientoRequest request);
        Task<ResponseGenerico<List<MovimientoResponse>>> ObtenerMovimiento(MovimentoFiltroFechaRequest request);
    }
    public class MovimientoRepository : IMovimientoRepository
    {
        private CooperativaDatabase Database { get; }
        private ICuentaRepository CuentaRepository { get; }
        public MovimientoRepository(CooperativaDatabase database, 
            ICuentaRepository cuentaRepository)
        {
            Database = database;
            CuentaRepository = cuentaRepository;
        }

        public async Task<ResponseGenerico> RetiroCajero(MovimientoRequest request)
        {
            var cuentaResponse = await CuentaRepository.ObtenerCuenta(request.CuentaId);
            if (!cuentaResponse.Success)
            {
                return new ResponseGenerico
                {
                    Success = false,
                    Message = cuentaResponse.Message
                };
            }
            var cuenta = cuentaResponse.Result;
            var confirmacionClave = Utilidad.ConfirmationPassword(request.ClaveCajero, cuenta.Cliente!.PasswordHash, cuenta.Cliente.Password);
            if (!confirmacionClave)
                return new ResponseGenerico { Success = false, Message = "Clave cajero no es valida" };


            var movimientos = await Database.Movimientos.Where(x => x.CuentaId == request.CuentaId)
                            .ToListAsync();

            var movimiento = movimientos.LastOrDefault()!;


            if (request.Valor > movimiento.Saldo)
            {

                return new ResponseGenerico { Success = false, Message = "Fondos insuficientes" };
            }

            await Database.Movimientos.AddAsync(new Movimiento
            {
                CuentaId = request.CuentaId,
                Fecha = DateTime.Now,
                Observacion = $"{request.Observacion} {request.Valor}",
                Saldo = movimiento.Saldo - request.Valor,
                Valor = request.Valor,
                TipoMovimiento = "R"
            });
            await Database.SaveChangesAsync();
            return new ResponseGenerico
            {
                Success = true,
                Message = "Retire su dinero"
            };
        }

        public async Task<ResponseGenerico> Deposito(MovimientoRequest request)
        {
            var cuentaResponse = await CuentaRepository.ObtenerCuenta(request.CuentaId);
            if (!cuentaResponse.Success)
            {
                return new ResponseGenerico
                {
                    Success = false,
                    Message = cuentaResponse.Message
                };
            }

            var movimientos = await Database.Movimientos.Where(x => x.CuentaId == request.CuentaId)
                            .ToListAsync();

            var movimiento = movimientos.LastOrDefault()!;
            await Database.Movimientos.AddAsync(new Movimiento
            {
                CuentaId = request.CuentaId,
                Fecha = DateTime.Now,
                Observacion = $"{request.Observacion} {request.Valor}",
                Saldo = movimiento.Saldo + request.Valor,
                Valor = request.Valor,
                TipoMovimiento = "I"
            });
            await Database.SaveChangesAsync();
            return new ResponseGenerico
            {
                Success = true,
                Message = "Dinero acreditado"
            };

        }

        public async Task<ResponseGenerico<List<MovimientoResponse>>> ObtenerMovimiento(MovimentoFiltroFechaRequest request)
        {

            List<MovimientoResponse> listado = await Database.Movimientos.Include(x => x.Cuenta).ThenInclude(x => x.Cliente)
                            .Where(x => x.Fecha.Date >= Convert.ToDateTime(request.Desde).Date
                            && x.Fecha.Date <= Convert.ToDateTime(request.Hasta).Date
                            && (x.Cuenta.Cliente.Identificacion.Contains(request.Identificacion) || string.IsNullOrWhiteSpace(request.Identificacion))
                            )
                            .Select(x => new MovimientoResponse
                            {
                                MovimientoId = x.MovimientoId,
                                Cliente = x.Cuenta.Cliente.Nombre,
                                Fecha = x.Fecha.ToString("yyyy-MM-dd"),
                                Movimiento = x.TipoMovimiento.Equals("I") ? x.Valor.ToString() : $"-{x.Valor}",
                                NumCuenta = x.Cuenta.NumeroCuenta,
                                TipoCuenta = x.Cuenta.TipoCuenta,
                                SaldoInicial = x.Cuenta.SaldoInicial,
                                SaldoDisponible = x.Saldo,
                                Observacion = x.Observacion
                            }).ToListAsync();

     
            
            
            return new ResponseGenerico<List<MovimientoResponse>> { 
                Success = listado.Count > 0, 
                Message = listado.Count > 0 ? "": "No existen registros en el rango de fecha solicitado",
                Result =listado
            
            };



        }
    }
}
