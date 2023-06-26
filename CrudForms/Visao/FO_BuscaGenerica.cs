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
    public partial class FO_BuscaGenerica : Form
    {
        FO_Principal principal;

        /// <summary>
        /// Construtor principal da classe
        /// </summary>
        /// <param name="principal"></param>
        /// <param name="consulta"></param>
        public FO_BuscaGenerica(FO_Principal principal, string consulta = "")
        {
            InitializeComponent();
            this.principal = principal;
            this.tbx_consulta.Text = consulta;

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
            this.grb_configuracaoSQLSERVER.ForeColor = this.ForeColor;
        }

        /// <summary>
        /// Evento lançado no clique da opção de confirmação
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_confirmar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.tbx_consulta.Text))
            {
                Message.MensagemAlerta("Campo de consulta em branco");
            }
            else
            {
                this.principal.AbreJanelaFormularioGenerico(this.tbx_consulta.Text);
                this.Dispose();
            }
        }
    }
}
