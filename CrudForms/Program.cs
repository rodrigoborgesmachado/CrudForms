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
        static void Main()
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

            Visao.FO_Login login = new Visao.FO_Login();
            if (login.ShowDialog() == DialogResult.Cancel)
            {
                return;
            }
            Application.Run(new Visao.FO_Principal());

            DataBase.Connection.CloseConnection();
            Util.CL_Files.WriteOnTheLog("--------------------------------------Finalizando sistema", Util.Global.TipoLog.SIMPLES);
        }
    }
}
