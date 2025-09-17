using Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Regras.FrontEndClasses
{
    public static class RoutesCreator
    {
        public static bool Create(List<MD_Tabela> tabelas, string projectPath)
        {
            bool success = true;

            try
            {
                string routesAdmin = NamesHandler.GetDirectoryByType(projectPath, NamesHandler.FileType.RoutesAdmin);
                CreateAdminRoutes(tabelas, routesAdmin);
            }
            catch (Exception ex)
            {
                Util.CL_Files.LogException(ex);
                success = false;
            }

            return success;
        }

        private static void CreateAdminRoutes(List<MD_Tabela> tabelas, string path)
        {
            StringBuilder js = new StringBuilder();
            js.AppendLine("import React from 'react';");
            js.AppendLine("import { Route, Routes } from 'react-router-dom';");
            js.AppendLine("import AdminLayout from '../../layouts/admin/AdminLayout';");
            js.AppendLine("import DashboardPage from '../../pages/admin/DashBoard/DashboardPage';");

            foreach (var tabela in tabelas)
            {
                string componentName = NamesHandler.CreateComponentName(tabela.DAO.Nome);
                string name = NamesHandler.CreateComponentListName(tabela.DAO.Nome);
                js.AppendLine($"import {name} from '../../pages/admin/{componentName}/{name}/{name}';");
            }
            js.AppendLine("");
            js.AppendLine("const AdminRoutes = () => (");
            js.AppendLine("  <AdminLayout>");
            js.AppendLine("    <Routes>");
            js.AppendLine("      <Route path=\"/\" element={<DashboardPage />} />");
            foreach (var tabela in tabelas)
            {
                string nameList = NamesHandler.CreateComponentListName(tabela.DAO.Nome);
                string namePage = NamesHandler.CreateComponentPageName(tabela.DAO.Nome);
                js.AppendLine($"      <Route path=\"/{tabela.DAO.Nome}\" element={{<{nameList} />}} />");
                js.AppendLine($"      <Route path=\"/{tabela.DAO.Nome}/:code\" element={{<{namePage} />}} />");
            }
            js.AppendLine("      <Route path=\"/*\" element={<DashboardPage />} />");
            js.AppendLine("      </Routes>");
            js.AppendLine("  </AdminLayout>");
            js.AppendLine(");");
            js.AppendLine("");
            js.AppendLine("export default AdminRoutes;");
            js.AppendLine("");

            File.WriteAllText(path + "//AdminRoutes.js", js.ToString());
        }
    }
}
