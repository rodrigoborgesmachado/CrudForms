using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Valores
    {
        public List<string> campos = new List<string>();
        public List<string> valores = new List<string>();

        /// <summary>
        /// Método que atualiza os valores no banco
        /// </summary>
        /// <param name="tabela"></param>
        /// <param name="campos"></param>
        /// <param name="mensagem"></param>
        /// <returns></returns>
        public bool DeleteValores(Model.MD_Tabela tabela, List<Model.MD_Campos> campos, out string mensagem)
        {
            bool retorno = true;
            string delete = MontaComandoDelete(tabela.DAO.Nome, campos, out mensagem);

            if (string.IsNullOrEmpty(delete))
            {
                return false;
            }

            string connection = Parametros.ConexaoBanco.DAO.Valor;

            DataBase.Connection.CloseConnection();
            if (!DataBase.Connection.OpenConection(connection, Util.Enumerator.BancoDados.SQL_SERVER))
            {
                mensagem = "Não foi possível conectar";
                retorno = false;
            }
            else
            {
                Util.CL_Files.WriteOnTheLog(delete, Util.Global.TipoLog.SIMPLES);
                retorno = DataBase.Connection.Delete(delete); 
            }

            DataBase.Connection.CloseConnection();
            DataBase.Connection.OpenConection(Util.Global.app_base_file, Util.Enumerator.BancoDados.SQLite);

            return retorno;
        }

        /// <summary>
        /// Método que insere os valores no banco
        /// </summary>
        /// <param name="tabela"></param>
        /// <param name="campos"></param>
        /// <param name="mensagem"></param>
        /// <returns></returns>
        public bool InsereValores(Model.MD_Tabela tabela, List<Model.MD_Campos> campos, out string mensagem)
        {
            bool retorno = true;
            mensagem = string.Empty;

            string insert = MontaComandoInsert(tabela.DAO.Nome, campos, out mensagem);

            if (string.IsNullOrEmpty(insert))
            {
                return false;
            }

            string connection = Parametros.ConexaoBanco.DAO.Valor;

            DataBase.Connection.CloseConnection();
            if (!DataBase.Connection.OpenConection(connection, Util.Enumerator.BancoDados.SQL_SERVER))
            {
                mensagem = "Não foi possível conectar";
                retorno = false;
            }
            else
            {
                Util.CL_Files.WriteOnTheLog(insert, Util.Global.TipoLog.SIMPLES);
                retorno = DataBase.Connection.Insert(insert);
            }

            DataBase.Connection.CloseConnection();
            DataBase.Connection.OpenConection(Util.Global.app_base_file, Util.Enumerator.BancoDados.SQLite);

            return retorno;
        }

        /// <summary>
        /// Método que atualiza os valores no banco
        /// </summary>
        /// <param name="tabela"></param>
        /// <param name="campos"></param>
        /// <param name="mensagem"></param>
        /// <returns></returns>
        public bool AtualizaValores(Model.MD_Tabela tabela, List<Model.MD_Campos> campos, Valores valoresAnteriores, out string mensagem)
        {
            bool retorno = true;
            string update = MontaComandoUpdate(tabela.DAO.Nome, campos, valoresAnteriores, out mensagem);

            if (string.IsNullOrEmpty(update))
            {
                mensagem = "Não há campos a atualizar!";
                return false;
            }

            string connection = Parametros.ConexaoBanco.DAO.Valor;

            try
            {
                DataBase.Connection.CloseConnection();
                if (!DataBase.Connection.OpenConection(connection, Util.Enumerator.BancoDados.SQL_SERVER))
                {
                    mensagem = "Não foi possível conectar";
                    retorno = false;
                }
                else
                {
                    Util.CL_Files.WriteOnTheLog(update, Util.Global.TipoLog.SIMPLES);
                    retorno = DataBase.Connection.Update(update);
                }
            }
            finally
            {
                DataBase.Connection.CloseConnection();
                DataBase.Connection.OpenConection(Util.Global.app_base_file, Util.Enumerator.BancoDados.SQLite);
            }

            return retorno;
        }

        /// <summary>
        /// Método que monta o comando update
        /// </summary>
        /// <param name="tabela"></param>
        /// <param name="campos"></param>
        /// <returns></returns>
        private string MontaComandoUpdate(string tabela, List<Model.MD_Campos> campos, Valores valoresAnteriores, out string mensagem) 
        {
            string retorno = string.Empty;
            mensagem = string.Empty;

            retorno = $"update [{tabela}] set ";
            List<Model.MD_Campos> camposPk = new List<MD_Campos>();
            List<int> listaCampos = new List<int>();
            bool temAlteracao = false;

            for(int i = 0; i< campos.Count; i++)
            {
                if (campos[i].DAO.PrimaryKey)
                {
                    camposPk.Add(campos[i]);
                    listaCampos.Add(i);
                    continue;
                }

                // Só adiciona se houver alteração no campo
                if (this.valores[i] == valoresAnteriores.valores[i])
                    continue;

                retorno += Valores.MontaCampoUpdate(campos[i], valores[i]) + ", ";
                temAlteracao = true;
            }

            if(camposPk.Count == 0)
            {
                mensagem = "Não há chave primary key!";
                retorno = string.Empty;
            }
            else
            {
                retorno += "WHERE ";
                for (int i = 0; i< camposPk.Count; i++)
                {
                    if(i != 0)
                    {
                        retorno += " AND ";
                    }
                    retorno += Valores.MontaCampoWhereUpdateDelete(camposPk[i], valores[listaCampos[i]]);
                }
            }

            retorno = retorno.Replace(", WHERE", " WHERE");

            if (!temAlteracao) retorno = string.Empty;

            return retorno;
        }

        /// <summary>
        /// Método que monta o comando update
        /// </summary>
        /// <param name="tabela"></param>
        /// <param name="campos"></param>
        /// <returns></returns>
        private string MontaComandoInsert(string tabela, List<Model.MD_Campos> campos, out string mensagem)
        {
            string retorno = string.Empty;
            mensagem = string.Empty;

            retorno = $"insert into {tabela} (";
            string values = ") VALUES (";
            for (int i = 0; i < campos.Count; i++)
            {
                if (i != 0)
                {
                    retorno += ", ";
                    values += ", ";
                }
                retorno += $"{campos[i].DAO.Nome}";
                values += MontaCampoInsert(campos[i], valores[i]);
            }
            retorno += values + ")";
            
            return retorno;
        }

        /// <summary>
        /// Método que monta o comando update
        /// </summary>
        /// <param name="tabela"></param>
        /// <param name="campos"></param>
        /// <returns></returns>
        private string MontaComandoDelete(string tabela, List<Model.MD_Campos> campos, out string mensagem)
        {
            string retorno = string.Empty;
            mensagem = string.Empty;

            retorno = $"DELETE FROM {tabela} WHERE ";
            bool achouPk = false;

            for (int i = 0; i < campos.Count; i++)
            {
                if (campos[i].DAO.PrimaryKey)
                {
                    achouPk = true;
                    retorno += MontaCampoWhereUpdateDelete(campos[i], valores[i]);
                }
            }

            if (!achouPk)
            {
                mensagem = "Não há chave PK";
                retorno = string.Empty;
            }

            return retorno;
        }

        /// <summary>
        /// Método que preenche os valores a partir da tabela
        /// </summary>
        /// <param name="tabela"></param>
        /// <returns></returns>
        public static List<Valores> BuscaLista(MD_Tabela tabela, List<MD_Campos> campos, Model.Filtro filtro, out string consulta)
        {
            List<Valores> valores = new List<Valores>();

            try
            {
                consulta = CreateCommandSQLTable(tabela, campos, filtro);

                Visao.BarraDeCarregamento barra = new Visao.BarraDeCarregamento(int.Parse(Model.Parametros.QuantidadeLinhasTabelas.DAO.Valor), "Buscando");
                barra.Show();

                string connection = Parametros.ConexaoBanco.DAO.Valor;

                DataBase.Connection.CloseConnection();
                DataBase.Connection.OpenConection(connection, Util.Enumerator.BancoDados.SQL_SERVER);

                DbDataReader reader = DataBase.Connection.Select(consulta);

                if (reader == null)
                {
                    barra.Dispose();
                    return null;
                }

                List<string> columns = new List<string>();
                campos.ForEach(campo =>
                {
                    columns.Add(campo.DAO.Nome);
                });

                while (reader.Read())
                {
                    List<string> values = new List<string>();

                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        var temp = reader[campos[i].DAO.Nome];

                        if (campos[i].DAO.TipoCampo.Nome.Equals("VARBINARY"))
                        {
                            byte[] binaryString = (byte[])temp;

                            // if the original encoding was UTF-8
                            string y = Encoding.UTF8.GetString(binaryString);
                            values.Add(y);
                        }
                        else
                        {
                            values.Add(temp.ToString());
                        }
                    }

                    valores.Add(new Valores()
                    {
                        campos = columns,
                        valores = values
                    });
                    barra.AvancaBarra(1);
                }
                reader.Close();
                barra.Dispose();

                return valores;
            }
            finally
            {
                DataBase.Connection.CloseConnection();
                DataBase.Connection.OpenConection(Util.Global.app_base_file, Util.Enumerator.BancoDados.SQLite);
            }
        }

        /// <summary>
        /// Método que preenche os valores a partir da tabela
        /// </summary>
        /// <param name="tabela"></param>
        /// <returns></returns>
        public static List<Valores> BuscaLista(string sentenca)
        {
            List<Valores> valores = new List<Valores>();

            try
            {
                string connection = Parametros.ConexaoBanco.DAO.Valor;

                Visao.BarraDeCarregamento barra = new Visao.BarraDeCarregamento(int.Parse(Model.Parametros.QuantidadeLinhasTabelas.DAO.Valor), "Buscando");
                barra.Show();

                DataBase.Connection.CloseConnection();
                DataBase.Connection.OpenConection(connection, Util.Enumerator.BancoDados.SQL_SERVER);

                DbDataReader reader = DataBase.Connection.Select(sentenca);

                List<string> columns = new List<string>();

                if (reader == null)
                {
                    barra.Dispose();
                    return null;
                }

                int j = 0;
                while (reader.Read())
                {
                    List<string> values = new List<string>();

                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        if (j == 0)
                            columns.Add(reader.GetName(i).ToString());

                        values.Add(reader[i].ToString());
                    }

                    valores.Add(new Valores()
                    {
                        campos = columns,
                        valores = values
                    });
                    barra.AvancaBarra(1);
                    j++;
                }
                reader.Close();
                barra.Dispose();
            }
            finally
            {
                DataBase.Connection.CloseConnection();
                DataBase.Connection.OpenConection(Util.Global.app_base_file, Util.Enumerator.BancoDados.SQLite);
            }

            return valores;
        }

        /// <summary>
        /// Method that creates the command for select in table
        /// </summary>
        /// <returns>Command SQL</returns>
        private static string CreateCommandSQLTable(MD_Tabela tabela, List<MD_Campos> campos, Filtro filtro)
        {
            string command = $" SELECT TOP {Model.Parametros.QuantidadeLinhasTabelas.DAO.Valor} ";
            string fields = string.Empty;
            int qt = campos.Count, i = 1;

            foreach (Model.MD_Campos field in campos)
            {
                fields += $"[{field.DAO.Nome}]";
                if (i < qt)
                    fields += ", ";
                i++;
            }
            command += fields + " FROM [" + tabela.DAO.Nome + "]";
            command += MontaWhere(filtro, campos);

            if(filtro.Order != null)
            {
                if(filtro.Order.CampoOrdenacao != null)
                {
                    command += $" ORDER BY { (campos.IndexOf(campos.Where(campo => campo.DAO.Nome == filtro.Order.CampoOrdenacao.DAO.Nome).FirstOrDefault()) + 1)} {(filtro.Order.Asc ? "ASC" : "DESC")}";
                }
            }

            return command;
        }

        /// <summary>
        /// Método que monta o where a partir do filtro
        /// </summary>
        /// <param name="filtro"></param>
        /// <returns></returns>
        private static string MontaWhere(Model.Filtro filtro, List<MD_Campos> campos)
        {
            string retorno = string.Empty;

            retorno = " WHERE 1 = 1";

            for(int i = 0; i < filtro.valores.Count; i++)
            {
                if (string.IsNullOrEmpty(filtro.valores[i].Replace(";", "")))
                    continue;

                retorno += " AND (" + MontaCampoWhere(campos[i], filtro.valores[i]) + ")";
            }

            return retorno;
        }

        /// <summary>
        /// Método que monta o campo Where
        /// </summary>
        /// <param name="campo"></param>
        /// <param name="valor"></param>
        /// <returns></returns>
        private static string MontaCampoWhere(Model.MD_Campos campo, string valor)
        {
            string retorno = string.Empty;

            if(valor.Contains(";"))
            {
                bool first = true;

                switch (campo.DAO.TipoCampo.Nome)
                {
                    case "BIGINT":
                        retorno += $"[{campo.DAO.Nome}] IN ({string.Join(",", valor.Split(';').ToList())})";
                        break;
                    case "INT":
                        retorno += $"[{campo.DAO.Nome}] IN ({string.Join(",", valor.Split(';').ToList())})";
                        break;
                    case "TINYINT":
                        retorno += $"[{campo.DAO.Nome}] IN ({string.Join(",", valor.Split(';').ToList())})";
                        break;
                    case "SMALLINT":
                        retorno += $"[{campo.DAO.Nome}] IN ({string.Join(",", valor.Split(';').ToList())})";
                        break;
                    case "DECIMAL":
                        retorno += $"[{campo.DAO.Nome}] IN ({string.Join(",", valor.Split(';').ToList()).Replace(",", ".")})";
                        break;
                    case "REAL":
                        retorno += $"[{campo.DAO.Nome}] IN ({string.Join(",", valor.Split(';').ToList()).Replace(",", ".")})";
                        break;
                    case "NVARCHAR":
                        first = true;
                        valor.Split(';').ToList().ForEach(item =>
                        {
                            if (first) first = false;
                            else retorno += " OR ";

                            retorno += $"UPPER([{campo.DAO.Nome}]) like '%{item.ToUpper()}%'";
                        });
                        break;
                    case "NTEXT":
                        first = true;
                        valor.Split(';').ToList().ForEach(item =>
                        {
                            if (!string.IsNullOrEmpty(item))
                            {
                                if (first) first = false;
                                else retorno += " OR ";

                                retorno += $"UPPER([{campo.DAO.Nome}]) like '%{item.ToUpper()}%'";
                            }
                        });
                        break;
                    case "TEXT":
                        first = true;
                        valor.Split(';').ToList().ForEach(item =>
                        {
                            if (!string.IsNullOrEmpty(item))
                            {
                                if (first) first = false;
                                else retorno += " OR ";

                                retorno += $"UPPER([{campo.DAO.Nome}]) like '%{item.ToUpper()}%'";
                            }
                        });
                        break;
                    case "VARCHAR":
                        first = true;
                        valor.Split(';').ToList().ForEach(item =>
                        {
                            if (!string.IsNullOrEmpty(item))
                            {
                                if (first) first = false;
                                else retorno += " OR ";

                                retorno += $"UPPER([{campo.DAO.Nome}]) like '%{item.ToUpper()}%'";
                            }
                        });
                        break;
                    case "DATETIME":
                        retorno += $"[{campo.DAO.Nome}] = '{MontaStringDateTimeFromDateTime(DateTime.Parse(valor))}'";
                        break;
                    default:
                        first = true;
                        valor.Split(';').ToList().ForEach(item =>
                        {
                            if (!string.IsNullOrEmpty(item))
                            {
                                if (first) first = false;
                                else retorno += " OR ";

                                retorno += $"UPPER([{campo.DAO.Nome}]) like '%{item.ToUpper()}%'";
                            }
                        });
                        break;
                }
            }
            else
            {
                switch (campo.DAO.TipoCampo.Nome)
                {
                    case "BIGINT":
                        retorno += $"[{campo.DAO.Nome}] = {valor}";
                        break;
                    case "INT":
                        retorno += $"[{campo.DAO.Nome}] = {valor}";
                        break;
                    case "TINYINT":
                        retorno += $"[{campo.DAO.Nome}] = {valor}";
                        break;
                    case "SMALLINT":
                        retorno += $"[{campo.DAO.Nome}] = {valor}";
                        break;
                    case "DECIMAL":
                        retorno += $"[{campo.DAO.Nome}] = {valor.ToString().Replace(",", ".")}";
                        break;
                    case "REAL":
                        retorno += $"[{campo.DAO.Nome}] = {valor.ToString().Replace(",", ".")}";
                        break;
                    case "NVARCHAR":
                        retorno += $"UPPER([{campo.DAO.Nome}]) like '%{valor.ToUpper()}%'";
                        break;
                    case "NTEXT":
                        retorno += $"UPPER([{campo.DAO.Nome}]) like '%{valor.ToUpper()}%'";
                        break;
                    case "TEXT":
                        retorno += $"UPPER([{campo.DAO.Nome}]) like '%{valor.ToUpper()}%'";
                        break;
                    case "VARCHAR":
                        retorno += $"UPPER([{campo.DAO.Nome}]) like '%{valor.ToUpper()}%'";
                        break;
                    case "DATETIME":
                        retorno += $"[{campo.DAO.Nome}] = '{MontaStringDateTimeFromDateTime(DateTime.Parse(valor))}'";
                        break;
                    default:
                        retorno += $"[{campo.DAO.Nome}] = '{valor}'";
                        break;
                }
            }

            return retorno;
        }

        /// <summary>
        /// Método que monta o campo Where
        /// </summary>
        /// <param name="campo"></param>
        /// <param name="valor"></param>
        /// <returns></returns>
        private static string MontaCampoWhereUpdateDelete(Model.MD_Campos campo, string valor)
        {
            string retorno = string.Empty;

            switch (campo.DAO.TipoCampo.Nome)
            {
                case "BIGINT":
                    retorno += $"[{campo.DAO.Nome}] = {valor}";
                    break;
                case "INT":
                    retorno += $"[{campo.DAO.Nome}] = {valor}";
                    break;
                case "TINYINT":
                    retorno += $"[{campo.DAO.Nome}] = {valor}";
                    break;
                case "SMALLINT":
                    retorno += $"[{campo.DAO.Nome}] = {valor}";
                    break;
                case "DECIMAL":
                    retorno += $"[{campo.DAO.Nome}] = {valor.ToString().Replace(",", ".")}";
                    break;
                case "REAL":
                    retorno += $"[{campo.DAO.Nome}] = {valor.ToString().Replace(",", ".")}";
                    break;
                case "NVARCHAR":
                    retorno += $"[{campo.DAO.Nome}] = '{valor}'";
                    break;
                case "NTEXT":
                    retorno += $"[{campo.DAO.Nome}] = '{valor}'";
                    break;
                case "TEXT":
                    retorno += $"[{campo.DAO.Nome}] = '{valor}'";
                    break;
                case "VARCHAR":
                    retorno += $"[{campo.DAO.Nome}] = '{valor}'";
                    break;
                case "DATETIME":
                    retorno += $"[{campo.DAO.Nome}] = '{MontaStringDateTimeFromDateTime(DateTime.Parse(valor))}'";
                    break;
                default:
                    retorno += $"[{campo.DAO.Nome}] = '{valor}'";
                    break;
            }

            return retorno;
        }

        /// <summary>
        /// Método que monta o campo Where
        /// </summary>
        /// <param name="campo"></param>
        /// <param name="valor"></param>
        /// <returns></returns>
        private static string MontaCampoUpdate(Model.MD_Campos campo, string valor)
        {
            string retorno = string.Empty;

            switch (campo.DAO.TipoCampo.Nome)
            {
                case "BIGINT":
                    retorno += $"[{campo.DAO.Nome}] = {valor}";
                    break;
                case "INT":
                    retorno += $"[{campo.DAO.Nome}] = {valor}";
                    break;
                case "TINYINT":
                    retorno += $"[{campo.DAO.Nome}] = {valor}";
                    break;
                case "SMALLINT":
                    retorno += $"[{campo.DAO.Nome}] = {valor}";
                    break;
                case "DECIMAL":
                    retorno += $"[{campo.DAO.Nome}] = {valor.ToString().Replace(",", ".")}";
                    break;
                case "REAL":
                    retorno += $"[{campo.DAO.Nome}] = {valor.ToString().Replace(",", ".")}";
                    break;
                case "NVARCHAR":
                    retorno += $"[{campo.DAO.Nome}] = '{valor}'";
                    break;
                case "NTEXT":
                    retorno += $"[{campo.DAO.Nome}] = '{valor}'";
                    break;
                case "TEXT":
                    retorno += $"[{campo.DAO.Nome}] = '{valor}'";
                    break;
                case "VARCHAR":
                    retorno += $"[{campo.DAO.Nome}] = '{valor}'";
                    break;
                case "DATETIME":
                    retorno += $"[{campo.DAO.Nome}] = '{MontaStringDateTimeFromDateTime(DateTime.Parse(valor))}'";
                    break;
                default:
                    retorno += $"[{campo.DAO.Nome}] = '{valor}'";
                    break;
            }

            return retorno;
        }

        /// <summary>
        /// Método que monta o campo Where
        /// </summary>
        /// <param name="campo"></param>
        /// <param name="valor"></param>
        /// <returns></returns>
        private static string MontaCampoInsert(Model.MD_Campos campo, string valor)
        {
            string retorno = string.Empty;

            switch (campo.DAO.TipoCampo.Nome)
            {
                case "BIGINT":
                    retorno += $"{valor}";
                    break;
                case "INT":
                    retorno += $"{valor}";
                    break;
                case "TINYINT":
                    retorno += $"{valor}";
                    break;
                case "SMALLINT":
                    retorno += $"{valor}";
                    break;
                case "DECIMAL":
                    retorno += $"{valor.ToString().Replace(",", ".")}";
                    break;
                case "REAL":
                    retorno += $"{valor.ToString().Replace(",", ".")}";
                    break;
                case "NVARCHAR":
                    retorno += $"'{valor}'";
                    break;
                case "NTEXT":
                    retorno += $"'{valor}'";
                    break;
                case "TEXT":
                    retorno += $"'{valor}'";
                    break;
                case "VARCHAR":
                    retorno += $"'{valor}'";
                    break;
                case "DATETIME":
                    retorno += $"'{MontaStringDateTimeFromDateTime(DateTime.Parse(valor))}'";
                    break;
                default:
                    retorno += $"{campo.DAO.Nome} = '{valor}'";
                    break;
            }

            return retorno;
        }

        /// <summary>
        /// Metodo que padroniza a string datetime
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private static string MontaStringDateTimeFromDateTime(DateTime data)
        {
            return data.Year + "-" + data.Month + "-" + data.Day + " " + data.Hour + ":" + data.Minute + ":" + data.Second;
        }
    }
}
