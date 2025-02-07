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
            string query = @"
        SELECT 
            c.table_name,
            c.column_name,
            c.is_nullable,
            c.data_type,
            c.character_maximum_length,
            c.numeric_precision,
            c.column_default,
            CASE 
                WHEN tc.constraint_type = 'PRIMARY KEY' THEN 1
                ELSE 0
            END AS is_primary_key
        FROM information_schema.columns c
        LEFT JOIN information_schema.key_column_usage kcu
            ON c.table_schema = kcu.table_schema
            AND c.table_name = kcu.table_name
            AND c.column_name = kcu.column_name
        LEFT JOIN information_schema.table_constraints tc
            ON kcu.table_schema = tc.table_schema
            AND kcu.table_name = tc.table_name
            AND kcu.constraint_name = tc.constraint_name
            AND tc.constraint_type = 'PRIMARY KEY'
        WHERE c.table_schema NOT IN ('pg_catalog', 'information_schema');";

            DbDataReader reader = DataBase.Connection.Select(query);

            while (reader.Read())
            {
                barra.AvancaBarra(1);

                string nome = reader["column_name"].ToString();
                bool notnull = reader["is_nullable"].ToString().ToUpper() == "NO";
                string tipo = reader["data_type"].ToString();
                string valueDefault = reader["column_default"]?.ToString().Trim() ?? string.Empty;
                string tamanho = reader["character_maximum_length"]?.ToString();
                string precisao = reader["numeric_precision"]?.ToString();
                string tabela = reader["table_name"].ToString();
                bool isPrimaryKey = reader["is_primary_key"].ToString() == "1";

                Model.Campo c = new Model.Campo
                {
                    Name_Field = nome,
                    NotNull = notnull,
                    Unique = false,
                    Type = tipo,
                    ValueDefault = valueDefault,
                    Size = string.IsNullOrEmpty(tamanho) ? 0 : int.Parse(tamanho),
                    Precision = string.IsNullOrEmpty(precisao) ? decimal.Zero : decimal.Parse(precisao),
                    Tabela = tabela,
                    PrimaryKey = isPrimaryKey
                };

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
