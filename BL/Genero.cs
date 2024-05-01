using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Genero
    {
        public static Dictionary<string, object> GetAllGenero()
        {
            ML.Genero genero = new ML.Genero();
            string excepcion = "";
            Dictionary<string, object> diccionario = new Dictionary<string, object> { { "Genero", genero }, { "Exepcion", excepcion }, { "Resultado", false } };

            try
            {
                using (DL.ExamenAnahuacApdContext context = new DL.ExamenAnahuacApdContext())

                {
                    var registros = (from Genero in context.Generos
                                     select new
                                     {
                                         IdGenero = Genero.IdGenero,
                                         NombreGenero = Genero.NombreGenero
                                     }).ToList();

                    if (registros != null)
                    {
                        genero.Generos = new List<object>();
                        foreach (var registro in registros)
                        {

                            ML.Genero genero1 = new ML.Genero();
                            genero1.IdGenero = registro.IdGenero;
                            genero1.NombreGenero = registro.NombreGenero;

                            genero.Generos.Add(genero1);
                        }
                        diccionario["Resultado"] = true;
                        diccionario["Genero"] = genero;
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
