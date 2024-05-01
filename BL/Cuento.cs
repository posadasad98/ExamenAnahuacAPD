using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Cuento
    {
        public static Dictionary<string, object> GetAllCuento()
        {
            ML.Cuento cuento = new ML.Cuento();
            string excepcion = "";
            Dictionary<string, object> diccionario = new Dictionary<string, object> { { "Cuento", cuento }, { "Exepcion", excepcion }, { "Resultado", false } };

            try
            {
                using (DL.ExamenAnahuacApdContext context = new DL.ExamenAnahuacApdContext())

                {
                    var registros = (from Cuento in context.Cuentos
                                     select new
                                     {
                                         IdCuento = Cuento.IdCuento,
                                         IdEscritor = Cuento.IdEscritor,
                                         NombreCuento = Cuento.NombreCuento,
                                         FechaRegistro = Cuento.FechaRegistro,
                                         TextoCuento = Cuento.TextoCuento,
                                         Portada = Cuento.Portada,
                                         Referencias = Cuento.Referencias,
                                         Genero = Cuento.IdGenero

                                     }).ToList();

                    if (registros != null)
                    {
                        cuento.Cuentos = new List<object>();
                        foreach (var registro in registros)
                        {

                            ML.Cuento cuento1 = new ML.Cuento();

                            cuento1.IdCuento = registro.IdCuento;

                            cuento1.Escritor = new ML.Escritor();
                            cuento1.Escritor.IdEscritor = registro.IdEscritor.Value;

                            cuento1.NombreCuento = registro.NombreCuento;
                            cuento1.FechaRegistro = (DateTime)registro.FechaRegistro;
                            cuento1.TextoCuento = registro.TextoCuento;
                            cuento1.Portada = registro.Portada;
                            cuento1.Referencias = registro.Referencias;

                            cuento1.Genero = new ML.Genero();
                            cuento1.Genero.IdGenero = registro.Genero.Value;


                            cuento.Cuentos.Add(cuento1)
;
                        }
                        diccionario["Resultado"] = true;
                        diccionario["Cuento"] = cuento;
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
        public static Dictionary<string, object> GetByIdCuento(int id)
        {
            ML.Cuento cuento = new ML.Cuento();
            string excepcion = "";
            Dictionary<string, object> diccionario = new Dictionary<string, object> { { "Cuento", cuento }, { "Excepcion", excepcion }, { "Resultado", false } };

            try
            {
                using (DL.ExamenAnahuacApdContext context = new DL.ExamenAnahuacApdContext())
                {
                    var registro = context.Cuentos.FirstOrDefault(c => c.IdCuento == id);

                    if (registro != null)
                    {
                        cuento.IdCuento = registro.IdCuento;
                        cuento.Escritor = new ML.Escritor();
                        cuento.Escritor.IdEscritor = registro.IdEscritor.Value;
                        cuento.NombreCuento = registro.NombreCuento;
                        cuento.FechaRegistro = (DateTime)registro.FechaRegistro;
                        cuento.TextoCuento = registro.TextoCuento;
                        cuento.Portada = registro.Portada;
                        cuento.Referencias = registro.Referencias;
                        cuento.Genero = new ML.Genero();
                        cuento.Genero.IdGenero = registro.IdGenero.Value;

                        diccionario["Resultado"] = true;
                        diccionario["Cuento"] = cuento;
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
        public static Dictionary<string, object> AddCuento(ML.Cuento cuento)
        {
            Dictionary<string, object> diccionario = new Dictionary<string, object> { { "Cuento", cuento }, { "Excepcion", null }, { "Resultado", false } };

            try
            {
                using (DL.ExamenAnahuacApdContext context = new DL.ExamenAnahuacApdContext())
                {
                    var result = context.Database.ExecuteSqlRaw($"EXEC AddCuento {cuento.Escritor.IdEscritor}, {cuento.NombreCuento}, {cuento.FechaRegistro}, {cuento.TextoCuento},  {cuento.Genero.IdGenero}");

                    if (result > 0)
                    {
                        diccionario["Resultado"] = true;
                    }
                    else
                    {
                        diccionario["Resultado"] = false;
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
        public static Dictionary<string, object> DeleteCuento(int IdCuento)
        {
            Dictionary<string, object> diccionario = new Dictionary<string, object> { { "Excepcion", null }, { "Resultado", false } };

            try
            {
                using (DL.ExamenAnahuacApdContext context = new DL.ExamenAnahuacApdContext())
                {
                    var result = context.Database.ExecuteSqlInterpolated($"EXEC DeleteCuento {IdCuento}");

                    if (result > 0)
                    {
                        diccionario["Resultado"] = true;
                    }
                    else
                    {
                        diccionario["Resultado"] = false;
                        diccionario["Excepcion"] = "No se encontró ningún cuento con el IdCuento especificado.";
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
        public static Dictionary<string, object> UpdateCuento(ML.Cuento cuento)
        {
            Dictionary<string, object> diccionario = new Dictionary<string, object> { { "Cuento", cuento }, { "Excepcion", null }, { "Resultado", false } };

            try
            {
                using (DL.ExamenAnahuacApdContext context = new DL.ExamenAnahuacApdContext())
                {
                    // Ejecutar procedimiento almacenado o SQL para actualizar un cuento
                    // Asumiendo que el procedimiento almacenado se llama "UpdateCuento" y recibe los parámetros necesarios

                    var result = context.Database.ExecuteSqlInterpolated($"EXEC UpdateCuento {cuento.IdCuento}, {cuento.NombreCuento},{cuento.Escritor.IdEscritor}, {cuento.FechaRegistro}, {cuento.TextoCuento}, {cuento.Portada}, {cuento.Referencias}, {cuento.Genero.IdGenero}");

                    if (result > 0)
                    {
                        diccionario["Resultado"] = true;
                    }
                    else
                    {
                        diccionario["Resultado"] = false;
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


