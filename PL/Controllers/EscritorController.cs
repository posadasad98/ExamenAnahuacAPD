using BL;
using DL;
using Microsoft.AspNetCore.Mvc;
using ML;
using Newtonsoft.Json;

namespace PL.Controllers
{
    public class EscritorController : Controller
    {
        private readonly IConfiguration _configuration;
        public EscritorController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        [HttpGet]
        public ActionResult GetAll(ML.Escritor escritor)
        {
            ML.Escritor escritorResult = new ML.Escritor();
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(_configuration["URL"]);
                string endpoint = _configuration["URL"] + $"/Escritor/GetAll?nombre={escritor.Nombre}&correoElectronico={escritor.CorreoElectronico}";
                var responseTask = client.GetAsync(endpoint);

                responseTask.Wait();
                ML.Escritor escritor1 = new ML.Escritor();

                var respuesta = responseTask.Result;
                if (respuesta.IsSuccessStatusCode)
                {
                    var readTask = respuesta.Content.ReadAsAsync<Dictionary<string, object>>();
                    readTask.Wait();
                    if (readTask.Result.TryGetValue("Escritor", out object escritorObject) && escritorObject != null)
                    {
                        escritor1 = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.Escritor>(escritorObject.ToString());
                    }
                    escritorResult.Escritors = new List<object>();
                    foreach (var jsonEscritor in escritor1.Escritors)
                    {
                        ML.Escritor escritorDes = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.Escritor>(jsonEscritor.ToString());
                        escritorResult.Escritors.Add(escritorDes);
                    }

                }


            }

            if (escritorResult.Escritors != null)
            {
                return View(escritorResult);
            }
            else
            {

                return View();
            }

        }
        [HttpGet]
        [Produces("application/json")]
        public ActionResult Delete(int IdEscritor)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(_configuration["URL"]);
                var responseTask = client.DeleteAsync($"api/Escritor/Delete/{IdEscritor}");
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
                        ViewBag.Mensaje = "EL USUARIO HA SIDO ELIMINADO";
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

                return PartialView("Modal");
            }
        }
        public static byte[] ConvertToBytes(IFormFile imagen)
        {

            using var fileStream = imagen.OpenReadStream();

            byte[] bytes = new byte[fileStream.Length];
            fileStream.Read(bytes, 0, (int)fileStream.Length);

            return bytes;
        }
        [HttpGet]
        public ActionResult GetById(int IdEscritor)
        {
            ML.Escritor escritorResult = new ML.Escritor();

            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(_configuration["URL"]);
                string endpoint = _configuration["URL"] + $"/Escritor/GetById/{IdEscritor}";
                var responseTask = client.GetAsync(endpoint);

                responseTask.Wait(); // Realiza la llamada al método de la API

                var respuesta = responseTask.Result;
                if (respuesta.IsSuccessStatusCode)
                {
                    var readTask = respuesta.Content.ReadAsAsync<Dictionary<string, object>>();
                    readTask.Wait();
                    if (readTask.Result.TryGetValue("Escritor", out object escritorObject) && escritorObject != null)
                    {
                        // Deserializa el objeto del escritor obtenido en la respuesta
                        escritorResult = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.Escritor>(escritorObject.ToString());
                    }
                }
            }

            // Verifica si se obtuvieron datos del escritor
            if (escritorResult != null)
            {
                return View(escritorResult);
            }
            else
            {
                // Si no se encontró el escritor, muestra una vista vacía o maneja el caso según sea necesario
                return View();
            }
        }

        [HttpGet]
        public ActionResult GetCuentosAsignadosPorEscritor(int IdEscritor)
        {
            ML.Cuento cuentoResult = new ML.Cuento();
            ML.Cuento cuento = new ML.Cuento();
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(_configuration["URL"]);
                string endpoint = _configuration["URL"] + $"/Escritor/GetCuentosAsignadosPorEscritor/{IdEscritor}";
                var responseTask = client.GetAsync(endpoint);

                responseTask.Wait(); // Realiza la llamada al método de la API

                var respuesta = responseTask.Result;
                if (respuesta.IsSuccessStatusCode)
                {
                    var readTask = respuesta.Content.ReadAsAsync<Dictionary<string, object>>();
                    readTask.Wait();
                    if (readTask.Result.TryGetValue("Cuento", out object escritorObject) && escritorObject != null)
                    {
                        // Deserializa el objeto del escritor obtenido en la respuesta
                        cuentoResult = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.Cuento>(escritorObject.ToString());
                    }
                    cuento.Cuentos = new List<object>();
                    foreach (var jsonCuento in cuentoResult.Cuentos)
                    {
                        ML.Cuento cuentoDes = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.Cuento>(jsonCuento.ToString());
                        cuento.Cuentos.Add(cuentoDes);
                    }

                }
            }

            // Verifica si se obtuvieron datos del escritor
            if (cuentoResult != null)
            {
                cuento.Escritor = new ML.Escritor();
                cuento.Escritor.IdEscritor = IdEscritor;
                return View(cuento);
            }
            else
            {
                // Si no se encontró el escritor, muestra una vista vacía o maneja el caso según sea necesario
                return View();
            }
        }
        [HttpPost]
        public ActionResult Form(ML.Escritor escritor)
        {
            if (ModelState.IsValid)
            {
                //HttpPostedFileBase file = Request.Files["amd"];
                //if (file != null)
                //{
                //    usuario.Imagen = ConvertToBytes(file);
                //}
                if (escritor.IdEscritor > 0)
                {
                    //llamar al update
                    Dictionary<string, object> result = BL.Escritor.UpdateEscritor(escritor);
                    bool resultado = (bool)result["Resultado"];

                    if (resultado == true)
                    {

                        ViewBag.Mensaje = "Se ha actualizado el registro";
                        return PartialView("Modal");

                    }
                    else
                    {
                        ViewBag.Mensaje = "no se ha actualizado el registro";
                        return PartialView("Modal");
                    }


                }

                else
                {
                    Dictionary<string, object> result = BL.Escritor.AddEscritor(escritor);
                    bool resultado = (bool)result["Resultado"];

                    if (resultado == true)
                    {
                        ViewBag.Mensaje = "El Usuario ha sido insertado";
                        return PartialView("Modal");
                    }
                    else
                    {
                        //pendiente la alerta               
                        //string exepcion = (string)result["Exepcion"];
                        ViewBag.Mensaje = "El Usuario se pudo registrar "; // + exepcion;
                        return PartialView("Modal");
                    }

                }
            }
            else
            {
                return View();
            }


        }
        [HttpGet]
        public ActionResult Form()
        {


            return View();
        }

    }
}
