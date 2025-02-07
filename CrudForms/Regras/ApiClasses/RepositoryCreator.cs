using DAO;
using System;
using System.IO;
using System.Text;

namespace Regras.ApiClasses
{
    public static class RepositoryCreator
    {
        public static bool CreateRepository(MD_Tabela tabela, string nomeProjeto)
        {
            bool result = true;

            result &= CriaInterface(tabela, nomeProjeto);
            result &= CriaImplementacao(tabela, nomeProjeto);

            return result;
        }

        private static bool CriaInterface(MD_Tabela tabela, string nomeProjeto)
        {
            bool result = true;

            try
            {
                NamesHandler.ClasseType type = NamesHandler.ClasseType.InterfaceRepository;
                string classeName = NamesHandler.CriaNomeClasse(type, tabela.Nome);
                string caminhoFile = NamesHandler.GetDirectoryByType(type) + NamesHandler.CriaNomeArquivoClasse(type, tabela.Nome);
                Util.CL_Files.DeleteFilesIfExists(caminhoFile);

                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.AppendLine($"using Main = {nomeProjeto}.Domain.Entities.{NamesHandler.CriaNomeClasse(NamesHandler.ClasseType.Entity, tabela.Nome)};");
                stringBuilder.AppendLine();
                stringBuilder.AppendLine($"{NamesHandler.GetNamespaceByType(nomeProjeto, type)}");
                stringBuilder.AppendLine($"{{");
                stringBuilder.AppendLine($"    public interface {classeName} : IRepositoryBase<Main>");
                stringBuilder.AppendLine($"    {{");
                stringBuilder.AppendLine($"        Task<IEnumerable<Main>> GetAllAsync(string[] include = null);");
                stringBuilder.AppendLine($"        Task<IEnumerable<Main>> GetAllAsync(string parentCode, string[] include = null);");
                stringBuilder.AppendLine($"        Task<Main> GetAsync(string code, string[] include = null);");
                stringBuilder.AppendLine($"        Task<Tuple<int, IEnumerable<Main>>> GetAllPagedAsync(int page, int quantity, DateTime? startDate, DateTime? endDate, string isActive = null, string term = null, string orderBy = null, string[] include = null);");
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

        private static bool CriaImplementacao(MD_Tabela tabela, string nomeProjeto)
        {
            bool result = true;

            try
            {
                NamesHandler.ClasseType type = NamesHandler.ClasseType.Repository;
                string classeName = NamesHandler.CriaNomeClasse(type, tabela.Nome);
                string caminhoFile = NamesHandler.GetDirectoryByType(type) + NamesHandler.CriaNomeArquivoClasse(type, tabela.Nome);
                Util.CL_Files.DeleteFilesIfExists(caminhoFile);

                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.AppendLine($"using Main = {nomeProjeto}.Domain.Entities.{NamesHandler.CriaNomeClasse(NamesHandler.ClasseType.Entity, tabela.Nome)};");
                stringBuilder.AppendLine($"using {nomeProjeto}.Infrastructure.Data.Context;");
                stringBuilder.AppendLine($"using Microsoft.EntityFrameworkCore;");
                stringBuilder.AppendLine($"using System.Text.RegularExpressions;");
                stringBuilder.AppendLine($"using IMainRepository = {nomeProjeto}.Domain.Interfaces.Repository.{NamesHandler.CriaNomeClasse(NamesHandler.ClasseType.InterfaceRepository, tabela.Nome)};");
                stringBuilder.AppendLine($"");
                stringBuilder.AppendLine($"{NamesHandler.GetNamespaceByType(nomeProjeto, type)}");
                stringBuilder.AppendLine($"{{");
                stringBuilder.AppendLine($"    public class {classeName} : RepositoryBase<Main>, IMainRepository");
                stringBuilder.AppendLine($"    {{");
                stringBuilder.AppendLine($"        public {classeName}({nomeProjeto}Context currentContext)");
                stringBuilder.AppendLine($"            : base(currentContext)");
                stringBuilder.AppendLine($"        {{");
                stringBuilder.AppendLine($"        }}");
                stringBuilder.AppendLine($"");
                stringBuilder.AppendLine($"        public async Task<IEnumerable<Main>> GetAllAsync(string[] include = null)");
                stringBuilder.AppendLine($"        {{");
                stringBuilder.AppendLine($"            return await GetAllAsync(s => s.IsDeleted.Equals(0), include, orderBy: \"Created: Desc\");");
                stringBuilder.AppendLine($"        }}");
                stringBuilder.AppendLine($"");
                stringBuilder.AppendLine($"        public async Task<IEnumerable<Main>> GetAllAsync(string parentCode, string[] include = null)");
                stringBuilder.AppendLine($"        {{");
                stringBuilder.AppendLine($"            return await GetAllAsync(s => s.IsDeleted.Equals(0), include, orderBy: \"Created: Desc\");");
                stringBuilder.AppendLine($"        }}");
                stringBuilder.AppendLine($"");
                stringBuilder.AppendLine($"        public async Task<Main> GetAsync(string code, string[] include = null)");
                stringBuilder.AppendLine($"        {{");
                stringBuilder.AppendLine($"            var query = GetQueryable().Where(p => p.Code.Equals(code));");
                stringBuilder.AppendLine($"");
                stringBuilder.AppendLine($"            if (include != null)");
                stringBuilder.AppendLine($"            {{");
                stringBuilder.AppendLine($"                foreach (var toInclude in include)");
                stringBuilder.AppendLine($"                {{");
                stringBuilder.AppendLine($"                    query = query.Include(toInclude);");
                stringBuilder.AppendLine($"                }}");
                stringBuilder.AppendLine($"            }}");
                stringBuilder.AppendLine($"");
                stringBuilder.AppendLine($"            return await query.SingleOrDefaultAsync();");
                stringBuilder.AppendLine($"        }}");
                stringBuilder.AppendLine($"");
                stringBuilder.AppendLine($"        public async Task<Tuple<int, IEnumerable<Main>>> GetAllPagedAsync(int page, int quantity, DateTime? startDate, DateTime? endDate, string isActive = null, string term = null, string orderBy = null, string[] include = null)");
                stringBuilder.AppendLine($"        {{");
                stringBuilder.AppendLine($"            var query = GetQueryable();");
                stringBuilder.AppendLine($"");
                stringBuilder.AppendLine($"            if (!string.IsNullOrEmpty(isActive))");
                stringBuilder.AppendLine($"            {{");
                stringBuilder.AppendLine($"                Regex regexObj = new Regex(@\"[^\\d]\");");
                stringBuilder.AppendLine($"");
                stringBuilder.AppendLine($"                string isActiveString = regexObj.Replace(isActive, \"\");");
                stringBuilder.AppendLine($"");
                stringBuilder.AppendLine($"                int isActiveInt32 = 0;");
                stringBuilder.AppendLine($"");
                stringBuilder.AppendLine($"                if (!string.IsNullOrEmpty(isActiveString))");
                stringBuilder.AppendLine($"                {{");
                stringBuilder.AppendLine($"                    isActiveInt32 = Convert.ToInt32(regexObj.Replace(isActive, \"\"));");
                stringBuilder.AppendLine($"                }}");
                stringBuilder.AppendLine($"");
                stringBuilder.AppendLine($"                if (isActiveInt32 > 1)");
                stringBuilder.AppendLine($"                {{");
                stringBuilder.AppendLine($"                    isActiveInt32 = 1;");
                stringBuilder.AppendLine($"                }}");
                stringBuilder.AppendLine($"");
                stringBuilder.AppendLine($"                byte isActiveByte = Convert.ToByte(isActiveInt32);");
                stringBuilder.AppendLine($"");
                stringBuilder.AppendLine($"                query = query.Where(c => c.IsActive.Equals(isActiveByte));");
                stringBuilder.AppendLine($"            }}");
                stringBuilder.AppendLine($"");
                stringBuilder.AppendLine($"            if (!string.IsNullOrEmpty(term))");
                stringBuilder.AppendLine($"            {{");
                stringBuilder.AppendLine($"                query = query.Where(c => c.Code.Equals(term));");
                stringBuilder.AppendLine($"            }}");
                stringBuilder.AppendLine($"");
                stringBuilder.AppendLine($"            if (startDate != null)");
                stringBuilder.AppendLine($"            {{");
                stringBuilder.AppendLine($"                query = query.Where(o => o.Created >= startDate);");
                stringBuilder.AppendLine($"            }}");
                stringBuilder.AppendLine($"            if (endDate != null)");
                stringBuilder.AppendLine($"            {{");
                stringBuilder.AppendLine($"                query = query.Where(o => o.Created <= endDate);");
                stringBuilder.AppendLine($"            }}");
                stringBuilder.AppendLine($"");
                stringBuilder.AppendLine($"            var total = await GetAllPagedTotalAsync(query, include);");
                stringBuilder.AppendLine($"            var list = await GetAllPagedAsync(query, page, quantity, include, orderBy);");
                stringBuilder.AppendLine($"");
                stringBuilder.AppendLine($"            return Tuple.Create(total, list);");
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
