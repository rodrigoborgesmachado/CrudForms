using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Common;

namespace DAO
{

    /// <summary>
    /// [OBSERVERSENVIADOS] Tabela OBSERVERSENVIADOS
    /// </summary>
    public class MD_Observersenviados : MDN_Model
    {
        #region Atributos e Propriedades

        private int codigo;
        /// <summary>
        /// [CODIGO] 
        /// <summary>
        public int Codigo
        {
            get
            {
                return this.codigo;
            }
            set
            {
                this.codigo = value;
            }
        }

        private string jsonsended;
        /// <summary>
        /// [JSONSENDED] 
        /// <summary>
        public string Jsonsended
        {
            get
            {
                return this.jsonsended;
            }
            set
            {
                this.jsonsended = value;
            }
        }

        private DateTime created;
        /// <summary>
        /// [CREATED] 
        /// <summary>
        public DateTime Created
        {
            get
            {
                return this.created;
            }
            set
            {
                this.created = value;
            }
        }

        private int codigoobserver;
        /// <summary>
        /// [CODIGOOBSERVER] 
        /// <summary>
        public int Codigoobserver
        {
            get
            {
                return this.codigoobserver;
            }
            set
            {
                this.codigoobserver = value;
            }
        }


		#endregion Atributos e Propriedades

        #region Construtores

		/// <summary>
        /// Construtor Principal da classe
        /// </summary>
        public MD_Observersenviados()
            : base()
        {
            base.table = new MDN_Table("OBSERVERSENVIADOS");
            this.table.Fields_Table.Add(new MDN_Campo("CODIGO", false, Util.Enumerator.DataType.INT, 0, true, false, 0, 0));
            this.table.Fields_Table.Add(new MDN_Campo("JSONSENDED", true, Util.Enumerator.DataType.STRING, "", false, false, 4000, 0));
            this.table.Fields_Table.Add(new MDN_Campo("CREATED", true, Util.Enumerator.DataType.DATE, "", false, false, 20, 0));
            this.table.Fields_Table.Add(new MDN_Campo("CODIGOOBSERVER", true, Util.Enumerator.DataType.INT, 0, false, false, 0, 0));

            if (!base.table.ExistsTable())
                base.table.CreateTable(false);

            base.table.VerificaColunas();
        }

		/// <summary>
        /// Construtor Secundário da classe
        /// </summary>
        /// <param name="CODIGO">
        public MD_Observersenviados(int codigo)
            :this()
        {
            this.codigo = codigo;
            this.Load();
        }


		#endregion Construtores

        #region Métodos

		/// <summary>
        /// Método que faz o load da classe
        /// </summary>
        public override void Load()
        {
            Util.CL_Files.WriteOnTheLog("MD_Observersenviados.Load()", Util.Global.TipoLog.DETALHADO);

            string sentenca = base.table.CreateCommandSQLTable() + " WHERE CODIGO = " + Codigo + "";
            DbDataReader reader = DataBase.Connection.Select(sentenca);

            if (reader == null)
            {
                this.Empty = true;
            }
            else if (reader.Read())
            {
                this.Jsonsended = reader["JSONSENDED"].ToString();
                this.Created = DateTime.Parse(reader["CREATED"].ToString());
                this.Codigoobserver = int.Parse(reader["CODIGOOBSERVER"].ToString());

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
            string sentenca = "DELETE FROM " + this.table.Table_Name + " WHERE CODIGO = " + Codigo + "";
            return DataBase.Connection.Delete(sentenca);
        }

        /// <summary>
        /// Método que faz o insert da classe
        /// </summary>
        /// <returns></returns>
        public override bool Insert()
        {
            string sentenca = string.Empty;

            sentenca = "INSERT INTO " + table.Table_Name + " (" + table.TodosCampos() + ")" + 
                              " VALUES (" + this.codigo + ",  '" + this.jsonsended + "',  '" + MontaStringDateTimeFromDateTime(this.created) + "', " + this.codigoobserver + ")";
            if (DataBase.Connection.Insert(sentenca))
            {
                Empty = false;
                return true;
            }
            return false;
        }

        /// <summary>
        /// Método que faz o update da classe
        /// </summary>
        /// <returns>True - sucesso; False - erro</returns>
        public override bool Update()
        {
            string sentenca = string.Empty;

            sentenca = "UPDATE " + table.Table_Name + " SET " + 
                        "CODIGO = " + Codigo + ", JSONSENDED = '" + Jsonsended + "', CREATED = '" + MontaStringDateTimeFromDateTime(Created) + "', CODIGOOBSERVER = " + Codigoobserver + "" + 
                        " WHERE CODIGO = " + Codigo + "";

            return DataBase.Connection.Update(sentenca);
        }

		#endregion Métodos
    }
}
