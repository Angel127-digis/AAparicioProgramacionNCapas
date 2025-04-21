using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace SL_WCF
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "CRUDSOAP" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select CRUDSOAP.svc or CRUDSOAP.svc.cs at the Solution Explorer and start debugging.
    public class CRUDSOAP : ICRUDSOAP
    {
        public void DoWork()
        {
        }

        public ResultSOAP Add(ML.Usuario usuario)
        {
            ML.Result result = BL.Usuario.AddEF(usuario);

            return new ResultSOAP
            {
                Correct = result.Correct,
                ErrorMessage = result.ErrorMessage,
                Object = result.Object,
                Objects = result.Objects
            };
        }

        public ResultSOAP Delete(int idUsuario)
        {
            ML.Result result = BL.Usuario.DeleteEF(idUsuario);

            return new ResultSOAP
            {
                Correct = result.Correct,
                ErrorMessage = result.ErrorMessage,
                Object = result.Object,
                Objects = result.Objects
            };
        }

        public ResultSOAP Update(ML.Usuario usuario)
        {
            ML.Result result = BL.Usuario.UpdateEF(usuario);

            return new ResultSOAP
            {
                Correct = result.Correct,
                ErrorMessage = result.ErrorMessage,
                Object = result.Object,
                Objects = result.Objects
            };
        }

        public ResultSOAP GetAll(ML.Usuario usuario)
        {
            ML.Result result = BL.Usuario.GetAllEF(usuario);

            return new ResultSOAP
            {
                Correct = result.Correct,
                ErrorMessage = result.ErrorMessage,
                Object = result.Object,
                Objects = result.Objects
            };
        }

        public ResultSOAP GetById(int idUsuario)
        {
            ML.Result result = BL.Usuario.GetByIdEF(idUsuario);

            return new ResultSOAP
            {
                Correct = result.Correct,
                ErrorMessage = result.ErrorMessage,
                Object = result.Object,
                Objects = result.Objects
            };
        }

    }
}
