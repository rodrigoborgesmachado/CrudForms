using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Common;

namespace DAO.
{

    /// <summary>
    /// [OBSERVER] Tabela OBSERVER
    /// </summary>
    public class MD_Observer : MDN_Model
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

        private string descricao;
        /// <summary>
        /// [DESCRICAO] 
        /// <summary>
        public string Descricao
        {
            get
            {
                return this.descricao;
            }
            set
            {
                this.descricao = value;
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

        private DateTime updated;
        /// <summary>
        /// [UPDATED] 
        /// <summary>
        public DateTime Updated
        {
            get
            {
                return this.updated;
            }
            set
            {
                this.updated = value;
            }
        }

        private string isactive;
        /// <summary>
        /// [ISACTIVE] 
        /// <summary>
        public string Isactive
        {
            get
            {
                return this.isactive;
            }
            set
            {
                this.isactive = value;
            }
        }

        private string emailsenviar;
        /// <summary>
        /// [EMAILSENVIAR] 
        /// <summary>
        public string Emailsenviar
        {
            get
            {
                return this.emailsenviar;
            }
            set
            {
                this.emailsenviar = value;
            }
        }

        private int intervalorodar;
        /// <summary>
        /// [INTERVALORODAR] 
        /// <summary>
        public int Intervalorodar
        {
            get
            {
                return this.intervalorodar;
            }
            set
            {
                this.intervalorodar = value;
            }
        }


		#endregion Atributos e Propriedades

        #region Construtores

		/// <summary>
        /// Construtor Principal da classe
        /// </summary>
        public MD_Observer()
            : base()
        {
            base.table = new MDN_Table("OBSERVER");
            this.table.Fields_Table.Add(new MDN_Campo("CODIGO", false, Util.Enumerator.DataType.INT, 0, true, false, 0, 0));
            this.table.Fields_Table.Add(new MDN_Campo("DESCRICAO", true, Util.Enumerator.DataType.STRING, "", false, false, 400, 0));
            this.table.Fields_Table.Add(new MDN_Campo("CONSULTA", true, Util.Enumerator.DataType.STRING, "", false, false, 4500, 0));
            this.table.Fields_Table.Add(new MDN_Campo("CREATED", true, Util.Enumerator.DataType.DATE, DateTime.Now, false, false, 0, 0));
            this.table.Fields_Table.Add(new MDN_Campo("UPDATED", true, Util.Enumerator.DataType.DATE, DateTime.Now, false, false, 0, 0));
            this.table.Fields_Table.Add(new MDN_Campo("ISACTIVE", true, Util.Enumerator.DataType.CHAR, "1", false, false, 1, 0));
            this.table.Fields_Table.Add(new MDN_Campo("EMAILSENVIAR", true, Util.Enumerator.DataType.STRING, "", false, false, 2000, 0));
            this.table.Fields_Table.Add(new MDN_Campo("INTERVALORODAR", true, Util.Enumerator.DataType.INT, 0, false, false, 0, 0));

            if (!base.table.ExistsTable())
                base.table.CreateTable(false);

            base.table.VerificaColunas();
        }

		/// <summary>
        /// Construtor Secundário da classe
        /// </summary>
        /// <param name="CODIGO">
        public MD_Observer(int codigo)
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
            Util.CL_Files.WriteOnTheLog("MD_Observer.Load()", Util.Global.TipoLog.DETALHADO);

            string sentenca = base.table.CreateCommandSQLTable() + " WHERE CODIGO = " + Codigo + "";
            DbDataReader reader = DataBase.Connection.Select(sentenca);

            if (reader == null)
            {
                this.Empty = true;
            }
            else if (reader.Read())
            {
                this.Descricao = reader["DESCRICAO"].ToString();
                this.Consulta = reader["CONSULTA"].ToString();
                this.Created = DateTime.Parse(reader["CREATED"].ToString());
                this.Updated = DateTime.Parse(reader["UPDATED"].ToString());
                this.Isactive = reader["ISACTIVE"].ToString();
                this.Emailsenviar = reader["EMAILSENVIAR"].ToString();
                this.Intervalorodar = int.Parse(reader["INTERVALORODAR"].ToString());

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
                              " VALUES (" + this.codigo + ",  '" + this.descricao + "',  '" + this.consulta + "', " + MontaStringDateFromDateTime(this.created) + ", " + MontaStringDateFromDateTime(this.updated) + ",  '" + this.isactive + "',  '" + this.emailsenviar + "', " + this.intervalorodar + ")";
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
                        "CODIGO = " + Codigo + ", DESCRICAO = '" + Descricao + "', CONSULTA = '" + Consulta + "', CREATED = '" + MontaStringDateFromDateTime(Created) + "', UPDATED = '" + MontaStringDateFromDateTime(Updated) + "', ISACTIVE = '" + Isactive + "', EMAILSENVIAR = '" + Emailsenviar + "', INTERVALORODAR = " + Intervalorodar + "" + 
                        " WHERE CODIGO = " + Codigo + "";

            return DataBase.Connection.Update(sentenca);
        }

		#endregion Métodos
    }
}
