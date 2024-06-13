using System;
using System.Collections.Generic;
using System.IO;
using static Util.Enumerator;
using Newtonsoft;
using System.Xml;
using Newtonsoft.Json;
using System.Xml.Linq;
using System.Windows.Forms;
using System.Text;

namespace Regras
{
    public static class GerarArquivoExportacao
    {
        /// <summary>
        /// Método que gera o arquivo de relatório questionando o caminho
        /// </summary>
        /// <param name="tipo"></param>
        /// <param name="valores"></param>
        /// <param name="nomeArquivo"></param>
        /// <param name="mensagemErro"></param>
        /// <returns></returns>
        public static bool GerarArquivoSolicitandoCaminho(TipoArquivoExportacao tipo, List<AcessoBancoCliente.AcessoBanco> valores, string nomeArquivo, out string mensagemErro, out string diretorio)
        {
            mensagemErro = string.Empty;
            diretorio = string.Empty;

            FolderBrowserDialog dialog = new FolderBrowserDialog();
            dialog.Description = "Selecione onde deseja salvar o arquivo";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                DirectoryInfo directory = new DirectoryInfo(dialog.SelectedPath);
                diretorio = directory.FullName;
                return GerarArquivoExportacao.GerarArquivo(tipo, valores, directory, nomeArquivo, out mensagemErro);
            }

            return false;
        }

        /// <summary>
        /// Método que gera o arquivo CSV
        /// </summary>
        /// <param name="valores"></param>
        /// <param name="caminhoArquivo"></param>
        /// <param name="nomeArquivo"></param>
        /// <param name="mensagemErro"></param>
        /// <returns></returns>
        public static bool GerarArquivo(TipoArquivoExportacao tipo, List<AcessoBancoCliente.AcessoBanco> valores, DirectoryInfo caminhoArquivo, string nomeArquivo, out string mensagemErro)
        {
            mensagemErro = string.Empty;
            bool retorno = true;
            string extensao = tipo == TipoArquivoExportacao.CSV ? ".csv" : (tipo == TipoArquivoExportacao.JSON ? ".json" : ".xml");

            if (!caminhoArquivo.Exists)
                caminhoArquivo.Create();

            if (string.IsNullOrEmpty(nomeArquivo))
                nomeArquivo = $"relatorio{extensao}";

            if (!nomeArquivo.EndsWith(extensao))
                nomeArquivo += extensao;

            try
            {
                FileInfo file = new FileInfo(caminhoArquivo.FullName + "\\" + nomeArquivo);
                if (file.Exists)
                    file.Delete();

                string texto = tipo == TipoArquivoExportacao.CSV ? MontaTextoCSV(valores) : (tipo == TipoArquivoExportacao.JSON ? MontaTextoJson(valores) : MontaTexotXml(valores));

                File.WriteAllText(file.FullName, texto);

            }
            catch (Exception ex)
            {
                Util.CL_Files.LogException(ex);
                mensagemErro = "Erro: " + ex.Message;
                retorno = false;
            }

            return retorno;
        }

        /// <summary>
        /// Método que monta o texto do CSV
        /// </summary>
        /// <param name="valores"></param>
        /// <returns></returns>
        public static string MontaTextoCSV(List<AcessoBancoCliente.AcessoBanco> valores)
        {
            Visao.BarraDeCarregamento barra = new Visao.BarraDeCarregamento(valores.Count, "Carregando arquivo");
            barra.Show();

            StringBuilder builder = new StringBuilder();

            if(valores.Count > 0)
            {
                builder.AppendLine(string.Join(";", valores[0].campos));
                valores.RemoveAt(0);
                valores.ForEach(valor => 
                {
                    builder.AppendLine(string.Join(";", valor.valores));
                    barra.AvancaBarra(1);
                });
            }

            barra.Dispose();

            return builder.ToString();
        }

        /// <summary>
        /// Método que exporta os dados para json
        /// </summary>
        /// <param name="valores"></param>
        /// <returns></returns>
        public static string MontaTextoJson(List<AcessoBancoCliente.AcessoBanco> valores)
        {
            if (valores.Count == 0) return string.Empty;

            List<dynamic> lista = new List<dynamic>();

            List<string> campos = valores[0].campos;
            valores.ForEach(valor =>
            {
                dynamic temp = new System.Dynamic.ExpandoObject();
                for(int i = 0; i< valor.valores.Count; i++)
                {
                    ((IDictionary<string, Object>)temp).Add(campos[i], valor.valores[i]);
                }
                lista.Add(temp);
            });

            return Newtonsoft.Json.JsonConvert.SerializeObject(lista, Newtonsoft.Json.Formatting.Indented);
        }


        /// <summary>
        /// Método que exporta os dados para json
        /// </summary>
        /// <param name="valores"></param>
        /// <returns></returns>
        public static List<string> MontaTextoJsonPorLinha(List<AcessoBancoCliente.AcessoBanco> valores, bool identa = true)
        {
            if (valores.Count == 0) return new List<string>();

            List<dynamic> lista = new List<dynamic>();

            List<string> campos = valores[0].campos;
            valores.ForEach(valor =>
            {
                dynamic temp = new System.Dynamic.ExpandoObject();
                for (int i = 0; i < valor.valores.Count; i++)
                {
                    ((IDictionary<string, Object>)temp).Add(campos[i], valor.valores[i]);
                }
                lista.Add(temp);
            });

            List<string> linhas = new List<string>();

            if(identa)
                lista.ForEach(l => linhas.Add(Newtonsoft.Json.JsonConvert.SerializeObject(l, Newtonsoft.Json.Formatting.Indented)));
            else 
                lista.ForEach(l => linhas.Add(Newtonsoft.Json.JsonConvert.SerializeObject(l)));

            return linhas;
        }

        /// <summary>
        /// Método que exporta os dados para xml
        /// </summary>
        /// <param name="valores"></param>
        /// <returns></returns>
        public static string MontaTexotXml(List<AcessoBancoCliente.AcessoBanco> valores)
        {
            string json = MontaTextoJson(valores);

            string retorno = XDocument.Parse(DeserializeXmlNode(json, "root", "object").InnerXml).ToString();

            return retorno;
        }

        private static XmlDocument DeserializeXmlNode(string json, string rootName, string rootPropertyName)
        {
            return DeserializeXmlNode(new StringReader(json), rootName, rootPropertyName);
        }

        private static XmlDocument DeserializeXmlNode(TextReader textReader, string rootName, string rootPropertyName)
        {
            var prefix = "{" + JsonConvert.SerializeObject(rootPropertyName) + ":";
            var postfix = "}";

            string combinedReader = prefix + textReader.ReadToEnd() + postfix;
            var settings = new JsonSerializerSettings
            {
                Converters = { new Newtonsoft.Json.Converters.XmlNodeConverter() { DeserializeRootElementName = rootName } },
                DateParseHandling = DateParseHandling.None,
            };
            using (var jsonReader = new JsonTextReader(new StringReader(combinedReader)) { CloseInput = false, DateParseHandling = DateParseHandling.None })
            {
                return JsonSerializer.CreateDefault(settings).Deserialize<XmlDocument>(jsonReader);
            }
        }
    }
}
