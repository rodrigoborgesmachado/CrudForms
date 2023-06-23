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
    public partial class FO_Observer : Form
    {
        #region Atributos e Proriedades

        List<Model.MD_Observer> lista = new List<Model.MD_Observer>();
        bool locked = false;
        FO_Principal principal;

        #endregion Atributos e Proriedades

        #region Eventos

        /// <summary>
        /// Evento lançado no clique do botão de inativar o observer
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_inativar_Click(object sender, EventArgs e)
        {
            int index = BuscaSelecao();

            if (index != -1)
            {
                this.lista[index].DAO.Isactive = this.lista[index].DAO.Isactive.Equals("1") ? "0" : "1";
                this.lista[index].DAO.Update();
                this.dgv_observers.Rows[index].Cells[1].Value = this.lista[index].DAO.Isactive.Equals("1") ? "Ativo" : "Desativado";
                this.btn_inativar.Text = this.lista[index].DAO.Isactive.Equals("1") ? "Desativar" : "Ativar";
            }
        }

        /// <summary>
        /// Evento lançado no clique do botão excluir
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_excluir_Click(object sender, EventArgs e)
        {
            int index = BuscaSelecao();

            if (index != -1)
            {
                if(Message.MensagemConfirmaçãoYesNo($"Deseja excluir o registro '{this.lista[index].DAO.Descricao}'?") == DialogResult.Yes)
                {
                    this.lista[index].DAO.Delete();
                    this.lista.RemoveAt(index);
                    this.dgv_observers.Rows.RemoveAt(index);
                }
            }
        }

        /// <summary>
        /// Evento lançado no clique da opção editar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_editar_Click(object sender, EventArgs e)
        {
            int index = BuscaSelecao();

            if(index != -1)
            {
                AbreTelaCadastro(this.lista[index], Util.Enumerator.Tarefa.EDITANDO);
            }
        }

        /// <summary>
        /// Evento lançado no clique da opção excluir
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_adicionar_Click(object sender, EventArgs e)
        {
            AbreTelaCadastro(null, Util.Enumerator.Tarefa.INCLUINDO);
        }

        /// <summary>
        /// Evento lançado quando alterado a seleção no grid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgv_observers_SelectionChanged(object sender, EventArgs e)
        {
            if (locked) return;
            int index = BuscaSelecao();
            if(index > -1)
            {
                this.btn_inativar.Text = this.lista[index].DAO.Isactive.Equals("1") ? "Desativar" : "Ativar";
            }
        }

        #endregion Eventos

        #region Construtores

        /// <summary>
        /// Construtor principal da classe
        /// </summary>
        public FO_Observer(FO_Principal principal)
        {
            InitializeComponent();
            this.principal = principal;
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

            this.FillGrid();
        }

        /// <summary>
        /// Método que preenche o grid
        /// </summary>
        public void FillGrid()
        {
            locked = true;
            this.lista = Model.MD_Observer.BuscaTodos();

            this.dgv_observers.Rows.Clear();
            this.dgv_observers.Columns.Clear();

            this.dgv_observers.Columns.Add("Observer", "Observer");
            this.dgv_observers.Columns.Add("Status", "Status");

            this.lista.ForEach(l => this.FillGrid(l));

            for (int i = 0; i < this.dgv_observers.Columns.Count; i++)
            {
                this.dgv_observers.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                this.dgv_observers.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
            }
            locked = false;

        }

        /// <summary>
        /// Método que preenche o grid com a linha
        /// </summary>
        /// <param name="observer"></param>
        private void FillGrid(Model.MD_Observer observer)
        {
            List<string> temp = new List<string>();
            temp.Add(observer.DAO.Descricao); 
            temp.Add(observer.DAO.Isactive.Equals("1") ? "Ativo" : "Desativado");

            this.dgv_observers.Rows.Add(temp.ToArray());
        }

        /// <summary>
        /// Método que busca se está selecionado
        /// </summary>
        /// <returns></returns>
        private int BuscaSelecao()
        {
            if(this.dgv_observers.SelectedRows.Count == 0)
            {
                Message.MensagemAlerta("Selecione um item no grid");
                return -1;
            }

            return this.dgv_observers.SelectedRows[0].Index;
        }

        /// <summary>
        /// Método que abre a tela de cadastro
        /// </summary>
        /// <param name="model"></param>
        /// <param name="tarefa"></param>
        private void AbreTelaCadastro(Model.MD_Observer model, Util.Enumerator.Tarefa tarefa)
        {
            FO_CadastraObserver cadastraObserver = new FO_CadastraObserver(this.principal, this, model, tarefa);
            cadastraObserver.ShowDialog();
        }

        #endregion Métodos

    }
}
