using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SL_WebAPI.Controllers
{
    [RoutePrefix("api/Usuario")]
    public class UsuarioAPIController : ApiController
    {
        [HttpGet]
        [Route("GetAll")]
        public IHttpActionResult GetAll()
        {
            ML.Usuario usuario = new ML.Usuario();
            usuario.Rol = new ML.Rol();
            usuario.Nombre = "";
            usuario.ApellidoMaterno = "";
            usuario.ApellidoPaterno = "";
            usuario.Rol.IdRol = 0;

            var result = BL.Usuario.GetAllEF(usuario);
            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return NotFound();
            }
        }
        [HttpGet]
        [Route("GetById/{IdUsuario}")]
        public IHttpActionResult GetById(int IdUsuario)
        {
            var result = BL.Usuario.GetByIdEF(IdUsuario);
            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return NotFound();
            }
        }
        [Route("AddApi")]
        [HttpPost]
        public IHttpActionResult AddApi([FromBody] ML.Usuario usuario)
        {
            var result = BL.Usuario.AddEF(usuario);

            if (result.Correct)
            {

                return Ok(result);
            }
            else
            {
                return NotFound();
            }
        }

        [Route("Delete/{IdUsuario}")]
        [HttpDelete]
        public IHttpActionResult Delete(int IdUsuario)
        {
            var result = BL.Usuario.DeleteEF(IdUsuario);

            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return NotFound();
            }
        }

        [Route("Update")]
        [HttpPut]
        public IHttpActionResult Update([FromBody] ML.Usuario usuario)
        {
            var result = BL.Usuario.UpdateEF(usuario);

            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return NotFound();
            }
        }
    }
}
