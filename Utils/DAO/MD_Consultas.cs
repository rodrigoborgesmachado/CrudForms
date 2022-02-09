using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Common;

namespace DAO
{

    /// <summary>
    /// [CONSULTAS] Tabela CONSULTAS
    /// </summary>
    public class MD_Consultas : MDN_Model
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

        private string nomeconsulta;
        /// <summary>
        /// [NOMECONSULTA] 
        /// <summary>
        public string Nomeconsulta
        {
            get
            {
                return this.nomeconsulta;
            }
            set
            {
                this.nomeconsulta = value;
            }
        }

        private string consulta;
        /// <summary>
        /// [CONSULTA] 
        /// <summary>
        public string Consulta
        {
            get
            {
                return this.consulta;
            }
            set
            {
                this.consulta = value;
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


		#endregion Atributos e Propriedades

        #region Construtores

		/// <summary>
        /// Construtor Principal da classe
        /// </summary>
        public MD_Consultas()
            : base()
        {
            base.table = new MDN_Table("CONSULTAS");
            this.table.Fields_Table.Add(new MDN_Campo("CODIGO", false, Util.Enumerator.DataType.INT, 0, true, false, 0, 0));
            this.table.Fields_Table.Add(new MDN_Campo("NOMECONSULTA", true, Util.Enumerator.DataType.STRING, "", false, false, 100, 0));
            this.table.Fields_Table.Add(new MDN_Campo("CONSULTA", true, Util.Enumerator.DataType.STRING, "", false, false, 4000, 0));
            this.table.Fields_Table.Add(new MDN_Campo("CREATED", true, Util.Enumerator.DataType.STRING, "", false, false, 20, 0));

            if (!base.table.ExistsTable())
                base.table.CreateTable(false);

            base.table.VerificaColunas();
        }

		/// <summary>
        /// Construtor Secundário da classe
        /// </summary>
        /// <param name="CODIGO">
        public MD_Consultas(int codigo)
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
            Util.CL_Files.WriteOnTheLog("MD_Consultas.Load()", Util.Global.TipoLog.DETALHADO);

            string sentenca = base.table.CreateCommandSQLTable() + " WHERE CODIGO = " + Codigo + "";
            DbDataReader reader = DataBase.Connection.Select(sentenca);

            if (reader == null)
            {
                this.Empty = true;
            }
            else if (reader.Read())
            {
                this.Nomeconsulta = reader["NOMECONSULTA"].ToString();
                this.Consulta = reader["CONSULTA"].ToString();
                this.Created = DateTime.Parse(reader["CREATED"].ToString());

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
                              " VALUES (" + this.codigo + ",  '" + this.nomeconsulta.Replace("'", "\"") + "',  '" + this.consulta.Replace("'", "\"") + "',  '" + this.MontaStringDateTimeFromDateTime(this.created) + "')";
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
                        "CODIGO = " + Codigo + ", NOMECONSULTA = '" + Nomeconsulta.Replace("'", "\"") + "', CONSULTA = '" + Consulta.Replace("'", "\"") + "', CREATED = '" + this.MontaStringDateTimeFromDateTime(Created) + "'" + 
                        " WHERE CODIGO = " + Codigo + "";

            return DataBase.Connection.Update(sentenca);
        }

		#endregion Métodos
    }
}
