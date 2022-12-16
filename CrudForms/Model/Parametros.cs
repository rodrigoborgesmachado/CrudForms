using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public static class Parametros
    {
        private static Model.MD_Parametros conexaoBanco;
        public static Model.MD_Parametros ConexaoBanco 
        {
            get
            {
                if (conexaoBanco == null) conexaoBanco = new Model.MD_Parametros(Util.Global.parametro_connectionStrings);
                return conexaoBanco;
            }
            set
            {
                conexaoBanco = value;
            }
        }

        private static Model.MD_Parametros nomeConexao;
        public static Model.MD_Parametros NomeConexao
        {
            get
            {
                if (nomeConexao == null) nomeConexao = new Model.MD_Parametros(Util.Global.parametro_connectionName);
                return nomeConexao;
            }
            set
            {
                nomeConexao = value;
            }
        }

        private static Model.MD_Parametros quantidadeLinhasTabelas;
        public static Model.MD_Parametros QuantidadeLinhasTabelas
        {
            get
            {
                if (quantidadeLinhasTabelas == null) quantidadeLinhasTabelas = new Model.MD_Parametros(Util.Global.parametro_quantidadeItensPorTabela);
                return quantidadeLinhasTabelas;
            }
            set
            {
                quantidadeLinhasTabelas = value;
            }
        }

        private static Model.MD_Parametros filtrarAutomaticamente;
        public static bool FiltrarAutomaticamente
        {
            get
            {
                if (filtrarAutomaticamente == null) filtrarAutomaticamente = new Model.MD_Parametros(Util.Global.parametro_filtrarAutomaticamente);
                return filtrarAutomaticamente.DAO.Valor.Equals("1");
            }
            set
            {
                filtrarAutomaticamente = new MD_Parametros(Util.Global.parametro_filtrarAutomaticamente);
                filtrarAutomaticamente.DAO.Valor = (value ? "1" : "0");
                
                if(filtrarAutomaticamente.DAO.Empty)
                    filtrarAutomaticamente.DAO.Insert();
                else
                    filtrarAutomaticamente.DAO.Update();
            }
        }

        private static Model.MD_Parametros tipoBanco;
        public static Model.MD_Parametros TipoBanco
        {
            get
            {
                if (tipoBanco == null) tipoBanco = new Model.MD_Parametros(Util.Global.parametro_tipoBanco);
                return tipoBanco;
            }
            set
            {
                tipoBanco = new MD_Parametros(Util.Global.parametro_tipoBanco);
                tipoBanco.DAO.Valor = value.DAO.Valor;

                if (tipoBanco.DAO.Empty)
                    tipoBanco.DAO.Insert();
                else
                    tipoBanco.DAO.Update();
            }
        }

        private static Model.MD_Parametros numeracaoLinhasTabelas;
        public static bool NumeracaoLinhasTabelas
        {
            get
            {
                if (numeracaoLinhasTabelas == null)
                {
                    numeracaoLinhasTabelas = new Model.MD_Parametros(Util.Global.parametro_filtrarAutomaticamente);
                    numeracaoLinhasTabelas.DAO.Valor = "1";
                }
                return numeracaoLinhasTabelas.DAO.Valor.Equals("1");
            }
            set
            {
                numeracaoLinhasTabelas = new MD_Parametros(Util.Global.parametro_filtrarAutomaticamente);
                numeracaoLinhasTabelas.DAO.Valor = (value ? "1" : "0");

                if (numeracaoLinhasTabelas.DAO.Empty)
                    numeracaoLinhasTabelas.DAO.Insert();
                else
                    numeracaoLinhasTabelas.DAO.Update();
            }
        }
    }
}
