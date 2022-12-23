using System;
using System.Collections.Generic;
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

        #endregion Métodos

    }
}
