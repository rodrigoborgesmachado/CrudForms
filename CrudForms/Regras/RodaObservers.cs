using System;
using System.Collections.Generic;
using System.Linq;
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

                List<string> json = GerarArquivoExportacao.MontaTextoJsonPorLinha(valores, false);
                List<string> hash = new List<string>();
                json.ForEach(j => hash.Add(j.GetHashCode().ToString()));

                List<MD_Observersenviados> enviados = MD_Observersenviados.BuscaLista(observer.DAO.Codigo);

                hash.RemoveAll(h => enviados.Exists(i => i.DAO.Jsonsended.Equals(h)));
                json.RemoveAll(j => !hash.Exists(h => h.Equals(j.GetHashCode().ToString())));

                if (json.Count > 0)
                {
                    EnviaEmail(json, observer.DAO.Codigo, observer.DAO.Emailsenviar);
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
        private void EnviaEmail(List<string> json, int codigo, string emails)
        {
            Visao.BarraDeCarregamento barra = new Visao.BarraDeCarregamento(json.Count, "Enviando emails");
            barra.Show();

            try
            {
                foreach (string j in json)
                {
                    string textoEmail = MontaEmail(j);

                    emails = emails.Replace(",", ";").Replace("|", ";");
                    List<string> emailsList = emails.Split(';').ToList();
                    emailsList.RemoveAll(e => string.IsNullOrEmpty(e));

                    emailsList.ForEach(email =>
                    {
                        JSON.JS_RetornoEnvioEmail retorno = EnviarEmail.EnviaEmail(email, "Atividade automática - CrudForms", textoEmail);

                        if (retorno.Sucesso)
                        {
                            this.InsereObserverEnviado(codigo, j);
                        }
                    });
                    barra.AvancaBarra(1);
                }
            }
            catch(Exception e)
            {
                Util.CL_Files.LogException(e);
            }
            finally
            {
                barra.Dispose();
            }
        }

        /// <summary>
        /// Método que transforma todos os json
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        private string MontaEmail(string json)
        {
            return "Objeto do processo automatico:\n" + JsonConvert.SerializeObject(json);
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
