using DAO;
using System;
using System.IO;
using System.Text;

namespace Regras.ApiClasses
{
    public static class ServiceCreator
    {
        public static bool CreateService(MD_Tabela tabela, string nomeProjeto)
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
                NamesHandler.ClasseType type = NamesHandler.ClasseType.InterfaceService;
                string classeName = NamesHandler.CriaNomeClasse(type, tabela.Nome);
                string caminhoFile = NamesHandler.GetDirectoryByType(type) + NamesHandler.CriaNomeArquivoClasse(type, tabela.Nome);
                Util.CL_Files.DeleteFilesIfExists(caminhoFile);

                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.AppendLine($"using MainDTO = {nomeProjeto}.Application.DTO.{NamesHandler.CriaNomeClasse(NamesHandler.ClasseType.Dto, tabela.Nome)};");
                stringBuilder.AppendLine($"using static {nomeProjeto}.Infrastructure.CrossCutting.Enums.Enums;");
                stringBuilder.AppendLine();
                stringBuilder.AppendLine($"{NamesHandler.GetNamespaceByType(nomeProjeto, type)}");
                stringBuilder.AppendLine($"{{");
                stringBuilder.AppendLine($"    public interface {classeName} : IDisposable");
                stringBuilder.AppendLine($"    {{");
                stringBuilder.AppendLine($"        Task<IEnumerable<MainDTO>> GetAllAsync(string? include = null);");
                stringBuilder.AppendLine();
                stringBuilder.AppendLine($"        Task<MainDTO> GetAsync(string code, string? include = null);");
                stringBuilder.AppendLine();
                stringBuilder.AppendLine($"        Task<MainDTO> InsertAsync(MainDTO mainDto);");
                stringBuilder.AppendLine();
                stringBuilder.AppendLine($"        Task<MainDTO> UpdateAsync(MainDTO mainDto);");
                stringBuilder.AppendLine();
                stringBuilder.AppendLine($"        Task<MainDTO> RemoveAsync(MainDTO mainDto);");
                stringBuilder.AppendLine();
                stringBuilder.AppendLine($"        Task<Tuple<int, int, IEnumerable<MainDTO>>> GetAllPagedAsync(int page, int quantity, DateTime? startDate, DateTime? endDate, string isActive = null, string term = null, string orderBy = null, string? include = null);");
                stringBuilder.AppendLine();
                stringBuilder.AppendLine($"        Task<string> GetReport(DateTime? startDate, DateTime? endDate, string isActive = null, string term = null, string orderBy = null, string? include = null);");
                stringBuilder.AppendLine();
                stringBuilder.AppendLine($"        Task<MainDTO> UpdateStatus(Status status, long id);");
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
                NamesHandler.ClasseType type = NamesHandler.ClasseType.Service;
                string classeName = NamesHandler.CriaNomeClasse(type, tabela.Nome);
                string caminhoFile = NamesHandler.GetDirectoryByType(type) + NamesHandler.CriaNomeArquivoClasse(type, tabela.Nome);
                Util.CL_Files.DeleteFilesIfExists(caminhoFile);

                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.AppendLine($"using IBlobStorageService = {nomeProjeto}.Domain.Interfaces.Services.IBlobStorageService;");
                stringBuilder.AppendLine($"using ILoggerService = {nomeProjeto}.Application.Interfaces.ILoggerAppService;");
                stringBuilder.AppendLine($"using IMainRepository = {nomeProjeto}.Domain.Interfaces.Repository.{NamesHandler.CriaNomeClasse(NamesHandler.ClasseType.InterfaceRepository, tabela.Nome)};");
                stringBuilder.AppendLine($"using IMainService = {nomeProjeto}.Application.Interfaces.I{classeName};");
                stringBuilder.AppendLine($"using Main = {nomeProjeto}.Domain.Entities.{NamesHandler.CriaNomeClasse(NamesHandler.ClasseType.Entity, tabela.Nome)};");
                stringBuilder.AppendLine($"using MainDTO = {nomeProjeto}.Application.DTO.{NamesHandler.CriaNomeClasse(NamesHandler.ClasseType.Dto, tabela.Nome)};");
                stringBuilder.AppendLine($"using Microsoft.Extensions.Options;");
                stringBuilder.AppendLine($"using static {nomeProjeto}.Infrastructure.CrossCutting.Enums.Enums;");
                stringBuilder.AppendLine($"using {nomeProjeto}.Domain.ModelClasses;");
                stringBuilder.AppendLine($"using {nomeProjeto}.Application.Helpers;");
                stringBuilder.AppendLine($"");
                stringBuilder.AppendLine($"{NamesHandler.GetNamespaceByType(nomeProjeto, type)}");
                stringBuilder.AppendLine($"{{");
                stringBuilder.AppendLine($"    public class {classeName} : ServiceBase<MainDTO>, IMainService");
                stringBuilder.AppendLine($"    {{");
                stringBuilder.AppendLine($"        private readonly IMainRepository _mainRepository;");
                stringBuilder.AppendLine($"        private readonly ILoggerService _loggerService;");
                stringBuilder.AppendLine($"");
                stringBuilder.AppendLine($"        private string[] allowInclude = new string[] {{ }};");
                stringBuilder.AppendLine($"");
                stringBuilder.AppendLine($"        public {classeName}(IBlobStorageService blobStorageService, IOptions<Settings> options, IMainRepository mainRepository, ILoggerService loggerService)");
                stringBuilder.AppendLine($"            : base(blobStorageService, options)");
                stringBuilder.AppendLine($"        {{");
                stringBuilder.AppendLine($"            _mainRepository = mainRepository;");
                stringBuilder.AppendLine($"            _loggerService = loggerService;");
                stringBuilder.AppendLine($"        }}");
                stringBuilder.AppendLine($"");
                stringBuilder.AppendLine($"        public async Task<IEnumerable<MainDTO>> GetAllAsync(string? include = null)");
                stringBuilder.AppendLine($"        {{");
                stringBuilder.AppendLine($"            var list = await _mainRepository.GetAllAsync(IncludesMethods.GetIncludes(include, allowInclude));");
                stringBuilder.AppendLine($"            return list.ProjectedAsCollection<MainDTO>();");
                stringBuilder.AppendLine($"        }}");
                stringBuilder.AppendLine($"");
                stringBuilder.AppendLine($"        public async Task<IEnumerable<MainDTO>> GetAllAsync(string parentCode, string? include = null)");
                stringBuilder.AppendLine($"        {{");
                stringBuilder.AppendLine($"            var list = await _mainRepository.GetAllAsync(parentCode, IncludesMethods.GetIncludes(include, allowInclude));");
                stringBuilder.AppendLine($"            return list.ProjectedAsCollection<MainDTO>();");
                stringBuilder.AppendLine($"        }}");
                stringBuilder.AppendLine($"");
                stringBuilder.AppendLine($"        public async Task<MainDTO> GetAsync(string code, string? include = null)");
                stringBuilder.AppendLine($"        {{");
                stringBuilder.AppendLine($"            var result = await _mainRepository.GetAsync(code, IncludesMethods.GetIncludes(include, allowInclude));");
                stringBuilder.AppendLine($"            return result.ProjectedAs<MainDTO>();");
                stringBuilder.AppendLine($"        }}");
                stringBuilder.AppendLine($"");
                stringBuilder.AppendLine($"        public async Task<Tuple<int, int, IEnumerable<MainDTO>>> GetAllPagedAsync(int page, int quantity, DateTime? startDate, DateTime? endDate, string isActive = null, string term = null, string orderBy = null, string? include = null)");
                stringBuilder.AppendLine($"        {{");
                stringBuilder.AppendLine($"            var tuple = await _mainRepository.GetAllPagedAsync(page, quantity, startDate, endDate, isActive, term, orderBy, IncludesMethods.GetIncludes(include, allowInclude));");
                stringBuilder.AppendLine($"");
                stringBuilder.AppendLine($"            var total = tuple.Item1;");
                stringBuilder.AppendLine($"            var pages = (int)Math.Ceiling((double)total / quantity);");
                stringBuilder.AppendLine($"");
                stringBuilder.AppendLine($"            var list = tuple.Item2.ProjectedAsCollection<MainDTO>();");
                stringBuilder.AppendLine($"");
                stringBuilder.AppendLine($"            return Tuple.Create(total, pages, list);");
                stringBuilder.AppendLine($"        }}");
                stringBuilder.AppendLine($"");
                stringBuilder.AppendLine($"        public async Task<MainDTO> InsertAsync(MainDTO mainDto)");
                stringBuilder.AppendLine($"        {{");
                stringBuilder.AppendLine($"            var main = mainDto.ProjectedAs<Main>();");
                stringBuilder.AppendLine($"");
                stringBuilder.AppendLine($"            _mainRepository.Add(main);");
                stringBuilder.AppendLine($"            await _mainRepository.CommitAsync();");
                stringBuilder.AppendLine($"");
                stringBuilder.AppendLine($"            return main.ProjectedAs<MainDTO>();");
                stringBuilder.AppendLine($"        }}");
                stringBuilder.AppendLine($"");
                stringBuilder.AppendLine($"        public async Task<MainDTO> UpdateAsync(MainDTO mainDto)");
                stringBuilder.AppendLine($"        {{");
                stringBuilder.AppendLine($"            var main = mainDto.ProjectedAs<Main>();");
                stringBuilder.AppendLine($"            main.Updated = DateTime.UtcNow;");
                stringBuilder.AppendLine($"");
                stringBuilder.AppendLine($"            _mainRepository.Update(main);");
                stringBuilder.AppendLine($"            await _mainRepository.CommitAsync();");
                stringBuilder.AppendLine($"");
                stringBuilder.AppendLine($"            return main.ProjectedAs<MainDTO>();");
                stringBuilder.AppendLine($"        }}");
                stringBuilder.AppendLine($"");
                stringBuilder.AppendLine($"        public async Task<MainDTO> RemoveAsync(MainDTO mainDto)");
                stringBuilder.AppendLine($"        {{");
                stringBuilder.AppendLine($"            var main = mainDto.ProjectedAs<Main>();");
                stringBuilder.AppendLine($"");
                stringBuilder.AppendLine($"            _mainRepository.Remove(main);");
                stringBuilder.AppendLine($"            await _mainRepository.CommitAsync();");
                stringBuilder.AppendLine($"");
                stringBuilder.AppendLine($"            return main.ProjectedAs<MainDTO>();");
                stringBuilder.AppendLine($"        }}");
                stringBuilder.AppendLine($"");
                stringBuilder.AppendLine($"        public async Task<string> GetReport(DateTime? startDate, DateTime? endDate, string isActive = null, string term = null, string orderBy = null, string? include = null)");
                stringBuilder.AppendLine($"        {{");
                stringBuilder.AppendLine($"            await _loggerService.InsertAsync($\"Report - Starting GetReport - {{this.GetType().Name}}\");");
                stringBuilder.AppendLine($"");
                stringBuilder.AppendLine($"            var result = await GetAllPagedAsync(1, int.MaxValue, startDate, endDate, isActive, term, orderBy: orderBy, include: include);");
                stringBuilder.AppendLine($"            string link = await UploadReport(result.Item3.ToList());");
                stringBuilder.AppendLine($"");
                stringBuilder.AppendLine($"            await _loggerService.InsertAsync($\"Report - Finishing GetReport - {{this.GetType().Name}}\");");
                stringBuilder.AppendLine($"            return link;");
                stringBuilder.AppendLine($"        }}");
                stringBuilder.AppendLine($"");
                stringBuilder.AppendLine($"        public async Task<MainDTO> UpdateStatus(Status status, long id)");
                stringBuilder.AppendLine($"        {{");
                stringBuilder.AppendLine($"            var main = await _mainRepository.GetByIdAsync(id);");
                stringBuilder.AppendLine($"");
                stringBuilder.AppendLine($"            if (main == null)");
                stringBuilder.AppendLine($"                throw new Exception(\"Object not found\");");
                stringBuilder.AppendLine($"");
                stringBuilder.AppendLine($"            main.IsActive = (byte)(status == Status.IsActive ? 1 : 0);");
                stringBuilder.AppendLine($"            main.IsDeleted = (byte)(status == Status.IsDeleted ? 1 : 0);");
                stringBuilder.AppendLine($"            _mainRepository.Update(main);");
                stringBuilder.AppendLine($"            await _mainRepository.CommitAsync();");
                stringBuilder.AppendLine($"");
                stringBuilder.AppendLine($"            return main.ProjectedAs<MainDTO>();");
                stringBuilder.AppendLine($"        }}");
                stringBuilder.AppendLine($"");
                stringBuilder.AppendLine($"        public void Dispose()");
                stringBuilder.AppendLine($"        {{");
                stringBuilder.AppendLine($"            _mainRepository.Dispose();");
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
