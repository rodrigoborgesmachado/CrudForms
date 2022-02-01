using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using Util;
using System.Data.Common;

namespace DataBase
{
    class BancoSQLServer : Banco
    {
        /// <summary>
        /// Class of the connection with the data_base
        /// </summary>
        private static SqlConnection m_dbConnection;
        
        /// <summary>
        /// Method that open the transaction with the data_base
        /// </summary>
        /// <returns>True - Connection established; False - CAn't make the connection</returns>
        public override bool OpenConnection(string connectionString = "")
        {
            if (is_open)
                return true;

            if (string.IsNullOrEmpty(connectionString))
                return false;

            try
            {
                m_dbConnection = new SqlConnection(connectionString);
                m_dbConnection.Open();
                while (m_dbConnection.State == ConnectionState.Connecting) ;
                is_open = true;
                Util.CL_Files.WriteOnTheLog("Abrindo conexão. Banco: " + connectionString, Global.TipoLog.SIMPLES);
            }
            catch (Exception e)
            {
                Util.CL_Files.WriteOnTheLog("DataBase.OpenConnection. Erro: " + e.Message, Global.TipoLog.SIMPLES);
                is_open = false;
            }
            return is_open;
        }

        /// <summary>
        /// Method that close the connection with the database if it is open
        /// </summary>
        /// <returns>True - Connection closed; False: Problem to close the connection</returns>
        public override bool CloseConnection()
        {
            if (!is_open)
            {
                return true;
            }

            try
            {
                m_dbConnection.Close();
                m_dbConnection.Dispose();
                m_dbConnection = null;
                is_open = false;

                return true;
            }
            catch (Exception e)
            {
                Util.CL_Files.WriteOnTheLog("DataBase.CloseConnection. Erro: " + e.Message, Global.TipoLog.SIMPLES);
                is_open = true;
            }
            return false;
        }

        /// <summary>
        /// Method that makes a select on the database
        /// </summary>
        /// <param name="command_sql">Command to be executed</param>
        /// <returns>Returns a DataReader that provides the returns of consult</returns>
        public override DbDataReader Select(string command_sql)
        {
            try
            {
                if (!is_open)
                {
                    OpenConnection();
                }

                SqlCommand command = new SqlCommand(command_sql, m_dbConnection);
                SqlDataReader reader = command.ExecuteReader();
                return reader;
            }
            catch (Exception e)
            {
                Util.CL_Files.WriteOnTheLog("Problem on the sentence. Sentence: " + command_sql + ". Erro: " + e.Message, Global.TipoLog.SIMPLES);
                return null;
            }
        }
        
        /// <summary>
        ///  Method that converts date to int
        /// </summary>
        /// <param name="date">Date to be converted</param>
        /// <returns>Value integer that represents the date</returns>
        public override int Date_to_Int(DateTime date)
        {
            string sentenca = "SELECT strftime('%s','" + date.Year + "-" + (date.Month < 10 ? "0" + date.Month : date.Month.ToString()) + "-" + (date.Day < 10 ? "0" + date.Day : date.Day.ToString()) + " 00:00:00') ";
            DbDataReader reader = Select(sentenca);

            int num_date = 0;
            if (reader.Read())
            {
                num_date = int.Parse(reader[0].ToString());
            }
            reader.Close();
            return num_date;
        }

        /// <summary>
        /// Method that returns the name of table of tests
        /// </summary>
        /// <returns>name of table</returns>
        public string Tests_table()
        {
            string sql = "select 1 as TEST from " + name_table_test;
            DbDataReader reader = Select(sql);

            if (reader == null)
            {
                sql = "CREATE TABLE " + name_table_test + "(IDTESTE INT)";
                CreateTable(sql);
                sql = "INSERT INTO " + name_table_test + " VALUES (1)";
                Insert(sql);
            }
            else
                reader.Close();

            return name_table_test;
        }

        /// <summary>
        /// Method that execute the sentence on the data base
        /// </summary>
        /// <param name="command_sql">Command</param>
        /// <returns>True - Sucesso; False: erro</returns>
        public override bool Execute(string command_sql)
        {
            if (!is_open)
                OpenConnection();

            SqlCommand command = new SqlCommand(command_sql, m_dbConnection);
            try
            {
                command.Transaction = m_dbConnection.BeginTransaction(System.Data.IsolationLevel.ReadCommitted);
                command.Transaction.InitializeLifetimeService();
                command.ExecuteNonQuery();
                command.Transaction.Commit();
                command.Dispose();

                return true;
            }
            catch (Exception e)
            {
                command.Transaction.Rollback();
                Util.CL_Files.WriteOnTheLog("Erro no execute: " + command_sql + ". Erro: " + e.Message, Global.TipoLog.SIMPLES);
                return false;
            }
        }
    }
}
