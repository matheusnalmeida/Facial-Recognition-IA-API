using IA_Reconhecimento_Facial.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace IA_Reconhecimento_Facial.Service.Util
{
    public class ServiceResponse<T> : IServiceResponse<T>
    {
        public HttpStatusCode StatusCode { get; set; }

        public string Message { get; set; }

        public T Retorno { get; set; }
    }
}
