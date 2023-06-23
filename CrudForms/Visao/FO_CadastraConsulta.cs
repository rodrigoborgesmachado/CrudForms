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
    public partial class FO_CadastraConsulta : Form
    {
        Model.MD_Consultas consulta;
        Util.Enumerator.Tarefa tarefa;

        public FO_CadastraConsulta(Model.MD_Consultas consulta, Util.Enumerator.Tarefa tarefa)
        {
            InitializeComponent();
            this.consulta = consulta;
            this.tarefa = tarefa;
            this.tbx_consulta.Text = consulta.DAO.Consulta;

            if(tarefa == Util.Enumerator.Tarefa.EDITANDO)
            {
                this.tbx_nomeConsulta.Text = consulta.DAO.Nomeconsulta;
            }

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

        private void btn_confirmar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.tbx_nomeConsulta.Text))
            {
                Message.MensagemAlerta("Campo nome não pode estar em branco");
                return;
            }

            this.consulta.DAO.Nomeconsulta = this.tbx_nomeConsulta.Text;
            this.consulta.DAO.Consulta = this.tbx_consulta.Text;

            bool feito = false;
            if(tarefa == Util.Enumerator.Tarefa.EDITANDO)
            {
                feito = this.consulta.DAO.Update();
            }
            else
            {
                feito = this.consulta.DAO.Insert();
            }

            if (feito)
            {
                Message.MensagemSucesso("Cadastrado com sucesso!");
                this.Dispose();
            }
        }
    }
}
