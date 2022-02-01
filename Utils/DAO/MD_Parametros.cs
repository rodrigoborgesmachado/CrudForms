using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO
{
    /// <summary>
    /// [PARAMETROS] Tabela de parâmetros do sistema
    /// </summary>
    public class MD_Parametros : MDN_Model
    {
        #region Atributos e Propriedades

        private string chave = string.Empty;
        /// <summary>
        /// [CHAVE] Campo que representa a chave do parâmetro
        /// </summary>
        public string Chave
        {
            get
            {
                return this.chave;
            }
        }

        private string valor = string.Empty;
        /// <summary>
        /// [VALOR] Campo que recebe o valor do parâmetro
        /// </summary>
        public string Valor
        {
            get
            {
                return this.valor;
            }
            set
            {
                this.valor = value;
            }
        }

        #endregion Atributos e Propriedades

        #region Construtores

        /// <summary>
        /// Construtor principal da classe
        /// </summary>
        public MD_Parametros()
            : base()
        {
            Util.CL_Files.WriteOnTheLog("MD_Parametros()", Util.Global.TipoLog.DETALHADO);

            base.table = new MDN_Table("PARAMETROS");
            this.table.Fields_Table.Add(new MDN_Campo("CHAVE", true, Util.Enumerator.DataType.STRING, "0", true, false, 50, 0));
            this.table.Fields_Table.Add(new MDN_Campo("VALOR", true, Util.Enumerator.DataType.STRING, null, false, false, 100, 0));

            if (!base.table.ExistsTable())
            {
                base.table.CreateTable(false);
            }

            base.table.VerificaColunas();
        }

        /// <summary>
        /// Construtor secundário da classe
        /// </summary>
        /// <param name="chave">Chave do parâmetro</param>
        public MD_Parametros(string chave)
            : this()
        {
            Util.CL_Files.WriteOnTheLog("MD_Parametros()", Util.Global.TipoLog.DETALHADO);

            this.chave = chave;
            this.Load();
        }

        #endregion Construtores

        #region Métodos

        /// <summary>
        /// Método que faz o Load da classe
        /// </summary>
        public override void Load()
        {
            Util.CL_Files.WriteOnTheLog("MD_Parametros.Load()", Util.Global.TipoLog.DETALHADO);

            string sentenca = base.table.CreateCommandSQLTable() + " WHERE CHAVE = '" + Chave + "'";
            DbDataReader reader = DataBase.Connection.Select(sentenca);
            if (reader == null)
            {
                this.Empty = true;
            }
            else if (reader.Read())
            {
                this.chave = reader["CHAVE"].ToString();
                this.valor = reader["VALOR"].ToString();

                this.Empty = false;
                reader.Close();
            }
            else
            {
                this.Empty = true;
                reader.Close();
            }
        }

        /// <summary>
        /// Método que faz o delete da classe
        /// </summary>
        /// <returns>True - sucesso; False - erro</returns>
        public override bool Delete()
        {
            Util.CL_Files.WriteOnTheLog("MD_Parametros.Delete()", Util.Global.TipoLog.DETALHADO);

            string sentenca = "DELETE FROM " + this.table.Table_Name + " WHERE CHAVE = '" + this.Chave + "'";
            return DataBase.Connection.Delete(sentenca);
        }

        /// <summary>
        /// Método que efetua o update da classe
        /// </summary>
        /// <returns></returns>
        public override bool Update()
        {
            Util.CL_Files.WriteOnTheLog("MD_Parametros.Update()", Util.Global.TipoLog.DETALHADO);

            string sentenca = string.Empty;

            sentenca =  "UPDATE " + table.Table_Name + " SET " +
                        " VALOR = '" + this.Valor + "' " +
                        "WHERE CHAVE = '" + this.Chave + "'";

            return DataBase.Connection.Update(sentenca);
        }

        /// <summary>
        /// Método que faz o insert 
        /// </summary>
        /// <returns></returns>
        public override bool Insert()
        {
            Util.CL_Files.WriteOnTheLog("MD_Parametros.Insert()", Util.Global.TipoLog.DETALHADO);

            string sentenca = string.Empty;

            sentenca = " INSERT INTO " + table.Table_Name + " (CHAVE, VALOR) " +
                       " VALUES ('" + this.Chave + "', '" + this.Valor + "')";
            if (DataBase.Connection.Insert(sentenca))
            {
                Empty = false;
                return true;
            }
            return false;
        }


        #endregion Métodos
    }
}
