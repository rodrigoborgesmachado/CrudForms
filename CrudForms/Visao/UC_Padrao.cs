using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Visao
{
    public partial class UC_Padrao : UserControl
    {
        public UC_Padrao()
        {
            InitializeComponent();
            this.Dock = DockStyle.Fill;

            if (Model.Parametros.ModoDark)
            {
                this.BackColor = Color.FromArgb(51, 51, 51);
                this.ForeColor = Color.White;
                this.BackgroundImage = global::Pj.Properties.Resources.logo_png_crud_forms_200_200;
            }
            else
            {
                this.BackgroundImage = global::Pj.Properties.Resources.logo_png_crud_forms_200_200;
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
