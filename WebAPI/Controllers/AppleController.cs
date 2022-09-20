using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using WebAPI.Services;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("apples")]
    [Authorize]
    public class AppleController : Controller
    {
        private readonly IAppleService _appleService;

        public AppleController(IAppleService appleService)
        {
            _appleService = appleService;
        }

        /// <summary>
        /// Buscar una manzana por id
        /// </summary>
        /// <response code="200">Exito</response>
        /// <response code="400">Peticion incorrecta</response>
        /// <response code="401">No autorizado</response>
        /// <response code="403">Prohibido</response>
        /// <response code="404">No encontrado</response>
        /// <response code="500">Error del servidor</response>
        [HttpGet("{id}")]
        public async Task<IActionResult> FindById(int id)
        {
            try
            {
                var apple = await _appleService.FindById(id);
                if (apple.features.Any())
                {
                    return Ok(apple);
                }
                return NotFound($"No existe el registro con id: {id}");
            }
            catch (Exception err)
            {
                return Problem($"Hubo un error al completar la transaccion. {err}");
            }
        }

        /// <summary>
        /// Buscar manzanas mayores a una fecha 
        /// </summary>
        /// <response code="200">Exito</response>
        /// <response code="400">Peticion incorrecta</response>
        /// <response code="401">No autorizado</response>
        /// <response code="403">Prohibido</response>
        /// <response code="404">No encontrado</response>
        /// <response code="500">Error del servidor</response>
        [HttpGet("fecha")]
        public async Task<IActionResult> FindByDate(string date)
        {
            try
            {
                var apples = await _appleService.FindByDate(date);
                if (apples.features.Any())
                {
                    return Ok(apples);
                }
                return NotFound($"No se encontraron registros mayores a esa fecha");
            }
            catch (Exception err)
            {
                return Problem($"Hubo un error al completar la transaccion. {err}");
            }
        }
    }
}

