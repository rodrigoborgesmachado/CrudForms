using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Util.Enumerator;

namespace Visao
{
    public partial class UC_FormularioGenerico : UserControl
    {
        #region Atributos e Propriedades

        string consulta = string.Empty;

        /// <summary>
        /// Tabela de controle do formulário
        /// </summary>
        Model.MD_Tabela tabela;
        List<Model.MD_Campos> colunas;
        public List<Model.MD_Campos> Colunas
        {
            get
            {
                return colunas;
            }
            set
            {
                this.colunas = value;
            }
        }

        /// <summary>
        /// Controle da tela principal
        /// </summary>
        Visao.FO_Principal principal;

        /// <summary>
        /// Filtro
        /// </summary>
        Model.Filtro filtro;

        /// <summary>
        /// Lista de valores
        /// </summary>
        List<Regras.AcessoBancoCliente.AcessoBanco> valores;

        #endregion Atributos e Propriedades

        #region Eventos

        /// <summary>
        /// Evento lançado no clique do botão de reload
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_reload_Click(object sender, EventArgs e)
        {
            this.FillGrid();
        }

        /// <summary>
        /// Evento lançado no clique da opção de salvar consulta
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void salvarConsultaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SalvaConsulta();
        }

        /// <summary>
        /// Evento lançado no clique da opção de gerar relatório CSV a partir 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void exportarCSVToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GerarCSV();
        }

        /// <summary>
        /// Evento lançado no clique do botão de filtrar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_filtrar_Click(object sender, EventArgs e)
        {
            FO_FiltrarGenerico filtrar = new FO_FiltrarGenerico(this.tabela, this.colunas, this.filtro, this);
            filtrar.ShowDialog();
        }

        /// <summary>
        /// Evento lançado no clique do botão de limpar o filtro
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_limparFiltro_Click(object sender, EventArgs e)
        {
            this.filtro = new Model.Filtro();
            this.filtro.Order = new Model.OrderBy();

            this.colunas.ForEach(coluna =>
            {
                filtro.campos.Add(coluna.DAO.Nome);
                filtro.valores.Add(string.Empty);
            });

            this.FillGrid();
        }

        /// <summary>
        /// Evento lançado no clique da opção de visualizar os detalhes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_visualizar_Click(object sender, EventArgs e)
        {
            this.AbrirTelaCadastro(Tarefa.VISUALIZANDO);
        }

        /// <summary>
        /// Evento lançado no clique do botão de editar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_editar_Click(object sender, EventArgs e)
        {
            this.AbrirTelaCadastro(Tarefa.EDITANDO);
        }

        /// <summary>
        /// Evento lançado no clique da opção de incluir
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_incluir_Click(object sender, EventArgs e)
        {
            this.AbrirTelaCadastro(Tarefa.INCLUINDO);
        }

        /// <summary>
        /// Evento lançado no clique do botão de excluir
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_excluir_Click(object sender, EventArgs e)
        {
            if(this.dgv_generico.SelectedRows.Count == 0)
            {
                Visao.Message.MensagemAlerta("Selecione um item no grid!");
            }
            else
            {
                int index = this.dgv_generico.SelectedRows[0].Index;
                if(!this.valores[index].DeleteValores(this.tabela, this.tabela.CamposDaTabela(), out var mensagem))
                {
                    Visao.Message.MensagemAlerta("Erro: " + mensagem);
                }
                else
                {
                    Visao.Message.MensagemSucesso("Excluído com sucesso");
                    this.FillGrid();
                }
            }
        }

        /// <summary>
        /// Evento lançado no clique do botão de fechar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_fechar_Click(object sender, EventArgs e)
        {
            if(this.tabela == null)
            {
                this.principal.FecharTela("generica");
            }
            else
            {
                if(this.valores != null)
                {
                    this.valores.Clear();
                    this.colunas.Clear();
                }
                this.principal.FecharTela(this.tabela?.DAO.Nome);
            }
        }

        /// <summary>
        /// Evento lançado no clique do botão de ordenação
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_orderBy_Click(object sender, EventArgs e)
        {
            FO_OrderBy orderBy = new FO_OrderBy(this, colunas, filtro);
            orderBy.ShowDialog();
        }

        /// <summary>
        /// Evento lançado no clique da opção de configuração das colunas
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void configuraçãoDasColunasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FO_ConfigColunas configColunas = new FO_ConfigColunas(this);
            configColunas.ShowDialog();
        }

        #endregion Eventos

        #region Construtores

        /// <summary>
        /// Construtor principal da classe
        /// </summary>
        /// <param name="tabela"></param>
        /// <param name="principal"></param>
        public UC_FormularioGenerico(Model.MD_Tabela tabela, Visao.FO_Principal principal)
        {
            InitializeComponent();
            this.Dock = DockStyle.Fill;
            this.tabela = tabela;
            this.colunas = this.tabela.CamposDaTabela();
            this.consulta = string.Empty;

            this.filtro = new Model.Filtro();
            this.filtro.Order = new Model.OrderBy();
            this.colunas.ForEach(coluna => 
            {
                filtro.campos.Add(coluna.DAO.Nome);
                filtro.valores.Add(string.Empty);
            });

            this.principal = principal;
            this.IniciaForm();
        }

        /// <summary>
        /// Construtor secundario da classe
        /// </summary>
        /// <param name="consulta"></param>
        /// <param name="principal"></param>
        public UC_FormularioGenerico(string consulta, Visao.FO_Principal principal)
        {
            InitializeComponent();
            this.Dock = DockStyle.Fill;
            this.tabela = null;
            this.colunas = null;
            this.filtro = null;
            this.consulta = consulta;

            this.principal = principal;
            this.IniciaForm();
        }

        #endregion Construtores

        #region Métodos

        /// <summary>
        /// Método que inicia o formulário
        /// </summary>
        public void IniciaForm()
        {
            if(this.tabela != null)
            {
                this.grb_geral.Text = this.tabela.DAO.Nome;
                this.lbl_quantidadeLinhas.Visible = false;

                if (Model.Parametros.FiltrarAutomaticamente)
                {
                    this.FillGrid(this.filtro);
                }
            }
            else
            {
                this.grb_geral.Text = "Consulta Genérica";
                this.pan_botton.Visible = false;
                this.FillGrid(this.filtro);
            }
        }

        /// <summary>
        /// Preenche o grid com o filtro atual
        /// </summary>
        public void FillGrid()
        {
            FillGrid(filtro);
        }

        /// <summary>
        /// Método que preenche o grid
        /// </summary>
        public void FillGrid(Model.Filtro filter)
        {
            this.dgv_generico.Columns.Clear();
            this.dgv_generico.Rows.Clear();
            
            if (this.tabela != null)
            {
                this.valores = Util.Global.BancoDados == BancoDados.SQL_SERVER ?
                    new Regras.AcessoBancoCliente.AcessoBancoSqlServer().BuscaLista(tabela, colunas, filter, out consulta)
                    :
                    (Util.Global.BancoDados == BancoDados.POSTGRESQL ?
                    new Regras.AcessoBancoCliente.AcessoBancoPostGres().BuscaLista(tabela, colunas, filter, out consulta)
                    :
                    new Regras.AcessoBancoCliente.AcessoBancoMysql().BuscaLista(tabela, colunas, filter, out consulta))
                    ;

                foreach (Model.MD_Campos campo in colunas)
                {
                    if(campo.Visible)
                        this.dgv_generico.Columns.Add(campo.DAO.Nome, campo.DAO.Nome);
                }
            }
            else 
            {
                this.valores = Util.Global.BancoDados == BancoDados.SQL_SERVER ?
                    new Regras.AcessoBancoCliente.AcessoBancoSqlServer().BuscaLista(consulta)
                    :
                    new Regras.AcessoBancoCliente.AcessoBancoPostGres().BuscaLista(consulta)
                    ;

                bool dados = true;
                if (dados) dados &= valores != null;
                if (dados) dados &= valores.Count > 0;

                if (!dados)
                {
                    Message.MensagemAlerta("Seleção não retornou dados!");
                    this.principal.AbreJanelaFormularioConsultaGenerica(this.consulta);
                    this.btn_fechar_Click(null, null);
                    this.Dispose();
                    return;
                }

                foreach (string campo in valores[0].campos)
                {
                    this.dgv_generico.Columns.Add(campo, campo);
                }
            }

            if(valores != null)
            {
                this.lbl_quantidadeLinhas.Visible = true;
                this.lbl_quantidadeLinhas.Text = $"Quantidade: {this.valores.Count.ToString()}";

                FillGrid(valores);
            }
        }

        /// <summary>
        /// Método que preenche a tabela com a lista
        /// </summary>
        /// <param name="lista"></param>
        private void FillGrid(List<Regras.AcessoBancoCliente.AcessoBanco> lista)
        {
            Visao.BarraDeCarregamento barra = new Visao.BarraDeCarregamento(lista.Count, "Carregando");
            barra.Show();

            this.valores.ForEach(p =>
            {
                FillGrid(p);
                barra.AvancaBarra(1);
            });

            for (int i = 0; i < this.dgv_generico.Columns.Count; i++)
            {
                this.dgv_generico.Columns[i].Width = 100;
                this.dgv_generico.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                this.dgv_generico.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
            }

            barra.Dispose();
        }

        /// <summary>
        /// Método que fill o grid
        /// </summary>
        /// <param name="valores"></param>
        private void FillGrid(Regras.AcessoBancoCliente.AcessoBanco valores)
        {
            List<string> list = new List<string>();
            for (int i = 0;i<colunas.Count;i++)
            {
                if (colunas[i].Visible)
                {
                    list.Add(valores.valores[i]);
                }
            }


            this.dgv_generico.Rows.Add(list.ToArray());
        }

        /// <summary>
        /// Método que abre a tela de cadastro
        /// </summary>
        public void AbrirTelaCadastro(Tarefa tarefa)
        {
            if(tarefa != Tarefa.INCLUINDO)
            {
                if (this.dgv_generico.SelectedRows.Count == 0)
                {
                    Visao.Message.MensagemAlerta("Selecione um item no grid!");
                    return;
                }
                FO_FormularioCadastroGenerico formulario = new FO_FormularioCadastroGenerico(this, tabela, this.tabela.CamposDaTabela(), valores[this.dgv_generico.SelectedRows[0].Index], tarefa);
                formulario.ShowDialog();
            }
            else
            {
                Regras.AcessoBancoCliente.AcessoBanco valores;
                if (Util.Global.BancoDados == Util.Enumerator.BancoDados.SQL_SERVER)
                {
                    valores = new Regras.AcessoBancoCliente.AcessoBancoSqlServer();
                }
                else if (Util.Global.BancoDados == Util.Enumerator.BancoDados.POSTGRESQL)
                {
                    valores = new Regras.AcessoBancoCliente.AcessoBancoPostGres();
                }
                else 
                {
                    valores = new Regras.AcessoBancoCliente.AcessoBancoMysql();
                }

                FO_FormularioCadastroGenerico formulario = new FO_FormularioCadastroGenerico(this, tabela, this.tabela.CamposDaTabela(), valores, tarefa);
                formulario.ShowDialog();
            }
        }

        /// <summary>
        /// Método que gera o arquivo CSV
        /// </summary>
        public void GerarCSV()
        {
            bool haValores = true;
            haValores &= valores != null;
            haValores &= valores?.Count > 0;

            if (!haValores)
            {
                Message.MensagemAlerta("Não há dados na busca para gerar relatório!");
                return;
            }

            if(tabela != null)
            {
                if (!Regras.GerarCsv.GerarArquivo(this.valores, new System.IO.DirectoryInfo(Util.Global.app_rel_directory), this.tabela.DAO.Nome, out var mensagemErro))
                {
                    Message.MensagemErro("Houve erros.");
                    Message.MensagemErro(mensagemErro);
                }
                else
                {
                    Message.MensagemSucesso($"Relatório {this.tabela.DAO.Nome}.csv gerado com sucesso no caminho:\n {Util.Global.app_rel_directory}");
                }
            }
            else
            {
                if (!Regras.GerarCsv.GerarArquivo(this.valores, new System.IO.DirectoryInfo(Util.Global.app_rel_directory), "relatorio_generico", out var mensagemErro))
                {
                    Message.MensagemErro("Houve erros.");
                    Message.MensagemErro(mensagemErro);
                }
                else
                {
                    Message.MensagemSucesso($"Relatório relatorio_generico.csv gerado com sucesso no caminho:\n {Util.Global.app_rel_directory}");
                }
            }
            
        }

        /// <summary>
        /// Método que salva a consulta que está na tela
        /// </summary>
        public void SalvaConsulta()
        {
            if (string.IsNullOrEmpty(this.consulta))
            {
                Message.MensagemAlerta("Não há consulta preenchida!");
                return;
            }

            Model.MD_Consultas consulta = new Model.MD_Consultas(DataBase.Connection.GetIncrement(new DAO.MD_Consultas().table.Table_Name));
            consulta.DAO.Consulta = this.consulta;
            
            Visao.FO_CadastraConsulta cadastraConsulta = new FO_CadastraConsulta(consulta, Tarefa.INCLUINDO);
            cadastraConsulta.Show();
        }

        #endregion Métodos

    }
}
