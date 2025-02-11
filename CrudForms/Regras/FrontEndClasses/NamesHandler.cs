using System;
using System.IO;

namespace Regras.FrontEndClasses
{
    public static class NamesHandler
    {
        public enum FileType
        {
            Assets,
            AssetsImages,
            AssetsStyles,
            Components,
            ComponentsAdmin,
            ComponentsClient,
            ComponentsCommon,
            ComponentsIcons,
            Config,
            Layouts,
            LayoutsAdmin,
            LayoutsClient,
            Pages,
            PagesAdmin,
            PagesClient,
            PagesCommon,
            Routes,
            RoutesAdmin,
            RoutesClient,
            Services,
            ServicesApiServices,
            ServicesRedux,
            Utils
        }

        public static string CreateComponentListName(string tableName)
        {
            string formattedTableName = char.ToUpper(tableName[0]) + tableName.Substring(1).ToLower();

            return $"{formattedTableName}ListPage";
        }

        public static string CreateName(string tableName)
        {
            string formattedTableName = char.ToUpper(tableName[0]) + tableName.Substring(1).ToLower();

            return $"{formattedTableName}";
        }

        public static string GetApiName(string tableName)
        {
            string formattedTableName = tableName.ToLower();

            return $"{formattedTableName}Api";
        }

        public static string GetDirectoryByType(string directory, FileType type)
        {
            string basePath = Path.Combine(directory, "src"); // Base folder
            string retorno = basePath;

            switch (type)
            {
                case FileType.Assets:
                    retorno = Path.Combine(basePath, "assets");
                    break;
                case FileType.AssetsImages:
                    retorno = Path.Combine(basePath, "assets", "images");
                    break;
                case FileType.AssetsStyles:
                    retorno = Path.Combine(basePath, "assets", "styles");
                    break;
                case FileType.Components:
                    retorno = Path.Combine(basePath, "components");
                    break;
                case FileType.ComponentsAdmin:
                    retorno = Path.Combine(basePath, "components", "admin");
                    break;
                case FileType.ComponentsClient:
                    retorno = Path.Combine(basePath, "components", "client");
                    break;
                case FileType.ComponentsCommon:
                    retorno = Path.Combine(basePath, "components", "common");
                    break;
                case FileType.ComponentsIcons:
                    retorno = Path.Combine(basePath, "components", "icons");
                    break;
                case FileType.Config:
                    retorno = Path.Combine(basePath, "config");
                    break;
                case FileType.Layouts:
                    retorno = Path.Combine(basePath, "layouts");
                    break;
                case FileType.LayoutsAdmin:
                    retorno = Path.Combine(basePath, "layouts", "admin");
                    break;
                case FileType.LayoutsClient:
                    retorno = Path.Combine(basePath, "layouts", "client");
                    break;
                case FileType.Pages:
                    retorno = Path.Combine(basePath, "pages");
                    break;
                case FileType.PagesAdmin:
                    retorno = Path.Combine(basePath, "pages", "admin");
                    break;
                case FileType.PagesClient:
                    retorno = Path.Combine(basePath, "pages", "client");
                    break;
                case FileType.PagesCommon:
                    retorno = Path.Combine(basePath, "pages", "common");
                    break;
                case FileType.Routes:
                    retorno = Path.Combine(basePath, "routes");
                    break;
                case FileType.RoutesAdmin:
                    retorno = Path.Combine(basePath, "routes", "admin");
                    break;
                case FileType.RoutesClient:
                    retorno = Path.Combine(basePath, "routes", "clients");
                    break;
                case FileType.Services:
                    retorno = Path.Combine(basePath, "services");
                    break;
                case FileType.ServicesApiServices:
                    retorno = Path.Combine(basePath, "services", "apiServices");
                    break;
                case FileType.ServicesRedux:
                    retorno = Path.Combine(basePath, "services", "redux");
                    break;
                case FileType.Utils:
                    retorno = Path.Combine(basePath, "utils");
                    break;
                default:
                    retorno = basePath;
                    break;
            }

            return retorno;
        }

    }
}
