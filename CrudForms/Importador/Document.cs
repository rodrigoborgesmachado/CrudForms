using Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace ImportadorNamespace
{
    public abstract class Document
    {
        protected List<DAO.MDN_Table> tabelas = null;
        public abstract List<DAO.MDN_Table> RetornaDetalhesTabelas(Visao.BarraDeCarregamento barra);

        public abstract void RetornaDetalhesCampos(Visao.BarraDeCarregamento barra, ref List<Model.Campo> campos);

        public abstract void RetornaDetalhesRelacionamentos(Visao.BarraDeCarregamento barra, ref List<Model.Relacionamento> relacionamentos);

        /// <summary>
        /// Método que faz a importação das tabelas do banco de dados
        /// </summary>
        /// <returns></returns>
        public bool Importar()
        {
            try
            {
                Visao.BarraDeCarregamento barra = new Visao.BarraDeCarregamento(Total(), "Importando dados");
                barra.Show();

                GerarTabelas(barra);

                GerarColunas(barra);

                GerarRelacionamentos(barra);

                barra.Dispose();
                barra = null;
            }
            catch (Exception e)
            {
                Util.CL_Files.LogException(e);
                return false;
            }

            return true;
        }


        /// <summary>
        /// Método que retorna o total de processamento
        /// </summary>
        /// <returns>total</returns>
        public static int Total()
        {
            return 100000;
        }

        /// <summary>
        /// método que salva no arquivo
        /// </summary>
        /// <param name="tabelas"></param>
        private static void PreencheArquivoCampos(List<Campo> campo)
        {
            if (File.Exists(Util.Global.app_exportacao_campos_file))
            {
                File.Delete(Util.Global.app_exportacao_campos_file);
            }

            string json = JsonConvert.SerializeObject(campo);
            File.WriteAllLines(Util.Global.app_exportacao_campos_file, json.Split(Environment.NewLine.ToCharArray()));
        }

        /// <summary>
        /// Método que gera as colunas
        /// </summary>
        internal void GerarColunas(Visao.BarraDeCarregamento barra)
        {
            List<Campo> campos = new List<Campo>();

            RetornaDetalhesCampos(barra, ref campos);

            PreencheArquivoCampos(campos);
        }

        /// <summary>
        /// Método que gera as tabelas
        /// </summary>
        internal void GerarTabelas(Visao.BarraDeCarregamento barra)
        {
            tabelas = RetornaDetalhesTabelas(barra);

            GeraArquivotabela(tabelas);
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
            if (File.Exists(Util.Global.app_exportacao_tabela_file))
            {
                File.Delete(Util.Global.app_exportacao_tabela_file);
            }

            string json = JsonConvert.SerializeObject(tabelas);
            File.WriteAllLines(Util.Global.app_exportacao_tabela_file, json.Split(Environment.NewLine.ToCharArray()));
        }

        /// <summary>
        /// método que salva no arquivo
        /// </summary>
        /// <param name="tabelas"></param>
        internal void PreencheArquivoRelacionamentos(List<Relacionamento> relacionamentos)
        {
            if (File.Exists(Util.Global.app_exportacao_relacionamentos_file))
            {
                File.Delete(Util.Global.app_exportacao_relacionamentos_file);
            }

            string json = JsonConvert.SerializeObject(relacionamentos);
            File.WriteAllLines(Util.Global.app_exportacao_relacionamentos_file, json.Split(Environment.NewLine.ToCharArray()));
        }

        /// <summary>
        /// Método que gera os relacionamentos
        /// </summary>
        internal void GerarRelacionamentos(Visao.BarraDeCarregamento barra)
        {
            List<Relacionamento> relacionamentos = new List<Relacionamento>();

            RetornaDetalhesRelacionamentos(barra, ref relacionamentos);

            PreencheArquivoRelacionamentos(relacionamentos);

        }
    }
}
