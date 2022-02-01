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

            string connection = new DAO.MD_Parametros(Util.Global.parametro_connectionStrings).Valor;

            DataBase.Connection.CloseConnection();
            if (!DataBase.Connection.OpenConection(connection, Util.Enumerator.BancoDados.SQL_SERVER))
            {
                mensagem = "Não foi possível conectar";
                retorno = false;
            }
            else
            {
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

            string connection = new DAO.MD_Parametros(Util.Global.parametro_connectionStrings).Valor;

            DataBase.Connection.CloseConnection();
            if (!DataBase.Connection.OpenConection(connection, Util.Enumerator.BancoDados.SQL_SERVER))
            {
                mensagem = "Não foi possível conectar";
                retorno = false;
            }
            else
            {
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
                return false;
            }

            string connection = new DAO.MD_Parametros(Util.Global.parametro_connectionStrings).Valor;

            DataBase.Connection.CloseConnection();
            if(!DataBase.Connection.OpenConection(connection, Util.Enumerator.BancoDados.SQL_SERVER))
            {
                mensagem = "Não foi possível conectar";
                retorno = false;
            }
            else
            {
                retorno = DataBase.Connection.Update(update);
            }

            DataBase.Connection.CloseConnection();
            DataBase.Connection.OpenConection(Util.Global.app_base_file, Util.Enumerator.BancoDados.SQLite);

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

            retorno = $"update {tabela} set ";
            List<Model.MD_Campos> camposPk = new List<MD_Campos>();
            List<int> listaCampos = new List<int>();

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
                    retorno += Valores.MontaCampoWhere(camposPk[i], valores[listaCampos[i]]);
                }
            }

            retorno = retorno.Replace(", WHERE", " WHERE");

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
                    retorno += Valores.MontaCampoWhere(campos[i], valores[i]);
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
        public static List<Valores> BuscaLista(MD_Tabela tabela, List<MD_Campos> campos, Model.Filtro filtro)
        {
            List<Valores> valores = new List<Valores>();

            string sentenca = CreateCommandSQLTable(tabela, campos, filtro);

            string connection = new DAO.MD_Parametros(Util.Global.parametro_connectionStrings).Valor;

            DataBase.Connection.CloseConnection();
            DataBase.Connection.OpenConection(connection, Util.Enumerator.BancoDados.SQL_SERVER);

            DbDataReader reader = DataBase.Connection.Select(sentenca);

            List<string> columns = new List<string>();
            campos.ForEach(campo => 
            {
                columns.Add(campo.DAO.Nome);
            });
            
            while (reader.Read())
            {
                List<string> values = new List<string>();

                for(int i = 0; i< reader.FieldCount; i++)
                {
                    values.Add(reader[campos[i].DAO.Nome].ToString());
                }

                valores.Add(new Valores()
                {
                    campos = columns,
                    valores = values
                }) ;
            }
            reader.Close();

            DataBase.Connection.CloseConnection();
            DataBase.Connection.OpenConection(Util.Global.app_base_file, Util.Enumerator.BancoDados.SQLite);

            return valores;
        }

        /// <summary>
        /// Method that creates the command for select in table
        /// </summary>
        /// <returns>Command SQL</returns>
        private static string CreateCommandSQLTable(MD_Tabela tabela, List<MD_Campos> campos, Model.Filtro filtro)
        {
            string command = " SELECT TOP 500 ";
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
                if (string.IsNullOrEmpty(filtro.valores[i]))
                    continue;

                retorno += " AND " + MontaCampoWhere(campos[i], filtro.valores[i]);
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

            switch (campo.DAO.TipoCampo.Nome)
            {
                case "BIGINT":
                    retorno += $"{campo.DAO.Nome} = {valor}";
                    break;
                case "INT":
                    retorno += $"{campo.DAO.Nome} = {valor}";
                    break;
                case "TINYINT":
                    retorno += $"{campo.DAO.Nome} = {valor}";
                    break;
                case "SMALLINT":
                    retorno += $"{campo.DAO.Nome} = {valor}";
                    break;
                case "DECIMAL":
                    retorno += $"{campo.DAO.Nome} = {valor.ToString().Replace(",", ".")}";
                    break;
                case "REAL":
                    retorno += $"{campo.DAO.Nome} = {valor.ToString().Replace(",", ".")}";
                    break;
                case "NVARCHAR":
                    retorno += $"UPPER({campo.DAO.Nome}) like '%{valor.ToUpper()}%'";
                    break;
                case "NTEXT":
                    retorno += $"UPPER({campo.DAO.Nome}) like '%{valor.ToUpper()}%'";
                    break;
                case "TEXT":
                    retorno += $"UPPER({campo.DAO.Nome}) like '%{valor.ToUpper()}%'";
                    break;
                case "VARCHAR":
                    retorno += $"UPPER({campo.DAO.Nome}) like '%{valor.ToUpper()}%'";
                    break;
                case "DATETIME":
                    retorno += $"{campo.DAO.Nome} = '{MontaStringDateTimeFromDateTime(DateTime.Parse(valor))}'";
                    break;
                default:
                    retorno += $"{campo.DAO.Nome} = '{valor}'";
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
                    retorno += $"{campo.DAO.Nome} = {valor}";
                    break;
                case "INT":
                    retorno += $"{campo.DAO.Nome} = {valor}";
                    break;
                case "TINYINT":
                    retorno += $"{campo.DAO.Nome} = {valor}";
                    break;
                case "SMALLINT":
                    retorno += $"{campo.DAO.Nome} = {valor}";
                    break;
                case "DECIMAL":
                    retorno += $"{campo.DAO.Nome} = {valor.ToString().Replace(",", ".")}";
                    break;
                case "REAL":
                    retorno += $"{campo.DAO.Nome} = {valor.ToString().Replace(",", ".")}";
                    break;
                case "NVARCHAR":
                    retorno += $"{campo.DAO.Nome} = '{valor}'";
                    break;
                case "NTEXT":
                    retorno += $"{campo.DAO.Nome} = '{valor}'";
                    break;
                case "TEXT":
                    retorno += $"{campo.DAO.Nome} = '{valor}'";
                    break;
                case "VARCHAR":
                    retorno += $"{campo.DAO.Nome} = '{valor}'";
                    break;
                case "DATETIME":
                    retorno += $"{campo.DAO.Nome} = '{MontaStringDateTimeFromDateTime(DateTime.Parse(valor))}'";
                    break;
                default:
                    retorno += $"{campo.DAO.Nome} = '{valor}'";
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
