using System;
using System.IO;
using System.Text;

namespace Regras.FrontEndClasses
{
    public static class LayoutsCreator
    {
        public static bool Create(string projectPath)
        {
            bool success = true;

            try
            {
                string layoutAdmin = NamesHandler.GetDirectoryByType(projectPath, NamesHandler.FileType.LayoutsAdmin);
                CreateAdminLayout(layoutAdmin);

                string layoutClient = NamesHandler.GetDirectoryByType(projectPath, NamesHandler.FileType.LayoutsClient);
                CreateClientLayout(layoutClient);
            }
            catch (Exception ex)
            {
                Util.CL_Files.LogException(ex);
                success = false;
            }

            return success;
        }

        private static void CreateAdminLayout(string path)
        {
            StringBuilder css = new StringBuilder();
            css.AppendLine(".admin-layout {");
            css.AppendLine("    min-height: 100vh;");
            css.AppendLine("}");
            css.AppendLine("");
            css.AppendLine(".admin-sidebar-shell {");
            css.AppendLine("    width: var(--app-sidebar-width);");
            css.AppendLine("}");
            css.AppendLine("");
            css.AppendLine(".admin-header {");
            css.AppendLine("    min-height: var(--app-header-height);");
            css.AppendLine("    display: flex;");
            css.AppendLine("    align-items: center;");
            css.AppendLine("    flex-shrink: 0;");
            css.AppendLine("}");
            css.AppendLine("");
            css.AppendLine(".admin-header > div {");
            css.AppendLine("    min-height: var(--app-header-height);");
            css.AppendLine("}");
            css.AppendLine("");
            css.AppendLine(".admin-sidebar {");
            css.AppendLine("    width: var(--app-sidebar-width);");
            css.AppendLine("    min-height: 100vh;");
            css.AppendLine("    background-color: var(--app-sidebar-bg);");
            css.AppendLine("}");
            css.AppendLine("");
            css.AppendLine(".admin-sidebar .nav-link {");
            css.AppendLine("    color: rgba(255, 255, 255, 0.82);");
            css.AppendLine("}");
            css.AppendLine("");
            css.AppendLine(".admin-sidebar .nav-link:hover,");
            css.AppendLine(".admin-sidebar .nav-link.active {");
            css.AppendLine("    color: #fff;");
            css.AppendLine("    background-color: var(--app-primary-hover);");
            css.AppendLine("}");
            css.AppendLine("");
            css.AppendLine(".admin-main {");
            css.AppendLine("    min-width: 0;");
            css.AppendLine("}");
            css.AppendLine("");
            css.AppendLine(".admin-children {");
            css.AppendLine("    max-width: var(--app-content-max-width);");
            css.AppendLine("    width: 100%;");
            css.AppendLine("}");
            css.AppendLine("");
            css.AppendLine(".sidebar-overlay {");
            css.AppendLine("    position: fixed;");
            css.AppendLine("    inset: 0;");
            css.AppendLine("    background: rgba(0, 0, 0, 0.35);");
            css.AppendLine("    z-index: 1030;");
            css.AppendLine("}");
            css.AppendLine("");
            css.AppendLine("@media (max-width: 991.98px) {");
            css.AppendLine("    .admin-sidebar-shell {");
            css.AppendLine("        position: fixed;");
            css.AppendLine("        inset: 0 auto 0 0;");
            css.AppendLine("        z-index: 1040;");
            css.AppendLine("        transform: translateX(-100%);");
            css.AppendLine("        transition: transform 0.2s ease-in-out;");
            css.AppendLine("    }");
            css.AppendLine("");
            css.AppendLine("    .admin-sidebar-shell.visible {");
            css.AppendLine("        transform: translateX(0);");
            css.AppendLine("    }");
            css.AppendLine("}");

            StringBuilder js = new StringBuilder();
            js.AppendLine("import React, { useState } from 'react';");
            js.AppendLine("import './AdminLayout.css';");
            js.AppendLine("import AdminSidebar from '../../components/admin/AdminSidebar/AdminSidebar';");
            js.AppendLine("import { useLocation, Link } from 'react-router-dom';");
            js.AppendLine("import AdminHeader from '../../components/admin/AdminHeader/AdminHeader';");
            js.AppendLine("");
            js.AppendLine("const AdminLayout = ({ children }) => {");
            js.AppendLine("    const location = useLocation();");
            js.AppendLine("    const pathSegments = location.pathname.split('/').filter(Boolean);");
            js.AppendLine("    const [isSidebarOpen, setIsSidebarOpen] = useState(false);");
            js.AppendLine("");
            js.AppendLine("    return (");
            js.AppendLine("        <div className=\"admin-layout d-flex bg-body-tertiary\">");
            js.AppendLine("            <div className={`admin-sidebar-shell d-lg-block ${isSidebarOpen ? 'visible' : ''}`}>");
            js.AppendLine("                <AdminSidebar />");
            js.AppendLine("            </div>");
            js.AppendLine("            {isSidebarOpen && <div className=\"sidebar-overlay d-lg-none\" onClick={() => setIsSidebarOpen(false)}></div>}");
            js.AppendLine("");
            js.AppendLine("            <div className=\"admin-main flex-grow-1 d-flex flex-column\">");
            js.AppendLine("                <AdminHeader onclickMenu={() => setIsSidebarOpen(true)} />");
            js.AppendLine("                <main className=\"admin-children mx-auto p-3 p-lg-4 flex-grow-1\">");
            js.AppendLine("                    {pathSegments.length > 0 && (");
            js.AppendLine("                        <nav aria-label=\"breadcrumb\" className=\"mb-3\">");
            js.AppendLine("                            <ol className=\"breadcrumb small mb-0\">");
            js.AppendLine("                                <li className=\"breadcrumb-item\"><Link to=\"/\">Inicio</Link></li>");
            js.AppendLine("                                {pathSegments.map((segment, index) => {");
            js.AppendLine("                                    const path = `/${pathSegments.slice(0, index + 1).join('/')}`;");
            js.AppendLine("                                    const isLast = index === pathSegments.length - 1;");
            js.AppendLine("                                    return isLast ? (");
            js.AppendLine("                                        <li key={path} className=\"breadcrumb-item active\" aria-current=\"page\">{segment}</li>");
            js.AppendLine("                                    ) : (");
            js.AppendLine("                                        <li key={path} className=\"breadcrumb-item\"><Link to={path}>{segment}</Link></li>");
            js.AppendLine("                                    );");
            js.AppendLine("                                })}");
            js.AppendLine("                            </ol>");
            js.AppendLine("                        </nav>");
            js.AppendLine("                    )}");
            js.AppendLine("                    {children}");
            js.AppendLine("                </main>");
            js.AppendLine("            </div>");
            js.AppendLine("        </div>");
            js.AppendLine("    );");
            js.AppendLine("};");
            js.AppendLine("");
            js.AppendLine("export default AdminLayout;");

            File.WriteAllText(path + "//AdminLayout.jsx", js.ToString());
            File.WriteAllText(path + "//AdminLayout.css", css.ToString());
        }

        private static void CreateClientLayout(string path)
        {
        }
    }
}
