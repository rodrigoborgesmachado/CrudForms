using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO
{
    public class MDN_Table
    {
        #region Atributes

        string table_name;

        /// <summary>
        /// Table's name
        /// </summary>
        public string Table_Name
        {
            get
            {
                return table_name;
            }
            set
            {
                this.table_name = value;
            }
        }

        List<MDN_Campo> fields_Table = new List<MDN_Campo>();
        /// <summary>
        /// List of the table's fields
        /// </summary>
        public List<MDN_Campo> Fields_Table
        {
            set
            {
                this.fields_Table = value;
            }
            get
            {
                return this.fields_Table;
            }
        }

        int level = 0;
        /// <summary>
        /// Table's Relationship level (the greater more idependent the table be)
        /// </summary>
        public int Level
        {
            get
            {
                return level;
            }
            set
            {
                this.level = value;
            }
        }

        /// <summary>
        /// Data table to representes the table on the data base
        /// </summary>
        DataTable data_table = new DataTable();

        #endregion Atributes

        #region Construtores

        /// <summary>
        /// Construtor principal da classe
        /// </summary>
        /// <param name="name"></param>
        /// <param name="lista"></param>
        /// <param name="nivel"></param>
        public MDN_Table(string name)
        {
            this.table_name = name;
        }

        #endregion Construtores

        #region Methods 

        /// <summary>
        /// Method that make the creation of the table
        /// </summary>
        /// <param name="delete">Identify if delete the values on the table if it exists</param>
        /// <param name="num_lines">Number of lines to be fill on the table if 'fill' is true</param>
        public StringBuilder CreateTable(bool delete)
        {
            if (delete) this.DeleteTable();
            
            StringBuilder command = new StringBuilder(); 
            command.AppendLine("CREATE TABLE " + this.Table_Name + " (");
            int qt = fields_Table.Count, i = 0;
            foreach (MDN_Campo field in fields_Table)
            {
                i++;
                command.AppendLine(field.InsertFieldDataBase() + (i != qt ? ", " : ""));
            }

            string primary = CreateCommandPrimaryKey();
            if (!string.IsNullOrEmpty(primary))
            {
                command.AppendLine(", " + primary);
            }

            command.AppendLine(")");

            if (!ExistsTable())
            {
                DataBase.Connection.CreateTable(command.ToString());
                data_table.Clear();
                foreach (MDN_Campo field in fields_Table)
                {
                    data_table.Columns.Add(new DataColumn(field.Name_Field));
                }

                if (!Table_Name.Equals("TABELAS"))
                {
                    FillLib();
                }
            }

            return command;
        }

        /// <summary>
        /// Method that create the dictionary of the table
        /// </summary>
        public void FillLib()
        {
            MDN_Table table = new MDN_Table("TABELAS");
            table.Fields_Table.Add(new MDN_Campo("TAB", true, Util.Enumerator.DataType.STRING, null, true, true, 40, 0));
            table.Fields_Table.Add(new MDN_Campo("FIELD", true, Util.Enumerator.DataType.STRING, null, true, true, 25, 0));
            table.Fields_Table.Add(new MDN_Campo("TYPE", true, Util.Enumerator.DataType.CHAR, null, true, true, 1, 0));
            table.Fields_Table.Add(new MDN_Campo("SIZE", true, Util.Enumerator.DataType.INT, null, true, true, 1, 0));
            table.Fields_Table.Add(new MDN_Campo("PREC", true, Util.Enumerator.DataType.INT, null, true, true, 1, 0));
            table.Fields_Table.Add(new MDN_Campo("FLAG", true, Util.Enumerator.DataType.INT, null, true, true, 1, 0));
            if (!table.ExistsTable())
                table.CreateTable(false);
            table.VerificaColunas();

            string insert_base = "INSERT INTO TABELAS (tab,field,type,size,prec,flags) VALUES (";
            int i = fields_Table.Count;

            foreach (MDN_Campo field in fields_Table)
            {
                string insert = insert_base + "'" + this.Table_Name + "'";
                insert += ", '" + field.Name_Field + "'";
                switch (field.Type)
                {
                    case Util.Enumerator.DataType.CHAR:
                        insert += ",'C'";
                        break;
                    case Util.Enumerator.DataType.STRING:
                        insert += ",'C'";
                        break;
                    case Util.Enumerator.DataType.DATE:
                        insert += ",'T'";
                        break;
                    case Util.Enumerator.DataType.DECIMAL:
                        insert += ",'F'";
                        break;
                    case Util.Enumerator.DataType.INT:
                        insert += ",'I'";
                        break;
                }
                insert += "," + field.Size;
                insert += "," + field.Precision;
                insert += ",0)";

                DataBase.Connection.Insert(insert);
            }
        }

        /// <summary>
        /// Method how creates the command of primary key. Is usefull when the system is creating the database
        /// </summary>
        /// <returns>Command</returns>
        private string CreateCommandPrimaryKey()
        {
            string command_sql = string.Empty;
            int qt = 0;

            foreach (MDN_Campo field in fields_Table)
            {
                if (field.PrimaryKey)
                {
                    if (qt == 0)
                        command_sql = " PRIMARY KEY ( ";
                    else
                        command_sql += ", ";

                    command_sql += field.Name_Field + " ";
                    qt++;
                }
            }

            if (!string.IsNullOrEmpty(command_sql))
                command_sql += " )";

            return command_sql;
        }

        /// <summary>
        /// Method how make the DELETE on the database
        /// </summary>
        /// <returns>true - Sucess; false - Error</returns>
        public bool DeleteTable()
        {
            if (ExistsTable())
            {
                string command = "DELETE FROM " + this.table_name;
                return DataBase.Connection.Execute(command);
            }
            return true;
        }

        /// <summary>
        /// Method that verify if the table on the database is equal if the data table on the system
        /// </summary>
        /// <param name="database_directory">Disrectory of the database</param>
        /// <returns>True - correct; False - error</returns>
        public bool CheckDataBaseWithDataTable(string database_directory)
        {
            try
            {
                DataBase.Connection.CloseConnection();
                if (DataBase.Connection.OpenConection(database_directory, Util.Enumerator.BancoDados.SQLite))
                {
                    bool were_error = false;
                    string command = CreateCommandSQLTable();
                    DbDataReader reader = DataBase.Connection.Select(command);
                    DataTable table = new DataTable();

                    // Verify if all colluns were created on the database
                    foreach (DataColumn collumn in data_table.Columns)
                    {
                        string field = collumn.ColumnName;
                        int i = 0;
                        bool match = false;
                        for (i = 0; i < fields_Table.Count && !match; i++)
                        {
                            if (reader.GetName(i).ToUpper().Equals(field.ToUpper()))
                            {
                                table.Columns.Add(field);
                                match = true;
                            }
                        }

                        if (!match)
                        {
                            were_error = true;
                        }
                    }

                    List<string> list = new List<string>();
                    while (reader.Read())
                    {
                        list = new List<string>();
                        foreach (DataColumn collumn in data_table.Columns)
                        {
                            list.Add(reader[collumn.ColumnName].ToString());
                        }
                        table.Rows.Add(list.ToArray());
                    }
                    reader.Close();

                    for (int i = 0; i < data_table.Rows.Count; i++)
                    {
                        List<object> l1 = data_table.Rows[i].ItemArray.ToList();
                        List<object> l2 = table.Rows[i].ItemArray.ToList();

                        if (l1.Count != l2.Count)
                        {
                            were_error = true;
                            break;
                        }

                        for (int j = 0; j < l1.Count; j++)
                        {
                            if (l1[j].ToString().Replace(',', '.') != l2[j].ToString().Replace(',', '.'))
                            {
                                were_error = true;
                                break;
                            }
                        }
                        if (were_error)
                            break;
                    }

                    if (were_error)
                    {
                        Util.CL_Files.WriteOnTheLog("[TESTE_ZFX_2]The new database don't mach!", Util.Global.TipoLog.SIMPLES);
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }

                return true;
            }
            catch (Exception e)
            {
                Util.CL_Files.WriteOnTheLog("[TESTE_ZFX_2]Error comparing datatable.Error: " + e.Message, Util.Global.TipoLog.SIMPLES);
            }
            return false;
        }

        /// <summary>
        /// Method that creates the command for select in table
        /// </summary>
        /// <returns>Command SQL</returns>
        public string CreateCommandSQLTable()
        {
            string command = " SELECT ";
            string fields = string.Empty;
            int qt = fields_Table.Count, i = 1;

            foreach (MDN_Campo field in fields_Table)
            {
                fields += field.Name_Field;
                if (i < qt)
                    fields += ", ";
                i++;
            }
            command += fields + " FROM " + this.Table_Name;
            return command;
        }

        /// <summary>
        /// Método que valida se a tabela existe
        /// </summary>
        /// <returns></returns>
        public bool ExistsTable()
        {
            return DataBase.Connection.ExistsTable(this.Table_Name);
        }

        /// <summary>
        /// Método que verifica se a coluna existe na tabela
        /// </summary>
        /// <param name="coluna"></param>
        /// <returns>True - Existe; False - não existe</returns>
        public bool ExisteColuna(string coluna)
        {
            if (!ExistsTable())
                return false;

            string sentenca = "SELECT " + coluna + " FROM " + this.Table_Name;
            DbDataReader reader = DataBase.Connection.Select(sentenca);
            if (reader == null)
                return false;
            reader.Close();
            return true;
        }

        /// <summary>
        /// Método que cria a coluna
        /// </summary>
        /// <param name="coluna"></param>
        /// <returns>True - criou; False - não criou</returns>
        public bool CriaColuna(MDN_Campo coluna)
        {
            if (ExisteColuna(coluna.Name_Field))
                return true;

            string sentenca = "ALTER TABLE " + this.table_name + " ADD " + coluna.InsertFieldDataBase();
            return DataBase.Connection.Execute(sentenca);
        }

        /// <summary>
        /// Método que verifica as colunas e cria se elas não existirem
        /// </summary>
        /// <returns>True - Tudo correto; False - não conseguiu criar todas colunas</returns>
        public bool VerificaColunas()
        {
            bool retorno = true;
            foreach(MDN_Campo column in fields_Table)
            {
                if (!ExisteColuna(column.Name_Field))
                {
                    if (!CriaColuna(column))
                    {
                        retorno = false;
                        break;
                    }
                }
            }
            return retorno;
        }

        /// <summary>
        /// Método que retorna todas as colunas separadas por vírgula
        /// </summary>
        /// <returns></returns>
        public string TodosCampos()
        {
            string retorno = string.Empty;

            int i = 0;
            foreach (MDN_Campo field in fields_Table)
            {
                if (i > 0)
                    retorno += ", ";
                i++;

                retorno += field.Name_Field;
            }

            return retorno;
        }

        /// <summary>
        /// Method that creates a temp table
        /// </summary>
        /// <param name="campos">Filed on the table</param>
        /// <returns>True - Table Created; False - Impossible to create the table</returns>
        public static bool CreateTempTable(List<string> fields)
        {
            List<MDN_Campo> campos = new List<MDN_Campo>();
            foreach(string f in fields)
            {
                MDN_Campo campo = new MDN_Campo(f, false, Util.Enumerator.DataType.STRING, "", false, false, 3000, 0);
                campos.Add(campo);
            }

            return CreateTempTable(campos);
        }

        /// <summary>
        /// Method that creates a temp table
        /// </summary>
        /// <param name="campos">Filed on the table</param>
        /// <returns>True - Table Created; False - Impossible to create the table</returns>
        public static bool CreateTempTable(List<DAO.MDN_Campo> campos)
        {
            bool retorno = true;

            try
            {
                MDN_Table tabela = new MDN_Table(Util.Global.tempTable);
                tabela.Fields_Table = campos;

                if (tabela.ExistsTable())
                {
                    tabela.DeleteTable();
                    tabela.VerificaColunas();
                }
                else
                {
                    tabela.CreateTable(false);
                }

            }
            catch(Exception e)
            {
                Util.CL_Files.WriteOnTheLog("Error: " + e.Message, Util.Global.TipoLog.SIMPLES);
                retorno = false;
            }

            return retorno;
        }

        /// <summary>
        /// Method that creates a temp table
        /// </summary>
        /// <param name="campos">Filed on the table</param>
        /// <returns>True - Table Created; False - Impossible to create the table</returns>
        public static DbDataReader LerTabelaTemporaria(List<string> fields)
        {
            List<MDN_Campo> campos = new List<MDN_Campo>();
            foreach (string f in fields)
            {
                MDN_Campo campo = new MDN_Campo(f, false, Util.Enumerator.DataType.STRING, "", false, false, 3000, 0);
                campos.Add(campo);
            }

            return LerTabelaTemporaria(campos);
        }

        /// <summary>
        /// Método que faz leitura da tabela temporária a partir dos campos
        /// </summary>
        /// <param name="campos"></param>
        /// <returns></returns>
        public static DbDataReader LerTabelaTemporaria(List<DAO.MDN_Campo> campos)
        {
            DbDataReader reader = null;

            if (!DataBase.Connection.ExistsTable(Util.Global.tempTable))
            {
                CreateTempTable(campos);
            }

            string sentenca = "SELECT ";
            bool first = false;

            foreach(DAO.MDN_Campo campo in campos)
            {
                if (first)
                    sentenca += ", ";

                sentenca += campo.Name_Field;

                first = true;
            }

            sentenca += " FROM " + Util.Global.tempTable;

            reader = DataBase.Connection.Select(sentenca);
            return reader;
        }

        /// <summary>
        /// Método que dropa a tabela temporaria
        /// </summary>
        public static void DropTempTable()
        {
            DataBase.Connection.Execute("DROP TABLE TESTE");
        }

        #endregion Methods 
    }
}
