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
            if (dgv_colunas.SelectedRows.Count == 0)
            {
                Message.MensagemAlerta("Selecione um item no grid");
                return;
            }

            var colunas = this.formularioGenerico.Colunas;
            if (this.dgv_colunas.SelectedRows[0].Index == colunas.Count-1)
            {
                return;
            }

            Model.MD_Campos oldColuna = CampoSelecionado();
            colunas.Remove(oldColuna);
            colunas.Insert(this.dgv_colunas.SelectedRows[0].Index + 1, oldColuna);
            this.formularioGenerico.Colunas = colunas;

            this.FillGrid();
        }

        /// <summary>
        /// Evento lançado no clique da upção de up
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_up_Click(object sender, EventArgs e)
        {
            if (dgv_colunas.SelectedRows.Count == 0)
            {
                Message.MensagemAlerta("Selecione um item no grid");
                return;
            }

            if(this.dgv_colunas.SelectedRows[0].Index == 0)
            {
                return;
            }

            var colunas = this.formularioGenerico.Colunas;
            Model.MD_Campos oldColuna = CampoSelecionado();
            colunas.Remove(oldColuna);
            colunas.Insert(this.dgv_colunas.SelectedRows[0].Index - 1, oldColuna);
            this.formularioGenerico.Colunas = colunas;

            this.FillGrid();
        }

        /// <summary>
        /// Evento lançado no clique na opção de clique de visible
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_visible_Click(object sender, EventArgs e)
        {
            if(dgv_colunas.SelectedRows.Count == 0)
            {
                Message.MensagemAlerta("Selecione um item no grid");
                return;
            }

            var colunas = this.formularioGenerico.Colunas;
            Model.MD_Campos oldColuna = CampoSelecionado();
            bool oldValue = oldColuna.Visible;
            colunas.Where(coluna => coluna.Equals(oldColuna)).FirstOrDefault().Visible = !oldValue;
            this.formularioGenerico.Colunas = colunas;

            this.FillGrid();
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
            this.FillGrid();
        }

        /// <summary>
        /// Método que preenche o grid
        /// </summary>
        private void FillGrid()
        {
            this.dgv_colunas.Rows.Clear();
            this.dgv_colunas.Columns.Clear();

            this.dgv_colunas.Columns.Add("Colunas", "Colunas");
            this.dgv_colunas.Columns.Add("Visível", "Visível");

            this.formularioGenerico.Colunas.ForEach(coluna => this.FillGrid(coluna));
        }

        /// <summary>
        /// Método que adiciona a coluna no grid
        /// </summary>
        /// <param name="coluna"></param>
        private void FillGrid(Model.MD_Campos coluna)
        {
            List<string> linha = new List<string>();
            linha.Add(coluna.DAO.Nome);
            linha.Add(coluna.Visible ? "Visível" : "Não visível");

            this.dgv_colunas.Rows.Add(linha.ToArray());
        }

        /// <summary>
        /// Método que busca o campo selecionado
        /// </summary>
        /// <returns></returns>
        private Model.MD_Campos CampoSelecionado()
        {
            Model.MD_Campos coluna = null;

            if(this.dgv_colunas.SelectedRows.Count > 0)
            {
                coluna = this.formularioGenerico.Colunas[this.dgv_colunas.SelectedRows[0].Index];
            }

            return coluna;
        }

        #endregion Métodos

    }
}
