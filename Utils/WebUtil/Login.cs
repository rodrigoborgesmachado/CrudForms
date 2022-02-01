using JSON;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Util.WebUtil
{
    public static class Login
    {
        /// <summary>
        /// Método que valida o login do usuário
        /// </summary>
        /// <param name="login"></param>
        /// <param name="pass"></param>
        /// <returns></returns>
        public static bool ValidaLogin(string login, string pass)
        {
            bool retorno = true;

            try
            {
                string dadosPOST = "usu=" + login +
                                   "&pass=" + pass;

                var dados = Encoding.UTF8.GetBytes(dadosPOST);

                var requisicaoWeb = WebRequest.CreateHttp("http://www.sunsalesystem.com.br/php/login.php");
                requisicaoWeb.Method = "POST";
                requisicaoWeb.ContentType = "application/x-www-form-urlencoded";
                requisicaoWeb.ContentLength = dados.Length;
                requisicaoWeb.UserAgent = "RequisicaoDevTools";

                using (var stream = requisicaoWeb.GetRequestStream())
                {
                    stream.Write(dados, 0, dados.Length);
                    stream.Close();
                }

                using (var resposta = requisicaoWeb.GetResponse())
                {
                    var streamDados = resposta.GetResponseStream();
                    StreamReader reader = new StreamReader(streamDados);
                    object objResponse = reader.ReadToEnd();
                    retorno = JsonConvert.DeserializeObject<List<JS_Cliente>>(objResponse.ToString()).Count > 0 ;
                    streamDados.Close();
                    resposta.Close();
                }

            }
            catch (Exception e)
            {
                Util.CL_Files.WriteOnTheLog("Erro: " + e.Message, Global.TipoLog.SIMPLES);
                retorno = false;
            }

            return retorno;
        }
    }
}
