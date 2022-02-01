using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO
{
    public abstract class MDN_Model
    {
        /// <summary>
        /// Control if the class is empty
        /// </summary>
        public bool Empty = true;

        /// <summary>
        /// Class's control table 
        /// </summary>
        public MDN_Table table;

        /// <summary>
        /// Class's main constructor
        /// </summary>
        public MDN_Model()
        {

        }

        /// <summary>
        /// Abstract method to be implement on the heirs class
        /// </summary>
        /// <returns>True - Sucess; False - Error</returns>
        public abstract bool Insert();

        /// <summary>
        /// Abstract method to be implement on the heirs class
        /// </summary>
        /// <returns>True - Sucess; False - Error</returns>
        public abstract bool Update();

        /// <summary>
        /// Abstract method to be implement on the heirs class
        /// </summary>
        /// <returns>True - Sucess; False - Error</returns>
        public abstract bool Delete();

        /// <summary>
        /// Abstract method to be implement on the heirs class
        /// </summary>
        public abstract void Load();

        /// <summary>
        /// Quantidade totoal de projetos cadastrados
        /// </summary>
        /// <returns>Número total</returns>
        public int QuantidadeTotal()
        {
            int retorno = 0;

            DbDataReader reader = DataBase.Connection.Select(table.CreateCommandSQLTable());

            while (reader.Read())
            {
                retorno++;
            }
            reader.Close();

            return retorno;
        }

        /// <summary>
        /// Quantidade totoal de projetos cadastrados
        /// </summary>
        /// <returns>Número total</returns>
        public int QuantidadeTotal(string where)
        {
            int retorno = 0;

            DbDataReader reader = DataBase.Connection.Select(table.CreateCommandSQLTable() + " " + where);

            while (reader.Read())
            {
                retorno++;
            }
            reader.Close();

            return retorno;
        }
    }
}
