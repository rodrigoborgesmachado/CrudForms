using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    /// <summary>
    /// [CONSULTAS] Tabela da classe
    /// </summary>
    public class MD_Consultas 
    {
        #region Atributos e Propriedades

        /// <summary>
        /// DAO que representa a classe
        /// </summary>
        public DAO.MD_Consultas DAO = null;


        #endregion Atributos e Propriedades

        #region Construtores

        public MD_Consultas(int codigo)
        {
            Util.CL_Files.WriteOnTheLog("MD_Consultas()", Util.Global.TipoLog.DETALHADO);
            this.DAO = new DAO.MD_Consultas( codigo);
        }


        #endregion Construtores

        #region Métodos

        /// <summary>
        /// Método que busca todas as consultas
        /// </summary>
        /// <returns></returns>
        public static List<MD_Consultas> BuscaConsultas()
        {
            List<MD_Consultas> consultas = new List<MD_Consultas>();

            string sentenca = new DAO.MD_Consultas().table.CreateCommandSQLTable();
            DbDataReader reader = DataBase.Connection.Select(sentenca);

            while (reader.Read())
            {
                consultas.Add(new MD_Consultas(int.Parse(reader["CODIGO"].ToString())));
            }
            reader.Close();

            return consultas;
        }

        #endregion Métodos

    }
}
