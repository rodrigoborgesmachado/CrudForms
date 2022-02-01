using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    /// <summary>
    /// [PARAMETROS] Tabela de parâmetros do sistema
    /// </summary>
    public class MD_Parametros 
    {
        #region Atributos e Propriedades

        /// <summary>
        /// DAO que representa a classe
        /// </summary>
        public DAO.MD_Parametros DAO = null;

        #endregion Atributos e Propriedades

        #region Construtores

        /// <summary>
        /// Construtor principal da classe
        /// </summary>
        /// <param name="chave">Chave do parâmetro</param>
        public MD_Parametros(string chave)
        {
            Util.CL_Files.WriteOnTheLog("MD_Parametros()", Util.Global.TipoLog.DETALHADO);

            this.DAO = new DAO.MD_Parametros(chave);
        }

        #endregion Construtores
    }
}
