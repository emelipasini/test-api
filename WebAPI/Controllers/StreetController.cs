using Microsoft.AspNetCore.Authorization;
using AutoWrapper.Extensions;
using Microsoft.AspNetCore.Mvc;

using Models;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("streets")]
    //[Authorize]
    public class StreetController : Controller
    {
        public readonly string Entity = "Streets";

        private readonly APIDbContext context;
        public StreetController(APIDbContext context)
        {
            this.context = context;
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
        /// <response code="500">Error del servidor</response>
        [HttpGet]
        public IActionResult Index()
        {
            try
            {
                var streets = context.Streets.ToList();
                return Ok(new Response(200, false, "Peticion exitosa", streets));
            }
            catch(Exception err)
            {
                AddLog("GetAll", err.Message);
                return StatusCode(500, new Response(500, true, "Hubo un error al completar la transaccion. ", err));
            }
        }

        /// <summary>
        /// Detalle de una calle
        /// </summary>
        /// <response code="200">Exito</response>
        /// <response code="401">No autorizado</response>
        /// <response code="404">Calle no encontrada</response>
        /// <response code="500">Error del servidor</response>
        [HttpGet("{id}")]
        public IActionResult Details(int id)
        {
            try
            {
                var street = context.Streets.FirstOrDefault(x => x.Id == id);
                if (street != null)
                {
                    return Ok(new Response(200, false, "Peticion exitosa", street));
                }
                return StatusCode(404, new Response(404, true, $"No existe el registro con id: {id}.", ""));
            }
            catch (Exception err)
            {
                AddLog("GetDetail", err.Message);
                return StatusCode(500, new Response(500, true, "Hubo un error al completar la transaccion.", err));
            }
        }
        
        /// <summary>
        /// Creacion de una calle
        /// </summary>
        /// <param name="street">Street</param>
        /// <response code="200">Exito</response>
        /// <response code="400">La calle no tiene el formato correcto</response>
        /// <response code="401">No autorizado</response>
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
                    return Ok(new Response(200, false, "Peticion exitosa", street));
                }
                return StatusCode(400, new Response(400, true, "Peticion incorrecta", ModelState.AllErrors()));
            }
            catch(Exception err)
            {
                AddLog("Create", err.Message);
                return StatusCode(500, new Response(500, true, "Hubo un error al completar la transaccion.", err));
            }
        }
        
        /// <summary>
        /// Editar una calle
        /// </summary>
        /// <param name="id">Street</param>
        /// <param name="street">Street</param>
        /// <response code="200">Exito</response>
        /// <response code="400">La calle no tiene el formato correcto</response>
        /// <response code="401">No autorizado</response>
        /// <response code="404">Calle no encontrada</response>
        /// <response code="500">Error del servidor</response>
        [HttpPut("{id}")]
        public IActionResult Edit(int id, Street street)
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

                        return Ok(new Response(200, false, "Peticion exitosa", street));
                    }
                    return StatusCode(404, new Response(404, true, $"No existe el registro con id: {id}.", ""));
                }
                return StatusCode(400, new Response(400, true, "Peticion incorrecta", ModelState.AllErrors()));
            }
            catch(Exception err)
            {
                AddLog("Update", err.Message);
                return StatusCode(500, new Response(500, true, "Hubo un error al completar la transaccion.", err));
            }
        }
        
        /// <summary>
        /// Eliminar una calle
        /// </summary>
        /// <response code="200">Exito</response>
        /// <response code="401">No autorizado</response>
        /// <response code="404">Calle no encontrada</response>
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
                    return Ok(new Response(200, false, "Peticion exitosa", streetToDelete));
                }
                return StatusCode(404, new Response(404, true, $"No existe el registro con id: {id}.", ""));
            }
            catch (Exception err)
            {
                AddLog("Delete", err.Message);
                return StatusCode(500, new Response(500, true, "Hubo un error al completar la transaccion.", err));
            }
        }
    }
}
