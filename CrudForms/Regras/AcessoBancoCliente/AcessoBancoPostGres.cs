using Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Regras.AcessoBancoCliente
{
    public class AcessoBancoPostGres : AcessoBanco
    {
        private string schema;

        public AcessoBancoPostGres()
        {
            schema = Util.Global.connectionName;
        }

        /// <summary>
        /// Método que monta o comando update
        /// </summary>
        /// <param name="tabela"></param>
        /// <param name="campos"></param>
        /// <returns></returns>
        protected override string MontaComandoUpdate(string tabela, List<Model.MD_Campos> campos, AcessoBanco valoresAnteriores, out string mensagem)
        {
            string retorno = string.Empty;
            mensagem = string.Empty;

            retorno = $"update {schema}.{tabela} set ";
            List<Model.MD_Campos> camposPk = new List<MD_Campos>();
            List<int> listaCampos = new List<int>();
            bool temAlteracao = false;

            for (int i = 0; i < campos.Count; i++)
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

                retorno += MontaCampoUpdate(campos[i], valores[i]) + ", ";
                temAlteracao = true;
            }

            if (camposPk.Count == 0)
            {
                mensagem = "Não há chave primary key!";
                retorno = string.Empty;
            }
            else
            {
                retorno += "WHERE ";
                for (int i = 0; i < camposPk.Count; i++)
                {
                    if (i != 0)
                    {
                        retorno += " AND ";
                    }
                    retorno += MontaCampoWhereUpdateDelete(camposPk[i], valores[listaCampos[i]]);
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
        protected override string MontaComandoInsert(string tabela, List<Model.MD_Campos> campos, out string mensagem)
        {
            string retorno = string.Empty;
            mensagem = string.Empty;

            retorno = $"insert into {schema}.{tabela} (";
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
        protected override string MontaComandoDelete(string tabela, List<Model.MD_Campos> campos, out string mensagem)
        {
            string retorno = string.Empty;
            mensagem = string.Empty;

            retorno = $"DELETE FROM {schema}.{tabela} WHERE ";
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
        /// Method that creates the command for select in table
        /// </summary>
        /// <returns>Command SQL</returns>
        protected override string CreateCommandSQLTable(MD_Tabela tabela, List<MD_Campos> campos, Filtro filtro, bool limite = true)
        {
            string command = $" SELECT ";
            string fields = string.Empty;
            int qt = campos.Count, i = 1;

            foreach (Model.MD_Campos field in campos)
            {
                fields += $"{field.DAO.Nome}";
                if (i < qt)
                    fields += ", ";
                i++;
            }
            command += fields + $" FROM {schema}.{tabela.DAO.Nome}";
            command += MontaWhereSelect(filtro, campos);

            if (filtro.Order != null)
            {
                if (filtro.Order.CampoOrdenacao != null)
                {
                    command += $" ORDER BY {(campos.IndexOf(campos.Where(campo => campo.DAO.Nome == filtro.Order.CampoOrdenacao.DAO.Nome).FirstOrDefault()) + 1)} {(filtro.Order.Asc ? "ASC" : "DESC")}";
                }
            }

            return command + ( limite ? $" limit {Model.Parametros.QuantidadeLinhasTabelas.DAO.Valor}" : "");
        }

        /// <summary>
        /// Método que monta o where a partir do filtro
        /// </summary>
        /// <param name="filtro"></param>
        /// <returns></returns>
        protected override string MontaWhereSelect(Model.Filtro filtro, List<MD_Campos> campos)
        {
            string retorno = string.Empty;

            retorno = " WHERE 1 = 1";

            for (int i = 0; i < filtro.valores.Count; i++)
            {
                if (string.IsNullOrEmpty(filtro.valores[i].Replace(";", "")))
                    continue;

                retorno += " AND (" + MontaCampoWhereSelect(campos[i], filtro.valores[i]) + ")";
            }

            return retorno;
        }

        /// <summary>
        /// Método que monta o campo Where
        /// </summary>
        /// <param name="campo"></param>
        /// <param name="valor"></param>
        /// <returns></returns>
        protected override string MontaCampoWhereSelect(Model.MD_Campos campo, string valor)
        {
            string retorno = string.Empty;

            if (valor.Contains(";"))
            {
                bool first = true;

                switch (campo.DAO.TipoCampo.Nome)
                {
                    case "BIGINT":
                        retorno += $"{campo.DAO.Nome} IN ({string.Join(",", valor.Split(';').ToList())})";
                        break;
                    case "INT":
                        retorno += $"{campo.DAO.Nome} IN ({string.Join(",", valor.Split(';').ToList())})";
                        break;
                    case "TINYINT":
                        retorno += $"{campo.DAO.Nome} IN ({string.Join(",", valor.Split(';').ToList())})";
                        break;
                    case "DECIMAL":
                        retorno += $"{campo.DAO.Nome} IN ({string.Join(",", valor.Split(';').ToList()).Replace(",", ".")})";
                        break;
                    case "NUMERIC":
                        retorno += $"{campo.DAO.Nome} IN ({string.Join(",", valor.Split(';').ToList()).Replace(",", ".")})";
                        break;
                    case "CHARACTER VARYING":
                        first = true;
                        valor.Split(';').ToList().ForEach(item =>
                        {
                            if (first) first = false;
                            else retorno += " OR ";

                            retorno += $"UPPER({campo.DAO.Nome}) like '%{item.ToUpper()}%'";
                        });
                        break;
                    case "CHARACTER":
                        first = true;
                        valor.Split(';').ToList().ForEach(item =>
                        {
                            if (!string.IsNullOrEmpty(item))
                            {
                                if (first) first = false;
                                else retorno += " OR ";

                                retorno += $"UPPER({campo.DAO.Nome}) like '%{item.ToUpper()}%'";
                            }
                        });
                        break;
                    case "USER-DEFINED":
                        first = true;
                        valor.Split(';').ToList().ForEach(item =>
                        {
                            if (!string.IsNullOrEmpty(item))
                            {
                                if (first) first = false;
                                else retorno += " OR ";

                                retorno += $"UPPER({campo.DAO.Nome}) like '%{item.ToUpper()}%'";
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

                                retorno += $"UPPER({campo.DAO.Nome}) like '%{item.ToUpper()}%'";
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

                                retorno += $"UPPER({campo.DAO.Nome}) like '%{item.ToUpper()}%'";
                            }
                        });
                        break;
                    default:
                        first = true;
                        valor.Split(';').ToList().ForEach(item =>
                        {
                            if (!string.IsNullOrEmpty(item))
                            {
                                if (first) first = false;
                                else retorno += " OR ";

                                retorno += $"UPPER({campo.DAO.Nome}) like '%{item.ToUpper()}%'";
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
                        retorno += $"{campo.DAO.Nome} = {valor}";
                        break;
                    case "INTEGER":
                        retorno += $"{campo.DAO.Nome} = {valor}";
                        break;
                    case "TINYINT":
                        retorno += $"{campo.DAO.Nome} = {valor}";
                        break;
                    case "BOOLEAN":
                        retorno += $"{campo.DAO.Nome} = {valor}";
                        break;
                    case "DECIMAL":
                        retorno += $"{campo.DAO.Nome} = {valor.ToString().Replace(",", ".")}";
                        break;
                    case "NUMERIC":
                        retorno += $"{campo.DAO.Nome} = {valor.ToString().Replace(",", ".")}";
                        break;
                    case "CHARACTER VARYING":
                        retorno += $"UPPER({campo.DAO.Nome}) like '%{valor.ToUpper()}%'";
                        break;
                    case "CHARACTER":
                        retorno += $"UPPER({campo.DAO.Nome}) like '%{valor.ToUpper()}%'";
                        break;
                    case "USER-DEFINED":
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
                    case "DATE":
                        retorno += $"{campo.DAO.Nome} = '{MontaStringDateTimeFromDateTime(DateTime.Parse(valor))}'";
                        break;
                    case "TIMESTAMP WITH TIME ZONE":
                        retorno += $"{campo.DAO.Nome} = '{MontaStringDateTimeFromDateTime(DateTime.Parse(valor))}'";
                        break;
                    case "TIMESTAMP WITHOUT TIME ZONE":
                        retorno += $"{campo.DAO.Nome} = '{MontaStringDateTimeFromDateTime(DateTime.Parse(valor))}'";
                        break;
                    default:
                        retorno += $"{campo.DAO.Nome} = '{valor}'";
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
        protected override string MontaCampoWhereUpdateDelete(Model.MD_Campos campo, string valor)
        {
            string retorno = string.Empty;

            switch (campo.DAO.TipoCampo.Nome)
            {
                case "BIGINT":
                    retorno += $"{campo.DAO.Nome} = {valor}";
                    break;
                case "INTEGER":
                    retorno += $"{campo.DAO.Nome} = {valor}";
                    break;
                case "TINYINT":
                    retorno += $"{campo.DAO.Nome} = {valor}";
                    break;
                case "BOOLEAN":
                    retorno += $"{campo.DAO.Nome} = {valor}";
                    break;
                case "DECIMAL":
                    retorno += $"{campo.DAO.Nome} = {valor.ToString().Replace(",", ".")}";
                    break;
                case "NUMERIC":
                    retorno += $"{campo.DAO.Nome} = {valor.ToString().Replace(",", ".")}";
                    break;
                case "CHARACTER VARYING":
                    retorno += $"{campo.DAO.Nome} = '{valor}'";
                    break;
                case "CHARACTER":
                    retorno += $"{campo.DAO.Nome} = '{valor}'";
                    break;
                case "USER-DEFINED":
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
                case "DATE":
                    retorno += $"{campo.DAO.Nome} = '{MontaStringDateTimeFromDateTime(DateTime.Parse(valor))}'";
                    break;
                case "TIMESTAMP WITH TIME ZONE":
                    retorno += $"{campo.DAO.Nome} = '{MontaStringDateTimeFromDateTime(DateTime.Parse(valor))}'";
                    break;
                case "TIMESTAMP WITHOUT TIME ZONE":
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
        protected override string MontaCampoUpdate(Model.MD_Campos campo, string valor)
        {
            string retorno = string.Empty;

            switch (campo.DAO.TipoCampo.Nome)
            {
                case "BIGINT":
                    retorno += $"{campo.DAO.Nome} = {valor}";
                    break;
                case "INTEGER":
                    retorno += $"{campo.DAO.Nome} = {valor}";
                    break;
                case "TINYINT":
                    retorno += $"{campo.DAO.Nome} = {valor}";
                    break;
                case "BOOLEAN":
                    retorno += $"{campo.DAO.Nome} = {valor}";
                    break;
                case "DECIMAL":
                    retorno += $"{campo.DAO.Nome} = {valor.ToString().Replace(",", ".")}";
                    break;
                case "NUMERIC":
                    retorno += $"{campo.DAO.Nome} = {valor.ToString().Replace(",", ".")}";
                    break;
                case "CHARACTER VARYING":
                    retorno += $"{campo.DAO.Nome} = '{valor}'";
                    break;
                case "CHARACTER":
                    retorno += $"{campo.DAO.Nome} = '{valor}'";
                    break;
                case "USER-DEFINED":
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
                case "DATE":
                    retorno += $"{campo.DAO.Nome} = '{MontaStringDateTimeFromDateTime(DateTime.Parse(valor))}'";
                    break;
                case "TIMESTAMP WITH TIME ZONE":
                    retorno += $"{campo.DAO.Nome} = '{MontaStringDateTimeFromDateTime(DateTime.Parse(valor))}'";
                    break;
                case "TIMESTAMP WITHOUT TIME ZONE":
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
        protected override string MontaCampoInsert(Model.MD_Campos campo, string valor)
        {
            string retorno = string.Empty;

            switch (campo.DAO.TipoCampo.Nome)
            {
                case "BIGINT":
                    retorno += $"{valor}";
                    break;
                case "INTEGER":
                    retorno += $"{valor}";
                    break;
                case "TINYINT":
                    retorno += $"{valor}";
                    break;
                case "BOOLEAN":
                    retorno += $"{valor}";
                    break;
                case "DECIMAL":
                    retorno += $"{valor.ToString().Replace(",", ".")}";
                    break;
                case "NUMERIC":
                    retorno += $"{valor.ToString().Replace(",", ".")}";
                    break;
                case "CHARACTER VARYING":
                    retorno += $"'{valor}'";
                    break;
                case "CHARACTER":
                    retorno += $"'{valor}'";
                    break;
                case "USER-DEFINED":
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
                case "DATE":
                    retorno += $"'{MontaStringDateTimeFromDateTime(DateTime.Parse(valor))}'";
                    break;
                case "TIMESTAMP WITH TIME ZONE":
                    retorno += $"'{MontaStringDateTimeFromDateTime(DateTime.Parse(valor))}'";
                    break;
                case "TIMESTAMP WITHOUT TIME ZONE":
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
        protected override string MontaStringDateTimeFromDateTime(DateTime data)
        {
            return data.Year + "-" + data.Month + "-" + data.Day + " " + data.Hour + ":" + data.Minute + ":" + data.Second;
        }
    }
}
