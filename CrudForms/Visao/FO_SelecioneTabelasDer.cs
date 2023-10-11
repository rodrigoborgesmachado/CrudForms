using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Visao
{
    public partial class FO_SelecioneTabelasDer : Form
    {
        #region Atributos e Proriedades

        List<Model.MD_Tabela> lista = new List<Model.MD_Tabela>();
        List<Model.MD_Tabela> lista_tabelas = new List<Model.MD_Tabela>();
        bool locked = false;
        FO_Principal principal;

        #endregion Atributos e Proriedades

        #region Eventos

        private void btn_rigth_Click(object sender, System.EventArgs e)
        {
            if(this.dgv_tabela_in.SelectedRows.Count == 0)
            {
                Visao.Message.MensagemAlerta("Selecione um item na tabela");
                return;
            }

            int index = this.dgv_tabela_in.SelectedRows[0].Index;
            this.lista_tabelas.Add(this.lista[index]);
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
            this.lista.Add(this.lista_tabelas[index]);
            this.lista = this.lista.OrderBy(l => l.DAO.Nome).ToList();

            this.lista_tabelas.RemoveAt(index);
            FillGrid(this.lista);
            FillGridTabelaOut();
        }

        private void btn_gerar_Click(object sender, System.EventArgs e)
        {
            this.principal.GerarDer(this.lista_tabelas, abrir: true, forcar: true);
        }

        #endregion Eventos

        public FO_SelecioneTabelasDer(FO_Principal principal)
        {
            this.principal = principal;
            InitializeComponent();
            this.IniciaForm();
        }

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
        }

        /// <summary>
        /// Método que preenche o grid
        /// </summary>
        public void FillGrid(List<Model.MD_Tabela> lista = null)
        {
            locked = true;
            if(lista == null)
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

            this.lista_tabelas.ForEach(l => this.FillGridTabelaOut(l));

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

        #endregion Métodos


    }
}
