using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Rol

    {
        public static Dictionary<string, object> GetAllRol()
        {
            ML.Rol rol = new ML.Rol();
            string excepcion = "";
            Dictionary<string, object> diccionario = new Dictionary<string, object> { { "Rol", rol }, { "Exepcion", excepcion }, { "Resultado", false } };

            try
            {
                using (DL.ExamenAnahuacApdContext context = new DL.ExamenAnahuacApdContext())

                {
                    var registros = (from Rol in context.Rols
                                     select new
                                     {
                                         IdRol = Rol.IdRol,
                                         TipoRol = Rol.TipoRol
                                     }).ToList();

                    if (registros != null)
                    {
                        rol.Roles = new List<object>();
                        foreach (var registro in registros)
                        {

                            ML.Rol rol1 = new ML.Rol();
                            rol1.IdRol = registro.IdRol;
                            rol1.TipoRol = registro.TipoRol;

                            rol.Roles.Add(rol1);
                        }
                        diccionario["Resultado"] = true;
                        diccionario["Rol"] = rol;
                    }
                }
            }
            catch (Exception ex)
            {
                diccionario["Resultado"] = false;
                diccionario["Excepcion"] = ex.Message;
            }
            return diccionario;
        }
    }
}


