using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO
{
    /// <summary>
    /// [RELACAO] Tabela da classe
    /// </summary>
    public class MD_Relacao : MDN_Model
    {
        #region Atributos e Propriedades

        int codigo = 0;
        /// <summary>
        /// [CODIGO] Código do relacionamento
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
        /// [CODIGOPROJETO] Código do projeto
        /// </summary>
        public int Projeto
        {
            get
            {
                return this.codigoProjeto;
            }
        }

        int codigoTabelaOrigem = 0;
        MD_Tabela tabelaOrigem = null;
        /// <summary>
        /// [TABELAORIGEM] Código da tabela que origina a relação
        /// </summary>
        public MD_Tabela TabelaOrigem
        {
            get
            {
                if (this.tabelaOrigem == null)
                    this.tabelaOrigem = new MD_Tabela(this.codigoTabelaOrigem, codigoProjeto);
                return this.tabelaOrigem;
            }
        }

        int codigoCampoOrigem = 0;
        MD_Campos campoOrigem = null;
        /// <summary>
        /// [CAMPOORIGEM] Código do campo de origem da relação
        /// </summary>
        public MD_Campos CampoOrigem
        {
            get
            {
                if (this.campoOrigem == null)
                    this.campoOrigem = new MD_Campos(this.codigoCampoOrigem, this.TabelaOrigem);
                return this.campoOrigem;
            }
        }

        int codigoTabelaDestino = 0;
        MD_Tabela tabelaDestino = null;
        /// <summary>
        /// [TABELADESTINO] Código da tabela que origina a relação
        /// </summary>
        public MD_Tabela TabelaDestino
        {
            get
            {
                if (this.tabelaDestino == null)
                    this.tabelaDestino = new MD_Tabela(this.codigoTabelaDestino, codigoProjeto);
                return this.tabelaDestino;
            }
        }

        int codigoCampoDestino = 0;
        MD_Campos campoDestino = null;
        /// <summary>
        /// [CAMPODESTINO] Código do campo de Destino da relação
        /// </summary>
        public MD_Campos CampoDestino
        {
            get
            {
                if (this.campoDestino == null)
                    this.campoDestino = new MD_Campos(this.codigoCampoDestino, this.TabelaDestino);
                return this.campoDestino;
            }
        }

        string cardinalidadeOrigem = string.Empty;
        /// <summary>
        /// [CARDINALIDADEORIGEM] Campo que tem a cardinalidade do campo de origem
        /// </summary>
        public string CardinalidadeOrigem
        {
            get
            {
                return this.cardinalidadeOrigem;
            }
            set
            {
                this.cardinalidadeOrigem = value;
            }
        }

        string cardinalidadeDestino = string.Empty;
        /// <summary>
        /// [CARDINALIDADEDESTINO] Campo que tem a cardinalidade do campo de origem
        /// </summary>
        public string CardinalidadeDestino
        {
            get
            {
                return this.cardinalidadeDestino;
            }
            set
            {
                this.cardinalidadeDestino = value;
            }
        }

        string nomeForeingKey = string.Empty;
        /// <summary>
        /// [FOREINGKEY] Nome da foreing key
        /// </summary>
        public string NomeForeingKey
        {
            get
            {
                return this.nomeForeingKey;
            }
            set
            {
                this.nomeForeingKey = value;
            }
        }

        #endregion Atributos e Propriedades

        #region Construtores

        /// <summary>
        /// Construtor principal da classe
        /// </summary>
        public MD_Relacao()
            : base()
        {
            Util.CL_Files.WriteOnTheLog("MD_Relacao()", Util.Global.TipoLog.DETALHADO);

            base.table = new MDN_Table("RELACAO");
            this.table.Fields_Table.Add(new MDN_Campo("CODIGO", true, Util.Enumerator.DataType.INT, null, true, false, 15, 0));
            this.table.Fields_Table.Add(new MDN_Campo("CODIGOPROJETO", true, Util.Enumerator.DataType.INT, null, false, false, 15, 0));
            this.table.Fields_Table.Add(new MDN_Campo("TABELAORIGEM", true, Util.Enumerator.DataType.INT, null, false, false, 15, 0));
            this.table.Fields_Table.Add(new MDN_Campo("CAMPOORIGEM", true, Util.Enumerator.DataType.INT, null, false, false, 15, 0));
            this.table.Fields_Table.Add(new MDN_Campo("TABELADESTINO", true, Util.Enumerator.DataType.INT, null, false, false, 15, 0));
            this.table.Fields_Table.Add(new MDN_Campo("CAMPODESTINO", true, Util.Enumerator.DataType.INT, null, false, false, 15, 0));
            this.table.Fields_Table.Add(new MDN_Campo("CARDINALIDADEORIGEM", true, Util.Enumerator.DataType.STRING, null, false, false, 1, 0));
            this.table.Fields_Table.Add(new MDN_Campo("CARDINALIDADEDESTINO", true, Util.Enumerator.DataType.STRING, null, false, false, 1, 0));
            this.table.Fields_Table.Add(new MDN_Campo("FOREINGKEY", false, Util.Enumerator.DataType.STRING, null, false, false, 200, 0));

            if (!base.table.ExistsTable())
                base.table.CreateTable(false);

            base.table.VerificaColunas();
        }

        /// <summary>
        /// Construtor secundário da classe
        /// </summary>
        /// <param name="codigo"></param>
        /// <param name="projeto"></param>
        /// <param name="tabelaOrigem"></param>
        /// <param name="tabelaDestino"></param>
        /// <param name="campoOrigem"></param>
        /// <param name="campoDestino"></param>
        public MD_Relacao(int codigo, int projeto, MD_Tabela tabelaOrigem, MD_Tabela tabelaDestino, MD_Campos campoOrigem, MD_Campos campoDestino) : 
            this()
        {
            Util.CL_Files.WriteOnTheLog("MD_Relacao()", Util.Global.TipoLog.DETALHADO);

            this.codigo = codigo;
            this.codigoProjeto = projeto;

            this.tabelaOrigem = tabelaOrigem;
            this.codigoTabelaOrigem = this.TabelaOrigem.Codigo;

            this.tabelaDestino = tabelaDestino;
            this.codigoTabelaDestino = this.TabelaDestino.Codigo;

            this.campoOrigem = campoOrigem;
            this.codigoCampoOrigem = this.CampoOrigem.Codigo;

            this.campoDestino = campoDestino;
            this.codigoCampoDestino = this.CampoDestino.Codigo;

            this.Load();
        }

        #endregion Construtores

        #region Métodos

        /// <summary>
        /// Método que faz o load da classe
        /// </summary>
        public override void Load()
        {
            Util.CL_Files.WriteOnTheLog("MD_Relacao.Load()", Util.Global.TipoLog.DETALHADO);

            string sentenca = base.table.CreateCommandSQLTable() + " WHERE CODIGO = " + Codigo;
            DbDataReader reader = DataBase.Connection.Select(sentenca);
            if (reader == null)
            {
                this.Empty = true;
            }
            else if (reader.Read())
            {
                this.CardinalidadeOrigem = reader["CARDINALIDADEORIGEM"].ToString();
                this.CardinalidadeDestino = reader["CARDINALIDADEDESTINO"].ToString();
                this.NomeForeingKey = reader["FOREINGKEY"].ToString();

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
        /// Método que faz o insert da classe
        /// </summary>
        /// <returns>True - sucesso; False - erro</returns>
        public override bool Insert()
        {
            Util.CL_Files.WriteOnTheLog("MD_Relacao.Insert()", Util.Global.TipoLog.DETALHADO);

            string sentenca = string.Empty;

            sentenca = "INSERT INTO " + table.Table_Name + " (CODIGO, CODIGOPROJETO, TABELAORIGEM, CAMPOORIGEM, TABELADESTINO, CAMPODESTINO, CARDINALIDADEORIGEM, CARDINALIDADEDESTINO, FOREINGKEY) " +
                              " VALUES (" + this.Codigo + ", " + codigoProjeto + ", " + this.TabelaOrigem.Codigo + ", " + this.CampoOrigem.Codigo + ", " + this.TabelaDestino.Codigo + ", " + this.CampoDestino.Codigo + ", '" + this.CardinalidadeOrigem + "', '" + this.CardinalidadeDestino + "', '" + this.NomeForeingKey + "')";
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
        /// <returns>True - Sucesso; False - Erro</returns>
        public override bool Update()
        {
            Util.CL_Files.WriteOnTheLog("MD_Relacao.Update()", Util.Global.TipoLog.DETALHADO);

            string sentenca = string.Empty;

            sentenca = "UPDATE " + table.Table_Name + " SET " +
                                " TABELAORIGEM = " + this.TabelaOrigem.Codigo +"," +
                                " CAMPOORIGEM = " + this.CampoOrigem.Codigo +"," +
                                " TABELADESTINO = " + this.TabelaDestino.Codigo+"," +
                                " CAMPODESTINO = " + this.CampoDestino.Codigo +"," +
                                " CARDINALIDADEORIGEM = '" + this.CardinalidadeOrigem +"'," +
                                " CARDINALIDADEDESTINO = '" + this.CardinalidadeDestino +"'," +
                                " FOREINGKEY = '" + this.NomeForeingKey + "' " +
                        "WHERE CODIGO = " + this.Codigo;

            return DataBase.Connection.Update(sentenca);
        }

        /// <summary>
        /// Método que faz o delete da classe
        /// </summary>
        /// <returns></returns>
        public override bool Delete()
        {
            Util.CL_Files.WriteOnTheLog("MD_Relacao.Delete()", Util.Global.TipoLog.DETALHADO);

            return DataBase.Connection.Delete("DELETE FROM " + this.table.Table_Name + " WHERE CODIGO = " + this.codigo);
        }

        #endregion Métodos

    }
}
