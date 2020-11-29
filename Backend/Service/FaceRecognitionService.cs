using Microsoft.Azure.CognitiveServices.Vision.Face;
using System;
using System.Collections.Generic;
using System.Linq;
using static System.Environment;
using System.Threading.Tasks;
using IA_Reconhecimento_Facial.Interface;
using Microsoft.Azure.CognitiveServices.Vision.Face.Models;
using IA_Reconhecimento_Facial.Service.Util;
using System.Net;
using System.Reflection;

namespace IA_Reconhecimento_Facial.Service
{
    public class FaceRecognitionService : IFaceRecognitionService
    {
        public FaceClient GetFaceClient()
        {
            var serviceEndpoint = GetEnvironmentVariable("COGNITIVE_SERVICE_ENDPOINT");
            var subscriptionKey = GetEnvironmentVariable("COGNITIVE_SERVICE_KEY");
            var credential = new ApiKeyServiceClientCredentials(subscriptionKey);
            return new FaceClient(credential) { Endpoint = serviceEndpoint };
        }

        public async Task<IServiceResponse<bool>> VerificaRosto(string imagePath)
        {
            var faceClient = GetFaceClient();

            var result = await faceClient.Face.DetectWithUrlAsync(imagePath);

            return new ServiceResponse<bool>
            {
                StatusCode = HttpStatusCode.OK,
                Message = result.Count != 0 ? string.Format("Foram encontrados {0} rosto(s) na imagem.",result.Count) : "Não foram encontrados rostos na imagem.", 
                Retorno = result.Count != 0
            };
        }

        private string RetornaMaiorSentimento(Emotion emotion)
        {
            string maiorSentimento = "Não definido";
            double maiorNumero = 0;

            PropertyInfo[] info = emotion.GetType().GetProperties();
            foreach (PropertyInfo propInfo in info)
            {
                if (propInfo.PropertyType == typeof(double))
                {
                    double propValue = (double)(propInfo.GetValue(emotion, null));
                    if (propValue > maiorNumero)
                    {
                        maiorNumero = propValue;
                        switch (propInfo.Name)
                        {
                            case ("Anger"):
                                maiorSentimento = "Raiva";
                                break;
                            case ("Contempt"):
                                maiorSentimento = "Desprezo";
                                break;
                            case ("Disgust"):
                                maiorSentimento = "Desgosto";
                                break;
                            case ("Fear"):
                                maiorSentimento = "Medo";
                                break;
                            case ("Happiness"):
                                maiorSentimento = "Felicidade";
                                break;
                            case ("Neutral"):
                                maiorSentimento = "Neutro";
                                break;
                            case ("Sadness"):
                                maiorSentimento = "Tristeza";
                                break;
                            case ("Surprise"):
                                maiorSentimento = "Surpresa";
                                break;
                            default:
                                break;
                        }
                    }
                }
            }

            return maiorSentimento;
        }

        public async Task<IServiceResponse<IEnumerable<string>>> RetonaSentimentos(string imagePath)
        {
            var faceClient = GetFaceClient();

            var attributes = new FaceAttributeType[] {
                    FaceAttributeType.Emotion,
            };

            var result = await faceClient.Face.DetectWithUrlAsync(imagePath, false,false,attributes);

            var sentiments = new List<string>();

            foreach (var rosto in result)
            {
                if (rosto.FaceAttributes.Emotion != null) {
                    var emocoes = rosto.FaceAttributes.Emotion;
                    sentiments.Add(RetornaMaiorSentimento(emocoes));
                }
            }

            return new ServiceResponse<IEnumerable<String>>
            {
                StatusCode = sentiments.Count != 0 ? HttpStatusCode.OK : HttpStatusCode.NoContent,
                Message = sentiments.Count != 0 ? string.Format("Foram encontrados {0} sentimento(s) em diferente(s) rosto(s) na imagem.", sentiments.Count) : "Não foram encontrados sentimentos na imagem.",
                Retorno = sentiments,
            };
        }

        public async Task<IServiceResponse<Dictionary<string, int>>> RetornarGeneros(string imagePath)
        {
            var faceClient = GetFaceClient();

            var attributes = new FaceAttributeType[] {
                    FaceAttributeType.Gender,
            };

            var result = await faceClient.Face.DetectWithUrlAsync(imagePath, false, false, attributes);

            var generos = new Dictionary<string, int>
                            {
                                { "Masculino", 0 },
                                { "Feminino", 0 }
                            };
            foreach (var rosto in result)
            {
                if (rosto.FaceAttributes.Gender != null)
                {
                    var genero = rosto.FaceAttributes.Gender;
                    if (genero == Gender.Male)
                    {
                        generos["Masculino"]++;
                    }
                    else {
                        generos["Feminino"]++;
                    }
                }
            }

            var encontrouGeneros = generos["Masculino"] != 0 || generos["Feminino"] != 0;

            return new ServiceResponse<Dictionary<string,int>>
            {
                StatusCode =  HttpStatusCode.OK,
                Message = encontrouGeneros ? string.Format("Foram encontrado(s) {0} rosto(s) masculino(s) e {1} rosto(s) feminino(s) na imagem.", generos["Masculino"], generos["Feminino"])
                                                                : "Não foram encontrados generos na imagem.",
                Retorno = generos
            };
        }

        public async Task<IServiceResponse<double>> RetornarMediaIdade(string imagePath)
        {
            var faceClient = GetFaceClient();

            var attributes = new FaceAttributeType[] {
                    FaceAttributeType.Age,
            };

            var result = await faceClient.Face.DetectWithUrlAsync(imagePath, false, false, attributes);

            var idades = new List<double>();

            foreach (var rosto in result)
            {
                if (rosto.FaceAttributes.Age != null)
                {
                    idades.Add(rosto.FaceAttributes.Age.Value);
                }
            }

            var media = idades.Count == 0 ? 0 : idades.Sum() / idades.Count;

            return new ServiceResponse<double>
            {
                StatusCode = HttpStatusCode.OK,
                Message = media != 0 ? string.Format("A media da idade dos rostos encontrados na imagem é de {0}.", media)
                                                                : "Não foram encontrados rostos na imagem.",
                Retorno = media
            };
        }
    }
}
