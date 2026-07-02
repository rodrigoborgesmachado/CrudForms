using System;
using System.Collections.Generic;
using System.IO;
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
            catch(Exception ex)
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
                new Model.MD_FrontEndCssVariable("Background Colors", "--background-color", "#FAFAFA"),
                new Model.MD_FrontEndCssVariable("Background Colors", "--desktop-background-color", "#ECEFF1"),
                new Model.MD_FrontEndCssVariable("Primary Theme Colors", "--primary-color", "#1B4332"),
                new Model.MD_FrontEndCssVariable("Primary Theme Colors", "--primary-color-hover", "#142E24"),
                new Model.MD_FrontEndCssVariable("Primary Theme Colors", "--modal-primary-color", "rgba(27, 67, 50, 0.9)"),
                new Model.MD_FrontEndCssVariable("Panel Colors", "--secundary-color", "#D8E2DC"),
                new Model.MD_FrontEndCssVariable("Panel Colors", "--secundary-color-hover", "#B7C9B8"),
                new Model.MD_FrontEndCssVariable("Primary Button Colors", "--button-color-primary", "#1B4332"),
                new Model.MD_FrontEndCssVariable("Primary Button Colors", "--button-color-primary-hover", "#0F2C20"),
                new Model.MD_FrontEndCssVariable("Primary Button Colors", "--button-color-primary-disabled", "#9BB2A5"),
                new Model.MD_FrontEndCssVariable("Primary Button Colors", "--button-color-primary-right", "#2D6A4F"),
                new Model.MD_FrontEndCssVariable("Primary Button Colors", "--button-color-primary-right-hover", "#1F5237"),
                new Model.MD_FrontEndCssVariable("Primary Button Colors", "--button-color-primary-wrong", "#D62828"),
                new Model.MD_FrontEndCssVariable("Primary Button Colors", "--button-color-primary-wrong-hover", "#A61616"),
                new Model.MD_FrontEndCssVariable("Secondary Button Colors", "--button-color-secundary", "#3D5A80"),
                new Model.MD_FrontEndCssVariable("Secondary Button Colors", "--button-color-secundary-hover", "#293F50"),
                new Model.MD_FrontEndCssVariable("Secondary Button Colors", "--button-color-secundary-wrong", "#8D0801"),
                new Model.MD_FrontEndCssVariable("Secondary Button Colors", "--button-color-secundary-wrong-hover", "#5E0600"),
                new Model.MD_FrontEndCssVariable("Text Colors", "--title-color", "#102A43"),
                new Model.MD_FrontEndCssVariable("Text Colors", "--text-color-primary", "#2C3E50"),
                new Model.MD_FrontEndCssVariable("Text Colors", "--text-color-primary-light", "#52616B"),
                new Model.MD_FrontEndCssVariable("Text Colors", "--text-color-primary-dark", "#1A2A33"),
                new Model.MD_FrontEndCssVariable("Text Colors", "--text-color-secondary", "#FFFFFF"),
                new Model.MD_FrontEndCssVariable("Text Colors", "--text-color-accent", "#1B98E0"),
                new Model.MD_FrontEndCssVariable("Text Colors", "--text-color-code", "#1B4332"),
                new Model.MD_FrontEndCssVariable("Text Colors", "--text-color-button", "#FFFFFF"),
                new Model.MD_FrontEndCssVariable("Borders, Shadows, and Separators", "--separator-color-primary", "rgba(0, 0, 0, 0.12)"),
                new Model.MD_FrontEndCssVariable("Borders, Shadows, and Separators", "--separator-marker-color-primary", "#1B4332"),
                new Model.MD_FrontEndCssVariable("Borders, Shadows, and Separators", "--panel-border-color", "#D1D5DB"),
                new Model.MD_FrontEndCssVariable("Borders, Shadows, and Separators", "--shadow-color-primary", "rgba(0, 0, 0, 0.15)"),
                new Model.MD_FrontEndCssVariable("Borders, Shadows, and Separators", "--shadow-color-secondary", "rgba(0, 0, 0, 0.1)"),
                new Model.MD_FrontEndCssVariable("Surfaces and Blocks", "--block-color", "#FFFFFF"),
                new Model.MD_FrontEndCssVariable("Surfaces and Blocks", "--block-color-secundary", "#EDF2F7"),
                new Model.MD_FrontEndCssVariable("Surfaces and Blocks", "--color-surface", "#FFFFFF"),
                new Model.MD_FrontEndCssVariable("Surfaces and Blocks", "--color-surface-variant", "#E3F2FD"),
                new Model.MD_FrontEndCssVariable("Loader and Modal Colors", "--loader-color", "#1B4332"),
                new Model.MD_FrontEndCssVariable("Loader and Modal Colors", "--modal-background-color", "rgba(255, 255, 255, 0.95)"),
                new Model.MD_FrontEndCssVariable("Footer Height", "--footer-heigth", "48px"),
                new Model.MD_FrontEndCssVariable("Spacing and Layout", "--default-spacing", "8px"),
                new Model.MD_FrontEndCssVariable("Spacing and Layout", "--double-default-spacing", "calc(2 * var(--default-spacing))"),
                new Model.MD_FrontEndCssVariable("Spacing and Layout", "--triple-default-spacing", "calc(3 * var(--default-spacing))"),
                new Model.MD_FrontEndCssVariable("Spacing and Layout", "--default-max-width", "1080px"),
                new Model.MD_FrontEndCssVariable("Spacing and Layout", "--default-border-radius", "6px"),
                new Model.MD_FrontEndCssVariable("Spacing and Layout", "--default-border-radius-extra", "18px")
            };
        }

        private static void AppendCssVariables(StringBuilder cssFile, List<Model.MD_FrontEndCssVariable> cssVariables)
        {
            var variables = cssVariables ?? GetDefaultCssVariables();
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
            cssFile.AppendLine();
            AppendCssVariables(cssFile, cssVariables);
            cssFile.AppendLine();
            cssFile.AppendLine();
            cssFile.AppendLine("* {");
            cssFile.AppendLine("    padding: 0;");
            cssFile.AppendLine("    margin: 0;");
            cssFile.AppendLine("    box-sizing: border-box;");
            cssFile.AppendLine("}");
            cssFile.AppendLine();
            cssFile.AppendLine("body {");
            cssFile.AppendLine("    font-family: 'Jost';");
            cssFile.AppendLine("    background-color: var(--background-color);");
            cssFile.AppendLine("    color: var(--text-color-primary);");
            cssFile.AppendLine("    width: 100%;");
            cssFile.AppendLine("    height: 100%;");
            cssFile.AppendLine("    text-align: justify;");
            cssFile.AppendLine("}");
            cssFile.AppendLine();
            cssFile.AppendLine(".Toastify__progress-bar {");
            cssFile.AppendLine("    background-color: var(--primary-color) !important;");
            cssFile.AppendLine("}");
            cssFile.AppendLine();
            cssFile.AppendLine(".Toastify__toast-icon svg {");
            cssFile.AppendLine("    fill: var(--primary-color) !important; /* Adjust the icon color */");
            cssFile.AppendLine("}");
            cssFile.AppendLine();
            cssFile.AppendLine(".main-page {");
            cssFile.AppendLine("    padding: var(--double-default-spacing);");
            cssFile.AppendLine("    display: flex;");
            cssFile.AppendLine("    flex-direction: column;");
            cssFile.AppendLine("    align-items: center;");
            cssFile.AppendLine("    width: 100%;");
            cssFile.AppendLine("    background-color: var(--background-color);");
            cssFile.AppendLine("}");
            cssFile.AppendLine();
            cssFile.AppendLine(".main-button {");
            cssFile.AppendLine("    padding: var(--default-spacing);");
            cssFile.AppendLine("    background-color: var(--button-color-primary);");
            cssFile.AppendLine("    color: var(--text-color-button);");
            cssFile.AppendLine("    font-size: 16px;");
            cssFile.AppendLine("    font-weight: bold;");
            cssFile.AppendLine("    border: none;");
            cssFile.AppendLine("    border-radius: var(--default-border-radius);");
            cssFile.AppendLine("    cursor: pointer;");
            cssFile.AppendLine("    transition: background-color 0.3s;");
            cssFile.AppendLine("}");
            cssFile.AppendLine();
            cssFile.AppendLine(".main-button:hover {");
            cssFile.AppendLine("    background-color: var(--button-color-primary-hover);");
            cssFile.AppendLine("}");
            cssFile.AppendLine();
            cssFile.AppendLine(".main-button:disabled {");
            cssFile.AppendLine("    background-color: var(--button-color-primary-disabled);");
            cssFile.AppendLine("    cursor: not-allowed;");
            cssFile.AppendLine("}");
            cssFile.AppendLine();
            cssFile.AppendLine(".main-input {");
            cssFile.AppendLine("    width: 100%;");
            cssFile.AppendLine("    padding: 0.75rem;");
            cssFile.AppendLine("    border: 1px solid var(--primary-color);");
            cssFile.AppendLine("    border-radius: var(--default-border-radius);");
            cssFile.AppendLine("}");
            cssFile.AppendLine();
            cssFile.AppendLine(".div-with-border{");
            cssFile.AppendLine("    border: 1px solid #e0e0e0;");
            cssFile.AppendLine("}");
            cssFile.AppendLine();
            cssFile.AppendLine(".div-center{");
            cssFile.AppendLine("    display: flex;");
            cssFile.AppendLine("    align-items: center;");
            cssFile.AppendLine("    justify-content: center;");
            cssFile.AppendLine("}");
            cssFile.AppendLine();
            cssFile.AppendLine(".padding-default{");
            cssFile.AppendLine("    padding: 0.75rem;");
            cssFile.AppendLine("}");
            cssFile.AppendLine();
            cssFile.AppendLine(".margin-top-default{");
            cssFile.AppendLine("    margin-top: var(--default-spacing);");
            cssFile.AppendLine("}");
            cssFile.AppendLine();
            cssFile.AppendLine(".margin-top-double-default{");
            cssFile.AppendLine("    margin-top: var(--double-default-spacing);");
            cssFile.AppendLine("}");
            cssFile.AppendLine();
            cssFile.AppendLine(".margin-top-triple-default{");
            cssFile.AppendLine("    margin-top: var(--triple-default-spacing);");
            cssFile.AppendLine("}");
            cssFile.AppendLine();
            cssFile.AppendLine(".brighten-on-hover:hover {");
            cssFile.AppendLine("    filter: brightness(1.2); /* Increase brightness by 20% */");
            cssFile.AppendLine("}");
            cssFile.AppendLine();
            cssFile.AppendLine(".clickable {");
            cssFile.AppendLine("    cursor: pointer;");
            cssFile.AppendLine("}");
            cssFile.AppendLine();
            cssFile.AppendLine(".option-link {");
            cssFile.AppendLine("    position: relative;");
            cssFile.AppendLine("    cursor: pointer;");
            cssFile.AppendLine("}");
            cssFile.AppendLine();
            cssFile.AppendLine(".option-link::after {");
            cssFile.AppendLine("    content: '';");
            cssFile.AppendLine("    position: absolute;");
            cssFile.AppendLine("    left: 0;");
            cssFile.AppendLine("    bottom: 0;");
            cssFile.AppendLine("    width: 0;");
            cssFile.AppendLine("    height: 2px;");
            cssFile.AppendLine("    background-color: var(--primary-color);");
            cssFile.AppendLine("    transition: width 0.5s ease-in-out; ");
            cssFile.AppendLine("}");
            cssFile.AppendLine();
            cssFile.AppendLine(".option-link:hover::after {");
            cssFile.AppendLine("    width: 100%;");
            cssFile.AppendLine("}");
            cssFile.AppendLine();
            cssFile.AppendLine(".right-component {");
            cssFile.AppendLine("    display: flex;");
            cssFile.AppendLine("    justify-content: flex-end;");
            cssFile.AppendLine("    width: 100%;");
            cssFile.AppendLine("}");
            cssFile.AppendLine();
            cssFile.AppendLine(".center-component {");
            cssFile.AppendLine("    display: flex;");
            cssFile.AppendLine("    justify-content: center;");
            cssFile.AppendLine("    align-items: center;");
            cssFile.AppendLine("}");
            cssFile.AppendLine();
            cssFile.AppendLine(".gap-default{");
            cssFile.AppendLine("    gap: var(--default-spacing);");
            cssFile.AppendLine("}");
            cssFile.AppendLine();
            cssFile.AppendLine(".flex-row{");
            cssFile.AppendLine("    display: flex !important;");
            cssFile.AppendLine("    flex-direction: row !important;");
            cssFile.AppendLine("}");
            cssFile.AppendLine();
            cssFile.AppendLine(".flex-column{");
            cssFile.AppendLine("    display: flex;");
            cssFile.AppendLine("    flex-direction: column;");
            cssFile.AppendLine("}");
            cssFile.AppendLine();
            cssFile.AppendLine(".space-between {");
            cssFile.AppendLine("    justify-content: space-between;");
            cssFile.AppendLine("}");
            cssFile.AppendLine();
            cssFile.AppendLine(".margin-bottom-default{");
            cssFile.AppendLine("    margin-bottom: var(--default-spacing);");
            cssFile.AppendLine("}");
            cssFile.AppendLine();
            cssFile.AppendLine(".margin-bottom-double-default{");
            cssFile.AppendLine("    margin-bottom: var(--double-default-spacing);");
            cssFile.AppendLine("}");
            cssFile.AppendLine();
            cssFile.AppendLine(".padding-bottom-4-px{");
            cssFile.AppendLine("    padding-bottom: 4px;");
            cssFile.AppendLine("}");
            cssFile.AppendLine();
            cssFile.AppendLine(".title{");
            cssFile.AppendLine("    font-size: 16px;");
            cssFile.AppendLine("    color: var(--title-color);");
            cssFile.AppendLine("    font-weight: 500;");
            cssFile.AppendLine("    margin: 0;");
            cssFile.AppendLine("}");
            cssFile.AppendLine();
            cssFile.AppendLine(".small-font{");
            cssFile.AppendLine("    font-size: x-small;");
            cssFile.AppendLine("}");
            cssFile.AppendLine();
            cssFile.AppendLine(".small-input {");
            cssFile.AppendLine("    width: 64px;");
            cssFile.AppendLine("    text-align: center;");
            cssFile.AppendLine("}");
            cssFile.AppendLine();
            cssFile.AppendLine(".space-deffault-bottom{");
            cssFile.AppendLine("    margin-bottom: var(--default-spacing);");
            cssFile.AppendLine("}");
            cssFile.AppendLine();
            cssFile.AppendLine(".space-double-bottom{");
            cssFile.AppendLine("    margin-bottom: var(--double-default-spacing);");
            cssFile.AppendLine("}");
            cssFile.AppendLine();
            cssFile.AppendLine(".space-triple-bottom{");
            cssFile.AppendLine("    margin-bottom: var(--triple-default-spacing);");
            cssFile.AppendLine("}");
            cssFile.AppendLine();
            cssFile.AppendLine(".box {");
            cssFile.AppendLine("    border: 1px solid #ddd;");
            cssFile.AppendLine("    padding: 20px;");
            cssFile.AppendLine("    border-radius: 5px;");
            cssFile.AppendLine("}");
            cssFile.AppendLine();
            cssFile.AppendLine(".box-title{");
            cssFile.AppendLine("    display: flex;");
            cssFile.AppendLine("    flex-direction: row;");
            cssFile.AppendLine("    align-items: center;");
            cssFile.AppendLine("    gap: 8px");
            cssFile.AppendLine("}");
            cssFile.AppendLine();
            cssFile.AppendLine(".flex-space-between{");
            cssFile.AppendLine("    display: flex;");
            cssFile.AppendLine("    justify-content: space-between;");
            cssFile.AppendLine("    width: 100%;");
            cssFile.AppendLine("}");
            cssFile.AppendLine();
            cssFile.AppendLine(".item-active{");
            cssFile.AppendLine("    background-color: var(--button-color-primary-right);");
            cssFile.AppendLine("    color: var(--text-color-secondary) !important;");
            cssFile.AppendLine("    padding: var(--default-spacing);");
            cssFile.AppendLine("}");
            cssFile.AppendLine();
            cssFile.AppendLine(".item-inactive{");
            cssFile.AppendLine("    background-color: var(--button-color-primary-wrong);");
            cssFile.AppendLine("    color: var(--text-color-primary) !important;");
            cssFile.AppendLine("    padding: var(--default-spacing);");
            cssFile.AppendLine("}");
            cssFile.AppendLine();
            cssFile.AppendLine(".item-in-progress{");
            cssFile.AppendLine("    background-color: var(--button-color-primary-right);");
            cssFile.AppendLine("    color: var(--text-color-primary) !important;");
            cssFile.AppendLine("    padding: var(--default-spacing);");
            cssFile.AppendLine("}");
            cssFile.AppendLine();
            cssFile.AppendLine(".modal-backdrop {");
            cssFile.AppendLine("    position: fixed;");
            cssFile.AppendLine("    top: 0;");
            cssFile.AppendLine("    left: 0;");
            cssFile.AppendLine("    right: 0;");
            cssFile.AppendLine("    bottom: 0;");
            cssFile.AppendLine("    background: rgba(0, 0, 0, 0.5);");
            cssFile.AppendLine("    display: flex;");
            cssFile.AppendLine("    justify-content: center;");
            cssFile.AppendLine("    align-items: center;");
            cssFile.AppendLine("    z-index: 1000;");
            cssFile.AppendLine("}");
            cssFile.AppendLine();
            cssFile.AppendLine(".modal-container {");
            cssFile.AppendLine("    background-color: var(--modal-primary-color);");
            cssFile.AppendLine("    padding: 2rem;");
            cssFile.AppendLine("    border-radius: var(--default-border-radius-extra);");
            cssFile.AppendLine("    width: 100%;");
            cssFile.AppendLine("    max-width: 852px;");
            cssFile.AppendLine("    color: var(--text-color-secondary);");
            cssFile.AppendLine("    box-shadow: 0 4px 10px rgba(0, 0, 0, 0.2);");
            cssFile.AppendLine("    position: relative;");
            cssFile.AppendLine("}");
            cssFile.AppendLine();
            cssFile.AppendLine(".close-button {");
            cssFile.AppendLine("    position: absolute;");
            cssFile.AppendLine("    top: 0.5rem;");
            cssFile.AppendLine("    right: 1rem;");
            cssFile.AppendLine("    font-size: 2.5rem;");
            cssFile.AppendLine("    background: none;");
            cssFile.AppendLine("    border: none;");
            cssFile.AppendLine("    color: var(--text-color-secondary);");
            cssFile.AppendLine("    cursor: pointer;");
            cssFile.AppendLine("}");
            cssFile.AppendLine();
            cssFile.AppendLine(".modal-content{");
            cssFile.AppendLine("    width: 100%;");
            cssFile.AppendLine("    height: 100%;");
            cssFile.AppendLine("    display: flex;");
            cssFile.AppendLine("    justify-content: space-between;");
            cssFile.AppendLine("    align-items: center;");
            cssFile.AppendLine("}");
            cssFile.AppendLine();
            cssFile.AppendLine(".modal-container-generic{");
            cssFile.AppendLine("    background-color: var(--modal-primary-color);");
            cssFile.AppendLine("    padding: 2rem;");
            cssFile.AppendLine("    border-radius: var(--default-border-radius-extra);");
            cssFile.AppendLine("    color: var(--text-color-secondary);");
            cssFile.AppendLine("    box-shadow: 0 4px 10px rgba(0, 0, 0, 0.2);");
            cssFile.AppendLine("    position: relative;");
            cssFile.AppendLine("}");
            cssFile.AppendLine();
            cssFile.AppendLine(".modal-title {");
            cssFile.AppendLine("    font-size: 1.5rem;");
            cssFile.AppendLine("    margin-bottom: 1rem;");
            cssFile.AppendLine("}");
            cssFile.AppendLine();
            cssFile.AppendLine(".modal-message {");
            cssFile.AppendLine("    font-size: 1rem;");
            cssFile.AppendLine("    margin-bottom: 1.5rem;");
            cssFile.AppendLine("}");
            cssFile.AppendLine();
            cssFile.AppendLine(".modal-actions {");
            cssFile.AppendLine("    display: flex;");
            cssFile.AppendLine("    flex-direction: column;");
            cssFile.AppendLine("    width: 100%;");
            cssFile.AppendLine("    justify-content: space-around;");
            cssFile.AppendLine("    margin-top: 8px;");
            cssFile.AppendLine("    gap: 1rem;");
            cssFile.AppendLine("}");
            cssFile.AppendLine();
            cssFile.AppendLine(".modal-button {");
            cssFile.AppendLine("    padding: 0.5rem 1rem;");
            cssFile.AppendLine("    border: none;");
            cssFile.AppendLine("    border-radius: 4px;");
            cssFile.AppendLine("    cursor: pointer;");
            cssFile.AppendLine("}");
            cssFile.AppendLine();
            cssFile.AppendLine(".form-group {");
            cssFile.AppendLine("    margin-bottom: var(--default-spacing);");
            cssFile.AppendLine("}");
            cssFile.AppendLine();
            cssFile.AppendLine(".form-group label {");
            cssFile.AppendLine("    display: block;");
            cssFile.AppendLine("    margin-bottom: 0.5rem;");
            cssFile.AppendLine("}");
            cssFile.AppendLine();
            cssFile.AppendLine(".form-group input {");
            cssFile.AppendLine("    padding: 0.5rem;");
            cssFile.AppendLine("    border: 1px solid var(--primary-color);");
            cssFile.AppendLine("    border-radius: var(--default-border-radius);");
            cssFile.AppendLine("}");
            cssFile.AppendLine();
            cssFile.AppendLine(".form-group select {");
            cssFile.AppendLine("    padding: 0.5rem;");
            cssFile.AppendLine("    border: 1px solid var(--primary-color);");
            cssFile.AppendLine("    border-radius: var(--default-border-radius);");
            cssFile.AppendLine("}");
            cssFile.AppendLine();
            cssFile.AppendLine(".title-with-options{");
            cssFile.AppendLine("    display: flex;");
            cssFile.AppendLine("    width: 100%;");
            cssFile.AppendLine("    justify-content: space-between;");
            cssFile.AppendLine("    margin-bottom: var(--default-spacing);");
            cssFile.AppendLine("    flex-wrap: wrap;");
            cssFile.AppendLine("}");
            cssFile.AppendLine();
            cssFile.AppendLine(".display-none{");
            cssFile.AppendLine("    display: none !important;");
            cssFile.AppendLine("}");
            cssFile.AppendLine();
            cssFile.AppendLine(".info-group{");
            cssFile.AppendLine("    display: flex;");
            cssFile.AppendLine("    flex-direction: column;");
            cssFile.AppendLine("    gap: var(--default-spacing);");
            cssFile.AppendLine("}");
            cssFile.AppendLine();
            cssFile.AppendLine(".wrap{");
            cssFile.AppendLine("    flex-wrap: wrap;");
            cssFile.AppendLine("}");

            File.WriteAllText(fileDirectory, cssFile.ToString());
        }
    }
}
