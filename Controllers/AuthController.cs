using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NttDataApi.Repository;
using System;
using System.Threading.Tasks;
using ViewModel;
using ViewModel.Usuario;

namespace NttDataApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        public AuthController(IUsuarioRepository usuarioRepository)
        {
            UsuarioRepository = usuarioRepository;
        }

        public IUsuarioRepository UsuarioRepository { get; }

        [HttpPost("[action]")]
        public async Task<ActionResult<ResponseGenerico>> AddUser(UsuarioRequest request)
        {
            try
            {
                var validator = ValidatorCustom<UsuarioRequest>.Validator(request);
                if (!validator.Success)
                    return Ok(validator);
                return Ok(await UsuarioRepository.AddUser(request));
            }
            catch (Exception e)
            {

                return Ok(new ResponseGenerico
                {
                    Success = false,
                    Message = e.Message
                });
            }
          
        }

        [HttpPost("[action]")]
        public async Task<ActionResult<ResponseGenerico<LoginResponse>>> LoginUser(LoginRequest request)
        {
            try
            {
                return Ok(await UsuarioRepository.LoginUser(request));
            }
            catch (Exception e)
            {

                return Ok(new ResponseGenerico
                {
                    Success = false,
                    Message = e.Message
                });
            }
           
        }
    }
}
