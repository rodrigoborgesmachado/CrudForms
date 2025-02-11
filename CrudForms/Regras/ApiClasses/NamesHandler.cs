using System;

namespace Regras.ApiClasses
{
    public static class NamesHandler
    {
        public enum ClasseType
        {
            Entity,
            Dto,
            ViewModel,
            InterfaceRepository,
            Repository,
            InterfaceService,
            Service, 
            Controller,
            ProfilesDTO,
            ProfilesViewModel
        }

        public static string GetNamespaceByType(string projectName, NamesHandler.ClasseType type)
        {
            string retorno = string.Empty;

            switch (type)
            {
                case NamesHandler.ClasseType.Dto:
                    retorno = $"namespace {projectName}.Application.DTO";
                    break;
                case NamesHandler.ClasseType.ViewModel:
                    retorno = $"namespace {projectName}.Presentation.Model.ViewModels";
                    break;
                case NamesHandler.ClasseType.Entity:
                    retorno = $"namespace {projectName}.Domain.Entities";
                    break;
                case NamesHandler.ClasseType.InterfaceRepository:
                    retorno = $"namespace {projectName}.Domain.Interfaces.Repository";
                    break;
                case NamesHandler.ClasseType.Repository:
                    retorno = $"namespace {projectName}.Infrastructure.Data.Repository";
                    break;
                case NamesHandler.ClasseType.InterfaceService:
                    retorno = $"namespace {projectName}.Application.Interfaces";
                    break;
                case NamesHandler.ClasseType.Service:
                    retorno = $"namespace {projectName}.Application.Services";
                    break;
                case NamesHandler.ClasseType.ProfilesDTO:
                    retorno = $"namespace {projectName}.Application.Profiles";
                    break;
                case NamesHandler.ClasseType.ProfilesViewModel:
                    retorno = $"namespace {projectName}.Presentation.Model.ViewModels";
                    break;
                case NamesHandler.ClasseType.Controller:
                    retorno = $"namespace {projectName}.Presentation.Controllers";
                    break;
                default:
                    throw new ArgumentException($"Unsupported FileType: {type}");
            }

            return retorno;
        }

        public static string CriaNomeArquivoClasse(ClasseType classeType, string tableName)
        {
            return $"{CriaNomeClasse(classeType, tableName)}.cs";
        }

        public static string GetDirectoryByType(NamesHandler.ClasseType type)
        {
            string retorno = string.Empty;

            switch (type)
            {
                case NamesHandler.ClasseType.Dto:
                    retorno = Util.Global.app_classes_dto_directory;
                    break;
                case NamesHandler.ClasseType.ViewModel:
                    retorno = Util.Global.app_classes_viewModel_directory;
                    break;
                case NamesHandler.ClasseType.Entity:
                    retorno = Util.Global.app_classes_entities_directory;
                    break;
                case NamesHandler.ClasseType.InterfaceRepository:
                case NamesHandler.ClasseType.Repository:
                    retorno = Util.Global.app_classes_repository_directory;
                    break;
                case NamesHandler.ClasseType.InterfaceService:
                case NamesHandler.ClasseType.Service:
                    retorno = Util.Global.app_classes_services_directory;
                    break;
                case NamesHandler.ClasseType.Controller:
                    retorno = Util.Global.app_classes_controller_directory;
                    break;
                case NamesHandler.ClasseType.ProfilesDTO:
                    retorno = Util.Global.app_classes_profile_dto_directory;
                    break;
                case NamesHandler.ClasseType.ProfilesViewModel:
                    retorno = Util.Global.app_classes_profile_viewModel_directory;
                    break;
                default:
                    throw new ArgumentException($"Unsupported FileType: {type}");
            }

            return retorno;
        }

        public static string CriaNomeClasse(ClasseType classeType, string tableName)
        {
            // Formata o nome da tabela para iniciar com maiúscula e o restante em minúsculas.
            string formattedTableName = char.ToUpper(tableName[0]) + tableName.Substring(1).ToLower();

            string retorno = string.Empty;

            switch (classeType)
            {
                case ClasseType.Entity:
                    retorno = formattedTableName;
                    break;
                case ClasseType.Dto:
                    retorno = $"{formattedTableName}DTO";
                    break;
                case ClasseType.ViewModel:
                    retorno = $"{formattedTableName}ViewModel";
                    break;
                case ClasseType.InterfaceRepository:
                    retorno = $"I{formattedTableName}Repository";
                    break;
                case ClasseType.Repository:
                    retorno = $"{formattedTableName}Repository";
                    break;
                case ClasseType.InterfaceService:
                    retorno = $"I{formattedTableName}AppService";
                    break;
                case ClasseType.Service:
                    retorno = $"{formattedTableName}AppService";
                    break;
                case ClasseType.Controller:
                    retorno = $"{formattedTableName}Controller";
                    break;
                case ClasseType.ProfilesDTO:
                case ClasseType.ProfilesViewModel:
                    retorno = $"{formattedTableName}Profile";
                    break;
                default:
                    throw new ArgumentException("FileType inválido.");
            }

            return retorno;
        }
    }
}
