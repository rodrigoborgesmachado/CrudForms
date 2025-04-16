using Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace Regras.FrontEndClasses
{
    public static class ComponentsCreator
    {
        public static bool Create(List<MD_Tabela> tabelas, string projectPath)
        {
            bool success = true;

            try
            {
                string adminComponents = NamesHandler.GetDirectoryByType(projectPath, NamesHandler.FileType.ComponentsAdmin);
                success &= CreateAdminFoldersAndComponents(tabelas, adminComponents);

                string clientComponents = NamesHandler.GetDirectoryByType(projectPath, NamesHandler.FileType.ComponentsClient);
                success &= CreateClientFoldersAndComponents(clientComponents);

                string commonComponents = NamesHandler.GetDirectoryByType(projectPath, NamesHandler.FileType.ComponentsCommon);
                success &= CreateCommonFoldersAndComponents(commonComponents);

                string iconsComponents = NamesHandler.GetDirectoryByType(projectPath, NamesHandler.FileType.ComponentsIcons);
                success &= CreateIconFoldersAndComponents(iconsComponents);
            }
            catch (Exception ex)
            {
                Util.CL_Files.LogException(ex);
                success = false;
            }

            return success;
        }

        private static bool CreateIconFoldersAndComponents(string iconPath)
        {
            bool success = true;

            try
            {
                var assembly = Assembly.GetExecutingAssembly();
                string resourceNamespace = "Pj.FrontEndComponents.Icons";

                string[] resourceNames = assembly.GetManifestResourceNames();

                foreach (var resourceName in resourceNames)
                {
                    if (resourceName.StartsWith(resourceNamespace))
                    {
                        string fileName = resourceName.Substring(resourceNamespace.Length + 1);

                        string outputPath = Path.Combine(iconPath, fileName);

                        // Read the embedded resource
                        using (Stream resourceStream = assembly.GetManifestResourceStream(resourceName))
                        using (StreamReader reader = new StreamReader(resourceStream))
                        {
                            string fileContent = reader.ReadToEnd();

                            // Write to the new file
                            File.WriteAllText(outputPath, fileContent);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                success = false;
                Util.CL_Files.LogException(ex);
            }

            return success;
        }

        private static bool CreateCommonFoldersAndComponents(string commonComponent)
        {
            bool success = true;

            try
            {
                string modalsPath = Path.Combine(commonComponent, "Modals");
                Directory.CreateDirectory(modalsPath);

                string confirmModalPath = Path.Combine(modalsPath, "Modals");
                Directory.CreateDirectory(confirmModalPath);
                CreateConfirmModalFiles(confirmModalPath);

                string loadingModalPath = Path.Combine(modalsPath, "LoadingModal");
                Directory.CreateDirectory(loadingModalPath);
                CreateLoadingModalFiles(loadingModalPath);

                string messageModalPath = Path.Combine(modalsPath, "MessageModal");
                Directory.CreateDirectory(messageModalPath);
                CreateMessageModalFiles(messageModalPath);

                string paginationPath = Path.Combine(commonComponent, "Pagination");
                Directory.CreateDirectory(paginationPath);
                CreatePaginationFiles(paginationPath);
            }
            catch (Exception ex)
            {
                success = false;
                Util.CL_Files.LogException(ex);
            }

            return success;
        }

        private static void CreatePaginationFiles(string path)
        {
            StringBuilder css = new StringBuilder();
            css.AppendLine(".pagination {");
            css.AppendLine("    display: flex;");
            css.AppendLine("    align-items: center;");
            css.AppendLine("    justify-content: center;");
            css.AppendLine("    gap: 0.5rem;");
            css.AppendLine("    margin-top: 1rem;");
            css.AppendLine("}");
            css.AppendLine("");
            css.AppendLine(".pagination button {");
            css.AppendLine("    padding: 0.4rem 0.6rem;");
            css.AppendLine("    border: none;");
            css.AppendLine("    background-color: transparent;");
            css.AppendLine("    cursor: pointer;");
            css.AppendLine("    border-radius: 4px;");
            css.AppendLine("    transition: background-color 0.2s ease;");
            css.AppendLine("}");
            css.AppendLine("");
            css.AppendLine(".pagination .active {");
            css.AppendLine("    background-color: #333333;");
            css.AppendLine("    color: #ffffff;");
            css.AppendLine("}");
            css.AppendLine("");
            css.AppendLine(".pagination .ellipsis {");
            css.AppendLine("    padding: 0.4rem 0.6rem;");
            css.AppendLine("    color: #666666;");
            css.AppendLine("}");
            css.AppendLine("");
            css.AppendLine(".page-button {");
            css.AppendLine("    color: inherit;");
            css.AppendLine("    background-color: inherit;");
            css.AppendLine("}");
            css.AppendLine("");
            css.AppendLine("button:disabled {");
            css.AppendLine("    cursor: not-allowed;");
            css.AppendLine("    opacity: 0.5;");
            css.AppendLine("}");
            css.AppendLine("");
            css.AppendLine("@media (max-width: 1000px) {");
            css.AppendLine("    .pagination{");
            css.AppendLine("        gap: 0px;");
            css.AppendLine("    }");
            css.AppendLine("}");

            StringBuilder js = new StringBuilder();
            js.AppendLine("// src/components/Pagination/Pagination.js");
            js.AppendLine("");
            js.AppendLine("import React from 'react';");
            js.AppendLine("import './Pagination.css';");
            js.AppendLine("");
            js.AppendLine("const Pagination = ({ page, totalPages, onPageChange }) => {");
            js.AppendLine("  const getPages = () => {");
            js.AppendLine("    const pages = [];");
            js.AppendLine("    if (totalPages <= 5) {");
            js.AppendLine("      for (let i = 1; i <= totalPages; i++) {");
            js.AppendLine("        pages.push(i);");
            js.AppendLine("      }");
            js.AppendLine("    } else {");
            js.AppendLine("      if (page <= 3) {");
            js.AppendLine("        pages.push(1, 2, 3, '...', totalPages);");
            js.AppendLine("      } else if (page > totalPages - 3) {");
            js.AppendLine("        pages.push(1, '...', totalPages - 2, totalPages - 1, totalPages);");
            js.AppendLine("      } else {");
            js.AppendLine("        pages.push(1, '...', page - 1, page, page + 1, '...', totalPages);");
            js.AppendLine("      }");
            js.AppendLine("    }");
            js.AppendLine("    return pages;");
            js.AppendLine("  };");
            js.AppendLine("");
            js.AppendLine("  return (");
            js.AppendLine("    <div className=\"pagination\">");
            js.AppendLine("      <button ");
            js.AppendLine("        className={page === 1 ? '' : 'brighten-on-hover'} ");
            js.AppendLine("        onClick={() => onPageChange(page - 1)} ");
            js.AppendLine("        disabled={page === 1}");
            js.AppendLine("      >");
            js.AppendLine("        ← Anterior");
            js.AppendLine("      </button>");
            js.AppendLine("      {getPages().map((pageNumber, index) =>");
            js.AppendLine("        pageNumber === '...' ? (");
            js.AppendLine("          <span key={index} className=\"ellipsis\">...</span>");
            js.AppendLine("        ) : (");
            js.AppendLine("          <button");
            js.AppendLine("            key={index}");
            js.AppendLine("            className={`page-button ${pageNumber === page ? 'active' : 'brighten-on-hover'}`}");
            js.AppendLine("            onClick={() => onPageChange(pageNumber)}");
            js.AppendLine("          >");
            js.AppendLine("            {pageNumber}");
            js.AppendLine("          </button>");
            js.AppendLine("        )");
            js.AppendLine("      )}");
            js.AppendLine("      <button ");
            js.AppendLine("        className={page === totalPages ? '' : 'brighten-on-hover'} ");
            js.AppendLine("        onClick={() => onPageChange(page + 1)} ");
            js.AppendLine("        disabled={page === totalPages}");
            js.AppendLine("      >");
            js.AppendLine("        Próximo →");
            js.AppendLine("      </button>");
            js.AppendLine("    </div>");
            js.AppendLine("  );");
            js.AppendLine("};");
            js.AppendLine("");
            js.AppendLine("export default Pagination;");
            js.AppendLine("");

            File.WriteAllText(path + "//Pagination.js", js.ToString());
            File.WriteAllText(path + "//Pagination.css", css.ToString());
        }

        private static void CreateMessageModalFiles(string path)
        {
            StringBuilder js = new StringBuilder();
            js.AppendLine("import React from 'react';");
            js.AppendLine("");
            js.AppendLine("const MessageModal = ({isOpen, message, click, optionText = 'fechar'}) => {");
            js.AppendLine("");
            js.AppendLine("    if (!isOpen) return null;");
            js.AppendLine("");
            js.AppendLine("    return (");
            js.AppendLine("        <div className=\"loading-backdrop\">");
            js.AppendLine("        <div className=\"loading-modal\">");
            js.AppendLine("            <span>{message}</span>");
            js.AppendLine("            <button className='main-button margin-top-default' onClick={click}>{optionText}</button>");
            js.AppendLine("        </div>");
            js.AppendLine("        </div>");
            js.AppendLine("    );");
            js.AppendLine("};");
            js.AppendLine("");
            js.AppendLine("export default MessageModal;");

            File.WriteAllText(path + "//MessageModal.js", js.ToString());
        }

        private static void CreateLoadingModalFiles(string path)
        {
            StringBuilder css = new StringBuilder();
            css.AppendLine(".loading-backdrop {");
            css.AppendLine("    position: fixed;");
            css.AppendLine("    top: 0;");
            css.AppendLine("    left: 0;");
            css.AppendLine("    right: 0;");
            css.AppendLine("    bottom: 0;");
            css.AppendLine("    background-color: rgba(0, 0, 0, 0.5); /* Background with transparency */");
            css.AppendLine("    display: flex;");
            css.AppendLine("    justify-content: center;");
            css.AppendLine("    align-items: center;");
            css.AppendLine("    z-index: 9999; /* Ensures it's always on top */");
            css.AppendLine("}");
            css.AppendLine("");
            css.AppendLine(".loading-modal {");
            css.AppendLine("    background-color: white;");
            css.AppendLine("    padding: 2rem;");
            css.AppendLine("    border-radius: var(--default-border-radius-extra);");
            css.AppendLine("    text-align: center;");
            css.AppendLine("    display: flex;");
            css.AppendLine("    flex-direction: column;");
            css.AppendLine("    align-items: center;");
            css.AppendLine("}");
            css.AppendLine("");
            css.AppendLine(".spinner {");
            css.AppendLine("    border: 4px solid rgba(0, 0, 0, 0.1);");
            css.AppendLine("    border-top: 4px solid var(--primary-color); /* Use your primary color */");
            css.AppendLine("    border-radius: 50%;");
            css.AppendLine("    width: 40px;");
            css.AppendLine("    height: 40px;");
            css.AppendLine("    animation: spin 1s linear infinite;");
            css.AppendLine("}");
            css.AppendLine("");
            css.AppendLine(".super-z-index{");
            css.AppendLine("    z-index: 99999; /* Ensures it's always on top */");
            css.AppendLine("}");
            css.AppendLine("");
            css.AppendLine("@keyframes spin {");
            css.AppendLine("    0% { transform: rotate(0deg); }");
            css.AppendLine("    100% { transform: rotate(360deg); }");
            css.AppendLine("}");

            StringBuilder js = new StringBuilder();
            js.AppendLine("import React from 'react';");
            js.AppendLine("import './LoadingModal.css';");
            js.AppendLine("");
            js.AppendLine("const LoadingModal = () => {");
            js.AppendLine("  return (");
            js.AppendLine("    <div className=\"loading-backdrop super-z-index\">");
            js.AppendLine("      <div className=\"loading-modal\">");
            js.AppendLine("        <div className=\"spinner\"></div>");
            js.AppendLine("        <span>Carregando...</span>");
            js.AppendLine("      </div>");
            js.AppendLine("    </div>");
            js.AppendLine("  );");
            js.AppendLine("};");
            js.AppendLine("");
            js.AppendLine("export default LoadingModal;");

            File.WriteAllText(path + "//LoadingModal.js", js.ToString());
            File.WriteAllText(path + "//LoadingModal.css", css.ToString());
        }

        private static void CreateConfirmModalFiles(string path)
        {
            StringBuilder css = new StringBuilder();
            css.AppendLine(".modal-yes {");
            css.AppendLine("    background-color: #4CAF50;");
            css.AppendLine("    color: white;");
            css.AppendLine("}");
            css.AppendLine("");
            css.AppendLine(".modal-no {");
            css.AppendLine("    background-color: #F44336;");
            css.AppendLine("    color: white;");
            css.AppendLine("}");

            StringBuilder js = new StringBuilder();
            js.AppendLine("// src/components/modals/ConfirmModal.js");
            js.AppendLine("");
            js.AppendLine("import React from 'react';");
            js.AppendLine("import './ConfirmModal.css'; // Create a CSS file for modal styling");
            js.AppendLine("");
            js.AppendLine("const ConfirmModal = ({ isOpen, title, message, onYes, onNo, confirmData, yesLabel = \"Yes\", noLabel = \"No\" }) => {");
            js.AppendLine("  if (!isOpen) return null;");
            js.AppendLine("");
            js.AppendLine("  return (");
            js.AppendLine("    <div className=\"loading-backdrop\">");
            js.AppendLine("      <div className=\"modal-container-generic\">");
            js.AppendLine("        <h2 className=\"modal-title\">{title}</h2>");
            js.AppendLine("        <p className=\"modal-message\">{message}</p>");
            js.AppendLine("        <div className=\"modal-actions\">");
            js.AppendLine("          <button className=\"modal-button modal-yes\" onClick={() => onYes(confirmData)}>{yesLabel}</button>");
            js.AppendLine("          <button className=\"modal-button modal-no\" onClick={onNo}>{noLabel}</button>");
            js.AppendLine("        </div>");
            js.AppendLine("      </div>");
            js.AppendLine("    </div>");
            js.AppendLine("  );");
            js.AppendLine("};");
            js.AppendLine("");
            js.AppendLine("export default ConfirmModal;");

            File.WriteAllText(path + "//ConfirmModal.js", js.ToString());
            File.WriteAllText(path + "//ConfirmModal.css", css.ToString());
        }

        private static bool CreateClientFoldersAndComponents(string client)
        {
            bool success = true;

            try
            {
            }
            catch (Exception ex)
            {
                success = false;
                Util.CL_Files.LogException(ex);
            }

            return success;
        }

        private static bool CreateAdminFoldersAndComponents(List<MD_Tabela> tabelas, string adminComponent)
        {
            bool success = true;

            try
            {
                string adminHeaderFolder = Path.Combine(adminComponent, "AdminHeader");
                Directory.CreateDirectory(adminHeaderFolder);
                CreateAdminHeaderFiles(adminHeaderFolder);

                string adminSideBarFolder = Path.Combine(adminComponent, "AdminSidebar");
                Directory.CreateDirectory(adminSideBarFolder);
                CreateAdminSideBarFiles(tabelas, adminSideBarFolder);

                string adminFilterComponentFolder = Path.Combine(adminComponent, "FilterComponent");
                Directory.CreateDirectory(adminFilterComponentFolder);
                CreateAdminFilterComponentFiles(adminFilterComponentFolder);

                string adminModalsFolder = Path.Combine(adminComponent, "Modals");
                Directory.CreateDirectory(adminModalsFolder);

                string adminModalsAddUserModalFolder = Path.Combine(adminModalsFolder, "AddUserModal");
                Directory.CreateDirectory(adminModalsAddUserModalFolder);
                CreateAdminAddUserModalFiles(adminModalsAddUserModalFolder);
            }
            catch (Exception ex)
            {
                success = false;
                Util.CL_Files.LogException(ex);
            }

            return success;
        }

        private static void CreateAdminHeaderFiles(string path)
        {
            StringBuilder css = new StringBuilder();
            css.AppendLine(".admin-header {");
            css.AppendLine("    width: 100%;");
            css.AppendLine("    max-height: 6rem;");
            css.AppendLine("    display: flex;");
            css.AppendLine("    justify-content: center;");
            css.AppendLine("    background-color: var(--background-color);");
            css.AppendLine("    border-bottom: 8px solid var(--primary-color);");
            css.AppendLine("}");
            css.AppendLine("");
            css.AppendLine(".admin-header__all{");
            css.AppendLine("    width: 100%;");
            css.AppendLine("    display: flex;");
            css.AppendLine("    justify-content: space-between;");
            css.AppendLine("    align-items: center;");
            css.AppendLine("    padding: 1rem;");
            css.AppendLine("}");
            css.AppendLine("");
            css.AppendLine(".admin-header__left h1 {");
            css.AppendLine("    font-size: 2rem;");
            css.AppendLine("    margin: 0;");
            css.AppendLine("}");
            css.AppendLine("");
            css.AppendLine(".admin-header__right {");
            css.AppendLine("    display: flex;");
            css.AppendLine("    align-items: center;");
            css.AppendLine("}");
            css.AppendLine("");
            css.AppendLine(".admin-header__right span {");
            css.AppendLine("    margin-right: 0.5rem;");
            css.AppendLine("    font-size: 1.5rem;");
            css.AppendLine("}");
            css.AppendLine("");
            css.AppendLine("@media (max-width: 1000px) {");
            css.AppendLine("    .admin-header__right span{");
            css.AppendLine("        display: none;");
            css.AppendLine("    }");
            css.AppendLine("}");

            StringBuilder js = new StringBuilder();
            js.AppendLine("import React, {useState} from 'react';");
            js.AppendLine("import './AdminHeader.css';");
            js.AppendLine("import UserCustomIcon from '../../icons/UserCustomIcon';");
            js.AppendLine("import { useSelector } from 'react-redux';");
            js.AppendLine("import { selectUserName } from '../../../services/redux/authSlice';");
            js.AppendLine("");
            js.AppendLine("const AdminHeader = ({onclickMenu}) => {");
            js.AppendLine("  const userName = useSelector(selectUserName);");
            js.AppendLine("  const [isDropdownOpen, setDropdownOpen] = useState(false);");
            js.AppendLine("");
            js.AppendLine("  const handleUserIconClick = () => {");
            js.AppendLine("    setDropdownOpen(!isDropdownOpen);");
            js.AppendLine("  };");
            js.AppendLine("");
            js.AppendLine("  return (");
            js.AppendLine("    <header className=\"admin-header\">");
            js.AppendLine("      <div className={'admin-header__all'}>");
            js.AppendLine("        {/* Hamburger Button for Mobile */}");
            js.AppendLine("        <button ");
            js.AppendLine("            className={'sidebar-toggle-btn'} ");
            js.AppendLine("            onClick={() => onclickMenu()}");
            js.AppendLine("        >");
            js.AppendLine("          ☰");
            js.AppendLine("        </button>");
            js.AppendLine("        <div className=\"admin-header__left\">");
            js.AppendLine("          <h1>Cestas de Maria</h1>");
            js.AppendLine("        </div>");
            js.AppendLine("        <div className='navbar__menu-item'>");
            js.AppendLine("          <div className=\"admin-header__right\" onClick={handleUserIconClick}>");
            js.AppendLine("            <span>Olá, {userName}</span>");
            js.AppendLine("            <UserCustomIcon size={32} />");
            js.AppendLine("          </div>");
            js.AppendLine("        </div>");
            js.AppendLine("      </div>");
            js.AppendLine("    </header>");
            js.AppendLine("  );");
            js.AppendLine("};");
            js.AppendLine("");
            js.AppendLine("export default AdminHeader;");

            File.WriteAllText(path + "//AdminHeader.js", js.ToString());
            File.WriteAllText(path + "//AdminHeader.css", css.ToString());
        }

        private static void CreateAdminSideBarFiles(List<MD_Tabela> tabelas, string path)
        {
            StringBuilder css = new StringBuilder();
            css.AppendLine("/* Sidebar container */");
            css.AppendLine(".sidebar {");
            css.AppendLine("    width: 280px;");
            css.AppendLine("    height: 100vh;");
            css.AppendLine("    background-color: var(--primary-color);");
            css.AppendLine("    display: flex;");
            css.AppendLine("    flex-direction: column;");
            css.AppendLine("    align-items: center;");
            css.AppendLine("    padding-top: 20px;");
            css.AppendLine("    position: fixed;");
            css.AppendLine("    left: 0;");
            css.AppendLine("    top: 0;");
            css.AppendLine("    justify-content: space-between;");
            css.AppendLine("    overflow-y: auto; /* Adiciona a barra de rolagem */");
            css.AppendLine("    scrollbar-width: thin; /* Para navegadores Firefox */");
            css.AppendLine("    scrollbar-color: var(--button-color-secundary) var(--primary-color); /* Cor personalizada para Firefox */");
            css.AppendLine("    z-index: 500;");
            css.AppendLine("}");
            css.AppendLine("");
            css.AppendLine("/* Estilização da barra de rolagem para navegadores Webkit (Chrome, Safari) */");
            css.AppendLine(".sidebar::-webkit-scrollbar {");
            css.AppendLine("    width: 8px; /* Largura da barra de rolagem */");
            css.AppendLine("}");
            css.AppendLine("");
            css.AppendLine(".sidebar::-webkit-scrollbar-track {");
            css.AppendLine("    background: var(--primary-color); /* Cor do track da barra de rolagem */");
            css.AppendLine("}");
            css.AppendLine("");
            css.AppendLine(".sidebar::-webkit-scrollbar-thumb {");
            css.AppendLine("    background-color: var(--button-color-secundary); /* Cor do polegar da barra de rolagem */");
            css.AppendLine("    border-radius: 10px; /* Bordas arredondadas */");
            css.AppendLine("    border: 2px solid var(--primary-color); /* Espaço ao redor do polegar */");
            css.AppendLine("}");
            css.AppendLine("");
            css.AppendLine("/* Sidebar logo */");
            css.AppendLine(".sidebar__logo img {");
            css.AppendLine("    width: 150px; /* Adjust as needed */");
            css.AppendLine("    margin-bottom: 20px;");
            css.AppendLine("}");
            css.AppendLine("");
            css.AppendLine(".sidebar__logo p{");
            css.AppendLine("    color: var(--text-color-secondary);");
            css.AppendLine("    font-size: large;");
            css.AppendLine("}");
            css.AppendLine("");
            css.AppendLine("/* Sidebar menu */");
            css.AppendLine(".sidebar__menu {");
            css.AppendLine("    width: 100%;");
            css.AppendLine("    margin-top: 40px;");
            css.AppendLine("    display: flex;");
            css.AppendLine("    flex-direction: column;");
            css.AppendLine("    align-items: center;");
            css.AppendLine("    flex-grow: 1; /* Makes menu take up available space */");
            css.AppendLine("}");
            css.AppendLine("");
            css.AppendLine(".sidebar__menu-item {");
            css.AppendLine("    width: 100%;");
            css.AppendLine("    padding: 15px;");
            css.AppendLine("    display: flex;");
            css.AppendLine("    align-items: center;");
            css.AppendLine("    justify-content: left;");
            css.AppendLine("    gap:8px;");
            css.AppendLine("    color: var(--text-color-secondary);");
            css.AppendLine("    font-size: 20px;");
            css.AppendLine("    text-decoration: none;");
            css.AppendLine("    padding-left: 2rem;");
            css.AppendLine("    transition: background-color 0.3s, color 0.3s;");
            css.AppendLine("}");
            css.AppendLine("");
            css.AppendLine(".sidebar__menu-item:hover {");
            css.AppendLine("    background-color: var(--primary-color-hover);");
            css.AppendLine("    color: var(--text-color-secondary);");
            css.AppendLine("}");
            css.AppendLine("");
            css.AppendLine("/* Active menu item styling */");
            css.AppendLine(".sidebar__menu-item.active {");
            css.AppendLine("    background-color: var(--primary-color-hover);");
            css.AppendLine("    font-weight: bold;");
            css.AppendLine("}");
            css.AppendLine("");
            css.AppendLine(".sidebar__menu-item-selected{");
            css.AppendLine("    border-left: 5px solid var(--button-color-secundary);");
            css.AppendLine("    background-color: var(--primary-color-hover);");
            css.AppendLine("}");
            css.AppendLine("");
            css.AppendLine("    /* Logoff option */");
            css.AppendLine(".sidebar__logoff {");
            css.AppendLine("    width: 100%;");
            css.AppendLine("    padding: 15px;");
            css.AppendLine("    display: flex;");
            css.AppendLine("    align-items: center;");
            css.AppendLine("    gap:1rem;");
            css.AppendLine("    justify-content: center;");
            css.AppendLine("    text-align: center;");
            css.AppendLine("    color: var(--text-color-secundary);");
            css.AppendLine("    font-size: 20px;");
            css.AppendLine("    text-decoration: none;");
            css.AppendLine("    cursor: pointer;");
            css.AppendLine("    background-color: var(--button-color-primary);");
            css.AppendLine("    color: var(--text-color-secondary);");
            css.AppendLine("    transition: background-color 0.3s, color 0.3s;");
            css.AppendLine("}");
            css.AppendLine("");
            css.AppendLine(".sidebar__logoff:hover {");
            css.AppendLine("    background-color: var(--button-color-primary-hover);");
            css.AppendLine("}");


            StringBuilder js = new StringBuilder();
            js.AppendLine("import './AdminSidebar.css'; // Import the corresponding CSS file");
            js.AppendLine("import React from 'react';");
            js.AppendLine("import { useDispatch } from 'react-redux';");
            js.AppendLine("import { useNavigate } from 'react-router-dom';");
            js.AppendLine("import { logout } from '../../../services/redux/authSlice';");
            js.AppendLine("import { toast } from 'react-toastify';");
            js.AppendLine("import { useSelector } from 'react-redux';");
            js.AppendLine("import DashBoardIcon from '../../icons/DashBoardIcon';");
            js.AppendLine("import LogoffIcon from '../../icons/LogoffIcon';");
            js.AppendLine("import { useLocation } from 'react-router-dom';");
            js.AppendLine("import LogoIcon from '../../icons/LogoIcon';");
            js.AppendLine("import BasketIcon from '../../icons/BasketIcon';");
            js.AppendLine("");
            js.AppendLine("const AdminSidebar = () => {");
            js.AppendLine("  const location = useLocation();");
            js.AppendLine("  const pathSegments = location.pathname.split('/').filter(Boolean);");
            js.AppendLine("  const dispatch = useDispatch();");
            js.AppendLine("  const navigate = useNavigate();");
            js.AppendLine("  const isAdmin = useSelector((state) => state.auth.isAdmin);");
            js.AppendLine("");
            js.AppendLine("  const handleLogout = () => {");
            js.AppendLine("    dispatch(logout());");
            js.AppendLine("    navigate('/');");
            js.AppendLine("    toast.success(\"Até breve!\");");
            js.AppendLine("  };");
            js.AppendLine("");
            js.AppendLine("  return (");
            js.AppendLine("    <aside className=\"sidebar\">");
            js.AppendLine("      <div className=\"sidebar__logo flex-column center-component gap-default\" >");
            js.AppendLine("        <LogoIcon size={80} color='#FFF'/>");
            js.AppendLine("        <p>");
            js.AppendLine("          Cestas de Maria");
            js.AppendLine("        </p>");
            js.AppendLine("      </div>");
            js.AppendLine("      <nav className=\"sidebar__menu\">");
            js.AppendLine("        {isAdmin && <a href=\"/\" className={pathSegments.length === 0 || (pathSegments[0] === 'dashboard') || pathSegments[1] === 'dashboard' ? \"sidebar__menu-item sidebar__menu-item-selected\" : \"sidebar__menu-item\"}><DashBoardIcon color='white'/>Dashboard</a>}");
            foreach (var tabela in tabelas)
            {
                js.AppendLine($"        {{isAdmin && <a href=\"/{tabela.DAO.Nome}\" className={{pathSegments[0] === '{tabela.DAO.Nome}' ? \"sidebar__menu-item sidebar__menu-item-selected\" : \"sidebar__menu-item\"}}><BasketIcon color='white'/>{tabela.DAO.Nome}</a>}}");
            }
            js.AppendLine("        </nav>");
            js.AppendLine("      <button className=\"sidebar__logoff\" onClick={handleLogout}>");
            js.AppendLine("        Sair <LogoffIcon color='white'/>");
            js.AppendLine("      </button>");
            js.AppendLine("    </aside>");
            js.AppendLine("  );");
            js.AppendLine("};");
            js.AppendLine("");
            js.AppendLine("export default AdminSidebar;");

            File.WriteAllText(path + "//AdminSidebar.js", js.ToString());
            File.WriteAllText(path + "//AdminSidebar.css", css.ToString());
        }

        private static void CreateAdminFilterComponentFiles(string path)
        {
            StringBuilder css = new StringBuilder();
            css.AppendLine(".filter-container-main {");
            css.AppendLine("    margin: var(--double-default-spacing) 0;");
            css.AppendLine("    display: flex;");
            css.AppendLine("    gap: var(--double-default-spacing);");
            css.AppendLine("    width: 100%;");
            css.AppendLine("    align-items: center; /* Ensures items align vertically if needed */");
            css.AppendLine("    flex-wrap: wrap;");
            css.AppendLine("}");
            css.AppendLine("");
            css.AppendLine(".filter-itens {");
            css.AppendLine("    display: flex;");
            css.AppendLine("    flex-direction: column;");
            css.AppendLine("    gap: var(--double-default-spacing);");
            css.AppendLine("    flex-grow: 1; /* Takes up the remaining space on the left */");
            css.AppendLine("}");
            css.AppendLine("");
            css.AppendLine(".filter-buttons {");
            css.AppendLine("    display: flex;");
            css.AppendLine("    gap: var(--double-default-spacing);");
            css.AppendLine("    justify-content: flex-end; /* Aligns buttons to the right */");
            css.AppendLine("}");
            css.AppendLine("");
            css.AppendLine(".filters-more{");
            css.AppendLine("    display: flex;");
            css.AppendLine("    width: 100%;");
            css.AppendLine("    justify-content: center;");
            css.AppendLine("    gap: var(--double-default-spacing);");
            css.AppendLine("}");
            css.AppendLine("");
            css.AppendLine(".filters-more-item{");
            css.AppendLine("    display: flex;");
            css.AppendLine("    flex-direction: column;");
            css.AppendLine("    width: 100%;");
            css.AppendLine("}");
            css.AppendLine("");
            css.AppendLine(".filters-more-item label{");
            css.AppendLine("    margin-bottom: var(--default-spacing);");
            css.AppendLine("}");
            css.AppendLine("");
            css.AppendLine(".filter-basic{");
            css.AppendLine("    display: flex;");
            css.AppendLine("    justify-content: space-between;");
            css.AppendLine("    gap: 2rem;");
            css.AppendLine("}");
            css.AppendLine("");
            css.AppendLine(".filter-basic-item {");
            css.AppendLine("    display: flex;");
            css.AppendLine("    flex-direction: column;");
            css.AppendLine("    min-width: 16vh;");
            css.AppendLine("    width: 100%;");
            css.AppendLine("}");
            css.AppendLine("");
            css.AppendLine("@media (max-width: 1000px) {");
            css.AppendLine("    .filter-basic{");
            css.AppendLine("        flex-wrap: wrap;");
            css.AppendLine("    }");
            css.AppendLine("}");

            StringBuilder js = new StringBuilder();
            js.AppendLine("import React, { useEffect, useState } from 'react';");
            js.AppendLine("import familyStatusApi from '../../../services/apiServices/familyStatusApi';");
            js.AppendLine("import './FilterComponent.css';");
            js.AppendLine("import { toast } from 'react-toastify';");
            js.AppendLine("import { useDispatch } from 'react-redux';");
            js.AppendLine("import { setLoading } from '../../../services/redux/loadingSlice';");
            js.AppendLine("");
            js.AppendLine("const FilterComponent = ({ placeHolder, showTermFilter, showStartDate=false, showEndDate=false, showFamilyStatus=false, submitFilter, exportFunction }) => {");
            js.AppendLine("  const dispatch = useDispatch();");
            js.AppendLine("  const [term, setTerm] = useState('');");
            js.AppendLine("  const [startDate, setStartDate] = useState('');");
            js.AppendLine("  const [endDate, setEndDate] = useState('');");
            js.AppendLine("  const [familyStatusList, setFamilyStatusList] = useState([]);");
            js.AppendLine("  const [showMoreFilter, setShowMoreFilter] = useState(false);");
            js.AppendLine("  const [filter, setFilter] = useState({");
            js.AppendLine("    familyStatusId: ''");
            js.AppendLine("  });");
            js.AppendLine("");
            js.AppendLine("  const handleInputChange = (e) => {");
            js.AppendLine("    const { name, value } = e.target;");
            js.AppendLine("    setFilter((prevState) => ({");
            js.AppendLine("        ...prevState,");
            js.AppendLine("        [name]: value,");
            js.AppendLine("    }));");
            js.AppendLine("  };");
            js.AppendLine("");
            js.AppendLine("  const handleSubmit = () => {");
            js.AppendLine("    submitFilter({");
            js.AppendLine("      term: term || undefined,");
            js.AppendLine("      startDate: startDate || undefined,");
            js.AppendLine("      endDate: endDate || undefined,");
            js.AppendLine("      familyStatusId: filter.familyStatusId || undefined,");
            js.AppendLine("    });");
            js.AppendLine("  };");
            js.AppendLine("");
            js.AppendLine("  useEffect(() => {");
            js.AppendLine("    const fetchFamilyStatus = async () => {");
            js.AppendLine("        try {");
            js.AppendLine("            const response = await familyStatusApi.getAllFamilyStatuses();");
            js.AppendLine("            setFamilyStatusList(response);");
            js.AppendLine("        } catch (error) {");
            js.AppendLine("            toast.error('Erro ao carregar as marcas!');");
            js.AppendLine("        }");
            js.AppendLine("    };");
            js.AppendLine("");
            js.AppendLine("    const fetchData = async () => {");
            js.AppendLine("        try {");
            js.AppendLine("            dispatch(setLoading(true));");
            js.AppendLine("            if(showFamilyStatus)");
            js.AppendLine("              await fetchFamilyStatus();");
            js.AppendLine("        } finally {");
            js.AppendLine("            dispatch(setLoading(false));");
            js.AppendLine("        }");
            js.AppendLine("    };");
            js.AppendLine("");
            js.AppendLine("    fetchData();");
            js.AppendLine("  }, [showFamilyStatus, dispatch]);");
            js.AppendLine("");
            js.AppendLine("  const exportReport = () => {");
            js.AppendLine("    exportFunction({");
            js.AppendLine("      term: term || undefined,");
            js.AppendLine("      startDate: startDate || undefined,");
            js.AppendLine("      endDate: endDate || undefined,");
            js.AppendLine("      familyStatusId: filter.familyStatusId || undefined,");
            js.AppendLine("    });");
            js.AppendLine("  };");
            js.AppendLine("");
            js.AppendLine("  return (");
            js.AppendLine("    <div className=\"filter-container\">");
            js.AppendLine("      <div className='filter-container-main'>");
            js.AppendLine("        <div className='filter-itens'>");
            js.AppendLine("          <div className='filter-basic'>");
            js.AppendLine("            {showTermFilter && (");
            js.AppendLine("              <div className='filter-basic-item'>");
            js.AppendLine("                <span>Filtro</span>");
            js.AppendLine("                <input");
            js.AppendLine("                  type=\"text\"");
            js.AppendLine("                  placeholder={placeHolder}");
            js.AppendLine("                  className=\"main-input\"");
            js.AppendLine("                  value={term}");
            js.AppendLine("                  onChange={(e) => setTerm(e.target.value)}");
            js.AppendLine("                />");
            js.AppendLine("              </div>");
            js.AppendLine("            )}");
            js.AppendLine("            {showStartDate && (");
            js.AppendLine("              <div className='filter-basic-item'>");
            js.AppendLine("                <span>Data Inicial</span>");
            js.AppendLine("                <input");
            js.AppendLine("                  type=\"date\"");
            js.AppendLine("                  placeholder=\"Data Inicial\"");
            js.AppendLine("                  className=\"main-input\"");
            js.AppendLine("                  value={startDate}");
            js.AppendLine("                  onChange={(e) => setStartDate(e.target.value)}");
            js.AppendLine("                />");
            js.AppendLine("              </div>");
            js.AppendLine("            )}");
            js.AppendLine("            {showEndDate && (");
            js.AppendLine("              <div className='filter-basic-item'>");
            js.AppendLine("                <span>Data Final</span>");
            js.AppendLine("                <input");
            js.AppendLine("                  type=\"date\"");
            js.AppendLine("                  placeholder=\"Data Final\"");
            js.AppendLine("                  className=\"main-input\"");
            js.AppendLine("                  value={endDate}");
            js.AppendLine("                  onChange={(e) => setEndDate(e.target.value)}");
            js.AppendLine("                />");
            js.AppendLine("              </div>");
            js.AppendLine("            )}");
            js.AppendLine("          </div>");
            js.AppendLine("        </div>");
            js.AppendLine("");
            js.AppendLine("        <div className='filter-buttons'>");
            js.AppendLine("          <button onClick={handleSubmit} className=\"main-button margin-top-default\">");
            js.AppendLine("            Filtrar");
            js.AppendLine("          </button>");
            js.AppendLine("          {");
            js.AppendLine("            exportFunction && (");
            js.AppendLine("              <button onClick={exportReport} className=\"main-button margin-top-default\">");
            js.AppendLine("                Exportar");
            js.AppendLine("              </button>");
            js.AppendLine("            )");
            js.AppendLine("          }");
            js.AppendLine("");
            js.AppendLine("          {");
            js.AppendLine("            showFamilyStatus ? ");
            js.AppendLine("              <button onClick={() => setShowMoreFilter(prev => !prev)} className=\"main-button margin-top-default\">");
            js.AppendLine("                {");
            js.AppendLine("                  showMoreFilter ? ");
            js.AppendLine("                  'Menos Filtros'");
            js.AppendLine("                  :");
            js.AppendLine("                  'Mais Filtros'");
            js.AppendLine("                }");
            js.AppendLine("              </button>");
            js.AppendLine("              : ");
            js.AppendLine("              <></>");
            js.AppendLine("          }");
            js.AppendLine("        </div>");
            js.AppendLine("      </div>");
            js.AppendLine("");
            js.AppendLine("      {");
            js.AppendLine("        showMoreFilter &&");
            js.AppendLine("        <div className='filters-more'>");
            js.AppendLine("          {");
            js.AppendLine("            showFamilyStatus && ");
            js.AppendLine("            <div className=\"filters-more-item\">");
            js.AppendLine("                <label>");
            js.AppendLine("                    Status:");
            js.AppendLine("                </label>");
            js.AppendLine("");
            js.AppendLine("                <select");
            js.AppendLine("                    name=\"familyStatusId\"");
            js.AppendLine("                    className=\"main-input\"");
            js.AppendLine("                    value={filter.ModelCode}");
            js.AppendLine("                    onChange={handleInputChange}");
            js.AppendLine("                    required");
            js.AppendLine("                >");
            js.AppendLine("                    <option value=\"\">Selecione o Status</option>");
            js.AppendLine("                    {familyStatusList.map((obj) => (");
            js.AppendLine("                        <option key={obj.Id} value={obj.Id}>");
            js.AppendLine("                            {obj.Description}");
            js.AppendLine("                        </option>");
            js.AppendLine("                    ))}");
            js.AppendLine("                </select>");
            js.AppendLine("            </div>");
            js.AppendLine("          }");
            js.AppendLine("        </div>");
            js.AppendLine("      }");
            js.AppendLine("    </div>");
            js.AppendLine("  );");
            js.AppendLine("};");
            js.AppendLine("");
            js.AppendLine("export default FilterComponent;");

            File.WriteAllText(path + "//FilterComponent.js", js.ToString());
            File.WriteAllText(path + "//FilterComponent.css", css.ToString());
        }

        private static void CreateAdminAddUserModalFiles(string path)
        {
            StringBuilder css = new StringBuilder();
            css.AppendLine(".cadastro-new-user{");
            css.AppendLine("    display: flex;");
            css.AppendLine("    flex-direction: column;");
            css.AppendLine("    width: 100%;");
            css.AppendLine("}");

            StringBuilder js = new StringBuilder();
            js.AppendLine("import React, { useState } from \"react\";");
            js.AppendLine("import \"./AddUserModal.css\";");
            js.AppendLine("import adminApi from \"../../../../services/apiServices/adminApi\";");
            js.AppendLine("import { useDispatch } from \"react-redux\";");
            js.AppendLine("import { setLoading } from \"../../../../services/redux/loadingSlice\";");
            js.AppendLine("import MessageModal from \"../../../common/Modals/MessageModal/MessageModal\";");
            js.AppendLine("import { useNavigate } from 'react-router-dom';");
            js.AppendLine("");
            js.AppendLine("const AddUserModal = ({ isOpen, closeModal }) => {");
            js.AppendLine("    const navigate = useNavigate();");
            js.AppendLine("");
            js.AppendLine("    const [name, setName] = useState(\"\");");
            js.AppendLine("    const [username, setUsername] = useState(\"\");");
            js.AppendLine("    const [message, setMessage] = useState(\"\");");
            js.AppendLine("    const [isMessageOpen, setIsMessageOpen] = useState(false);");
            js.AppendLine("    const dispatch = useDispatch();");
            js.AppendLine("");
            js.AppendLine("    const openMessageModal = () => {");
            js.AppendLine("        setIsMessageOpen(true);");
            js.AppendLine("    };");
            js.AppendLine("");
            js.AppendLine("    const closeMessageModal = () => {");
            js.AppendLine("        setIsMessageOpen(false);");
            js.AppendLine("        if (message.includes(\"sucesso\")) {");
            js.AppendLine("            closeModal(); // Close the modal on successful creation");
            js.AppendLine("            navigate('/usuarios');");
            js.AppendLine("        }");
            js.AppendLine("    };");
            js.AppendLine("");
            js.AppendLine("    const handleSubmit = async (e) => {");
            js.AppendLine("        e.preventDefault();");
            js.AppendLine("");
            js.AppendLine("        const payload = {");
            js.AppendLine("            name,");
            js.AppendLine("            username");
            js.AppendLine("        };");
            js.AppendLine("");
            js.AppendLine("        try {");
            js.AppendLine("            dispatch(setLoading(true));");
            js.AppendLine("            const response = await adminApi.createAdmin(payload);");
            js.AppendLine("            var message = response.Id ? 'Usuário criado com sucesso' : 'Erro ao criar usuário. Verifique os dados e tente novamente.';");
            js.AppendLine("");
            js.AppendLine("            setUsername('');");
            js.AppendLine("            setName('');");
            js.AppendLine("            setMessage(message);");
            js.AppendLine("            openMessageModal();");
            js.AppendLine("        } catch (error) {");
            js.AppendLine("            setMessage(\"Erro ao criar usuário. Verifique os dados e tente novamente.\");");
            js.AppendLine("            openMessageModal();");
            js.AppendLine("        } finally {");
            js.AppendLine("            dispatch(setLoading(false));");
            js.AppendLine("        }");
            js.AppendLine("    };");
            js.AppendLine("");
            js.AppendLine("    if (!isOpen) return null;");
            js.AppendLine("");
            js.AppendLine("    return (");
            js.AppendLine("        <div className=\"modal-backdrop\">");
            js.AppendLine("            <MessageModal");
            js.AppendLine("                isOpen={isMessageOpen}");
            js.AppendLine("                click={closeMessageModal}");
            js.AppendLine("                message={message}");
            js.AppendLine("            />");
            js.AppendLine("            <div className=\"modal-container\">");
            js.AppendLine("                <button className=\"close-button\" onClick={closeModal}>");
            js.AppendLine("                    &times;");
            js.AppendLine("                </button>");
            js.AppendLine("                <h2 className=\"modal-title\">Adicionar Usuário</h2>");
            js.AppendLine("                <form onSubmit={handleSubmit} className=\"modal-content\">");
            js.AppendLine("                    <div className=\"cadastro-new-user\">");
            js.AppendLine("                        <div className=\"form-group\">");
            js.AppendLine("                            <label htmlFor=\"name\">Nome</label>");
            js.AppendLine("                            <input");
            js.AppendLine("                                type=\"text\"");
            js.AppendLine("                                id=\"name\"");
            js.AppendLine("                                className=\"main-input\"");
            js.AppendLine("                                placeholder=\"Digite o nome completo\"");
            js.AppendLine("                                value={name}");
            js.AppendLine("                                onChange={(e) => setName(e.target.value)}");
            js.AppendLine("                                required");
            js.AppendLine("                            />");
            js.AppendLine("                        </div>");
            js.AppendLine("                        <div className=\"form-group\">");
            js.AppendLine("                            <label htmlFor=\"username\">Email</label>");
            js.AppendLine("                            <input");
            js.AppendLine("                                type=\"email\"");
            js.AppendLine("                                id=\"username\"");
            js.AppendLine("                                className=\"main-input\"");
            js.AppendLine("                                placeholder=\"Digite o email\"");
            js.AppendLine("                                value={username}");
            js.AppendLine("                                onChange={(e) => setUsername(e.target.value)}");
            js.AppendLine("                                required");
            js.AppendLine("                            />");
            js.AppendLine("                        </div>");
            js.AppendLine("                        <div className=\"modal-actions flex-row\">");
            js.AppendLine("                            <button");
            js.AppendLine("                                type=\"button\"");
            js.AppendLine("                                className=\"main-button\"");
            js.AppendLine("                                onClick={closeModal}");
            js.AppendLine("                            >");
            js.AppendLine("                                Cancelar");
            js.AppendLine("                            </button>");
            js.AppendLine("                            <button type=\"submit\" className=\"main-button\">");
            js.AppendLine("                                Salvar");
            js.AppendLine("                            </button>");
            js.AppendLine("                        </div>");
            js.AppendLine("                    </div>");
            js.AppendLine("                </form>");
            js.AppendLine("            </div>");
            js.AppendLine("        </div>");
            js.AppendLine("    );");
            js.AppendLine("};");
            js.AppendLine("");
            js.AppendLine("export default AddUserModal;");

            File.WriteAllText(path + "//AddUserModal.js", js.ToString());
            File.WriteAllText(path + "//AddUserModal.css", css.ToString());
        }
    }
}
