using System.Collections.Generic;
using System.Data.Common;

namespace ImportadorNamespace
{
    public class DocumentPostGreSql : Document
    {
        /// <summary>
        /// Método que retorna uma lista de tabelas e suas descrições
        /// </summary>
        /// <returns></returns>
        public override void RetornaDetalhesCampos(Visao.BarraDeCarregamento barra, ref List<Model.Campo> campos)
        {
            string sentenca = @"SELECT 
                                c.*
                                FROM information_schema.columns c 
                                LEFT join INFORMATION_SCHEMA.KEY_COLUMN_USAGE pk on (c.COLUMN_NAME = pk.COLUMN_NAME and c.TABLE_NAME = pk.TABLE_NAME)
                                where c.table_schema not in ('pg_catalog', 'information_schema')";

            DbDataReader reader = DataBase.Connection.Select(sentenca);

            while (reader.Read())
            {
                barra.AvancaBarra(1);

                string nome = reader["COLUMN_NAME"].ToString();
                bool notnull = reader["IS_NULLABLE"].ToString().ToUpper().Equals("YES");
                string tipo = reader["DATA_TYPE"].ToString();
                string valueDefault = reader["COLUMN_DEFAULT"].ToString().Replace('(', ' ').Replace(')', ' ').Trim();
                string tamanho = reader["CHARACTER_MAXIMUM_LENGTH"].ToString();
                string precisao = reader["NUMERIC_PRECISION"].ToString();
                string tabela = reader["TABLE_NAME"].ToString();

                Model.Campo c = new Model.Campo();
                c.Name_Field = nome;
                c.NotNull = notnull;
                c.Unique = false;
                c.Type = tipo;
                c.ValueDefault = valueDefault;
                c.Size = string.IsNullOrEmpty(tamanho) ? 0 : int.Parse(tamanho);
                c.Precision = string.IsNullOrEmpty(precisao) ? decimal.Zero : decimal.Parse(precisao);
                c.Tabela = tabela;

                campos.Add(c);
            }
            reader.Close();

            campos.ForEach(campo =>
            {
                sentenca = @"select count(1) as qt from (
                    SELECT cast(c.conrelid::regclass as varchar) AS table_from,
                           c.conname colname,
                           cast(pg_get_constraintdef(c.oid) as varchar) const,
                           a.attname
                           FROM pg_constraint c
                                INNER JOIN pg_namespace n
                                           ON n.oid = c.connamespace
                                CROSS JOIN LATERAL unnest(c.conkey) ak(k)
                                INNER JOIN pg_attribute a
                                           ON a.attrelid = c.conrelid
                                              AND a.attnum = ak.k
                           ) t
                           where t.table_from = '" + DataBase.Connection.GetBancoSchema() + "." + campo.Tabela + @"'
                           and t.attname = '" + campo.Name_Field + @"'
                           and t.const like '%PRIMARY%'
                    ";

                reader = DataBase.Connection.Select(sentenca);

                if (reader.Read())
                {
                    campo.PrimaryKey = (int.Parse(reader["QT"].ToString())).Equals(1);
                }
                reader.Close();

            });

        }

        /// <summary>
        /// Método que retorna uma lista de tabelas e suas descrições
        /// </summary>
        /// <returns></returns>
        public override List<DAO.MDN_Table> RetornaDetalhesTabelas(Visao.BarraDeCarregamento barra)
        {
            List<DAO.MDN_Table> tables = new List<DAO.MDN_Table>();

            string sentenca = "SELECT tablename FROM pg_tables where schemaname  not in ('pg_catalog', 'information_schema')";
            DbDataReader reader = DataBase.Connection.Select(sentenca);

            while (reader.Read())
            {
                barra.AvancaBarra(1);

                DAO.MDN_Table table = new DAO.MDN_Table(reader["tablename"].ToString());
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
            return;

            // alterar select para buscar os relacionamentos
            string sentenca = @" SELECT
                                     f.name constraint_name
                                    ,OBJECT_NAME(f.parent_object_id) referencing_table_name
                                    ,COL_NAME(fc.parent_object_id, fc.parent_column_id) referencing_column_name
                                    ,OBJECT_NAME (f.referenced_object_id) referenced_table_name
                                    ,COL_NAME(fc.referenced_object_id, fc.referenced_column_id) referenced_column_name
                                 FROM sys.foreign_keys AS f
                                 INNER JOIN sys.foreign_key_columns AS fc
                                    ON f.object_id = fc.constraint_object_id
                                 ORDER BY f.name";

            DbDataReader reader = DataBase.Connection.Select(sentenca);

            while (reader.Read())
            {
                barra.AvancaBarra(1);

                string constraintName = reader["constraint_name"].ToString();
                string tabelaOrigem = reader["referencing_table_name"].ToString();
                string tabelaDestino = reader["referenced_table_name"].ToString().ToUpper();
                string campoOrigem = reader["referencing_column_name"].ToString();
                string campoDestino = reader["referenced_column_name"].ToString();


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
