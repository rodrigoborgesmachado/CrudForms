using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CrudForms
{
    static class Program
    {
        /// <summary>
        /// Ponto de entrada principal para o aplicativo.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            DataBase.Connection.OpenConection(Util.Global.app_base_file, Util.Enumerator.BancoDados.SQLite);

            Util.CL_Files.CreateMainDirectories();
            Util.CL_Files.WriteOnTheLog("--------------------------------------Iniciando sistema", Util.Global.TipoLog.SIMPLES);

            Util.Global.InsereDadosIniciais();
            Util.Global.log_system = DataBase.Connection.GetLog();
            Util.Global.CarregarAutomaticamente = DataBase.Connection.GetAutomatico();
            Util.Global.ApresentaInformacao = DataBase.Connection.GetApresentaInformacao();
            Util.Global.BancoDados = (Util.Enumerator.BancoDados)int.Parse(string.IsNullOrEmpty(Model.Parametros.TipoBanco.DAO.Valor) ? "0" : Model.Parametros.TipoBanco.DAO.Valor);
            Util.Global.connectionName = Model.Parametros.NomeConexao.DAO.Valor;

            // Chamadas das classes modelo para criação das tabelas
            DAO.MD_Campos campos = new DAO.MD_Campos();
            DAO.MD_Parametros param = new DAO.MD_Parametros();
            DAO.MD_Relacao rel = new DAO.MD_Relacao();
            DAO.MD_Tabela tab = new DAO.MD_Tabela();
            DAO.MD_TipoCampo tipo = new DAO.MD_TipoCampo();

            bool logar = false;
            if (args.Length == 0)
            {
                Visao.FO_Login login = new Visao.FO_Login();
                if (login.ShowDialog() == DialogResult.OK)
                {
                    logar = true;
                }
            }
            else if(args.Length == 1)
            {
                Visao.Message.MensagemInformacao("Deve ser preenchido 2 argumentos: usuário senha");
            }
            else
            {
                string user = args[0];
                string senha = args[1];
                senha = Hash(senha).ToString();

                var taask = Util.WebUtil.Login.ValidaLoginAsync(user, senha);
                while (!taask.IsCompleted);

                if (taask.Result)
                    logar = true;
                else
                    Visao.Message.MensagemAlerta("Usuário ou senha incorretos");
            }

            if (logar)
                Application.Run(new Visao.FO_Principal());

            DataBase.Connection.CloseConnection();
            Util.CL_Files.WriteOnTheLog("--------------------------------------Finalizando sistema", Util.Global.TipoLog.SIMPLES);
        }

        /// <summary>
        /// Método que calcula o hash de uma string
        /// </summary>
        /// <param name="texto"></param>
        /// <returns></returns>
        private static long Hash(string texto)
        {
            long hash = 0;

            if (texto.Length == 0) return hash;

            for (int i = 0; i < texto.Length; i++)
            {
                int t = (int)texto[i];
                hash = ((hash << 5) - hash) + t;
                hash = hash & hash;
            }

            return hash;
        }
    }
}
