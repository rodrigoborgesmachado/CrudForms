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
    public partial class FO_OrderBy : Form
    {
        #region Atributos e Propriedades

        UC_FormularioGenerico main;
        List<Model.MD_Campos> campos;
        Model.Filtro filtro;

        #endregion Atributos e Propriedades

        #region Eventos

        /// <summary>
        /// Evento lançado no cliqye da opção de filtrar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_filtrar_Click(object sender, EventArgs e)
        {
            this.Filtrar();
        }

        #endregion Eventos

        #region Construtores

        /// <summary>
        /// Construtor principal
        /// </summary>
        /// <param name="main">Tela principal</param>
        /// <param name="campos">Campos da tabela</param>
        /// <param name="filtro">Filtro atual</param>
        public FO_OrderBy(UC_FormularioGenerico main, List<Model.MD_Campos> campos, Model.Filtro filtro)
        {
            InitializeComponent();

            this.main = main;
            this.campos = campos;
            this.filtro = filtro;

            IniciaForm();
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

            CarregaCompo();
            this.cmb_formaOrdenacao.SelectedIndex = 0;
            this.cmb_campo.SelectedIndex = 0;
            
            if(this.filtro.Order.CampoOrdenacao != null)
            {
                this.cmb_campo.SelectedItem = this.filtro.Order.CampoOrdenacao.DAO.Nome;
                this.cmb_formaOrdenacao.SelectedIndex = (this.filtro.Order.Asc ? 0 : 1);
            }
        }

        /// <summary>
        /// Método que carrega o compbo de campos
        /// </summary>
        private void CarregaCompo()
        {
            this.campos.ForEach(campo => 
            {
                this.cmb_campo.Items.Add(campo.DAO.Nome);
            });
        }

        /// <summary>
        /// Evento lançado no clique da opção filtrar
        /// </summary>
        private void Filtrar()
        {
            if(this.cmb_campo.SelectedIndex == -1)
            {
                Message.MensagemAlerta("Selecione um campo");
            }
            else if (this.cmb_formaOrdenacao.SelectedIndex == -1)
            {
                Message.MensagemAlerta("Selecione uma forma de ordenação");
            }
            else
            {
                this.filtro.Order = new Model.OrderBy()
                {
                    Asc = this.cmb_formaOrdenacao.SelectedIndex == 0,
                    CampoOrdenacao = this.campos.Where(campo => campo.DAO.Nome == this.cmb_campo.SelectedItem.ToString()).FirstOrDefault(),
                    Desc = this.cmb_formaOrdenacao.SelectedIndex == 1
                };

                this.main.FillGrid(this.filtro);
                this.Dispose();
            }
        }

        #endregion Métodos

    }
}
