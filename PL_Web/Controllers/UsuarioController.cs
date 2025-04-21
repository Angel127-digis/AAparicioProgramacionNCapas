using ML;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Xml;
using System.Xml.Linq;

namespace PL_Web.Controllers
{
    public class UsuarioController : Controller
    {
        // GET: Usuario

        [HttpGet]
        //[NonAction]
        public ActionResult GetAll()
        {
            ML.Usuario usuario = new ML.Usuario();
            usuario.Rol = new ML.Rol();
            usuario.Nombre = usuario.Nombre == null ? "" : usuario.Nombre;
            usuario.ApellidoPaterno = usuario.ApellidoPaterno == null ? "" : usuario.ApellidoPaterno;
            usuario.ApellidoMaterno = usuario.ApellidoMaterno == null ? "" : usuario.ApellidoMaterno;
            usuario.Rol.IdRol = usuario.Rol.IdRol == 0 ? 0 : usuario.Rol.IdRol;

            //CRUDSOAP.CRUDSOAPClient obj = new CRUDSOAP.CRUDSOAPClient();
            //var result = obj.GetAll(usuario);

            //ML.Result result = BL.Usuario.GetAllEF(usuario);

            ML.Result result = new ML.Result();

            try
            {
                using (var usuarioClient = new HttpClient())
                {
                    //RecuperarBaseAddress de AppSettings
                    String endpoint = ConfigurationManager.AppSettings["UsuarioApi"].ToString();
                    usuarioClient.BaseAddress = new Uri(endpoint);




                    var responseTask = usuarioClient.GetAsync("GetAll");


                    responseTask.Wait(); //abrir otro hilo

                    var resultServicio = responseTask.Result;

                    if (resultServicio.IsSuccessStatusCode) //200 - 299
                    {
                        result.Objects = new List<object>();

                        var readTask = resultServicio.Content.ReadAsAsync<ML.Result>();
                        readTask.Wait();

                        foreach (var resultItem in readTask.Result.Objects)
                        {
                            ML.Usuario usuarioLista = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.Usuario>(resultItem.ToString());
                            result.Objects.Add(usuarioLista);
                        }
                        result.Correct = true;
                    }
                }
            }
            catch (Exception ex)
            {
                result.ErrorMessage = ex.Message;
                result.Correct = false;
                result.Ex = ex;
            }

            if (result.Correct)
            {
                //Obtuvo toda la informacion
                usuario.Usuarios = result.Objects.ToList();

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


                //ML.Result result = BL.Usuario.GetByIdEF(IdUsuario.Value);
                //usuario = (ML.Usuario)result.Object;

                ML.Result result = GetByIdWebAPI(IdUsuario.Value);
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
            //usuario.Rol = new ML.Rol();
            //CRUDSOAP.CRUDSOAPClient obj = new CRUDSOAP.CRUDSOAPClient();




            //if (ModelState.IsValid)
            //{
                HttpPostedFileBase file = Request.Files["inputFileImagen"];
                if (file != null && file.ContentLength > 0)
                {
                    usuario.Imagen = ConvertirAArrayBytes(file);
                }
                if (usuario.IdUsuario == 0)
                {
                    //Add con web services
                    //obj.Add(usuario);
                    //BL.Usuario.AddEF(usuario);

                    //API REST
                    using (var usuarioClient = new HttpClient())
                    {
                        String endpoint = ConfigurationManager.AppSettings["UsuarioApi"].ToString();
                        usuarioClient.BaseAddress = new Uri(endpoint);
                        //client.BaseAddress = new Uri("http://localhost:40840//api/");

                        //HTTP POST
                        var postTask = usuarioClient.PostAsJsonAsync<ML.Usuario>("AddApi", usuario); //Serializar
                        postTask.Wait();

                        var result = postTask.Result;
                        if (result.IsSuccessStatusCode)
                        {
                            //MODAL
                            //ViewBag.ErrorMessage

                            ViewBag.Errores = "Registro agregado correctamente";
                            return PartialView("_ModalErrores");
                            //return RedirectToAction("GetAll");
                        }
                    }
                }
                else
                {
                    //Actualizar con web services
                    //obj.Update(usuario);
                    //BL.Usuario.UpdateEF(usuario);

                    using (var usuarioClient = new HttpClient())
                    {
                        String endpoint = ConfigurationManager.AppSettings["UsuarioApi"].ToString();
                        usuarioClient.BaseAddress = new Uri(endpoint);

                        //HTTP POST
                        var postTask = usuarioClient.PutAsJsonAsync<ML.Usuario>("Update", usuario);
                        postTask.Wait();

                        var result = postTask.Result;
                        if (result.IsSuccessStatusCode)
                        {
                            ViewBag.Errores = "Registro actualizado correctamente";
                            return PartialView("_ModalErrores");
                        }
                        return View("GetAll");
                    }

                }

            //}
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
        public ActionResult Delete1(int idUsuario)
        {
            //Eliminar con web services
            CRUDSOAP.CRUDSOAPClient obj = new CRUDSOAP.CRUDSOAPClient();
            var result = obj.Delete(idUsuario);

            //ML.Result result = BL.Usuario.DeleteEF(idUsuario);
            if (result.Correct)
            {
                ViewBag.Errores = "Registro elimindado correctamente";
                return PartialView("_ModalErrores");
            }
            else
            {
                ViewBag.Errores = "Hubo un error " + result.ErrorMessage;
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

        [HttpGet]
        //[NonAction]
        public ActionResult GetAll1()
        {
            string action = "http://tempuri.org/ICRUDSOAP/GetAll";
            string url = "http://localhost:52372/CRUDSOAP.svc"; // Cambia a la URL del servicio
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Headers.Add("SOAPAction", action);
            request.ContentType = "text/xml;charset=\"utf-8\"";
            request.Accept = "text/xml";
            request.Method = "POST"; // Cambia a POST ya que estás usando un servicio SOAP

            // Crear el sobre SOAP
            string soapEnvelope = $@"<?xml version=""1.0"" encoding=""utf-8""?>
            <soapenv:Envelope xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:tem=""http://tempuri.org/"" xmlns:ml=""http://schemas.datacontract.org/2004/07/ML"" xmlns:arr=""http://schemas.microsoft.com/2003/10/Serialization/Arrays"">
               <soapenv:Header/>
               <soapenv:Body>
                  <tem:GetAll>
                     <!--Optional:-->
                     <tem:usuario>
                        <!--Optional:-->
                        <ml:ApellidoMaterno></ml:ApellidoMaterno>
                        <!--Optional:-->
                        <ml:ApellidoPaterno></ml:ApellidoPaterno>
                        <!--Optional:-->
                        <!--Optional:-->
                        <!--Optional:-->
            
                        <ml:Nombre></ml:Nombre>
                        <!--Optional:-->
            
                        <!--Optional:-->
                        <ml:Rol>
                           <!--Optional:-->
                           <ml:IdRol>0</ml:IdRol>
                           <!--Optional:-->
            
                           <!--Optional:-->
                        </ml:Rol>
                        <!--Optional:-->
                     </tem:usuario>
                  </tem:GetAll>
               </soapenv:Body>
            </soapenv:Envelope>";

            // Enviar la solicitud
            using (Stream stream = request.GetRequestStream())
            {
                byte[] content = Encoding.UTF8.GetBytes(soapEnvelope);
                stream.Write(content, 0, content.Length);
            }

            // Obtener la respuesta
            try
            {
                using (WebResponse response = request.GetResponse())
                {
                    using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                    {
                        string result = reader.ReadToEnd();

                        // Deserializar el XML
                        var usuario = GetAllUsuarios(result); // Captura el objeto completo

                        return View(usuario); // Asegúrate de que tu vista esté lista para recibir este objeto
                    }
                }
            }
            catch (WebException ex)
            {
                ViewBag.Error = ex.Message; // Para mostrar en la vista si es necesario
            }

            return View(); // Devuelve la vista
        }

        private ML.Usuario GetAllUsuarios(string xml)
        {
            //var usuario1 = new ML.Usuario();
            ML.Result result = new ML.Result();
            result.Objects = new List<object>();

            var xdoc = XDocument.Parse(xml);

            // Acceder a GetAllUsuarioResult
            var objects = xdoc.Descendants("{http://schemas.microsoft.com/2003/10/Serialization/Arrays}anyType");

            foreach (var elem in objects)
            {
                var usuario = new ML.Usuario();
                usuario.Rol = new ML.Rol();
                usuario.Direccion = new ML.Direccion();
                usuario.Direccion.Colonia = new ML.Colonia();
                usuario.Direccion.Colonia.Municipio = new ML.Municipio();
                usuario.Direccion.Colonia.Municipio.Estado = new ML.Estado();

                // Manejo de IdUsuario null  
                //byte[]
                int idUsuario;
                int.TryParse(elem.Element("{http://schemas.datacontract.org/2004/07/ML}IdUsuario")?.Value, out idUsuario); //0
                usuario.IdUsuario = idUsuario;

                var rol = elem.Element("{http://schemas.datacontract.org/2004/07/ML}Rol"); //0 o N
                usuario.Rol.Nombre = (string)(rol.Element("{http://schemas.datacontract.org/2004/07/ML}Nombre")?.Value ?? string.Empty);

                var direccion = elem.Descendants("{http://schemas.datacontract.org/2004/07/ML}Direccion").FirstOrDefault(); //0 o N
                usuario.Direccion.Calle = (direccion.Element("{http://schemas.datacontract.org/2004/07/ML}Calle")?.Value);
                usuario.Direccion.NumeroExterior = (direccion.Element("{http://schemas.datacontract.org/2004/07/ML}NumeroExterior")?.Value);
                usuario.Direccion.NumeroInterior = (direccion.Element("{http://schemas.datacontract.org/2004/07/ML}NumeroInterior")?.Value);


                var colonia = elem.Descendants("{http://schemas.datacontract.org/2004/07/ML}Colonia").FirstOrDefault(); //0 o N
                usuario.Direccion.Colonia.IdColonia = Convert.ToInt16((colonia.Element("{http://schemas.datacontract.org/2004/07/ML}IdColonia"))?.Value);

                var municipio = elem.Descendants("{http://schemas.datacontract.org/2004/07/ML}Municipio").FirstOrDefault(); //0 o N
                usuario.Direccion.Colonia.Municipio.IdMunicipio = Convert.ToInt16((municipio.Element("{http://schemas.datacontract.org/2004/07/ML}IdMunicipio"))?.Value);

                var estado = elem.Descendants("{http://schemas.datacontract.org/2004/07/ML}Municipio").FirstOrDefault(); //0 o N
                usuario.Direccion.Colonia.Municipio.Estado.IdEstado = Convert.ToInt16((estado.Element("{http://schemas.datacontract.org/2004/07/ML}IdMunicipio"))?.Value);

                // Acceso a otros campos
                usuario.UserName = (string)(elem.Element("{http://schemas.datacontract.org/2004/07/ML}UserName")?.Value ?? string.Empty);
                usuario.Nombre = (string)(elem.Element("{http://schemas.datacontract.org/2004/07/ML}Nombre")?.Value ?? string.Empty);
                usuario.ApellidoPaterno = (string)(elem.Element("{http://schemas.datacontract.org/2004/07/ML}ApellidoPaterno")?.Value ?? string.Empty);
                usuario.ApellidoMaterno = (string)(elem.Element("{http://schemas.datacontract.org/2004/07/ML}ApellidoMaterno")?.Value ?? string.Empty);
                usuario.Email = (string)(elem.Element("{http://schemas.datacontract.org/2004/07/ML}Email")?.Value ?? string.Empty);
                usuario.Password = (string)(elem.Element("{http://schemas.datacontract.org/2004/07/ML}Password")?.Value ?? string.Empty);
                usuario.FechaNacimiento = (string)(elem.Element("{http://schemas.datacontract.org/2004/07/ML}FechaNacimiento")?.Value ?? string.Empty);
                usuario.Sexo = (string)(elem.Element("{http://schemas.datacontract.org/2004/07/ML}Sexo")?.Value ?? string.Empty);
                usuario.Telefono = (string)(elem.Element("{http://schemas.datacontract.org/2004/07/ML}Telefono")?.Value ?? string.Empty);
                usuario.Celular = (string)(elem.Element("{http://schemas.datacontract.org/2004/07/ML}Celular")?.Value ?? string.Empty);
                usuario.CURP = (string)(elem.Element("{http://schemas.datacontract.org/2004/07/ML}CURP")?.Value ?? string.Empty);



                result.Objects.Add(usuario);
            }

            ML.Usuario usuarioRegistro = new ML.Usuario();
            usuarioRegistro.Usuarios = result.Objects;
            usuarioRegistro.Rol = new ML.Rol();
            ML.Result resultDDL = BL.Rol.GetAll();

            usuarioRegistro.Rol.Rols = resultDDL.Objects;
            return usuarioRegistro; // Devuelve el objeto completo
        }

        //---------------------Acuatlizar y Agregar--------------------------------//
        [HttpPost]
        public ActionResult FormSOAP(ML.Usuario usuario)
        {
            string url = "http://localhost:52372/CRUDSOAP.svc"; // URL del servicio
            string soapEnvelope;
            string action; // Declarar la variable action aquí

            // Verificar si IdUsuario es null o 0 (o algún valor que determines como "nuevo")
            if (usuario.IdUsuario == 0)
            {
                // Crear el sobre SOAP para agregar un nuevo usuario
                action = "http://tempuri.org/ICRUDSOAP/Add";
                soapEnvelope = $@"<?xml version=""1.0"" encoding=""utf-8""?>
                    <soapenv:Envelope xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:tem=""http://tempuri.org/"" xmlns:ml=""http://schemas.datacontract.org/2004/07/ML"" xmlns:arr=""http://schemas.microsoft.com/2003/10/Serialization/Arrays"">
                           <soapenv:Header/>
                           <soapenv:Body>
                              <tem:Add>
                                 <!--Optional:-->
                                 <tem:usuario>
                                                                                <!--Optional:-->
                                            <ml:ApellidoMaterno>{usuario.ApellidoMaterno}</ml:ApellidoMaterno>
                                            <!--Optional:-->
                                            <ml:ApellidoPaterno>{usuario.ApellidoPaterno}</ml:ApellidoPaterno>
                                            <!--Optional:-->
                                            <ml:CURP>{usuario.CURP}</ml:CURP>
                                            <!--Optional:-->
                                            <ml:Celular>{usuario.Celular}</ml:Celular>
                                            <!--Optional:-->
                                            <ml:Direccion>
                                               <!--Optional:-->
                                               <ml:Calle>{usuario.Direccion.Calle}</ml:Calle>
                                               <!--Optional:-->
                                               <ml:Colonia>
                                                  <!--Optional:-->
               
                                                  <!--Optional:-->

                                                  <!--Optional:-->
                                                  <ml:IdColonia>{usuario.Direccion.Colonia.IdColonia}</ml:IdColonia>
                                                  <!--Optional:-->
                                                  <ml:Municipio>
                                                     <!--Optional:-->
                                                     <ml:Estado>
                                                        <!--Optional:-->

                                                        <!--Optional:-->
                                                        <ml:IdEstado>{usuario.Direccion.Colonia.Municipio.Estado.IdEstado}</ml:IdEstado>
                                                        <!--Optional:-->
                                                        <ml:Nombre></ml:Nombre>
                                                     </ml:Estado>
                                                     <!--Optional:-->
                                                     <ml:IdMunicipio>{usuario.Direccion.Colonia.Municipio.IdMunicipio}</ml:IdMunicipio>
                                                     <!--Optional:-->

                                                     <!--Optional:-->
                                                     <ml:Nombre></ml:Nombre>
                                                  </ml:Municipio>
                                                  <!--Optional:-->
                                                  <ml:Nombre></ml:Nombre>
                                               </ml:Colonia>
                                               <!--Optional:-->

                                               <!--Optional:-->
              
                                               <!--Optional:-->
                                               <ml:NumeroExterior>{usuario.Direccion.NumeroExterior}</ml:NumeroExterior>
                                               <!--Optional:-->
                                               <ml:NumeroInterior>{usuario.Direccion.NumeroInterior}</ml:NumeroInterior>
                                            </ml:Direccion>
                                            <!--Optional:-->
                                            <ml:Email>{usuario.Email}</ml:Email>
                                            <!--Optional:-->
                                            <ml:Estatus>1</ml:Estatus>
                                            <!--Optional:-->
                                            <ml:FechaNacimiento>{usuario.FechaNacimiento}</ml:FechaNacimiento>
                                            <!--Optional id usuario:-->
                                            

                                            <!--Optional Imagen:-->
            
                                            <!--Optional:-->
                                            <ml:Nombre>{usuario.Nombre}</ml:Nombre>
                                            <!--Optional:-->
                                            <ml:Password>{usuario.Password}</ml:Password>
                                            <!--Optional:-->
                                            <ml:Rol>
                                               <!--Optional:-->
                                               <ml:IdRol>{usuario.Rol.IdRol}</ml:IdRol>
                                               <!--Optional:-->
                                               <ml:Nombre></ml:Nombre>
                                               <!--Optional:-->

                                            </ml:Rol>
                                            <!--Optional:-->
                                            <ml:Sexo>{usuario.Sexo}</ml:Sexo>
                                            <!--Optional:-->
                                            <ml:Telefono>{usuario.Telefono}</ml:Telefono>
                                            <!--Optional:-->
                                            <ml:UserName>{usuario.UserName}</ml:UserName>
                                            <!--Optional:-->
                                 </tem:usuario>
                              </tem:Add>
                           </soapenv:Body>
                        </soapenv:Envelope>";
            }
            else
            {
                // Crear el sobre SOAP para actualizar un usuario existente
                action = "http://tempuri.org/ICRUDSOAP/Update";
                soapEnvelope = $@"<?xml version=""1.0"" encoding=""utf-8""?>
                                <soapenv:Envelope xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:tem=""http://tempuri.org/"" xmlns:ml=""http://schemas.datacontract.org/2004/07/ML"" xmlns:arr=""http://schemas.microsoft.com/2003/10/Serialization/Arrays"">
                                   <soapenv:Header/>
                                   <soapenv:Body>
                                      <tem:Update>
                                         <!--Optional:-->
                                         <tem:usuario>
                                            <!--Optional:-->
                                            <ml:ApellidoMaterno>{usuario.ApellidoMaterno}</ml:ApellidoMaterno>
                                            <!--Optional:-->
                                            <ml:ApellidoPaterno>{usuario.ApellidoPaterno}</ml:ApellidoPaterno>
                                            <!--Optional:-->
                                            <ml:CURP>{usuario.CURP}</ml:CURP>
                                            <!--Optional:-->
                                            <ml:Celular>{usuario.Celular}</ml:Celular>
                                            <!--Optional:-->
                                            <ml:Direccion>
                                               <!--Optional:-->
                                               <ml:Calle>{usuario.Direccion.Calle}</ml:Calle>
                                               <!--Optional:-->
                                               <ml:Colonia>
                                                  <!--Optional:-->
               
                                                  <!--Optional:-->

                                                  <!--Optional:-->
                                                  <ml:IdColonia>{usuario.Direccion.Colonia.IdColonia}</ml:IdColonia>
                                                  <!--Optional:-->
                                                  <ml:Municipio>
                                                     <!--Optional:-->
                                                     <ml:Estado>
                                                        <!--Optional:-->

                                                        <!--Optional:-->
                                                        <ml:IdEstado>{usuario.Direccion.Colonia.Municipio.Estado.IdEstado}</ml:IdEstado>
                                                        <!--Optional:-->
                                                        <ml:Nombre></ml:Nombre>
                                                     </ml:Estado>
                                                     <!--Optional:-->
                                                     <ml:IdMunicipio>{usuario.Direccion.Colonia.Municipio.IdMunicipio}</ml:IdMunicipio>
                                                     <!--Optional:-->

                                                     <!--Optional:-->
                                                     <ml:Nombre></ml:Nombre>
                                                  </ml:Municipio>
                                                  <!--Optional:-->
                                                  <ml:Nombre></ml:Nombre>
                                               </ml:Colonia>
                                               <!--Optional:-->

                                               <!--Optional:-->
              
                                               <!--Optional:-->
                                               <ml:NumeroExterior>{usuario.Direccion.NumeroExterior}</ml:NumeroExterior>
                                               <!--Optional:-->
                                               <ml:NumeroInterior>{usuario.Direccion.NumeroInterior}</ml:NumeroInterior>
                                            </ml:Direccion>
                                            <!--Optional:-->
                                            <ml:Email>{usuario.Email}</ml:Email>
                                            <!--Optional:-->
                                            <ml:Estatus>1</ml:Estatus>
                                            <!--Optional:-->
                                            <ml:FechaNacimiento>{usuario.FechaNacimiento}</ml:FechaNacimiento>
                                            <!--Optional id usuario:-->
                                            <ml:IdUsuario>{usuario.IdUsuario}</ml:IdUsuario>

                                            <!--Optional Imagen:-->
            
                                            <!--Optional:-->
                                            <ml:Nombre>{usuario.Nombre}</ml:Nombre>
                                            <!--Optional:-->
                                            <ml:Password>{usuario.Password}</ml:Password>
                                            <!--Optional:-->
                                            <ml:Rol>
                                               <!--Optional:-->
                                               <ml:IdRol>{usuario.Rol.IdRol}</ml:IdRol>
                                               <!--Optional:-->
                                               <ml:Nombre></ml:Nombre>
                                               <!--Optional:-->

                                            </ml:Rol>
                                            <!--Optional:-->
                                            <ml:Sexo>{usuario.Sexo}</ml:Sexo>
                                            <!--Optional:-->
                                            <ml:Telefono>{usuario.Telefono}</ml:Telefono>
                                            <!--Optional:-->
                                            <ml:UserName>{usuario.UserName}</ml:UserName>
                                            <!--Optional:-->

                                         </tem:usuario>

                                      </tem:Update>
                                   </soapenv:Body>
                                </soapenv:Envelope>";
            }

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Headers.Add("SOAPAction", action); // Aquí ya existe la variable action
            request.ContentType = "text/xml;charset=\"utf-8\"";
            request.Accept = "text/xml";
            request.Method = "POST";

            // Enviar la solicitud
            using (Stream stream = request.GetRequestStream())
            {
                byte[] content = Encoding.UTF8.GetBytes(soapEnvelope);
                stream.Write(content, 0, content.Length);
            }

            // Obtener la respuesta
            try
            {
                using (WebResponse response = request.GetResponse())
                {
                    using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                    {
                        string result = reader.ReadToEnd();
                        Console.WriteLine("Respuesta SOAP:");
                        Console.WriteLine(result);
                        // Aquí puedes manejar la respuesta según sea necesario
                    }
                }
            }
            catch (WebException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                ViewBag.Error = ex.Message; // Para mostrar en la vista si es necesario
            }

            return RedirectToAction("GetAll"); // Redirigir a la lista de usuarios después de agregar o actualizar
        }

        [HttpGet]
        public ActionResult FormSOAP(int? IdUsuario)
        {
            ML.Usuario usuario = null;

            if (IdUsuario.HasValue)
            {
                // Obtener el usuario por ID
                string action = "http://tempuri.org/ICRUDSOAP/GetById";
                string url = "http://localhost:52372/CRUDSOAP.svc";

                // Crear el sobre SOAP para obtener un usuario por su ID
                string soapEnvelope = $@"<?xml version=""1.0"" encoding=""utf-8""?>
<soapenv:Envelope xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:tem=""http://tempuri.org/"">
   <soapenv:Header/>
   <soapenv:Body>
      <tem:GetById>
         <!--Optional:-->
         <tem:idUsuario>{IdUsuario}</tem:idUsuario>
      </tem:GetById>
   </soapenv:Body>
</soapenv:Envelope>";

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.Headers.Add("SOAPAction", action);
                request.ContentType = "text/xml;charset=\"utf-8\"";
                request.Accept = "text/xml";
                request.Method = "POST";

                // Enviar la solicitud
                using (Stream stream = request.GetRequestStream())
                {
                    byte[] content = Encoding.UTF8.GetBytes(soapEnvelope);
                    stream.Write(content, 0, content.Length);
                }

                // Obtener la respuesta
                try
                {
                    using (WebResponse response = request.GetResponse())
                    {
                        using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                        {
                            string result = reader.ReadToEnd();
                            Console.WriteLine("Respuesta SOAP:");
                            Console.WriteLine(result);

                            // Deserializar el usuario
                            usuario = GetUsuarioById(result);
                        }
                    }
                }
                catch (WebException ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                    ViewBag.Error = ex.Message; // Para mostrar en la vista si es necesario
                }
            }

            return View(usuario); // Devuelve la vista con el usuario si existe
        }

        private ML.Usuario GetUsuarioById(string xml)
        {
            var xdoc = XDocument.Parse(xml);
            // Acceder a GetUsuarioByIdResult usando el namespace correcto
            var elem = xdoc.Descendants().FirstOrDefault(e =>
                e.Name.LocalName == "Object" &&
                e.GetDefaultNamespace().NamespaceName == "http://tempuri.org/");

            if (elem != null)
            {
                ML.Usuario usuario = new ML.Usuario();
                usuario.Rol = new ML.Rol();
                usuario.Direccion = new Direccion();
                usuario.Direccion.Colonia = new Colonia();
                usuario.Direccion.Colonia.Municipio = new Municipio();
                usuario.Direccion.Colonia.Municipio.Estado = new Estado();

                int idUsuario;
                int.TryParse(elem.Element("{http://schemas.datacontract.org/2004/07/ML}IdUsuario")?.Value, out idUsuario); //0
                usuario.IdUsuario = idUsuario;

                var rol = elem.Element("{http://schemas.datacontract.org/2004/07/ML}Rol"); //0 o N
                usuario.Rol.Nombre = (string)(rol.Element("{http://schemas.datacontract.org/2004/07/ML}Nombre")?.Value ?? string.Empty);

                var direccion = elem.Descendants("{http://schemas.datacontract.org/2004/07/ML}Direccion").FirstOrDefault(); //0 o N
                usuario.Direccion.Calle = (direccion.Element("{http://schemas.datacontract.org/2004/07/ML}Calle")?.Value);
                usuario.Direccion.NumeroExterior = (direccion.Element("{http://schemas.datacontract.org/2004/07/ML}NumeroExterior")?.Value);
                usuario.Direccion.NumeroInterior = (direccion.Element("{http://schemas.datacontract.org/2004/07/ML}NumeroInterior")?.Value);


                var colonia = elem.Descendants("{http://schemas.datacontract.org/2004/07/ML}Colonia").FirstOrDefault(); //0 o N
                usuario.Direccion.Colonia.IdColonia = Convert.ToInt16((colonia.Element("{http://schemas.datacontract.org/2004/07/ML}IdColonia"))?.Value);

                var municipio = elem.Descendants("{http://schemas.datacontract.org/2004/07/ML}Municipio").FirstOrDefault(); //0 o N
                usuario.Direccion.Colonia.Municipio.IdMunicipio = Convert.ToInt16((municipio.Element("{http://schemas.datacontract.org/2004/07/ML}IdMunicipio"))?.Value);

                var estado = elem.Descendants("{http://schemas.datacontract.org/2004/07/ML}Municipio").FirstOrDefault(); //0 o N
                usuario.Direccion.Colonia.Municipio.Estado.IdEstado = Convert.ToInt16((estado.Element("{http://schemas.datacontract.org/2004/07/ML}IdMunicipio"))?.Value);

                // Acceso a otros campos
                usuario.UserName = (string)(elem.Element("{http://schemas.datacontract.org/2004/07/ML}UserName")?.Value ?? string.Empty);
                usuario.Nombre = (string)(elem.Element("{http://schemas.datacontract.org/2004/07/ML}Nombre")?.Value ?? string.Empty);
                usuario.ApellidoPaterno = (string)(elem.Element("{http://schemas.datacontract.org/2004/07/ML}ApellidoPaterno")?.Value ?? string.Empty);
                usuario.ApellidoMaterno = (string)(elem.Element("{http://schemas.datacontract.org/2004/07/ML}ApellidoMaterno")?.Value ?? string.Empty);
                usuario.Email = (string)(elem.Element("{http://schemas.datacontract.org/2004/07/ML}Email")?.Value ?? string.Empty);
                usuario.Password = (string)(elem.Element("{http://schemas.datacontract.org/2004/07/ML}Password")?.Value ?? string.Empty);
                usuario.FechaNacimiento = (string)(elem.Element("{http://schemas.datacontract.org/2004/07/ML}FechaNacimiento")?.Value ?? string.Empty);
                usuario.Sexo = (string)(elem.Element("{http://schemas.datacontract.org/2004/07/ML}Sexo")?.Value ?? string.Empty);
                usuario.Telefono = (string)(elem.Element("{http://schemas.datacontract.org/2004/07/ML}Telefono")?.Value ?? string.Empty);
                usuario.Celular = (string)(elem.Element("{http://schemas.datacontract.org/2004/07/ML}Celular")?.Value ?? string.Empty);
                usuario.CURP = (string)(elem.Element("{http://schemas.datacontract.org/2004/07/ML}CURP")?.Value ?? string.Empty);

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

                return usuario;

            }

            return null; // O lanzar una excepción si no se encontró el usuario
        }

        //-----------------Delete con SOAP------------------------//
        [NonAction]
        public ActionResult Delete12(int idUsuario)
        {

            // Obtener el usuario por ID
            string action = "http://tempuri.org/ICRUDSOAP/Delete";
            string url = "http://localhost:52372/CRUDSOAP.svc";

            // Crear el sobre SOAP para obtener un usuario por su ID
            string soapEnvelope = $@"<?xml version=""1.0"" encoding=""utf-8""?>
<soapenv:Envelope xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:tem=""http://tempuri.org/"">
   <soapenv:Header/>
   <soapenv:Body>
      <tem:Delete>
         <!--Optional:-->
         <tem:idUsuario>{idUsuario}</tem:idUsuario>
      </tem:Delete>
   </soapenv:Body>
</soapenv:Envelope>";

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Headers.Add("SOAPAction", action);
            request.ContentType = "text/xml;charset=\"utf-8\"";
            request.Accept = "text/xml";
            request.Method = "POST";

            // Enviar la solicitud
            using (Stream stream = request.GetRequestStream())
            {
                byte[] content = Encoding.UTF8.GetBytes(soapEnvelope);
                stream.Write(content, 0, content.Length);
            }

            // Obtener la respuesta
            try
            {
                using (WebResponse response = request.GetResponse())
                {
                    using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                    {
                        string result = reader.ReadToEnd();
                        Console.WriteLine("Respuesta SOAP:");
                        Console.WriteLine(result);

                        // Deserializar el usuario
                        //GetUsuarioById(result);
                    }
                }
            }
            catch (WebException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                ViewBag.Error = ex.Message; // Para mostrar en la vista si es necesario
            }


            return RedirectToAction("GetAll"); ; // Devuelve la vista con el usuario si existe
        }

        public static ML.Result GetByIdWebAPI(int IdUsuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                string urlAPI = System.Configuration.ConfigurationManager.AppSettings["UsuarioApi"];
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(urlAPI);
                    var responseTask = client.GetAsync("GetById/" + IdUsuario);
                    responseTask.Wait();
                    var resultAPI = responseTask.Result;
                    if (resultAPI.IsSuccessStatusCode)
                    {
                        var readTask = resultAPI.Content.ReadAsAsync<ML.Result>();
                        readTask.Wait();
                        ML.Usuario resultItemList = new ML.Usuario();
                        resultItemList = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.Usuario>(readTask.Result.Object.ToString());
                        result.Object = resultItemList;

                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No existen registros de usuario";
                    }

                }
            }

            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;

            }

            return result;
        }

        //[HttpPost]
        public ActionResult Delete(int IdUsuario)
        {
            ML.Result resultListProduct = new ML.Result();
            int id = IdUsuario;
            using (var client = new HttpClient())
            {
                String endpoint = ConfigurationManager.AppSettings["UsuarioApi"].ToString();
                client.BaseAddress = new Uri(endpoint);

                //HTTP POST
                var postTask = client.DeleteAsync("Delete/" + id);
                postTask.Wait();

                var result = postTask.Result;
                //if (result.IsSuccessStatusCode)
                //{
                //    //resultListProduct = BL.SubCategoria.GetEAll();
                //    return RedirectToAction("GetAll");
                //}

                //ML.Result result = BL.Usuario.DeleteEF(idUsuario);
                if (result.IsSuccessStatusCode)
                {
                    ViewBag.Errores = "Registro elimindado correctamente";
                    return PartialView("_ModalErrores");
                }
                else
                {
                    ViewBag.Errores = "Hubo un error ";
                    return PartialView("_ModalErrores");
                }
            }


            //resultListProduct = BL.SubCategoria.GetEAll();

            //return View("GetAll");

        }
    }
}