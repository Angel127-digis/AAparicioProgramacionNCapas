using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PL_Web.Controllers
{
    public class CRUDJsController : Controller
    {
        // GET: CRUDJs
        public ActionResult GetAll()
        {
            return View();
        }

        [HttpGet]
        public JsonResult GetAllJs()
        {
            ML.Usuario usuario = new ML.Usuario();
            usuario.Rol = new ML.Rol();
            usuario.Nombre = "";
            usuario.ApellidoPaterno = "";
            usuario.ApellidoMaterno = "";
            usuario.Rol.IdRol = 0;
            ML.Result result = BL.Usuario.GetAllEF(usuario);

            JsonResult jsonResult = Json(result, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;

            return jsonResult;
        }

        [HttpGet]
        public JsonResult DDLRol()
        {
            ML.Result result = BL.Rol.GetAll();
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult DDLEstado()
        {
            ML.Result result = BL.Estado.GetAll();
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult MunicipioGetByIdEstado(int IdEstado)
        {
            ML.Result result = BL.Municipio.GetByIdEstado(IdEstado);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult ColoniaGetByIdMunicipio(int IdMunicipio)
        {
            ML.Result result = BL.Colonia.GetByIdMunicipio(IdMunicipio);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]

        //public byte[] ConvertirAArrayBytes(HttpPostedFileBase foto)
        //{
        //    System.IO.BinaryReader reader = new System.IO.BinaryReader(foto.InputStream);
        //    byte[] data = reader.ReadBytes((int)foto.ContentLength);
        //    return data;
        //}
        public JsonResult UsuarioAdd(ML.Usuario usuario)
        {

            string dataUrl = usuario.ImagenBase64;

            // Extrae solo el Base64 (eliminando el prefijo)
            string base64String = dataUrl.Split(',')[1]; // "ABC123..."

            // Convierte a byte[]
            usuario.Imagen = Convert.FromBase64String(base64String);

            //HttpPostedFileBase file = Request.Files["inputFileImagen"];
            //usuario.Imagen = ConvertirAArrayBytes(file);
            ML.Result result = BL.Usuario.AddEF(usuario);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult GetByIdUsuario(int idUsuario)
        {
            ML.Result result = BL.Usuario.GetByIdEF(idUsuario);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
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

        [HttpGet]
        public ActionResult Form(int? idUsuario)
        {
            ML.Usuario usuario = new ML.Usuario();
            //ML.Result result = new ML.Result();
            usuario.Rol = new ML.Rol();
            usuario.Direccion = new ML.Direccion();
            usuario.Direccion.Colonia = new ML.Colonia();
            usuario.Direccion.Colonia.Municipio = new ML.Municipio();
            usuario.Direccion.Colonia.Municipio.Estado = new ML.Estado();


            if (idUsuario == null)
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

                ML.Result result = BL.Usuario.GetByIdEF(idUsuario.Value);
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

        [HttpPost]
        public ActionResult Form(ML.Usuario usuario)
        {
            usuario.Rol = new ML.Rol();

            if (ModelState.IsValid)
            {
                HttpPostedFileBase file = Request.Files["inputFileImagen"];
                if (file != null && file.ContentLength > 0)
                {
                    //usuario.Imagen = ConvertirAArrayBytes(file);
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
    }
}