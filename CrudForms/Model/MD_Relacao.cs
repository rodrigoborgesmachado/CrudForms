using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    /// <summary>
    /// [RELACAO] Tabela da classe
    /// </summary>
    public class MD_Relacao 
    {
        #region Atributos e Propriedades

        /// <summary>
        /// DAO que representa a classe
        /// </summary>
        public DAO.MD_Relacao DAO = null;

        #endregion Atributos e Propriedades

        #region Construtores
        
        /// <summary>
        /// Construtor secundário da classe
        /// </summary>
        /// <param name="codigo"></param>
        /// <param name="projeto"></param>
        /// <param name="tabelaOrigem"></param>
        /// <param name="tabelaDestino"></param>
        /// <param name="campoOrigem"></param>
        /// <param name="campoDestino"></param>
        public MD_Relacao(int codigo, int projeto, DAO.MD_Tabela tabelaOrigem, DAO.MD_Tabela tabelaDestino, DAO.MD_Campos campoOrigem, DAO.MD_Campos campoDestino) 
        {
            Util.CL_Files.WriteOnTheLog("MD_Relacao()", Util.Global.TipoLog.DETALHADO);

            this.DAO = new DAO.MD_Relacao(codigo, projeto, tabelaOrigem, tabelaDestino, campoOrigem, campoDestino);
        }

        #endregion Construtores

        #region Métodos
        
        #endregion Métodos

    }
}
