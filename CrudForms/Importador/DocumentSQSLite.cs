using System.Collections.Generic;
using System.Data.Common;

namespace ImportadorNamespace
{
    public class DocumentSQSLite : Document
    {
        /// <summary>
        /// Método que retorna uma lista de tabelas e suas descrições
        /// </summary>
        /// <returns></returns>
        public override void RetornaDetalhesCampos(Visao.BarraDeCarregamento barra,ref List<Model.Campo> campos)
        {
            foreach (DAO.MDN_Table table in tabelas)
            {
                string sentenca = "PRAGMA table_info('" + table.Table_Name + "')";

                DbDataReader reader = DataBase.Connection.Select(sentenca);

                while (reader.Read())
                {
                    barra.AvancaBarra(1);
                    string nome = reader["name"].ToString();
                    bool notnull = reader["notnull"].ToString().Equals("1");
                    string tipo = reader["type"].ToString();
                    string valueDefault = reader["dflt_value"].ToString();
                    bool primarykey = reader["pk"].ToString().Equals("1");
                    bool unique = false;
                    string tamanho = "0";
                    string precisao = "0";
                    string tabela = table.Table_Name;

                    PreencheTipoTamanhoPrecisao(ref tipo, ref tamanho, ref precisao);

                    Model.Campo c = new Model.Campo();
                    c.Name_Field = nome;
                    c.NotNull = notnull;
                    c.Type = tipo;
                    c.ValueDefault = valueDefault;
                    c.PrimaryKey = primarykey;
                    c.Unique = unique;
                    c.Size = string.IsNullOrEmpty(tamanho) ? 0 : int.Parse(tamanho);
                    c.Precision = string.IsNullOrEmpty(precisao) ? decimal.Zero : decimal.Parse(precisao);
                    c.Tabela = tabela;

                    campos.Add(c);
                }
                reader.Close();
            }
        }

        /// <summary>
        /// Método que preenche o tamanho e a precisão a partir do tipo
        /// </summary>
        /// <param name="tipo">tipo do campo</param>
        /// <param name="tamanho">tamanho do campo</param>
        /// <param name="precisao">precisao do campo</param>
        private static void PreencheTipoTamanhoPrecisao(ref string tipo, ref string tamanho, ref string precisao)
        {
            if (tipo.Contains("("))
            {
                if (tipo.Contains(","))
                {
                    string temp = tipo.Split('(')[1];
                    tamanho = temp.Split(',')[0];
                    precisao = temp.Split(',')[1].Split(')')[0];
                    tipo = tipo.Split('(')[0];
                }
                else
                {
                    string temp = tipo.Split('(')[1];
                    tamanho = temp.Split(')')[0];
                    tipo = tipo.Split('(')[0];
                }
            }
        }

        /// <summary>
        /// Método que retorna uma lista de tabelas e suas descrições
        /// </summary>
        /// <returns></returns>
        public override List<DAO.MDN_Table> RetornaDetalhesTabelas(Visao.BarraDeCarregamento barra)
        {
            List<DAO.MDN_Table> tables = new List<DAO.MDN_Table>();

            string sentenca = @"SELECT      
                                      name
                                FROM sqlite_master 
                                WHERE 
                                    type='table'";

            DbDataReader reader = DataBase.Connection.Select(sentenca);

            while (reader.Read())
            {
                barra.AvancaBarra(1);
                DAO.MDN_Table table = new DAO.MDN_Table(reader["name"].ToString());
                tables.Add(table);
            }
            reader.Close();

            return tables;
        }

        /// <summary>
        /// Métodos que retorna os relacionamentos
        /// </summary>
        /// <param name="tabelas"></param>
        /// <param name="relacionamentos"></param>
        public override void RetornaDetalhesRelacionamentos(Visao.BarraDeCarregamento barra, ref List<Model.Relacionamento> relacionamentos)
        {
            foreach (DAO.MDN_Table table in tabelas)
            {
                string sentenca = " pragma foreign_key_list(" + table.Table_Name + ")";

                DbDataReader reader = DataBase.Connection.Select(sentenca);

                while (reader.Read())
                {
                    barra.AvancaBarra(1);

                    string constraintName = string.Empty;
                    string tabelaOrigem = table.Table_Name;
                    string tabelaDestino = reader["table"].ToString().ToUpper();
                    string campoOrigem = reader["from"].ToString();
                    string campoDestino = reader["to"].ToString();


                    Model.Relacionamento r = new Model.Relacionamento();
                    r.constraintName = constraintName;
                    r.tabelaDestino = tabelaDestino;
                    r.tabelaOrigem = tabelaOrigem;
                    r.campoDestino = campoDestino;
                    r.campoOrigem = campoOrigem;

                    relacionamentos.Add(r);
                }
                reader.Close();
            }
        }
    }
}
