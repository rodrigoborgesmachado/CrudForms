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
    public partial class FO_IdentaJson : Form
    {
        public FO_IdentaJson()
        {
            InitializeComponent();
        }

        private void btn_confirmar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.tbx_json.Text))
            {
                Message.MensagemAlerta("Preencha o campo com valor correto");
            }
            else
            {
                string mensagem = string.Empty;
                string json = Regras.IdentJsonString.IdentaJson(this.tbx_json.Text, ref mensagem);

                if (string.IsNullOrEmpty(mensagem))
                {
                    this.tbx_json.Text = json;
                }
                else
                {
                    Message.MensagemAlerta("Json incorreto! Erro: " + mensagem);
                }
            }
        }

        private void btn_copiar_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(this.tbx_json.Text);
        }
    }
}
