using Microsoft.Azure.CognitiveServices.Vision.Face;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IA_Reconhecimento_Facial.Interface
{
    public interface IFaceRecognitionService
    {
        public FaceClient GetFaceClient();
        public Task<IServiceResponse<bool>> VerificaRosto(string imagePath);
        public Task<IServiceResponse<IEnumerable<string>>> RetonaSentimentos(string imagePath);
        public Task<IServiceResponse<Dictionary<string, int>>> RetornarGeneros(string imagePath);
        public Task<IServiceResponse<double>> RetornarMediaIdade(string imagePath);

    }
}
