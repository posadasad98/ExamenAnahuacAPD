using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Usuario
    {
        public static Dictionary<string, object> Add(ML.Usuario usuario, int IdRol)
        {
            string exepcion = "";
            Dictionary<string, object> diccionario = new Dictionary<string, object> { { "Exepcion", exepcion }, { "Resultado", false } };

            try
            {
                using (DL.ExamenAnahuacApdContext context = new DL.ExamenAnahuacApdContext())
                {
                    int filasAfectadas = context.Database.ExecuteSqlRaw($"AddUsuario '{usuario.NombreUsuario}','{usuario.Email}','{usuario.Rol.IdRol}'," + $"@Password", new SqlParameter("@Password",usuario.Password));

                    if (filasAfectadas > 0)
                    {
                        diccionario["Resultado"] = true;
                        diccionario["Usuario"] = usuario;
                    }
                    else
                    {
                    }
                }
            }
            catch (Exception ex)
            {
                diccionario["Resultado"] = false;
                diccionario["Exepcion"] = ex.Message;
            }
            return diccionario;
        }
        public static Dictionary<string, object> GetByEmailPassword(string email)
        {
            Dictionary<string, object> diccionario = new Dictionary<string, object> { { "Exepcion", "" }, { "Resultado", false }, { "Usuario", null } };
            try
            {
                using (DL.ExamenAnahuacApdContext context = new DL.ExamenAnahuacApdContext())
                {
                    var registro = (from usario in context.Usuarios

                                    where usario.Email == email

                                    select new
                                    {

                                        Email = usario.Email,
                                        Password = usario.Password

                                    }).FirstOrDefault();

                    if (registro != null)
                    {
                        ML.Usuario user = new ML.Usuario();

                        user.Email = registro.Email;
                        user.Password = registro.Password;

                        diccionario["Resultado"] = true;
                        diccionario["Usuario"] = user;
                    }
                    else
                    {

                    }
                }

            }
            catch (Exception ex)
            {
                diccionario["Exepcion"] = ex;
            }
            return diccionario;
        }
    }
}
