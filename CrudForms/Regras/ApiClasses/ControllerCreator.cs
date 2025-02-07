using DAO;
using System.Text;
using System;
using System.IO;

namespace Regras.ApiClasses
{
    public static class ControllerCreator
    {
        public static bool CreateController(MD_Tabela tabela, string nomeProjeto)
        {
            bool result = true;

            result &= Cria(tabela, nomeProjeto);

            return result;
        }

        private static bool Cria(MD_Tabela tabela, string nomeProjeto)
        {
            bool result = true;

            try
            {
                NamesHandler.ClasseType type = NamesHandler.ClasseType.Controller;
                string classeName = NamesHandler.CriaNomeClasse(type, tabela.Nome);
                string caminhoFile = NamesHandler.GetDirectoryByType(type) + NamesHandler.CriaNomeArquivoClasse(type, tabela.Nome);
                Util.CL_Files.DeleteFilesIfExists(caminhoFile);

                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.AppendLine($"using {nomeProjeto}.Presentation.Api.Handler;");
                stringBuilder.AppendLine($"using {nomeProjeto}.Application.Helpers;");
                stringBuilder.AppendLine($"using {nomeProjeto}.Domain.ModelClasses;");
                stringBuilder.AppendLine($"using {nomeProjeto}.Presentation.Model.Returns;");
                stringBuilder.AppendLine($"using Microsoft.AspNetCore.Authorization;");
                stringBuilder.AppendLine($"using Microsoft.AspNetCore.Mvc;");
                stringBuilder.AppendLine($"using Microsoft.Extensions.Options;");
                stringBuilder.AppendLine($"using IMainAppService = {nomeProjeto}.Application.Interfaces.{NamesHandler.CriaNomeClasse(NamesHandler.ClasseType.InterfaceService, tabela.Nome)};");
                stringBuilder.AppendLine($"using MainDTO = {nomeProjeto}.Application.DTO.{NamesHandler.CriaNomeClasse(NamesHandler.ClasseType.Dto, tabela.Nome)};");
                stringBuilder.AppendLine($"using MainViewModel = {nomeProjeto}.Presentation.Model.ViewModels.{NamesHandler.CriaNomeClasse(NamesHandler.ClasseType.ViewModel, tabela.Nome)};");
                stringBuilder.AppendLine($"");
                stringBuilder.AppendLine($"namespace {nomeProjeto}.Presentation.Api.Controllers");
                stringBuilder.AppendLine($"{{");
                stringBuilder.AppendLine($"    /// <summary>");
                stringBuilder.AppendLine($"    /// Controller Class");
                stringBuilder.AppendLine($"    /// </summary>");
                stringBuilder.AppendLine($"    [ApiController]");
                stringBuilder.AppendLine($"    [Authorize]");
                stringBuilder.AppendLine($"    [Route(\"{tabela.Nome}\")]");
                stringBuilder.AppendLine($"    public class {classeName} : ControllerBase, IDisposable");
                stringBuilder.AppendLine($"    {{");
                stringBuilder.AppendLine($"        private readonly IMainAppService _mainAppService;");
                stringBuilder.AppendLine($"        private readonly Settings _settings;");
                stringBuilder.AppendLine($"");
                stringBuilder.AppendLine($"        private readonly TokenHandler tokenController;");
                stringBuilder.AppendLine($"");
                stringBuilder.AppendLine($"        /// <summary>");
                stringBuilder.AppendLine($"        /// Class constructor");
                stringBuilder.AppendLine($"        /// </summary>");
                stringBuilder.AppendLine($"        public {classeName}(IMainAppService mainAppService, IOptions<Settings> options, IHttpContextAccessor httpContextAccessor)");
                stringBuilder.AppendLine($"        {{");
                stringBuilder.AppendLine($"            _mainAppService = mainAppService;");
                stringBuilder.AppendLine($"            _settings = options.Value;");
                stringBuilder.AppendLine($"");
                stringBuilder.AppendLine($"            tokenController = new TokenHandler(httpContextAccessor);");
                stringBuilder.AppendLine($"        }}");
                stringBuilder.AppendLine($"");
                stringBuilder.AppendLine($"        /// <summary>");
                stringBuilder.AppendLine($"        /// Get all");
                stringBuilder.AppendLine($"        /// </summary>");
                stringBuilder.AppendLine($"        /// <param name=\"include\"></param>");
                stringBuilder.AppendLine($"        /// <returns><![CDATA[Task<IEnumerable<MainViewModel>>]]></returns>");
                stringBuilder.AppendLine($"        [HttpGet]");
                stringBuilder.AppendLine($"        public async Task<IActionResult> Get(string? include = null)");
                stringBuilder.AppendLine($"        {{");
                stringBuilder.AppendLine($"            var mainDto = await _mainAppService.GetAllAsync(include);");
                stringBuilder.AppendLine($"");
                stringBuilder.AppendLine($"            return Ok(mainDto.ProjectedAsCollection<MainViewModel>());");
                stringBuilder.AppendLine($"        }}");
                stringBuilder.AppendLine($"");
                stringBuilder.AppendLine($"        /// <summary>");
                stringBuilder.AppendLine($"        /// Get by code");
                stringBuilder.AppendLine($"        /// </summary>");
                stringBuilder.AppendLine($"        /// <param name=\"code\"></param>");
                stringBuilder.AppendLine($"        /// <param name=\"include\"></param>");
                stringBuilder.AppendLine($"        /// <returns><![CDATA[Task<MainViewModel>]]></returns>");
                stringBuilder.AppendLine($"        [HttpGet(\"{{code}}\")]");
                stringBuilder.AppendLine($"        public async Task<IActionResult> Get(string code, string? include = null)");
                stringBuilder.AppendLine($"        {{");
                stringBuilder.AppendLine($"            var mainDto = await _mainAppService.GetAsync(code, include);");
                stringBuilder.AppendLine($"");
                stringBuilder.AppendLine($"            return Ok(mainDto.ProjectedAs<MainViewModel>());");
                stringBuilder.AppendLine($"        }}");
                stringBuilder.AppendLine($"");
                stringBuilder.AppendLine($"        /// <summary>");
                stringBuilder.AppendLine($"        /// List paged");
                stringBuilder.AppendLine($"        /// </summary>");
                stringBuilder.AppendLine($"        /// <param name=\"page\"></param>");
                stringBuilder.AppendLine($"        /// <param name=\"quantity\"></param>");
                stringBuilder.AppendLine($"        /// <param name=\"startDate\"></param>");
                stringBuilder.AppendLine($"        /// <param name=\"endDate\"></param>");
                stringBuilder.AppendLine($"        /// <param name=\"isActive\"></param>");
                stringBuilder.AppendLine($"        /// <param name=\"term\"></param>");
                stringBuilder.AppendLine($"        /// <param name=\"orderBy\"></param>");
                stringBuilder.AppendLine($"        /// <param name=\"include\"></param>");
                stringBuilder.AppendLine($"        /// <returns><![CDATA[Task<PaggedBaseReturn<MainViewModel>>]]></returns>");
                stringBuilder.AppendLine($"        [HttpGet(\"pagged\")]");
                stringBuilder.AppendLine($"        public async Task<IActionResult> GetPagged(int page, int quantity, DateTime? startDate, DateTime? endDate, string isActive = null, string term = null, string orderBy = null, string? include = null)");
                stringBuilder.AppendLine($"        {{");
                stringBuilder.AppendLine($"            var result = await _mainAppService.GetAllPagedAsync(page, quantity, startDate, endDate, isActive, term, orderBy: orderBy, include: include);");
                stringBuilder.AppendLine($"");
                stringBuilder.AppendLine($"            var list = result.Item3.ProjectedAsCollection<MainViewModel>();");
                stringBuilder.AppendLine($"");
                stringBuilder.AppendLine($"            return Ok(");
                stringBuilder.AppendLine($"                new PaggedBaseReturn<MainViewModel>");
                stringBuilder.AppendLine($"                {{");
                stringBuilder.AppendLine($"                    Page = page,");
                stringBuilder.AppendLine($"                    Quantity = quantity,");
                stringBuilder.AppendLine($"                    TotalCount = result.Item1,");
                stringBuilder.AppendLine($"                    TotalPages = result.Item2,");
                stringBuilder.AppendLine($"                    Results = list");
                stringBuilder.AppendLine($"                }}");
                stringBuilder.AppendLine($"            );");
                stringBuilder.AppendLine($"        }}");
                stringBuilder.AppendLine($"");
                stringBuilder.AppendLine($"        /// <summary>");
                stringBuilder.AppendLine($"        /// List paged");
                stringBuilder.AppendLine($"        /// </summary>");
                stringBuilder.AppendLine($"        /// <param name=\"startDate\"></param>");
                stringBuilder.AppendLine($"        /// <param name=\"endDate\"></param>");
                stringBuilder.AppendLine($"        /// <param name=\"isActive\"></param>");
                stringBuilder.AppendLine($"        /// <param name=\"term\"></param>");
                stringBuilder.AppendLine($"        /// <param name=\"orderBy\"></param>");
                stringBuilder.AppendLine($"        /// <param name=\"include\"></param>");
                stringBuilder.AppendLine($"        /// <returns><![CDATA[Task<PaggedBaseReturn<MainViewModel>>]]></returns>");
                stringBuilder.AppendLine($"        [HttpGet(\"export\")]");
                stringBuilder.AppendLine($"        public async Task<IActionResult> Export(DateTime? startDate, DateTime? endDate, string isActive = null, string term = null, string orderBy = null, string? include = null)");
                stringBuilder.AppendLine($"        {{");
                stringBuilder.AppendLine($"            var result = await _mainAppService.GetReport(startDate, endDate, isActive, term, orderBy: orderBy, include: include);");
                stringBuilder.AppendLine($"");
                stringBuilder.AppendLine($"            return Ok(new BaseReturn<string>");
                stringBuilder.AppendLine($"            {{");
                stringBuilder.AppendLine($"                Message = \"Report created\",");
                stringBuilder.AppendLine($"                Status = 200,");
                stringBuilder.AppendLine($"                Object = result");
                stringBuilder.AppendLine($"            }});");
                stringBuilder.AppendLine($"        }}");
                stringBuilder.AppendLine($"");
                stringBuilder.AppendLine($"        /// <summary>");
                stringBuilder.AppendLine($"		/// Insert new");
                stringBuilder.AppendLine($"		/// </summary>");
                stringBuilder.AppendLine($"		/// <param name=\"model\"></param>");
                stringBuilder.AppendLine($"		/// <returns><![CDATA[Task<IActionResult>]]></returns>");
                stringBuilder.AppendLine($"        [HttpPost]");
                stringBuilder.AppendLine($"        public async Task<IActionResult> Post([FromBody] MainViewModel model)");
                stringBuilder.AppendLine($"        {{");
                stringBuilder.AppendLine($"            var mainDto = model.ProjectedAs<MainDTO>();");
                stringBuilder.AppendLine($"            var result = await _mainAppService.InsertAsync(mainDto);");
                stringBuilder.AppendLine($"");
                stringBuilder.AppendLine($"            return Ok(result);");
                stringBuilder.AppendLine($"        }}");
                stringBuilder.AppendLine($"");
                stringBuilder.AppendLine($"        /// <summary>");
                stringBuilder.AppendLine($"		/// Update");
                stringBuilder.AppendLine($"		/// </summary>");
                stringBuilder.AppendLine($"		/// <param name=\"model\"></param>");
                stringBuilder.AppendLine($"		/// <returns><![CDATA[Task<IActionResult>]]></returns>");
                stringBuilder.AppendLine($"        [HttpPut]");
                stringBuilder.AppendLine($"        public async Task<IActionResult> Put([FromBody] MainViewModel model)");
                stringBuilder.AppendLine($"        {{");
                stringBuilder.AppendLine($"            var mainDto = model.ProjectedAs<MainDTO>();");
                stringBuilder.AppendLine($"            var result = await _mainAppService.UpdateAsync(mainDto);");
                stringBuilder.AppendLine($"");
                stringBuilder.AppendLine($"            return Ok(result);");
                stringBuilder.AppendLine($"        }}");
                stringBuilder.AppendLine($"");
                stringBuilder.AppendLine($"        /// <summary>");
                stringBuilder.AppendLine($"		/// Remove");
                stringBuilder.AppendLine($"		/// </summary>");
                stringBuilder.AppendLine($"		/// <param name=\"model\"></param>");
                stringBuilder.AppendLine($"		/// <returns><![CDATA[Task<IActionResult>]]></returns>");
                stringBuilder.AppendLine($"        [HttpDelete]");
                stringBuilder.AppendLine($"        public async Task<IActionResult> Delete([FromBody] MainViewModel model)");
                stringBuilder.AppendLine($"        {{");
                stringBuilder.AppendLine($"            var mainDto = model.ProjectedAs<MainDTO>();");
                stringBuilder.AppendLine($"            var result = await _mainAppService.RemoveAsync(mainDto);");
                stringBuilder.AppendLine($"");
                stringBuilder.AppendLine($"            return Ok(result);");
                stringBuilder.AppendLine($"        }}");
                stringBuilder.AppendLine($"");
                stringBuilder.AppendLine($"        /// <summary>");
                stringBuilder.AppendLine($"		/// Dispose");
                stringBuilder.AppendLine($"		/// </summary>");
                stringBuilder.AppendLine($"		/// <param name=\"disposing\"></param>");
                stringBuilder.AppendLine($"		/// <returns><![CDATA[]]></returns>");
                stringBuilder.AppendLine($"        public void Dispose()");
                stringBuilder.AppendLine($"        {{");
                stringBuilder.AppendLine($"            _mainAppService.Dispose();");
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
