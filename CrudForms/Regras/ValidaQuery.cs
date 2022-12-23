using Model;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Regras
{
    public static class ValidaQuery
    {
        /// <summary>
        /// Método que valida se a query é válida
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public static bool Valida(string query)
        {
            bool retorno = true;

            try
            {
                string connection = Parametros.ConexaoBanco.DAO.Valor;

                DataBase.Connection.CloseConnection();
                DataBase.Connection.OpenConection(connection, Util.Global.BancoDados);

                Visao.BarraDeCarregamento barra = new Visao.BarraDeCarregamento(1, "Validando query");
                barra.Show();

                DbDataReader reader = DataBase.Connection.Select(query);

                barra.Close();
                if(reader == null)
                {
                    retorno = false;
                }
                else
                {
                    reader.Read();
                    reader.Close();
                }
            }
            catch(Exception e)
            {
                Util.CL_Files.LogException(e);
                retorno = false;
            }
            finally
            {
                DataBase.Connection.CloseConnection();
                DataBase.Connection.OpenConection(Util.Global.app_base_file, Util.Enumerator.BancoDados.SQLite);
            }

            return retorno;
        }
    }
}
