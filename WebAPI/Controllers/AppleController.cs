using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using System.Text.Json;

using WebAPI.Models;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("apples")]
    //[Authorize]
    public class AppleController : Controller
    {
        private readonly IConfiguration Configuration;
        static readonly HttpClient client = new();

        public AppleController(IConfiguration configuration)
        {
            Configuration = configuration;
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
        async public Task<IActionResult> FindById(int id)
        {
            try
            {
                string url = $"{Configuration["Callings:apples"]}&CQL_Filter=(id={id})";
                var response = await client.GetStringAsync(url);
                if (response != null)
                {
                    var apple = JsonSerializer.Deserialize<GeoserverResponse<Apple>>(response);

                    if (apple?.features.Length > 0)
                    {
                        return Ok(apple);
                    }
                    else
                    {
                        return NotFound("La manzana no existe");
                    }
                }
                else
                {
                    return BadRequest("Peticion incorrecta");
                }
            }
            catch (Exception err)
            {
                return Problem("Hubo un error al obtener la manzana. " + err.Message);
            }
        }

        /// <summary>
        /// Buscar manzanas mayores a una fecha
        /// </summary>
        /// <param name="date">Formato: AAAA-MM-DD</param>
        /// <response code="200">Exito</response>
        /// <response code="400">Peticion incorrecta</response>
        /// <response code="401">No autorizado</response>
        /// <response code="403">Prohibido</response>
        /// <response code="404">No encontrado</response>
        /// <response code="500">Error del servidor</response>
        [HttpGet("date")]
        async public Task<IActionResult> FindByDate(string date)
        {
            try
            {
                string url = $"{Configuration["Callings:apples"]}&CQL_Filter=(fecha_modif AFTER {date}T00:00:00)";

                var response = await client.GetStringAsync(url);
                if (response != null)
                {
                    var apple = JsonSerializer.Deserialize<GeoserverResponse<Apple>>(response);

                    if (apple?.features.Length > 0)
                    {
                        return Ok(apple);
                    }
                    else
                    {
                        return NotFound("No hay manzanas modificadas despues de esa fecha.");
                    }
                }
                else
                {
                    return BadRequest("Peticion incorrecta");
                }
            }
            catch (Exception err)
            {
                return Problem("Hubo un error al obtener las manzanas " + err.Message);
            }
        }
    }
}
