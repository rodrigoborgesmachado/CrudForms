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
            JS_RetornoLogin retorno = new JS_RetornoLogin();

            try
            {
                var requisicaoWeb = WebRequest.CreateHttp($"http://devtoolsapi.sunsalesystem.com.br/api/usuarioscrudforms/login?login={login}&senha={pass}");
                requisicaoWeb.Method = "GET";
                requisicaoWeb.ContentType = "application/x-www-form-urlencoded";
                requisicaoWeb.UserAgent = "RequisicaoDevTools";

                using (var resposta = requisicaoWeb.GetResponse())
                {
                    var streamDados = resposta.GetResponseStream();
                    StreamReader reader = new StreamReader(streamDados);
                    object objResponse = reader.ReadToEnd();

                    retorno = JsonConvert.DeserializeObject<JS_RetornoLogin>(objResponse.ToString());
                    if (retorno.Sucesso)
                    {
                        Util.Global.usuarioLogado = retorno.Objeto;
                    }
                    else
                    {
                        retorno = null;
                    }

                    streamDados.Close();
                    resposta.Close();
                }

            }
            catch (Exception e)
            {
                Util.CL_Files.LogException(e);
                return false;
            }

            return retorno != null;
        }
    }
}
