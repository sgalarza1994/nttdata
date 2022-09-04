using Microsoft.EntityFrameworkCore;
using NttDataApi.Common;
using NttDataApi.Database;
using NttDataApi.Database.Entitidades;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ViewModel;
using ViewModel.Cliente;

namespace NttDataApi.Repository
{
    public interface IClienteRepository
    {
        Task<ResponseGenerico> AddClient(ClienteRequest request);
        Task<ResponseGenerico> EditCLient(ClienteRequest request);

        Task<ResponseGenerico> DeleteCliente(int clientId);

        Task<ResponseGenerico<List<ClienteResponse>>> ObtenerCliente();

        Task<ResponseGenerico<ClienteResponse>> ObtenerCliente(int clienteId);
    }
    public  class ClienteRepository : IClienteRepository
    {
        private CooperativaDatabase Database { get; }
        public ClienteRepository(CooperativaDatabase database
           )
        {
            Database = database;
        }
        public async Task<ResponseGenerico> AddClient(ClienteRequest request)
        {
            var findUser = await Database.Clientes!.FirstOrDefaultAsync(x => x.UserName.Equals(request.UserName));
            if (findUser != null)
                return new ResponseGenerico { Success = false, Message = "El nombre de usuario, ya se encuentra registrado" };

            findUser = await Database.Clientes!.FirstOrDefaultAsync(x => x.Identificacion.Equals(request.Identificacion));
            if (findUser != null)
                return new ResponseGenerico { Success = false, Message = "EL cliente ya se encuentra registrado" };


            var createdPassword = Utilidad.Generatehashed(request.Password);

            findUser = new Cliente
            {
                Password = createdPassword.Item2,
                PasswordHash = createdPassword.Item1,
                Email = request.Email,
                Direccion = request.Direccion,
                Genero = request.Genero,
                Identificacion = request.Identificacion,
                UserName = request.UserName,
                Nombre = request.Nombre,
                Telefono = request.Telefono,
                FechaNacimiento = request.FechaNacimiento,
                Estado = "C"
            };

            await Database.Clientes!.AddAsync(findUser);
            await Database.SaveChangesAsync();

            return new ResponseGenerico { Success = true, Message = "Cliente registrado" };
        }

        public async Task<ResponseGenerico> DeleteCliente(int clientId)
        {
            var client = await Database.Clientes!.FindAsync(clientId);
            if (client == null)
                return new ResponseGenerico { Success = false, Message = "Cliente no encontrado" };


            client.Estado = "E";
            await Database.SaveChangesAsync();
            return new ResponseGenerico { Success = true, Message = "Cliente eliminado" };
        }

        public async Task<ResponseGenerico> EditCLient(ClienteRequest request)
        {
            var client = await Database.Clientes!.FindAsync(request.ClienteId);
            if (client == null)
                return new ResponseGenerico { Success = false, Message = "Cliente no encontrado" };

            client.Direccion = request.Direccion;
            client.Telefono = request.Telefono;
            client.Email = request.Email;
            client.FechaNacimiento = request.FechaNacimiento;

            await Database.SaveChangesAsync();
            return new ResponseGenerico { Success = true, Message = "Cliente Editado" };
        }

        public async Task<ResponseGenerico<List<ClienteResponse>>> ObtenerCliente()
        {
            var rsp = await Database.Clientes!
                     .Select(s => new ClienteResponse
                     {
                         ClienteId = s.ClienteId,
                         Direccion = s.Direccion,
                         Edad = Utilidad.ObtenerEdad(s.FechaNacimiento),
                         FechaNacimiento = s.FechaNacimiento,
                         Email = s.Email,
                         Genero = s.Genero.Equals("M") ? "MASCULINO" : "FEMENINO",
                         Identificacion = s.Identificacion,
                         Nombre = s.Nombre,
                         Telefono = s.Telefono,
                         UserName = s.UserName,
                         Estado = Utilidad.EstadoVisualizar(s.Estado)

                     }).ToListAsync();

            return new ResponseGenerico<List<ClienteResponse>>
            {
                Success = rsp.Count > 0,
                Message = rsp.Count > 0 ? "exito" : "No hay registros",
                Result = rsp
            };
        }

        public async Task<ResponseGenerico<ClienteResponse>> ObtenerCliente(int clienteId)
        {
            var rsp = await Database.Clientes!.FindAsync(clienteId);
            if (rsp == null)
                return new ResponseGenerico<ClienteResponse>
                {
                    Message = "Cliente no encontrado",
                    Success = false
                };
            var rspCliente = new ClienteResponse
            {
                ClienteId = rsp.ClienteId,
                Direccion = rsp.Direccion,
                Telefono = rsp.Telefono,
                Email = rsp.Email,
                FechaNacimiento = rsp.FechaNacimiento,
                Identificacion = rsp.Identificacion,
                Nombre = rsp.Nombre,
                Estado = rsp.Estado
            };
            return new ResponseGenerico<ClienteResponse> { Success = true, Result = rspCliente };
        }
    }
}
