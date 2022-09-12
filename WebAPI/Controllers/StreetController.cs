using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using WebAPI.Models;
using WebAPI.Contracts;

namespace WebAPI.Controllers
{
    [Route("streets")]
    [ApiController]
    [Authorize]
    public class StreetController : ControllerBase
    {
        private readonly IStreetService _service;
        public readonly string Entity = "Streets";

        private readonly APIDbContext context;
        public StreetController(APIDbContext context, IStreetService service)
        {
            this.context = context;
            _service = service;
        }

        [NonAction]
        private void AddLog(string action, string message)
        {
            try
            {
                int lastId = context.Logs.Max(p => p.Id);
                context.Logs.Add(new Log(lastId + 1, Entity, action, message));
                context.SaveChanges();
            }
            catch (Exception err)
            {
                Console.WriteLine(err.Message);
            }
        }

        /// <summary>
        /// Listado de calles
        /// </summary>
        /// <response code="200">Exito</response>
        /// <response code="401">No autorizado</response>
        /// <response code="403">Prohibido</response>
        /// <response code="500">Error del servidor</response>
        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                var streets = context.Streets.ToList();
                return Ok(streets);
            }
            catch(Exception err)
            {
                AddLog("GetAll", err.Message);
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
        public IActionResult GetById(int id)
        {
            try
            {
                var street = context.Streets.FirstOrDefault(x => x.Id == id);
                if (street != null)
                {
                    return Ok(street);
                }
                return NotFound($"No existe el registro con id: {id}.");
            }
            catch (Exception err)
            {
                AddLog("GetDetail", err.Message);
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
        public IActionResult Create(Street street)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    context.Streets.Add(street);
                    context.SaveChanges();
                    return Ok(street);
                }
                return BadRequest("Peticion incorrecta");
            }
            catch(Exception err)
            {
                AddLog("Create", err.Message);
                return Problem($"Hubo un error al completar la transaccion. {err}");
            }
        }

        /// <summary>
        /// Editar una calle
        /// </summary>
        /// <param name="id">Street</param>
        /// <param name="street">Street</param>
        /// <response code="200">Exito</response>
        /// <response code="400">Peticion incorrecta</response>
        /// <response code="401">No autorizado</response>
        /// <response code="403">Prohibido</response>
        /// <response code="404">No encontrado</response>
        /// <response code="500">Error del servidor</response>
        [HttpPut("{id}")]
        public IActionResult Update(int id, Street street)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var streetToEdit = context.Streets.FirstOrDefault(x => x.Id == id);
                    if(streetToEdit != null)
                    {
                        streetToEdit.Name = street.Name;
                        streetToEdit.City = street.City;
                        context.SaveChanges();

                        return Ok(street);
                    }
                    return NotFound($"No existe el registro con id: {id}.");
                }
                return BadRequest("Peticion incorrecta");
            }
            catch(Exception err)
            {
                AddLog("Update", err.Message);
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
        public IActionResult Delete(int id)
        {
            try
            {
                var streetToDelete = context.Streets.FirstOrDefault(x => x.Id == id);
                if (streetToDelete != null)
                {
                    context.Streets.Remove(streetToDelete);
                    context.SaveChanges();
                    return Ok(streetToDelete);
                }
                return NotFound($"No existe el registro con id: {id}");
            }
            catch (Exception err)
            {
                AddLog("Delete", err.Message);
                return Problem($"Hubo un error al completar la transaccion. {err}");
            }
        }
    }
}
