using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    /// <summary>
    /// [TIPOCAMPO] Classe referente ao tipo de campo
    /// </summary>
    public class MD_TipoCampo
    {
        #region Atibutos e Propriedades

        /// <summary>
        /// DAO que representa a classe
        /// </summary>
        public DAO.MD_TipoCampo DAO = null;

        #endregion Atibutos e Propriedades

        #region Construtores

        /// <summary>
        /// Constutor princiapl da classe
        /// </summary>
        /// <param name="codigo"></param>
        public MD_TipoCampo(int codigo)
        {
            Util.CL_Files.WriteOnTheLog("MD_Tabela()", Util.Global.TipoLog.DETALHADO);

            this.DAO = new DAO.MD_TipoCampo(codigo);
        }

        #endregion Construtores

        #region Métodos
        /// <summary>
        /// Método que inclui os tipops principais de data types
        /// </summary>
        private void IncluirPrincipais()
        {
            Util.CL_Files.WriteOnTheLog("MD_TipoCampo.IncluirPrincipais()", Util.Global.TipoLog.DETALHADO);

            List<DAO.MD_TipoCampo> tipos = new List<DAO.MD_TipoCampo>();

            DAO.MD_TipoCampo sample = new DAO.MD_TipoCampo(DataBase.Connection.GetIncrement(this.DAO.table.Table_Name));
            sample.Nome = "CHAR";
            tipos.Add(sample);

            sample = new DAO.MD_TipoCampo(DataBase.Connection.GetIncrement(this.DAO.table.Table_Name));
            sample.Nome = "VARCHAR";
            tipos.Add(sample);

            sample = new DAO.MD_TipoCampo(DataBase.Connection.GetIncrement(this.DAO.table.Table_Name));
            sample.Nome = "TEXT";
            tipos.Add(sample);

            sample = new DAO.MD_TipoCampo(DataBase.Connection.GetIncrement(this.DAO.table.Table_Name));
            sample.Nome = "BINARY";
            tipos.Add(sample);

            sample = new DAO.MD_TipoCampo(DataBase.Connection.GetIncrement(this.DAO.table.Table_Name));
            sample.Nome = "IMAGE";
            tipos.Add(sample);

            sample = new DAO.MD_TipoCampo(DataBase.Connection.GetIncrement(this.DAO.table.Table_Name));
            sample.Nome = "SMALLINT";
            tipos.Add(sample);

            sample = new DAO.MD_TipoCampo(DataBase.Connection.GetIncrement(this.DAO.table.Table_Name));
            sample.Nome = "INT";
            tipos.Add(sample);

            sample = new DAO.MD_TipoCampo(DataBase.Connection.GetIncrement(this.DAO.table.Table_Name));
            sample.Nome = "BIGINT";
            tipos.Add(sample);

            sample = new DAO.MD_TipoCampo(DataBase.Connection.GetIncrement(this.DAO.table.Table_Name));
            sample.Nome = "DECIMAL";
            tipos.Add(sample);

            sample = new DAO.MD_TipoCampo(DataBase.Connection.GetIncrement(this.DAO.table.Table_Name));
            sample.Nome = "NUMERIC";
            tipos.Add(sample);

            sample = new DAO.MD_TipoCampo(DataBase.Connection.GetIncrement(this.DAO.table.Table_Name));
            sample.Nome = "MONEY";
            tipos.Add(sample);

            sample = new DAO.MD_TipoCampo(DataBase.Connection.GetIncrement(this.DAO.table.Table_Name));
            sample.Nome = "FLOAT";
            tipos.Add(sample);

            sample = new DAO.MD_TipoCampo(DataBase.Connection.GetIncrement(this.DAO.table.Table_Name));
            sample.Nome = "REAL";
            tipos.Add(sample);

            sample = new DAO.MD_TipoCampo(DataBase.Connection.GetIncrement(this.DAO.table.Table_Name));
            sample.Nome = "DATETIME";
            tipos.Add(sample);

            sample = new DAO.MD_TipoCampo(DataBase.Connection.GetIncrement(this.DAO.table.Table_Name));
            sample.Nome = "TIMESTAMP";
            tipos.Add(sample);

            foreach(DAO.MD_TipoCampo campo in tipos)
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

            MD_TipoCampo type = new MD_TipoCampo(-1);

            string sentenca = type.DAO.table.CreateCommandSQLTable() + " WHERE UPPER(NOME) = UPPER('" + tipo + "')";
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
            if(type.DAO.Codigo == -1)
            {
                MD_TipoCampo sample = new MD_TipoCampo(DataBase.Connection.GetIncrement(type.DAO.table.Table_Name));
                sample.DAO.Nome = tipo.ToUpper();
                sample.DAO.Insert();
                type = sample;
            }

            return type;
        }

        #endregion Métodos
    }
}
