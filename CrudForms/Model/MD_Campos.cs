﻿using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{

    /// <summary>
    /// [CAMPOS] Tabela referente ao campo
    /// </summary>
    public class MD_Campos
    {
        #region Atributos e Propriedades

        private bool visible = true;
        /// <summary>
        /// Identifica se o campo deve ser visível ou não
        /// </summary>
        public bool Visible { 
            get 
            { 
                return visible; 
            } 
            set 
            {
                this.visible = value;
            } 
        }

        /// <summary>
        /// DAO que representa a classe
        /// </summary>
        public DAO.MD_Campos DAO = null;

        #endregion Atributos e Propriedades

        #region Contrutor

        /// <summary>
        /// Construtor principal da classe
        /// </summary>
        /// <param name="codigo">Código do campo</param>
        /// <param name="codigoTabela">Código da tabela</param>
        public MD_Campos(int codigo, int codigoTabela, int codigoProjeto, bool load = true)
        {
            this.DAO = new DAO.MD_Campos(codigo, new DAO.MD_Tabela(codigoTabela, codigoProjeto));
        }

        #endregion Contrutor

        #region Métodos

        /// <summary>
        /// Método que valida se há alguma foreing key associado ao campo
        /// </summary>
        /// <returns>True - há; false - não há</returns>
        private bool ForeingKeyValida()
        {
            Util.CL_Files.WriteOnTheLog("MD_Campos.ForeingKeyValida()", Util.Global.TipoLog.DETALHADO);

            bool ha = false;

            string sentenca = new DAO.MD_Relacao().table.CreateCommandSQLTable() + " WHERE CAMPOORIGEM = " + this.DAO.Codigo;

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

            string sentenca = "SELECT COMENTARIO FROM CAMPOS WHERE NOME = '" + campo + "' AND CODIGOTABELA = (SELECT CODIGO FROM TABELA WHERE NOME = '" + tabela + "')";
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

            if (this.DAO.TipoCampo.Nome.ToUpper().Contains("INT"))
            {
                data = Util.Enumerator.DataType.INT;
            }
            else if (this.DAO.TipoCampo.Nome.ToUpper().Contains("DECIMAL") || this.DAO.TipoCampo.Nome.Contains("FLOAT"))
            {
                data = Util.Enumerator.DataType.DECIMAL;
            }
            else if (this.DAO.TipoCampo.Nome.ToUpper().Contains("VARCHAR"))
            {
                data = Util.Enumerator.DataType.STRING;
            }
            else if (this.DAO.TipoCampo.Nome.ToUpper().Contains("CHAR"))
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
        public static bool ExisteCampoTabela(string tabela, string campo, int projeto)
        {
            bool retorno = false;

            string sentenca = "SELECT COUNT(1) AS QT FROM CAMPOS WHERE UPPER(NOME) = UPPER('" + campo + "') AND CODIGOTABELA IN (SELECT CODIGO FROM TABELA WHERE UPPER(NOME) = UPPER('" + tabela + "') AND PROJETO = " + projeto +")";

            DbDataReader reader = DataBase.Connection.Select(sentenca);

            if (reader.Read())
            {
                // Se o retorno for diferente de 0 então existe coluna na tabela com o mesmo nome
                retorno = !reader["QT"].ToString().Equals("0");
            }

            return retorno;
        }

        private void teste(string[] temp)
        {
            List<int> lista = new List<int>();
            int i = 0;
            foreach(string t in temp)
            {
                if (t.Equals("C"))
                {
                    lista.RemoveAt(lista.Count - 1);
                }
                else if (t.Equals("+"))
                {
                    lista.Add(lista.ElementAt(i - 2) + lista.ElementAt(i - 1));
                }
                else if (int.TryParse(t, out var res))
                {
                }
                i++;
            }

            lista.Sum();
        }

        /// <summary>
        /// Método que busca todas as tabelas do projeto
        /// </summary>
        /// <returns></returns>
        public static List<MD_Campos> RetornaTodosCampos()
        {
            List<MD_Campos> retorno = new List<MD_Campos>();

            string sentenca = "SELECT CODIGO, CODIGOTABELA FROM CAMPOS";

            DbDataReader reader = DataBase.Connection.Select(sentenca);
            List<int> campos = new List<int>();
            List<int> tabelas = new List<int>();

            while (reader.Read())
            {
                // Se o retorno for diferente de 0 então existe coluna na tabela com o mesmo nome
                campos.Add(int.Parse(reader["CODIGO"].ToString()));
                tabelas.Add(int.Parse(reader["CODIGOTABELA"].ToString()));
            }
            reader.Close();

            for(int i = 0; i < campos.Count; i++)
            {
                retorno.Add(new MD_Campos(campos[i], tabelas[i], 0));
            }

            return retorno;
        }

        #endregion Métodos
    }
}
