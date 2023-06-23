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
    public partial class FO_FiltrarGenerico : Form
    {
        #region Atributos e Propriedades

        Model.MD_Tabela tabela;
        List<Model.MD_Campos> campos;
        Model.Filtro filtro;
        List<TextBox> textBoxes = new List<TextBox>();
        List<Panel> panels = new List<Panel>();
        List<Label> labels = new List<Label>();
        UC_FormularioGenerico formularioGenerico;

        #endregion Atributos e Propriedades

        #region Eventos

        /// <summary>
        /// Evento lançado no clique do botão do filtrar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_filtrar_Click(object sender, EventArgs e)
        {
            if (PreencheFiltro())
            {
                this.formularioGenerico.FillGrid(this.filtro);
                this.Dispose();
            }
        }

        #endregion Eventos

        #region Construtores

        /// <summary>
        /// Construtor principal da classe
        /// </summary>
        /// <param name="tabela"></param>
        /// <param name="campos"></param>
        /// <param name="filtro"></param>
        public FO_FiltrarGenerico(Model.MD_Tabela tabela, List<Model.MD_Campos> campos, Model.Filtro filtro, UC_FormularioGenerico formularioGenerico)
        {
            InitializeComponent();
            this.tabela = tabela;
            this.campos = campos;
            this.filtro = filtro;
            this.formularioGenerico = formularioGenerico;
            this.Text = $"Tabela {tabela.DAO.Nome}";
            this.IniciaForm();
        }

        #endregion Construtores

        #region Métodos

        /// <summary>
        /// Método que inicializa o formulário
        /// </summary>
        private void IniciaForm()
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

            this.PreencheTelaCamposInformacoes();
        }

        /// <summary>
        /// Método que preenche a tela com os campos e as informações
        /// </summary>
        private void PreencheTelaCamposInformacoes()
        {
            int i = 0;
            foreach(Model.MD_Campos campo in this.campos)
            {
                Panel tempPanel = new Panel();
                tempPanel.Dock = DockStyle.Top;
                tempPanel.Size = new Size(482, 49);
                tempPanel.BackColor = this.pan_tot.BackColor;
                this.panels.Add(tempPanel);

                Label labelTemp = new Label();
                labelTemp.Location = new Point(15, 15);
                labelTemp.Text = campo.DAO.Nome;
                labelTemp.AutoSize = true;
                this.labels.Add(labelTemp);

                TextBox textBoxTemp = new TextBox();
                textBoxTemp.Location = new Point(181, 12);
                textBoxTemp.Size = new Size(347, 27);
                textBoxTemp.Text = this.filtro.valores[i];
                this.textBoxes.Add(textBoxTemp);

                tempPanel.Controls.Add(labelTemp);
                tempPanel.Controls.Add(textBoxTemp);
                
                this.pan_tot.Controls.Add(tempPanel);
                i++;
            }

        }

        /// <summary>
        /// Método que preenche filtro de acordo com o formulário
        /// </summary>
        private bool PreencheFiltro()
        {
            this.filtro.valores.Clear();
            bool retorno = true;

            int i = 0;
            this.textBoxes.ForEach(tbx => 
            {
                if (TrataCampo(tbx.Text, campos[i], out var mensagem))
                {
                    this.filtro.valores.Add(tbx.Text);
                }
                else
                {
                    Visao.Message.MensagemAlerta("Erro: " + mensagem);
                    this.filtro.valores.Add(string.Empty);
                    retorno = false;
                }
                i++;
            });

            return retorno;
        }

        /// <summary>
        /// Método que valida se o campo está correto
        /// </summary>
        /// <param name="tbx"></param>
        /// <param name="campos"></param>
        /// <param name="mensagem"></param>
        /// <returns></returns>
        private bool TrataCampo(string tbx,Model.MD_Campos campo, out string mensagem)
        {
            mensagem = string.Empty;
            bool retorno = true;

            if (!string.IsNullOrEmpty(tbx))
            {
                tbx.Split(';').ToList().ForEach(item =>
                {
                    switch (campo.DAO.TipoCampo.Nome)
                    {
                        case "BIGINT":
                            retorno &= int.TryParse(item, out var i);
                            break;
                        case "INT":
                            retorno &= int.TryParse(item, out var i1);
                            break;
                        case "TINYINT":
                            retorno &= int.TryParse(item, out var i2);
                            break;
                        case "SMALLINT":
                            retorno &= int.TryParse(item, out var i3);
                            break;
                        case "DECIMAL":
                            retorno &= decimal.TryParse(item, out var i4);
                            break;
                        case "REAL":
                            retorno &= decimal.TryParse(item, out var i5);
                            break;
                        case "DATETIME":
                            retorno &= DateTime.TryParse(item, out var i6);
                            break;
                    }
                });
            }

            return retorno;
        }

        #endregion Métodos

    }
}
