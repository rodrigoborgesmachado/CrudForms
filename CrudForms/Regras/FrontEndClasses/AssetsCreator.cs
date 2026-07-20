using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Regras.FrontEndClasses
{
    public static class AssetsCreator
    {
        public static bool Create(string projectPath, List<Model.MD_FrontEndCssVariable> cssVariables = null)
        {
            bool success = true;

            try
            {
                string fileCss = NamesHandler.GetDirectoryByType(projectPath, NamesHandler.FileType.AssetsStyles) + "//index.css";
                CreateCssFile(fileCss, cssVariables);
            }
            catch (Exception ex)
            {
                Util.CL_Files.LogException(ex);
                success = false;
            }

            return success;
        }

        public static List<Model.MD_FrontEndCssVariable> GetDefaultCssVariables()
        {
            return new List<Model.MD_FrontEndCssVariable>
            {
                new Model.MD_FrontEndCssVariable("Light Theme", "--app-light-primary", "#1b4332"),
                new Model.MD_FrontEndCssVariable("Light Theme", "--app-light-primary-hover", "#143326"),
                new Model.MD_FrontEndCssVariable("Light Theme", "--app-light-sidebar-bg", "#1b4332"),
                new Model.MD_FrontEndCssVariable("Light Theme", "--app-light-body-bg", "#f8f9fa"),
                new Model.MD_FrontEndCssVariable("Light Theme", "--app-light-body-color", "#212529"),
                new Model.MD_FrontEndCssVariable("Light Theme", "--app-light-tertiary-bg", "#f1f3f5"),
                new Model.MD_FrontEndCssVariable("Dark Theme", "--app-dark-primary", "#2cdebf"),
                new Model.MD_FrontEndCssVariable("Dark Theme", "--app-dark-primary-hover", "#23b99f"),
                new Model.MD_FrontEndCssVariable("Dark Theme", "--app-dark-sidebar-bg", "#1a1a1a"),
                new Model.MD_FrontEndCssVariable("Dark Theme", "--app-dark-body-bg", "#242424"),
                new Model.MD_FrontEndCssVariable("Dark Theme", "--app-dark-body-color", "#e2e8f0"),
                new Model.MD_FrontEndCssVariable("Dark Theme", "--app-dark-tertiary-bg", "#2b2b2b"),
                new Model.MD_FrontEndCssVariable("Theme Status", "--app-secondary", "#6c757d"),
                new Model.MD_FrontEndCssVariable("Theme Status", "--app-success", "#198754"),
                new Model.MD_FrontEndCssVariable("Theme Status", "--app-danger", "#dc3545"),
                new Model.MD_FrontEndCssVariable("Theme Status", "--app-warning", "#ffc107"),
                new Model.MD_FrontEndCssVariable("Theme Status", "--app-info", "#0dcaf0"),
                new Model.MD_FrontEndCssVariable("Layout", "--app-sidebar-width", "260px"),
                new Model.MD_FrontEndCssVariable("Layout", "--app-header-height", "64px"),
                new Model.MD_FrontEndCssVariable("Layout", "--app-content-max-width", "1600px"),
                new Model.MD_FrontEndCssVariable("Layout", "--app-border-radius", "0.5rem")
            };
        }

        private static void AppendCssVariables(StringBuilder cssFile, List<Model.MD_FrontEndCssVariable> cssVariables)
        {
            var defaults = GetDefaultCssVariables();
            var provided = cssVariables ?? defaults;
            var variables = defaults
                .Select(defaultVariable =>
                {
                    var configuredVariable = provided.FirstOrDefault(variable =>
                        string.Equals(variable.Nome, defaultVariable.Nome, StringComparison.OrdinalIgnoreCase));

                    return configuredVariable ?? defaultVariable;
                })
                .ToList();

            string currentGroup = null;

            cssFile.AppendLine(":root {");
            foreach (var variable in variables)
            {
                if (!string.Equals(currentGroup, variable.Grupo, StringComparison.OrdinalIgnoreCase))
                {
                    if (currentGroup != null)
                    {
                        cssFile.AppendLine();
                    }

                    currentGroup = variable.Grupo;
                    cssFile.AppendLine($"    /* {currentGroup} */");
                }

                cssFile.AppendLine($"    {variable.Nome}: {variable.Valor};");
            }
            cssFile.AppendLine("}");
        }

        private static void CreateCssFile(string fileDirectory, List<Model.MD_FrontEndCssVariable> cssVariables)
        {
            StringBuilder cssFile = new StringBuilder();
            cssFile.AppendLine("@import url('https://fonts.googleapis.com/css2?family=Jost:wght@100..900&family=Roboto:wght@100;300;400;500;700;900&display=swap');");
            cssFile.AppendLine();
            AppendCssVariables(cssFile, cssVariables);
            cssFile.AppendLine();
            cssFile.AppendLine("[data-bs-theme=\"light\"] {");
            cssFile.AppendLine("    --app-primary: var(--app-light-primary);");
            cssFile.AppendLine("    --app-primary-hover: var(--app-light-primary-hover);");
            cssFile.AppendLine("    --app-sidebar-bg: var(--app-light-sidebar-bg);");
            cssFile.AppendLine("    --bs-primary: var(--app-light-primary);");
            cssFile.AppendLine("    --bs-body-bg: var(--app-light-body-bg);");
            cssFile.AppendLine("    --bs-body-color: var(--app-light-body-color);");
            cssFile.AppendLine("    --bs-tertiary-bg: var(--app-light-tertiary-bg);");
            cssFile.AppendLine("    --bs-heading-color: var(--app-light-body-color);");
            cssFile.AppendLine("}");
            cssFile.AppendLine();
            cssFile.AppendLine("[data-bs-theme=\"dark\"] {");
            cssFile.AppendLine("    --app-primary: var(--app-dark-primary);");
            cssFile.AppendLine("    --app-primary-hover: var(--app-dark-primary-hover);");
            cssFile.AppendLine("    --app-sidebar-bg: var(--app-dark-sidebar-bg);");
            cssFile.AppendLine("    --bs-primary: var(--app-dark-primary);");
            cssFile.AppendLine("    --bs-body-bg: var(--app-dark-body-bg);");
            cssFile.AppendLine("    --bs-body-color: var(--app-dark-body-color);");
            cssFile.AppendLine("    --bs-tertiary-bg: var(--app-dark-tertiary-bg);");
            cssFile.AppendLine("    --bs-heading-color: var(--app-dark-body-color);");
            cssFile.AppendLine("    --bs-card-bg: #1a1a1a;");
            cssFile.AppendLine("    --bs-card-color: var(--app-dark-body-color);");
            cssFile.AppendLine("    --bs-border-color: #717986;");
            cssFile.AppendLine("    --bs-table-color: var(--app-dark-body-color);");
            cssFile.AppendLine("    --bs-table-bg: #1a1a1a;");
            cssFile.AppendLine("    --bs-table-border-color: #717986;");
            cssFile.AppendLine("    --bs-table-hover-color: #f8fafc;");
            cssFile.AppendLine("    --bs-table-hover-bg: #242424;");
            cssFile.AppendLine("}");
            cssFile.AppendLine();
            cssFile.AppendLine("body {");
            cssFile.AppendLine("    min-height: 100vh;");
            cssFile.AppendLine("    font-family: 'Jost', system-ui, -apple-system, BlinkMacSystemFont, 'Segoe UI', sans-serif;");
            cssFile.AppendLine("    background-color: var(--bs-body-bg);");
            cssFile.AppendLine("}");
            cssFile.AppendLine();
            cssFile.AppendLine("#root {");
            cssFile.AppendLine("    min-height: 100vh;");
            cssFile.AppendLine("}");
            cssFile.AppendLine();
            cssFile.AppendLine(".btn-primary {");
            cssFile.AppendLine("    --bs-btn-bg: var(--app-primary);");
            cssFile.AppendLine("    --bs-btn-border-color: var(--app-primary);");
            cssFile.AppendLine("    --bs-btn-hover-bg: var(--app-primary-hover);");
            cssFile.AppendLine("    --bs-btn-hover-border-color: var(--app-primary-hover);");
            cssFile.AppendLine("    --bs-btn-active-bg: var(--app-primary-hover);");
            cssFile.AppendLine("    --bs-btn-active-border-color: var(--app-primary-hover);");
            cssFile.AppendLine("    --bs-btn-disabled-bg: var(--app-primary);");
            cssFile.AppendLine("    --bs-btn-disabled-border-color: var(--app-primary);");
            cssFile.AppendLine("}");
            cssFile.AppendLine();
            cssFile.AppendLine(".text-app-primary {");
            cssFile.AppendLine("    color: var(--app-primary) !important;");
            cssFile.AppendLine("}");
            cssFile.AppendLine();
            cssFile.AppendLine(".bg-app-primary {");
            cssFile.AppendLine("    background-color: var(--app-primary) !important;");
            cssFile.AppendLine("}");
            cssFile.AppendLine();
            cssFile.AppendLine(".table-actions {");
            cssFile.AppendLine("    width: 1%;");
            cssFile.AppendLine("    white-space: nowrap;");
            cssFile.AppendLine("}");
            cssFile.AppendLine();
            cssFile.AppendLine(".modal-backdrop.show {");
            cssFile.AppendLine("    opacity: 0.5;");
            cssFile.AppendLine("}");
            cssFile.AppendLine();
            cssFile.AppendLine("[data-bs-theme=\"dark\"] .btn-primary {");
            cssFile.AppendLine("    --bs-btn-color: #1a1a1a;");
            cssFile.AppendLine("    --bs-btn-hover-color: #1a1a1a;");
            cssFile.AppendLine("    --bs-btn-active-color: #1a1a1a;");
            cssFile.AppendLine("}");
            cssFile.AppendLine();
            cssFile.AppendLine("[data-bs-theme=\"dark\"] .text-body-secondary {");
            cssFile.AppendLine("    color: var(--app-dark-body-color) !important;");
            cssFile.AppendLine("}");
            cssFile.AppendLine();
            cssFile.AppendLine(".theme-toggle-button {");
            cssFile.AppendLine("    width: 38px;");
            cssFile.AppendLine("    height: 38px;");
            cssFile.AppendLine("}");

            File.WriteAllText(fileDirectory, cssFile.ToString());
        }
    }
}
