using Microsoft.AspNetCore.Mvc;

namespace SL.Controllers
{
    public class EscritorController : ControllerBase
    {
        [HttpGet]
        [Route("api/Escritor/GetAll")]
        public IActionResult GetAll( string nombre =null  , string correoElectronico = null )
        {
            ML.Escritor escritor = new ML.Escritor();
            Dictionary<string, object> resultado = BL.Escritor.GetAll(nombre, correoElectronico);
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
        [Route("api/Escritor/GetById/{id}")]
        public IActionResult GetById(int id)
        {
            ML.Escritor escritor = new ML.Escritor();
            Dictionary<string, object> resultado = BL.Escritor.GetById(id);
            bool result = (bool)resultado["Resultado"];
            if (result)
            {
                escritor = (ML.Escritor)resultado["Escritor"];
                return Ok(escritor);
            }
            else
            {
                return BadRequest("Error: " + resultado["Excepcion"]);
            }
        }

        [HttpDelete]
        [Route("api/Escritor/Delete/{id}")]
        [Produces("application/json")]
        public IActionResult Delete(int id)
        {
            Dictionary<string, object> resultado = BL.Escritor.DeleteEscritor(id);
            bool result = (bool)resultado["Resultado"];

            if (result)
            {
                return Ok(resultado);
            }
            else
            {
                return BadRequest(new { ErrorMessage = "Error al eliminar el escritor.", Exception = resultado["Excepcion"] });
            }
        }

        [HttpPost]
        [Route("api/Escritor/Add")]
        public IActionResult Add([FromBody] ML.Escritor escritor)
        {
            if (escritor == null)
            {
                return BadRequest("El escritor proporcionado es nulo.");
            }

            Dictionary<string, object> resultado = BL.Escritor.AddEscritor(escritor);

            bool result = (bool)resultado["Resultado"];

            if (result)
            {
                return Ok("Escritor agregado exitosamente.");
            }
            else
            {
                return BadRequest("Error al agregar el escritor: " + resultado["Excepcion"]);
            }
        }
        [HttpPost]
        [Route("api/Escritor/Update")]
        public IActionResult Update([FromBody] ML.Escritor escritor)
        {
            if (escritor == null)
            {
                return BadRequest("El escritor proporcionado es nulo.");
            }

            Dictionary<string, object> resultado = BL.Escritor.UpdateEscritor(escritor);

            bool result = (bool)resultado["Resultado"];

            if (result)
            {
                return Ok("Escritor actualizado exitosamente.");
            }
            else
            {
                return BadRequest("Error al actualizar el escritor: " + resultado["Excepcion"]);
            }
        }

        [HttpGet]
        [Route("api/Escritor/GetCuentosAsignadosPorEscritor/{id}")]
        [Produces("application/json")]
        public IActionResult GetCuentosAsignadosPorEscritor(int id)
        {
            try
            {
                // Aquí deberías llamar a tu lógica de negocio para obtener el escritor por su ID
                Dictionary<string, object> resultado = BL.Escritor.GetCuentoAsignadoByIdEscritor(id);

                // Verificar si se encontró el escritor
                if (resultado != null)
                {
                    return Ok(resultado);
                }
                else
                {
                    return NotFound(new { ErrorMessage = "Escritor no encontrado." });
                }
            }
            catch (Exception ex)
            {
                // Manejo de excepciones
                return StatusCode(500, new { ErrorMessage = "Error interno del servidor.", Exception = ex.Message });
            }
        }

    }
}
