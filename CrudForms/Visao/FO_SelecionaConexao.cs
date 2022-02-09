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
    public partial class FO_SelecionaConexao : Form
    {
        public FO_SelecionaConexao()
        {
            InitializeComponent();
            this.IniciaForm();
        }

        /// <summary>
        /// Método que inicializa o formulário
        /// </summary>
        public void IniciaForm()
        {
            this.tbx_connectionStrings.Text = Model.Parametros.ConexaoBanco.DAO.Valor;
        }

        /// <summary>
        /// Evento lançado no clique do botão de confirmar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_confirmar_Click(object sender, EventArgs e)
        {
            string connections = this.tbx_connectionStrings.Text;

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
            bool abriuConexao = DataBase.Connection.OpenConection(connections, Util.Enumerator.BancoDados.SQL_SERVER);
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
                    Visao.Message.MensagemSucesso("Atualizado com sucesso, foi possível conectar");
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
