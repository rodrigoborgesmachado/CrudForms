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
    public static class Licenciamento
    {

        /// <summary>
        /// Método que insere a licença na API
        /// </summary>
        /// <param name="chave"></param>
        /// <param name="ativo"></param>
        /// <returns></returns>
        public static bool InsereLicencaAPI(string chave, string ativo, string cnpj, int diavencimento)
        {
            try
            {
                string dadosPOST = "chaveGuid=" + chave;
                dadosPOST += "&ativo=" + ativo + "&cnpj=" + Util.Funcoes.TrataCNPJ(cnpj) + "&diaVencimento=" + diavencimento;

                var dados = Encoding.UTF8.GetBytes(dadosPOST);

                var requisicaoWeb = WebRequest.CreateHttp("http://www.sunsalesystem.com.br/php/verificadorSet.php");
                requisicaoWeb.Method = "POST";
                requisicaoWeb.ContentType = "application/x-www-form-urlencoded";
                requisicaoWeb.ContentLength = dados.Length;
                requisicaoWeb.UserAgent = "RequisicaoSunSalePro";

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

                    var post = JsonConvert.DeserializeObject<WebResult>(objResponse.ToString());
                    CL_Files.WriteOnTheLog("Retorno API: " + post.Result, Util.Global.TipoLog.SIMPLES);

                    streamDados.Close();
                    resposta.Close();
                }
            }
            catch (Exception e)
            {
                Util.CL_Files.WriteOnTheLog("Error: " + e.Message, Util.Global.TipoLog.SIMPLES);
                return false;
            }
            return true;
        }

        /// <summary>
        /// Método que retorna todas as chaves
        /// </summary>
        /// <returns></returns>
        public static List<JS_Licenca> RetornaTodasChaves()
        {
            List<JS_Licenca> lista = new List<JS_Licenca>();

            try
            {

                var requisicaoWeb = WebRequest.CreateHttp("http://www.sunsalesystem.com.br/php/verificadorGetAll.php");
                requisicaoWeb.Method = "GET";
                requisicaoWeb.ContentType = "application/x-www-form-urlencoded";

                using (var resposta = requisicaoWeb.GetResponse())
                {
                    var streamDados = resposta.GetResponseStream();
                    StreamReader reader = new StreamReader(streamDados);
                    object objResponse = reader.ReadToEnd();
                    lista = JsonConvert.DeserializeObject<List<JS_Licenca>>(objResponse.ToString());
                    streamDados.Close();
                    resposta.Close();
                }
                   
            }
            catch (Exception e)
            {
                Util.CL_Files.WriteOnTheLog("Erro: " + e.Message, Global.TipoLog.SIMPLES);
            }

            return lista;
        }

        /// <summary>
        /// Método que delete a nova licença
        /// </summary>
        /// <returns></returns>
        public static bool DeleteLicenca(string line)
        {
            try
            {
                string dadosPOST = "chaveGuid=" + line;

                var dados = Encoding.UTF8.GetBytes(dadosPOST);

                var requisicaoWeb = WebRequest.CreateHttp("http://www.sunsalesystem.com.br/php/verificadorDelete.php");
                requisicaoWeb.Method = "POST";
                requisicaoWeb.ContentType = "application/x-www-form-urlencoded";
                requisicaoWeb.ContentLength = dados.Length;
                requisicaoWeb.UserAgent = "RequisicaoSunSalePro";

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
                    Util.CL_Files.WriteOnTheLog("Chave: " + objResponse.ToString(), Util.Global.TipoLog.SIMPLES);

                    streamDados.Close();
                    resposta.Close();
                }
            }
            catch (Exception e)
            {
                Util.CL_Files.WriteOnTheLog("Error: " + e.Message, Util.Global.TipoLog.SIMPLES);
                return false;
            }
            return true;
        }

        /// <summary>
        /// Método que insere na API
        /// </summary>
        public static bool AtualizaAPI(string chave, string ativo)
        {
            try
            {
                string dadosPOST = "chaveGuid=" + chave;
                dadosPOST += "&ativo=" + ativo;

                var dados = Encoding.UTF8.GetBytes(dadosPOST);

                var requisicaoWeb = WebRequest.CreateHttp("http://www.sunsalesystem.com.br/php/verificadorAtualiza.php");
                requisicaoWeb.Method = "POST";
                requisicaoWeb.ContentType = "application/x-www-form-urlencoded";
                requisicaoWeb.ContentLength = dados.Length;
                requisicaoWeb.UserAgent = "RequisicaoSunSalePro";

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
                    Util.CL_Files.WriteOnTheLog("Chave: " + objResponse.ToString(), Util.Global.TipoLog.SIMPLES);

                    streamDados.Close();
                    resposta.Close();
                }
            }
            catch (Exception e)
            {
                Util.CL_Files.WriteOnTheLog("Error: " + e.Message, Util.Global.TipoLog.SIMPLES);
                return false;
            }
            return true;
        }
    }
}
