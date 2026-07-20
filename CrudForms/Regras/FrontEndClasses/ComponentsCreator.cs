using Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Regras.FrontEndClasses
{
    public static class ComponentsCreator
    {
        public static bool Create(List<MD_Tabela> tabelas, string projectPath, string projectName)
        {
            bool success = true;

            try
            {
                string adminComponents = NamesHandler.GetDirectoryByType(projectPath, NamesHandler.FileType.ComponentsAdmin);
                success &= CreateAdminFoldersAndComponents(tabelas, adminComponents, projectName);

                string clientComponents = NamesHandler.GetDirectoryByType(projectPath, NamesHandler.FileType.ComponentsClient);
                success &= CreateClientFoldersAndComponents(clientComponents);

                string commonComponents = NamesHandler.GetDirectoryByType(projectPath, NamesHandler.FileType.ComponentsCommon);
                success &= CreateCommonFoldersAndComponents(commonComponents);
            }
            catch (Exception ex)
            {
                Util.CL_Files.LogException(ex);
                success = false;
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

                string confirmModalPath = Path.Combine(modalsPath, "ConfirmModal");
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
            StringBuilder js = new StringBuilder();
            js.AppendLine("import React from 'react';");
            js.AppendLine("");
            js.AppendLine("const Pagination = ({ page, totalPages, onPageChange }) => {");
            js.AppendLine("    const getPages = () => {");
            js.AppendLine("        const pages = [];");
            js.AppendLine("        if (totalPages <= 5) {");
            js.AppendLine("            for (let i = 1; i <= totalPages; i++) {");
            js.AppendLine("                pages.push(i);");
            js.AppendLine("            }");
            js.AppendLine("        } else if (page <= 3) {");
            js.AppendLine("            pages.push(1, 2, 3, '...', totalPages);");
            js.AppendLine("        } else if (page > totalPages - 3) {");
            js.AppendLine("            pages.push(1, '...', totalPages - 2, totalPages - 1, totalPages);");
            js.AppendLine("        } else {");
            js.AppendLine("            pages.push(1, '...', page - 1, page, page + 1, '...', totalPages);");
            js.AppendLine("        }");
            js.AppendLine("        return pages;");
            js.AppendLine("    };");
            js.AppendLine("");
            js.AppendLine("    return (");
            js.AppendLine("        <nav aria-label=\"Paginação\">");
            js.AppendLine("            <ul className=\"pagination justify-content-end mb-0\">");
            js.AppendLine("                <li className={`page-item ${page <= 1 ? 'disabled' : ''}`}>");
            js.AppendLine("                    <button type=\"button\" className=\"page-link\" onClick={() => onPageChange(page - 1)} disabled={page <= 1}>");
            js.AppendLine("                        Anterior");
            js.AppendLine("                    </button>");
            js.AppendLine("                </li>");
            js.AppendLine("                {getPages().map((pageNumber, index) => (");
            js.AppendLine("                    pageNumber === '...' ? (");
            js.AppendLine("                        <li key={`ellipsis-${index}`} className=\"page-item disabled\"><span className=\"page-link\">...</span></li>");
            js.AppendLine("                    ) : (");
            js.AppendLine("                        <li key={pageNumber} className={`page-item ${pageNumber === page ? 'active' : ''}`}>");
            js.AppendLine("                            <button type=\"button\" className=\"page-link\" onClick={() => onPageChange(pageNumber)}>");
            js.AppendLine("                                {pageNumber}");
            js.AppendLine("                            </button>");
            js.AppendLine("                        </li>");
            js.AppendLine("                    )");
            js.AppendLine("                ))}");
            js.AppendLine("                <li className={`page-item ${page >= totalPages ? 'disabled' : ''}`}>");
            js.AppendLine("                    <button type=\"button\" className=\"page-link\" onClick={() => onPageChange(page + 1)} disabled={page >= totalPages}>");
            js.AppendLine("                        Próximo");
            js.AppendLine("                    </button>");
            js.AppendLine("                </li>");
            js.AppendLine("            </ul>");
            js.AppendLine("        </nav>");
            js.AppendLine("    );");
            js.AppendLine("};");
            js.AppendLine("");
            js.AppendLine("export default Pagination;");

            File.WriteAllText(path + "//Pagination.jsx", js.ToString());
        }

        private static void CreateMessageModalFiles(string path)
        {
            StringBuilder js = new StringBuilder();
            js.AppendLine("import React from 'react';");
            js.AppendLine("");
            js.AppendLine("const MessageModal = ({ isOpen, message, click, optionText = 'Fechar' }) => {");
            js.AppendLine("    if (!isOpen) return null;");
            js.AppendLine("");
            js.AppendLine("    return (");
            js.AppendLine("        <>");
            js.AppendLine("            <div className=\"modal d-block\" tabIndex=\"-1\" role=\"dialog\" aria-modal=\"true\">");
            js.AppendLine("                <div className=\"modal-dialog modal-dialog-centered\">");
            js.AppendLine("                    <div className=\"modal-content border-0 shadow\">");
            js.AppendLine("                        <div className=\"modal-body\">");
            js.AppendLine("                            <p className=\"mb-0\">{message}</p>");
            js.AppendLine("                        </div>");
            js.AppendLine("                        <div className=\"modal-footer\">");
            js.AppendLine("                            <button type=\"button\" className=\"btn btn-primary\" onClick={click}>{optionText}</button>");
            js.AppendLine("                        </div>");
            js.AppendLine("                    </div>");
            js.AppendLine("                </div>");
            js.AppendLine("            </div>");
            js.AppendLine("            <div className=\"modal-backdrop show\"></div>");
            js.AppendLine("        </>");
            js.AppendLine("    );");
            js.AppendLine("};");
            js.AppendLine("");
            js.AppendLine("export default MessageModal;");

            File.WriteAllText(path + "//MessageModal.jsx", js.ToString());
        }

        private static void CreateLoadingModalFiles(string path)
        {
            StringBuilder js = new StringBuilder();
            js.AppendLine("import React from 'react';");
            js.AppendLine("");
            js.AppendLine("const LoadingModal = () => {");
            js.AppendLine("    return (");
            js.AppendLine("        <>");
            js.AppendLine("            <div className=\"modal d-block\" tabIndex=\"-1\" role=\"dialog\" aria-modal=\"true\" aria-label=\"Carregando\">");
            js.AppendLine("                <div className=\"modal-dialog modal-dialog-centered\">");
            js.AppendLine("                    <div className=\"modal-content border-0 shadow\">");
            js.AppendLine("                        <div className=\"modal-body d-flex align-items-center justify-content-center gap-3 p-4\">");
            js.AppendLine("                            <div className=\"spinner-border text-app-primary\" role=\"status\" aria-hidden=\"true\"></div>");
            js.AppendLine("                            <span>Carregando...</span>");
            js.AppendLine("                        </div>");
            js.AppendLine("                    </div>");
            js.AppendLine("                </div>");
            js.AppendLine("            </div>");
            js.AppendLine("            <div className=\"modal-backdrop show\"></div>");
            js.AppendLine("        </>");
            js.AppendLine("    );");
            js.AppendLine("};");
            js.AppendLine("");
            js.AppendLine("export default LoadingModal;");

            File.WriteAllText(path + "//LoadingModal.jsx", js.ToString());
        }

        private static void CreateConfirmModalFiles(string path)
        {
            StringBuilder js = new StringBuilder();
            js.AppendLine("import React from 'react';");
            js.AppendLine("");
            js.AppendLine("const ConfirmModal = ({ isOpen, title, message, onYes, onNo, confirmData, yesLabel = 'Sim', noLabel = 'Não' }) => {");
            js.AppendLine("    if (!isOpen) return null;");
            js.AppendLine("");
            js.AppendLine("    return (");
            js.AppendLine("        <>");
            js.AppendLine("            <div className=\"modal d-block\" tabIndex=\"-1\" role=\"dialog\" aria-modal=\"true\" aria-labelledby=\"confirm-modal-title\">");
            js.AppendLine("                <div className=\"modal-dialog modal-dialog-centered\">");
            js.AppendLine("                    <div className=\"modal-content border-0 shadow\">");
            js.AppendLine("                        <div className=\"modal-header\">");
            js.AppendLine("                            <h2 id=\"confirm-modal-title\" className=\"modal-title h5\">{title}</h2>");
            js.AppendLine("                        </div>");
            js.AppendLine("                        <div className=\"modal-body\">");
            js.AppendLine("                            <p className=\"mb-0\">{message}</p>");
            js.AppendLine("                        </div>");
            js.AppendLine("                        <div className=\"modal-footer\">");
            js.AppendLine("                            <button type=\"button\" className=\"btn btn-outline-secondary\" onClick={onNo}>{noLabel}</button>");
            js.AppendLine("                            <button type=\"button\" className=\"btn btn-primary\" onClick={() => onYes(confirmData)}>{yesLabel}</button>");
            js.AppendLine("                        </div>");
            js.AppendLine("                    </div>");
            js.AppendLine("                </div>");
            js.AppendLine("            </div>");
            js.AppendLine("            <div className=\"modal-backdrop show\"></div>");
            js.AppendLine("        </>");
            js.AppendLine("    );");
            js.AppendLine("};");
            js.AppendLine("");
            js.AppendLine("export default ConfirmModal;");

            File.WriteAllText(path + "//ConfirmModal.jsx", js.ToString());
        }

        private static bool CreateClientFoldersAndComponents(string client)
        {
            return true;
        }

        private static bool CreateAdminFoldersAndComponents(List<MD_Tabela> tabelas, string adminComponent, string projectName)
        {
            bool success = true;

            try
            {
                string adminHeaderFolder = Path.Combine(adminComponent, "AdminHeader");
                Directory.CreateDirectory(adminHeaderFolder);
                CreateAdminHeaderFiles(adminHeaderFolder, projectName);

                string adminSideBarFolder = Path.Combine(adminComponent, "AdminSidebar");
                Directory.CreateDirectory(adminSideBarFolder);
                CreateAdminSideBarFiles(tabelas, adminSideBarFolder, projectName);

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

        private static void CreateAdminHeaderFiles(string path, string projectName)
        {
            StringBuilder js = new StringBuilder();
            js.AppendLine("import React, { useEffect, useState } from 'react';");
            js.AppendLine("import { useSelector } from 'react-redux';");
            js.AppendLine("import { selectUserName } from '../../../services/redux/authSlice';");
            js.AppendLine("import { applyTheme, getStoredTheme } from '../../../utils/themeMode';");
            js.AppendLine("");
            js.AppendLine("const AdminHeader = ({ onclickMenu }) => {");
            js.AppendLine("    const userName = useSelector(selectUserName);");
            js.AppendLine("    const [theme, setTheme] = useState(() => getStoredTheme());");
            js.AppendLine("");
            js.AppendLine("    useEffect(() => {");
            js.AppendLine("        setTheme(applyTheme(theme));");
            js.AppendLine("    }, [theme]);");
            js.AppendLine("");
            js.AppendLine("    const switchTheme = (nextTheme) => {");
            js.AppendLine("        setTheme(applyTheme(nextTheme));");
            js.AppendLine("    };");
            js.AppendLine("");
            js.AppendLine("    return (");
            js.AppendLine("        <header className=\"admin-header bg-body border-bottom\">");
            js.AppendLine("            <div className=\"d-flex align-items-center justify-content-between gap-3 px-3 py-2 w-100\">");
            js.AppendLine("                <div className=\"d-flex align-items-center gap-3\">");
            js.AppendLine("                    <button type=\"button\" className=\"btn btn-outline-secondary d-lg-none\" onClick={onclickMenu} aria-label=\"Abrir menu\">");
            js.AppendLine("                        <i className=\"bi bi-list\" aria-hidden=\"true\"></i>");
            js.AppendLine("                    </button>");
            js.AppendLine($"                    <h1 className=\"h5 mb-0\">{EscapeJsString(projectName)}</h1>");
            js.AppendLine("                </div>");
            js.AppendLine("                <div className=\"d-flex align-items-center gap-2 text-body-secondary\">");
            js.AppendLine("                    <button");
            js.AppendLine("                        type=\"button\"");
            js.AppendLine("                        className={`btn btn-outline-secondary rounded-circle theme-toggle-button d-flex align-items-center justify-content-center ${theme === 'dark' ? 'd-none' : ''}`}");
            js.AppendLine("                        onClick={() => switchTheme('dark')}");
            js.AppendLine("                        aria-label=\"Ativar tema escuro\"");
            js.AppendLine("                        title=\"Tema escuro\"");
            js.AppendLine("                    >");
            js.AppendLine("                        <i className=\"bi bi-moon\" aria-hidden=\"true\"></i>");
            js.AppendLine("                    </button>");
            js.AppendLine("                    <button");
            js.AppendLine("                        type=\"button\"");
            js.AppendLine("                        className={`btn btn-outline-secondary rounded-circle theme-toggle-button d-flex align-items-center justify-content-center ${theme === 'light' ? 'd-none' : ''}`}");
            js.AppendLine("                        onClick={() => switchTheme('light')}");
            js.AppendLine("                        aria-label=\"Ativar tema claro\"");
            js.AppendLine("                        title=\"Tema claro\"");
            js.AppendLine("                    >");
            js.AppendLine("                        <i className=\"bi bi-sun\" aria-hidden=\"true\"></i>");
            js.AppendLine("                    </button>");
            js.AppendLine("                    <span className=\"d-none d-sm-inline\">Olá, {userName}</span>");
            js.AppendLine("                    <i className=\"bi bi-person-circle fs-4\" aria-hidden=\"true\"></i>");
            js.AppendLine("                </div>");
            js.AppendLine("            </div>");
            js.AppendLine("        </header>");
            js.AppendLine("    );");
            js.AppendLine("};");
            js.AppendLine("");
            js.AppendLine("export default AdminHeader;");

            File.WriteAllText(path + "//AdminHeader.jsx", js.ToString());
        }

        private static void CreateAdminSideBarFiles(List<MD_Tabela> tabelas, string path, string projectName)
        {
            StringBuilder js = new StringBuilder();
            js.AppendLine("import React from 'react';");
            js.AppendLine("import { useDispatch, useSelector } from 'react-redux';");
            js.AppendLine("import { Link, useLocation, useNavigate } from 'react-router-dom';");
            js.AppendLine("import { logout } from '../../../services/redux/authSlice';");
            js.AppendLine("import { toast } from 'react-toastify';");
            js.AppendLine("");
            js.AppendLine("const AdminSidebar = () => {");
            js.AppendLine("    const location = useLocation();");
            js.AppendLine("    const pathSegments = location.pathname.split('/').filter(Boolean);");
            js.AppendLine("    const dispatch = useDispatch();");
            js.AppendLine("    const navigate = useNavigate();");
            js.AppendLine("    const isAdmin = useSelector((state) => state.auth.isAdmin);");
            js.AppendLine("");
            js.AppendLine("    const handleLogout = () => {");
            js.AppendLine("        dispatch(logout());");
            js.AppendLine("        navigate('/');");
            js.AppendLine("        toast.success('Até breve!');");
            js.AppendLine("    };");
            js.AppendLine("");
            js.AppendLine("    return (");
            js.AppendLine("        <aside className=\"admin-sidebar d-flex flex-column flex-shrink-0 p-3 text-white\">");
            js.AppendLine("            <div className=\"d-flex align-items-center gap-2 mb-4\">");
            js.AppendLine("                <i className=\"bi bi-grid-1x2-fill fs-3\" aria-hidden=\"true\"></i>");
            js.AppendLine($"                <span className=\"fs-5 fw-semibold\">{EscapeJsString(projectName)}</span>");
            js.AppendLine("            </div>");
            js.AppendLine("            <nav className=\"nav nav-pills flex-column gap-1 flex-grow-1\">");
            js.AppendLine("                {isAdmin && (");
            js.AppendLine("                    <Link to=\"/\" className={pathSegments.length === 0 || pathSegments[0] === 'dashboard' ? 'nav-link active' : 'nav-link'}>");
            js.AppendLine("                        <i className=\"bi bi-speedometer2 me-2\" aria-hidden=\"true\"></i>");
            js.AppendLine("                        Dashboard");
            js.AppendLine("                    </Link>");
            js.AppendLine("                )}");
            foreach (var tabela in tabelas)
            {
                string routeName = NamesHandler.CreateRouteName(tabela.Apelido);
                string iconClass = EscapeJsString(tabela.IconeFrontEnd);
                js.AppendLine("                {isAdmin && (");
                js.AppendLine($"                    <Link to=\"/{routeName}\" className={{pathSegments[0] === '{routeName}' ? 'nav-link active' : 'nav-link'}}>");
                js.AppendLine($"                        <i className=\"bi {iconClass} me-2\" aria-hidden=\"true\"></i>");
                js.AppendLine($"                        {EscapeJsString(tabela.Apelido)}");
                js.AppendLine("                    </Link>");
                js.AppendLine("                )}");
            }
            js.AppendLine("            </nav>");
            js.AppendLine("            <button type=\"button\" className=\"btn btn-outline-light w-100 mt-3\" onClick={handleLogout}>");
            js.AppendLine("                <i className=\"bi bi-box-arrow-right me-2\" aria-hidden=\"true\"></i>");
            js.AppendLine("                Sair");
            js.AppendLine("            </button>");
            js.AppendLine("        </aside>");
            js.AppendLine("    );");
            js.AppendLine("};");
            js.AppendLine("");
            js.AppendLine("export default AdminSidebar;");

            File.WriteAllText(path + "//AdminSidebar.jsx", js.ToString());
        }

        private static void CreateAdminFilterComponentFiles(string path)
        {
            StringBuilder js = new StringBuilder();
            js.AppendLine("import React, { useState } from 'react';");
            js.AppendLine("");
            js.AppendLine("const FilterComponent = ({ placeHolder, showTermFilter, showStartDate = false, showEndDate = false, showIsActiveFilter = false, submitFilter, exportFunction }) => {");
            js.AppendLine("    const [term, setTerm] = useState('');");
            js.AppendLine("    const [startDate, setStartDate] = useState('');");
            js.AppendLine("    const [endDate, setEndDate] = useState('');");
            js.AppendLine("    const [isActive, setIsActive] = useState('');");
            js.AppendLine("");
            js.AppendLine("    const handleSubmit = () => {");
            js.AppendLine("        submitFilter({");
            js.AppendLine("            term: term || undefined,");
            js.AppendLine("            startDate: startDate || undefined,");
            js.AppendLine("            endDate: endDate || undefined,");
            js.AppendLine("            isActive: isActive || undefined,");
            js.AppendLine("        });");
            js.AppendLine("    };");
            js.AppendLine("");
            js.AppendLine("    const exportReport = () => {");
            js.AppendLine("        exportFunction({");
            js.AppendLine("            term: term || undefined,");
            js.AppendLine("            startDate: startDate || undefined,");
            js.AppendLine("            endDate: endDate || undefined,");
            js.AppendLine("            isActive: isActive || undefined,");
            js.AppendLine("        });");
            js.AppendLine("    };");
            js.AppendLine("");
            js.AppendLine("    return (");
            js.AppendLine("        <div className=\"row g-3 align-items-end mb-4\">");
            js.AppendLine("            {showTermFilter && (");
            js.AppendLine("                <div className=\"col-lg-4 col-md-6\">");
            js.AppendLine("                    <label htmlFor=\"search\" className=\"form-label\">Filtro</label>");
            js.AppendLine("                    <input id=\"search\" type=\"text\" placeholder={placeHolder} className=\"form-control\" value={term} onChange={(event) => setTerm(event.target.value)} />");
            js.AppendLine("                </div>");
            js.AppendLine("            )}");
            js.AppendLine("            {showStartDate && (");
            js.AppendLine("                <div className=\"col-lg-2 col-md-3\">");
            js.AppendLine("                    <label htmlFor=\"startDate\" className=\"form-label\">Data inicial</label>");
            js.AppendLine("                    <input id=\"startDate\" type=\"date\" className=\"form-control\" value={startDate} onChange={(event) => setStartDate(event.target.value)} />");
            js.AppendLine("                </div>");
            js.AppendLine("            )}");
            js.AppendLine("            {showEndDate && (");
            js.AppendLine("                <div className=\"col-lg-2 col-md-3\">");
            js.AppendLine("                    <label htmlFor=\"endDate\" className=\"form-label\">Data final</label>");
            js.AppendLine("                    <input id=\"endDate\" type=\"date\" className=\"form-control\" value={endDate} onChange={(event) => setEndDate(event.target.value)} />");
            js.AppendLine("                </div>");
            js.AppendLine("            )}");
            js.AppendLine("            {showIsActiveFilter && (");
            js.AppendLine("                <div className=\"col-lg-2 col-md-6\">");
            js.AppendLine("                    <label htmlFor=\"status\" className=\"form-label\">Status</label>");
            js.AppendLine("                    <select id=\"status\" className=\"form-select\" value={isActive} onChange={(event) => setIsActive(event.target.value)}>");
            js.AppendLine("                        <option value=\"\">Todos</option>");
            js.AppendLine("                        <option value=\"true\">Ativos</option>");
            js.AppendLine("                        <option value=\"false\">Inativos</option>");
            js.AppendLine("                    </select>");
            js.AppendLine("                </div>");
            js.AppendLine("            )}");
            js.AppendLine("            <div className=\"col-lg-2 col-md-6 d-grid\">");
            js.AppendLine("                <button type=\"button\" onClick={handleSubmit} className=\"btn btn-outline-primary\">");
            js.AppendLine("                    <i className=\"bi bi-search me-2\" aria-hidden=\"true\"></i>");
            js.AppendLine("                    Filtrar");
            js.AppendLine("                </button>");
            js.AppendLine("            </div>");
            js.AppendLine("            {exportFunction && (");
            js.AppendLine("                <div className=\"col-lg-2 col-md-6 d-grid\">");
            js.AppendLine("                    <button type=\"button\" onClick={exportReport} className=\"btn btn-outline-secondary\">");
            js.AppendLine("                        <i className=\"bi bi-download me-2\" aria-hidden=\"true\"></i>");
            js.AppendLine("                        Exportar");
            js.AppendLine("                    </button>");
            js.AppendLine("                </div>");
            js.AppendLine("            )}");
            js.AppendLine("        </div>");
            js.AppendLine("    );");
            js.AppendLine("};");
            js.AppendLine("");
            js.AppendLine("export default FilterComponent;");

            File.WriteAllText(path + "//FilterComponent.jsx", js.ToString());
        }

        private static void CreateAdminAddUserModalFiles(string path)
        {
            StringBuilder js = new StringBuilder();
            js.AppendLine("import React, { useState } from 'react';");
            js.AppendLine("import adminApi from '../../../../services/apiServices/adminApi';");
            js.AppendLine("import { useDispatch } from 'react-redux';");
            js.AppendLine("import { setLoading } from '../../../../services/redux/loadingSlice';");
            js.AppendLine("import MessageModal from '../../../common/Modals/MessageModal/MessageModal';");
            js.AppendLine("import { useNavigate } from 'react-router-dom';");
            js.AppendLine("");
            js.AppendLine("const AddUserModal = ({ isOpen, closeModal }) => {");
            js.AppendLine("    const navigate = useNavigate();");
            js.AppendLine("    const [name, setName] = useState('');");
            js.AppendLine("    const [username, setUsername] = useState('');");
            js.AppendLine("    const [message, setMessage] = useState('');");
            js.AppendLine("    const [isMessageOpen, setIsMessageOpen] = useState(false);");
            js.AppendLine("    const dispatch = useDispatch();");
            js.AppendLine("");
            js.AppendLine("    const closeMessageModal = () => {");
            js.AppendLine("        setIsMessageOpen(false);");
            js.AppendLine("        if (message.includes('sucesso')) {");
            js.AppendLine("            closeModal();");
            js.AppendLine("            navigate('/usuarios');");
            js.AppendLine("        }");
            js.AppendLine("    };");
            js.AppendLine("");
            js.AppendLine("    const handleSubmit = async (event) => {");
            js.AppendLine("        event.preventDefault();");
            js.AppendLine("        try {");
            js.AppendLine("            dispatch(setLoading(true));");
            js.AppendLine("            const response = await adminApi.createAdmin({ name, username });");
            js.AppendLine("            setMessage(response.Id ? 'Usuário criado com sucesso' : 'Erro ao criar usuário. Verifique os dados e tente novamente.');");
            js.AppendLine("            setUsername('');");
            js.AppendLine("            setName('');");
            js.AppendLine("            setIsMessageOpen(true);");
            js.AppendLine("        } catch {");
            js.AppendLine("            setMessage('Erro ao criar usuário. Verifique os dados e tente novamente.');");
            js.AppendLine("            setIsMessageOpen(true);");
            js.AppendLine("        } finally {");
            js.AppendLine("            dispatch(setLoading(false));");
            js.AppendLine("        }");
            js.AppendLine("    };");
            js.AppendLine("");
            js.AppendLine("    if (!isOpen) return null;");
            js.AppendLine("");
            js.AppendLine("    return (");
            js.AppendLine("        <>");
            js.AppendLine("            <MessageModal isOpen={isMessageOpen} click={closeMessageModal} message={message} />");
            js.AppendLine("            <div className=\"modal d-block\" tabIndex=\"-1\" role=\"dialog\" aria-modal=\"true\" aria-labelledby=\"add-user-title\">");
            js.AppendLine("                <div className=\"modal-dialog modal-dialog-centered\">");
            js.AppendLine("                    <div className=\"modal-content border-0 shadow\">");
            js.AppendLine("                        <div className=\"modal-header\">");
            js.AppendLine("                            <h2 id=\"add-user-title\" className=\"modal-title h5\">Adicionar usuário</h2>");
            js.AppendLine("                            <button type=\"button\" className=\"btn-close\" onClick={closeModal} aria-label=\"Fechar\"></button>");
            js.AppendLine("                        </div>");
            js.AppendLine("                        <form onSubmit={handleSubmit}>");
            js.AppendLine("                            <div className=\"modal-body\">");
            js.AppendLine("                                <div className=\"mb-3\">");
            js.AppendLine("                                    <label htmlFor=\"name\" className=\"form-label\">Nome</label>");
            js.AppendLine("                                    <input type=\"text\" id=\"name\" className=\"form-control\" placeholder=\"Digite o nome completo\" value={name} onChange={(event) => setName(event.target.value)} required />");
            js.AppendLine("                                </div>");
            js.AppendLine("                                <div className=\"mb-0\">");
            js.AppendLine("                                    <label htmlFor=\"username\" className=\"form-label\">Email</label>");
            js.AppendLine("                                    <input type=\"email\" id=\"username\" className=\"form-control\" placeholder=\"Digite o email\" value={username} onChange={(event) => setUsername(event.target.value)} required />");
            js.AppendLine("                                </div>");
            js.AppendLine("                            </div>");
            js.AppendLine("                            <div className=\"modal-footer\">");
            js.AppendLine("                                <button type=\"button\" className=\"btn btn-outline-secondary\" onClick={closeModal}>Cancelar</button>");
            js.AppendLine("                                <button type=\"submit\" className=\"btn btn-primary\">Salvar</button>");
            js.AppendLine("                            </div>");
            js.AppendLine("                        </form>");
            js.AppendLine("                    </div>");
            js.AppendLine("                </div>");
            js.AppendLine("            </div>");
            js.AppendLine("            <div className=\"modal-backdrop show\"></div>");
            js.AppendLine("        </>");
            js.AppendLine("    );");
            js.AppendLine("};");
            js.AppendLine("");
            js.AppendLine("export default AddUserModal;");

            File.WriteAllText(path + "//AddUserModal.jsx", js.ToString());
        }

        private static string EscapeJsString(string value)
        {
            return (value ?? string.Empty).Replace("\\", "\\\\").Replace("'", "\\'");
        }
    }
}
