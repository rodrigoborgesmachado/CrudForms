using System.Collections.Generic;
using System.Data.Common;

namespace ImportadorNamespace
{
    public class DocumentMySql : Document
    {
        /// <summary>
        /// Método que retorna uma lista de tabelas e suas descrições
        /// </summary>
        /// <returns></returns>
        public override void RetornaDetalhesCampos(Visao.BarraDeCarregamento barra, ref List<Model.Campo> campos)
        {
            string sentenca = @"select * from `information_schema`.`columns` where table_schema not in ('information_schema')";

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
                sentenca = $"SELECT COUNT(1) as QT FROM information_schema.columns WHERE table_name='{campo.Tabela}' and COLUMN_NAME = '{campo.Name_Field}' and column_key = 'PRI'";

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

            string sentenca = "SELECT table_name FROM INFORMATION_SCHEMA.TABLES where table_schema not in ('information_schema')";
            DbDataReader reader = DataBase.Connection.Select(sentenca);

            while (reader.Read())
            {
                barra.AvancaBarra(1);

                DAO.MDN_Table table = new DAO.MDN_Table(reader["table_name"].ToString());
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
