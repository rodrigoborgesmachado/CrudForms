using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Regras
{
    public static class GerarCsv
    {
        /// <summary>
        /// Método que gera o arquivo CSV
        /// </summary>
        /// <param name="valores"></param>
        /// <param name="caminhoArquivo"></param>
        /// <param name="nomeArquivo"></param>
        /// <param name="mensagemErro"></param>
        /// <returns></returns>
        public static bool GerarArquivo(List<AcessoBancoCliente.AcessoBanco> valores, DirectoryInfo caminhoArquivo, string nomeArquivo, out string mensagemErro)
        {
            mensagemErro = string.Empty;
            bool retorno = true;

            if (!caminhoArquivo.Exists)
                caminhoArquivo.Create();

            if (string.IsNullOrEmpty(nomeArquivo))
                nomeArquivo = "relatorio.csv";

            if (!nomeArquivo.EndsWith(".csv"))
                nomeArquivo += ".csv";

            try
            {
                FileInfo file = new FileInfo(caminhoArquivo.FullName + nomeArquivo);
                if (file.Exists)
                    file.Delete();

                string textoCsv = MontaTextoCSV(valores);

                File.WriteAllText(file.FullName, textoCsv);

            }
            catch (Exception ex)
            {
                Util.CL_Files.WriteOnTheLog("Erro: " + ex.Message, Util.Global.TipoLog.SIMPLES);
                mensagemErro = "Erro: " + ex.Message;
                retorno = false;
            }

            return retorno;
        }

        /// <summary>
        /// Método que monta o texto do CSV
        /// </summary>
        /// <param name="valores"></param>
        /// <returns></returns>
        public static string MontaTextoCSV(List<AcessoBancoCliente.AcessoBanco> valores)
        {
            string retorno = string.Empty;

            int i = 0;
            Visao.BarraDeCarregamento barra = new Visao.BarraDeCarregamento(valores.Count, "Carregando arquivo");
            barra.Show();

            valores.ForEach(valor => 
            { 
                if(i == 0)
                {
                    int j = 0;
                    valor.campos.ForEach(campo => 
                    {
                        if(j != 0)
                        {
                            retorno += ";";
                        }

                        retorno += $"\"{campo}\"";
                        j++;
                    });
                    i++;
                    retorno += Environment.NewLine;
                }

                int a = 0;
                valor.valores.ForEach(result => 
                {
                    if (a != 0)
                    {
                        retorno += ";";
                    }

                    retorno += $"\"{result}\"";
                    a++;
                });
                retorno += Environment.NewLine;
                barra.AvancaBarra(1);
            });

            barra.Dispose();

            return retorno;
        }
    }
}
