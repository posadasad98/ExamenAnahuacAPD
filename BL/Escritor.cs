using DL;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Escritor
    {
        public static Dictionary<string, object> GetAll(string nombre = null, string correoElectronico = null)
        {
            ML.Escritor escritor = new ML.Escritor();
            string excepcion = "";
            Dictionary<string, object> diccionario = new Dictionary<string, object> { { "Escritor", escritor }, { "Exepcion", excepcion }, { "Resultado", false } };

            try
            {
                using (DL.ExamenAnahuacApdContext context = new DL.ExamenAnahuacApdContext())
                {
                    var query = from escritorEntity in context.Escritors
                                select escritorEntity;

                    if (!string.IsNullOrEmpty(nombre))
                    {
                        query = query.Where(e => e.Nombre.Contains(nombre));
                    }

                    if (!string.IsNullOrEmpty(correoElectronico))
                    {
                        query = query.Where(e => e.CorreoElectronico.Contains(correoElectronico));
                    }

                    var registros = query.Select(e => new
                    {
                        IdEscritor = e.IdEscritor,
                        Nombre = e.Nombre,
                        ApellidoPaterno = e.ApellidoPaterno,
                        ApellidoMaterno = e.ApellidoMaterno,
                        CorreoeLectronico = e.CorreoElectronico,
                        Telefono = e.Telefono,
                        Foto = e.Foto
                    }).ToList();

                    if (registros != null)
                    {
                        escritor.Escritors = new List<object>();
                        foreach (var registro in registros)
                        {
                            ML.Escritor escritor1 = new ML.Escritor();

                            escritor1.IdEscritor = registro.IdEscritor;
                            escritor1.Nombre = registro.Nombre;
                            escritor1.ApellidoPaterno = registro.ApellidoPaterno;
                            escritor1.ApellidoMaterno = registro.ApellidoMaterno;
                            escritor1.CorreoElectronico = registro.CorreoeLectronico;
                            escritor1.Telefono = registro.Telefono;
                            escritor1.Foto = registro.Foto;

                            escritor.Escritors.Add(escritor1);
                        }
                        diccionario["Resultado"] = true;
                        diccionario["Escritor"] = escritor;
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
        public static Dictionary<string, object> DeleteEscritor(int IdEscritor)
        {
            Dictionary<string, object> diccionario = new Dictionary<string, object> { { "Excepcion", null }, { "Resultado", false } };

            try
            {
                using (DL.ExamenAnahuacApdContext context = new DL.ExamenAnahuacApdContext())
                {
                    var result = context.Database.ExecuteSqlInterpolated($"EXEC DeleteEscritor {IdEscritor}");

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
        public static Dictionary<string, object> GetById(int id)
        {
            ML.Escritor escritor = new ML.Escritor();
            string excepcion = "";
            Dictionary<string, object> diccionario = new Dictionary<string, object> { { "Escritor", escritor }, { "Excepcion", excepcion }, { "Resultado", false } };

            try
            {
                using (DL.ExamenAnahuacApdContext context = new DL.ExamenAnahuacApdContext())
                {
                    var registro = context.Escritors.FirstOrDefault(e => e.IdEscritor == id);

                    if (registro != null)
                    {
                        escritor.IdEscritor = registro.IdEscritor;
                        escritor.Nombre = registro.Nombre;
                        escritor.ApellidoPaterno = registro.ApellidoPaterno;
                        escritor.ApellidoMaterno = registro.ApellidoMaterno;
                        escritor.CorreoElectronico = registro.CorreoElectronico;
                        escritor.Telefono = registro.Telefono;
                        escritor.Foto = registro.Foto;

                        diccionario["Resultado"] = true;
                        diccionario["Escritor"] = escritor;
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
        public static Dictionary<string, object> AddEscritor(ML.Escritor escritor)
        {
            Dictionary<string, object> diccionario = new Dictionary<string, object> { { "Escritor", escritor }, { "Excepcion", null }, { "Resultado", false } };

            try
            {
                using (DL.ExamenAnahuacApdContext context = new DL.ExamenAnahuacApdContext())
                {
                    var result = context.Database.ExecuteSqlInterpolated($"EXEC AddEscritor '{escritor.Nombre}', '{escritor.ApellidoPaterno}', '{escritor.ApellidoMaterno}', '{escritor.Telefono}', '{escritor.CorreoElectronico}', '{escritor.Foto}'");
                 
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
        public static Dictionary<string, object> UpdateEscritor(ML.Escritor escritor)
        {
            Dictionary<string, object> diccionario = new Dictionary<string, object> { { "Escritor", escritor }, { "Excepcion", null }, { "Resultado", false } };

            try
            {
                using (DL.ExamenAnahuacApdContext context = new DL.ExamenAnahuacApdContext())
                {
                    var result = context.Database.ExecuteSqlInterpolated($"EXEC UpdateEscritor {escritor.IdEscritor}, {escritor.Nombre}, {escritor.ApellidoPaterno}, {escritor.ApellidoMaterno}, {escritor.Telefono}, {escritor.CorreoElectronico}, {escritor.Foto}");

                    if (result != 0)
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
        public static Dictionary<string, object> GetCuentoAsignadoByIdEscritor(int IdEscritor)
        {
            ML.Cuento cuento = new ML.Cuento();
            string excepcion = "";
            Dictionary<string, object> diccionario = new Dictionary<string, object> { { "Cuento", cuento }, { "Excepcion", excepcion }, { "Resultado", false } };

            try
            {
                using (DL.ExamenAnahuacApdContext context = new DL.ExamenAnahuacApdContext())
                {
                    var registros = context.Cuentos.Where(c => c.IdEscritor == IdEscritor).ToList();

                    if (registros != null && registros.Count > 0)
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
                            cuento1.Genero.IdGenero = registro.IdGenero.Value;

                            cuento.Cuentos.Add(cuento1);

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
    }
}
