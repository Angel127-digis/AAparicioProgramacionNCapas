using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SL_WebAPI.Controllers
{
    [RoutePrefix("api/Calculadora")]
    public class CalculadoraController : ApiController
    {
        [HttpPost]
        [Route("Suma")]
        public IHttpActionResult Suma(int numeroUno, int numeroDos) {

            int result = numeroUno + numeroDos;
            return Content(HttpStatusCode.OK, result);
        }
        [HttpPost]
        [Route("Resta")]
        public IHttpActionResult Resta(int numeroUno, int numeroDos)
        {

            int result = numeroUno - numeroDos;
            return Ok(result);
        }
        [HttpPost]
        [Route("M")]
        public IHttpActionResult Multiplicacion(int numeroUno, int numeroDos)
        {

            int result = numeroUno * numeroDos;
            return Ok(result);
        }
        [HttpPost]
        [Route("D")]
        public IHttpActionResult Division(int numeroUno, int numeroDos)
        {

            int result = numeroUno / numeroDos;
            return Ok(result);
        }
    }
}
