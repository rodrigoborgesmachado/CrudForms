using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using static Util.Enumerator;

namespace Visao
{
    public partial class FO_IdentaJson : Form
    {
        #region Atributos e Propriedades

        public TipoManutencaoTexto Tipo;

        #endregion Atributos e Propriedades

        #region Eventos

        /// <summary>
        /// Evento lançado no clique do bõtão de confirmar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_confirmar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.tbx_input.Text))
            {
                Message.MensagemAlerta("Preencha o campo com valor correto");
            }
            else
            {
                if (Tipo == TipoManutencaoTexto.IDENTAR_JSON || Tipo == TipoManutencaoTexto.IDENTAR_XML)
                    Identa(this.tbx_input.Text);
                else
                    Transforma(this.tbx_input.Text);
            }
        }

        /// <summary>
        /// Evento lançado no botão de copiar texto
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_copiar_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(this.tbx_input.Text);
            Message.MensagemInformacao("Texto copiado para a memória de transferência!");
        }

        #endregion Eventos

        #region Construtor

        public FO_IdentaJson(TipoManutencaoTexto tipo)
        {
            InitializeComponent();

            this.Tipo = tipo;
            IniciaForm();
        }

        #endregion Construtor

        #region Métodos

        /// <summary>
        /// Método que inicializa a tela
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
            this.grb_total.ForeColor = this.ForeColor;

            if(this.Tipo == TipoManutencaoTexto.IDENTAR_JSON)
            {
                this.grb_total.Text = "Json";
                this.btn_confirmar.Text = "Identar";
            }
            else if (this.Tipo == TipoManutencaoTexto.IDENTAR_XML)
            {
                this.grb_total.Text = "XML";
                this.btn_confirmar.Text = "Identar";
            }
            else if (this.Tipo == TipoManutencaoTexto.TRANSFORMAR_JSON_TO_XML)
            {
                this.grb_total.Text = "Json";
                this.btn_confirmar.Text = "Transformar";
            }
            else if (this.Tipo == TipoManutencaoTexto.TRANSFORMAR_XML_TO_JSON)
            {
                this.grb_total.Text = "XML";
                this.btn_confirmar.Text = "Transformar";
            }
        }

        /// <summary>
        /// Método que transforma a string em json formatado
        /// </summary>
        private void Identa(string texto)
        {
            string mensagem = string.Empty;
            string output = this.Tipo == TipoManutencaoTexto.IDENTAR_JSON ? Regras.TransformaTipos.IdentaJson(texto, ref mensagem) : Regras.TransformaTipos.IdentaXml(texto, ref mensagem);

            if (string.IsNullOrEmpty(mensagem))
            {
                this.tbx_input.Text = output;
            }
            else
            {
                Message.MensagemAlerta("Input incorreto! Erro: " + mensagem);
            }
        }

        /// <summary>
        /// Método que transforma a string em json formatado
        /// </summary>
        private void Transforma(string texto)
        {
            string mensagem = string.Empty;
            string output = this.Tipo == TipoManutencaoTexto.TRANSFORMAR_JSON_TO_XML ? Regras.TransformaTipos.ConvertJsonToXml(texto, ref mensagem) : Regras.TransformaTipos.ConvertXmlToJson(texto, ref mensagem);

            if (string.IsNullOrEmpty(mensagem))
            {
                this.tbx_input.Text = output;
            }
            else
            {
                Message.MensagemAlerta("Input incorreto! Erro: " + mensagem);
            }
        }

        #endregion Métodos
    }
}
