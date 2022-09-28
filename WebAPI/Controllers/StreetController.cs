using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using WebAPI.Services;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("streets")]
    [Authorize]
    public class StreetController : ControllerBase
    {
        private readonly IStreetService _streetService;

        public StreetController(IStreetService streetService)
        {
            _streetService = streetService;
        }

        /// <summary>
        /// Listado de calles
        /// </summary>
        /// <response code="200">Exito</response>
        /// <response code="204">No hay contenido</response>
        /// <response code="401">No autorizado</response>
        /// <response code="403">Prohibido</response>
        /// <response code="500">Error del servidor</response>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var streets = await _streetService.GetAll();
                if(streets.Count > 0)
                {
                    return Ok(streets);
                }
                return NoContent();
            }
            catch (Exception err)
            {
                return Problem($"Hubo un error al completar la transaccion. {err}");
            }
        }

        /// <summary>
        /// Detalle de una calle
        /// </summary>
        /// <response code="200">Exito</response>
        /// <response code="401">No autorizado</response>
        /// <response code="403">Prohibido</response>
        /// <response code="404">No encontrado</response>
        /// <response code="500">Error del servidor</response>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                if(ModelState.IsValid && id != 0)
                {
                    var street = await _streetService.GetById(id);
                    if(street.Name != null)
                    {
                        return Ok(street);
                    }
                    return NotFound($"No existe el registro con id: {id}");
                }
                return BadRequest("Peticion incorrecta");
            }
            catch (Exception err)
            {
                return Problem($"Hubo un error al completar la transaccion. {err}");
            }
        }

        /// <summary>
        /// Creacion de una calle
        /// </summary>
        /// <param name="street">Street</param>
        /// <response code="200">Exito</response>
        /// <response code="400">Peticion incorrecta</response>
        /// <response code="401">No autorizado</response>
        /// <response code="403">Prohibido</response>
        /// <response code="500">Error del servidor</response>
        [HttpPost]
        public async Task<IActionResult> Create(Street street)
        {
            try
            {
                if (ModelState.IsValid && street != null)
                {
                    var newStreet = await _streetService.Create(street);
                    if(newStreet.Name != null)
                    {
                        return Ok(newStreet);
                    }
                }
                return BadRequest("Peticion incorrecta");
            }
            catch(Exception err)
            {
                return Problem($"Hubo un error al completar la transaccion. {err}");
            }
        }

        /// <summary>
        /// Editar una calle
        /// </summary>
        /// <param name="street">Street</param>
        /// <response code="200">Exito</response>
        /// <response code="400">Peticion incorrecta</response>
        /// <response code="401">No autorizado</response>
        /// <response code="403">Prohibido</response>
        /// <response code="404">No encontrado</response>
        /// <response code="500">Error del servidor</response>
        [HttpPut]
        public async Task<IActionResult> Update(Street street)
        {
            try
            {
                if(ModelState.IsValid && street != null)
                {
                    var streetToEdit = await _streetService.Update(street);
                    if(streetToEdit.Name != null)
                    {
                        return Ok(streetToEdit);
                    }
                    return NotFound($"No existe el registro con id: {street.Id}.");
                }
                return BadRequest("Peticion incorrecta");
            }
            catch(Exception err)
            {
                return Problem($"Hubo un error al completar la transaccion. {err}");
            }
        }

        /// <summary>
        /// Eliminar una calle
        /// </summary>
        /// <response code="200">Exito</response>
        /// <response code="401">No autorizado</response>
        /// <response code="403">Prohibido</response>
        /// <response code="404">No encontrado</response>
        /// <response code="500">Error del servidor</response>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                if(ModelState.IsValid && id != 0)
                {
                    var streetToDelete = await _streetService.Delete(id);
                    if (streetToDelete)
                    {
                        return Ok("Registro eliminado");
                    }
                    return NotFound($"No existe el registro con id: {id}.");
                }
                return BadRequest("Peticion incorrecta");
            }
            catch (Exception err)
            {
                return Problem($"Hubo un error al completar la transaccion. {err}");
            }
        }
    }
}

