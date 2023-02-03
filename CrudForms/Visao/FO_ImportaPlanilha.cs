using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Visao
{
    public partial class FO_ImportaPlanilha : Form
    {
        #region Atributos e Propriedades

        FO_Principal principal;

        #endregion Atributos e Propriedades

        #region Eventos

        /// <summary>
        /// Evento lançado no clique da opção de selecionar o arquivo
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_select_file_Click(object sender, EventArgs e)
        {
            this.SelecionaArquivo();
        }

        /// <summary>
        /// Evento lançado no clique da opção confirma
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_confirmar_Click(object sender, EventArgs e)
        {
            this.ImportaArquvio();
        }

        #endregion Eventos

        #region Construtores

        public FO_ImportaPlanilha(FO_Principal principal)
        {
            InitializeComponent();
            this.principal = principal;
        }

        #endregion Construtores

        #region Métodos

        /// <summary>
        /// Método que abre o File Dialog para selecionar a planilha
        /// </summary>
        private void SelecionaArquivo()
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.DefaultExt = ".csv";
            dialog.AddExtension = true;
            dialog.Title = "Selecione a planilha csv";

            if(dialog.ShowDialog() == DialogResult.OK)
            {
                this.tbx_caminhaPlanilha.Text = dialog.FileName;
            }
        }

        /// <summary>
        /// Método que importa o arquivo
        /// </summary>
        private void ImportaArquvio()
        {
            if (string.IsNullOrEmpty(this.tbx_caminhaPlanilha.Text))
            {
                Message.MensagemAlerta("Seelcione um arquivo");
            }
            else if (string.IsNullOrEmpty(this.tbx_nome_tabela.Text))
            {
                Message.MensagemAlerta("Preencha o nome da tabela que será criada a partir da planilha");
            }
            else if (!File.Exists(this.tbx_caminhaPlanilha.Text))
            {
                Message.MensagemAlerta("Arquivo selecionado incorreto");
            }
            else
            {
                var temp = new Regras.ImportarPlanilhaCsv();
                if(!temp.CriarTabelaPlanilha(this.tbx_nome_tabela.Text.Replace(" ", "_"), new FileInfo(this.tbx_caminhaPlanilha.Text), out var mensagem))
                {
                    Message.MensagemAlerta("Houve erros para importar");
                }
                else
                {
                    Message.MensagemSucesso("Importado com sucesso");
                }
                this.principal.CarregaTreeViewAutomaticamente();
            }
        }

        #endregion Métodos

    }
}
