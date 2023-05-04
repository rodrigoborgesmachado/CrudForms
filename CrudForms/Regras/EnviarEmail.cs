using JSON;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Regras
{
    public static class EnviarEmail
    {
        /// <summary>
        /// Método que envia email
        /// </summary>
        /// <param name="destinatario"></param>
        /// <param name="assunto"></param>
        /// <param name="corpo"></param>
        /// <param name="observacao"></param>
        /// <returns></returns>
        public static JS_RetornoEnvioEmail EnviaEmail(string destinatario, string assunto, string corpo, string observacao = "")
        {

            if (string.IsNullOrEmpty(observacao)) 
            {
                StackFrame frame = new StackTrace().GetFrame(1);
                observacao = frame.GetFileName() + "." + frame.GetMethod().Name;
            }

            var url = "https://apisunsale.azurewebsites.net/api/Email";
            var json = JsonConvert.SerializeObject(MontaObjetoBody(destinatario, assunto, corpo, observacao));

            var request = WebRequest.Create(url);
            request.Headers.Add(HttpRequestHeader.Authorization, "Bearer " + Util.Global.usuarioLogado.TOKEN);

            request.Method = "POST";
            request.ContentType = "application/json";

            using (var streamWriter = new StreamWriter(request.GetRequestStream()))
            {
                streamWriter.Write(json);
            }

            JS_RetornoEnvioEmail retorno;

            var response = request.GetResponse();
            using (var streamReader = new StreamReader(response.GetResponseStream()))
            {
                string result = streamReader.ReadToEnd();
                retorno = JsonConvert.DeserializeObject<JS_RetornoEnvioEmail>(result);

            }
            return retorno;
        }

        /// <summary>
        /// Método que monta o body para enviar o email
        /// </summary>
        /// <param name="email"></param>
        /// <param name="textoEmail"></param>
        /// <returns></returns>
        private static Object MontaObjetoBody(string email, string assunto, string corpo, string observacao)
        {
            List<string> chaves = new List<string>();
            List<string> valores = new List<string>();
            chaves.Add("destinatario");
            chaves.Add("assunto");
            chaves.Add("texto");
            chaves.Add("observacao");
            chaves.Add("status");
            valores.Add(email);
            valores.Add(assunto);
            valores.Add(corpo);
            valores.Add(observacao);
            valores.Add("0");

            dynamic temp = new System.Dynamic.ExpandoObject();
            ((IDictionary<string, object>)temp).Add(chaves[0], valores[0]);
            ((IDictionary<string, object>)temp).Add(chaves[1], valores[1]);
            ((IDictionary<string, object>)temp).Add(chaves[2], valores[2]);
            ((IDictionary<string, object>)temp).Add(chaves[3], valores[3]);
            ((IDictionary<string, object>)temp).Add(chaves[4], valores[4]);

            return temp;
        }
    }
}
