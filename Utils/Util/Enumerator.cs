using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Util
{
    public class Enumerator
    {

        /// <summary>
        /// Enumerator do tipo do banco
        /// </summary>
        public enum BancoDados
        {
            SQL_SERVER = 0,
            SQLite = 1,
            ORACLE = 2,
            POSTGRESQL = 3,
            MYSQL = 4
        }

        /// <summary>
        /// Tarefa sendo executada na tela
        /// </summary>
        public enum Tarefa
        {
            INCLUINDO = 0,
            EDITANDO = 1,
            EXCLUINDO = 2,
            VISUALIZANDO = 3
        }

        /// <summary>
        /// Enumerator for type of data
        /// </summary>
        public enum DataType
        {
            DATE = 1,
            INT = 2,
            STRING = 3,
            CHAR = 4,
            DECIMAL
        }

        /// <summary>
        /// Enumerador para identificar o tipo do arquivo sendo exportado
        /// </summary>
        public enum TipoArquivoExportacao
        {
            JSON = 0,
            CSV = 1,
            XML = 2
        }

        /// <summary>
        /// Enumerator para controlar qual o tipo de manutenção será feito com o texto
        /// </summary>
        public enum TipoManutencaoTexto
        {
            IDENTAR_JSON = 0,
            IDENTAR_XML = 1,
            TRANSFORMAR_XML_TO_JSON = 2,
            TRANSFORMAR_JSON_TO_XML = 3
        }
    }
}
