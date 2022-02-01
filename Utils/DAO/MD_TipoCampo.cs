using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO
{
    /// <summary>
    /// [TIPOCAMPO] Classe referente ao tipo de campo
    /// </summary>
    public class MD_TipoCampo : MDN_Model
    {
        #region Atibutos e Propriedades

        int codigo = -1;
        /// <summary>
        /// [CODIGO] Código do tipo de campo
        /// </summary>
        public int Codigo
        {
            get
            {
                return this.codigo;
            }
        }

        string nome = string.Empty;
        /// <summary>
        /// [NOME] Nome do tipo do campo
        /// </summary>
        public string Nome
        {
            get
            {
                return this.nome;
            }
            set
            {
                this.nome = value;
            }
        }

        #endregion Atibutos e Propriedades

        #region Construtores

        /// <summary>
        /// Construtor principal da tela
        /// </summary>
        public MD_TipoCampo()
            : base()
        {
            Util.CL_Files.WriteOnTheLog("MD_Tabela()", Util.Global.TipoLog.DETALHADO);

            base.table = new MDN_Table("TIPOCAMPO");
            this.table.Fields_Table.Add(new MDN_Campo("CODIGO", true, Util.Enumerator.DataType.INT, null, true, false, 15, 0));
            this.table.Fields_Table.Add(new MDN_Campo("NOME", true, Util.Enumerator.DataType.STRING, null, true, false, 50, 0));

            if (!base.table.ExistsTable())
            {
                base.table.CreateTable(false);
                this.IncluirPrincipais();
            }

            base.table.VerificaColunas();
        }

        /// <summary>
        /// Constutor secundário da classe
        /// </summary>
        /// <param name="codigo"></param>
        public MD_TipoCampo(int codigo)
            : this()
        {
            Util.CL_Files.WriteOnTheLog("MD_Tabela()", Util.Global.TipoLog.DETALHADO);

            this.codigo = codigo;
            this.Load();
        }

        #endregion Construtores

        #region Métodos

        /// <summary>
        /// Faz o load da classe
        /// </summary>
        public override void Load()
        {
            Util.CL_Files.WriteOnTheLog("MD_TipoCampo.Load()", Util.Global.TipoLog.DETALHADO);

            string sentenca = base.table.CreateCommandSQLTable() + " WHERE CODIGO = " + Codigo;
            DbDataReader reader = DataBase.Connection.Select(sentenca);
            if (reader == null)
            {
                this.Empty = true;
            }
            else if (reader.Read())
            {
                this.nome = reader["NOME"].ToString();

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
            Util.CL_Files.WriteOnTheLog("MD_TipoCampo.Delete()", Util.Global.TipoLog.DETALHADO);

            string sentenca = "DELETE FROM " + this.table.Table_Name + " WHERE CODIGO = " + this.Codigo;
            return DataBase.Connection.Delete(sentenca);
        }

        /// <summary>
        /// Método que efetua o update da classe
        /// </summary>
        /// <returns></returns>
        public override bool Update()
        {
            Util.CL_Files.WriteOnTheLog("MD_TipoCampo.Update()", Util.Global.TipoLog.DETALHADO);

            string sentenca = string.Empty;

            sentenca = "UPDATE " + table.Table_Name + " SET " +
                        " NOME = '" + this.Nome + "' " +
                        "WHERE CODIGO = " + this.Codigo;

            return DataBase.Connection.Update(sentenca);
        }

        /// <summary>
        /// Método que faz o insert 
        /// </summary>
        /// <returns></returns>
        public override bool Insert()
        {
            Util.CL_Files.WriteOnTheLog("MD_TipoCampo.Insert()", Util.Global.TipoLog.DETALHADO);

            string sentenca = string.Empty;

            sentenca = "INSERT INTO " + table.Table_Name + " (CODIGO, NOME) " +
                       " VALUES (" + this.Codigo + ", '" + this.Nome + "')";
            if (DataBase.Connection.Insert(sentenca))
            {
                Empty = false;
                return true;
            }
            return false;
        }

        /// <summary>
        /// Método que inclui os tipops principais de data types
        /// </summary>
        private void IncluirPrincipais()
        {
            Util.CL_Files.WriteOnTheLog("MD_TipoCampo.IncluirPrincipais()", Util.Global.TipoLog.DETALHADO);

            List<MD_TipoCampo> tipos = new List<MD_TipoCampo>();

            MD_TipoCampo sample = new MD_TipoCampo(DataBase.Connection.GetIncrement(this.table.Table_Name));
            sample.Nome = "CHAR";
            tipos.Add(sample);

            sample = new MD_TipoCampo(DataBase.Connection.GetIncrement(this.table.Table_Name));
            sample.Nome = "VARCHAR";
            tipos.Add(sample);

            sample = new MD_TipoCampo(DataBase.Connection.GetIncrement(this.table.Table_Name));
            sample.Nome = "TEXT";
            tipos.Add(sample);

            sample = new MD_TipoCampo(DataBase.Connection.GetIncrement(this.table.Table_Name));
            sample.Nome = "BINARY";
            tipos.Add(sample);

            sample = new MD_TipoCampo(DataBase.Connection.GetIncrement(this.table.Table_Name));
            sample.Nome = "IMAGE";
            tipos.Add(sample);

            sample = new MD_TipoCampo(DataBase.Connection.GetIncrement(this.table.Table_Name));
            sample.Nome = "SMALLINT";
            tipos.Add(sample);

            sample = new MD_TipoCampo(DataBase.Connection.GetIncrement(this.table.Table_Name));
            sample.Nome = "INT";
            tipos.Add(sample);

            sample = new MD_TipoCampo(DataBase.Connection.GetIncrement(this.table.Table_Name));
            sample.Nome = "BIGINT";
            tipos.Add(sample);

            sample = new MD_TipoCampo(DataBase.Connection.GetIncrement(this.table.Table_Name));
            sample.Nome = "DECIMAL";
            tipos.Add(sample);

            sample = new MD_TipoCampo(DataBase.Connection.GetIncrement(this.table.Table_Name));
            sample.Nome = "NUMERIC";
            tipos.Add(sample);

            sample = new MD_TipoCampo(DataBase.Connection.GetIncrement(this.table.Table_Name));
            sample.Nome = "MONEY";
            tipos.Add(sample);

            sample = new MD_TipoCampo(DataBase.Connection.GetIncrement(this.table.Table_Name));
            sample.Nome = "FLOAT";
            tipos.Add(sample);

            sample = new MD_TipoCampo(DataBase.Connection.GetIncrement(this.table.Table_Name));
            sample.Nome = "REAL";
            tipos.Add(sample);

            sample = new MD_TipoCampo(DataBase.Connection.GetIncrement(this.table.Table_Name));
            sample.Nome = "DATETIME";
            tipos.Add(sample);

            sample = new MD_TipoCampo(DataBase.Connection.GetIncrement(this.table.Table_Name));
            sample.Nome = "TIMESTAMP";
            tipos.Add(sample);

            foreach(MD_TipoCampo campo in tipos)
            {
                campo.Insert();
            }
        }

        /// <summary>
        /// Método que retorna o tipo do campo a partir da sua descrição
        /// </summary>
        /// <param name="tipo">Tipo em string</param>
        /// <returns>Instância de classe com o novo tipo</returns>
        public static MD_TipoCampo RetornaTipoCampo(string tipo)
        {
            Util.CL_Files.WriteOnTheLog("MD_TipoCampo.RetornaTipoCampo()", Util.Global.TipoLog.DETALHADO);

            MD_TipoCampo type = new MD_TipoCampo();

            string sentenca = type.table.CreateCommandSQLTable() + " WHERE UPPER(NOME) = UPPER('" + tipo + "')";
            DbDataReader reader = DataBase.Connection.Select(sentenca);
            if (reader != null)
            {
                if (reader.Read())
                {
                    type = new MD_TipoCampo(int.Parse(reader["CODIGO"].ToString()));    
                }

                reader.Close();
            }

            // Se não existe o tipo ainda
            if(type.Codigo == -1)
            {
                MD_TipoCampo sample = new MD_TipoCampo(DataBase.Connection.GetIncrement(type.table.Table_Name));
                sample.Nome = tipo.ToUpper();
                sample.Insert();
                type = sample;
            }

            return type;
        }

        #endregion Métodos
    }
}
