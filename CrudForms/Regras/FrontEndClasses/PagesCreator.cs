using Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Regras.FrontEndClasses
{
    public static class PagesCreator
    {
        public static bool Create(List<MD_Tabela> tabelas, string projectPath, string projectName)
        {
            bool success = true;

            try
            {
                string pagesAdmin = NamesHandler.GetDirectoryByType(projectPath, NamesHandler.FileType.PagesAdmin);
                string dashboard = Path.Combine(pagesAdmin, "DashBoard");
                Directory.CreateDirectory(dashboard);
                CreateDashBoardPage(dashboard);

                string pagesCommon = NamesHandler.GetDirectoryByType(projectPath, NamesHandler.FileType.PagesCommon);
                string confirmUserPage = Path.Combine(pagesCommon, "ConfirmUserPage");
                Directory.CreateDirectory(confirmUserPage);
                CreateConfirmUserPage(confirmUserPage);

                string createLoginPage = Path.Combine(pagesCommon, "LoginPage");
                Directory.CreateDirectory(createLoginPage);
                CreateLoginPage(createLoginPage);

                string createRecoverPasswordPage = Path.Combine(pagesCommon, "RecoverPasswordPage");
                Directory.CreateDirectory(createRecoverPasswordPage);
                CreateRecoverPasswordPage(createRecoverPasswordPage, projectName);

                foreach (var tabela in tabelas)
                {
                    string componentName = NamesHandler.CreateComponentName(tabela.DAO.Nome);
                    string path = Path.Combine(pagesAdmin, componentName);
                    Directory.CreateDirectory(path);

                    string nameListPage = NamesHandler.CreateComponentListName(tabela.DAO.Nome);
                    path = Path.Combine(path, nameListPage);

                    Directory.CreateDirectory(path);
                    CreateListPages(tabela, path);

                    string namePage = NamesHandler.CreateComponentPageName(tabela.DAO.Nome);
                    path = Path.Combine(pagesAdmin, componentName, namePage);

                    Directory.CreateDirectory(path);
                    CreatePage(tabela, path);
                }
            }
            catch (Exception ex)
            {
                Util.CL_Files.LogException(ex);
                success = false;
            }

            return success;
        }

        private static void CreateListPages(MD_Tabela tabela, string path)
        {
            string name = NamesHandler.CreateComponentListName(tabela.DAO.Nome);
            StringBuilder js = new StringBuilder();
            js.AppendLine("import React, { useState, useEffect } from 'react';");
            js.AppendLine($"import './{name}.css';");
            js.AppendLine($"import {NamesHandler.GetApiName(tabela.DAO.Nome)} from '../../../../services/apiServices/{NamesHandler.GetApiName(tabela.DAO.Nome)}';");
            js.AppendLine("import { setLoading } from '../../../../services/redux/loadingSlice';");
            js.AppendLine("import { useDispatch } from 'react-redux';");
            js.AppendLine("import configService from '../../../../services/configService';");
            js.AppendLine("import Pagination from '../../../../components/common/Pagination/Pagination'; ");
            js.AppendLine("import { toast } from 'react-toastify';");
            js.AppendLine("import { putDateOnPattern } from '../../../../utils/functions';");
            js.AppendLine("import FilterComponent from '../../../../components/admin/FilterComponent/FilterComponent';");
            js.AppendLine("");
            js.AppendLine($"const {name} = () => {{");
            js.AppendLine("    const dispatch = useDispatch();");
            js.AppendLine("    const [items, setItems] = useState([]);");
            js.AppendLine("    const [page, setPage] = useState(1);");
            js.AppendLine("    const [searchTerm, setSearchTerm] = useState('');");
            js.AppendLine("    const [startDate, setStartDate] = useState('');");
            js.AppendLine("    const [endDate, setEndDate] = useState('');");
            js.AppendLine("    const [totalPages, setTotalPages] = useState(1);");
            js.AppendLine("    const [totalItens, setTotalItens] = useState(0);");
            js.AppendLine("    const quantity = configService.getDefaultNumberOfItemsTable(); ");
            js.AppendLine("    const orderBy = \"Id:Desc\";");
            js.AppendLine("");
            js.AppendLine("    useEffect(() => {");
            js.AppendLine("        const fetchItems = async () => {");
            js.AppendLine("            dispatch(setLoading(true));");
            js.AppendLine("            try {");
            js.AppendLine($"                const response = await {NamesHandler.GetApiName(tabela.DAO.Nome)}.getPaginated({{ page, quantity, orderBy, term: searchTerm, startDate, endDate, include: \"\" }});");
            js.AppendLine("");
            js.AppendLine("                setItems(response.Results);");
            js.AppendLine("                setTotalPages(response.TotalPages);");
            js.AppendLine("                setTotalItens(response.TotalCount);");
            js.AppendLine("            } catch (error) {");
            js.AppendLine("                toast.error('Erro ao buscar os itens.');");
            js.AppendLine("            } finally {");
            js.AppendLine("                dispatch(setLoading(false));");
            js.AppendLine("            }");
            js.AppendLine("        };");
            js.AppendLine("        fetchItems();");
            js.AppendLine("    }, [page, quantity, searchTerm, startDate, endDate, dispatch]);");
            js.AppendLine("");
            js.AppendLine("    const handlePageChange = (newPage) => {");
            js.AppendLine("        if (newPage > 0 && newPage <= totalPages) {");
            js.AppendLine("            setPage(newPage);");
            js.AppendLine("        }");
            js.AppendLine("    };");
            js.AppendLine("");
            js.AppendLine("    const search = ({term, startDate, endDate} = {}) => {");
            js.AppendLine("        setSearchTerm(term);");
            js.AppendLine("        setStartDate(startDate);");
            js.AppendLine("        setEndDate(endDate);");
            js.AppendLine("    }");
            js.AppendLine("");
            js.AppendLine("    const exportFunction = async ({term}) => {");
            js.AppendLine("        try {");
            js.AppendLine("            dispatch(setLoading(true));");
            js.AppendLine($"            const response = await {NamesHandler.GetApiName(tabela.DAO.Nome)}.export({{ term: term, startDate, endDate }});");
            js.AppendLine("");
            js.AppendLine("            if (response.Status === 200 && response.Object) {");
            js.AppendLine("                window.open(response.Object, \"_blank\");");
            js.AppendLine("                toast.success('Relatório gerado com sucesso!');");
            js.AppendLine("            } else {");
            js.AppendLine("                toast.error('Erro ao gerar o relatório');");
            js.AppendLine("            }");
            js.AppendLine("        } catch (error) {");
            js.AppendLine("            toast.error('Erro ao gerar o relatório');");
            js.AppendLine("        }");
            js.AppendLine("        finally{");
            js.AppendLine("            dispatch(setLoading(false));");
            js.AppendLine("        }");
            js.AppendLine("    };");
            js.AppendLine("");
            js.AppendLine("    return (");
            js.AppendLine("    <div className=\"container-admin-page\">");
            js.AppendLine("        <h1>Lista dos Itens</h1>");
            js.AppendLine("        <div className='container-admin-page-filters div-with-border'>");
            js.AppendLine("            <h3>Filtros</h3>");
            js.AppendLine("            <FilterComponent placeHolder={'Descrição'} showTermFilter={true} showStartDate={true} showEndDate={true} submitFilter={search} exportFunction={exportFunction}/>");
            js.AppendLine("        </div>");
            js.AppendLine("        <div className='container-admin-page-table div-with-border'>");
            js.AppendLine("            <table className=\"admin-table\">");
            js.AppendLine("                <thead>");
            js.AppendLine("                    <tr>");
            foreach(var item in tabela.DAO.CamposDaTabela())
            {
                js.AppendLine($"                        <th>{item.Nome}</th>");
            }
            js.AppendLine("                    </tr>");
            js.AppendLine("                </thead>");
            js.AppendLine("                <tbody>");
            js.AppendLine("                {items.map((item) => (");
            js.AppendLine("                    <tr key={item.Id}>");
            foreach (var item in tabela.DAO.CamposDaTabela())
            {
                if(item.TipoCampo.Nome.ToUpper().Contains("DATE"))
                {
                    js.AppendLine($"                        <td data-label='{item.Nome}'><span>{{putDateOnPattern(item.{item.Nome})}}</span></td>");
                }
                else
                {
                    js.AppendLine($"                        <td data-label='{item.Nome}'><span>{{item.{item.Nome}}}</span></td>");
                }
            }
            js.AppendLine("                    </tr>");
            js.AppendLine("                ))}");
            js.AppendLine("                </tbody>");
            js.AppendLine("            </table>");
            js.AppendLine("            <sub>Total de Itens: {totalItens}</sub>");
            js.AppendLine("            <Pagination page={page} totalPages={totalPages} onPageChange={handlePageChange} />");
            js.AppendLine("        </div>");
            js.AppendLine("    </div>");
            js.AppendLine("    );");
            js.AppendLine("};");
            js.AppendLine("");
            js.AppendLine($"export default {name};");
            js.AppendLine("");

            File.WriteAllText(path + $"//{name}.js", js.ToString());
            File.WriteAllText(path + $"//{name}.css", string.Empty);
        }

        private static void CreatePage(MD_Tabela tabela, string path)
        {
            string name = NamesHandler.CreateComponentPageName(tabela.DAO.Nome);
            StringBuilder js = new StringBuilder();
            js.AppendLine("import React, { useState, useEffect } from 'react';");
            js.AppendLine($"import './{name}.css';");
            js.AppendLine($"import {NamesHandler.GetApiName(tabela.DAO.Nome)} from '../../../../services/apiServices/{NamesHandler.GetApiName(tabela.DAO.Nome)}';");
            js.AppendLine("import { setLoading } from '../../../../services/redux/loadingSlice';");
            js.AppendLine("import { useDispatch } from 'react-redux';");
            js.AppendLine("import { useParams } from 'react-router-dom';");
            js.AppendLine("import { toast } from 'react-toastify';");
            js.AppendLine("import { putDateOnPattern } from '../../../../utils/functions';");
            js.AppendLine("");
            js.AppendLine($"const {name} = () => {{");
            js.AppendLine("    const dispatch = useDispatch();");
            js.AppendLine("    const { code } = useParams();");
            js.AppendLine("    const [item, setItem] = useState([]);");
            js.AppendLine("");
            js.AppendLine("    useEffect(() => {");
            js.AppendLine("        const fetchItem = async () => {");
            js.AppendLine("            dispatch(setLoading(true));");
            js.AppendLine("            try {");
            js.AppendLine($"               const response = await {NamesHandler.GetApiName(tabela.DAO.Nome)}.getByCode(code, {{include: ''}});");
            js.AppendLine("");
            js.AppendLine("                setItem(response);");
            js.AppendLine("            } catch (error) {");
            js.AppendLine("                toast.error('Erro ao buscar.');");
            js.AppendLine("            } finally {");
            js.AppendLine("                dispatch(setLoading(false));");
            js.AppendLine("            }");
            js.AppendLine("        };");
            js.AppendLine("        fetchItem();");
            js.AppendLine("    }, [code, dispatch]);");
            js.AppendLine("");
            js.AppendLine("    return (");
            js.AppendLine("    <div className=\"container-admin-page\">");
            js.AppendLine("        <h1>Informações</h1>");
            js.AppendLine("            <div className=\"box space-double-bottom\">");
            js.AppendLine("                <div className=\"info-group\">");
            foreach (var item in tabela.DAO.CamposDaTabela())
            {
                if (item.TipoCampo.Nome.ToUpper().Contains("DATE"))
                {
                    js.AppendLine($"           <p><strong>{item.TipoCampo.Nome}:</strong>{{putDateOnPattern(item.{item.Nome})}}</p>");
                }
                else
                {
                    js.AppendLine($"           <p><strong>{item.TipoCampo.Nome}:</strong>{{item.{item.Nome}}}</p>");
                }
            }
            js.AppendLine("                </div>");
            js.AppendLine("            </div>");
            js.AppendLine("    </div>");
            js.AppendLine("    );");
            js.AppendLine("};");
            js.AppendLine("");
            js.AppendLine($"export default {name};");
            js.AppendLine("");

            File.WriteAllText(path + $"//{name}.js", js.ToString());
            File.WriteAllText(path + $"//{name}.css", string.Empty);
        }

        private static void CreateRecoverPasswordPage(string path, string projectName)
        {
            StringBuilder js = new StringBuilder();
            js.AppendLine("import React, { useState } from \"react\";");
            js.AppendLine("import { useDispatch } from \"react-redux\";");
            js.AppendLine("import { setLoading } from \"../../../../services/redux/loadingSlice\";");
            js.AppendLine("import adminApi from \"../../../../services/apiServices/adminApi\"; ");
            js.AppendLine("import MessageModal from \"../../../../components/common/Modals/MessageModal/MessageModal\";");
            js.AppendLine("import { useNavigate } from \"react-router-dom\";");
            js.AppendLine("import { toast } from \"react-toastify\";");
            js.AppendLine("");
            js.AppendLine("const RecoverPasswordPage = () => {");
            js.AppendLine("    const [email, setEmail] = useState(\"\");");
            js.AppendLine("    const [message, setMessage] = useState(\"\");");
            js.AppendLine("    const [isMessageOpen, setIsMessageOpen] = useState(false);");
            js.AppendLine("    const dispatch = useDispatch();");
            js.AppendLine("    const navigate = useNavigate();");
            js.AppendLine("");
            js.AppendLine("    const handleRecover = async (e) => {");
            js.AppendLine("        e.preventDefault();");
            js.AppendLine("        dispatch(setLoading(true));");
            js.AppendLine("");
            js.AppendLine("        try {");
            js.AppendLine("            const response = await adminApi.recoverPass(email);");
            js.AppendLine("");
            js.AppendLine("            if (response) {");
            js.AppendLine("                toast.success(\"Se este email estiver cadastrado, você receberá um link para redefinir sua senha.\");");
            js.AppendLine("                navigate(\"/\"); // Redirect to login");
            js.AppendLine("            } else {");
            js.AppendLine("                setMessage(\"Erro ao solicitar recuperação de senha.\");");
            js.AppendLine("            }");
            js.AppendLine("        } catch (error) {");
            js.AppendLine("            setMessage(\"Erro ao processar a solicitação. Tente novamente.\");");
            js.AppendLine("        } finally {");
            js.AppendLine("            setIsMessageOpen(true);");
            js.AppendLine("            dispatch(setLoading(false));");
            js.AppendLine("        }");
            js.AppendLine("    };");
            js.AppendLine("");
            js.AppendLine("    return (");
            js.AppendLine("        <div className=\"login-page-container\">");
            js.AppendLine("            <MessageModal isOpen={isMessageOpen} click={() => setIsMessageOpen(false)} message={message} />");
            js.AppendLine("            <form className=\"login-page-form\" onSubmit={handleRecover}>");
            js.AppendLine("                <div className=\"login-page-form-group\">");
            js.AppendLine("                    <div className=\"div-center margin-bottom-double-default flex-column\">");
            js.AppendLine($"                        <h1>{projectName}</h1>");
            js.AppendLine("                    </div>");
            js.AppendLine("");
            js.AppendLine("                    <div className=\"login-page-form-group\">");
            js.AppendLine("                        <label htmlFor=\"email\" className=\"login-page-label\">Digite seu e-mail</label>");
            js.AppendLine("                        <input");
            js.AppendLine("                            type=\"email\"");
            js.AppendLine("                            id=\"email\"");
            js.AppendLine("                            className=\"login-page-input\"");
            js.AppendLine("                            value={email}");
            js.AppendLine("                            onChange={(e) => setEmail(e.target.value)}");
            js.AppendLine("                            placeholder=\"Digite seu e-mail cadastrado\"");
            js.AppendLine("                            required");
            js.AppendLine("                        />");
            js.AppendLine("                    </div>");
            js.AppendLine("");
            js.AppendLine("                    <button type=\"submit\" className=\"login-page-submit-button margin-bottom-double-default\">Recuperar Senha</button>");
            js.AppendLine("                    <button className=\"main-button\" onClick={()=> navigate('/')}>Voltar</button>");
            js.AppendLine("                </div>");
            js.AppendLine("            </form>");
            js.AppendLine("        </div>");
            js.AppendLine("    );");
            js.AppendLine("};");
            js.AppendLine("");
            js.AppendLine("export default RecoverPasswordPage;");

            File.WriteAllText(path + "//RecoverPasswordPage.js", js.ToString());
        }

        private static void CreateLoginPage(string path)
        {
            StringBuilder css = new StringBuilder();
            css.AppendLine(".login-page-container {");
            css.AppendLine("    display: flex;");
            css.AppendLine("    flex-direction: column;");
            css.AppendLine("    align-items: center;");
            css.AppendLine("    justify-content: center;");
            css.AppendLine("    height: 100vh;");
            css.AppendLine("    padding: var(--double-default-spacing);");
            css.AppendLine("    background: linear-gradient(to right, var(--button-color-primary-hover), var(--primary-color));");
            css.AppendLine("}");
            css.AppendLine("");
            css.AppendLine(".login-page-description {");
            css.AppendLine("    color: var(--text-color-primary);");
            css.AppendLine("    font-size: 14px;");
            css.AppendLine("    margin-bottom: var(--default-spacing);");
            css.AppendLine("    text-align: center;");
            css.AppendLine("}");
            css.AppendLine("");
            css.AppendLine(".login-page-form {");
            css.AppendLine("    background-color: var(--color-surface);");
            css.AppendLine("    padding: var(--double-default-spacing);");
            css.AppendLine("    border-radius: var(--default-border-radius-extra);");
            css.AppendLine("    box-shadow: 0 4px 10px var(--shadow-color-primary);");
            css.AppendLine("    width: 100%;");
            css.AppendLine("    max-width: 600px;");
            css.AppendLine("    display: flex;");
            css.AppendLine("    flex-direction: row;");
            css.AppendLine("    justify-content: center;");
            css.AppendLine("    align-items: center;");
            css.AppendLine("    gap: var(--double-default-spacing);");
            css.AppendLine("}");
            css.AppendLine("");
            css.AppendLine(".login-page-form-group {");
            css.AppendLine("    display: flex;");
            css.AppendLine("    flex-direction: column;");
            css.AppendLine("    justify-self: center;");
            css.AppendLine("    width: 100%;");
            css.AppendLine("}");
            css.AppendLine("");
            css.AppendLine(".login-page-label {");
            css.AppendLine("    font-size: 14px;");
            css.AppendLine("    color: var(--text-color-primary);");
            css.AppendLine("    font-weight: 500;");
            css.AppendLine("    margin-bottom: 4px;");
            css.AppendLine("}");
            css.AppendLine("");
            css.AppendLine(".login-page-input {");
            css.AppendLine("    width: 100%;");
            css.AppendLine("    padding: 0.75rem;");
            css.AppendLine("    border: 1px solid var(--primary-color);");
            css.AppendLine("    border-radius: var(--default-border-radius);");
            css.AppendLine("    font-size: 14px;");
            css.AppendLine("    color: var(--text-color-primary);");
            css.AppendLine("}");
            css.AppendLine("");
            css.AppendLine(".login-page-input:focus {");
            css.AppendLine("    outline: none;");
            css.AppendLine("    border-color: var(--button-color-primary);");
            css.AppendLine("    box-shadow: 0 0 4px var(--button-color-primary);");
            css.AppendLine("}");
            css.AppendLine("");
            css.AppendLine(".login-page-error-message {");
            css.AppendLine("    color: var(--button-color-primary-wrong);");
            css.AppendLine("    font-size: 14px;");
            css.AppendLine("    text-align: center;");
            css.AppendLine("    margin-top: var(--default-spacing);");
            css.AppendLine("}");
            css.AppendLine("");
            css.AppendLine(".login-page-submit-button {");
            css.AppendLine("    margin-top: var(--triple-default-spacing);");
            css.AppendLine("    padding: var(--default-spacing);");
            css.AppendLine("    background-color: var(--button-color-primary);");
            css.AppendLine("    color: var(--text-color-button);");
            css.AppendLine("    font-size: 16px;");
            css.AppendLine("    font-weight: bold;");
            css.AppendLine("    border: none;");
            css.AppendLine("    border-radius: var(--default-border-radius);");
            css.AppendLine("    cursor: pointer;");
            css.AppendLine("    transition: background-color 0.3s;");
            css.AppendLine("}");
            css.AppendLine("");
            css.AppendLine(".login-page-submit-button:hover {");
            css.AppendLine("    background-color: var(--button-color-primary-hover);");
            css.AppendLine("}");
            css.AppendLine("");
            css.AppendLine(".login-page-submit-button:disabled {");
            css.AppendLine("    background-color: var(--button-color-primary-disabled);");
            css.AppendLine("    cursor: not-allowed;");
            css.AppendLine("}");
            css.AppendLine("");
            css.AppendLine(".login-page-forgot-password {");
            css.AppendLine("    text-align: center;");
            css.AppendLine("    margin-top: var(--default-spacing);");
            css.AppendLine("}");
            css.AppendLine("");
            css.AppendLine(".login-page-forgot-password a {");
            css.AppendLine("    color: var(--text-color-accent);");
            css.AppendLine("    text-decoration: none;");
            css.AppendLine("    font-size: 14px;");
            css.AppendLine("    font-weight: bold;");
            css.AppendLine("    transition: color 0.3s ease-in-out;");
            css.AppendLine("}");
            css.AppendLine("");
            css.AppendLine(".login-page-forgot-password a:hover {");
            css.AppendLine("    color: var(--text-color-primary);");
            css.AppendLine("    text-decoration: underline;");
            css.AppendLine("}");
            css.AppendLine("");
            css.AppendLine(".logo-class-img img{");
            css.AppendLine("    width: 16rem;");
            css.AppendLine("    height: 24rem;");
            css.AppendLine("}");
            css.AppendLine("");
            css.AppendLine("@media (max-width: 1000px) {");
            css.AppendLine("    .login-page-form {");
            css.AppendLine("        display: flex;");
            css.AppendLine("        flex-direction: column;");
            css.AppendLine("        gap: var(--double-default-spacing);");
            css.AppendLine("    }");
            css.AppendLine("");
            css.AppendLine("    .logo-class-img img{");
            css.AppendLine("        width: 20rem;");
            css.AppendLine("        height: 28rem;");
            css.AppendLine("    }");
            css.AppendLine("}");

            StringBuilder js = new StringBuilder();
            js.AppendLine("import React, { useState } from \"react\";");
            js.AppendLine("import { useDispatch } from \"react-redux\";");
            js.AppendLine("import { login } from \"../../../services/redux/authSlice\";");
            js.AppendLine("import tokenApi from \"../../../services/apiServices/tokenApi\"; // Adjust the path based on your project structure");
            js.AppendLine("import \"./LoginPage.css\";");
            js.AppendLine("import { setLoading } from '../../../services/redux/loadingSlice';");
            js.AppendLine("import MessageModal from \"../../../components/common/Modals/MessageModal/MessageModal\";");
            js.AppendLine("");
            js.AppendLine("const LoginPage = () => {");
            js.AppendLine("    const [identifier, setIdentifier] = useState(\"\"); // For email (admin) or CPF/CNPJ (client)");
            js.AppendLine("    const [password, setPassword] = useState(\"\"); // For admin login");
            js.AppendLine("    const [error, setError] = useState(\"\");");
            js.AppendLine("    const [isMessageOpen, setIsMessageOpen] = useState(false);");
            js.AppendLine("    const [message, setMessage] = useState(false);");
            js.AppendLine("    const dispatch = useDispatch();");
            js.AppendLine("");
            js.AppendLine("    const openMessageModal = () => {");
            js.AppendLine("        setIsMessageOpen(true);");
            js.AppendLine("    }");
            js.AppendLine("");
            js.AppendLine("    const closeMessageModal = () => {");
            js.AppendLine("        setIsMessageOpen(false);");
            js.AppendLine("    }");
            js.AppendLine("");
            js.AppendLine("    const handleSubmit = async (e) => {");
            js.AppendLine("        e.preventDefault();");
            js.AppendLine("        try {");
            js.AppendLine("            dispatch(setLoading(true));");
            js.AppendLine("            let payload = { userName: identifier, password: password }; ");
            js.AppendLine("            const response = await tokenApi.getToken(payload);");
            js.AppendLine("");
            js.AppendLine("            if(response.code)");
            js.AppendLine("            {");
            js.AppendLine("                setMessage(response.message);");
            js.AppendLine("                openMessageModal();");
            js.AppendLine("            }");
            js.AppendLine("            else{");
            js.AppendLine("                // Dispatch the login action to Redux");
            js.AppendLine("                dispatch(");
            js.AppendLine("                    login({");
            js.AppendLine("                        access_token: response.access_token,");
            js.AppendLine("                        nameListPage: response.nameListPage,");
            js.AppendLine("                        code: response.id,");
            js.AppendLine("                    })");
            js.AppendLine("                );");
            js.AppendLine("            }");
            js.AppendLine("");
            js.AppendLine("        } catch (err) {");
            js.AppendLine("            setError(\"Falha na autenticação. Verifique seus dados e tente novamente.\");");
            js.AppendLine("            console.error(\"Login failed:\", err);");
            js.AppendLine("        }");
            js.AppendLine("        finally{");
            js.AppendLine("            dispatch(setLoading(false));");
            js.AppendLine("        }");
            js.AppendLine("    };");
            js.AppendLine("");
            js.AppendLine("    return (");
            js.AppendLine("        <div className=\"login-page-container\">");
            js.AppendLine("            <MessageModal isOpen={isMessageOpen} click={closeMessageModal} message={message} />");
            js.AppendLine("            <form className=\"login-page-form\" onSubmit={handleSubmit}>");
            js.AppendLine("                <div className=\"login-page-form-group\">");
            js.AppendLine("                    <div className=\"div-center margin-bottom-double-default flex-column\">");
            js.AppendLine("                        <h1>");
            js.AppendLine("                            Nome");
            js.AppendLine("                        </h1>");
            js.AppendLine("                    </div>");
            js.AppendLine("                    <div className=\"login-page-form-group\">");
            js.AppendLine("                        <label htmlFor=\"identifier\" className=\"login-page-label\">");
            js.AppendLine("                            Email");
            js.AppendLine("                        </label>");
            js.AppendLine("                        <input");
            js.AppendLine("                            type=\"email\"");
            js.AppendLine("                            id=\"identifier\"");
            js.AppendLine("                            className=\"login-page-input\"");
            js.AppendLine("                            value={identifier}");
            js.AppendLine("                            onChange={(e) => setIdentifier(e.target.value)}");
            js.AppendLine("                            placeholder=\"Digite seu email\"");
            js.AppendLine("                            required");
            js.AppendLine("                        />");
            js.AppendLine("                    </div>");
            js.AppendLine("                    <div className=\"login-page-form-group\">");
            js.AppendLine("                        <label htmlFor=\"password\" className=\"login-page-label\">Senha</label>");
            js.AppendLine("                        <input");
            js.AppendLine("                            type=\"password\"");
            js.AppendLine("                            id=\"password\"");
            js.AppendLine("                            className=\"login-page-input\"");
            js.AppendLine("                            value={password}");
            js.AppendLine("                            onChange={(e) => setPassword(e.target.value)}");
            js.AppendLine("                            placeholder=\"Digite sua senha\"");
            js.AppendLine("                            required");
            js.AppendLine("                        />");
            js.AppendLine("                    </div>");
            js.AppendLine("                    {error && <p className=\"login-page-error-message\">{error}</p>}");
            js.AppendLine("                    <p className=\"login-page-forgot-password\">");
            js.AppendLine("                        <a href=\"/recuperar-senha\">Esqueceu a senha?</a>");
            js.AppendLine("                    </p>");
            js.AppendLine("                    <button type=\"submit\" className=\"login-page-submit-button\">Acessar</button>");
            js.AppendLine("                </div>");
            js.AppendLine("            </form>");
            js.AppendLine("        </div>");
            js.AppendLine("    );");
            js.AppendLine("};");
            js.AppendLine("");
            js.AppendLine("export default LoginPage;");

            File.WriteAllText(path + "//LoginPage.js", js.ToString());
            File.WriteAllText(path + "//LoginPage.css", css.ToString());
        }

        private static void CreateConfirmUserPage(string path)
        {
            StringBuilder js = new StringBuilder();
            js.AppendLine("import React, { useEffect, useState } from \"react\";");
            js.AppendLine("import { useNavigate, useSearchParams } from \"react-router-dom\";");
            js.AppendLine("import { useDispatch } from \"react-redux\";");
            js.AppendLine("import { setLoading } from \"../../../services/redux/loadingSlice\";");
            js.AppendLine("import adminApi from \"../../../services/apiServices/adminApi\";");
            js.AppendLine("import { toast } from \"react-toastify\";");
            js.AppendLine("import MessageModal from \"../../../components/common/Modals/MessageModal/MessageModal\";");
            js.AppendLine("");
            js.AppendLine("const ConfirmUserPage = ({isRecover}) => {");
            js.AppendLine("    const dispatch = useDispatch();");
            js.AppendLine("    const navigate = useNavigate();");
            js.AppendLine("    const [searchParams] = useSearchParams();");
            js.AppendLine("    const [password, setPassword] = useState(\"\");");
            js.AppendLine("    const [verifyPassword, setVerifyPassword] = useState(\"\");");
            js.AppendLine("    const [token, setToken] = useState(\"\");");
            js.AppendLine("    const [isMessageOpen, setIsMessageOpen] = useState(false);");
            js.AppendLine("    const [message, setMessage] = useState(\"\");");
            js.AppendLine("");
            js.AppendLine("    useEffect(() => {");
            js.AppendLine("        const guidToken = searchParams.get(\"token\"); // Extract token from URL");
            js.AppendLine("        if (guidToken) {");
            js.AppendLine("            setToken(guidToken);");
            js.AppendLine("        } else {");
            js.AppendLine("            setMessage(\"Token inválido ou expirado.\");");
            js.AppendLine("            setIsMessageOpen(true);");
            js.AppendLine("        }");
            js.AppendLine("    }, [searchParams]);");
            js.AppendLine("");
            js.AppendLine("    const handleSubmit = async (e) => {");
            js.AppendLine("        e.preventDefault();");
            js.AppendLine("        if (password !== verifyPassword) {");
            js.AppendLine("            toast.error(\"As senhas não coincidem.\");");
            js.AppendLine("            return;");
            js.AppendLine("        }");
            js.AppendLine("");
            js.AppendLine("        dispatch(setLoading(true));");
            js.AppendLine("        try {");
            js.AppendLine("            const response = await adminApi.confirmUser({");
            js.AppendLine("                password,");
            js.AppendLine("                verifypassword: verifyPassword,");
            js.AppendLine("                guid: token,");
            js.AppendLine("            });");
            js.AppendLine("");
            js.AppendLine("            if (!response) {");
            js.AppendLine("                setMessage(\"O token expirou. Solicite um novo email de confirmação.\");");
            js.AppendLine("                setIsMessageOpen(true);");
            js.AppendLine("            } else {");
            js.AppendLine("                toast.success(isRecover ? \"Senha atualizada com sucesso!\" : \"Cadastro confirmado com sucesso!\");");
            js.AppendLine("                navigate(\"/\"); // Redirect to login");
            js.AppendLine("            }");
            js.AppendLine("        } catch (error) {");
            js.AppendLine("            toast.error(\"Erro ao confirmar o usuário.\");");
            js.AppendLine("        } finally {");
            js.AppendLine("            dispatch(setLoading(false));");
            js.AppendLine("        }");
            js.AppendLine("    };");
            js.AppendLine("");
            js.AppendLine("    return (");
            js.AppendLine("        <div className=\"login-page-container\">");
            js.AppendLine("            <MessageModal isOpen={isMessageOpen} click={() => navigate(\"/\")} message={message} />");
            js.AppendLine("            <form className=\"login-page-form\" onSubmit={handleSubmit}>");
            js.AppendLine("                <div className=\"login-page-form-group\">");
            js.AppendLine("");
            js.AppendLine("                    {");
            js.AppendLine("                        isRecover ? ");
            js.AppendLine("                        <div className=\"div-center margin-bottom-double-default flex-column\">");
            js.AppendLine("                            <h1>Recuperação de senha</h1>");
            js.AppendLine("                            <p>Por favor, defina uma senha para acessar sua conta.</p>");
            js.AppendLine("                        </div>");
            js.AppendLine("                        :");
            js.AppendLine("                        <div className=\"div-center margin-bottom-double-default flex-column\">");
            js.AppendLine("                            <h1>Bem-vindo!</h1>");
            js.AppendLine("                            <p>Por favor, defina uma senha para ativar sua conta.</p>");
            js.AppendLine("                        </div>");
            js.AppendLine("");
            js.AppendLine("                    }");
            js.AppendLine("");
            js.AppendLine("                    <div className=\"login-page-form-group\">");
            js.AppendLine("                        <label htmlFor=\"password\" className=\"login-page-label\">Senha</label>");
            js.AppendLine("                        <input");
            js.AppendLine("                            type=\"password\"");
            js.AppendLine("                            id=\"password\"");
            js.AppendLine("                            className=\"login-page-input\"");
            js.AppendLine("                            value={password}");
            js.AppendLine("                            onChange={(e) => setPassword(e.target.value)}");
            js.AppendLine("                            placeholder=\"Digite sua senha\"");
            js.AppendLine("                            required");
            js.AppendLine("                        />");
            js.AppendLine("                    </div>");
            js.AppendLine("");
            js.AppendLine("                    <div className=\"login-page-form-group\">");
            js.AppendLine("                        <label htmlFor=\"verifyPassword\" className=\"login-page-label\">Confirmar Senha</label>");
            js.AppendLine("                        <input");
            js.AppendLine("                            type=\"password\"");
            js.AppendLine("                            id=\"verifyPassword\"");
            js.AppendLine("                            className=\"login-page-input\"");
            js.AppendLine("                            value={verifyPassword}");
            js.AppendLine("                            onChange={(e) => setVerifyPassword(e.target.value)}");
            js.AppendLine("                            placeholder=\"Confirme sua senha\"");
            js.AppendLine("                            required");
            js.AppendLine("                        />");
            js.AppendLine("                    </div>");
            js.AppendLine("");
            js.AppendLine("                    <button type=\"submit\" className=\"login-page-submit-button\">Confirmar</button>");
            js.AppendLine("                </div>");
            js.AppendLine("            </form>");
            js.AppendLine("        </div>");
            js.AppendLine("");
            js.AppendLine("    );");
            js.AppendLine("};");
            js.AppendLine("");
            js.AppendLine("export default ConfirmUserPage;");

            File.WriteAllText(path + "//ConfirmUserPage.js", js.ToString());
        }

        private static void CreateDashBoardPage(string path)
        {
            StringBuilder css = new StringBuilder();
            css.AppendLine(".dashboard-summary {");
            css.AppendLine("    display: grid;");
            css.AppendLine("    grid-template-columns: repeat(auto-fit, minmax(250px, 1fr));");
            css.AppendLine("    gap: var(--double-default-spacing);");
            css.AppendLine("    margin-bottom: var(--double-default-spacing);");
            css.AppendLine("}");
            css.AppendLine("");
            css.AppendLine(".summary-card {");
            css.AppendLine("    background-color: var(--color-surface);");
            css.AppendLine("    padding: var(--double-default-spacing);");
            css.AppendLine("    border-radius: var(--default-border-radius);");
            css.AppendLine("    box-shadow: 0 2px 5px var(--shadow-color-primary);");
            css.AppendLine("    text-align: center;");
            css.AppendLine("    font-weight: bold;");
            css.AppendLine("    color: var(--text-color-primary);");
            css.AppendLine("}");
            css.AppendLine("");
            css.AppendLine(".dashboard-chart {");
            css.AppendLine("    margin-top: var(--double-default-spacing);");
            css.AppendLine("    padding: var(--double-default-spacing);");
            css.AppendLine("    background-color: var(--color-surface);");
            css.AppendLine("    border-radius: var(--default-border-radius);");
            css.AppendLine("    box-shadow: 0 2px 5px var(--shadow-color-primary);");
            css.AppendLine("}");
            css.AppendLine("");
            css.AppendLine(".dashboard-chart h2 {");
            css.AppendLine("    text-align: center;");
            css.AppendLine("    margin-bottom: var(--double-default-spacing);");
            css.AppendLine("    color: var(--text-color-primary);");
            css.AppendLine("}");
            css.AppendLine("");
            css.AppendLine("/* Ensure filters align properly */");
            css.AppendLine(".container-admin-page-filters {");
            css.AppendLine("    display: flex;");
            css.AppendLine("    flex-direction: column;");
            css.AppendLine("    gap: var(--double-default-spacing);");
            css.AppendLine("    flex-wrap: wrap;");
            css.AppendLine("}");
            css.AppendLine("");
            css.AppendLine(".container-admin-page-filters label {");
            css.AppendLine("    font-weight: bold;");
            css.AppendLine("    color: var(--text-color-primary);");
            css.AppendLine("}");
            css.AppendLine("");
            css.AppendLine(".dashboard-filter {");
            css.AppendLine("    display: flex;");
            css.AppendLine("    align-items: center;");
            css.AppendLine("    justify-content: center;");
            css.AppendLine("    gap: var(--double-default-spacing);");
            css.AppendLine("    padding: var(--double-default-spacing);");
            css.AppendLine("    background-color: var(--color-surface);");
            css.AppendLine("    border-radius: var(--default-border-radius);");
            css.AppendLine("    box-shadow: 0 2px 5px var(--shadow-color-primary);");
            css.AppendLine("    margin-bottom: var(--double-default-spacing);");
            css.AppendLine("}");
            css.AppendLine("");
            css.AppendLine(".dashboard-filter label {");
            css.AppendLine("    font-weight: bold;");
            css.AppendLine("    color: var(--text-color-primary);");
            css.AppendLine("}");
            css.AppendLine("");
            css.AppendLine(".dashboard-filter input {");
            css.AppendLine("    padding: var(--default-spacing);");
            css.AppendLine("    border: 1px solid var(--border-color);");
            css.AppendLine("    border-radius: var(--default-border-radius);");
            css.AppendLine("    background-color: var(--color-surface);");
            css.AppendLine("    color: var(--text-color-primary);");
            css.AppendLine("    box-shadow: 0 2px 5px var(--shadow-color-primary);");
            css.AppendLine("}");
            css.AppendLine("");
            css.AppendLine(".dashboard-chart>canvas {");
            css.AppendLine("    width: 100% !important;");
            css.AppendLine("}");
            css.AppendLine("");
            css.AppendLine("/* Responsive Adjustments */");
            css.AppendLine("@media (max-width: 768px) {");
            css.AppendLine("    .dashboard-summary {");
            css.AppendLine("        grid-template-columns: repeat(auto-fit, minmax(150px, 1fr));");
            css.AppendLine("    }");
            css.AppendLine("");
            css.AppendLine("    .summary-card {");
            css.AppendLine("        font-size: 0.9rem;");
            css.AppendLine("        padding: var(--default-spacing);");
            css.AppendLine("    }");
            css.AppendLine("");
            css.AppendLine("    .container-admin-page-filters {");
            css.AppendLine("        flex-direction: column;");
            css.AppendLine("        align-items: flex-start;");
            css.AppendLine("    }");
            css.AppendLine("");
            css.AppendLine("    .dashboard-chart {");
            css.AppendLine("        padding: var(--default-spacing);");
            css.AppendLine("    }");
            css.AppendLine("");
            css.AppendLine("    .dashboard-filter {");
            css.AppendLine("        flex-direction: column;");
            css.AppendLine("        align-items: flex-start;");
            css.AppendLine("    }");
            css.AppendLine("");
            css.AppendLine("    .dashboard-filter input {");
            css.AppendLine("        width: 100%;");
            css.AppendLine("    }");
            css.AppendLine("}");
            css.AppendLine("");


            StringBuilder js = new StringBuilder();
            js.AppendLine("import \"./DashboardPage.css\";");
            js.AppendLine("");
            js.AppendLine("const DashboardPage = () => {");
            js.AppendLine("");
            js.AppendLine("");
            js.AppendLine("    return (");
            js.AppendLine("        <div className=\"container-admin-page\">");
            js.AppendLine("            <div className='title-with-options'>");
            js.AppendLine("                <h1>Dashboard</h1>");
            js.AppendLine("            </div>");
            js.AppendLine("        </div>");
            js.AppendLine("    );");
            js.AppendLine("};");
            js.AppendLine("");
            js.AppendLine("export default DashboardPage;");


            File.WriteAllText(path + "//DashboardPage.js", js.ToString());
            File.WriteAllText(path + "//DashboardPage.css", css.ToString());
        }
    }
}
