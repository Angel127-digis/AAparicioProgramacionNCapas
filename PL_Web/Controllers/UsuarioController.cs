using ML;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PL_Web.Controllers
{
    public class UsuarioController : Controller
    {
        // GET: Usuario

        [HttpGet]
        public ActionResult GetAll()
        {
            ML.Usuario usuario = new ML.Usuario();
            usuario.Rol = new ML.Rol();
            usuario.Nombre = usuario.Nombre == null ? "" : usuario.Nombre;
            usuario.ApellidoPaterno = usuario.ApellidoPaterno == null ? "" : usuario.ApellidoPaterno;
            usuario.ApellidoMaterno = usuario.ApellidoMaterno == null ? "" : usuario.ApellidoMaterno;
            usuario.Rol.IdRol = usuario.Rol.IdRol == 0 ? 0 : usuario.Rol.IdRol;
            ML.Result result = BL.Usuario.GetAllEF(usuario);

            if (result.Correct)
            {
                //Obtuvo toda la informacion
                usuario.Usuarios = result.Objects;

            }
            else
            {
                usuario.Usuarios = new List<object>();
            }
            ML.Result resultDDL = BL.Rol.GetAll();
            usuario.Rol.Rols = resultDDL.Objects;
            ViewBag.ErroresExcel = TempData["ErrorExcel"];
            return View(usuario); //Solo puede pasar un valor
        }
        [HttpPost]
        public ActionResult GetAll(ML.Usuario usuario)
        {

            //usuario.Rol = new ML.Rol();
            usuario.Nombre = usuario.Nombre == null ? "" : usuario.Nombre;
            usuario.ApellidoPaterno = usuario.ApellidoPaterno == null ? "" : usuario.ApellidoPaterno;
            usuario.ApellidoMaterno = usuario.ApellidoMaterno == null ? "" : usuario.ApellidoMaterno;
            usuario.Rol.IdRol = usuario.Rol.IdRol == 0 ? 0 : usuario.Rol.IdRol;

            ML.Result result = BL.Usuario.GetAllEF(usuario);
            if (result.Correct)
            {
                //Obtuvo toda la informacion
                usuario.Usuarios = result.Objects;
            }
            else
            {
                usuario.Usuarios = new List<object>();
            }
            ML.Result resultDDL = BL.Rol.GetAll();
            usuario.Rol.Rols = resultDDL.Objects;
            return View(usuario); //Solo puede pasar un valor
        }


        [HttpGet]
        public ActionResult Form(int? IdUsuario)
        {
            ML.Usuario usuario = new ML.Usuario();
            //ML.Result result = new ML.Result();
            usuario.Rol = new ML.Rol();
            usuario.Direccion = new ML.Direccion();
            usuario.Direccion.Colonia = new ML.Colonia();
            usuario.Direccion.Colonia.Municipio = new ML.Municipio();
            usuario.Direccion.Colonia.Municipio.Estado = new ML.Estado();


            if (IdUsuario == null)
            {
                //Usuario es vacio    
                usuario.Rol = new ML.Rol();//Abrir puerta

                //Direccion
            }
            else
            {
                //Actualizar
                //Lleno
                //GetById


                ML.Result result = BL.Usuario.GetByIdEF(IdUsuario.Value);
                usuario = (ML.Usuario)result.Object;

                ML.Result resultDDLMunicipio = new ML.Result();
                resultDDLMunicipio = BL.Municipio.GetByIdEstado(usuario.Direccion.Colonia.Municipio.Estado.IdEstado);
                usuario.Direccion.Colonia.Municipio.Municipios = resultDDLMunicipio.Objects;


                ML.Result resultColonia = new ML.Result();
                resultColonia = BL.Colonia.GetByIdMunicipio(usuario.Direccion.Colonia.Municipio.IdMunicipio);
                usuario.Direccion.Colonia.Colonias = resultColonia.Objects;
                //result.Objet
                //UNBOXING
            }




            ML.Result resultDDL = BL.Rol.GetAll();
            usuario.Rol.Rols = resultDDL.Objects;
            ML.Result resultDDLEstado = BL.Estado.GetAll();
            usuario.Direccion.Colonia.Municipio.Estado.Estados = resultDDLEstado.Objects;

            return View(usuario);
        }
        public byte[] ConvertirAArrayBytes(HttpPostedFileBase foto)
        {
            System.IO.BinaryReader reader = new System.IO.BinaryReader(foto.InputStream);
            byte[] data = reader.ReadBytes((int)foto.ContentLength);
            return data;
        }

        [HttpPost]
        public ActionResult Form(ML.Usuario usuario)
        {
            usuario.Rol = new ML.Rol();

            if (ModelState.IsValid)
            {
                HttpPostedFileBase file = Request.Files["inputFileImagen"];
                if (file != null && file.ContentLength > 0)
                {
                    usuario.Imagen = ConvertirAArrayBytes(file);
                }
                if (usuario.IdUsuario == 0)
                {
                    BL.Usuario.AddEF(usuario);
                    ViewBag.ErroresExcel = "Registro agregado correctamente";
                    return PartialView("_ModalErrores");
                }
                else
                {
                    BL.Usuario.UpdateEF(usuario);
                    ViewBag.ErroresExcel = "Registro actualizado correctamente";
                    return PartialView("_ModalErrores");
                }

            }
            //ML.Result result = BL.Usuario.GetByIdEF(IdUsuario.Value);
            //usuario = (ML.Usuario)result.Object;




            ML.Result resultDDLMunicipio = new ML.Result();
            resultDDLMunicipio = BL.Municipio.GetByIdEstado(usuario.Direccion.Colonia.Municipio.Estado.IdEstado);
            usuario.Direccion.Colonia.Municipio.Municipios = resultDDLMunicipio.Objects;


            ML.Result resultColonia = new ML.Result();
            resultColonia = BL.Colonia.GetByIdMunicipio(usuario.Direccion.Colonia.Municipio.IdMunicipio);
            usuario.Direccion.Colonia.Colonias = resultColonia.Objects;

            ML.Result resultDDL = BL.Rol.GetAll();
            usuario.Rol.Rols = resultDDL.Objects;
            ML.Result resultDDLEstado = BL.Estado.GetAll();
            usuario.Direccion.Colonia.Municipio.Estado.Estados = resultDDLEstado.Objects;
            return View(usuario);
        }
        public ActionResult Delete(int idUsuario)
        {
            ML.Result result = BL.Usuario.DeleteEF(idUsuario);
            if (result.Correct)
            {
                ViewBag.ErroresExcel = "Registro elimindado correctamente";
                return PartialView("_ModalErrores");
            }
            else
            {
                ViewBag.ErroresExcel = "Hubo un error " + result.ErrorMessage;
                return PartialView("_ModalErrores");
            }

            //return RedirectToAction("GetAll");
        }
        [HttpPost]
        public JsonResult CambiarStatus(int IdUsuario, bool status)
        {
            ML.Result result = BL.Usuario.CambiarStatus(IdUsuario, status);

            return Json(result, JsonRequestBehavior.AllowGet);

        }
        [HttpGet]
        public JsonResult GetByIdMunicipio(int idEstado)
        {
            ML.Result result = BL.Municipio.GetByIdEstado(idEstado);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult GetByIdColonia(int IdMunicipio)
        {
            ML.Result result = BL.Colonia.GetByIdMunicipio(IdMunicipio);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult CargaMasiva()
        {
            if (Session["RutaExcel"] == null)
            {

                HttpPostedFileBase excelUsuario = Request.Files["Excel"];

                string extensionPermitida = ".xlsx";

                if (excelUsuario.ContentLength > 0) //El usuario me envio un archivo
                {
                    String extensionObtenida = Path.GetExtension(excelUsuario.FileName);

                    if (extensionObtenida == extensionPermitida)
                    {
                        String ruta = Server.MapPath("~/CargaMasiva/") + Path.GetFileNameWithoutExtension(excelUsuario.FileName) + "-" + DateTime.Now.ToString("ddMMyyyyHmmssff") + ".xlsx";

                        if (!System.IO.File.Exists(ruta))
                        {
                            excelUsuario.SaveAs(ruta);

                            String cadenaConexion = ConfigurationManager.ConnectionStrings["OleDbConnection"] + ruta;

                            ML.Result result = BL.Usuario.LeerExcel(cadenaConexion);

                            if (result.Objects.Count > 0)
                            {
                                ML.Result resultValidacion = BL.Usuario.ValidarExcel(result.Objects);
                                if (resultValidacion.Objects.Count > 0)
                                {
                                    //Hubo un error
                                    //Mostrar una vista, una tabla
                                    ViewBag.ErroresExcel = resultValidacion.Objects;
                                    //TempData["ErrorExcel"] = resultValidacion.Objects;
                                    return PartialView("_Modal");
                                }
                                else
                                {
                                    Session["RutaExcel"] = ruta;
                                }
                            }
                        }
                        else
                        {
                            //Vista Parcial
                            //Vuelve a cargar el Archivo porque ya existe
                            ViewBag.ErroresExcel = "EL archivo ya existe";
                            return PartialView("_ModalErrores");
                        }
                    }
                    else
                    {
                        //Vista Parcial
                        //El archivo no es un Excel
                        ViewBag.ErroresExcel = "EL archivo que ingreso no es un excel";
                        return PartialView("_ModalErrores");
                    }
                }
                else
                {
                    //Vista Parciales
                    //No me diste ningun archivo
                    ViewBag.ErroresExcel = "No ingresaste ningun archivo";
                    return PartialView("_ModalErrores");
                }
            }
            else
            {
                //ya lei y valide un excel
                //INSERTAR
                String cadenaConexion = ConfigurationManager.ConnectionStrings["OleDbConnection"] + Session["RutaExcel"].ToString();

                ML.Result resultLeer = BL.Usuario.LeerExcel(cadenaConexion);

                if (resultLeer.Objects.Count > 0)
                {
                    //todo lo leyo bien

                    ML.ResultSQL resultFilasError = new ML.ResultSQL();
                    //resultSQL.Resultados = new List<object>();
                    resultFilasError.Resultados = new List<object>();
                    int registros = 0;
                    int registroIncorrecto = 1;
                    foreach (ML.Usuario usuario in resultLeer.Objects)
                    {
                        ML.ResultSQL resultSQL = new ML.ResultSQL();

                        ML.Result resultInsertar = BL.Usuario.AddEF(usuario);
                        if (!resultInsertar.Correct)
                        {
                            //Mostrar error salido
                            resultSQL.Incorrectos = registroIncorrecto;
                            resultFilasError.Resultados.Add(resultSQL);
                            registroIncorrecto++;
                        }

                        registros++;
                    }

                    //Cuantos inserts fueron correctos
                    //cuantos inserts fueron incorrectos
                    //cuales estuvieron mal
                    var registrosCorrectos = registros - resultFilasError.Resultados.Count;
                    var registrosIncorrectos = resultFilasError.Resultados.Count;
                    var informacion = new List<object>();
                    informacion.Add(registrosCorrectos);
                    informacion.Add(registrosIncorrectos);

                    foreach (ML.ResultSQL result in resultFilasError.Resultados)
                    {
                        informacion.Add(result.Incorrectos);
                    }
                    ViewBag.Errores = informacion;
                    Session["RutaExcel"] = null;
                    return PartialView("_ModalErrores");
                }
            }
            return RedirectToAction("GetAll");
            //return View();
        }
    }
}