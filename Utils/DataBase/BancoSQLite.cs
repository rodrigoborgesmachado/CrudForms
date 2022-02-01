using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Util;

namespace DataBase
{
    class BancoSQLite : Banco
    {

        /// <summary>
        /// Class of the connection with the data_base
        /// </summary>
        private static SQLiteConnection m_dbConnection;

        /// <summary>
        /// Method that open the transaction with the data_base
        /// </summary>
        /// <returns>True - Connection established; False - CAn't make the connection</returns>
        public override bool OpenConnection(string directory_database = "")
        {
            if (this.is_open)
                return true;

            // If the database don't exists it is create
            if (!File.Exists(directory_database))
            {
                SQLiteConnection.CreateFile(directory_database);
            }

            try
            {
                m_dbConnection = new SQLiteConnection("Data Source=" + directory_database + ";Version=3;");
                m_dbConnection.Open();
                while (m_dbConnection.State == ConnectionState.Connecting) ;
                is_open = true;
                Util.CL_Files.WriteOnTheLog("Abrindo conexão. Banco: " + directory_database, Global.TipoLog.SIMPLES);
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
                GC.Collect();
                GC.WaitForPendingFinalizers();

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

                SQLiteCommand command = new SQLiteCommand(command_sql, m_dbConnection);
                SQLiteDataReader reader = command.ExecuteReader();
                return reader;
            }
            catch (Exception e)
            {
                Util.CL_Files.WriteOnTheLog("Problem on the sentence. Sentence: " + command_sql + ". Erro: " + e.Message, Global.TipoLog.SIMPLES);
                return null;
            }
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

            SQLiteCommand command = new SQLiteCommand(command_sql, m_dbConnection);
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

        /// <summary>
        ///  Method that converts date to int
        /// </summary>
        /// <param name="date">Date to be converted</param>
        /// <returns>Value integer that represents the date</returns>
        public override int Date_to_Int(DateTime date)
        {
            string sentenca = "SELECT strftime('%s','" + date.Year + "-" + (date.Month < 10 ? "0" + date.Month : date.Month.ToString()) + "-" + (date.Day < 10 ? "0" + date.Day : date.Day.ToString()) + " 00:00:00') AS VALOR";
            DbDataReader reader = Select(sentenca);

            int num_date = 0;
            if (reader.Read())
            {
                num_date = int.Parse(reader["VALOR"].ToString());
            }
            reader.Close();
            return num_date;
        }

        /// <summary>
        ///  Method that converts the value integer to date
        /// </summary>
        /// <param name="num_date">Value integer to be converted</param>
        /// <returns>Date</returns>
        public DateTime Int_to_Date(int num_date)
        {
            string sentenca = "SELECT datetime(" + num_date + ",'unixepoch') AS TESTE ";
            DbDataReader reader = Select(sentenca);

            DateTime date = DateTime.Now;
            if (reader.Read())
            {
                date = DateTime.Parse(reader["TESTE"].ToString());
            }
            reader.Close();

            return date;
        }

        /// <summary>
        /// Método que retorna um data table a partir de um datareader
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        public static DataTable GetDataSourceFromDataReader(SQLiteDataReader reader)
        {
            DataTable dataTable = new DataTable();

            dataTable.Load(reader);

            return dataTable;
        }
    }
}
