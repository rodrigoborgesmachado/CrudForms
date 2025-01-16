using DAO;
using System;
using System.IO;
using System.Text;

namespace Regras.ApiClasses
{
    public static class EntidadesCreator
    {
        public static bool CreateEntidades(MD_Tabela tabela, string nomeProjeto)
        {
            bool result = true;  

            result &= Cria(tabela, nomeProjeto, NamesHandler.ClasseType.Entity);
            result &= Cria(tabela, nomeProjeto, NamesHandler.ClasseType.Dto);
            result &= Cria(tabela, nomeProjeto, NamesHandler.ClasseType.ViewModel);

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
                stringBuilder.AppendLine($"{NamesHandler.GetNamespaceByType(nomeProjeto, type)}");
                stringBuilder.AppendLine($"{{");
                stringBuilder.AppendLine($"    public class {classeName} : {GetExtensionByType(type)}");
                stringBuilder.AppendLine($"    {{");
                stringBuilder.Append(CreateProps(tabela));
                stringBuilder.AppendLine($"    }}");
                stringBuilder.AppendLine($"}}");

                File.WriteAllText(caminhoFile, stringBuilder.ToString());
            }
            catch (Exception ex)
            {
                result = false;
                Util.CL_Files.LogException(ex);
            }

            return result;
        }

        private static string GetExtensionByType(NamesHandler.ClasseType type)
        {
            string retorno = string.Empty;

            switch (type)
            {
                case NamesHandler.ClasseType.Dto:
                    retorno = "BaseDTO";
                    break;
                case NamesHandler.ClasseType.ViewModel:
                    retorno = "BaseViewModel";
                    break;
                case NamesHandler.ClasseType.Entity:
                    retorno = "BaseEntity";
                    break;
            }

            return retorno;
        }

        private static StringBuilder CreateProps(MD_Tabela tabela)
        {
            StringBuilder sb = new StringBuilder();

            foreach (var campo in tabela.CamposDaTabela())
            {
                // Skip specific fields
                if (campo.Nome.Equals("Code", StringComparison.OrdinalIgnoreCase) ||
                    campo.Nome.Equals("IsActive", StringComparison.OrdinalIgnoreCase) ||
                    campo.Nome.Equals("IsDeleted", StringComparison.OrdinalIgnoreCase) ||
                    campo.Nome.Equals("Created", StringComparison.OrdinalIgnoreCase) ||
                    campo.Nome.Equals("Updated", StringComparison.OrdinalIgnoreCase))
                {
                    continue;
                }

                // Map the database type to a C# type
                string csharpType = MapDatabaseTypeToCSharpType(campo.TipoCampo.Nome);

                // Generate the property
                sb.AppendLine($"        public {csharpType} {char.ToUpper(campo.Nome[0]) + campo.Nome.Substring(1).ToLower()} {{ get; set; }}");
            }

            return sb;
        }

        private static string MapDatabaseTypeToCSharpType(string dbType)
        {
            // Normalize the type for case-insensitive matching
            string normalizedDbType = dbType.Trim().ToUpper();

            switch (normalizedDbType)
            {
                // String types
                case "CHARACTER VARYING":
                case "VARCHAR":
                case "CHAR":
                case "TEXT":
                case "NVARCHAR":
                case "NCHAR":
                case "NTEXT":
                case "CLOB":
                    return "string";

                // Guid
                case "UUID":
                case "UNIQUEIDENTIFIER":
                    return "Guid";

                // Date and time types
                case "TIMESTAMP WITHOUT TIME ZONE":
                case "TIMESTAMP":
                case "DATETIME":
                case "SMALLDATETIME":
                case "DATETIME2":
                case "DATE":
                    return "DateTime";
                case "TIME":
                    return "TimeSpan";

                // Numeric types
                case "NUMERIC":
                case "DECIMAL":
                    return "decimal";
                case "FLOAT":
                    return "double";
                case "REAL":
                    return "float";
                case "DOUBLE":
                case "DOUBLE PRECISION":
                    return "double";

                // Integer types
                case "INTEGER":
                case "INT":
                    return "int";
                case "TINYINT":
                    return "byte";
                case "SMALLINT":
                    return "short";
                case "BIGINT":
                    return "long";

                // Boolean
                case "BOOLEAN":
                case "BIT":
                    return "bool";

                // Binary data
                case "BYTEA":
                case "BLOB":
                case "VARBINARY":
                case "BINARY":
                case "IMAGE":
                    return "byte[]";

                // Special types
                case "XML":
                case "JSON":
                    return "string";
                case "GEOMETRY":
                case "GEOGRAPHY":
                    return "object";

                // Default fallback
                default:
                    return "object"; // Default to object for unmapped types
            }
        }
    }
}
