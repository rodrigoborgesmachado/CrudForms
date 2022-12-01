using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft;
using Newtonsoft.Json.Linq;

namespace Regras
{
    public static class IdentJsonString
    {
        /// <summary>
        /// Método que identa o json
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        public static string IdentaJson(string json, ref string mensageErro)
        {
            mensageErro = string.Empty;

            try
            {
                string newJson = JValue.Parse(json).ToString(Newtonsoft.Json.Formatting.Indented);

                return newJson;
            }
            catch(Exception ex)
            {
                mensageErro = ex.Message;
            }

            return string.Empty;
        }
    }
}
