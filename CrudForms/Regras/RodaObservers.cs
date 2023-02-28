using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Model;
using Newtonsoft.Json;
using RestSharp;
using static Util.Enumerator;

namespace Regras
{
    public class RodaObservers
    {
        List<Timer> timer;
        public RodaObservers()
        {
            timer = new List<Timer>();
        }

        /// <summary>
        /// Método que processa o observer
        /// </summary>
        /// <param name="observer"></param>
        public void Processa(MD_Observer observer)
        {
            Util.CL_Files.WriteOnTheLog("ProcessaObserver Inicio", Util.Global.TipoLog.SIMPLES);

            Visao.BarraDeCarregamento barra = new Visao.BarraDeCarregamento(1, "Processos automaticos");
            barra.Show();
            try
            {

                string query = observer.DAO.Consulta.Replace("\"", "'");
                string connection = Parametros.ConexaoBanco.DAO.Valor;

                List<AcessoBancoCliente.AcessoBanco> valores = Util.Global.BancoDados == BancoDados.SQL_SERVER ?
                    new Regras.AcessoBancoCliente.AcessoBancoSqlServer().BuscaLista(query)
                    :
                    (Util.Global.BancoDados == BancoDados.POSTGRESQL ?
                    new Regras.AcessoBancoCliente.AcessoBancoPostGres().BuscaLista(query)
                    :
                    new Regras.AcessoBancoCliente.AcessoBancoMysql().BuscaLista(query))
                    ;

                Util.CL_Files.WriteOnTheLog("Quantidade de itens da consulta: " + valores.Count, Util.Global.TipoLog.SIMPLES);

                List<string> json = GerarArquivoExportacao.MontaTextoJsonPorLinha(valores, false);
                List<string> hash = new List<string>();
                json.ForEach(j => hash.Add(j.GetHashCode().ToString()));

                List<MD_Observersenviados> enviados = MD_Observersenviados.BuscaLista(observer.DAO.Codigo);

                hash.RemoveAll(h => enviados.Exists(i => i.DAO.Jsonsended.Equals(h)));
                json.RemoveAll(j => !hash.Exists(h => h.Equals(j.GetHashCode().ToString())));

                Util.CL_Files.WriteOnTheLog("Quantidade a processar: " + json.Count, Util.Global.TipoLog.SIMPLES);


                if (json.Count > 0)
                {
                    EnviaEmail(json, observer.DAO.Codigo, observer.DAO.Emailsenviar, observer.DAO.Descricao);
                }
                barra.AvancaBarra(1);
            }
            catch(Exception ex)
            {
                Util.CL_Files.LogException(ex);
            }
            finally
            {
                barra.Dispose();
            }
        }

        /// <summary>
        /// Método que envia o email
        /// </summary>
        /// <param name="json"></param>
        /// <param name="codigo"></param>
        /// <param name="emails"></param>
        private void EnviaEmail(List<string> json, int codigo, string emails, string descricao)
        {

            try
            {
                string textoEmail = MontaInicioEmail(descricao, json.Count);
                string textoTabela = string.Empty;
                string header = string.Empty;

                int i = 0;
                json.ForEach(j =>
                {
                    if(i == 0)
                        header += "<tr>" + MontaColunasEmail(j) + "</tr>";
                    textoTabela += "<tr>" + MontaLinhaEmail(j) + "</tr>";
                    this.InsereObserverEnviado(codigo, j);
                    i++;
                });
                
                textoEmail = textoEmail.Replace("#HEADER", header);
                textoEmail = textoEmail.Replace("#TABELA", textoTabela);

                Util.CL_Files.WriteOnTheLog("Envia Email: " + textoEmail, Util.Global.TipoLog.SIMPLES);

                emails = emails.Replace(",", ";").Replace("|", ";");
                List<string> emailsList = emails.Split(';').ToList();
                emailsList.RemoveAll(e => string.IsNullOrEmpty(e));

                emailsList.ForEach(email =>
                {
                    JSON.JS_RetornoEnvioEmail retorno = EnviarEmail.EnviaEmail(email, "Atividade automática - " + descricao, textoEmail);
                });
            }
            catch(Exception e)
            {
                Util.CL_Files.LogException(e);
            }
        }

        /// <summary>
        /// Método que monta o início do email automático
        /// </summary>
        /// <returns></returns>
        private string MontaInicioEmail(string descricao, int itensQt)
        {
            StringBuilder builder = new StringBuilder();

            builder.AppendLine("<!DOCTYPE html PUBLIC \" -//W3C//DTD HTML 4.01//EN\" \"https://www.w3.org/TR/html4/strict.dtd\">");
            builder.AppendLine("<html lang=\"pt-BR\">");
            builder.AppendLine("<head>");
            builder.AppendLine("<meta http-equiv=\"Content-Type\" content=\"text/html; charset=UTF-8\">");
            builder.AppendLine("<style type=\"text/css\" nonce=\"\">");
            builder.AppendLine("body,");
            builder.AppendLine("td,");
            builder.AppendLine("div,");
            builder.AppendLine("p,");
            builder.AppendLine("a,");
            builder.AppendLine("input {");
            builder.AppendLine("font-family: arial, sans-serif;");
            builder.AppendLine("}");
            builder.AppendLine("</style>");
            builder.AppendLine("<meta http-equiv=\"X-UA-Compatible\" content=\"IE=edge\">");
            builder.AppendLine("<title>Relatório Automático</title>");
            builder.AppendLine("<style type=\"text/css\" nonce=\"\">");
            builder.AppendLine("body {");
            builder.AppendLine("background: #FFFFE0");
            builder.AppendLine("}");
            builder.AppendLine("body,");
            builder.AppendLine("td {");
            builder.AppendLine("font-size: 13px");
            builder.AppendLine("}");
            builder.AppendLine("a:link,");
            builder.AppendLine("a:active {");
            builder.AppendLine("color: #1155CC;");
            builder.AppendLine("text-decoration: none");
            builder.AppendLine("}");
            builder.AppendLine("a:hover {");
            builder.AppendLine("text-decoration: underline;");
            builder.AppendLine("cursor: pointer");
            builder.AppendLine("}");
            builder.AppendLine("a:visited {");
            builder.AppendLine("color: #6611CC");
            builder.AppendLine("}");
            builder.AppendLine("img {");
            builder.AppendLine("border: 1px");
            builder.AppendLine("}");
            builder.AppendLine("pre {");
            builder.AppendLine("white-space: pre;");
            builder.AppendLine("white-space: -moz-pre-wrap;");
            builder.AppendLine("white-space: -o-pre-wrap;");
            builder.AppendLine("white-space: pre-wrap;");
            builder.AppendLine("word-wrap: break-word;");
            builder.AppendLine("max-width: 800px;");
            builder.AppendLine("overflow: auto;");
            builder.AppendLine("}");
            builder.AppendLine(".logo {");
            builder.AppendLine("left: -7px;");
            builder.AppendLine("position: relative;");
            builder.AppendLine("}");
            builder.AppendLine("</style>");
            builder.AppendLine("<style type=\"text/css\"></style>");
            builder.AppendLine("</head>");
            builder.AppendLine("<body>");
            builder.AppendLine("<div class=\"bodycontainer\" align=\"justify\">");
            builder.AppendLine("<table width=\"100%\" cellpadding=\"0\" cellspacing=\"0\" border=\"0\">");
            builder.AppendLine("<tbody>");
            builder.AppendLine("<tr height=\"14px\">");
            builder.AppendLine("<td width=\"143\"> ");
            builder.AppendLine("<b>SunSale System</b> ");
            builder.AppendLine("</td>");
            builder.AppendLine("<td align=\"right\">");
            builder.AppendLine("<font size=\"-1\" color=\"#777\">");
            builder.AppendLine("<b>SunSale System &lt;sunsalesystem@gmail.com&gt;</b>");
            builder.AppendLine("</font> ");
            builder.AppendLine("</td>");
            builder.AppendLine("</tr>");
            builder.AppendLine("</tbody>");
            builder.AppendLine("</table>");
            builder.AppendLine("<br>");
            builder.AppendLine("<hr>");
            builder.AppendLine("<div class=\"maincontent\">");
            builder.AppendLine("<table width=\"100%\" cellpadding=\"0\" cellspacing=\"0\" border=\"0\">");
            builder.AppendLine("<tbody>");
            builder.AppendLine("<tr>");
            builder.AppendLine("<td> ");
            builder.AppendLine("<font size=\"+1\">");
            builder.AppendLine(descricao);
            builder.AppendLine("</font>");
            builder.AppendLine("<br>");
            builder.AppendLine("<font size=\"-1\" color=\"#777\">");
            builder.AppendLine($"Quantidade de itens: {itensQt}");
            builder.AppendLine("</font> ");
            builder.AppendLine("</td>");
            builder.AppendLine("<td align=\"right\">");
            builder.AppendLine("<font size=\"-1\">");
            builder.AppendLine("" + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString());
            builder.AppendLine("</font>");
            builder.AppendLine("</td>");
            builder.AppendLine("</tr>");
            builder.AppendLine("</tbody>");
            builder.AppendLine("</table>");
            builder.AppendLine("<br>");
            builder.AppendLine("<br>");
            builder.AppendLine("<br>");
            builder.AppendLine("<table width=\"100%\" cellpadding=\"0\" cellspacing=\"0\" border=\"0\" class=\"message\">");
            builder.AppendLine("<tbody>");
            builder.AppendLine("<tr>");
            builder.AppendLine("<td colspan=\"2\">");
            builder.AppendLine("<table width=\"100%\" cellpadding=\"12\" cellspacing=\"0\" border=\"1\">");
            builder.AppendLine("<thead>");
            builder.AppendLine("#HEADER");
            builder.AppendLine("</thead>");
            builder.AppendLine("<tbody>");
            builder.AppendLine("#TABELA");
            builder.AppendLine("</tbody>");
            builder.AppendLine("</table>");
            builder.AppendLine("</td>");
            builder.AppendLine("</tr>");
            builder.AppendLine("</tbody>");
            builder.AppendLine("</table>");
            builder.AppendLine("<br>");
            builder.AppendLine("<br>");
            builder.AppendLine("<br>");
            builder.AppendLine("<hr> ");
            builder.AppendLine("</div>");
            builder.AppendLine("</div>");
            builder.AppendLine("</body>");
            builder.AppendLine("</html>");

            return builder.ToString();
        }

        /// <summary>
        /// Método que transforma todos os json
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        private string MontaLinhaEmail(string json)
        {
            string retorno = string.Empty;
            dynamic obj = JsonConvert.DeserializeObject(json);

            foreach (Newtonsoft.Json.Linq.JProperty jproperty in obj)
            {
                retorno += $"<td>{jproperty.Value}</td>";
            }

            return retorno;
        }

        /// <summary>
        /// Método que transforma todos os json
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        private string MontaColunasEmail(string json)
        {
            string retorno = string.Empty;
            dynamic obj = JsonConvert.DeserializeObject(json);

            foreach (Newtonsoft.Json.Linq.JProperty jproperty in obj)
            {
                retorno += $"<th>{jproperty.Name}</th>";
            }

            return retorno;
        }

        /// <summary>
        /// Método que insere o observer enviado
        /// </summary>
        private void InsereObserverEnviado(int codigoObserver, string json)
        {
            DAO.MD_Observersenviados observersenviados = new DAO.MD_Observersenviados();
            observersenviados.Codigo = DataBase.Connection.GetIncrement(observersenviados.table.Table_Name);
            observersenviados.Codigoobserver = codigoObserver;
            observersenviados.Created = DateTime.Now;
            observersenviados.Jsonsended = json.GetHashCode().ToString();
            observersenviados.Insert();
        }
    }
}
