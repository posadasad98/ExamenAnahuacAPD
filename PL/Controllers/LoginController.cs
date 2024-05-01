using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using ML;

namespace PL.Controllers
{
    public class LoginController : Controller
    {
        public ActionResult Login()
        {
            ML.Usuario usuario = new ML.Usuario();
            usuario.Rol = new ML.Rol();


            Dictionary<string, object> rolVersion = BL.Rol.GetAllRol();
            usuario.Rol = (ML.Rol)rolVersion["Rol"];

            return View(usuario);
        }


        [HttpPost]
        public ActionResult form(Usuario usuario, string password, int idRol)
        {
            // Encripta la contrase�a
            byte[] encryptedPassword = EncryptPassword(password);
            usuario.Password = encryptedPassword;

            // Agrega el usuario con su rol
            Dictionary<string, object> result = BL.Usuario.Add(usuario, idRol);
            bool resultado = (bool)result["Resultado"];
            if (resultado)
            {
                ViewBag.Mensaje = "El Usuario ha sido insertado";
                return PartialView("Modal");
            }
            else
            {
                string exepcion = (string)result["Exepcion"];
                ViewBag.Mensaje = "El Usuario no se pudo registrar " + exepcion;
                return PartialView("Modal");
            }
        }

        [HttpPost]
        public ActionResult Login(string email, string password)
        {
            // Encripta la contrase�a ingresada por el usuario
            byte[] encryptedPassword = EncryptPassword(password);

            // Obtiene el usuario de la base de datos
            Dictionary<string, object> diccionario = BL.Usuario.GetByEmailPassword(email);
            bool resultado = (bool)diccionario["Resultado"];

            if (resultado)
            {
                Usuario usuario = (Usuario)diccionario["Usuario"];

                // Verifica si el email coincide
                if (usuario.Email == email)
                {
                    // Compara las contrase�as encriptadas
                    bool contrase�asCoinciden = usuario.Password.SequenceEqual(encryptedPassword);

                    if (contrase�asCoinciden)
                    {
                        // Las contrase�as coinciden, la autenticaci�n es exitosa
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        // Las contrase�as no coinciden
                        ViewBag.Mensaje = "El usuario y/o contrase�a no son v�lidos";
                        return PartialView("Modal");
                    }
                }
                else
                {
                    // El email no coincide
                    ViewBag.Mensaje = "El usuario y/o contrase�a no son v�lidos";
                    return PartialView("Modal");
                }
            }
            else
            {
                // El m�todo no funcion� correctamente
                ViewBag.Mensaje = "El usuario y/o contrase�a no son v�lidos";
                return PartialView("Modal");
            }
        }

        private byte[] EncryptPassword(string password)
        {
            // Encripta la contrase�a usando SHA256
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));
                return bytes;
            }
        }
    }
}
