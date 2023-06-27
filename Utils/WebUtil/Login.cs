using JSON;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace Util.WebUtil
{
    public static class Login
    {
        /// <summary>
        /// Método que valida o login do usuário
        /// </summary>
        /// <param name="login"></param>
        /// <param name="pass"></param>
        /// <returns></returns>
        public static async Task<bool> ValidaLoginAsync(string login, string pass)
        {
            JS_RetornoLogin retorno = new JS_RetornoLogin();

            try
            {
                var url = "https://apisunsale.azurewebsites.net/api/Token/crudforms";
                var json = JsonConvert.SerializeObject(new { userName = login, password = pass });


                var request = WebRequest.Create(url);
                request.Method = "POST";
                request.ContentType = "application/json";

                using (var streamWriter = new StreamWriter(request.GetRequestStream()))
                {
                    streamWriter.Write(json);
                }

                var response = request.GetResponse();
                using (var streamReader = new StreamReader(response.GetResponseStream()))
                {
                    string result = streamReader.ReadToEnd();
                    
                    retorno = JsonConvert.DeserializeObject<JS_RetornoLogin>(result);
                    Util.Global.usuarioLogado = new JS_Usuario()
                    {
                        ADMINISTRADOR = retorno.Admin,
                        CODIGO = retorno.Id,
                        DESENVOLVEDOR = retorno.Admin,
                        EMAIL = retorno.Username,
                        LASTVERSION = retorno.CrudVersao,
                        TOKEN = retorno.token
                    };
                }
            }
            catch (Exception e)
            {
                Util.CL_Files.LogException(e);
                return false;
            }

            return retorno != null;
        }
    }
}
