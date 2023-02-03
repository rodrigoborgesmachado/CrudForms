using Model;
using System;
using System.Windows.Forms;
using static Util.Enumerator;

namespace Visao
{
    public partial class FO_CadastraObserver : Form
    {
        #region Atribuitos e Propriedades

        Tarefa tarefa;
        MD_Observer model;
        FO_Observer tela;
        FO_Principal principal;

        #endregion Atribuitos e Propriedades

        #region Eventos

        /// <summary>
        /// Evento lançado no clique do botão executar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_executar_Click(object sender, EventArgs e)
        {
            this.Inserir();
        }

        #endregion Eventos

        #region Construtores

        /// <summary>
        /// Construtor principal da classe
        /// </summary>
        /// <param name="tela"></param>
        /// <param name="observer"></param>
        /// <param name="tarefa"></param>
        public FO_CadastraObserver(FO_Principal principal, FO_Observer tela, MD_Observer observer, string nome, string query)
        {
            InitializeComponent();
            this.principal = principal;
            this.tela = tela;
            this.model = observer;
            this.tarefa = Tarefa.INCLUINDO;
            this.tbx_descricao.Text = nome;
            this.tbx_consulta.Text = query;
            this.IniciaForm();
        }

        /// <summary>
        /// Construtor principal da classe
        /// </summary>
        /// <param name="tela"></param>
        /// <param name="observer"></param>
        /// <param name="tarefa"></param>
        public FO_CadastraObserver(FO_Principal principal, FO_Observer tela, MD_Observer observer, Tarefa tarefa)
        {
            InitializeComponent();
            this.principal = principal;
            this.tela = tela;
            this.model = observer;
            this.tarefa = tarefa;
            this.IniciaForm();
        }

        #endregion Construtores

        #region Métodos

        /// <summary>
        /// Método que inicializa o formulário
        /// </summary>
        public void IniciaForm()
        {
            if (this.tarefa == Tarefa.EDITANDO)
            {
                this.tbx_descricao.Text = this.model.DAO.Descricao;
                this.tbx_consulta.Text = this.model.DAO.Consulta;
                this.tbx_emails.Text = this.model.DAO.Emailsenviar;
                this.tbx_intervalo.Text = this.model.DAO.Intervalorodar.ToString();
                this.btn_executar.Text = "Atualizar";
            }
            else
            {
                this.model = new MD_Observer(DataBase.Connection.GetIncrement(new DAO.MD_Observer().table.Table_Name));
                this.tbx_emails.Text = Util.Global.usuarioLogado.EMAIL + ";";
                this.btn_executar.Text = "Incluir";
                this.tbx_intervalo.Text = "5";
            }
        }

        /// <summary>
        /// Método que insere ou atualiza
        /// </summary>
        private void Inserir()
        {
            string query = this.tbx_consulta.Text;
            if (string.IsNullOrEmpty(this.tbx_descricao.Text))
            {
                Message.MensagemAlerta("Descrição não está preenchida");
            }
            else if (string.IsNullOrEmpty(query))
            {
                Message.MensagemAlerta("Consulta não está preenchida");
            }
            else if (string.IsNullOrEmpty(this.tbx_emails.Text))
            {
                Message.MensagemAlerta("Emails não está preenchida");
            }
            else if (!int.TryParse(this.tbx_intervalo.Text, out int intervalo))
            {
                Message.MensagemAlerta("Intervalo incorreto");
            }
            else if(!Regras.ValidaQuery.Valida(query))
            {
                Message.MensagemAlerta("Consulta inválida");
            }
            else if(intervalo < 5)
            {
                Message.MensagemAlerta("Intervalo mínimo de 5 minutos!");
            }
            else
            {
                model.DAO.Descricao = this.tbx_descricao.Text;
                model.DAO.Consulta = query;
                if(tarefa == Tarefa.INCLUINDO) model.DAO.Created = DateTime.Now;
                model.DAO.Updated = DateTime.Now;
                model.DAO.Isactive = "1";
                model.DAO.Emailsenviar = this.tbx_emails.Text;
                model.DAO.Intervalorodar = intervalo;

                bool retorno = tarefa == Tarefa.INCLUINDO ? model.DAO.Insert() : model.DAO.Update();

                if (retorno)
                {
                    Message.MensagemSucesso((tarefa == Tarefa.INCLUINDO ? "Inclído" : "Alterado") + $" com sucesso\n. O processo será executado a cada {model.DAO.Intervalorodar} minutos");
                    this.tela.IniciaForm();
                    this.principal.CarregaObservers();
                    this.Dispose();
                }
                else
                {
                    Message.MensagemErro("Erro ao cadastrar");
                }
            }
        }

        #endregion Métodos

    }
}
