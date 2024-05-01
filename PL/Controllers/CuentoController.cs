using Azure.Core;
using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    public class CuentoController : Controller
    {
        private readonly IConfiguration _configuration;
        public CuentoController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        [HttpGet]
        public ActionResult GetAll()
        {
            ML.Cuento cuentoResult = new ML.Cuento();
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(_configuration["URL"]);
                string endpoint = _configuration["URL"] + $"/Cuento/GetAll";
                var responseTask = client.GetAsync(endpoint);

                responseTask.Wait(); // Llamada al metodo de la api
                ML.Cuento cuento1 = new ML.Cuento();

                var respuesta = responseTask.Result;
                if (respuesta.IsSuccessStatusCode)
                {
                    var readTask = respuesta.Content.ReadAsAsync<Dictionary<string, object>>();
                    readTask.Wait();
                    if (readTask.Result.TryGetValue("Cuento", out object cuentoObject) && cuentoObject != null)
                    {
                        cuento1 = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.Cuento>(cuentoObject.ToString());
                    }
                    cuentoResult.Cuentos = new List<object>();
                    foreach (var jsonCuento in cuento1.Cuentos)
                    {
                        ML.Cuento cuentoDes = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.Cuento>(jsonCuento.ToString());
                        cuentoResult.Cuentos.Add(cuentoDes);
                    }

                }


            }


            if (cuentoResult.Cuentos != null)
            {
                return View(cuentoResult);
            }
            else
            {

                return View();
            }

        }
        [HttpGet]
        [Produces("application/json")]
        public ActionResult Delete(int IdCuento, int IdEscritor)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(_configuration["URL"]);
                var responseTask = client.DeleteAsync($"api/Cuento/Delete/{IdCuento}");
                responseTask.Wait();
                var respuesta = responseTask.Result;
                if (respuesta.IsSuccessStatusCode)
                {
                    var readTask = respuesta.Content.ReadAsAsync<Dictionary<string, object>>();
                    readTask.Wait();
                    var result = readTask.Result;

                    bool resultado = (bool)result["Resultado"];
                    if (resultado)
                    {
                        ViewBag.Mensaje = "EL CUENTO HA SIDO ELIMINADO";
                    }
                    else
                    {
                        string errorMessage = result.ContainsKey("ErrorMessage") ? (string)result["ErrorMessage"] : "Error desconocido";
                        ViewBag.Mensaje = errorMessage;
                    }
                }
                else
                {
                    var readTask = respuesta.Content.ReadAsStringAsync();
                    readTask.Wait();
                    string errorMessage = readTask.Result;
                    ViewBag.Mensaje = errorMessage;
                }
                ViewBag.IdEscritor = IdEscritor;
                return PartialView("Modal");
            }
        }
        //[HttpPost]
        //public ActionResult Form(ML.Cuento cuento, IFormFile portada, IFormFile referencias)
        //{
        //    if (portada != null)
        //    {
        //        string base64Image = ConvertToBase64Async(portada);
        //        cuento.Portada = base64Image;
        //    }
        //    if (referencias != null)
        //    {
        //        byte[] pdfBytes = ConvertToByteArrayAsync(referencias);
        //        cuento.Referencias = pdfBytes;
        //    }
        //    if (cuento.IdCuento > 0)
        //    {
        //        Dictionary<string, object> result = new Dictionary<string, object>();
        //        using (HttpClient client = new HttpClient())
        //        {
        //            client.BaseAddress = new Uri(_configuration["URL"]);
        //            var responseTask = client.PutAsJsonAsync($"Cuento/Update/{cuento.IdCuento}", cuento);
        //            responseTask.Wait();
        //            var respuesta = responseTask.Result;
        //            if (respuesta.IsSuccessStatusCode)
        //            {
        //                var readTask = respuesta.Content.ReadAsAsync<Dictionary<string, object>>();
        //                readTask.Wait();
        //                result = readTask.Result;

        //            }
        //        }
        //        bool resultado = (bool)result["Resultado"];

        //        if (resultado == true)
        //        {

        //            ViewBag.Mensaje = "Se ha actualizado el registro";
        //            return PartialView("Modal");

        //        }
        //        else
        //        {
        //            ViewBag.Mensaje = "no se ha actualizado el registro";
        //            return PartialView("Modal");
        //        }


        //    }

        //    else
        //    {
        //        Dictionary<string, object> result = new Dictionary<string, object>();
        //        using (HttpClient client = new HttpClient())
        //        {
        //            client.BaseAddress = new Uri("http://localhost:5236/api");
        //            var responseTask = client.PostAsJsonAsync("Cuento/GetCuentosAsignadosPorEscritor?IdEscritor=", cuento.Escritor.IdEscritor);
        //            responseTask.Wait(); // Llamada al metodo de la api
        //            var responseTask = client.PostAsJsonAsync<ML.Cuento>("/Cuento/Add", cuento);
        //            responseTask.Wait();
        //            var respuesta = responseTask.Result;
        //            if (respuesta.IsSuccessStatusCode)
        //            {
        //                var readTask = respuesta.Content.ReadAsAsync<Dictionary<string, object>>();
        //                readTask.Wait();
        //                result = readTask.Result;

        //            }
        //        }
        //        bool resultado = (bool)result["Resultado"];

        //        if (resultado == true)
        //        {
        //            ViewBag.Mensaje = "El cuento ha sido insertado";
        //            return PartialView("Modal");
        //        }
        //        else
        //        {
        //            ViewBag.Mensaje = "El cuento no se pudo insertar ";
        //            return PartialView("Modal");
        //        }

        //    }




        //}
        [HttpPost]
        public ActionResult Form(ML.Cuento cuento)
        {
            if (cuento == null)
            {
                ViewBag.Mensaje = "Error: No se ha proporcionado información del cuento.";
                return PartialView("Modal");
            }

            try
            {
                if (cuento.IdCuento > 0)
                {
                    Dictionary<string, object> result = BL.Cuento.UpdateCuento(cuento);
                    bool resultado = (bool)result["Resultado"];

                    if (resultado)
                    {
                        ViewBag.Mensaje = "Se ha actualizado el cuento exitosamente.";
                    }
                    else
                    {
                        ViewBag.Mensaje = "No se pudo actualizar el cuento.";
                    }
                }
                else
                {
                    Dictionary<string, object> result = BL.Cuento.AddCuento(cuento);
                    bool resultado = (bool)result["Resultado"];

                    if (resultado)
                    {
                        ViewBag.Mensaje = "El cuento ha sido registrado exitosamente.";
                    }
                    else
                    {
                        ViewBag.Mensaje = "No se pudo registrar el cuento.";
                    }
                }
            }
            catch (Exception ex)
            {
                ViewBag.Mensaje = "Ocurrió un error durante el proceso: " + ex.Message;
            }

            return PartialView("Modal");
        }

        [HttpGet]
        public ActionResult Form(int? idEscritor)
        {
            ML.Cuento cuento = new ML.Cuento();
            cuento.Escritor = new ML.Escritor();
            if (idEscritor.HasValue)
            {
                cuento.Escritor.IdEscritor = idEscritor.Value;
                Dictionary<string, object> generoVersion = BL.Genero.GetAllGenero();
                ML.Genero generoLista = (ML.Genero)generoVersion["Genero"];
                cuento.Genero = generoLista;

            }

             return View(cuento);
        }
        private string ConvertToBase64Async(IFormFile file)
        {
            if (file != null && file.Length > 0)
            {
                using (var memoryStream = new MemoryStream())
                {
                    file.CopyTo(memoryStream);
                    byte[] imageData = memoryStream.ToArray();
                    return Convert.ToBase64String(imageData);
                }
            }
            return null;
        }

        private byte[] ConvertToByteArrayAsync(IFormFile file)
        {
            if (file != null && file.Length > 0)
            {
                using (var memoryStream = new MemoryStream())
                {
                    file.CopyTo(memoryStream);
                    return memoryStream.ToArray();
                }
            }
            return null;
        }



    }
}

