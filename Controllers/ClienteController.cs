using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NttDataApi.Repository;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ViewModel;
using ViewModel.Cliente;

namespace NttDataApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ClienteController : ControllerBase
    {
        public ClienteController(IClienteRepository clienteRepository)
        {
            ClienteRepository = clienteRepository;
        }
        public IClienteRepository ClienteRepository { get; }

        [HttpGet("[action]")]
        public async Task<ActionResult<ResponseGenerico<List<ClienteResponse>>>> ObtenerClientes()
        {
            try
            { 

                return Ok(await ClienteRepository.ObtenerCliente());
            }
            catch (Exception e)
            {

                return Ok(new ResponseGenerico { Success=false,Message=e.Message });
            }
            
        }

        [HttpPost("[action]")]
        public async Task<ActionResult<ResponseGenerico>> AddCliente(ClienteRequest request)
        {
            try
            {
                var validator = ValidatorCustom<ClienteRequest>.Validator(request);
                if (!validator.Success)
                    return Ok(validator);
                return Ok(await ClienteRepository.AddClient(request));
            }
            catch (Exception e) { return Ok(new ResponseGenerico { Success = false, Message = e.Message }); }
        }

        [HttpPut("[action]")]
        public async Task<ActionResult<ResponseGenerico>> ModificarCliente(ClienteRequest request)
        {
            try
            {
                var validator = ValidatorCustom<ClienteRequest>.Validator(request);
                if (!validator.Success)
                    return Ok(validator);
                return Ok(await ClienteRepository.EditCLient(request));
            }
            catch (Exception e)
            {

                return Ok(new ResponseGenerico { Success = false, Message = e.Message });
            }
            
        }

        [HttpDelete("[action]/{clienteId}")]
        public async Task<ActionResult<ResponseGenerico>> EliminarCliente(int clienteId)
        {
            try
            {
                return Ok(await ClienteRepository.DeleteCliente(clienteId));
            }
            catch (Exception e)
            {

                return Ok(new ResponseGenerico { Success = false, Message = e.Message });
            }
           
        }

    }
}
