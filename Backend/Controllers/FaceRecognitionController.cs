using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.CognitiveServices.Vision.Face;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using static System.Environment;
using System.Threading.Tasks;
using IA_Reconhecimento_Facial.Service;
using IA_Reconhecimento_Facial.Interface;

namespace IA_Reconhecimento_Facial.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FaceRecognitionController : ControllerBase
    {
        private readonly IFaceRecognitionService _facialRecognitionService;

        public FaceRecognitionController(IFaceRecognitionService faceRecognitionService)
        {
            _facialRecognitionService = faceRecognitionService;
        }

        /// <summary>
        /// Verifica se existem rostos na imagem
        /// </summary>
        /// <returns></returns>
        [HttpGet("VerificaRosto")]
        public ActionResult<bool> VerificaRosto([FromHeader] string url)
        {
            var response = _facialRecognitionService.VerificaRosto(url).Result;

            return StatusCode(
                 (int)response.StatusCode,
                 new
                 {
                     PossuiRosto = response.Retorno,
                     Mensagem = response.Message
                 });
        }

        /// <summary>
        /// Retorna os sentimentos mais presente nos rostos encontrados na imagem
        /// </summary>
        /// <returns></returns>
        [HttpGet("RetornaSentimentos")]
        public ActionResult<IEnumerable<string>> RetornaSentimentos([FromHeader] string url)
        {
            var response = _facialRecognitionService.RetonaSentimentos(url).Result;

            return StatusCode(
                 (int)response.StatusCode,
                 new
                 {
                     Sentimentos = response.Retorno,
                     Mensagem = response.Message
                 });
        }

        /// <summary>
        /// Retorna as quantidades de cada genero para cada rosto na imagem
        /// </summary>
        /// <returns></returns>
        [HttpGet("RetornarGeneros")]
        public ActionResult<Dictionary<string, int>> RetornarGeneros([FromHeader] string url)
        {
            var response = _facialRecognitionService.RetornarGeneros(url).Result;

            return StatusCode(
                 (int)response.StatusCode,
                 new
                 {
                     Generos = response.Retorno,
                     Mensagem = response.Message
                 });
        }

        /// <summary>
        /// Retorna a media das idades para cada rosto na imagem
        /// </summary>
        /// <returns></returns>
        [HttpGet("RetornarMediaIdade")]
        public ActionResult<double> RetornarMediaIdade([FromHeader] string url)
        {
            var response = _facialRecognitionService.RetornarMediaIdade(url).Result;

            return StatusCode(
                 (int)response.StatusCode,
                 new
                 {
                     MediaIdades = response.Retorno,
                     Mensagem = response.Message
                 });
        }
    }
}
