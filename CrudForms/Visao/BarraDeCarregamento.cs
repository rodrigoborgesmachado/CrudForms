using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Visao
{
    public partial class BarraDeCarregamento : Form
    {
        #region Atributos e Propriedades

        /// <summary>
        /// Total de arquivos
        /// </summary>
        int total = 0;

        /// <summary>
        /// Controle das imagens a ser produzidas
        /// </summary>
        int prosseguindo = 0;

        #endregion Atributos e Propriedades

        #region Construtores

        public BarraDeCarregamento(int totalm, string texto)
        {
            InitializeComponent();
            this.lbl_texto.Text = texto;
            this.total = totalm;
            Application.DoEvents();
            InicializaForm();
        }

        #endregion Construtores

        #region Métodos

        /// <summary>
        /// Método que inicializa a tela
        /// </summary>
        public void InicializaForm()
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

            AtualizaLabel();
            pgb_progresso.Maximum = total;
            Refresh();
        }

        /// <summary>
        /// Método que atualiza a label em tela
        /// </summary>
        public void AtualizaLabel()
        {

            this.lbl_valor.Text = "0" + total.ToString();
            Refresh();
        }

        /// <summary>
        /// Método que faz a barra avançar em valor vezes
        /// </summary>
        /// <param name="valor"></param>
        public void AvancaBarra(int valor)
        {

            pgb_progresso.Increment(valor);
            if (total >= prosseguindo) prosseguindo++;
            this.lbl_valor.Text = (prosseguindo).ToString() + "/" + total.ToString();
            Refresh();
        }

        #endregion Métodos
    }
}
