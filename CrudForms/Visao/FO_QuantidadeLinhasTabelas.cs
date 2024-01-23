using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Util.Enumerator;

namespace Visao
{
    public partial class FO_QuantidadeLinhasTabelas : Form
    {
        #region Atributos e Propriedades

        TipoNumero tipo;

        #endregion Atributos e Propriedades

        #region Eventos

        /// <summary>
        /// Evento lançado no clique da opçção de executar ação
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_acao_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(this.tbx_quantidade.Text, out var temp))
            {
                Message.MensagemAlerta("Número inválido!");
            }
            else
            {
                Confirma(this.tbx_quantidade.Text);
            }
        }

        #endregion Eventos

        #region Construtores

        /// <summary>
        /// Construtor principal da classe
        /// </summary>
        public FO_QuantidadeLinhasTabelas(TipoNumero tipo)
        {
            InitializeComponent();
            this.tipo = tipo;
            IniciaForm();
        }

        #endregion Construtores

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

            this.tbx_quantidade.Text = this.tipo == TipoNumero.QUANTIDADE_ITENS_TABELA ? Model.Parametros.QuantidadeLinhasTabelas.DAO.Valor : Model.Parametros.QuantidadeDiasLog.ToString();
            this.grb_total.Text = this.tipo == TipoNumero.QUANTIDADE_ITENS_TABELA ? "Quantidade itens por tabela" : "Quantidade de dias para manter o log";
        }

        /// <summary>
        /// Método que confirma
        /// </summary>
        private void Confirma(string valor)
        {
            if(this.tipo == TipoNumero.QUANTIDADE_ITENS_TABELA)
            {
                Model.MD_Parametros parametros = new Model.MD_Parametros(Util.Global.parametro_quantidadeItensPorTabela);
                parametros.DAO.Valor = valor;
                parametros.DAO.Update();

                Model.Parametros.QuantidadeLinhasTabelas = parametros;
            }
            else
            {
                Model.Parametros.QuantidadeDiasLog = int.Parse(valor);
            }

            this.Dispose();
        }

        #endregion Métodos
    }
}
