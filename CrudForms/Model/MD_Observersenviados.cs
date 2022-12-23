using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    /// <summary>
    /// [OBSERVERSENVIADOS] Tabela da classe
    /// </summary>
    public class MD_Observersenviados 
    {
        #region Atributos e Propriedades

        /// <summary>
        /// DAO que representa a classe
        /// </summary>
        public DAO.MD_Observersenviados DAO = null;


        #endregion Atributos e Propriedades

        #region Construtores

        public MD_Observersenviados(int codigo)
        {
            Util.CL_Files.WriteOnTheLog("MD_Observersenviados()", Util.Global.TipoLog.DETALHADO);
            this.DAO = new DAO.MD_Observersenviados( codigo);
        }


        #endregion Construtores

        #region Métodos

        /// <summary>
        /// Método que busca os observers já enviado
        /// </summary>
        /// <param name="codigoObserver"></param>
        /// <returns></returns>
        public static List<MD_Observersenviados> BuscaLista(int codigoObserver = -1)
        {
            List<int> codigos = new List<int>();

            DbDataReader reader = DataBase.Connection.Select(new DAO.MD_Observersenviados().table.CreateCommandSQLTable() + (codigoObserver > -1 ? $" where CODIGOOBSERVER = {codigoObserver}" : string.Empty));

            while (reader.Read())
            {
                codigos.Add(int.Parse(reader["CODIGO"].ToString()));
            }
            reader.Close();
            List<MD_Observersenviados> lista = new List<MD_Observersenviados>();

            codigos.ForEach(c => lista.Add(new MD_Observersenviados(c)));

            return lista;
        }

        #endregion Métodos

    }
}
