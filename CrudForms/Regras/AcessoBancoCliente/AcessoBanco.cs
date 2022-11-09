using Model;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Regras.AcessoBancoCliente
{
    public abstract class AcessoBanco
    {
        public List<string> campos = new List<string>();
        public List<string> valores = new List<string>();

        /// <summary>
        /// Método que atualiza os valores no banco
        /// </summary>
        /// <param name="tabela"></param>
        /// <param name="campos"></param>
        /// <param name="mensagem"></param>
        /// <returns></returns>
        public bool DeleteValores(MD_Tabela tabela, List<MD_Campos> campos, out string mensagem)
        {
            bool retorno = true;
            string delete = MontaComandoDelete(tabela.DAO.Nome, campos, out mensagem);

            if (string.IsNullOrEmpty(delete))
            {
                return false;
            }

            string connection = Parametros.ConexaoBanco.DAO.Valor;

            DataBase.Connection.CloseConnection();
            if (!DataBase.Connection.OpenConection(connection, Util.Global.BancoDados))
            {
                mensagem = "Não foi possível conectar";
                retorno = false;
            }
            else
            {
                Util.CL_Files.WriteOnTheLog(delete, Util.Global.TipoLog.SIMPLES);
                retorno = DataBase.Connection.Delete(delete);
            }

            DataBase.Connection.CloseConnection();
            DataBase.Connection.OpenConection(Util.Global.app_base_file, Util.Enumerator.BancoDados.SQLite);

            return retorno;
        }

        /// <summary>
        /// Método que insere os valores no banco
        /// </summary>
        /// <param name="tabela"></param>
        /// <param name="campos"></param>
        /// <param name="mensagem"></param>
        /// <returns></returns>
        public bool InsereValores(Model.MD_Tabela tabela, List<Model.MD_Campos> campos, out string mensagem)
        {
            bool retorno = true;
            mensagem = string.Empty;

            string insert = MontaComandoInsert(tabela.DAO.Nome, campos, out mensagem);

            if (string.IsNullOrEmpty(insert))
            {
                return false;
            }

            string connection = Parametros.ConexaoBanco.DAO.Valor;

            DataBase.Connection.CloseConnection();
            if (!DataBase.Connection.OpenConection(connection, Util.Global.BancoDados))
            {
                mensagem = "Não foi possível conectar";
                retorno = false;
            }
            else
            {
                Util.CL_Files.WriteOnTheLog(insert, Util.Global.TipoLog.SIMPLES);
                retorno = DataBase.Connection.Insert(insert);
            }

            DataBase.Connection.CloseConnection();
            DataBase.Connection.OpenConection(Util.Global.app_base_file, Util.Enumerator.BancoDados.SQLite);

            return retorno;
        }

        /// <summary>
        /// Método que atualiza os valores no banco
        /// </summary>
        /// <param name="tabela"></param>
        /// <param name="campos"></param>
        /// <param name="mensagem"></param>
        /// <returns></returns>
        public bool AtualizaValores(Model.MD_Tabela tabela, List<Model.MD_Campos> campos, AcessoBanco valoresAnteriores, out string mensagem)
        {
            bool retorno = true;
            string update = MontaComandoUpdate(tabela.DAO.Nome, campos, valoresAnteriores, out mensagem);

            if (string.IsNullOrEmpty(update))
            {
                mensagem = "Não há campos a atualizar!";
                return false;
            }

            string connection = Parametros.ConexaoBanco.DAO.Valor;

            try
            {
                DataBase.Connection.CloseConnection();
                if (!DataBase.Connection.OpenConection(connection, Util.Global.BancoDados))
                {
                    mensagem = "Não foi possível conectar";
                    retorno = false;
                }
                else
                {
                    Util.CL_Files.WriteOnTheLog(update, Util.Global.TipoLog.SIMPLES);
                    retorno = DataBase.Connection.Update(update);
                }
            }
            finally
            {
                DataBase.Connection.CloseConnection();
                DataBase.Connection.OpenConection(Util.Global.app_base_file, Util.Enumerator.BancoDados.SQLite);
            }

            return retorno;
        }

        /// <summary>
        /// Método que preenche os valores a partir da tabela
        /// </summary>
        /// <param name="tabela"></param>
        /// <returns></returns>
        public List<AcessoBanco> BuscaLista(MD_Tabela tabela, List<MD_Campos> campos, Model.Filtro filtro, out string consulta)
        {
            List<AcessoBanco> valores = new List<AcessoBanco>();

            try
            {
                consulta = CreateCommandSQLTable(tabela, campos, filtro);

                Visao.BarraDeCarregamento barra = new Visao.BarraDeCarregamento(int.Parse(Model.Parametros.QuantidadeLinhasTabelas.DAO.Valor), "Buscando");
                barra.Show();

                string connection = Parametros.ConexaoBanco.DAO.Valor;

                DataBase.Connection.CloseConnection();
                DataBase.Connection.OpenConection(connection, Util.Global.BancoDados);

                DbDataReader reader = DataBase.Connection.Select(consulta);

                if (reader == null)
                {
                    barra.Dispose();
                    return null;
                }

                List<string> columns = new List<string>();
                campos.ForEach(campo =>
                {
                    columns.Add(campo.DAO.Nome);
                });

                while (reader.Read())
                {
                    List<string> values = new List<string>();

                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        var temp = reader[campos[i].DAO.Nome];

                        if (campos[i].DAO.TipoCampo.Nome.Equals("VARBINARY"))
                        {
                            byte[] binaryString = (byte[])temp;

                            // if the original encoding was UTF-8
                            string y = Encoding.UTF8.GetString(binaryString);
                            values.Add(y);
                        }
                        else
                        {
                            values.Add(temp.ToString());
                        }
                    }

                    AcessoBanco temp2;
                    if(Util.Global.BancoDados == Util.Enumerator.BancoDados.SQL_SERVER)
                    {
                        temp2 = new AcessoBancoSqlServer()
                        {
                            campos = columns,
                            valores = values
                        };
                    }
                    else{
                        temp2 = new AcessoBancoPostGres()
                        {
                            campos = columns,
                            valores = values
                        };
                    }
                        
                    valores.Add(
                        temp2
                    );
                    barra.AvancaBarra(1);
                }
                reader.Close();
                barra.Dispose();

                return valores;
            }
            finally
            {
                DataBase.Connection.CloseConnection();
                DataBase.Connection.OpenConection(Util.Global.app_base_file, Util.Enumerator.BancoDados.SQLite);
            }
        }

        /// <summary>
        /// Método que preenche os valores a partir da tabela
        /// </summary>
        /// <param name="tabela"></param>
        /// <returns></returns>
        public List<AcessoBanco> BuscaLista(string sentenca)
        {
            List<AcessoBanco> valores = new List<AcessoBanco>();

            try
            {
                string connection = Parametros.ConexaoBanco.DAO.Valor;

                Visao.BarraDeCarregamento barra = new Visao.BarraDeCarregamento(int.Parse(Model.Parametros.QuantidadeLinhasTabelas.DAO.Valor), "Buscando");
                barra.Show();

                DataBase.Connection.CloseConnection();
                DataBase.Connection.OpenConection(connection, Util.Global.BancoDados);

                DbDataReader reader = DataBase.Connection.Select(sentenca);

                List<string> columns = new List<string>();

                if (reader == null)
                {
                    barra.Dispose();
                    return null;
                }

                int j = 0;
                while (reader.Read())
                {
                    List<string> values = new List<string>();

                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        if (j == 0)
                            columns.Add(reader.GetName(i).ToString());

                        values.Add(reader[i].ToString());
                    }

                    AcessoBanco temp2;
                    if (Util.Global.BancoDados == Util.Enumerator.BancoDados.SQL_SERVER)
                    {
                        temp2 = new AcessoBancoSqlServer()
                        {
                            campos = columns,
                            valores = values
                        };
                    }
                    else
                    {
                        temp2 = new AcessoBancoPostGres()
                        {
                            campos = columns,
                            valores = values
                        };
                    }

                    valores.Add(temp2);
                    barra.AvancaBarra(1);
                    j++;
                }
                reader.Close();
                barra.Dispose();
            }
            finally
            {
                DataBase.Connection.CloseConnection();
                DataBase.Connection.OpenConection(Util.Global.app_base_file, Util.Enumerator.BancoDados.SQLite);
            }

            return valores;
        }

        protected abstract string MontaComandoDelete(string tabela, List<Model.MD_Campos> campos, out string mensagem);

        protected abstract string MontaComandoInsert(string tabela, List<Model.MD_Campos> campos, out string mensagem);

        protected abstract string MontaComandoUpdate(string tabela, List<Model.MD_Campos> campos, AcessoBanco valoresAnteriores, out string mensagem);

        protected abstract string CreateCommandSQLTable(MD_Tabela tabela, List<MD_Campos> campos, Filtro filtro);

        protected abstract string MontaWhereSelect(Model.Filtro filtro, List<MD_Campos> campos);

        protected abstract string MontaCampoWhereSelect(Model.MD_Campos campo, string valor);

        protected abstract string MontaCampoWhereUpdateDelete(Model.MD_Campos campo, string valor);

        protected abstract string MontaCampoUpdate(Model.MD_Campos campo, string valor);

        protected abstract string MontaCampoInsert(Model.MD_Campos campo, string valor);

        protected abstract string MontaStringDateTimeFromDateTime(DateTime data);

    }
}
