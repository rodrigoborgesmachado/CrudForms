using Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Util
{
    public class DocumentSQSLite
    {
        /// <summary>
        /// Método que faz a importação das tabelas do banco de dados
        /// </summary>
        /// <returns></returns>
        public static bool Importar()
        {
            try
            {
                Visao.BarraDeCarregamento barra = new Visao.BarraDeCarregamento(Total(), "Importando dados");
                barra.Show();

                List<DAO.MDN_Table> tabelas = GerarTabelas(barra);

                GerarColunas(barra, tabelas);

                GerarRelacionamentos(barra, tabelas);

                barra.Dispose();
                barra = null;
            }
            catch (Exception e)
            {
                Util.CL_Files.WriteOnTheLog("Error: " + e.Message, Global.TipoLog.SIMPLES);
                return false;
            }

            return true;
        }

        /// <summary>
        /// Método que retorna o total de processamento
        /// </summary>
        /// <returns>total</returns>
        private static int Total()
        {
            return TotalCamposTabelasRelacoes();
        }

        /// <summary>
        /// Método que retorna o total
        /// </summary>
        /// <returns>o total a ser processado</returns>
        public static int TotalCamposTabelasRelacoes()
        {
            int retorno = 0;

            string sentenca = "SELECT 10000 AS TOTAL";

            DbDataReader reader = DataBase.Connection.Select(sentenca);

            if (reader.Read())
            {
                retorno = int.Parse(reader["total"].ToString());
            }
            reader.Close();

            return retorno;
        }

        /// <summary>
        /// método que salva no arquivo
        /// </summary>
        /// <param name="tabelas"></param>
        private static void PreencheArquivoCampos(List<Campo> campo)
        {
            if (File.Exists(Global.app_exportacao_campos_file))
            {
                File.Delete(Global.app_exportacao_campos_file);
            }

            string json = JsonConvert.SerializeObject(campo);
            File.WriteAllLines(Global.app_exportacao_campos_file, json.Split(Environment.NewLine.ToCharArray()));
        }

        /// <summary>
        /// Método que gera as colunas
        /// </summary>
        private static void GerarColunas(Visao.BarraDeCarregamento barra, List<DAO.MDN_Table> tabelas)
        {
            List<Campo> campos = new List<Campo>();

            RetornaDetalhesCampos(barra, tabelas, ref campos);

            PreencheArquivoCampos(campos);
        }

        /// <summary>
        /// Método que retorna uma lista de tabelas e suas descrições
        /// </summary>
        /// <returns></returns>
        public static void RetornaDetalhesCampos(Visao.BarraDeCarregamento barra, List<DAO.MDN_Table> tabelas, ref List<Model.Campo> campos)
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
        /// Método que gera as tabelas
        /// </summary>
        private static List<DAO.MDN_Table> GerarTabelas(Visao.BarraDeCarregamento barra)
        {
            List<DAO.MDN_Table> tabelas = new List<DAO.MDN_Table>();

            tabelas = RetornaDetalhesTabelas(barra);

            GeraArquivotabela(tabelas);

            return tabelas;
        }

        /// <summary>
        /// Método que retorna uma lista de tabelas e suas descrições
        /// </summary>
        /// <returns></returns>
        public static List<DAO.MDN_Table> RetornaDetalhesTabelas(Visao.BarraDeCarregamento barra)
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
        /// Método que armazena nos arquivos as tabelas
        /// </summary>
        /// <param name="tables"></param>
        private static void GeraArquivotabela(List<DAO.MDN_Table> tables)
        {
            List<Tabela> tabelas = new List<Tabela>();

            foreach (DAO.MDN_Table table in tables)
            {
                Tabela t = new Tabela();
                t.nome = table.Table_Name;
                tabelas.Add(t);
            }

            PreencheArquivo(tabelas);
        }

        /// <summary>
        /// método que salva no arquivo
        /// </summary>
        /// <param name="tabelas"></param>
        private static void PreencheArquivo(List<Tabela> tabelas)
        {
            if (File.Exists(Global.app_exportacao_tabela_file))
            {
                File.Delete(Global.app_exportacao_tabela_file);
            }

            string json = JsonConvert.SerializeObject(tabelas);
            File.WriteAllLines(Global.app_exportacao_tabela_file, json.Split(Environment.NewLine.ToCharArray()));
        }

        /// <summary>
        /// Método que gera os relacionamentos
        /// </summary>
        private static void GerarRelacionamentos(Visao.BarraDeCarregamento barra, List<DAO.MDN_Table>  tabelas)
        {
            List<Relacionamento> relacionamentos = new List<Relacionamento>();

            RetornaDetalhesRelacionamentos(barra, tabelas, ref relacionamentos);

            PreencheArquivoRelacionamentos(relacionamentos);

        }

        /// <summary>
        /// Métodos que retorna os relacionamentos
        /// </summary>
        /// <param name="tabelas"></param>
        /// <param name="relacionamentos"></param>
        public static void RetornaDetalhesRelacionamentos(Visao.BarraDeCarregamento barra, List<DAO.MDN_Table> tabelas, ref List<Model.Relacionamento> relacionamentos)
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

        /// <summary>
        /// método que salva no arquivo
        /// </summary>
        /// <param name="tabelas"></param>
        private static void PreencheArquivoRelacionamentos(List<Relacionamento> relacionamentos)
        {
            if (File.Exists(Global.app_exportacao_relacionamentos_file))
            {
                File.Delete(Global.app_exportacao_relacionamentos_file);
            }

            string json = JsonConvert.SerializeObject(relacionamentos);
            File.WriteAllLines(Global.app_exportacao_relacionamentos_file, json.Split(Environment.NewLine.ToCharArray()));
        }
    }
}
