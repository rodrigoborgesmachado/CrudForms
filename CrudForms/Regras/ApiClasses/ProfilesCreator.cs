using DAO;
using System.Text;
using System;
using System.IO;

namespace Regras.ApiClasses
{
    public static class ProfilesCreator
    {
        public static bool CreateProfiles(MD_Tabela tabela, string nomeProjeto)
        {
            bool result = true;

            result &= Cria(tabela, nomeProjeto, NamesHandler.ClasseType.ProfilesDTO);
            result &= Cria(tabela, nomeProjeto, NamesHandler.ClasseType.ProfilesViewModel);

            return result;
        }

        private static bool Cria(MD_Tabela tabela, string nomeProjeto, NamesHandler.ClasseType type)
        {
            bool result = true;

            try
            {
                string classeName = NamesHandler.CriaNomeClasse(type, tabela.Nome);
                string caminhoFile = NamesHandler.GetDirectoryByType(type) + NamesHandler.CriaNomeArquivoClasse(type, tabela.Nome);
                Util.CL_Files.DeleteFilesIfExists(caminhoFile);

                StringBuilder stringBuilder = new StringBuilder();
                if(type == NamesHandler.ClasseType.ProfilesDTO)
                {
                    stringBuilder.AppendLine($"using Main = {nomeProjeto}.Domain.Entities.{NamesHandler.CriaNomeClasse(NamesHandler.ClasseType.Entity, tabela.Nome)};");
                    stringBuilder.AppendLine($"using MainDto = {nomeProjeto}.Application.DTO.{NamesHandler.CriaNomeClasse(NamesHandler.ClasseType.Dto, tabela.Nome)};");
                }
                else
                {
                    stringBuilder.AppendLine($"using MainDto = {nomeProjeto}.Application.DTO.{NamesHandler.CriaNomeClasse(NamesHandler.ClasseType.Dto, tabela.Nome)};");
                    stringBuilder.AppendLine($"using MainViewModel = {nomeProjeto}.Presentation.Model.ViewModels.{NamesHandler.CriaNomeClasse(NamesHandler.ClasseType.ViewModel, tabela.Nome)};");
                }
                stringBuilder.AppendLine();
                stringBuilder.AppendLine($"{NamesHandler.GetNamespaceByType(nomeProjeto, type)}");
                stringBuilder.AppendLine($"{{");
                stringBuilder.AppendLine($"    public class {classeName} : AutoMapper.Profile");
                stringBuilder.AppendLine($"    {{");
                stringBuilder.AppendLine($"        public {classeName}()");
                stringBuilder.AppendLine($"        {{");
                if (type == NamesHandler.ClasseType.ProfilesDTO)
                {
                    stringBuilder.AppendLine($"            CreateMap<Main, MainDto>().PreserveReferences();");
                    stringBuilder.AppendLine($"            CreateMap<MainDto, Main>().PreserveReferences();");
                }
                else
                {
                    stringBuilder.AppendLine($"            CreateMap<MainDto, MainViewModel>().PreserveReferences();");
                    stringBuilder.AppendLine($"            CreateMap<MainViewModel, MainDto>().PreserveReferences();");
                }
                stringBuilder.AppendLine($"        }}");
                stringBuilder.AppendLine($"    }}");
                stringBuilder.AppendLine($"}}");

                File.WriteAllText(caminhoFile, stringBuilder.ToString());
            }
            catch (Exception ex)
            {
                Util.CL_Files.LogException(ex);
                result = false;
            }

            return result;
        }
    }
}
