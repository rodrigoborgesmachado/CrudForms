using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Util.Enumerator;
using static Util.Global;

namespace Visao
{
    public partial class FO_Principal : Form
    {
        #region Atributos e Propriedades

        bool lockChange = false;

        /// <summary>
        /// Controle de eventos da tela
        /// </summary>
        bool lockchange = false;

        /// <summary>
        /// Páginas abertas
        /// </summary>
        List<TabPage> Pages = new List<TabPage>();

        #endregion Atributos e Propriedades

        #region Eventos

        /// <summary>
        /// Evento lançado no clique da opção de consultar consultas salvas
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void consultasSalvasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FO_ConsultasSalvas consultasSalvas = new FO_ConsultasSalvas(this);
            consultasSalvas.ShowDialog();
        }

        /// <summary>
        /// Evento lançado no clique da opção de filtrar automaticamente
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void filtrarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (lockchange) return;
            this.nãoFiltrarToolStripMenuItem.Checked = false;
            this.filtrarToolStripMenuItem.Checked = true;
            Model.Parametros.FiltrarAutomaticamente = true;
        }

        /// <summary>
        /// Evento lançado no clique da opção de não filtrar automaticamente
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void nãoFiltrarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (lockchange) return;

            this.filtrarToolStripMenuItem.Checked = false;
            this.nãoFiltrarToolStripMenuItem.Checked = true;
            Model.Parametros.FiltrarAutomaticamente = false;
        }

        /// <summary>
        /// Evento lançado no clique da opção de quantidade de itens nas tabelas
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void quantidadeLinhasTabelasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FO_QuantidadeLinhasTabelas linhasTabelas = new FO_QuantidadeLinhasTabelas();
            linhasTabelas.ShowDialog();
        }

        /// <summary>
        /// Evento lançado quando a tela abre
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FO_Principal_Load(object sender, EventArgs e)
        {
            this.CarregaTreeView();
        }

        /// <summary>
        /// Evento lançado no clique da opção de selecionar a base
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void selecionaBaseToolStripMenuItem_Click(object sender, EventArgs e)
        {

            FO_SelecionaConexao selecionaConexao = new FO_SelecionaConexao();
            if (selecionaConexao.ShowDialog() != DialogResult.OK)
                return;

            if (!Regras.Importador.Importar(0))
            {
                Message.MensagemAlerta("Erro ao importar");
            }

            this.FecharTelas();
            this.CarregaTreeView();
            this.IniciaForm();
        }

        /// <summary>
        /// Evento lançado após seleção no gridview
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void trv_projetos_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (lockChange) return;
            if (this.trv_tabelas.SelectedNode == null) return;
            if (this.trv_tabelas.SelectedNode.Tag.ToString().Trim().Equals("-1")) return;

            string tag = this.trv_tabelas.SelectedNode.Tag.ToString();
            this.AbrirJanela(tag);
        }

        /// <summary>
        /// Evento lançado no clique da opção de fazer consulta genérica
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void abrirConsultaGenéricaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.AbreJanelaFormularioConsultaGenerica();
        }

        /// <summary>
        /// Evento lançado no clique do botão de gerar o DER
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gerarDERToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Regras.GerarDer.Gerar())
            {
                Message.MensagemSucesso("Gerado com sucesso");
                System.Diagnostics.Process.Start(Util.Global.app_DER_file_Table);
            }
            else
            {
                Message.MensagemAlerta("Erro ao gerar o DER");
            }
        }

        #endregion Eventos

        #region Construtores

        /// <summary>
        /// Construtor principal da classe
        /// </summary>
        public FO_Principal()
        {
            Util.CL_Files.WriteOnTheLog("FO_Principal.FO_Principal()", Util.Global.TipoLog.DETALHADO);
            this.InitializeComponent();
            IniciaForm();
        }

        #endregion Construtores

        #region Métodos

        /// <summary>
        /// Método que faz a inicialização do Form
        /// </summary>
        public void IniciaForm()
        {
            Util.CL_Files.WriteOnTheLog("FO_Principal.IniciaForm()", Util.Global.TipoLog.DETALHADO);

            System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
            System.Diagnostics.FileVersionInfo fvi = System.Diagnostics.FileVersionInfo.GetVersionInfo(assembly.Location);
            string version = fvi.FileVersion;
            this.lbl_valorVersao.Text = version;
            this.filtrarToolStripMenuItem.Checked = Model.Parametros.FiltrarAutomaticamente;
            this.nãoFiltrarToolStripMenuItem.Checked = !Model.Parametros.FiltrarAutomaticamente;

            string connection = Model.Parametros.NomeConexao.DAO.Valor;
            if (string.IsNullOrEmpty(connection))
            {
                this.lbl_base.Visible = false;
            }
            else
            {
                this.lbl_base.Visible = true;
                this.lbl_base.Text = "Base: " + connection;
            }

        }

        /// <summary>
        /// Método que carrega o tree view
        /// </summary>
        private void CarregaTreeView()
        {
            Util.CL_Files.WriteOnTheLog("FO_Principal.CarregaTreeView()", Util.Global.TipoLog.DETALHADO);

            this.trv_tabelas.Nodes.Clear();

            BarraDeCarregamento aguarde = new BarraDeCarregamento(this.BuscaTotalItensTreeView(), "Carregando TreeView");

            aguarde.Show();
            this.trv_tabelas.Scrollable = true;

            this.CarregaTabelas(ref aguarde);

            aguarde.Close();
            aguarde.Dispose();
            aguarde = null;
        }

        /// <summary>
        /// Método que adiciona as tabelas no node do projeto
        /// </summary>
        /// <param name="project">Projeto selecionado no node</param>
        /// /// <param name="node">Node a acionar as tabelas</param>
        public void CarregaTabelas(ref BarraDeCarregamento aguarde)
        {
            Util.CL_Files.WriteOnTheLog("FO_Principal.CarregaTabelas()", Util.Global.TipoLog.DETALHADO);

            DbDataReader reader = DataBase.Connection.Select(new DAO.MD_Tabela().table.CreateCommandSQLTable() + " ORDER BY NOME");

            TreeNode nodeTabelas = new TreeNode("Tabelas");
            nodeTabelas.Tag = string.Empty;

            while (reader.Read())
            {
                Model.MD_Tabela tabela = new Model.MD_Tabela(int.Parse(reader["CODIGO"].ToString()), 0);
                if (string.IsNullOrEmpty(tabela.DAO.Nome)) continue;

                TreeNode nodeTabela = new TreeNode(tabela.DAO.Nome);
                nodeTabela.Tag = "tabelas:" + tabela.DAO.Codigo;
                nodeTabela.ImageIndex = 1;
                nodeTabela.SelectedImageIndex = 1;

                this.IncluiMenuTabela(ref nodeTabela, tabela);

                nodeTabelas.Nodes.Add(nodeTabela);
                aguarde.AvancaBarra(1);
            }

            nodeTabelas.Expand();
            this.trv_tabelas.Nodes.Add(nodeTabelas);
            reader.Close();
        }

        /// <summary>
        /// Método que adiciona o menu à tabela
        /// </summary>
        /// <param name="nodeTabela">Node referente à tabela</param>
        private void IncluiMenuTabela(ref TreeNode nodeTabela, Model.MD_Tabela tabela)
        {
            Util.CL_Files.WriteOnTheLog("FO_Principal.IncluiMenuTabela()", Util.Global.TipoLog.DETALHADO);
        }

        /// <summary>
        /// Método que abre a janela para visualização
        /// </summary>
        /// <param name="code">Código da janela a ser aberta</param>
        public void AbrirJanela(string code)
        {
            Util.CL_Files.WriteOnTheLog("FO_Principal.AbrirJanela()", Util.Global.TipoLog.DETALHADO);

            if (string.IsNullOrEmpty(code)) return;

            string codigoTabela = code.Split(':')[1];

            Model.MD_Tabela tabela = new Model.MD_Tabela(int.Parse(codigoTabela), 0);

            AbreJanelaFormularioGenerico(tabela);
        }

        /// <summary>
        /// Método que abre a janela de log
        /// </summary>
        public void AbreJanelaFormularioGenerico(Model.MD_Tabela tabela)
        {
            Util.CL_Files.WriteOnTheLog("FO_Principal.AbreJanelaFormularioGenerico()", Util.Global.TipoLog.DETALHADO);

            UC_FormularioGenerico controle = new UC_FormularioGenerico(tabela, this);
            this.AbreJanela(controle, tabela.DAO.Nome, tabela.DAO.Nome);
        }

        /// <summary>
        /// Método que abre a janela de log
        /// </summary>
        public void AbreJanelaFormularioGenerico(string consulta)
        {
            Util.CL_Files.WriteOnTheLog("FO_Principal.AbreJanelaFormularioGenerico()", Util.Global.TipoLog.DETALHADO);

            UC_FormularioGenerico controle = new UC_FormularioGenerico(consulta, this);

            if(controle.Controls.Count > 0)
                this.AbreJanela(controle, "Tabela Genérica", "generica");
        }

        /// <summary>
        /// Método que abre a janela de log
        /// </summary>
        public void AbreJanelaFormularioConsultaGenerica(string consulta = "")
        {
            Util.CL_Files.WriteOnTheLog("FO_Principal.AbreJanelaFormularioConsulta()", Util.Global.TipoLog.DETALHADO);

            FO_BuscaGenerica controle = new FO_BuscaGenerica(this, consulta);
            controle.Show();
        }

        /// <summary>
        /// Método que abre uma nova aba no tab page
        /// </summary>
        /// <param name="control">User control a ser aberto dentro da page</param>
        /// <param name="titulo">Título da aba da página a ser aberta</param>
        public void AbreJanela(UserControl control, string titulo, string tag)
        {
            Util.CL_Files.WriteOnTheLog("FO_Principal.AbreJanela()", Util.Global.TipoLog.DETALHADO);

            int index = 0;
            string tag_aberto = "";
            bool aberto = false;
            foreach (TabPage p in Pages)
            {
                if (p.Tag.ToString().Equals(tag))
                {
                    tag_aberto = p.Tag.ToString();
                    aberto = true;
                    break;
                }
                else index++;
            }

            if (aberto)
                FecharTela(tag_aberto);
            TabPage page = new TabPage(titulo);

            TabPage tabPage1 = new TabPage(titulo);
            tabPage1.Tag = tag;
            Pages.Add(tabPage1);

            tabPage1.Controls.Add(control);
            this.tbc_table_control.Controls.Add(tabPage1);
            this.tbc_table_control.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbc_table_control.Name = titulo;

            index = 0;
            foreach (TabPage p in this.tbc_table_control.Controls)
            {
                if (p.Tag.ToString().Equals(tag))
                        break;
                index++;
            }

            this.tbc_table_control.TabIndex = index;
            this.tbc_table_control.SelectedIndex = index;
        }

        /// <summary>
        /// Método que fecha todas as telas
        /// </summary>
        public void FecharTelas()
        {
            List<string> lista = new List<string>();
            
            this.Pages.ForEach(elem => lista.Add(elem.Tag.ToString()));

            lista.ForEach(elem => FecharTela(elem));
        }

        /// <summary>
        /// Método que fecha a tela
        /// </summary>
        /// <param name="tag"></param>
        public void FecharTela(string tag)
        {
            Util.CL_Files.WriteOnTheLog("FO_Principal.FecharTela()", Util.Global.TipoLog.DETALHADO);

            int index = 0;
            foreach (TabPage p in Pages)
            {
                if (p.Tag.ToString().Equals(tag))
                {
                    p.Dispose();
                    break;
                }
                else index++;
            }

            if (index < Pages.Count)
                Pages.RemoveAt(index);
        }

        /// <summary>
        /// Método que carrega o treeview se estiver automático
        /// </summary>
        public void CarregaTreeViewAutomaticamente()
        {
            Util.CL_Files.WriteOnTheLog("FO_Principal.CarregaTreeViewAutomaticamente()", Util.Global.TipoLog.DETALHADO);

            if (Util.Global.CarregarAutomaticamente == Automatico.Automatico)
            {
                this.CarregaTreeView();
            }
        }

        /// <summary>
        /// Método que busca a quantidade de itens no tree view
        /// </summary>
        /// <returns></returns>
        public int BuscaTotalItensTreeView()
        {
            int retorno = 100;
            string sentenca = $"SELECT COUNT(1) FROM { new DAO.MD_Tabela().table.Table_Name }";
            DbDataReader reader = DataBase.Connection.Select(sentenca);

            if (reader.Read())
            {
                retorno = int.Parse(reader[0].ToString());
            }
            reader.Close();
            return retorno;
        }



        #endregion Métodos

        
    }
}
