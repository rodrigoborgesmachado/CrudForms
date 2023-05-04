using System.Collections.Generic;
using System.Data.Common;

namespace Util
{
    public class DocumentOracle : Document
    {
        /// <summary>
        /// Método que retorna uma lista de tabelas e suas descrições
        /// </summary>
        /// <returns></returns>
        public override void RetornaDetalhesCampos(Visao.BarraDeCarregamento barra, ref List<Model.Campo> campos)
        {
            string sentenca = @"SELECT 
                                case
                                	when cons.constraint_type = 'P'
                                	then 'S'
                                	else 'N'
                                end primarykey,
                                case
                                	when cons.constraint_type = 'U'
                                	then 'S'
                                	else 'N'
                                end uniqueKey
                                , cols1.* 
                                FROM user_tab_cols cols1
                                left join all_cons_columns cols on UPPER(cols.TABLE_NAME) = UPPER(cols1.TABLE_NAME) and UPPER(cols.COLUMN_NAME) = UPPER(cols1.COLUMN_NAME)
                                left join all_constraints cons on cons.constraint_name = cols.constraint_name
                                order by cols1.table_name";

            DbDataReader reader = DataBase.Connection.Select(sentenca);

            while (reader.Read())
            {
                barra.AvancaBarra(1);

                string nome = reader["COLUMN_NAME"].ToString();
                bool notnull = reader["NULLABLE"].ToString().ToUpper().Equals("N");
                string tipo = reader["DATA_TYPE"].ToString();
                string valueDefault = reader["DATA_DEFAULT"].ToString();
                bool primarykey = reader["primarykey"].ToString().Equals("S");
                bool unique = reader["uniqueKey"].ToString().Equals("S");
                string tamanho = reader["DEFAULT_LENGTH"].ToString();
                string precisao = reader["DATA_PRECISION"].ToString();
                string tabela = reader["TABLE_NAME"].ToString();

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

        /// <summary>
        /// Método que retorna uma lista de tabelas e suas descrições
        /// </summary>
        /// <returns></returns>
        public override List<DAO.MDN_Table> RetornaDetalhesTabelas(Visao.BarraDeCarregamento barra)
        {
            List<DAO.MDN_Table> tables = new List<DAO.MDN_Table>();

            string sentenca = "SELECT TABLE_NAME FROM USER_TABLES";

            DbDataReader reader = DataBase.Connection.Select(sentenca);

            while (reader.Read())
            {
                barra.AvancaBarra(1);

                DAO.MDN_Table table = new DAO.MDN_Table(reader["TABLE_NAME"].ToString());
                tables.Add(table);
            }
            reader.Close();

            return tables;
        }

        /// <summary>
        /// Método que retorna os detalhes de todos os relacionamentos
        /// </summary>
        /// <param name="barra"></param>
        /// <param name="relacionamentos"></param>
        public override void RetornaDetalhesRelacionamentos(Visao.BarraDeCarregamento barra, ref List<Model.Relacionamento> relacionamentos)
        {
            string sentenca = @" SELECT a.constraint_name, a.table_name, a.column_name,  c.owner, 
                                      c_pk.table_name r_table_name,  b.column_name r_column_name
                                 FROM user_cons_columns a
                                 JOIN user_constraints c ON a.owner = c.owner
                                      AND a.constraint_name = c.constraint_name
                                 JOIN user_constraints c_pk ON c.r_owner = c_pk.owner
                                      AND c.r_constraint_name = c_pk.constraint_name
                                 JOIN user_cons_columns b ON C_PK.owner = b.owner
                                      AND  C_PK.CONSTRAINT_NAME = b.constraint_name AND b.POSITION = a.POSITION     
                                WHERE c.constraint_type = 'R'";

            DbDataReader reader = DataBase.Connection.Select(sentenca);

            while (reader.Read())
            {
                barra.AvancaBarra(1);

                string constraintName = reader["constraint_name"].ToString();
                string tabelaOrigem = reader["table_name"].ToString();
                string tabelaDestino = reader["r_table_name"].ToString().ToUpper();
                string campoOrigem = reader["column_name"].ToString();
                string campoDestino = reader["r_column_name"].ToString();


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
