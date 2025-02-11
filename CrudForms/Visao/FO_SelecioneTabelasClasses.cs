using Regras.ApiClasses;
using Regras.FrontEndClasses;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

namespace Visao
{
    public partial class FO_SelecioneTabelasClasses : Form
    {
        #region Atributos e Propriedades

        bool backEnd = true;
        List<Model.MD_Tabela> lista = new List<Model.MD_Tabela>();
        List<Model.MD_Tabela> lista_selecionados = new List<Model.MD_Tabela>();
        bool locked = false;

        #endregion Atributos e Propriedades

        #region Eventos

        private void btn_colocarTodos_Click(object sender, System.EventArgs e)
        {
            this.lista_selecionados.AddRange(lista);
            this.lista.Clear();
            FillGrid(this.lista);
            FillGridTabelaOut();
        }

        private void btn_rigth_Click(object sender, System.EventArgs e)
        {
            if (this.dgv_tabela_in.SelectedRows.Count == 0)
            {
                Visao.Message.MensagemAlerta("Selecione um item na tabela");
                return;
            }

            int index = this.dgv_tabela_in.SelectedRows[0].Index;
            this.lista_selecionados.Add(this.lista[index]);
            this.lista = this.lista.OrderBy(l => l.DAO.Nome).ToList();

            this.lista.RemoveAt(index);
            FillGrid(this.lista);
            FillGridTabelaOut();
        }

        private void btn_left_Click(object sender, System.EventArgs e)
        {
            if (this.dgv_tabela_out.SelectedRows.Count == 0)
            {
                Visao.Message.MensagemAlerta("Selecione um item na tabela");
                return;
            }

            int index = this.dgv_tabela_out.SelectedRows[0].Index;
            this.lista.Add(this.lista_selecionados[index]);
            this.lista = this.lista.OrderBy(l => l.DAO.Nome).ToList();

            this.lista_selecionados.RemoveAt(index);
            FillGrid(this.lista);
            FillGridTabelaOut();
        }

        private void btn_gerar_Click(object sender, System.EventArgs e)
        {
            GerarClasses();
        }

        private void btn_path_Click(object sender, System.EventArgs e)
        {
            using (FolderBrowserDialog folderDialog = new FolderBrowserDialog())
            {
                folderDialog.Description = "Selecione o diretório";
                folderDialog.ShowNewFolderButton = true;

                if (folderDialog.ShowDialog() == DialogResult.OK)
                {
                    this.tbx_directory.Text = folderDialog.SelectedPath;
                }
            }
        }

        #endregion Eventos

        #region Construtores

        public FO_SelecioneTabelasClasses(bool backEnd)
        {
            this.backEnd = backEnd;
            InitializeComponent();
            this.IniciaForm();
        }

        #endregion Construtores

        #region Métodos

        /// <summary>
        /// Método que inicializa o formulário
        /// </summary>
        private void IniciaForm()
        {
            if (Model.Parametros.ModoDark)
            {
                this.BackColor = Color.FromArgb(51, 51, 51);
                this.ForeColor = Color.White;

                this.dgv_tabela_in.GridColor = this.ForeColor;
                this.dgv_tabela_in.BackgroundColor = this.BackColor;
                this.dgv_tabela_in.DefaultCellStyle.BackColor = this.BackColor;
                this.dgv_tabela_in.DefaultCellStyle.ForeColor = this.ForeColor;
                this.dgv_tabela_in.RowHeadersDefaultCellStyle.BackColor = this.BackColor;
                this.dgv_tabela_in.RowHeadersDefaultCellStyle.ForeColor = this.ForeColor;
                this.dgv_tabela_in.ColumnHeadersDefaultCellStyle.BackColor = this.BackColor;
                this.dgv_tabela_in.ColumnHeadersDefaultCellStyle.ForeColor = this.ForeColor;
            }
            else
            {
                this.BackColor = Color.FromArgb(251, 249, 238);
                this.ForeColor = Color.Black;
            }
            foreach (Button button in this.Controls.OfType<Button>())
            {
                button.BackColor = this.BackColor;
                button.ForeColor = this.ForeColor;
            }

            this.grb_configuracaoSQLSERVER.ForeColor = this.ForeColor;

            this.FillGrid();

            if (backEnd)
            {
                this.Text = "Selecione as tabelas para geração das classes";
                this.pan_selectDirectory.Visible = false;
            }
            else
            {
                this.pan_selectDirectory.Visible = true;
                this.Text = "Selecione as tabelas para geração do projeto front end";
            }
        }

        /// <summary>
        /// Método que preenche o grid
        /// </summary>
        public void FillGrid(List<Model.MD_Tabela> lista = null)
        {
            locked = true;
            if (lista == null)
            {
                this.lista = Model.MD_Tabela.RetornaTodasTabelas(0).OrderBy(t => t.DAO.Nome).ToList();
            }

            this.dgv_tabela_in.Rows.Clear();
            this.dgv_tabela_in.Columns.Clear();

            this.dgv_tabela_in.Columns.Add("Tabela", "Tabela");

            this.lista.ForEach(l => this.FillGrid(l));

            for (int i = 0; i < this.dgv_tabela_in.Columns.Count; i++)
            {
                this.dgv_tabela_in.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                this.dgv_tabela_in.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
            }
            locked = false;

        }

        /// <summary>
        /// Método que preenche o grid com a linha
        /// </summary>
        /// <param name="tabela"></param>
        private void FillGrid(Model.MD_Tabela tabela)
        {
            List<string> temp = new List<string>();
            temp.Add(tabela.DAO.Nome);

            this.dgv_tabela_in.Rows.Add(temp.ToArray());
        }

        /// <summary>
        /// Método que preenche o grid
        /// </summary>
        public void FillGridTabelaOut()
        {
            locked = true;

            this.dgv_tabela_out.Rows.Clear();
            this.dgv_tabela_out.Columns.Clear();

            this.dgv_tabela_out.Columns.Add("Tabela", "Tabela");

            this.lista_selecionados.ForEach(l => this.FillGridTabelaOut(l));

            for (int i = 0; i < this.dgv_tabela_out.Columns.Count; i++)
            {
                this.dgv_tabela_out.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                this.dgv_tabela_out.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
            }
            locked = false;

        }

        /// <summary>
        /// Método que preenche o grid com a linha
        /// </summary>
        /// <param name="tabela"></param>
        private void FillGridTabelaOut(Model.MD_Tabela tabela)
        {
            List<string> temp = new List<string>();
            temp.Add(tabela.DAO.Nome);

            this.dgv_tabela_out.Rows.Add(temp.ToArray());
        }

        public void GerarClasses()
        {
            if(!backEnd && string.IsNullOrEmpty(this.tbx_directory.Text))
            {
                Message.MensagemAlerta("Selecione um diretório!");
                return;
            }

            if (string.IsNullOrEmpty(this.tbx_message.Text))
            {
                Message.MensagemAlerta("Preencha o nome do projeto!");
                return;
            }

            var result = backEnd ? 
                CreatorApiClasses.CreateApiClasses(lista_selecionados, this.tbx_message.Text.Replace(" ", "").Replace("_", ""), out string mensagem) :
                CreatorFrontEndProject.CreateProject(lista_selecionados, this.tbx_directory.Text, this.tbx_message.Text, out mensagem);

            if (result)
            {
                Message.MensagemSucesso("Criado com sucesso");

                if(backEnd)
                    Process.Start("explorer.exe", Util.Global.app_classes_directory);
            }
            else
            {
                Message.MensagemErro("Erro: " + mensagem);
            }
        }

        #endregion Métodos
    }
}
