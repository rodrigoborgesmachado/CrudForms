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
                string csharpType = MapDatabaseTypeToCSharpType(campo.TipoCampo.Nome, campo.NotNull);

                // Generate the property
                sb.AppendLine($"        public {csharpType} {char.ToUpper(campo.Nome[0]) + campo.Nome.Substring(1).ToLower()} {{ get; set; }}");
            }

            return sb;
        }

        private static string MapDatabaseTypeToCSharpType(string dbType, bool notNull)
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
                    return GetNullableType("string", notNull);

                // Guid
                case "UUID":
                case "UNIQUEIDENTIFIER":
                    return GetNullableType("Guid", notNull);

                // Date and time types
                case "TIMESTAMP WITHOUT TIME ZONE":
                case "TIMESTAMP":
                case "DATETIME":
                case "SMALLDATETIME":
                case "DATETIME2":
                case "DATE":
                    return GetNullableType("DateTime", notNull);
                case "TIME":
                    return GetNullableType("TimeSpan", notNull);

                // Numeric types
                case "NUMERIC":
                case "DECIMAL":
                    return GetNullableType("decimal", notNull);
                case "FLOAT":
                    return GetNullableType("double", notNull);
                case "REAL":
                    return GetNullableType("float", notNull);
                case "DOUBLE":
                case "DOUBLE PRECISION":
                    return GetNullableType("double", notNull);

                // Integer types
                case "INTEGER":
                case "INT":
                    return GetNullableType("int", notNull);
                case "TINYINT":
                    return GetNullableType("byte", notNull);
                case "SMALLINT":
                    return GetNullableType("short", notNull);
                case "BIGINT":
                    return GetNullableType("long", notNull);

                // Boolean
                case "BOOLEAN":
                case "BIT":
                    return GetNullableType("bool", notNull);

                // Binary data
                case "BYTEA":
                case "BLOB":
                case "VARBINARY":
                case "BINARY":
                case "IMAGE":
                    return GetNullableType("byte[]", notNull);

                // Special types
                case "XML":
                case "JSON":
                    return GetNullableType("string", notNull);
                case "GEOMETRY":
                case "GEOGRAPHY":
                    return GetNullableType("object", notNull);

                // Default fallback
                default:
                    return GetNullableType("object", notNull); // Default to object for unmapped types
            }
        }

        private static string GetNullableType(string csharpType, bool notNull)
        {
            return notNull ? csharpType : $"{csharpType}?";
        }
    }
}
