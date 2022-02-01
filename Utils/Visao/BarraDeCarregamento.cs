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
            Util.CL_Files.WriteOnTheLog("BarraDeCarregamento()", Util.Global.TipoLog.DETALHADO);

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
            Util.CL_Files.WriteOnTheLog("BarraDeCarregamento.InicializaForm()", Util.Global.TipoLog.DETALHADO);

            AtualizaLabel();
            pgb_progresso.Maximum = total;
            Refresh();
        }

        /// <summary>
        /// Método que atualiza a label em tela
        /// </summary>
        public void AtualizaLabel()
        {
            Util.CL_Files.WriteOnTheLog("BarraDeCarregamento.AtualizaLabel()", Util.Global.TipoLog.DETALHADO);

            this.lbl_valor.Text = "0" + total.ToString();
            Refresh();
        }

        /// <summary>
        /// Método que faz a barra avançar em valor vezes
        /// </summary>
        /// <param name="valor"></param>
        public void AvancaBarra(int valor)
        {
            Util.CL_Files.WriteOnTheLog("BarraDeCarregamento.AvancaBarra()", Util.Global.TipoLog.DETALHADO);

            pgb_progresso.Increment(valor);
            if (total >= prosseguindo) prosseguindo++;
            this.lbl_valor.Text = (prosseguindo).ToString() + "/" + total.ToString();
            Refresh();
        }

        #endregion Métodos
    }
}
