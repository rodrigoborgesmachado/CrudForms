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
    public partial class FO_Aguarde : Form
    {
        /// <summary>
        /// Contrutor principal da classe
        /// </summary>
        public FO_Aguarde(string mengagem)
        {
            Util.CL_Files.WriteOnTheLog("BarraDeCarregamento.AvancaBarra()", Util.Global.TipoLog.DETALHADO);

            InitializeComponent();
            lbl_carregando.Text = mengagem;

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
        }
    }
}
