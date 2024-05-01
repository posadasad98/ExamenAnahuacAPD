using Microsoft.AspNetCore.Mvc;
using ML;

namespace SL.Controllers
{
    public class CuentoController : ControllerBase
    {
        [HttpGet]
        [Route("api/Cuento/GetAll")]
        public IActionResult GetAll()
        {
            ML.Cuento cuento = new ML.Cuento();
            Dictionary<string, object> resultado = BL.Cuento.GetAllCuento();
            bool result = (bool)resultado["Resultado"];
            if (result)
            {
                return Ok(resultado);
            }
            else
            {
                return BadRequest("Error");
            }
        }

        [HttpGet]
        [Route("api/Cuento/GetById/{id}")]
        public IActionResult GetById(int id)
        {
            ML.Cuento cuento = new ML.Cuento();
            Dictionary<string, object> resultado = BL.Cuento.GetByIdCuento(id);
            bool result = (bool)resultado["Resultado"];
            if (result)
            {
                cuento = (ML.Cuento)resultado["Cuento"];
                return Ok(resultado);
            }
            else
            {
                return BadRequest("Error: " + resultado["Excepcion"]);
            }
        }

        [HttpDelete]
        [Route("api/Cuento/Delete/{id}")]
        [Produces("application/json")]
        public IActionResult Delete(int id)
        {
            Dictionary<string, object> resultado = BL.Cuento.DeleteCuento(id);
            bool result = (bool)resultado["Resultado"];

            if (result)
            {
                return Ok(resultado);
            }
            else
            {
                return BadRequest(new { ErrorMessage = "Error al eliminar el cuento.", Exception = resultado["Excepcion"] });
            }
        }

        [HttpPost]
        [Route("api/Cuento/Add")]
        public IActionResult Add([FromBody] ML.Cuento cuento)
        {
          
            Dictionary<string, object> resultado = BL.Cuento.AddCuento(cuento);

            bool result = (bool)resultado["Resultado"];

            if (result)
            {
                return Ok(resultado);
            }
            else
            {
                return BadRequest("Error al agregar el cuento: " + resultado["Excepcion"]);
            }
        }

        [HttpPost]
        [Route("api/Cuento/Update")]
        public IActionResult Update([FromBody] ML.Cuento cuento)
        {
            if (cuento == null)
            {
                return BadRequest("El cuento proporcionado es nulo.");
            }

            Dictionary<string, object> resultado = BL.Cuento.UpdateCuento(cuento);

            bool result = (bool)resultado["Resultado"];

            if (result)
            {
                return Ok("Cuento actualizado exitosamente.");
            }
            else
            {
                return BadRequest("Error al actualizar el cuento: " + resultado["Excepcion"]);
            }
        }



    }
}
