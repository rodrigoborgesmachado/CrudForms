using Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Regras
{
    public class ImportarPlanilhaCsv
    {
        public bool CriarTabelaPlanilha(string nomeTabela, FileInfo file, out string mensagemErro)
        {
            bool retorno = true;
            mensagemErro = string.Empty;
            nomeTabela = nomeTabela.Trim();
            try
            {
                string connection = Parametros.ConexaoBanco.DAO.Valor;

                DataBase.Connection.CloseConnection();
                if (!DataBase.Connection.OpenConection(connection, Util.Global.BancoDados))
                {
                    mensagemErro = "Erro ao conectar com o banco";
                    retorno = false;
                }
                else
                {
                    string[] linhas = File.ReadAllLines(file.FullName);
                    if(linhas.Length == 0)
                    {
                        mensagemErro = "Arquivo vazio";
                    }
                    else
                    {
                        retorno = ProcessaNoBanco(nomeTabela, linhas, out mensagemErro, out var tabela, out var colunas);

                        DataBase.Connection.CloseConnection();
                        DataBase.Connection.OpenConection(Util.Global.app_base_file, Util.Enumerator.BancoDados.SQLite);

                        if (!Model.MD_Tabela.RetornaTodasTabelas(0).Exists(t => t.DAO.Nome.ToUpper().Equals(tabela.nome.ToUpper())))
                        {
                            DAO.MD_Tabela table = new DAO.MD_Tabela(DataBase.Connection.GetIncrement(new DAO.MD_Tabela().table.Table_Name), 0, false);
                            table.Nome = tabela.nome;
                            table.Insert();

                            foreach (var col in colunas)
                            {
                                DAO.MD_Campos campo = new DAO.MD_Campos(DataBase.Connection.GetIncrement(new DAO.MD_Campos().table.Table_Name), table);
                                campo.Check = string.Empty;
                                campo.Comentario = string.Empty;
                                campo.Default = col.ValueDefault;
                                campo.Dominio = string.Empty;
                                campo.Nome = col.Name_Field;
                                campo.NotNull = col.NotNull;
                                campo.Precisao = col.Precision;
                                campo.PrimaryKey = col.PrimaryKey;
                                campo.Tabela = table;
                                campo.Tamanho = col.Size;
                                campo.TipoCampo = Model.MD_TipoCampo.RetornaTipoCampo(col.Type).DAO;
                                campo.Unique = col.Unique;
                                campo.Insert();
                            }
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                Util.CL_Files.LogException(ex);
                retorno = false;
            }
            finally
            {
                DataBase.Connection.CloseConnection();
                DataBase.Connection.OpenConection(Util.Global.app_base_file, Util.Enumerator.BancoDados.SQLite);
            }
            return retorno;
        }

        /// <summary>
        /// Método que cria o objeto no banco
        /// </summary>
        /// <param name="linhas"></param>
        /// <param name="mensagemErro"></param>
        /// <returns></returns>
        private bool ProcessaNoBanco(string tableName, string[] linhas, out string mensagemErro, out Tabela tabelaRetorno, out List<Campo> colunasRetorno)
        {
            mensagemErro = string.Empty;
            bool retorno = true;
            colunasRetorno = new List<Campo>();

            List<string> colunas = linhas[0].Split(';').ToList().Where(p => !string.IsNullOrWhiteSpace(p)).ToList().Select(i => i.Replace(" ", "_").Replace("/", "_")).ToList();
            linhas = linhas.Skip(1).ToArray();

            List<Campo> temp = new List<Campo>();
            DAO.MDN_Table tabela = new DAO.MDN_Table(tableName.Replace(" ", "_"));
            colunas.ForEach(coluna => 
            {
                tabela.Fields_Table.Add(new DAO.MDN_Campo(coluna, false, Util.Enumerator.DataType.STRING, null, false, false, 4000, 0));
                var camp = new Campo();
                camp.Size = 4000;
                camp.Unique = false;
                camp.Precision = 0;
                camp.PrimaryKey = false;
                camp.NotNull = false;
                camp.Name_Field = coluna;
                camp.Type = "VARCHAR";
                camp.ValueDefault = "null";

                temp.Add(camp);
            });
            colunasRetorno.AddRange(temp);

            tabela.CreateTable(false);
            tabela.VerificaColunas();

            tabelaRetorno = new Tabela();
            tabelaRetorno.nome = tabela.Table_Name;

            List<string> comandos = new List<string>();
            linhas.ToList().ForEach(linha =>
            {
                if (linha.Split(';').Length == colunas.Count)
                {
                    comandos.Add($"insert into {tabela.Table_Name} ({string.Join(", ", colunas)}) values ('{string.Join("', '", linha.Replace("'", "").Split(';').ToList())}');");
                }
                else
                {
                    string t = $"insert into {tabela.Table_Name} ({string.Join(", ", colunas)}) values ('{string.Join("', '", linha.Replace("'", "").Split(';').ToList())}'";
                    int d = colunas.Count - linha.Split(';').Length;
                    if(d > 0)
                    {
                        while (d-- > 0) t += ",''";
                        t += ");";
                        comandos.Add(t);
                    }
                }
            });

            string erros = string.Empty;
            Visao.BarraDeCarregamento barra = new Visao.BarraDeCarregamento(comandos.Count, "Inserindo dados");
            barra.Show();
            int quantidade = 0;
            string comando = string.Empty;
            comandos.ForEach(c => 
            {
                if (!string.IsNullOrEmpty(c))
                {
                    if (quantidade == 1000)
                    {
                        if (!DataBase.Connection.Insert(comando))
                        {
                            erros += comando;
                        }
                        comando = string.Empty;
                        quantidade = 0;
                    }
                    else
                    {
                        comando += c;
                        quantidade++;
                    }

                }
                barra.AvancaBarra(1);
            }
            );
            if (quantidade > 0)
            {
                if (!DataBase.Connection.Insert(comando))
                {
                    erros += comando;
                }
                comando = string.Empty;
                quantidade = 0;
            }
            barra.Dispose();

            if (!string.IsNullOrWhiteSpace(erros))
            {
                mensagemErro = $"Houve erros{Environment.NewLine}{erros}";
                retorno = false;
            }
            return retorno;
        }
    }
}
