using Regras;
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

        System.Diagnostics.FileVersionInfo fvi;

        List<Timer> timers = new List<Timer>();

        ContextMenuStrip contextMenuStrip;

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
            this.VerificaAtualizacoes();
            this.CarregaObservers();
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

            this.ImportaDadosBanco();
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
            this.GerarDer();
        }

        /// <summary>
        /// Evento lançado no clique da opção de identar json
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void identarJsonToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            FO_IdentaJson identaJson = new FO_IdentaJson();
            identaJson.Show();
        }

        /// <summary>
        /// Evento lançado no clique dá opção de atualizar o 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void atualizaTabelasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.ImportaDadosBanco();
        }

        /// <summary>
        /// Evento lançado no clique da opção de enumerar as linhas das tabelas
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void enumeraLinhasDasTabelasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (lockchange) return;

            enumeraLinhasDasTabelasToolStripMenuItem.Checked = !enumeraLinhasDasTabelasToolStripMenuItem.Checked;
            Model.Parametros.NumeracaoLinhasTabelas = enumeraLinhasDasTabelasToolStripMenuItem.Checked;
        }

        /// <summary>
        /// Evento lançado quando digitado a quantidade de dias
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbx_quantidade_dias_atualizacao_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (lockchange) return;

            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
                return;
            }
            else
            {
                e.Handled = false;
            }

            if (int.TryParse(this.tbx_quantidade_dias_atualizacao.Text, out int numero))
            {
                Model.Parametros.QuantidadeDiasAtualizacaoTabela = numero;
            }
        }

        /// <summary>
        /// Evento lançado no clique do botão de atualizar o sistema
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buscarAtualizaçãoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.BuscaAtualização();
        }

        /// <summary>
        /// Evento lançado no clique da opção de adicionar inspeção automática
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void adicionarInspeçãoAutomáticaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FO_Observer observer = new FO_Observer(this);
            observer.ShowDialog();
        }

        /// <summary>
        /// Método que importa planilha
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void importarPlanilhaCSVToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FO_ImportaPlanilha importaPlanilha = new FO_ImportaPlanilha(this);
            importaPlanilha.ShowDialog();
        }

        /// <summary>
        /// Evento lançado no clique da opção para gerar planilha de csv
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void importarPlanilhaCSVToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            FO_ImportaPlanilha importaPlanilha = new FO_ImportaPlanilha(this);
            importaPlanilha.ShowDialog();
        }

        /// <summary>
        /// Evento lançado no clique do botão direito no tab
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Tbc_table_control_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                this.contextMenuStrip.Show(this.tbc_table_control, e.Location);
            }
        }

        /// <summary>
        /// Evento lançado no clique da opção para fechar demais telas
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CloseAllOthersPages_Click(object sender, EventArgs e)
        {
            FecharDemaisTelas();
        }

        /// <summary>
        /// Evento lançado no clique da opção de limpar filtragem do treeview
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_limpar_filtro_Click(object sender, EventArgs e)
        {
            this.tbx_filtro.Text = string.Empty;
        }

        /// <summary>
        /// Evento lançado na alteração do texto do filtro
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbx_filtro_TextChanged(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(this.tbx_filtro.Text))
                this.CarregaTreeView(false);

            List<TreeNode> nodeToAdd = new List<TreeNode>();
            foreach(TreeNode node in this.trv_tabelas.Nodes[0].Nodes)
            {
                if (node.Text.ToUpper().Contains(this.tbx_filtro.Text.ToUpper()))
                {
                    nodeToAdd.Add(node);
                }
            }
            this.trv_tabelas.Nodes[0].Nodes.Clear();
            nodeToAdd.ForEach(node => this.trv_tabelas.Nodes[0].Nodes.Add(node));
        }

        /// <summary>
        /// Evento lançado na entrada de botão backspace
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbx_filtro_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar == (char)Keys.Delete || e.KeyChar == (char)Keys.Back)
            {
                this.CarregaTreeView(false);
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
            fvi = System.Diagnostics.FileVersionInfo.GetVersionInfo(assembly.Location);
            string version = fvi.FileVersion;
            this.lbl_valorVersao.Text = version;
            this.filtrarToolStripMenuItem.Checked = Model.Parametros.FiltrarAutomaticamente;
            this.nãoFiltrarToolStripMenuItem.Checked = !Model.Parametros.FiltrarAutomaticamente;
            this.enumeraLinhasDasTabelasToolStripMenuItem.Checked = Model.Parametros.NumeracaoLinhasTabelas;
            this.tbx_quantidade_dias_atualizacao.Text = Model.Parametros.QuantidadeDiasAtualizacaoTabela.ToString();

            ToolStripMenuItem closeAllPages = new ToolStripMenuItem();
            closeAllPages.Text = "Fechar todas as telas";
            closeAllPages.Click += delegate { FecharTelas(); };

            ToolStripMenuItem closeAllOthersPages = new ToolStripMenuItem();
            closeAllOthersPages.Text = "Fechar as demais telas";
            closeAllOthersPages.Click += CloseAllOthersPages_Click;


            this.contextMenuStrip = new ContextMenuStrip();
            this.contextMenuStrip.Items.Add(closeAllPages);
            this.contextMenuStrip.Items.Add(closeAllOthersPages);

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
            AbreJanela(new UC_Padrao(), string.Empty, "padrao");
        }

        /// <summary>
        /// Método que carrega o tree view
        /// </summary>
        private void CarregaTreeView(bool showBarra = true)
        {
            Util.CL_Files.WriteOnTheLog("FO_Principal.CarregaTreeView()", Util.Global.TipoLog.DETALHADO);

            this.trv_tabelas.Nodes.Clear();

            BarraDeCarregamento aguarde = new BarraDeCarregamento(this.BuscaTotalItensTreeView(), "Carregando TreeView");
            if(showBarra)
                aguarde.Show();
            this.trv_tabelas.Scrollable = true;

            this.CarregaTabelas(ref aguarde);

            if (showBarra)
            {
                aguarde.Close();
            }
            
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

            string select = new DAO.MD_Tabela().table.CreateCommandSQLTable() + " ORDER BY NOME";
            DbDataReader reader = DataBase.Connection.Select(select);

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
            
            MenuItem menu1 = new MenuItem("CSV", delegate { this.ExportaTabela(tabela.DAO.Codigo, TipoArquivoExportacao.CSV); });
            MenuItem menu2 = new MenuItem("JSON", delegate { this.ExportaTabela(tabela.DAO.Codigo, TipoArquivoExportacao.JSON); });
            MenuItem menu3 = new MenuItem("XML", delegate { this.ExportaTabela(tabela.DAO.Codigo, TipoArquivoExportacao.XML); });

            MenuItem menu = new MenuItem("Exportar tabela");
            menu.MenuItems.Add(menu1);
            menu.MenuItems.Add(menu2);
            menu.MenuItems.Add(menu3);

            nodeTabela.ContextMenu = new ContextMenu();
            nodeTabela.ContextMenu.MenuItems.Add(menu);
        }

        /// <summary>
        /// Método que abre a janela para visualização
        /// </summary>
        /// <param name="code">Código da janela a ser aberta</param>
        public void AbrirJanela(string code)
        {
            Util.CL_Files.WriteOnTheLog("FO_Principal.AbrirJanela()", Util.Global.TipoLog.DETALHADO);

            if (string.IsNullOrEmpty(code)) return;

            if (!string.IsNullOrEmpty(this.tbx_filtro.Text))
            {
                this.tbx_filtro.Text = string.Empty;
                this.CarregaTreeView(false);
            }

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

            bool opened = false;
            foreach (TabPage p in Pages)
            {
                if (p.Tag.ToString().Equals(tag))
                {
                    this.tbc_table_control.SelectedTab = p;
                    opened = true;
                    break;
                }
            }

            if (!opened)
            {
                TabPage page = new TabPage(titulo);

                page.Tag = tag;
                Pages.Add(page);
                
                page.Controls.Add(control);
                this.tbc_table_control.Controls.Add(page);
                this.tbc_table_control.Dock = DockStyle.Fill;
                this.tbc_table_control.Name = titulo;
                this.tbc_table_control.SelectedTab = page;
                this.tbc_table_control.MouseClick += Tbc_table_control_MouseClick;

                if (this.Pages.Count > 1 && this.Pages.Where(p => p.Tag.ToString().Equals("padrao")).Count() > 0)
                    FecharTela("padrao");
            }
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
        /// Método que fecha todas as telas
        /// </summary>
        public void FecharDemaisTelas()
        {
            List<string> lista = new List<string>();

            this.Pages.ForEach(elem => lista.Add(elem.Tag.ToString()));
            lista.RemoveAll(i => i.Equals(this.tbc_table_control.SelectedTab.Tag));

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

            if (this.tbc_table_control.TabPages.Count > 1)
                this.tbc_table_control.SelectedTab = this.tbc_table_control.TabPages[this.tbc_table_control.TabPages.Count - 1];
            
            if(this.Pages.Count == 0)
                AbreJanela(new UC_Padrao(), string.Empty, "padrao");
        }

        /// <summary>
        /// Método que carrega o treeview se estiver automático
        /// </summary>
        public void CarregaTreeViewAutomaticamente()
        {
            Util.CL_Files.WriteOnTheLog("FO_Principal.CarregaTreeViewAutomaticamente()", Util.Global.TipoLog.DETALHADO);

                this.CarregaTreeView();
            if (Util.Global.CarregarAutomaticamente == Automatico.Automatico)
            {
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

        /// <summary>
        /// Método que gera der
        /// </summary>
        public void GerarDer(List<Model.MD_Tabela> tabelas = null, List<Model.MD_Campos> campos = null, bool abrir = true)
        {
            if (File.Exists(Util.Global.app_DER_file_TableB))
            {
                if (abrir)
                {
                    System.Diagnostics.Process.Start(Util.Global.app_DER_file_Table);
                }
                return;
            }

            if (tabelas == null)
            {
                tabelas = Model.MD_Tabela.RetornaTodasTabelas(0);
            }

            if(campos == null)
            {
                campos = Model.MD_Campos.RetornaTodosCampos();
            }

            if (new GerarDer().Gerar(tabelas, campos))
            {
                if (abrir)
                {
                    System.Diagnostics.Process.Start(Util.Global.app_DER_file_Table);
                }
            }
            else
            {
                Message.MensagemAlerta("Erro ao gerar o DER");
            }
        }

        /// <summary>
        /// Método que faz importação dos dados do banco
        /// </summary>
        public void ImportaDadosBanco()
        {
            Importador importar = new Importador();
            InformacoesSaidaImportador importador = importar.Importar(0);

            if (!importador.Importado)
            {
                Message.MensagemAlerta("Erro ao importar");
            }
            else
            {
                Model.Parametros.UltimaAtualizacaoTabela = DateTime.Now;
                importador.tabelasModel = importador.tabelasModel.OrderByDescending(t => t.DAO.Nome).Reverse().ToList();
                importador.camposModel = importador.camposModel.OrderByDescending(c => c.DAO.Nome).Reverse().ToList();
                this.GerarDer(importador.tabelasModel, importador.camposModel, false);
            }

            this.FecharTelas();
            this.CarregaTreeView();
            this.IniciaForm();
        }

        /// <summary>
        /// Método que verifica as atualizações
        /// </summary>
        public void VerificaAtualizacoes()
        {
            VerificaAtualizacoesTabelas();
            VerificaAtualizacaoSistema();
        }

        /// <summary>
        /// Método que verifica se é necessário atualizar os dados com o banco de dados
        /// </summary>
        public void VerificaAtualizacoesTabelas()
        {
            if (Model.Parametros.UltimaAtualizacaoTabela.AddDays(Model.Parametros.QuantidadeDiasAtualizacaoTabela) < DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd")))
            {
                this.ImportaDadosBanco();
            }
        }

        /// <summary>
        /// Método que verifica se existe atualização pendente para o sistema
        /// </summary>
        public void VerificaAtualizacaoSistema()
        {
            if(PrecisaAtualizarSistema())
            {
                if(Message.MensagemConfirmaçãoYesNo("Há uma nova versão disponível. Deseja atualizar?") == DialogResult.Yes)
                {
                    this.BuscaAtualização();
                }
                else
                {
                    this.buscarAtualizaçãoToolStripMenuItem.Text = "*" + this.buscarAtualizaçãoToolStripMenuItem.Text + "*";
                    this.buscarAtualizaçãoToolStripMenuItem.BackColor = Color.Green;
                }
            }
            else
            {
                this.buscarAtualizaçãoToolStripMenuItem.BackColor = Color.Transparent;
            }
        }

        /// <summary>
        /// Método que valida se precisa atualizar o sistema
        /// </summary>
        /// <returns></returns>
        private bool PrecisaAtualizarSistema()
        {
            int lastVersion = int.Parse(Util.Global.usuarioLogado.LASTVERSION.Replace(".", ""));
            int currentVersion = int.Parse(fvi.FileVersion.Replace(".", ""));

            return lastVersion > currentVersion;
        }

        /// <summary>
        /// Método que busca a atualização
        /// </summary>
        public void BuscaAtualização()
        {
            BuscaNovaVersaoSistema busca = new BuscaNovaVersaoSistema();
            if (PrecisaAtualizarSistema())
            {
                this.buscarAtualizaçãoToolStripMenuItem.BackColor = Color.Transparent;
                busca.BuscaNovaVersao(Util.Global.usuarioLogado.LASTVERSION);
            }
            else
            {
                Message.MensagemAlerta("Não há nova versão disponível");
            }

        }

        /// <summary>
        /// Método que carrega os observers
        /// </summary>
        public void CarregaObservers()
        {
            List<Model.MD_Observer> observers = Model.MD_Observer.BuscaTodos().Where(l => l.DAO.Isactive.Equals("1")).ToList();
            timers.ForEach(t => t.Stop());

            timers = new List<Timer>();
            observers.ForEach(observer =>
            {
                Timer t = new Timer();

                t.Interval = observer.DAO.Intervalorodar * 60000;
                
                RodaObservers exec = new RodaObservers();
                t.Tick += delegate
                {
                    exec.Processa(observer);
                };

                exec.Processa(observer);
                t.Start();
                timers.Add(t);
            });
            
        }

        /// <summary>
        /// Método que faz exportação de toda a tabela dado o código da mesma e o tipo de exportação
        /// </summary>
        /// <param name="codigoTabela"></param>
        /// <param name="tipo"></param>
        public void ExportaTabela(int codigoTabela, TipoArquivoExportacao tipo)
        {
            Model.MD_Tabela tabela = new Model.MD_Tabela(codigoTabela, 0);
            var valores = Regras.AcessoBancoCliente.AcessoBanco.GetInstanciaBancoCliente().BuscaLista(tabela, tabela.CamposDaTabela(), new Model.Filtro(), out string consulta, false);
            string nomeArquivo = tabela != null ? tabela.DAO.Nome : "relatorio_generico";

            if (!GerarArquivoExportacao.GerarArquivoSolicitandoCaminho(tipo, valores, nomeArquivo, out var mensagemErro, out var diretorio))
            {
                Message.MensagemErro("Houve erros.");
                Message.MensagemErro(mensagemErro);
            }
            else
            {
                Message.MensagemSucesso($"Relatório {nomeArquivo}.{(tipo == TipoArquivoExportacao.CSV ? "csv" : (tipo == TipoArquivoExportacao.JSON ? "json" : "csv"))} gerado com sucesso no caminho:\n {diretorio}");
            }
        }




        #endregion Métodos

    }
}
