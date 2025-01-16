using Model;
using System;
using System.Collections.Generic;

namespace Regras.ApiClasses
{
    public static class CreatorApiClasses
    {
        public static bool CreateApiClasses(List<MD_Tabela> tabelas, string projetoNome, out string message)
        {
            bool result = true;
            message = string.Empty;
            Visao.BarraDeCarregamento barra = new Visao.BarraDeCarregamento(tabelas.Count, "Criando classes");
            barra.Show();

            try
            {

                foreach(var tabela in tabelas)
                {
                    result &= EntidadesCreator.CreateEntidades(tabela.DAO, projetoNome);

                    result &= RepositoryCreator.CreateRepository(tabela.DAO, projetoNome);

                    result &= ProfilesCreator.CreateProfiles(tabela.DAO, projetoNome);

                    result &= ServiceCreator.CreateService(tabela.DAO, projetoNome);

                    result &= ControllerCreator.CreateController(tabela.DAO, projetoNome);
                    barra.AvancaBarra(1);
                }

            }
            catch (Exception ex)
            {
                result = false;
                Util.CL_Files.LogException(ex);
                message = ex.Message; 
            }
            finally
            {
                barra.Dispose();
            }

            return result;
        }
    }
}
