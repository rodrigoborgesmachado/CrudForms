using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Visao
{
    public partial class FO_SelecionaConexao : Form
    {
        public FO_SelecionaConexao()
        {
            InitializeComponent();
            this.IniciaForm();
            this.cbm_bancoDados.SelectedIndex = (int)Util.Global.BancoDados;
        }

        /// <summary>
        /// Método que inicializa o formulário
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
            this.grb_configuracaoSQLSERVER.ForeColor = this.ForeColor;

            this.tbx_connectionStrings.Text = Model.Parametros.ConexaoBanco.DAO.Valor;
            this.tbx_nome.Text = Model.Parametros.NomeConexao.DAO.Valor;
        }

        /// <summary>
        /// Evento lançado no clique do botão de confirmar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_confirmar_Click(object sender, EventArgs e)
        {
            string connections = this.tbx_connectionStrings.Text;

            if (this.cbm_bancoDados.SelectedIndex == -1)
            {
                Visao.Message.MensagemAlerta("Selecione o tipo de banco!");
                return;
            }

            if (string.IsNullOrEmpty(connections))
            {
                Visao.Message.MensagemAlerta("Preencha o campo de conexão!");
                return;
            }

            string sentenca = "DELETE FROM " + new DAO.MD_Tabela().table.Table_Name;
            DataBase.Connection.Delete(sentenca);

            sentenca = "DELETE FROM " + new DAO.MD_Relacao().table.Table_Name;
            DataBase.Connection.Delete(sentenca);

            sentenca = "DELETE FROM " + new DAO.MD_Campos().table.Table_Name;
            DataBase.Connection.Delete(sentenca);

            DataBase.Connection.CloseConnection();
            Util.Global.BancoDados = (Util.Enumerator.BancoDados)this.cbm_bancoDados.SelectedIndex;

            bool abriuConexao = DataBase.Connection.OpenConection(connections, Util.Global.BancoDados, this.tbx_nome.Text);
            if (!abriuConexao)
            {
                Visao.Message.MensagemAlerta("Não foi possível abrir conexão com os dados fornecidos!");
            }

            DataBase.Connection.CloseConnection();
            DataBase.Connection.OpenConection(Util.Global.app_base_file, Util.Enumerator.BancoDados.SQLite);

            if (abriuConexao)
            {
                Model.MD_Parametros parametro = new Model.MD_Parametros(Util.Global.parametro_connectionStrings);
                parametro.DAO.Valor = connections;

                if (parametro.DAO.Update())
                {
                    Model.Parametros.ConexaoBanco = parametro;
                    parametro = new Model.MD_Parametros(Util.Global.parametro_connectionName);
                    parametro.DAO.Valor = tbx_nome.Text;
                    if (parametro.DAO.Update())
                    {
                        Model.Parametros.NomeConexao = parametro;
                        
                        parametro = new Model.MD_Parametros(Util.Global.parametro_tipoBanco);
                        parametro.DAO.Valor = ((int)Util.Global.BancoDados).ToString();
                        parametro.DAO.Update();
                        Model.Parametros.TipoBanco = parametro;

                        Util.Global.connectionName = Model.Parametros.NomeConexao.DAO.Valor;
                        
                        Visao.Message.MensagemSucesso("Atualizado com sucesso, foi possível conectar");

                        this.DialogResult = DialogResult.OK;
                        this.Dispose();
                    }
                    else
                    {
                        Visao.Message.MensagemErro("Erro ao atualizar;");
                    }
                }
                else
                {
                    Visao.Message.MensagemErro("Erro ao atualizar;");
                }
            }
        }

        /// <summary>
        /// Evento lançado no clique da opção de informação sobre a conexão com o banco de dados
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_info_servidorSqlServer_Click(object sender, EventArgs e)
        {
            Message.MensagemInformacao("Essa conexão deve ser válida, deve conter o Data Source da conexão com o Banco de Dados!");
        }
    }
}
