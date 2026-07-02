using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Visao
{
    public partial class FO_ConfigColunas : Form
    {
        #region Atributos e Propriedades

        UC_FormularioGenerico formularioGenerico;

        #endregion Atributos e Propriedades

        #region Eventos

        /// <summary>
        /// Evento lançado no clique da opção de down
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_down_Click(object sender, EventArgs e)
        {
            Model.MD_Campos colunaSelecionada = CampoSelecionado();
            if (colunaSelecionada == null)
            {
                Message.MensagemAlerta("Selecione um item no grid");
                return;
            }

            var colunas = this.formularioGenerico.Colunas;
            int currentIndex = colunas.IndexOf(colunaSelecionada);
            int targetIndex = currentIndex + 1;
            if (currentIndex < 0 || targetIndex >= colunas.Count)
            {
                return;
            }

            var temp = colunas[targetIndex];
            colunas[targetIndex] = colunas[currentIndex];
            colunas[currentIndex] = temp;
            this.formularioGenerico.Colunas = colunas;

            this.FillGrid(colunaSelecionada);
        }

        /// <summary>
        /// Evento lançado no clique da upção de up
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_up_Click(object sender, EventArgs e)
        {
            Model.MD_Campos colunaSelecionada = CampoSelecionado();
            if (colunaSelecionada == null)
            {
                Message.MensagemAlerta("Selecione um item no grid");
                return;
            }

            var colunas = this.formularioGenerico.Colunas;
            int currentIndex = colunas.IndexOf(colunaSelecionada);
            int targetIndex = currentIndex - 1;
            if (currentIndex < 0 || targetIndex < 0)
            {
                return;
            }

            var temp = colunas[targetIndex];
            colunas[targetIndex] = colunas[currentIndex];
            colunas[currentIndex] = temp;
            this.formularioGenerico.Colunas = colunas;

            this.FillGrid(colunaSelecionada);
        }

        /// <summary>
        /// Evento lançado no clique na opção de clique de visible
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_visible_Click(object sender, EventArgs e)
        {
            Model.MD_Campos colunaSelecionada = CampoSelecionado();
            if (colunaSelecionada == null)
            {
                Message.MensagemAlerta("Selecione um item no grid");
                return;
            }

            colunaSelecionada.Visible = !colunaSelecionada.Visible;

            this.FillGrid(colunaSelecionada);
        }

        /// <summary>
        /// Evento lançado no clique do botão de confirmar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_confirmar_Click(object sender, EventArgs e)
        {
            this.formularioGenerico.FillGrid();
            this.Close();
            this.Dispose();
        }

        #endregion Eventos

        #region Construtores

        public FO_ConfigColunas(UC_FormularioGenerico formularioGenerico)
        {
            InitializeComponent();
            this.formularioGenerico = formularioGenerico;
            this.IniciaForm();
        }

        #endregion Construtores

        #region Métodos

        /// <summary>
        /// Método que inicializa o formulário
        /// </summary>
        public void IniciaForm()
        {
            if (Model.Parametros.ModoDark)
            {
                this.BackColor = Color.FromArgb(51, 51, 51);
                this.ForeColor = Color.White;

                this.dgv_colunas.GridColor = this.ForeColor;
                this.dgv_colunas.BackgroundColor = this.BackColor;
                this.dgv_colunas.DefaultCellStyle.BackColor = this.BackColor;
                this.dgv_colunas.DefaultCellStyle.ForeColor = this.ForeColor;
                this.dgv_colunas.RowHeadersDefaultCellStyle.BackColor = this.BackColor;
                this.dgv_colunas.RowHeadersDefaultCellStyle.ForeColor = this.ForeColor;
                this.dgv_colunas.ColumnHeadersDefaultCellStyle.BackColor = this.BackColor;
                this.dgv_colunas.ColumnHeadersDefaultCellStyle.ForeColor = this.ForeColor;
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
        }

        /// <summary>
        /// Método que preenche o grid
        /// </summary>
        private void FillGrid(Model.MD_Campos colunaSelecionada = null)
        {
            this.dgv_colunas.Rows.Clear();
            this.dgv_colunas.Columns.Clear();

            this.dgv_colunas.Columns.Add("Colunas", "Colunas");
            this.dgv_colunas.Columns.Add("Visível", "Visível");

            this.formularioGenerico.Colunas.ForEach(coluna => this.FillGridRow(coluna));
            SelectRowByTag(colunaSelecionada);
        }

        /// <summary>
        /// Método que adiciona a coluna no grid
        /// </summary>
        /// <param name="coluna"></param>
        private void FillGridRow(Model.MD_Campos coluna)
        {
            List<string> linha = new List<string>();
            linha.Add(coluna.DAO.Nome);
            linha.Add(coluna.Visible ? "Visível" : "Não visível");

            int rowIndex = this.dgv_colunas.Rows.Add(linha.ToArray());
            this.dgv_colunas.Rows[rowIndex].Tag = coluna;
        }

        /// <summary>
        /// Método que busca o campo selecionado
        /// </summary>
        /// <returns></returns>
        private Model.MD_Campos CampoSelecionado()
        {
            if(this.dgv_colunas.SelectedRows.Count == 0)
            {
                return null;
            }

            return this.dgv_colunas.SelectedRows[0].Tag as Model.MD_Campos;
        }

        private void SelectRowByTag(Model.MD_Campos coluna)
        {
            this.dgv_colunas.ClearSelection();

            if (coluna == null)
            {
                return;
            }

            foreach (DataGridViewRow row in this.dgv_colunas.Rows)
            {
                if (!ReferenceEquals(row.Tag, coluna))
                {
                    continue;
                }

                row.Selected = true;
                this.dgv_colunas.CurrentCell = row.Cells[0];
                return;
            }
        }

        #endregion Métodos

    }
}
