using System.Text;
using System;
using System.IO;

namespace Regras.FrontEndClasses
{
    public static class UtilCreator
    {
        public static bool Create(string projectPath)
        {
            bool success = true;

            try
            {
                string utilsPath = NamesHandler.GetDirectoryByType(projectPath, NamesHandler.FileType.Utils);
                CreateFunctions(utilsPath);
                CreateMasks(utilsPath);
                CreateThemeMode(utilsPath);
            }
            catch (Exception ex)
            {
                Util.CL_Files.LogException(ex);
                success = false;
            }

            return success;
        }

        private static void CreateMasks(string path)
        {
            StringBuilder js = new StringBuilder();
            js.AppendLine("export const maskCPF = (value) => {");
            js.AppendLine("    return value?.replace(/\\D/g, '').replace(/(\\d{3})(\\d)/, '$1.$2').replace(/(\\d{3})(\\d)/, '$1.$2').replace(/(\\d{3})(\\d{1,2})$/, '$1-$2');");
            js.AppendLine("};");
            js.AppendLine("");
            js.AppendLine("export const maskCNPJ = (value) => {");
            js.AppendLine("    return value?.replace(/\\D/g, '').replace(/(\\d{2})(\\d)/, '$1.$2').replace(/(\\d{3})(\\d)/, '$1.$2').replace(/(\\d{3})(\\d)/, '$1/$2').replace(/(\\d{4})(\\d{1,2})$/, '$1-$2');");
            js.AppendLine("};");
            js.AppendLine("");
            js.AppendLine("export const maskCEP = (value) => {");
            js.AppendLine("    return value?.replace(/\\D/g, '').replace(/(\\d{5})(\\d)/, '$1-$2').slice(0, 9);");
            js.AppendLine("};");
            js.AppendLine("");
            js.AppendLine("export const maskPhone = (phone) => {");
            js.AppendLine("    return phone?.replace(/\\D/g, '').replace(/^(\\d{2})(\\d)/, '($1) $2').replace(/(\\d{4,5})(\\d{4})$/, '$1-$2').slice(0, 15);");
            js.AppendLine("};");

            File.WriteAllText(path + "//masks.jsx", js.ToString());
        }

        private static void CreateThemeMode(string path)
        {
            StringBuilder js = new StringBuilder();
            js.AppendLine("const THEME_STORAGE_KEY = 'crudforms-theme';");
            js.AppendLine("");
            js.AppendLine("export const getStoredTheme = () => {");
            js.AppendLine("    if (typeof window === 'undefined') {");
            js.AppendLine("        return 'light';");
            js.AppendLine("    }");
            js.AppendLine("");
            js.AppendLine("    return localStorage.getItem(THEME_STORAGE_KEY) || 'light';");
            js.AppendLine("};");
            js.AppendLine("");
            js.AppendLine("export const applyTheme = (theme) => {");
            js.AppendLine("    const nextTheme = theme === 'dark' ? 'dark' : 'light';");
            js.AppendLine("    document.documentElement.setAttribute('data-bs-theme', nextTheme);");
            js.AppendLine("    localStorage.setItem(THEME_STORAGE_KEY, nextTheme);");
            js.AppendLine("    window.dispatchEvent(new CustomEvent('crudforms-theme-change', { detail: { theme: nextTheme } }));");
            js.AppendLine("    return nextTheme;");
            js.AppendLine("};");

            File.WriteAllText(path + "//themeMode.js", js.ToString());
        }

        private static void CreateFunctions(string path)
        {
            StringBuilder js = new StringBuilder();
            js.AppendLine("import { startOfWeek, addDays, format } from \"date-fns\";");
            js.AppendLine("");
            js.AppendLine("export const putDateOnPatternSimple = (date) => {");
            js.AppendLine("  if (!date) return ''; // Return empty string if date is null or undefined");
            js.AppendLine("");
            js.AppendLine("  const parsedDate = new Date(date);");
            js.AppendLine("");
            js.AppendLine("  if (isNaN(parsedDate.getTime())) {");
            js.AppendLine("    return ''; // Return empty string if parsedDate is invalid");
            js.AppendLine("  }");
            js.AppendLine("");
            js.AppendLine("  // Format day, month, and year");
            js.AppendLine("  const day = String(parsedDate.getDate()).padStart(2, '0');");
            js.AppendLine("  const month = String(parsedDate.getMonth() + 1).padStart(2, '0'); // Months are zero-based");
            js.AppendLine("  const year = parsedDate.getFullYear();");
            js.AppendLine("");
            js.AppendLine("  return `${day}/${month}/${year}`;");
            js.AppendLine("};");
            js.AppendLine("");
            js.AppendLine("export const putDateOnPattern = (date) => {");
            js.AppendLine("    if (!date) return ''; // Return empty string if date is null or undefined");
            js.AppendLine("");
            js.AppendLine("    const parsedDate = new Date(date);");
            js.AppendLine("");
            js.AppendLine("    if (isNaN(parsedDate.getTime())) {");
            js.AppendLine("      return ''; // Return empty string if parsedDate is invalid");
            js.AppendLine("    }");
            js.AppendLine("");
            js.AppendLine("    // Format day, month, and year");
            js.AppendLine("    const day = String(parsedDate.getDate()).padStart(2, '0');");
            js.AppendLine("    const month = String(parsedDate.getMonth() + 1).padStart(2, '0'); // Months are zero-based");
            js.AppendLine("    const year = parsedDate.getFullYear();");
            js.AppendLine("");
            js.AppendLine("    // Format hours and minutes");
            js.AppendLine("    const hours = String(parsedDate.getHours()).padStart(2, '0');");
            js.AppendLine("    const minutes = String(parsedDate.getMinutes()).padStart(2, '0');");
            js.AppendLine("");
            js.AppendLine("    return `${day}/${month}/${year} às ${hours}:${minutes}`;");
            js.AppendLine("};");
            js.AppendLine("");
            js.AppendLine("export const putDateOnPatternOnlyDate = (date) => {");
            js.AppendLine("  if (!date) return ''; // Return empty string if date is null or undefined");
            js.AppendLine("");
            js.AppendLine("  const parsedDate = new Date(date);");
            js.AppendLine("");
            js.AppendLine("  if (isNaN(parsedDate.getTime())) {");
            js.AppendLine("    return ''; // Return empty string if parsedDate is invalid");
            js.AppendLine("  }");
            js.AppendLine("");
            js.AppendLine("  // Format day, month, and year");
            js.AppendLine("  const day = String(parsedDate.getDate()).padStart(2, '0');");
            js.AppendLine("  const month = String(parsedDate.getMonth() + 1).padStart(2, '0'); // Months are zero-based");
            js.AppendLine("  const year = parsedDate.getFullYear();");
            js.AppendLine("");
            js.AppendLine("  return `${day}/${month}/${year}`;");
            js.AppendLine("};");
            js.AppendLine("");
            js.AppendLine("export const getLastDayOfWeek = (year, weekNumber) => {");
            js.AppendLine("    const firstDayOfYear = new Date(year, 0, 1);");
            js.AppendLine("    const daysOffset = (weekNumber - 1) * 7 + (firstDayOfYear.getDay() === 0 ? 0 : (8 - firstDayOfYear.getDay())); // Adjust for weeks starting on Monday");
            js.AppendLine("    const lastDayOfWeek = new Date(year, 0, daysOffset + 6);");
            js.AppendLine("    return lastDayOfWeek.toLocaleDateString();");
            js.AppendLine("};");
            js.AppendLine("");
            js.AppendLine("export const getLastDayOfWeekByDate = (date) => {");
            js.AppendLine("    const lastDayOfWeek = addDays(startOfWeek(date, { weekStartsOn: 1 }), 6); ");
            js.AppendLine("    return format(lastDayOfWeek, \"yyyy-MM-dd\"); // Format as YYYY-MM-DD");
            js.AppendLine("};");
            js.AppendLine("");
            js.AppendLine("export const toDateTimeLocalValue = (value) => {");
            js.AppendLine("  if (!value) return '';");
            js.AppendLine("");
            js.AppendLine("  const stringValue = String(value);");
            js.AppendLine("  const isoMatch = stringValue.match(/^(\\d{4}-\\d{2}-\\d{2})T(\\d{2}:\\d{2})/);");
            js.AppendLine("");
            js.AppendLine("  if (isoMatch) {");
            js.AppendLine("    return `${isoMatch[1]}T${isoMatch[2]}`;");
            js.AppendLine("  }");
            js.AppendLine("");
            js.AppendLine("  const localDateMatch = stringValue.match(/^(\\d{2})\\/(\\d{2})\\/(\\d{4})(?:\\s+(?:\\D+\\s*)?(\\d{2}):(\\d{2})(?::\\d{2})?)?$/);");
            js.AppendLine("");
            js.AppendLine("  if (localDateMatch) {");
            js.AppendLine("    const [, day, month, year, hour = '00', minute = '00'] = localDateMatch;");
            js.AppendLine("    return `${year}-${month}-${day}T${hour}:${minute}`;");
            js.AppendLine("  }");
            js.AppendLine("");
            js.AppendLine("  const parsedDate = new Date(value);");
            js.AppendLine("");
            js.AppendLine("  if (Number.isNaN(parsedDate.getTime())) {");
            js.AppendLine("    return '';");
            js.AppendLine("  }");
            js.AppendLine("");
            js.AppendLine("  const year = parsedDate.getFullYear();");
            js.AppendLine("  const month = String(parsedDate.getMonth() + 1).padStart(2, '0');");
            js.AppendLine("  const day = String(parsedDate.getDate()).padStart(2, '0');");
            js.AppendLine("  const hour = String(parsedDate.getHours()).padStart(2, '0');");
            js.AppendLine("  const minute = String(parsedDate.getMinutes()).padStart(2, '0');");
            js.AppendLine("");
            js.AppendLine("  return `${year}-${month}-${day}T${hour}:${minute}`;");
            js.AppendLine("};");
            js.AppendLine("");
            js.AppendLine("export const normalizeFormValueToApi = (field, value, dateFields = []) => {");
            js.AppendLine("  if (value === '') {");
            js.AppendLine("    return null;");
            js.AppendLine("  }");
            js.AppendLine("");
            js.AppendLine("  if (dateFields.includes(field) && value) {");
            js.AppendLine("    const dateTimeValue = toDateTimeLocalValue(value);");
            js.AppendLine("    return dateTimeValue ? `${dateTimeValue}:00` : null;");
            js.AppendLine("  }");
            js.AppendLine("");
            js.AppendLine("  return value;");
            js.AppendLine("};");
            js.AppendLine("");
            js.AppendLine("export const copyToMemory = async (text) => {");
            js.AppendLine("  if (!text) return false;");
            js.AppendLine("");
            js.AppendLine("  try {");
            js.AppendLine("      await navigator.clipboard.writeText(text);");
            js.AppendLine("      return true;");
            js.AppendLine("  } catch (err) {");
            js.AppendLine("      console.error(\"Erro ao copiar o texto: \", err);");
            js.AppendLine("      return false;");
            js.AppendLine("  }");
            js.AppendLine("};");

            File.WriteAllText(path + "//functions.jsx", js.ToString());
        }
    }
}
