﻿using System;
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
        List<Model.Valores> lista;

        #endregion Atributos e Propriedades

        #region Eventos

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
                if(!this.lista[index].DeleteValores(this.tabela, this.tabela.CamposDaTabela(), out var mensagem))
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
                this.lista.Clear();
                this.colunas.Clear();
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
                this.lista = Model.Valores.BuscaLista(tabela, colunas, filter);

                foreach (Model.MD_Campos campo in this.tabela.CamposDaTabela())
                {
                    this.dgv_generico.Columns.Add(campo.DAO.Nome, campo.DAO.Nome);
                }
            }
            else 
            {
                this.lista = Model.Valores.BuscaLista(consulta);

                bool dados = true;
                if (dados) dados &= lista != null;
                if (dados) dados &= lista.Count > 0;

                if (!dados)
                {
                    Message.MensagemAlerta("Seleção não retornou dados!");
                    this.principal.AbreJanelaFormularioConsultaGenerica(this.consulta);
                    this.btn_fechar_Click(null, null);
                    this.Dispose();
                    return;
                }

                foreach (string campo in lista[0].campos)
                {
                    
                    this.dgv_generico.Columns.Add(campo, campo);
                }
            }
            this.lbl_quantidadeLinhas.Visible = true;
            this.lbl_quantidadeLinhas.Text = $"Quantidade: {this.lista.Count.ToString()}";

            FillGrid(lista);
        }

        /// <summary>
        /// Método que preenche a tabela com a lista
        /// </summary>
        /// <param name="lista"></param>
        private void FillGrid(List<Model.Valores> lista)
        {
            Visao.BarraDeCarregamento barra = new Visao.BarraDeCarregamento(lista.Count, "Carregando");
            barra.Show();

            this.lista.ForEach(p =>
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
        private void FillGrid(Model.Valores valores)
        {
            List<string> list = valores.valores;

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
                FO_FormularioCadastroGenerico formulario = new FO_FormularioCadastroGenerico(this, tabela, this.tabela.CamposDaTabela(), lista[this.dgv_generico.SelectedRows[0].Index], tarefa);
                formulario.ShowDialog();
            }
            else
            {
                FO_FormularioCadastroGenerico formulario = new FO_FormularioCadastroGenerico(this, tabela, this.tabela.CamposDaTabela(), new Model.Valores(), tarefa);
                formulario.ShowDialog();
            }
        }

        #endregion Métodos

    }
}
