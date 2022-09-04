using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NttDataApi.Repository;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ViewModel;
using ViewModel.Movimiento;

namespace NttDataApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovimientoController : ControllerBase
    {
        public MovimientoController(IMovimientoRepository movimientoRepository)
        {
            MovimientoRepository = movimientoRepository;
        }
        public IMovimientoRepository MovimientoRepository { get; }


        [HttpPost("[action]")]
        public async Task<ActionResult<ResponseGenerico>> RetiroCajero(MovimientoRequest request)
        {
            try
            {
                return Ok(await MovimientoRepository.RetiroCajero(request));
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
        public async Task<ActionResult<ResponseGenerico>> Deposito(MovimientoRequest request)
        {
            try
            {
                return Ok(await MovimientoRepository.Deposito(request));
            }
            catch (Exception e)
            {
                return Ok(new ResponseGenerico { Success = false, Message = e.Message });  
            }
           
        }

        [HttpPost("[action]")]
        public async Task<ActionResult<ResponseGenerico<List<MovimientoResponse>>>> ObtenerMovimientos(MovimentoFiltroFechaRequest request)
        {
            try
            {
                return Ok(await MovimientoRepository.ObtenerMovimiento(request));
            }
            catch (Exception e)
            {
                return Ok(new ResponseGenerico { Success=false,Message= e.Message});    
            }
            
        }
    }
}
