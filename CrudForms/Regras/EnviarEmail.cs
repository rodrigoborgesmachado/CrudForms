using JSON;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
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

            var client = new RestClient("http://devtoolsapi.sunsalesystem.com.br/api/email/enviarEmail");
            var request = new RestRequest();
            request.Method = Method.Post;
            request.AddHeader("Content-Type", "application/json");
            var body = JsonConvert.SerializeObject(MontaObjetoBody(destinatario, assunto, corpo, observacao));
            request.AddParameter("application/json", body, ParameterType.RequestBody);
            RestResponse response = client.Execute(request);
            JS_RetornoEnvioEmail retorno = JsonConvert.DeserializeObject<JS_RetornoEnvioEmail>(response.Content.ToString());
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
            chaves.Add("Destinatario");
            chaves.Add("Assunto");
            chaves.Add("Texto");
            chaves.Add("Observacao");
            valores.Add(email);
            valores.Add(assunto);
            valores.Add(corpo);
            valores.Add(observacao);

            dynamic temp = new System.Dynamic.ExpandoObject();
            ((IDictionary<string, object>)temp).Add(chaves[0], valores[0]);
            ((IDictionary<string, object>)temp).Add(chaves[1], valores[1]);
            ((IDictionary<string, object>)temp).Add(chaves[2], valores[2]);
            ((IDictionary<string, object>)temp).Add(chaves[3], valores[3]);

            return temp;
        }
    }
}
