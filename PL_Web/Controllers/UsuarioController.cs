using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
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

        public byte[] ConvertirAArrayBytes(HttpPostedFileBase foto)
        {
            System.IO.BinaryReader reader = new System.IO.BinaryReader(foto.InputStream);
            byte[] data = reader.ReadBytes((int)foto.ContentLength);
            return data;
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

        [HttpPost]
        public ActionResult Form(ML.Usuario usuario)
        {
            HttpPostedFileBase file = Request.Files["inputFileImagen"];
            if (file != null && file.ContentLength > 0)
            {
                usuario.Imagen = ConvertirAArrayBytes(file);
            }
            if (usuario.IdUsuario == 0)
            {
                BL.Usuario.AddEF(usuario);

            }
            else
            {
                BL.Usuario.UpdateEF(usuario);
            }

            return RedirectToAction("GetAll");
        }
        public ActionResult Delete(int idUsuario)
        {
            ML.Result result = BL.Usuario.DeleteEF(idUsuario);
            if (result.Correct)
            {
                Console.WriteLine("Registro elimindado correctamente");
            }
            else
            {
                Console.WriteLine("Hubo un error: " + result.ErrorMessage);
            }

            return RedirectToAction("GetAll");
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

    }
}