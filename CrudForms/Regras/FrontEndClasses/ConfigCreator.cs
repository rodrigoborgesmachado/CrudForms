using System;
using System.IO;
using System.Text;

namespace Regras.FrontEndClasses
{
    public static class ConfigCreator
    {
        public static bool Create(string projectPath)
        {
            bool success = true;

            try
            {
                string configComponents = NamesHandler.GetDirectoryByType(projectPath, NamesHandler.FileType.Config);
                CreateConfigJson(configComponents);
                CreateEnvConfig(configComponents);
                CreateStorageConfiguration(configComponents);
            }
            catch (Exception ex)
            {
                Util.CL_Files.LogException(ex);
                success = false;
            }

            return success;
        }

        private static void CreateStorageConfiguration(string path)
        {
            StringBuilder js = new StringBuilder();
            js.AppendLine("const Config = {");
            js.AppendLine("    TOKEN: \"token\",");
            js.AppendLine("    CODE: \"code\",");
            js.AppendLine("    NAME: \"name\",");
            js.AppendLine("};");
            js.AppendLine("export default Config;");

            File.WriteAllText(path + "//storageConfiguration.jsx", js.ToString());
        }

        private static void CreateEnvConfig(string path)
        {
            StringBuilder js = new StringBuilder();
            js.AppendLine("import ConfigService from \"../services/configService\";");
            js.AppendLine("");
            js.AppendLine("const EnvConfig = {");
            js.AppendLine("    DEVELOPMENT: {");
            js.AppendLine("      API_URL: ConfigService.getApiUrl(),");
            js.AppendLine("    },");
            js.AppendLine("    PRODUCTION: {");
            js.AppendLine("      API_URL: \"\",");
            js.AppendLine("    }");
            js.AppendLine("};");
            js.AppendLine("");
            js.AppendLine("const getCurrentEnvConfig = () => {");
            js.AppendLine("    return process.env.NODE_ENV === \"production\"");
            js.AppendLine("      ? EnvConfig.PRODUCTION.API_URL");
            js.AppendLine("      : EnvConfig.DEVELOPMENT.API_URL;");
            js.AppendLine("};");
            js.AppendLine("");
            js.AppendLine("export default getCurrentEnvConfig;");

            File.WriteAllText(path + "//envConfig.jsx", js.ToString());
        }

        private static void CreateConfigJson(string path)
        {
            StringBuilder js = new StringBuilder();
            js.AppendLine("{    ");
            js.AppendLine("    \"apiUrl\": \"https://localhost:7157\",");
            js.AppendLine("    \"DefaultNumberOfItemsTable\": 6");
            js.AppendLine("}");

            File.WriteAllText(path + "//config.json", js.ToString());
        }
    }
}
