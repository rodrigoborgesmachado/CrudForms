using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO
{
    /// <summary>
    /// [TABELA] Classe de controle da tabela "tabela"
    /// </summary>
    public class MD_Tabela : MDN_Model
    {
        #region Atributos e Propriedades

        int codigo = 0;
        /// <summary>
        /// [CODIGO] Código referente a tabela
        /// </summary>
        public int Codigo
        {
            get
            {
                return this.codigo;
            }
        }

        int projeto = -1;
        /// <summary>
        /// [PROJETO] Código do projeto ao qual a tabela pertence
        /// </summary>
        public int Projeto
        {
            get
            {
                return this.projeto;
            }
        }

        string nome = string.Empty;
        /// <summary>
        /// [NOME] Nome da tabela
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

        string descricao = string.Empty;
        /// <summary>
        /// [DESCRICAO] Descrição da tabela
        /// </summary>
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

        string notas = string.Empty;
        /// <summary>
        /// [NOTAS] Notas da tabela
        /// </summary>
        public string Notas
        {
            get
            {
                return this.notas;
            }
            set
            {
                this.notas = value;
            }
        }

        #endregion Atributos e Propriedades

        #region Contrutores

        /// <summary>
        /// Construtor principal da classe que cria a tabela e faz sua manutenção
        /// </summary>
        public MD_Tabela() 
            : base()
        {
            Util.CL_Files.WriteOnTheLog("MD_Tabela()", Util.Global.TipoLog.DETALHADO);

            base.table = new MDN_Table("TABELA");
            this.table.Fields_Table.Add(new MDN_Campo("CODIGO", true, Util.Enumerator.DataType.INT, null, true, false, 15, 0));
            this.table.Fields_Table.Add(new MDN_Campo("PROJETO", true, Util.Enumerator.DataType.INT, null, true, false, 15, 0));
            this.table.Fields_Table.Add(new MDN_Campo("NOME", true, Util.Enumerator.DataType.STRING, null, false, false, 50, 0));
            this.table.Fields_Table.Add(new MDN_Campo("DESCRICAO", false, Util.Enumerator.DataType.STRING, null, false, false, 400, 0));
            this.table.Fields_Table.Add(new MDN_Campo("NOTAS", false, Util.Enumerator.DataType.STRING, null, false, false, 400, 0));

            if (!base.table.ExistsTable())
                base.table.CreateTable(false);

            base.table.VerificaColunas();
        }

        /// <summary>
        /// Construtor secundário da classe
        /// </summary>
        /// <param name="codigo">Código da tabela</param>
        /// <param name="projeto">Código do projeto</param>
        public MD_Tabela(int codigo, int projeto, bool load = true):
            this()
        {
            Util.CL_Files.WriteOnTheLog("MD_Tabela()", Util.Global.TipoLog.DETALHADO);

            this.codigo = codigo;
            this.projeto = projeto;
            if(load) this.Load();
        }

        #endregion Contrutores

        #region Métodos

        /// <summary>
        /// Método que faz o load da classe
        /// </summary>
        public override void Load()
        {
            Util.CL_Files.WriteOnTheLog("MD_Tabela.Load()", Util.Global.TipoLog.DETALHADO);

            string sentenca = base.table.CreateCommandSQLTable() + " WHERE CODIGO = " + Codigo + " AND PROJETO = " + this.Projeto;
            DbDataReader reader = DataBase.Connection.Select(sentenca);
            if (reader == null)
            {
                this.Empty = true;
            }
            else if (reader.Read())
            {
                this.nome = reader["NOME"].ToString();
                this.Descricao = reader["DESCRICAO"].ToString();
                this.Notas = reader["NOTAS"].ToString();

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
            Util.CL_Files.WriteOnTheLog("MD_Tabela.Delete()", Util.Global.TipoLog.DETALHADO);

            string sentenca = "DELETE FROM " + this.table.Table_Name + " WHERE CODIGO = " + this.Codigo + " AND PROJETO = " + this.Projeto;
            return DataBase.Connection.Delete(sentenca);
        }

        /// <summary>
        /// Método que efetua o update da classe
        /// </summary>
        /// <returns></returns>
        public override bool Update()
        {
            Util.CL_Files.WriteOnTheLog("MD_Tabela.Update()", Util.Global.TipoLog.DETALHADO);

            string sentenca = string.Empty;

            sentenca = "UPDATE " + table.Table_Name + " SET " +
                        " NOME = '" + this.Nome + "', " +
                        " DESCRICAO = '" + this.Descricao + "', " +
                        " NOTAS = '" + this.Notas + "' " +
                        "WHERE CODIGO = " + this.Codigo +" AND PROJETO  = " + projeto;

            return DataBase.Connection.Update(sentenca);
        }

        /// <summary>
        /// Método que faz o insert 
        /// </summary>
        /// <returns></returns>
        public override bool Insert()
        {
            Util.CL_Files.WriteOnTheLog("MD_Tabela.Insert()", Util.Global.TipoLog.DETALHADO);

            string sentenca = string.Empty;

            sentenca = "INSERT INTO " + table.Table_Name + " (CODIGO, PROJETO, NOME, DESCRICAO, NOTAS) " +
                       " VALUES (" + this.Codigo + ", " + projeto + ", '" + this.Nome + "', '" + this.Descricao + "', '" + this.Notas + "')";
            if (DataBase.Connection.Insert(sentenca))
            {
                Empty = false;
                return true;
            }
            return false;
        }

        /// <summary>
        /// Método que retorna uma lista com os campos que pertencem à tabela
        /// </summary>
        /// <returns>Lista com os campos</returns>
        public List<DAO.MD_Campos> CamposDaTabela()
        {
            Util.CL_Files.WriteOnTheLog("MD_Tabela.CamposDaTabela()", Util.Global.TipoLog.DETALHADO);

            List<MD_Campos> campos = new List<MD_Campos>();

            string sentenca = "SELECT CODIGO FROM CAMPOS WHERE CODIGOTABELA = " + this.Codigo;
            DbDataReader reader = DataBase.Connection.Select(sentenca);

            while (reader.Read())
            {
                campos.Add(new MD_Campos(int.Parse(reader["codigo"].ToString()), this));
            }

            return campos;
        }

        /// <summary>
        /// Método que valida se a tabela já existe no projeto
        /// </summary>
        /// <param name="tabela">nome da tabela</param>
        /// <param name="projeto">Código do projeto</param>
        /// <returns></returns>
        public static bool ValidaExisteTabelaProjeto(string tabela, int projeto)
        {
            bool retorno = false;

            string sentenca = "SELECT COUNT(1) AS QT FROM TABELA WHERE UPPER(NOME) = UPPER('" + tabela + "') AND PROJETO = " + projeto + "";

            DbDataReader reader = DataBase.Connection.Select(sentenca);

            if (reader.Read())
            {
                // Se o retorno for diferente de 0 então existe coluna na tabela com o mesmo nome
                retorno = !reader["QT"].ToString().Equals("0");
            }

            return retorno;
        }

        #endregion Métodos
    }
}
