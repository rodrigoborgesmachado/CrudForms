using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO
{

    /// <summary>
    /// [CAMPOS] Tabela referente ao campo
    /// </summary>
    public partial class MD_Campos : MDN_Model
    {
        #region Atributos e Propriedades

        int codigo = -1;
        /// <summary>
        /// [CODIGO] Código do campo
        /// </summary>
        public int Codigo
        {
            get
            {
                return this.codigo;
            }
        }

        int codigoProjeto = 0;
        /// <summary>
        /// Projeto ao qual a tabela que o campo pertence (nem é armazenado no banco)
        /// </summary>
        public int Projeto
        {
            get
            {
                return this.codigoProjeto;
            }
            set
            {
                this.codigoProjeto = value;
            }
        }

        int codigoTabela = -1;
        /// <summary>
        /// [CODIGOTABELA] Tabela que o campo pertence
        /// </summary>
        MD_Tabela tabela = null;
        public MD_Tabela Tabela
        {
            get
            {
                if (this.tabela == null)
                    this.tabela = new MD_Tabela(codigoTabela, this.Projeto);
                return this.tabela;
            }
            set
            {
                this.tabela = value;
                this.codigoTabela = this.tabela.Codigo;

            }
        }

        string nome = string.Empty;
        /// <summary>
        /// [NOME] Nome do campo
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

        bool primaryKey = false;
        /// <summary>
        /// [CHAVE] Identifica se é uma primary key
        /// </summary>
        public bool PrimaryKey
        {
            get
            {
                return this.primaryKey;
            }
            set
            {
                this.primaryKey = value;
            }
        }

        string dominio = string.Empty;
        /// <summary>
        /// [DOMINIO] Domínio da tabela
        /// </summary>
        public string Dominio
        {
            get
            {
                return this.dominio;
            }
            set
            {
                this.dominio = value;
            }
        }

        int codigoTipo = -1;
        MD_TipoCampo tipo = null;
        /// <summary>
        /// [CODIGOTIPO] Código do tipo do campo
        /// </summary>
        public MD_TipoCampo TipoCampo
        {
            get
            {
                if (this.tipo == null)
                    this.tipo = new MD_TipoCampo(codigoTipo);
                return this.tipo;
            }
            set
            {
                this.tipo = value;
                this.codigoTipo = this.tipo.Codigo;
            }
        }

        bool notnull = false;
        /// <summary>
        /// [NOTNULL] Identifica se o campo é notnull
        /// </summary>
        public bool NotNull
        {
            get
            {
                return this.notnull;
            }
            set
            {
                this.notnull = value;
            }
        }

        bool unique = false;
        /// <summary>
        /// [UNIQUE] Identifica se o campo é UNIQUE
        /// </summary>
        public bool Unique
        {
            get
            {
                return this.unique;
            }
            set
            {
                this.unique = value;
            }
        }

        string check = string.Empty;
        /// <summary>
        /// [CHECK] Campo que valida o check do campo da tabela
        /// </summary>
        public string Check
        {
            get
            {
                return this.check;
            }
            set
            {
                this.check = value;
            }
        }

        object default_value = null;
        /// <summary>
        /// [DEFAULT] Valor default do campo
        /// </summary>
        public object Default
        {
            get
            {
                return this.default_value;
            }
            set
            {
                this.default_value = value;
            }
        }

        string comentario = string.Empty;
        /// <summary>
        /// [COMENTARIO] Comentário do campo
        /// </summary>
        public string Comentario
        {
            get
            {
                return this.comentario;
            }
            set
            {
                this.comentario = value;
            }
        }

        int tamanho = 0;
        /// <summary>
        /// [TAMANHO] Tamanho do campo
        /// </summary>
        public int Tamanho
        {
            get
            {
                return this.tamanho;
            }
            set
            {
                this.tamanho = value;
            }
        }

        decimal precisao = decimal.Zero;
        /// <summary>
        /// [PRECISAO] Precisão do campo
        /// </summary>
        public decimal Precisao
        {
            get
            {
                return this.precisao;
            }
            set
            {
                this.precisao = value;
            }
        }

        /// <summary>
        /// identifica se o campo tem foreing key
        /// </summary>
        public bool ForeingKey
        {
            get
            {
                return this.ForeingKeyValida();
            }
        }

        #endregion Atributos e Propriedades

        #region Construtores

        /// <summary>
        /// Construtor principal da classe
        /// </summary>
        public MD_Campos()
            : base()
        {
            Util.CL_Files.WriteOnTheLog("MD_Campos()", Util.Global.TipoLog.DETALHADO);

            base.table = new MDN_Table("CAMPOS");
            this.table.Fields_Table.Add(new MDN_Campo("CODIGO", true, Util.Enumerator.DataType.INT, null, true, false, 15, 0));
            this.table.Fields_Table.Add(new MDN_Campo("CODIGOTABELA", true, Util.Enumerator.DataType.INT, null, true, false, 15, 0));
            this.table.Fields_Table.Add(new MDN_Campo("NOME", true, Util.Enumerator.DataType.STRING, null, true, false, 50, 0));
            this.table.Fields_Table.Add(new MDN_Campo("CHAVE", true, Util.Enumerator.DataType.CHAR, "0", true, false, 1, 0));
            this.table.Fields_Table.Add(new MDN_Campo("DOMINIO", true, Util.Enumerator.DataType.STRING, null, true, false, 100, 0));
            this.table.Fields_Table.Add(new MDN_Campo("CODIGOTIPO", true, Util.Enumerator.DataType.INT, null, true, false, 15, 0));
            this.table.Fields_Table.Add(new MDN_Campo("NAONULO", true, Util.Enumerator.DataType.CHAR, "0", true, false, 1, 0));
            this.table.Fields_Table.Add(new MDN_Campo("UNICO", true, Util.Enumerator.DataType.CHAR, "0", true, false, 1, 0));
            this.table.Fields_Table.Add(new MDN_Campo("CHECAR", true, Util.Enumerator.DataType.STRING, null, true, false, 300, 0));
            this.table.Fields_Table.Add(new MDN_Campo("PADRAO", true, Util.Enumerator.DataType.STRING, null, true, false, 300, 0));
            this.table.Fields_Table.Add(new MDN_Campo("COMENTARIO", true, Util.Enumerator.DataType.STRING, null, true, false, 900, 0));
            this.table.Fields_Table.Add(new MDN_Campo("TAMANHO", true, Util.Enumerator.DataType.INT, null, true, false, 10, 0));
            this.table.Fields_Table.Add(new MDN_Campo("PRECISAO", true, Util.Enumerator.DataType.DECIMAL, null, true, false, 15, 4));

            if (!base.table.ExistsTable())
            {
                base.table.CreateTable(false);
            }

            base.table.VerificaColunas();
        }

        /// <summary>
        /// Método secundário da classe
        /// </summary>
        /// <param name="codigo">Código do campo</param>
        /// <param name="tabela">Código da tabela</param>
        public MD_Campos(int codigo, MD_Tabela tabela)
            :this()
        {
            Util.CL_Files.WriteOnTheLog("MD_Campos()", Util.Global.TipoLog.DETALHADO);

            this.codigo = codigo;
            this.Tabela = tabela;
            this.Load();
        }

        #endregion Construtores

        #region Métodos

        /// <summary>
        /// Método que faz o load da classe
        /// </summary>
        public override void Load()
        {
            Util.CL_Files.WriteOnTheLog("MD_Campos.Load()", Util.Global.TipoLog.DETALHADO);

            string sentenca = base.table.CreateCommandSQLTable() + " WHERE CODIGO = " + Codigo + " AND CODIGOTABELA = " + this.Tabela.Codigo;
            DbDataReader reader = DataBase.Connection.Select(sentenca);
            if (reader == null)
            {
                this.Empty = true;
            }
            else if (reader.Read())
            {
                this.nome = reader["NOME"].ToString();
                this.primaryKey = reader["CHAVE"].ToString().Equals("1");
                this.dominio = reader["DOMINIO"].ToString();
                this.codigoTipo = int.Parse(reader["CODIGOTIPO"].ToString());
                this.TipoCampo = new MD_TipoCampo(this.codigoTipo);
                this.notnull = reader["NAONULO"].Equals("1");
                this.unique = reader["UNICO"].Equals("1");
                this.check = reader["CHECAR"].ToString();
                this.default_value = reader["PADRAO"].ToString();
                this.comentario = reader["COMENTARIO"].ToString();
                this.tamanho = int.Parse(reader["TAMANHO"].ToString());
                this.precisao = decimal.Parse(reader["PRECISAO"].ToString());

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
            Util.CL_Files.WriteOnTheLog("MD_Campos.Delete()", Util.Global.TipoLog.DETALHADO);

            string sentenca = "DELETE FROM " + this.table.Table_Name + " WHERE CODIGO = " + this.Codigo + " AND CODIGOTABELA = " + this.Tabela.Codigo;
            return DataBase.Connection.Delete(sentenca);
        }

        /// <summary>
        /// Método que efetua o update da classe
        /// </summary>
        /// <returns></returns>
        public override bool Update()
        {
            Util.CL_Files.WriteOnTheLog("MD_Campos.Update()", Util.Global.TipoLog.DETALHADO);

            string sentenca = string.Empty;

            sentenca = "UPDATE " + table.Table_Name + " SET " +
                        " NOME = '" + this.Nome + "', " +
                        " CHAVE      = '" + (this.PrimaryKey ? "1" : "0") + "', " +
                        " DOMINIO    = '" + this.Dominio + "', " +
                        " CODIGOTIPO = '" + this.TipoCampo.Codigo + "', " +
                        " NAONULO    = '" + (this.NotNull ? "1" : "0") + "', " +
                        " UNICO      = '" + (this.Unique ? "1" : "0") + "', " +
                        " CHECAR     = '" + this.Check + "', " +
                        " PADRAO     = '" + this.default_value.ToString().Replace("'", "") + "', " +
                        " COMENTARIO = '" + this.Comentario + "', " +
                        " TAMANHO    = " + tamanho.ToString() + ", " +
                        " PRECISAO   = " + precisao.ToString().Replace(',', '.') + " " +
                        "WHERE CODIGO = " + this.Codigo + " AND CODIGOTABELA = " + this.Tabela.Codigo;

            return DataBase.Connection.Update(sentenca);
        }

        /// <summary>
        /// Método que faz o insert 
        /// </summary>
        /// <returns></returns>
        public override bool Insert()
        {
            Util.CL_Files.WriteOnTheLog("MD_Campos.Insert()", Util.Global.TipoLog.DETALHADO);

            string sentenca = string.Empty;

            sentenca = " INSERT INTO " + table.Table_Name + " (CODIGO, CODIGOTABELA, NOME, CHAVE, DOMINIO, CODIGOTIPO, NAONULO, UNICO, CHECAR, PADRAO, COMENTARIO, TAMANHO, PRECISAO) " +
                       " VALUES (" + this.Codigo + ", " + this.Tabela.Codigo + ", '" + this.Nome + "', '"  + (this.PrimaryKey ? "1" : "0") + "', '" + this.Dominio + "', " + this.TipoCampo.Codigo + ", '" + (this.NotNull ? "1" : "0") + "', '" + (this.Unique ? "1" : "0") + "', '" + this.Check + "', '" + this.default_value.ToString().Replace("'", "") + "', '" + this.Comentario + "', " + this.Tamanho.ToString() + ", "  + precisao.ToString().Replace(',', '.') + ")";
            if (DataBase.Connection.Insert(sentenca))
            {
                Empty = false;
                return true;
            }
            return false;
        }

        /// <summary>
        /// Método que valida se há alguma foreing key associado ao campo
        /// </summary>
        /// <returns>True - há; false - não há</returns>
        private bool ForeingKeyValida()
        {
            Util.CL_Files.WriteOnTheLog("MD_Campos.ForeingKeyValida()", Util.Global.TipoLog.DETALHADO);

            bool ha = false;

            string sentenca = new MD_Relacao().table.CreateCommandSQLTable() + " WHERE CAMPOORIGEM = " + this.codigo;

            DbDataReader reader = DataBase.Connection.Select(sentenca);
            if (reader == null)
            {
                ha = false;
            }
            else if (reader.Read())
                ha = true;

            return ha;
        }

        /// <summary>
        /// Método que busca o comentário de um campo a partir da tabela e do campo passado em referência
        /// </summary>
        /// <param name="tabela">Tabela onde o campo está</param>
        /// <param name="campo">Campo a ser pesquisado</param>
        /// <returns></returns>
        public static string BuscaComentario(string tabela, string campo)
        {
            Util.CL_Files.WriteOnTheLog("MD_Campos.BuscaComentario()", Util.Global.TipoLog.DETALHADO);

            string retorno = string.Empty;

            string sentenca = "SELECT COMENTARIO FROM CAMPOS WHERE NOME = '" + campo + "' AND CODIGOTABELA = (SELECT CODIGO FROM TABELA WHERE NOME = '" + tabela +"')";
            DbDataReader reader = DataBase.Connection.Select(sentenca);

            if (reader.Read())
            {
                retorno = reader["COMENTARIO"].ToString();
                reader.Close();
            }

            return retorno;
        }

        /// <summary>
        /// Método que retorna o tipo do campo referente ao núcleo
        /// </summary>
        /// <returns></returns>
        public Util.Enumerator.DataType TipoNucleo()
        {
            Util.CL_Files.WriteOnTheLog("MD_Campos.TipoNucleo()", Util.Global.TipoLog.DETALHADO);

            Util.Enumerator.DataType data = Util.Enumerator.DataType.STRING;

            if (this.TipoCampo.Nome.ToUpper().Contains("INT"))
            {
                data = Util.Enumerator.DataType.INT;
            }
            else if (this.tipo.Nome.ToUpper().Contains("DECIMAL") || this.tipo.Nome.Contains("FLOAT"))
            {
                data = Util.Enumerator.DataType.DECIMAL;
            }
            else if (this.tipo.Nome.ToUpper().Contains("VARCHAR"))
            {
                data = Util.Enumerator.DataType.STRING;
            }
            else if (this.tipo.Nome.ToUpper().Contains("CHAR"))
            {
                data = Util.Enumerator.DataType.CHAR;
            }
            else
            {
                data = Util.Enumerator.DataType.STRING;
            }

            return data;
        }

        /// <summary>
        /// Método que valida se o campo já existe na tabela 
        /// </summary>
        /// <param name="tabela">Tabela para verificar se o campo já existe</param>
        /// <param name="campo">Campo para ser verificado se já existe</param>
        /// <returns>True - Existe; False - Não existe</returns>
        public static bool ExisteCampoTabela(string tabela, string campo)
        {
            bool retorno = false;

            string sentenca = "SELECT COUNT(1) AS QT FROM CAMPOS WHERE UPPER(NOME) = UPPER('" + campo + "') AND CODIGOTABELA IN (SELECT CODIGO FROM TABELA WHERE UPPER(NOME) = UPPER('" + tabela + "'))";

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
