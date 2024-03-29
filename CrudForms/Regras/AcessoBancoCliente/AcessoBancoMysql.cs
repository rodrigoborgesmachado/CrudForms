﻿using Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Regras.AcessoBancoCliente
{
    public class AcessoBancoMysql : AcessoBanco
    {
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

            retorno = $"update `{tabela}` set ";
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

                retorno += MontaCampoUpdate(campos[i], valores[i]) + ", ";
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

            retorno = $"insert into `{tabela}` (";
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

            retorno = $"DELETE FROM `{tabela}` WHERE ";
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
        protected override string CreateCommandSQLTable(MD_Tabela tabela, List<MD_Campos> campos, Filtro filtro, int page, bool limite = true)
        {
            string command = $" SELECT ";
            string fields = string.Empty;
            int qt = campos.Count, i = 1;

            foreach (Model.MD_Campos field in campos)
            {
                fields += $"`{field.DAO.Nome}`";
                if (i < qt)
                    fields += ", ";
                i++;
            }
            command += fields + " FROM `" + tabela.DAO.Nome + "`";
            command += MontaWhereSelect(filtro, campos);

            if(filtro.Order != null)
            {
                if(filtro.Order.CampoOrdenacao != null)
                {
                    command += $" ORDER BY { (campos.IndexOf(campos.Where(campo => campo.DAO.Nome == filtro.Order.CampoOrdenacao.DAO.Nome).FirstOrDefault()) + 1)} {(filtro.Order.Asc ? "ASC" : "DESC")}";
                }
            }

            return command + (limite ? $" limit {Model.Parametros.QuantidadeLinhasTabelas.DAO.Valor} ofset {((page-1) * int.Parse(Model.Parametros.QuantidadeLinhasTabelas.DAO.Valor)).ToString()}" : "");
        }

        /// <summary>
        /// Method that creates the command for select in table
        /// </summary>
        /// <returns>Command SQL</returns>
        protected override string GetCommandQuantidadeTotal(MD_Tabela tabela, List<MD_Campos> campos, Filtro filtro)
        {
            string command = $" SELECT count(1)";
            string fields = string.Empty;
            command += fields + " FROM `" + tabela.DAO.Nome + "`";
            command += MontaWhereSelect(filtro, campos);

            return command;
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

            for(int i = 0; i < filtro.valores.Count; i++)
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

            if(valor.Contains(";"))
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
                    case "BOOL":
                        retorno += $"{campo.DAO.Nome} IN ({string.Join(",", valor.Split(';').ToList())})";
                        break;
                    case "BOOLEAN":
                        retorno += $"{campo.DAO.Nome} IN ({string.Join(",", valor.Split(';').ToList())})";
                        break;
                    case "TINYINT":
                        retorno += $"{campo.DAO.Nome} IN ({string.Join(",", valor.Split(';').ToList())})";
                        break;
                    case "SMALLINT":
                        retorno += $"{campo.DAO.Nome} IN ({string.Join(",", valor.Split(';').ToList())})";
                        break;
                    case "DECIMAL":
                        retorno += $"{campo.DAO.Nome} IN ({string.Join(",", valor.Split(';').ToList()).Replace(",", ".")})";
                        break;
                    case "DOUBLE":
                        retorno += $"{campo.DAO.Nome} IN ({string.Join(",", valor.Split(';').ToList()).Replace(",", ".")})";
                        break;
                    case "DOUBLE PRECISION":
                        retorno += $"{campo.DAO.Nome} IN ({string.Join(",", valor.Split(';').ToList()).Replace(",", ".")})";
                        break;
                    case "DEC":
                        retorno += $"{campo.DAO.Nome} IN ({string.Join(",", valor.Split(';').ToList()).Replace(",", ".")})";
                        break;
                    case "FLOAT":
                        retorno += $"{campo.DAO.Nome} IN ({string.Join(",", valor.Split(';').ToList()).Replace(",", ".")})";
                        break;
                    case "CHAR":
                        first = true;
                        valor.Split(';').ToList().ForEach(item =>
                        {
                            if (first) first = false;
                            else retorno += " OR ";

                            retorno += $"UPPER({campo.DAO.Nome}) like '%{item.ToUpper()}%'";
                        });
                        break;
                    case "VARCHAR":
                        first = true;
                        valor.Split(';').ToList().ForEach(item =>
                        {
                            if (first) first = false;
                            else retorno += " OR ";

                            retorno += $"UPPER({campo.DAO.Nome}) like '%{item.ToUpper()}%'";
                        });
                        break;
                    case "LONGTEXT":
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
                    case "INT":
                        retorno += $"{campo.DAO.Nome} = {valor}";
                        break;
                    case "BOOL":
                        retorno += $"{campo.DAO.Nome} = {valor}";
                        break;
                    case "BOOLEAN":
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
                    case "DOUBLE":
                        retorno += $"{campo.DAO.Nome} = {valor.ToString().Replace(",", ".")}";
                        break;
                    case "DOUBLE PRECISION":
                        retorno += $"{campo.DAO.Nome} = {valor.ToString().Replace(",", ".")}";
                        break;
                    case "DEC":
                        retorno += $"{campo.DAO.Nome} = {valor.ToString().Replace(",", ".")}";
                        break;
                    case "FLOAT":
                        retorno += $"{campo.DAO.Nome} = {valor.ToString().Replace(",", ".")}";
                        break;
                    case "CHAR":
                        retorno += $"UPPER({campo.DAO.Nome}) like '%{valor.ToUpper()}%'";
                        break;
                    case "LONGTEXT":
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
                    case "TIMESTAMP":
                        retorno += $"{campo.DAO.Nome} = '{MontaStringDateTimeFromDateTime(DateTime.Parse(valor))}'";
                        break;
                    case "TIME":
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
                case "INT":
                    retorno += $"{campo.DAO.Nome} = {valor}";
                    break;
                case "BOOL":
                    retorno += $"{campo.DAO.Nome} = {valor}";
                    break;
                case "BOOLEAN":
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
                case "DOUBLE":
                    retorno += $"{campo.DAO.Nome} = {valor.ToString().Replace(",", ".")}";
                    break;
                case "DOUBLE PRECISION":
                    retorno += $"{campo.DAO.Nome} = {valor.ToString().Replace(",", ".")}";
                    break;
                case "DEC":
                    retorno += $"{campo.DAO.Nome} = {valor.ToString().Replace(",", ".")}";
                    break;
                case "FLOAT":
                    retorno += $"{campo.DAO.Nome} = {valor.ToString().Replace(",", ".")}";
                    break;
                case "CHAR":
                    retorno += $"{campo.DAO.Nome} = '{valor}'";
                    break;
                case "VARCHAR":
                    retorno += $"{campo.DAO.Nome} = '{valor}'";
                    break;
                case "LONGTEXT":
                    retorno += $"{campo.DAO.Nome} = '{valor}'";
                    break;
                case "TEXT":
                    retorno += $"{campo.DAO.Nome} = '{valor}'";
                    break;
                case "DATETIME":
                    retorno += $"{campo.DAO.Nome} = '{MontaStringDateTimeFromDateTime(DateTime.Parse(valor))}'";
                    break;
                case "DATE":
                    retorno += $"{campo.DAO.Nome} = '{MontaStringDateTimeFromDateTime(DateTime.Parse(valor))}'";
                    break;
                case "TIMESTAMP":
                    retorno += $"{campo.DAO.Nome} = '{MontaStringDateTimeFromDateTime(DateTime.Parse(valor))}'";
                    break;
                case "TIME":
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
                case "INT":
                    retorno += $"{campo.DAO.Nome} = {valor}";
                    break;
                case "BOOL":
                    retorno += $"{campo.DAO.Nome} = {valor}";
                    break;
                case "BOOLEAN":
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
                case "DOUBLE":
                    retorno += $"{campo.DAO.Nome} = {valor.ToString().Replace(",", ".")}";
                    break;
                case "DOUBLE PRECISION":
                    retorno += $"{campo.DAO.Nome} = {valor.ToString().Replace(",", ".")}";
                    break;
                case "DEC":
                    retorno += $"{campo.DAO.Nome} = {valor.ToString().Replace(",", ".")}";
                    break;
                case "FLOAT":
                    retorno += $"{campo.DAO.Nome} = {valor.ToString().Replace(",", ".")}";
                    break;
                case "CHAR":
                    retorno += $"{campo.DAO.Nome} = '{valor}'";
                    break;
                case "VARCHAR":
                    retorno += $"{campo.DAO.Nome} = '{valor}'";
                    break;
                case "LONGTEXT":
                    retorno += $"{campo.DAO.Nome} = '{valor}'";
                    break;
                case "TEXT":
                    retorno += $"{campo.DAO.Nome} = '{valor}'";
                    break;
                case "DATETIME":
                    retorno += $"{campo.DAO.Nome} = '{MontaStringDateTimeFromDateTime(DateTime.Parse(valor))}'";
                    break;
                case "DATE":
                    retorno += $"{campo.DAO.Nome} = '{MontaStringDateTimeFromDateTime(DateTime.Parse(valor))}'";
                    break;
                case "TIMESTAMP":
                    retorno += $"{campo.DAO.Nome} = '{MontaStringDateTimeFromDateTime(DateTime.Parse(valor))}'";
                    break;
                case "TIME":
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
                case "INT":
                    retorno += $"{valor}";
                    break;
                case "BOOL":
                    retorno += $"{valor}";
                    break;
                case "BOOLEAN":
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
                case "DOUBLE":
                    retorno += $"{valor.ToString().Replace(",", ".")}";
                    break;
                case "DOUBLE PRECISION":
                    retorno += $"{valor.ToString().Replace(",", ".")}";
                    break;
                case "DEC":
                    retorno += $"{valor.ToString().Replace(",", ".")}";
                    break;
                case "FLOAT":
                    retorno += $"{valor.ToString().Replace(",", ".")}";
                    break;
                case "CHAR":
                    retorno += $"'{valor}'";
                    break;
                case "VARCHAR":
                    retorno += $"'{valor}'";
                    break;
                case "LONGTEXT":
                    retorno += $"'{valor}'";
                    break;
                case "TEXT":
                    retorno += $"'{valor}'";
                    break;
                case "DATETIME":
                    retorno += $"'{MontaStringDateTimeFromDateTime(DateTime.Parse(valor))}'";
                    break;
                case "DATE":
                    retorno += $"'{MontaStringDateTimeFromDateTime(DateTime.Parse(valor))}'";
                    break;
                case "TIMESTAMP":
                    retorno += $"'{MontaStringDateTimeFromDateTime(DateTime.Parse(valor))}'";
                    break;
                case "TIME":
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
