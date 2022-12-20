using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Regras
{
    public class BuscaNovaVersaoSistema
    {
        /// <summary>
        /// Método que busca uma nova versão
        /// </summary>
        /// <param name="version"></param>
        /// <returns></returns>
        public bool BuscaNovaVersao(string version)
        {
            bool retorno = true;

            try
            {
                string urlDownload = BuscaUrlDownload(version);
                BaixaArquivo(urlDownload, version);
            }
            catch(Exception ex)
            {
                Util.CL_Files.WriteOnTheLog(ex.Message, Util.Global.TipoLog.SIMPLES);
                retorno = false;
            }

            return retorno;
        }

        /// <summary>
        /// Método que busca a url para download
        /// </summary>
        /// <returns></returns>
        private string BuscaUrlDownload(string versao)
        {
            string url = $"http://devtoolsapi.sunsalesystem.com.br/api/crudformsinstalador/getItem?versao={versao}";
            
            var client = new RestClient(url);
            var request = new RestRequest(url , Method.Get);
            RestResponse response = client.Execute(request);
            JSON.JS_RetornoVersao retorno = JsonConvert.DeserializeObject<JSON.JS_RetornoVersao>(response.Content);

            string urlretorno = retorno.Objeto.Diretorio;

            return urlretorno;
        }

        /// <summary>
        /// Método que baixa o arquivo
        /// </summary>
        /// <param name="url"></param>
        /// <param name="version"></param>
        private void BaixaArquivo(string url, string version)
        {
            Visao.BarraDeCarregamento barra = new Visao.BarraDeCarregamento(1, "Baixando arquivo");
            barra.Show();

            string file = Util.Global.app_temp_directory + "devtools-" + version + ".exe";

            if (File.Exists(file))
            {
                File.Delete(file);
            }

            using (var client = new WebClient())
            {
                client.DownloadFile(url, file);
                barra.AvancaBarra(1);
            }

            barra.Close();
            barra.Dispose();

            if (File.Exists(file))
            {
                System.Diagnostics.Process.Start(file);
            }
        }
    }
}
