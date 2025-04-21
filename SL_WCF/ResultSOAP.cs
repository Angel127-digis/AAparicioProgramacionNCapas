using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace SL_WCF
{
    [DataContract]
    public class ResultSOAP
    {
        [DataMember]
        public bool Correct { get; set; } //TRUE = Proceso hecho correctamente
                                          // FALSE = Hubo un error
        [DataMember]
        public string ErrorMessage { get; set; } //Cual es el error especifico
        [DataMember]
        public Exception Ex { get; set; } //traer TODO el error a detalle
        [DataMember]
        public object Object { get; set; } //MOSTRAR 1 registro
        [DataMember]
        public List<object> Objects { get; set; } //MOSTRAR N Registros
    }
}