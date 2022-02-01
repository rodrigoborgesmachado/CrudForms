using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Util;

namespace DataBase
{
    abstract class Banco
    {
        #region Atributos e Propriedades

        /// <summary>
        /// Checker is the connection is opened
        /// </summary>
        protected bool is_open = false;

        /// <summary>
        /// Checker if the transaction is opened
        /// </summary>
        protected bool is_in_transaction = false;

        /// <summary>
        /// Name of the data_base of tests
        /// </summary>
        protected string name_table_test = "TESTE";

        #endregion Atributos e Propriedades

        /// <summary>
        /// Método que abre a conexão
        /// </summary>
        /// <param name="directory_database">string de conexão</param>
        /// <returns>True - conectado com sucesso; False - erro ao conectar</returns>
        public abstract bool OpenConnection(string directory_database = "");

        /// <summary>
        /// Método que fecha a conexão
        /// </summary>
        /// <returns></returns>
        public abstract bool CloseConnection();

        /// <summary>
        /// Método que faz o select
        /// </summary>
        /// <param name="command_sql">Comando Select</param>
        /// <returns>DataReader com o resultado</returns>
        public abstract DbDataReader Select(string command_sql);

        /// <summary>
        /// Método que faz o create da tabela
        /// </summary>
        /// <param name="command_sql">SQL</param>
        /// <returns>True - foi criado; False - erro</returns>
        public bool CreateTable(string command_sql)
        {
            try
            {
                return Execute(command_sql);
            }
            catch (Exception e)
            {
                Util.CL_Files.WriteOnTheLog("Erro no delete: " + command_sql + ". Erro: " + e.Message, Global.TipoLog.SIMPLES);
                return false;
            }
        }

        /// <summary>
        /// Método que faz o insert
        /// </summary>
        /// <param name="command_sql">Comando insert</param>
        /// <returns>True - insert efetuado com sucesso</returns>
        public bool Insert(string command_sql)
        {
            try
            {
                return Execute(command_sql);
            }
            catch (Exception e)
            {
                Util.CL_Files.WriteOnTheLog("Erro no delete: " + command_sql + ". Erro: " + e.Message, Global.TipoLog.SIMPLES);
                return false;
            }
        }

        /// <summary>
        /// Método que faz o Update
        /// </summary>
        /// <param name="command_sql">Comando update</param>
        /// <returns>True - update com sucesso; False - erro</returns>
        public bool Update(string command_sql)
        {
            try
            {
                return Execute(command_sql);
            }
            catch (Exception e)
            {
                Util.CL_Files.WriteOnTheLog("Erro no delete: " + command_sql + ". Erro: " + e.Message, Global.TipoLog.SIMPLES);
                return false;
            }
        }

        /// <summary>
        /// Método que faz o Delete da classe
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public bool Delete(string command)
        {
            try
            {
                return Execute(command);
            }
            catch (Exception e)
            {
                Util.CL_Files.WriteOnTheLog("Erro no delete: " + command + ". Erro: " + e.Message, Global.TipoLog.SIMPLES);
                return false;
            }
        }

        /// <summary>
        /// Método que executa os comandos
        /// </summary>
        /// <param name="command_sql">Comando a ser executado</param>
        /// <returns></returns>
        public abstract bool Execute(string command_sql);

        /// <summary>
        /// Method that verify if the data base exists
        /// </summary>
        /// <param name="table">Name of the table</param>
        /// <returns>True - exists; False - don't exists</returns>
        public bool ExistsTable(string table)
        {
            return Select("SELECT 1 FROM " + table) != null;
        }

        /// <summary>
        /// Método que pega o incremental da tabela
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        public int GetIncrement(string table)
        {
            if (!ExistsTable("CODIGOS_TABLE"))
            {
                CriaTabelaIncrementais();
            }
            this.AtualizaIncrementais(table);

            string senteca = "SELECT CODIGO FROM CODIGOS_TABLE WHERE TABELA = '" + table + "'";
            DbDataReader reader = Select(senteca);
            if (reader == null)
            {
                return -1;
            }
            else if (reader.Read())
            {
                int retorno = int.Parse(reader["CODIGO"].ToString());
                reader.Close();

                senteca = "UPDATE CODIGOS_TABLE SET CODIGO = CODIGO+1 WHERE TABELA = '" + table + "'";
                Update(senteca);

                return retorno;
            }
            else
            {
                return -1;
            }
        }

        /// <summary>
        /// Método que cria tabela de incrementais
        /// </summary>
        public void CriaTabelaIncrementais()
        {
            string sentenca = "CREATE TABLE CODIGOS_TABLE (TABELA VARCHAR(30) NOT NULL, CODIGO INT DEFAULT 0 NOT NULL, PRIMARY KEY(TABELA))";
            CreateTable(sentenca);
        }

        /// <summary>
        /// Método que atualiza os incrementais
        /// </summary>
        public void AtualizaIncrementais(string tabela)
        {
            string sentenca = "SELECT 1 FROM CODIGOS_TABLE WHERE TABELA = '" + tabela + "'";
            if (!Select(sentenca).HasRows)
            {
                sentenca = "INSERT INTO CODIGOS_TABLE (TABELA, CODIGO) VALUES ('" + tabela +"', 0)";
                Insert(sentenca);
            }
            
        }

        /// <summary>
        /// Método que pega o tipo de sistema
        /// </summary>
        public Global.TipoLog GetLog()
        {
            Global.TipoLog tipo = Global.TipoLog.SIMPLES;

            if (!ExistsTable("LOGG"))
            {
                CreateTable("CREATE TABLE LOGG( TIPO_LOG CHAR(1) DEFAULT '0' NOT NULL, PRIMARY KEY(TIPO_LOG))");
            }
            if (Select("SELECT 1 FROM LOGG") != null)
            {
                if (!Select("SELECT 1 FROM LOGG").Read())
                {
                    Insert("INSERT INTO LOGG (TIPO_LOG) VALUES ('0')");
                }
            }

            DbDataReader reader = Select("SELECT TIPO_LOG FROM LOGG");
            reader.Read();
            tipo = (reader["TIPO_LOG"].ToString().Equals("0") ? Global.TipoLog.SIMPLES : Global.TipoLog.DETALHADO);
            reader.Close();

            return tipo;
        }

        /// <summary>
        /// Método que seta o Log
        /// </summary>
        public void SetLog(Global.TipoLog tipo)
        {
            if (!ExistsTable("LOGG"))
            {
                CreateTable("CREATE TABLE LOGG( TIPO_LOG CHAR(1) DEFAULT '0' NOT NULL, PRIMARY KEY(TIPO_LOG))");
            }
            if (!Select("SELECT 1 FROM LOGG").Read())
            {
                Insert("INSERT INTO LOGG (TIPO_LOG) VALUES ('0')");
            }

            Update("UPDATE LOGG SET TIPO_LOG = '" + (tipo == Global.TipoLog.DETALHADO ? '1' : '0') + "'");
            Global.log_system = tipo;
        }

        /// <summary>
        /// Método que pega a flag de automático
        /// </summary>
        public Global.Automatico GetAutomatico()
        {
            Global.Automatico tipo = Global.Automatico.Automatico;

            if (!ExistsTable("AUTOMATICO"))
            {
                CreateTable("CREATE TABLE AUTOMATICO( AUTOMATICO CHAR(1) DEFAULT '0' NOT NULL, PRIMARY KEY(AUTOMATICO))");
            }
            if (Select("SELECT 1 FROM AUTOMATICO") != null)
                if (!Select("SELECT 1 FROM AUTOMATICO").Read())
                {
                    Insert("INSERT INTO AUTOMATICO (AUTOMATICO) VALUES ('0')");
                }

            DbDataReader reader = Select("SELECT AUTOMATICO FROM AUTOMATICO");
            reader.Read();
            tipo = (reader["AUTOMATICO"].ToString().Equals("0") ? Global.Automatico.Automatico: Global.Automatico.Manual);
            reader.Close();

            return tipo;
        }

        /// <summary>
        /// Método que seta a flag de automático
        /// </summary>
        public void SetAutomatico(Global.Automatico tipo)
        {
            if (!ExistsTable("LOGG"))
            {
                CreateTable("CREATE TABLE AUTOMATICO( AUTOMATICO CHAR(1) DEFAULT '0' NOT NULL, PRIMARY KEY(AUTOMATICO))");
            }
            if (!Select("SELECT 1 FROM AUTOMATICO").Read())
            {
                Insert("INSERT INTO AUTOMATICO (AUTOMATICO) VALUES ('0')");
            }

            Update("UPDATE AUTOMATICO SET AUTOMATICO = '" + (tipo == Global.Automatico.Automatico ? '0' : '1') + "'");
        }

        /// <summary>
        /// Método que pega a flag de automático
        /// </summary>
        public Global.Informacao GetApresentaInformacao()
        {
            Global.Informacao tipo = Global.Informacao.APRESETAR;

            if (!ExistsTable("APRESENTACAO"))
            {
                SetApresentaInformacao(Global.Informacao.APRESETAR);
            }

            DbDataReader reader = Select("SELECT APRESENTACAO FROM APRESENTACAO");
            reader.Read();
            tipo = (reader["APRESENTACAO"].ToString().Equals("0") ? Global.Informacao.NAOAPRESENTAR : Global.Informacao.APRESETAR);
            reader.Close();

            return tipo;
        }

        /// <summary>
        /// Método que seta a flag de automático
        /// </summary>
        public void SetApresentaInformacao(Global.Informacao tipo)
        {
            if (!ExistsTable("APRESENTACAO"))
            {
                CreateTable("CREATE TABLE APRESENTACAO( APRESENTACAO CHAR(1) DEFAULT '0' NOT NULL, PRIMARY KEY(APRESENTACAO))");
            }
            if (!Select("SELECT 1 FROM APRESENTACAO").Read())
            {
                Insert("INSERT INTO APRESENTACAO (APRESENTACAO) VALUES ('1')");
            }

            Update("UPDATE APRESENTACAO SET APRESENTACAO = '" + (tipo == Global.Informacao.NAOAPRESENTAR ? '0' : '1') + "'");
        }

        /// <summary>
        /// Método que converte a data para int 
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public abstract int Date_to_Int(DateTime date);

    }
}
