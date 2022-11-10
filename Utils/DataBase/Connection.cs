using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Util.Enumerator;

namespace DataBase
{
    public class Connection
    {
        private static Banco banco = null;

        public static string GetBancoSchema()
        {
            return banco.Schema;
        }

        public static Util.Enumerator.BancoDados GetTipoBanco()
        {
            return banco.tipoBanco;
        }

        /// <summary>
        /// Abrir conexão
        /// </summary>
        /// <param name="conection"></param>
        /// <param name="bd"></param>
        /// <returns></returns>
        public static bool OpenConection(string conection, BancoDados bd, string schema = "")
        {
            bool retorno = false;
            try
            {
                if (bd == BancoDados.SQLite)
                {
                    banco = new BancoSQLite();
                    Util.Global.log_system = Util.Global.TipoLog.SIMPLES;
                }
                else if(bd == BancoDados.SQL_SERVER)
                {
                    banco = new BancoSQLServer();
                    Util.Global.log_system = Util.Global.TipoLog.DETALHADO;
                }
                else if (bd == BancoDados.POSTGRESQL)
                {
                    if (string.IsNullOrEmpty(schema))
                        schema = Util.Global.connectionName;

                    banco = new BancoPostGreSql(schema);
                    Util.Global.log_system = Util.Global.TipoLog.DETALHADO;
                }
                else if (bd == BancoDados.MYSQL)
                {
                    if (string.IsNullOrEmpty(schema))
                        schema = Util.Global.connectionName;

                    banco = new BancoMysql(schema);
                    Util.Global.log_system = Util.Global.TipoLog.DETALHADO;
                }

                retorno = banco.OpenConnection(conection);
            }
            catch(Exception e)
            {
                Util.CL_Files.WriteOnTheLog("Error: " + e.Message, Util.Global.TipoLog.SIMPLES);
                retorno = false;
            }

            return retorno;
        }

        /// <summary>
        /// Método que fecha a conexão com o banco de dados
        /// </summary>
        /// <returns>True - fechado com sucesso; False - erro ao fechar conexão</returns>
        public static bool CloseConnection()
        {
            return banco.CloseConnection();
        }

        /// <summary>
        /// Método que faz select no banco
        /// </summary>
        /// <param name="sentenca"></param>
        public static DbDataReader Select(string sentenca)
        {
            return banco.Select(sentenca);
        }

        /// <summary>
        /// Método que faz o insert
        /// </summary>
        /// <param name="sentenca">string de comando</param>
        /// <returns>True - sucesso; False - erro</returns>
        public static bool Insert(string sentenca)
        {
            return banco.Insert(sentenca);
        }

        /// <summary>
        /// Método que faz o insert
        /// </summary>
        /// <param name="sentenca">string de comando</param>
        /// <returns>True - sucesso; False - erro</returns>
        public static bool Update(string sentenca)
        {
            return banco.Update(sentenca);
        }

        /// <summary>
        /// Método que faz o insert
        /// </summary>
        /// <param name="sentenca">string de comando</param>
        /// <returns>True - sucesso; False - erro</returns>
        public static bool Delete(string sentenca)
        {
            return banco.Delete(sentenca);
        }

        /// <summary>
        /// Método que faz a criação da tabela
        /// </summary>
        /// <param name="sentenca">Comando</param>
        /// <returns>True - sucesso; False - errado</returns>
        public static bool CreateTable(string sentenca)
        {
            return banco.CreateTable(sentenca);
        }

        /// <summary>
        /// Método que executa o comando no banco
        /// </summary>
        /// <param name="sentenca"></param>
        /// <returns></returns>
        public static bool Execute(string sentenca)
        {
            return banco.Execute(sentenca);
        }

        /// <summary>
        /// Método que verifica se a tabela existe
        /// </summary>
        /// <param name="tabela">Tabela para verificar</param>
        /// <returns>True - existe; False - não existe</returns>
        public static bool ExistsTable(string tabela)
        {
            return banco.ExistsTable(tabela);
        }

        /// <summary>
        /// Método que converte a data para inteiro
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static int Date_to_Int(DateTime date)
        {
            return banco.Date_to_Int(date);
        }

        /// <summary>
        /// Método que pega o incremental
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        public static int GetIncrement(string table)
        {
            return banco.GetIncrement(table);
        }

        /// <summary>
        /// Método que seta o tipo do log
        /// </summary>
        /// <param name="tipo">Tipo do log</param>
        public static void SetLog(Util.Global.TipoLog tipo)
        {
            banco.SetLog(tipo);
        }

        /// <summary>
        /// Método que retorna o log
        /// </summary>
        /// <returns>Tipo do log</returns>
        public static Util.Global.TipoLog GetLog()
        {
            return banco.GetLog();
        }

        /// <summary>
        /// Método que seta a flag de automático
        /// </summary>
        /// <param name="tipo"></param>
        public static void SetAutomatico(Util.Global.Automatico tipo)
        {
            banco.SetAutomatico(tipo);
        }

        /// <summary>
        /// Método que retorna qual é a flag automático
        /// </summary>
        /// <returns></returns>
        public static Util.Global.Automatico GetAutomatico()
        {
            return banco.GetAutomatico();
        }

        /// <summary>
        /// Método que seta a flag de automático
        /// </summary>
        /// <param name="tipo"></param>
        public static void SetApresentaInformacao(Util.Global.Informacao tipo)
        {
            banco.SetApresentaInformacao(tipo);
        }

        /// <summary>
        /// Método que retorna qual se deve apresentar mensagens de confirmacao ou não
        /// </summary>
        /// <returns></returns>
        public static Util.Global.Informacao GetApresentaInformacao()
        {
            return banco.GetApresentaInformacao();
        }
    }
}
