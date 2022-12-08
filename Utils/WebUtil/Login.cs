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
            JS_Usuario retorno = new JS_Usuario();

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

                    List<JS_Usuario> lista = JsonConvert.DeserializeObject<List<JS_Usuario>>(objResponse.ToString());
                    if(lista.Count > 0)
                    {
                        retorno = lista[0];
                        Util.Global.usuarioLogado = retorno;
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
                Util.CL_Files.WriteOnTheLog("Erro: " + e.Message, Global.TipoLog.SIMPLES);
                return false;
            }

            return retorno != null;
        }
    }
}
