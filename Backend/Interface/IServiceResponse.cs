using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace IA_Reconhecimento_Facial.Interface
{
    public interface IServiceResponse<T>
    {
        HttpStatusCode StatusCode { get; set; }

        string Message { get; set; }

        T Retorno { get; set; }
    }
}
