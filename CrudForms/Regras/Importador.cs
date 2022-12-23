using Visao;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Util;
using System.Data.Common;

namespace Regras
{
    public class Importador
    {
        /// <summary>
        /// Método que faz a importação dos arquivos JSON
        /// </summary>
        /// <param name="projeto">Código do projeto para associar os dados</param>
        /// <returns></returns>
        public InformacoesSaidaImportador Importar(int projeto)
        {
            Util.CL_Files.WriteOnTheLog("Importador.Importar()", Util.Global.TipoLog.DETALHADO);
            InformacoesSaidaImportador importador = new InformacoesSaidaImportador();
            importador.Importado = false;

            try
            {
                ApagaArquivosExportacao();
                string conexao = Model.Parametros.ConexaoBanco.DAO.Valor;

                if (!string.IsNullOrEmpty(conexao))
                {
                    DataBase.Connection.CloseConnection();
                    if (DataBase.Connection.OpenConection(conexao, Global.BancoDados))
                    {
                        Util.Document document = null;

                        if (Global.BancoDados == Enumerator.BancoDados.SQL_SERVER)
                        {
                            document = new Util.DocumentSQLServer();
                        }
                        else if (Global.BancoDados == Enumerator.BancoDados.SQLite)
                        {
                            document = new Util.DocumentSQSLite();
                        }
                        else if (Global.BancoDados == Enumerator.BancoDados.POSTGRESQL)
                        {
                            document = new Util.DocumentPostGreSql();
                        }
                        else if (Global.BancoDados == Enumerator.BancoDados.MYSQL)
                        {
                            document = new Util.DocumentMySql();
                        }

                        importador.Importado = document.Importar();

                        DataBase.Connection.CloseConnection();
                        DataBase.Connection.OpenConection(Util.Global.app_base_file, Enumerator.BancoDados.SQLite);
                        if (importador.Importado)
                        {
                            importador = ImportarArquivos(projeto);
                            importador.Importado = true;
                        }
                    }
                    else
                    {
                        DataBase.Connection.CloseConnection();
                        DataBase.Connection.OpenConection(Util.Global.app_base_file, Enumerator.BancoDados.SQLite);
                    }
                }
            }
            catch (Exception e)
            {
                Util.CL_Files.LogException(e);
                importador.Importado = false;
            }

            return importador;
        }

        /// <summary>
        /// Método que apaga os arquivos json old
        /// </summary>
        private void ApagaArquivosExportacao()
        {
            Util.CL_Files.WriteOnTheLog("Importador.ApagaArquivosExportacao()", Util.Global.TipoLog.DETALHADO);

            foreach (string file in Directory.GetFiles(Util.Global.app_exportacao_directory))
            {
                File.Delete(file);
            }

            foreach(string file in Directory.GetFiles(Util.Global.app_DER_directory))
            {
                File.Delete(file);
            }
        }

        /// <summary>
        /// Método que faz a importação dos arquivos para o banco de dados
        /// </summary>
        private InformacoesSaidaImportador ImportarArquivos(int projeto)
        {
            Util.CL_Files.WriteOnTheLog("Importador.ImportarArquivos()", Util.Global.TipoLog.DETALHADO);

            List<Model.Tabela> tabelas = new List<Model.Tabela>();
            PreencheLista(ref tabelas);

            List<Model.Campo> campos = new List<Model.Campo>();
            PreencheLista(ref campos);

            List<Model.Relacionamento> relacionamentos = new List<Model.Relacionamento>();
            PreencheLista(ref relacionamentos);

            InformacoesSaidaImportador importador = TratarImportacao(projeto, ref tabelas, ref campos, ref relacionamentos);

            CopiaArquivos();

            return importador;
        }

        /// <summary>
        /// Método que preenche a lista com as tabelas do arquivo JSON
        /// </summary>
        /// <param name="tabelas">Lista de tabelas a ser preenchida</param>
        private void PreencheLista(ref List<Model.Tabela> tabelas)
        {
            Util.CL_Files.WriteOnTheLog("Importador.PreencheLista()", Util.Global.TipoLog.DETALHADO);

            if (!File.Exists(Global.app_exportacao_tabela_file))
                return;

            string json = string.Empty;
            List<string> linhas = File.ReadAllLines(Global.app_exportacao_tabela_file).ToList();
            foreach (string linha in linhas)
            {
                json += linha;
            }
            linhas.Clear();

            tabelas = JsonConvert.DeserializeObject<List<Model.Tabela>>(json);
        }

        /// <summary>
        /// Método que preenche a lista com os campos do arquivo JSON
        /// </summary>
        /// <param name="campos">Lista de campos a ser preenchida</param>
        private void PreencheLista(ref List<Model.Campo> campos)
        {
            Util.CL_Files.WriteOnTheLog("Importador.PreencheLista()", Util.Global.TipoLog.DETALHADO);

            if (!File.Exists(Global.app_exportacao_campos_file))
                return;

            string json = string.Empty;
            List<string> linhas = File.ReadAllLines(Global.app_exportacao_campos_file).ToList();
            foreach (string linha in linhas)
            {
                json += linha;
            }

            campos = JsonConvert.DeserializeObject<List<Model.Campo>>(json);
        }

        /// <summary>
        /// Método que preenche a lista com os relacionamentos do arquivo JSON
        /// </summary>
        /// <param name="relacionamentos">Lista de relacionamentos a ser preenchida</param>
        private void PreencheLista(ref List<Model.Relacionamento> relacionamentos)
        {
            Util.CL_Files.WriteOnTheLog("Importador.PreencheLista()", Util.Global.TipoLog.DETALHADO);

            if (!File.Exists(Global.app_exportacao_relacionamentos_file))
                return;

            string json = string.Empty;
            List<string> linhas = File.ReadAllLines(Global.app_exportacao_relacionamentos_file).ToList();
            foreach (string linha in linhas)
            {
                json += linha;
            }

            relacionamentos = JsonConvert.DeserializeObject<List<Model.Relacionamento>>(json);
        }

        /// <summary>
        /// Método que trata as tabelas de importação e coloca nas tabelas definitivas vinculando com o projeto
        /// </summary>
        /// <param name="projeto">Código do projeto a se vincular as tabelas</param>
        private InformacoesSaidaImportador TratarImportacao(int codigo, ref List<Model.Tabela> tabelas, ref List<Model.Campo> campos, ref List<Model.Relacionamento> relacionamentos)
        {
            Util.CL_Files.WriteOnTheLog("Importador.TratarImportacao()", Util.Global.TipoLog.DETALHADO);

            ZeraDados();

            InformacoesSaidaImportador importador = new InformacoesSaidaImportador();

            BarraDeCarregamento barraCarregamento = new BarraDeCarregamento(tabelas.Count(), "Montando Tabelas");
            barraCarregamento.Show();

            importador.tabelasModel = TratarTabelas(ref tabelas, ref barraCarregamento);

            barraCarregamento.Hide();
            barraCarregamento.Dispose();
            barraCarregamento = null;

            barraCarregamento = new BarraDeCarregamento(campos.Count(), "Montando Colunas");
            barraCarregamento.Show();

            importador.camposModel = TratarColunas(importador.tabelasModel, ref campos, ref barraCarregamento);

            barraCarregamento.Hide();
            barraCarregamento.Dispose();
            barraCarregamento = null;

            barraCarregamento = new BarraDeCarregamento(relacionamentos.Count(), "Montando Relacionamentos");

            barraCarregamento.Show();
            importador.relacoesModel = TratarRelacionamento(importador.camposModel, importador.tabelasModel, ref relacionamentos, ref barraCarregamento);
            barraCarregamento.Hide();
            barraCarregamento.Dispose();


            barraCarregamento = new BarraDeCarregamento(importador.camposModel.Count() + importador.tabelasModel.Count() + importador.relacoesModel.Count(), "Inserindo informações");
            barraCarregamento.Show();

            importador.tabelasModel.ForEach(t => 
            {
                t.DAO.Insert();
                barraCarregamento.AvancaBarra(1);
            }
            );
            importador.camposModel.ForEach(c =>
            {
                c.DAO.Insert();
                barraCarregamento.AvancaBarra(1);
            }
            );
            importador.relacoesModel.ForEach(r =>
            {
                r.DAO.Insert();
                barraCarregamento.AvancaBarra(1);
            }
            );

            barraCarregamento.Hide();
            barraCarregamento.Dispose();
            barraCarregamento = null;

            return importador;
        }

        /// <summary>
        /// Método que trata as tabelas
        /// </summary>
        /// <param name="projeto">Projeto ao qual a tabela pertence</param>
        /// <param name="tabelas">Tabelas a serem importadas para o projeto</param>
        private List<Model.MD_Campos> TratarColunas(List<Model.MD_Tabela> tabelaModel, ref List<Model.Campo> campos, ref BarraDeCarregamento barra)
        {
            Util.CL_Files.WriteOnTheLog("Importador.TratarColunas()", Util.Global.TipoLog.DETALHADO);
            List<Model.MD_Campos> camposModel = new List<Model.MD_Campos>();

            int campoCodigo = DataBase.Connection.GetIncrement("CAMPOS");

            foreach (Model.Campo c in campos)
            {
                barra.AvancaBarra(1);

                Model.MD_Tabela tabela = tabelaModel.Where(t => t.DAO.Nome.ToUpper().Equals(c.Tabela.ToUpper())).FirstOrDefault();
                Model.MD_Campos campo = new Model.MD_Campos(campoCodigo, tabela.DAO.Codigo, 0, false);
                campo.DAO.Tabela = tabela.DAO;
                campo.DAO.Nome = c.Name_Field;
                campo.DAO.Default = c.ValueDefault;
                campo.DAO.NotNull = c.NotNull;
                campo.DAO.Precisao = c.Precision;
                campo.DAO.PrimaryKey = c.PrimaryKey;
                campo.DAO.Projeto = 0;
                campo.DAO.Tamanho = c.Size;
                campo.DAO.TipoCampo = Model.MD_TipoCampo.RetornaTipoCampo(c.Type).DAO;
                campo.DAO.Unique = c.Unique;

                if(!camposModel.Exists(item => item.DAO.Nome == campo.DAO.Nome && item.DAO.Tabela.Nome == campo.DAO.Tabela.Nome))
                {
                    camposModel.Add(campo);
                    campoCodigo++;
                }
            }

            DataBase.Connection.SetIncrement("CAMPOS", campoCodigo);

            return camposModel;
        }

        /// <summary>
        /// Método que trata as tabelas
        /// </summary>
        /// <param name="projeto">Projeto ao qual a tabela pertence</param>
        /// <param name="tabelas">Tabelas a serem importadas para o projeto</param>
        private List<Model.MD_Tabela> TratarTabelas(ref List<Model.Tabela> tabelas, ref BarraDeCarregamento barra)
        {
            Util.CL_Files.WriteOnTheLog("Importador.TratarTabelas()", Util.Global.TipoLog.DETALHADO);
            List<Model.MD_Tabela> tabelasRetorno = new List<Model.MD_Tabela>();
            int tableCodigo = DataBase.Connection.GetIncrement("TABELA");

            foreach (Model.Tabela t in tabelas)
            {
                barra.AvancaBarra(1);
                Model.MD_Tabela tabela = new Model.MD_Tabela(tableCodigo, 0);
                tabela.DAO.Nome = t.nome;

                tabelasRetorno.Add(tabela);
                tableCodigo++;
            }

            DataBase.Connection.SetIncrement("TABELA", tableCodigo+1);

            return tabelasRetorno;
        }

        /// <summary>
        /// Método que trata os relacionamentos
        /// </summary>
        /// <param name="projeto">Projeto ao qual o relacionamento pertence</param>
        /// <param name="relacionamentos">Relacionamentos a serem importadas para o projeto</param>
        private List<Model.MD_Relacao> TratarRelacionamento(List<Model.MD_Campos> camposModel, List<Model.MD_Tabela> tabelasModel, ref List<Model.Relacionamento> relacionamentos, ref BarraDeCarregamento barra)
        {
            Util.CL_Files.WriteOnTheLog("Importador.TratarRelacionamento()", Util.Global.TipoLog.DETALHADO);
            List<Model.MD_Relacao> retorno = new List<Model.MD_Relacao>();
            int codigoRelacao = DataBase.Connection.GetIncrement("RELACAO");
            relacionamentos.ForEach(relacionamento =>
            {

                DAO.MD_Tabela tabelaOrigem = tabelasModel.Where(tabela => tabela.DAO.Nome.ToUpper().Equals(relacionamento.tabelaOrigem.ToUpper())).FirstOrDefault()?.DAO;
                DAO.MD_Tabela tabelaDestino = tabelasModel.Where(tabela => tabela.DAO.Nome.ToUpper().Equals(relacionamento.tabelaDestino.ToUpper())).FirstOrDefault()?.DAO;
                DAO.MD_Campos campoOrigem = camposModel.Where(campo => campo.DAO.Nome.ToUpper().Equals(relacionamento.campoOrigem.ToUpper())).FirstOrDefault()?.DAO;
                DAO.MD_Campos campoDestino = camposModel.Where(campo => campo.DAO.Nome.ToUpper().Equals(relacionamento.campoDestino.ToUpper())).FirstOrDefault()?.DAO;

                Model.MD_Relacao relacao = new Model.MD_Relacao(codigoRelacao, 0, tabelaOrigem, tabelaDestino, campoOrigem, campoDestino);
                relacao.DAO.NomeForeingKey = relacionamento.constraintName;
                retorno.Add(relacao);
                codigoRelacao++;
            });

            DataBase.Connection.SetIncrement("RELACAO", codigoRelacao);

            return retorno;
        }

        /// <summary>
        /// Método que apaga os registros do banco
        /// </summary>
        private void ZeraDados()
        {
            List<string> sentenca = new List<string>();
            sentenca.Add("DELETE FROM TABELA");
            sentenca.Add("DELETE FROM CAMPOS");
            sentenca.Add("DELETE FROM RELACAO");

            sentenca.ForEach(s => DataBase.Connection.Delete(s));
        }

        /// <summary>
        /// Método que copia os arquivos de exportação para importação
        /// </summary>
        public void CopiaArquivos()
        {
            Util.CL_Files.WriteOnTheLog("Importador.CopiaArquivos()", Util.Global.TipoLog.DETALHADO);

            if (Directory.Exists(Global.app_importacao_directory))
                Directory.Delete(Global.app_importacao_directory, true);

            Directory.CreateDirectory(Global.app_importacao_directory);
            foreach(string file in Directory.GetFiles(Global.app_exportacao_directory))
            {
                FileInfo f = new FileInfo(file);
                File.Copy(file, Global.app_importacao_directory + f.Name);
            }
        }

    }
}
