using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Util
{
    public static class Funcoes
    {

        /// <summary>
        /// Método que trata o CNPJ
        /// </summary>
        /// <param name="cnpj"></param>
        /// <returns></returns>
        public static string TrataCNPJ(string cnpj)
        {
            return cnpj.Replace(".", "").Replace("/", "").Replace("\\", "").Replace("-", "");
        }

    }
}
