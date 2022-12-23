using System;
using System.Collections.Generic;
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

        #endregion Métodos

    }
}
