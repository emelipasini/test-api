using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("streets")]
    [Authorize]
    public class StreetController : Controller
    {

        public static List<Street> AllStreets = new List<Street>
        {
            new Street(1, "Rivadavia", "Rosario"),
            new Street(2, "Laprida", "Rosario"),
            new Street(3, "Uruguay", "Rosario")
        };

        /// <summary>
        /// Lista de calles
        /// </summary>
        /// <response code="200">Exito</response>
        /// <response code="401">No autorizado</response>
        /// <response code="500">Error del servidor</response>
        [HttpGet]
        public JsonResult Index()
        {
            return new JsonResult(AllStreets);
        }

        /// <summary>
        /// Detalle de una calle
        /// </summary>
        /// <response code="200">Exito</response>
        /// <response code="401">No autorizado</response>
        /// <response code="404">Calle no encontrada</response>
        /// <response code="500">Error del servidor</response>
        [HttpGet("details/{id}")]
        public JsonResult Details(int id)
        {
            var street = AllStreets.FirstOrDefault(x => x.Id == id);
            if (street == null)
            {
                var result = new JsonResult("Calle no encontrada");
                result.StatusCode = 404;
                return result;
            } else
            {
                return new JsonResult(street);
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
        public JsonResult Create(Street street)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    AllStreets.Add(street);
                    return new JsonResult(street);
                }
                var result = new JsonResult("Formato invalido");
                result.StatusCode = 400;
                return result;
            }
            catch(Exception err)
            {
                return new JsonResult("Hubo un error al crear la calle. " + err);
            }

        }

        /// <summary>
        /// Editar una calle
        /// </summary>
        /// <param name="street">Street</param>
        /// <response code="200">Exito</response>
        /// <response code="400">La calle no tiene el formato correcto</response>
        /// <response code="401">No autorizado</response>
        /// <response code="404">Calle no encontrada</response>
        /// <response code="500">Error del servidor</response>
        [HttpPost("edit/{id}")]
        public JsonResult Edit(int id, Street street)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var streetToEdit = AllStreets.FirstOrDefault(x => x.Id == id);
                    if(streetToEdit != null)
                    {
                        var index = AllStreets.IndexOf(streetToEdit);
                        AllStreets[index] = street;
                        return new JsonResult(street);
                    } else
                    {
                        var result = new JsonResult("Calle no encontrada");
                        result.StatusCode = 404;
                        return result;
                    }
                } else
                {
                    var result = new JsonResult("Formato invalido");
                    result.StatusCode = 400;
                    return result;
                }
            }
            catch(Exception err)
            {
                return new JsonResult("Hubo un error al editar la calle. " + err);
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
        public JsonResult Delete(int id)
        {
            try
            {
                var streetToDelete = AllStreets.FirstOrDefault(x => x.Id == id);
                if (streetToDelete != null)
                {
                    AllStreets.Remove(streetToDelete);
                    return new JsonResult("Calle eliminada");
                }
                else
                {
                    var result = new JsonResult("Calle no encontrada");
                    result.StatusCode = 404;
                    return result;
                }
            }
            catch (Exception err)
            {
                return new JsonResult("Hubo un error al eliminar la calle. " + err);
            }
        }
    }
}
