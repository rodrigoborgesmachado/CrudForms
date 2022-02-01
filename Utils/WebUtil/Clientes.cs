using JSON;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Util;

namespace Util.WebUtil
{
    public static class Clientes
    {
        /// <summary>
        /// Método que retorna todas as chaves
        /// </summary>
        /// <returns></returns>
        public static List<JS_Cliente> RetornarTodosClientes()
        {
            List<JS_Cliente> lista = new List<JS_Cliente>();

            try
            {

                var requisicaoWeb = WebRequest.CreateHttp("http://www.sunsalesystem.com.br/php/clientesGetAll.php");
                requisicaoWeb.Method = "GET";
                requisicaoWeb.ContentType = "application/x-www-form-urlencoded";

                using (var resposta = requisicaoWeb.GetResponse())
                {
                    var streamDados = resposta.GetResponseStream();
                    StreamReader reader = new StreamReader(streamDados);
                    object objResponse = reader.ReadToEnd();
                    lista = JsonConvert.DeserializeObject<List<JS_Cliente>>(objResponse.ToString());
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
    }
}
