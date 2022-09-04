using ViewModel;
using ViewModel.Usuario;
using Microsoft.Extensions.Configuration;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using NttDataApi.Common;
using System.Collections.Generic;
using NttDataApi.Database;
using NttDataApi.Database.Entitidades;

namespace NttDataApi.Repository
{
    public interface IUsuarioRepository
    {
        Task<ResponseGenerico> AddUser(UsuarioRequest request);
        Task<ResponseGenerico<LoginResponse>> LoginUser(LoginRequest request);
    }

    public class UsuarioRepository : IUsuarioRepository
    {
        private CooperativaDatabase Database { get; }
        private IConfiguration Configuration { get; }


        public UsuarioRepository(CooperativaDatabase dataDatabase, IConfiguration configuration)
        {
            Database = dataDatabase;
            Configuration = configuration;
        }


        public async Task<ResponseGenerico> AddUser(UsuarioRequest request)
        {
            var findUser = await Database.Usuarios!.FirstOrDefaultAsync(x => x.UserName.Equals(request.UserName));
            if (findUser != null)
                return new ResponseGenerico { Success = false, Message = "El nombre de usuario, ya se encuentra registrado" };

            findUser = await Database.Usuarios!.FirstOrDefaultAsync(x => x.Email.Equals(request.Email));
            if (findUser != null)
                return new ResponseGenerico { Success = false, Message = "El email ya se encuentra registrado" };

            findUser = await Database.Usuarios!.FirstOrDefaultAsync(x => x.Identificacion.Equals(request.Identificacion));
            if (findUser != null)
                return new ResponseGenerico { Success = false, Message = "Identificacion se encuentra registrado" };

            var createdPassword = Utilidad.Generatehashed(request.Password);

            findUser = new Usuario
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
                FechaNacimiento = request.FechaNacimiento
            };

            await Database.Usuarios!.AddAsync(findUser);
            await Database.SaveChangesAsync();

            return new ResponseGenerico { Success = true, Message = "Usuario creado exitosamente" };
        }

        public async Task<ResponseGenerico<LoginResponse>> LoginUser(LoginRequest request)
        {
            var findUser = await Database.Usuarios.FirstOrDefaultAsync(x => x.UserName.Equals(request.UserName));
            if (findUser == null)
                return new ResponseGenerico<LoginResponse> { Success = false, Message = "Usuario no encontrado" };


            var confirmacionPassword = Utilidad.ConfirmationPassword(request.Password, findUser.PasswordHash!, findUser.Password);

            if (!confirmacionPassword)
                return new ResponseGenerico<LoginResponse> { Success = false, Message = "Password incorrecto" };

            var claims = new List<Claim>
                    {
                        new Claim("UserId" , findUser.UsuarioId.ToString()),
                        new Claim("UserName" , findUser.UserName),


                    };


            var secret = Configuration["Jwt:Key"].ToString();
            var audiencia = Configuration["Jwt:Issuer"].ToString();
            var issur = Configuration["Jwt:Audience"].ToString();

            var generarToken = Utilidad.GenerateToken(secret, issur, audiencia, claims);
            return new ResponseGenerico<LoginResponse>
            {
                Success = true,
                Message = "Usuario autenticado",
                Result = new LoginResponse
                {
                    Token = generarToken
                }
            };

        }
    }
}
