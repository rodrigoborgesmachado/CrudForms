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
    public partial class FO_FormularioCadastroGenerico : Form
    {
        #region Atributos e Propriedades

        UC_FormularioGenerico formularioGenerico;
        Model.MD_Tabela tabela;
        List<Model.MD_Campos> campos;
        Model.Valores Valores;
        Tarefa tarefa;
        List<TextBox> textBoxes = new List<TextBox>();
        List<Panel> panels = new List<Panel>();
        List<Label> labels = new List<Label>();

        #endregion Atributos e Propriedades

        #region Eventos

        /// <summary>
        /// Evento lançado no clique do botão ação
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_acao_Click(object sender, EventArgs e)
        {
            if (this.PreencheValores(out var valoresAnteriores))
            {
                switch (this.tarefa) 
                {
                    case Tarefa.EDITANDO:
                        if (!this.Valores.AtualizaValores(tabela, campos, valoresAnteriores, out var mensagem))
                        {
                            Visao.Message.MensagemAlerta("Erro: " + mensagem);
                        }
                        else
                        {
                            Visao.Message.MensagemSucesso("Atualizado com sucesso");
                            this.formularioGenerico.FillGrid();
                            this.Dispose();
                        }
                        break;
                    case Tarefa.INCLUINDO:
                        if (!this.Valores.InsereValores(tabela, campos, out var mensagem1))
                        {
                            Visao.Message.MensagemAlerta("Erro: " + mensagem1);
                        }
                        else
                        {
                            Visao.Message.MensagemSucesso("Inserido com sucesso");
                            this.formularioGenerico.FillGrid();
                            this.Dispose();
                        }
                        break;
                    case Tarefa.VISUALIZANDO:
                        this.tarefa = Tarefa.EDITANDO;
                        this.IniciaForm();
                        break;
                }
            }
        }

        #endregion Eventos

        #region Construtores

        /// <summary>
        /// Construtor principal da classe
        /// </summary>
        /// <param name="formularioGenerico">Formulário genérico que chama essa tela</param>
        /// <param name="tabela">Tabela que está sendo feito a manutenção</param>
        /// <param name="campos">Campos da tabela</param>
        /// <param name="valores">Valores da linha atual</param>
        /// <param name="tarefa">Tarefa a ser feita na tela</param>
        public FO_FormularioCadastroGenerico(UC_FormularioGenerico formularioGenerico, Model.MD_Tabela tabela, List<Model.MD_Campos> campos, Model.Valores valores, Tarefa tarefa)
        {
            InitializeComponent();
            this.formularioGenerico = formularioGenerico;
            this.tabela = tabela;
            this.campos = campos;
            this.Valores = valores;
            this.tarefa = tarefa;
            IniciaForm();
        }

        #endregion Construtores

        #region Métodos

        /// <summary>
        /// Método que inicializa o formulário
        /// </summary>
        private void IniciaForm()
        {
            this.Text = this.tabela.DAO.Nome;

            if (this.tarefa == Tarefa.VISUALIZANDO)
            {
                this.btn_acao.Text = "Editar";
            }
            else if(this.tarefa == Tarefa.EDITANDO)
            {
                this.btn_acao.Text = "Atualizar";
            }
            else if (this.tarefa == Tarefa.INCLUINDO)
            {
                this.btn_acao.Text = "Incluir";
            }
            this.PreencheTelaCamposInformacoes();
        }

        /// <summary>
        /// Método que preenche a tela com os campos e as informações
        /// </summary>
        private void PreencheTelaCamposInformacoes()
        {
            int i = 0;
            this.panels.ForEach(panel => panel.Controls.Clear());
            this.pan_tot.Controls.Clear();

            this.panels.Clear();
            this.textBoxes.Clear();

            foreach (Model.MD_Campos campo in this.campos)
            {
                Panel tempPanel = new Panel();
                tempPanel.Dock = DockStyle.Top;
                tempPanel.Size = new Size(482, 49);
                tempPanel.BackColor = this.pan_tot.BackColor;
                this.panels.Add(tempPanel);

                Label labelTemp = new Label();
                labelTemp.Location = new Point(15, 15);
                labelTemp.Text = campo.DAO.Nome;
                this.labels.Add(labelTemp);

                TextBox textBoxTemp = new TextBox();
                textBoxTemp.Location = new Point(181, 12);
                textBoxTemp.Size = new Size(730, 27);
                textBoxTemp.Text = (this.tarefa == Tarefa.INCLUINDO ? string.Empty : this.Valores.valores[i]);
                textBoxTemp.Enabled = this.tarefa != Tarefa.VISUALIZANDO;
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
        private bool PreencheValores(out Model.Valores valoresAnteriores)
        {
            valoresAnteriores = new Model.Valores();

            foreach(string valor in this.Valores.valores)
            {
                valoresAnteriores.valores.Add(valor);
            }
            foreach (string campo in this.Valores.campos)
            {
                valoresAnteriores.campos.Add(campo);
            }

            this.Valores.valores.Clear();
            bool retorno = true;

            int i = 0;
            this.textBoxes.ForEach(tbx =>
            {
                if (TrataCampo(tbx.Text, campos[i], out var mensagem))
                {
                    this.Valores.valores.Add(tbx.Text);
                }
                else
                {
                    Visao.Message.MensagemAlerta("Erro: " + mensagem);
                    this.Valores.valores.Add(string.Empty);
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
        private bool TrataCampo(string tbx, Model.MD_Campos campo, out string mensagem)
        {
            mensagem = string.Empty;
            bool retorno = true;

            if (!string.IsNullOrEmpty(tbx))
            {
                switch (campo.DAO.TipoCampo.Nome)
                {
                    case "BIGINT":
                        retorno &= int.TryParse(tbx, out var i);
                        break;
                    case "INT":
                        retorno &= int.TryParse(tbx, out var i1);
                        break;
                    case "TINYINT":
                        retorno &= int.TryParse(tbx, out var i2);
                        break;
                    case "SMALLINT":
                        retorno &= int.TryParse(tbx, out var i3);
                        break;
                    case "DECIMAL":
                        retorno &= decimal.TryParse(tbx, out var i4);
                        break;
                    case "REAL":
                        retorno &= decimal.TryParse(tbx, out var i5);
                        break;
                    case "DATETIME":
                        retorno &= DateTime.TryParse(tbx, out var i6);
                        break;
                }
            }

            return retorno;
        }

        #endregion Métodos

    }
}
