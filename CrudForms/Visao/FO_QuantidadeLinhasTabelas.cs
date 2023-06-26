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
    public partial class FO_QuantidadeLinhasTabelas : Form
    {
        /// <summary>
        /// Construtor principal da classe
        /// </summary>
        public FO_QuantidadeLinhasTabelas()
        {
            InitializeComponent();
            this.tbx_quantidade.Text = Model.Parametros.QuantidadeLinhasTabelas.DAO.Valor;

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
            this.grb_total.ForeColor = this.ForeColor;
        }

        /// <summary>
        /// Evento lançado no clique da opçção de executar ação
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_acao_Click(object sender, EventArgs e)
        {
            if(!int.TryParse(this.tbx_quantidade.Text, out var temp))
            {
                Message.MensagemAlerta("Número inválido!");
            }
            else
            {
                Model.MD_Parametros parametros = new Model.MD_Parametros(Util.Global.parametro_quantidadeItensPorTabela);
                parametros.DAO.Valor = temp.ToString();
                parametros.DAO.Update();

                Model.Parametros.QuantidadeLinhasTabelas = parametros;

                this.Dispose();
            }
        }
    }
}
