using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    /// <summary>
    /// [OBSERVER] Tabela da classe
    /// </summary>
    public class MD_Observer 
    {
        #region Atributos e Propriedades

        /// <summary>
        /// DAO que representa a classe
        /// </summary>
        public DAO.MD_Observer DAO = null;


        #endregion Atributos e Propriedades

        #region Construtores

        public MD_Observer(int codigo)
        {
            Util.CL_Files.WriteOnTheLog("MD_Observer()", Util.Global.TipoLog.DETALHADO);
            this.DAO = new DAO.MD_Observer( codigo);
        }


        #endregion Construtores

        #region Métodos

        /// <summary>
        /// Método que busca todos os observers
        /// </summary>
        /// <returns></returns>
        public static List<MD_Observer> BuscaTodos()
        {
            List<int> codigos = new List<int>();
            List<MD_Observer> lista = new List<MD_Observer>();

            DbDataReader reader = DataBase.Connection.Select(new DAO.MD_Observer().table.CreateCommandSQLTable());
            while (reader.Read())
            {
                codigos.Add(int.Parse(reader["CODIGO"].ToString()));
            }

            codigos.ForEach(c => lista.Add(new MD_Observer(c)));

            return lista;
        }

        #endregion Métodos

    }
}
