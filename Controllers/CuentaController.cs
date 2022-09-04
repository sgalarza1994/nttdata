using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NttDataApi.Repository;
using NttDataApi.ViewModels.Cuenta;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ViewModel;
using ViewModel.Cuenta;

namespace NttDataApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CuentaController : ControllerBase
    {
        public CuentaController(ICuentaRepository cuentaRepository)
        {
            CuentaRepository = cuentaRepository;
        }

        public ICuentaRepository CuentaRepository { get; }

        [HttpPost("[action]")]
        public async Task<ActionResult<ResponseGenerico>> CrearCuenta(CuentaRequest request)
        {
            try
            {
                return Ok(await CuentaRepository.CrearCuenta(request));
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


        [HttpGet("[action]")]
        public async Task<ActionResult<ResponseGenerico<List<CuentaResponse>>>> ObtenerCuentaAll()
        {
            try
            {

                return Ok(await CuentaRepository.ObtenerCuentaAll());
            }
            catch (Exception e)
            {

                return Ok(new ResponseGenerico { Success = false, Message = e.Message });
            }

        }

        [HttpGet("[action]/{clienteId}")]
        public async Task<ActionResult<ResponseGenerico<List<CuentaResponse>>>> ObtenerCuentaAll(int clienteId)
        {
            try
            {

                return Ok(await CuentaRepository.ObtenerCuentaByClienteId(clienteId));
            }
            catch (Exception e)
            {

                return Ok(new ResponseGenerico { Success = false, Message = e.Message });
            }

        }

        [HttpGet("[action]")]
        public async Task<ActionResult<ResponseGenerico<List<CuentaResponse>>>> ObtenerCuentaFiltro([FromQuery] CuentaFiltroRequest request)
        {
            try
            {

                return Ok(await CuentaRepository.ObtenerCuentaByCliente(request));
            }
            catch (Exception e)
            {

                return Ok(new ResponseGenerico { Success = false, Message = e.Message });
            }

        }
    }
}
