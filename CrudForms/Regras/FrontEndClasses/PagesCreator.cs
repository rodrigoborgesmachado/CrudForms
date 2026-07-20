using Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
                CreateLoginPage(createLoginPage, projectName);

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

                    string nameForm = NamesHandler.CreateComponentFormName(tabela.DAO.Nome);
                    path = Path.Combine(pagesAdmin, componentName, nameForm);
                    Directory.CreateDirectory(path);
                    CreateForm(tabela, path);
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
            string tableLabel = EscapeJsString(tabela.Apelido);
            StringBuilder js = new StringBuilder();
            js.AppendLine("import React, { useState, useEffect } from 'react';");
            js.AppendLine($"import {NamesHandler.GetApiName(tabela.DAO.Nome)} from '../../../../services/apiServices/{NamesHandler.GetApiName(tabela.DAO.Nome)}';");
            js.AppendLine("import { setLoading } from '../../../../services/redux/loadingSlice';");
            js.AppendLine("import { useDispatch } from 'react-redux';");
            js.AppendLine("import configService from '../../../../services/configService';");
            js.AppendLine("import Pagination from '../../../../components/common/Pagination/Pagination';");
            js.AppendLine("import { toast } from 'react-toastify';");
            js.AppendLine("import { putDateOnPattern } from '../../../../utils/functions';");
            js.AppendLine("import FilterComponent from '../../../../components/admin/FilterComponent/FilterComponent';");
            js.AppendLine("import { useNavigate } from 'react-router-dom';");
            js.AppendLine("");
            js.AppendLine($"const {name} = () => {{");
            js.AppendLine("    const navigate = useNavigate();");
            js.AppendLine("    const dispatch = useDispatch();");
            js.AppendLine("    const [items, setItems] = useState([]);");
            js.AppendLine("    const [page, setPage] = useState(1);");
            js.AppendLine("    const [searchTerm, setSearchTerm] = useState('');");
            js.AppendLine("    const [startDate, setStartDate] = useState('');");
            js.AppendLine("    const [endDate, setEndDate] = useState('');");
            js.AppendLine("    const [isActive, setIsActive] = useState('');");
            js.AppendLine("    const [totalPages, setTotalPages] = useState(1);");
            js.AppendLine("    const [totalItens, setTotalItens] = useState(0);");
            js.AppendLine("    const [refresh, setRefresh] = useState(0);");
            js.AppendLine("    const quantity = configService.getDefaultNumberOfItemsTable();");
            js.AppendLine("    const orderBy = 'Id:Desc';");
            js.AppendLine("");
            js.AppendLine("    useEffect(() => {");
            js.AppendLine("        const fetchItems = async () => {");
            js.AppendLine("            dispatch(setLoading(true));");
            js.AppendLine("            try {");
            js.AppendLine($"                const response = await {NamesHandler.GetApiName(tabela.DAO.Nome)}.getPaginated({{ page, quantity, orderBy, term: searchTerm, startDate, endDate, isActive: isActive || undefined, include: '' }});");
            js.AppendLine("                setItems(response.Results);");
            js.AppendLine("                setTotalPages(response.TotalPages);");
            js.AppendLine("                setTotalItens(response.TotalCount);");
            js.AppendLine("            } catch {");
            js.AppendLine("                toast.error('Erro ao buscar os itens.');");
            js.AppendLine("            } finally {");
            js.AppendLine("                dispatch(setLoading(false));");
            js.AppendLine("            }");
            js.AppendLine("        };");
            js.AppendLine("        fetchItems();");
            js.AppendLine("    }, [page, quantity, searchTerm, startDate, endDate, isActive, dispatch, refresh]);");
            js.AppendLine("");
            js.AppendLine("    const handlePageChange = (newPage) => {");
            js.AppendLine("        if (newPage > 0 && newPage <= totalPages) {");
            js.AppendLine("            setPage(newPage);");
            js.AppendLine("        }");
            js.AppendLine("    };");
            js.AppendLine("");
            js.AppendLine("    const search = ({ term, startDate, endDate, isActive } = {}) => {");
            js.AppendLine("        setSearchTerm(term);");
            js.AppendLine("        setStartDate(startDate);");
            js.AppendLine("        setEndDate(endDate);");
            js.AppendLine("        setIsActive(isActive || '');");
            js.AppendLine("        setPage(1);");
            js.AppendLine("    };");
            js.AppendLine("");
            js.AppendLine("    const exportFunction = async ({ term, isActive }) => {");
            js.AppendLine("        try {");
            js.AppendLine("            dispatch(setLoading(true));");
            js.AppendLine($"            const response = await {NamesHandler.GetApiName(tabela.DAO.Nome)}.export({{ term, startDate, endDate, isActive: isActive || undefined }});");
            js.AppendLine("            if (response.Status === 200 && response.Object) {");
            js.AppendLine("                window.open(response.Object, '_blank');");
            js.AppendLine("                toast.success('Relatório gerado com sucesso!');");
            js.AppendLine("            } else {");
            js.AppendLine("                toast.error('Erro ao gerar o relatório');");
            js.AppendLine("            }");
            js.AppendLine("        } catch {");
            js.AppendLine("            toast.error('Erro ao gerar o relatório');");
            js.AppendLine("        } finally {");
            js.AppendLine("            dispatch(setLoading(false));");
            js.AppendLine("        }");
            js.AppendLine("    };");
            js.AppendLine("");
            js.AppendLine("    const openItem = (code) => navigate(`${code}`);");
            js.AppendLine("    const editItem = (code) => navigate(`${code}/editar`);");
            js.AppendLine("    const createItem = () => navigate('novo');");
            js.AppendLine("");
            js.AppendLine("    const changeStatus = async (currentIsActive, code) => {");
            js.AppendLine("        try {");
            js.AppendLine("            dispatch(setLoading(true));");
            js.AppendLine("            const nextIsActive = !Boolean(currentIsActive);");
            js.AppendLine($"            const response = await {NamesHandler.GetApiName(tabela.DAO.Nome)}.changeStatus(code, {{ isActive: nextIsActive, isDeleted: false }});");
            js.AppendLine("            if (response) {");
            js.AppendLine("                toast.success('Atualizado com sucesso!');");
            js.AppendLine("                setRefresh(prev => prev + 1);");
            js.AppendLine("            } else {");
            js.AppendLine("                toast.error('Erro ao atualizar o item!');");
            js.AppendLine("            }");
            js.AppendLine("        } catch {");
            js.AppendLine("            toast.error('Erro ao atualizar o item!');");
            js.AppendLine("        } finally {");
            js.AppendLine("            dispatch(setLoading(false));");
            js.AppendLine("        }");
            js.AppendLine("    };");
            js.AppendLine("");
            js.AppendLine("    return (");
            js.AppendLine("        <div className=\"container-fluid py-4\">");
            js.AppendLine("            <div className=\"d-flex flex-wrap justify-content-between align-items-center gap-3 mb-4\">");
            js.AppendLine("                <div>");
            js.AppendLine($"                    <h1 className=\"h3 mb-1\">{tableLabel}</h1>");
            js.AppendLine($"                    <p className=\"text-body-secondary mb-0\">Gerencie os registros de {tableLabel}.</p>");
            js.AppendLine("                </div>");
            js.AppendLine("                <button type=\"button\" className=\"btn btn-primary\" onClick={createItem}>");
            js.AppendLine("                    <i className=\"bi bi-plus-lg me-2\" aria-hidden=\"true\"></i>");
            js.AppendLine("                    Novo");
            js.AppendLine("                </button>");
            js.AppendLine("            </div>");
            js.AppendLine("");
            js.AppendLine("            <div className=\"card border-0 shadow-sm\">");
            js.AppendLine("                <div className=\"card-body\">");
            js.AppendLine($"                    <FilterComponent placeHolder=\"{tableLabel}\" showTermFilter={{true}} showStartDate={{true}} showEndDate={{true}} showIsActiveFilter={{true}} submitFilter={{search}} exportFunction={{exportFunction}} />");
            js.AppendLine("                    <div className=\"table-responsive\">");
            js.AppendLine("                        <table className=\"table table-hover align-middle mb-0\">");
            js.AppendLine("                            <thead>");
            js.AppendLine("                                <tr>");
            foreach (var item in tabela.CamposFrontEnd().Where(c => c.VisivelListagem))
            {
                js.AppendLine($"                                    <th scope=\"col\">{EscapeJsString(item.Apelido)}</th>");
            }
            js.AppendLine("                                    <th scope=\"col\">Status</th>");
            js.AppendLine("                                    <th scope=\"col\" className=\"table-actions text-end\">Ações</th>");
            js.AppendLine("                                </tr>");
            js.AppendLine("                            </thead>");
            js.AppendLine("                            <tbody>");
            js.AppendLine("                                {items.map((item) => (");
            js.AppendLine("                                    <tr key={item.Code ?? item.Id}>");
            foreach (var item in tabela.CamposFrontEnd().Where(c => c.VisivelListagem))
            {
                string fieldName = NamesHandler.CreateComponentName(item.DAO.Nome);
                string fieldLabel = EscapeJsString(item.Apelido);
                if (IsDateField(item.DAO.TipoCampo.Nome))
                {
                    js.AppendLine($"                                        <td data-label=\"{fieldLabel}\">{{putDateOnPattern(item.{fieldName})}}</td>");
                }
                else
                {
                    js.AppendLine($"                                        <td data-label=\"{fieldLabel}\">{{item.{fieldName}}}</td>");
                }
            }
            js.AppendLine("                                        <td data-label=\"Status\">");
            js.AppendLine("                                            <span className={`badge ${item.IsActive ? 'text-bg-success' : 'text-bg-secondary'}`}>");
            js.AppendLine("                                                {item.IsActive ? 'Ativo' : 'Inativo'}");
            js.AppendLine("                                            </span>");
            js.AppendLine("                                        </td>");
            js.AppendLine("                                        <td className=\"table-actions text-end\">");
            js.AppendLine("                                            <div className=\"btn-group btn-group-sm\" role=\"group\" aria-label=\"Ações do registro\">");
            js.AppendLine("                                                <button type=\"button\" className=\"btn btn-outline-secondary\" title=\"Visualizar\" aria-label=\"Visualizar\" onClick={() => openItem(`${item.Code}`)}>");
            js.AppendLine("                                                    <i className=\"bi bi-eye\" aria-hidden=\"true\"></i>");
            js.AppendLine("                                                </button>");
            js.AppendLine("                                                <button type=\"button\" className=\"btn btn-outline-primary\" title=\"Editar\" aria-label=\"Editar\" onClick={() => editItem(`${item.Code}`)}>");
            js.AppendLine("                                                    <i className=\"bi bi-pencil\" aria-hidden=\"true\"></i>");
            js.AppendLine("                                                </button>");
            js.AppendLine("                                                <button type=\"button\" className={item.IsActive ? 'btn btn-outline-danger' : 'btn btn-outline-success'} title={item.IsActive ? 'Desativar' : 'Ativar'} aria-label={item.IsActive ? 'Desativar' : 'Ativar'} onClick={() => changeStatus(item.IsActive, item.Code)}>");
            js.AppendLine("                                                    <i className={item.IsActive ? 'bi bi-pause-circle' : 'bi bi-check-circle'} aria-hidden=\"true\"></i>");
            js.AppendLine("                                                </button>");
            js.AppendLine("                                            </div>");
            js.AppendLine("                                        </td>");
            js.AppendLine("                                    </tr>");
            js.AppendLine("                                ))}");
            js.AppendLine("                            </tbody>");
            js.AppendLine("                        </table>");
            js.AppendLine("                    </div>");
            js.AppendLine("                </div>");
            js.AppendLine("                <div className=\"card-footer bg-body border-0 d-flex flex-wrap justify-content-between align-items-center gap-3\">");
            js.AppendLine("                    <small className=\"text-body-secondary\">Total de itens: {totalItens}</small>");
            js.AppendLine("                    <Pagination page={page} totalPages={totalPages} onPageChange={handlePageChange} />");
            js.AppendLine("                </div>");
            js.AppendLine("            </div>");
            js.AppendLine("        </div>");
            js.AppendLine("    );");
            js.AppendLine("};");
            js.AppendLine("");
            js.AppendLine($"export default {name};");

            File.WriteAllText(path + $"//{name}.jsx", js.ToString());
        }

        private static void CreatePage(MD_Tabela tabela, string path)
        {
            string name = NamesHandler.CreateComponentPageName(tabela.DAO.Nome);
            string tableLabel = EscapeJsString(tabela.Apelido);
            StringBuilder js = new StringBuilder();
            js.AppendLine("import React, { useState, useEffect } from 'react';");
            js.AppendLine($"import {NamesHandler.GetApiName(tabela.DAO.Nome)} from '../../../../services/apiServices/{NamesHandler.GetApiName(tabela.DAO.Nome)}';");
            js.AppendLine("import { setLoading } from '../../../../services/redux/loadingSlice';");
            js.AppendLine("import { useDispatch } from 'react-redux';");
            js.AppendLine("import { useNavigate, useParams } from 'react-router-dom';");
            js.AppendLine("import { toast } from 'react-toastify';");
            js.AppendLine("import { putDateOnPattern } from '../../../../utils/functions';");
            js.AppendLine("");
            js.AppendLine($"const {name} = () => {{");
            js.AppendLine("    const navigate = useNavigate();");
            js.AppendLine("    const dispatch = useDispatch();");
            js.AppendLine("    const { code } = useParams();");
            js.AppendLine("    const [item, setItem] = useState({});");
            js.AppendLine("");
            js.AppendLine("    useEffect(() => {");
            js.AppendLine("        const fetchItem = async () => {");
            js.AppendLine("            dispatch(setLoading(true));");
            js.AppendLine("            try {");
            js.AppendLine($"                const response = await {NamesHandler.GetApiName(tabela.DAO.Nome)}.getByCode(code, {{ include: '' }});");
            js.AppendLine("                setItem(response);");
            js.AppendLine("            } catch {");
            js.AppendLine("                toast.error('Erro ao buscar.');");
            js.AppendLine("            } finally {");
            js.AppendLine("                dispatch(setLoading(false));");
            js.AppendLine("            }");
            js.AppendLine("        };");
            js.AppendLine("        fetchItem();");
            js.AppendLine("    }, [code, dispatch]);");
            js.AppendLine("");
            js.AppendLine("    return (");
            js.AppendLine("        <div className=\"container-fluid py-4\">");
            js.AppendLine("            <div className=\"card border-0 shadow-sm\">");
            js.AppendLine("                <div className=\"card-header bg-body d-flex flex-wrap justify-content-between align-items-center gap-3\">");
            js.AppendLine($"                    <h1 className=\"h4 mb-0\">Detalhes de {tableLabel}</h1>");
            js.AppendLine("                    <button type=\"button\" className=\"btn btn-outline-primary\" onClick={() => navigate('editar')}>");
            js.AppendLine("                        <i className=\"bi bi-pencil me-2\" aria-hidden=\"true\"></i>");
            js.AppendLine("                        Editar");
            js.AppendLine("                    </button>");
            js.AppendLine("                </div>");
            js.AppendLine("                <div className=\"card-body\">");
            js.AppendLine("                    <dl className=\"row mb-0\">");
            foreach (var item in tabela.CamposFrontEnd().Where(c => c.VisivelDetalhes))
            {
                string fieldName = NamesHandler.CreateComponentName(item.DAO.Nome);
                string fieldLabel = EscapeJsString(item.Apelido);
                js.AppendLine($"                        <dt className=\"col-sm-3 text-body-secondary\">{fieldLabel}</dt>");
                if (IsDateField(item.DAO.TipoCampo.Nome))
                {
                    js.AppendLine($"                        <dd className=\"col-sm-9\">{{putDateOnPattern(item.{fieldName}) || '-'}}</dd>");
                }
                else
                {
                    js.AppendLine($"                        <dd className=\"col-sm-9\">{{item.{fieldName} ?? '-'}}</dd>");
                }
            }
            js.AppendLine("                    </dl>");
            js.AppendLine("                </div>");
            js.AppendLine("            </div>");
            js.AppendLine("        </div>");
            js.AppendLine("    );");
            js.AppendLine("};");
            js.AppendLine("");
            js.AppendLine($"export default {name};");

            File.WriteAllText(path + $"//{name}.jsx", js.ToString());
        }

        private static void CreateForm(MD_Tabela tabela, string path)
        {
            string name = NamesHandler.CreateComponentFormName(tabela.DAO.Nome);
            string tableLabel = EscapeJsString(tabela.Apelido);
            string routeName = NamesHandler.CreateRouteName(tabela.Apelido);
            var editableFields = tabela.CamposFrontEnd()
                .Where(c => c.VisivelInclusaoEdicao && !IsSystemField(c.DAO.Nome) && !c.DAO.PrimaryKey)
                .ToList();
            var dateFieldNames = editableFields
                .Where(c => IsDateField(c.DAO.TipoCampo.Nome))
                .Select(c => NamesHandler.CreateComponentName(c.DAO.Nome))
                .ToList();

            StringBuilder js = new StringBuilder();
            js.AppendLine("import React, { useState, useEffect } from 'react';");
            js.AppendLine($"import {NamesHandler.GetApiName(tabela.DAO.Nome)} from '../../../../services/apiServices/{NamesHandler.GetApiName(tabela.DAO.Nome)}';");
            js.AppendLine("import { setLoading } from '../../../../services/redux/loadingSlice';");
            js.AppendLine("import { useDispatch } from 'react-redux';");
            js.AppendLine("import { useNavigate, useParams } from 'react-router-dom';");
            js.AppendLine("import { toast } from 'react-toastify';");
            js.AppendLine("import { normalizeFormValueToApi, toDateTimeLocalValue } from '../../../../utils/functions';");
            js.AppendLine("");
            js.AppendLine("const createEmptyForm = () => ({");
            foreach (var item in editableFields)
            {
                js.AppendLine($"    {NamesHandler.CreateComponentName(item.DAO.Nome)}: '',");
            }
            js.AppendLine("});");
            js.AppendLine("");
            js.AppendLine($"const dateFields = [{string.Join(", ", dateFieldNames.Select(field => $"'{field}'"))}];");
            js.AppendLine("");
            js.AppendLine($"const {name} = () => {{");
            js.AppendLine("    const dispatch = useDispatch();");
            js.AppendLine("    const navigate = useNavigate();");
            js.AppendLine("    const { code } = useParams();");
            js.AppendLine("    const isEditing = Boolean(code);");
            js.AppendLine("    const [formData, setFormData] = useState(createEmptyForm());");
            js.AppendLine("    const [initialData, setInitialData] = useState(createEmptyForm());");
            js.AppendLine("");
            js.AppendLine("    useEffect(() => {");
            js.AppendLine("        if (!isEditing) {");
            js.AppendLine("            return;");
            js.AppendLine("        }");
            js.AppendLine("");
            js.AppendLine("        const fetchItem = async () => {");
            js.AppendLine("            dispatch(setLoading(true));");
            js.AppendLine("            try {");
            js.AppendLine($"                const response = await {NamesHandler.GetApiName(tabela.DAO.Nome)}.getByCode(code, {{ include: '' }});");
            js.AppendLine("                const nextFormData = {");
            js.AppendLine("                    ...createEmptyForm(),");
            js.AppendLine("                    ...response,");
            foreach (var fieldName in dateFieldNames)
            {
                js.AppendLine($"                    {fieldName}: toDateTimeLocalValue(response.{fieldName}),");
            }
            js.AppendLine("                };");
            js.AppendLine("                setFormData(nextFormData);");
            js.AppendLine("                setInitialData(nextFormData);");
            js.AppendLine("            } catch {");
            js.AppendLine("                toast.error('Erro ao buscar o item.');");
            js.AppendLine("            } finally {");
            js.AppendLine("                dispatch(setLoading(false));");
            js.AppendLine("            }");
            js.AppendLine("        };");
            js.AppendLine("");
            js.AppendLine("        fetchItem();");
            js.AppendLine("    }, [code, dispatch, isEditing]);");
            js.AppendLine("");
            js.AppendLine("    const handleChange = (event) => {");
            js.AppendLine("        const { name, value, type, checked } = event.target;");
            js.AppendLine("        setFormData((previous) => ({");
            js.AppendLine("            ...previous,");
            js.AppendLine("            [name]: type === 'checkbox' ? checked : value,");
            js.AppendLine("        }));");
            js.AppendLine("    };");
            js.AppendLine("");
            js.AppendLine("    const getChangedFields = () => {");
            js.AppendLine("        return Object.keys(formData).reduce((changes, field) => {");
            js.AppendLine("            if (formData[field] !== initialData[field]) {");
            js.AppendLine("                changes[field] = normalizeFormValueToApi(field, formData[field], dateFields);");
            js.AppendLine("            }");
            js.AppendLine("");
            js.AppendLine("            return changes;");
            js.AppendLine("        }, {});");
            js.AppendLine("    };");
            js.AppendLine("");
            js.AppendLine("    const handleSubmit = async (event) => {");
            js.AppendLine("        event.preventDefault();");
            js.AppendLine("        dispatch(setLoading(true));");
            js.AppendLine("");
            js.AppendLine("        try {");
            js.AppendLine("            if (isEditing) {");
            js.AppendLine("                const changes = getChangedFields();");
            js.AppendLine("                if (Object.keys(changes).length === 0) {");
            js.AppendLine("                    toast.info('Nenhuma alteração para salvar.');");
            js.AppendLine("                    return;");
            js.AppendLine("                }");
            js.AppendLine($"                await {NamesHandler.GetApiName(tabela.DAO.Nome)}.update(code, changes);");
            js.AppendLine("                toast.success('Atualizado com sucesso!');");
            js.AppendLine("            } else {");
            js.AppendLine($"                await {NamesHandler.GetApiName(tabela.DAO.Nome)}.create(formData);");
            js.AppendLine("                toast.success('Criado com sucesso!');");
            js.AppendLine("            }");
            js.AppendLine("");
            js.AppendLine($"            navigate('/{routeName}');");
            js.AppendLine("        } catch {");
            js.AppendLine("            toast.error('Erro ao salvar o item.');");
            js.AppendLine("        } finally {");
            js.AppendLine("            dispatch(setLoading(false));");
            js.AppendLine("        }");
            js.AppendLine("    };");
            js.AppendLine("");
            js.AppendLine("    return (");
            js.AppendLine("        <div className=\"container-fluid py-4\">");
            js.AppendLine("            <div className=\"card border-0 shadow-sm\">");
            js.AppendLine("                <div className=\"card-header bg-body\">");
            js.AppendLine($"                    <h1 className=\"h4 mb-0\">{{isEditing ? 'Editar {tableLabel}' : 'Novo {tableLabel}'}}</h1>");
            js.AppendLine("                </div>");
            js.AppendLine("                <div className=\"card-body\">");
            js.AppendLine("                    <form onSubmit={handleSubmit}>");
            js.AppendLine("                        <div className=\"row g-3\">");
            foreach (var item in editableFields)
            {
                AppendFormField(js, item);
            }
            js.AppendLine("                        </div>");
            js.AppendLine("                        <div className=\"d-flex justify-content-end gap-2 mt-4\">");
            js.AppendLine("                            <button type=\"button\" className=\"btn btn-outline-secondary\" onClick={() => navigate(-1)}>");
            js.AppendLine("                                Cancelar");
            js.AppendLine("                            </button>");
            js.AppendLine("                            <button type=\"submit\" className=\"btn btn-primary\">");
            js.AppendLine("                                <i className=\"bi bi-check-lg me-2\" aria-hidden=\"true\"></i>");
            js.AppendLine("                                Salvar");
            js.AppendLine("                            </button>");
            js.AppendLine("                        </div>");
            js.AppendLine("                    </form>");
            js.AppendLine("                </div>");
            js.AppendLine("            </div>");
            js.AppendLine("        </div>");
            js.AppendLine("    );");
            js.AppendLine("};");
            js.AppendLine("");
            js.AppendLine($"export default {name};");

            File.WriteAllText(path + $"//{name}.jsx", js.ToString());
        }

        private static void CreateRecoverPasswordPage(string path, string projectName)
        {
            StringBuilder js = new StringBuilder();
            js.AppendLine("import React, { useState } from 'react';");
            js.AppendLine("import { useDispatch } from 'react-redux';");
            js.AppendLine("import { setLoading } from '../../../services/redux/loadingSlice';");
            js.AppendLine("import tokenApi from '../../../services/apiServices/token';");
            js.AppendLine("import MessageModal from '../../../components/common/Modals/MessageModal/MessageModal';");
            js.AppendLine("import { useNavigate } from 'react-router-dom';");
            js.AppendLine("import { toast } from 'react-toastify';");
            js.AppendLine("import '../LoginPage/LoginPage.css';");
            js.AppendLine("");
            js.AppendLine("const RecoverPasswordPage = () => {");
            js.AppendLine("    const [email, setEmail] = useState('');");
            js.AppendLine("    const [message, setMessage] = useState('');");
            js.AppendLine("    const [isMessageOpen, setIsMessageOpen] = useState(false);");
            js.AppendLine("    const dispatch = useDispatch();");
            js.AppendLine("    const navigate = useNavigate();");
            js.AppendLine("");
            js.AppendLine("    const handleRecover = async (event) => {");
            js.AppendLine("        event.preventDefault();");
            js.AppendLine("        dispatch(setLoading(true));");
            js.AppendLine("        try {");
            js.AppendLine("            await tokenApi.recoverPass(email);");
            js.AppendLine("            setMessage('Se o email estiver cadastrado, enviaremos instruções para recuperação.');");
            js.AppendLine("            setIsMessageOpen(true);");
            js.AppendLine("        } catch {");
            js.AppendLine("            toast.error('Erro ao solicitar recuperação de senha.');");
            js.AppendLine("        } finally {");
            js.AppendLine("            dispatch(setLoading(false));");
            js.AppendLine("        }");
            js.AppendLine("    };");
            js.AppendLine("");
            js.AppendLine("    return (");
            js.AppendLine("        <main className=\"login-page\">");
            js.AppendLine("            <MessageModal isOpen={isMessageOpen} click={() => navigate('/')} message={message} />");
            js.AppendLine("            <div className=\"card border-0 shadow-lg login-card\">");
            js.AppendLine("                <div className=\"card-body p-4 p-md-5\">");
            js.AppendLine($"                    <h1 className=\"h3 mb-2\">{EscapeJsString(projectName)}</h1>");
            js.AppendLine("                    <p className=\"text-body-secondary mb-4\">Recuperação de senha</p>");
            js.AppendLine("                    <form onSubmit={handleRecover}>");
            js.AppendLine("                        <div className=\"mb-3\">");
            js.AppendLine("                            <label htmlFor=\"email\" className=\"form-label\">Email</label>");
            js.AppendLine("                            <input id=\"email\" type=\"email\" className=\"form-control\" value={email} onChange={(event) => setEmail(event.target.value)} placeholder=\"Digite seu email cadastrado\" required />");
            js.AppendLine("                        </div>");
            js.AppendLine("                        <div className=\"d-grid gap-2\">");
            js.AppendLine("                            <button type=\"submit\" className=\"btn btn-primary\">Recuperar senha</button>");
            js.AppendLine("                            <button type=\"button\" className=\"btn btn-outline-secondary\" onClick={() => navigate('/')}>Voltar</button>");
            js.AppendLine("                        </div>");
            js.AppendLine("                    </form>");
            js.AppendLine("                </div>");
            js.AppendLine("            </div>");
            js.AppendLine("        </main>");
            js.AppendLine("    );");
            js.AppendLine("};");
            js.AppendLine("");
            js.AppendLine("export default RecoverPasswordPage;");

            File.WriteAllText(path + "//RecoverPasswordPage.jsx", js.ToString());
        }

        private static void CreateLoginPage(string path, string projectName)
        {
            StringBuilder css = new StringBuilder();
            css.AppendLine(".login-page {");
            css.AppendLine("    min-height: 100vh;");
            css.AppendLine("    display: flex;");
            css.AppendLine("    align-items: center;");
            css.AppendLine("    justify-content: center;");
            css.AppendLine("    padding: 1.5rem;");
            css.AppendLine("    background: linear-gradient(135deg, var(--app-primary-hover), var(--app-primary));");
            css.AppendLine("}");
            css.AppendLine("");
            css.AppendLine(".login-card {");
            css.AppendLine("    width: 100%;");
            css.AppendLine("    max-width: 480px;");
            css.AppendLine("    border-radius: var(--app-border-radius);");
            css.AppendLine("}");

            StringBuilder js = new StringBuilder();
            js.AppendLine("import React, { useState } from 'react';");
            js.AppendLine("import { useDispatch } from 'react-redux';");
            js.AppendLine("import { login } from '../../../services/redux/authSlice';");
            js.AppendLine("import tokenApi from '../../../services/apiServices/token';");
            js.AppendLine("import './LoginPage.css';");
            js.AppendLine("import { setLoading } from '../../../services/redux/loadingSlice';");
            js.AppendLine("import MessageModal from '../../../components/common/Modals/MessageModal/MessageModal';");
            js.AppendLine("");
            js.AppendLine("const LoginPage = () => {");
            js.AppendLine("    const [identifier, setIdentifier] = useState('');");
            js.AppendLine("    const [password, setPassword] = useState('');");
            js.AppendLine("    const [error, setError] = useState('');");
            js.AppendLine("    const [isMessageOpen, setIsMessageOpen] = useState(false);");
            js.AppendLine("    const [message, setMessage] = useState('');");
            js.AppendLine("    const dispatch = useDispatch();");
            js.AppendLine("");
            js.AppendLine("    const handleSubmit = async (event) => {");
            js.AppendLine("        event.preventDefault();");
            js.AppendLine("        setError('');");
            js.AppendLine("        try {");
            js.AppendLine("            dispatch(setLoading(true));");
            js.AppendLine("            const response = await tokenApi.getToken({ userName: identifier, password });");
            js.AppendLine("            if (response.code) {");
            js.AppendLine("                setMessage(response.message);");
            js.AppendLine("                setIsMessageOpen(true);");
            js.AppendLine("                return;");
            js.AppendLine("            }");
            js.AppendLine("            dispatch(login({");
            js.AppendLine("                access_token: response.access_token,");
            js.AppendLine("                nameListPage: response.nameListPage,");
            js.AppendLine("                code: response.id,");
            js.AppendLine("            }));");
            js.AppendLine("        } catch (err) {");
            js.AppendLine("            setError('Falha na autenticação. Verifique seus dados e tente novamente.');");
            js.AppendLine("            console.error('Login failed:', err);");
            js.AppendLine("        } finally {");
            js.AppendLine("            dispatch(setLoading(false));");
            js.AppendLine("        }");
            js.AppendLine("    };");
            js.AppendLine("");
            js.AppendLine("    return (");
            js.AppendLine("        <main className=\"login-page\">");
            js.AppendLine("            <MessageModal isOpen={isMessageOpen} click={() => setIsMessageOpen(false)} message={message} />");
            js.AppendLine("            <div className=\"card border-0 shadow-lg login-card\">");
            js.AppendLine("                <div className=\"card-body p-4 p-md-5\">");
            js.AppendLine($"                    <h1 className=\"h3 mb-2\">{EscapeJsString(projectName)}</h1>");
            js.AppendLine("                    <p className=\"text-body-secondary mb-4\">Acesse sua conta</p>");
            js.AppendLine("                    <form onSubmit={handleSubmit}>");
            js.AppendLine("                        <div className=\"mb-3\">");
            js.AppendLine("                            <label htmlFor=\"identifier\" className=\"form-label\">Email</label>");
            js.AppendLine("                            <input id=\"identifier\" type=\"email\" className=\"form-control\" value={identifier} onChange={(event) => setIdentifier(event.target.value)} placeholder=\"Digite seu email\" required />");
            js.AppendLine("                        </div>");
            js.AppendLine("                        <div className=\"mb-3\">");
            js.AppendLine("                            <label htmlFor=\"password\" className=\"form-label\">Senha</label>");
            js.AppendLine("                            <input id=\"password\" type=\"password\" className=\"form-control\" value={password} onChange={(event) => setPassword(event.target.value)} placeholder=\"Digite sua senha\" required />");
            js.AppendLine("                        </div>");
            js.AppendLine("                        {error && <div className=\"alert alert-danger py-2\" role=\"alert\">{error}</div>}");
            js.AppendLine("                        <div className=\"d-flex justify-content-end mb-3\">");
            js.AppendLine("                            <a href=\"/recuperar-senha\" className=\"link-secondary small\">Esqueceu a senha?</a>");
            js.AppendLine("                        </div>");
            js.AppendLine("                        <button type=\"submit\" className=\"btn btn-primary w-100\">Acessar</button>");
            js.AppendLine("                    </form>");
            js.AppendLine("                </div>");
            js.AppendLine("            </div>");
            js.AppendLine("        </main>");
            js.AppendLine("    );");
            js.AppendLine("};");
            js.AppendLine("");
            js.AppendLine("export default LoginPage;");

            File.WriteAllText(path + "//LoginPage.jsx", js.ToString());
            File.WriteAllText(path + "//LoginPage.css", css.ToString());
        }

        private static void CreateConfirmUserPage(string path)
        {
            StringBuilder js = new StringBuilder();
            js.AppendLine("import React, { useEffect, useState } from 'react';");
            js.AppendLine("import { useNavigate, useSearchParams } from 'react-router-dom';");
            js.AppendLine("import { useDispatch } from 'react-redux';");
            js.AppendLine("import { setLoading } from '../../../services/redux/loadingSlice';");
            js.AppendLine("import adminApi from '../../../services/apiServices/adminApi';");
            js.AppendLine("import { toast } from 'react-toastify';");
            js.AppendLine("import MessageModal from '../../../components/common/Modals/MessageModal/MessageModal';");
            js.AppendLine("import '../LoginPage/LoginPage.css';");
            js.AppendLine("");
            js.AppendLine("const ConfirmUserPage = ({ isRecover }) => {");
            js.AppendLine("    const dispatch = useDispatch();");
            js.AppendLine("    const navigate = useNavigate();");
            js.AppendLine("    const [searchParams] = useSearchParams();");
            js.AppendLine("    const [password, setPassword] = useState('');");
            js.AppendLine("    const [verifyPassword, setVerifyPassword] = useState('');");
            js.AppendLine("    const [token, setToken] = useState('');");
            js.AppendLine("    const [isMessageOpen, setIsMessageOpen] = useState(false);");
            js.AppendLine("    const [message, setMessage] = useState('');");
            js.AppendLine("");
            js.AppendLine("    useEffect(() => {");
            js.AppendLine("        const guidToken = searchParams.get('token');");
            js.AppendLine("        if (guidToken) {");
            js.AppendLine("            setToken(guidToken);");
            js.AppendLine("        } else {");
            js.AppendLine("            setMessage('Token inválido ou expirado.');");
            js.AppendLine("            setIsMessageOpen(true);");
            js.AppendLine("        }");
            js.AppendLine("    }, [searchParams]);");
            js.AppendLine("");
            js.AppendLine("    const handleSubmit = async (event) => {");
            js.AppendLine("        event.preventDefault();");
            js.AppendLine("        if (password !== verifyPassword) {");
            js.AppendLine("            toast.error('As senhas não coincidem.');");
            js.AppendLine("            return;");
            js.AppendLine("        }");
            js.AppendLine("        dispatch(setLoading(true));");
            js.AppendLine("        try {");
            js.AppendLine("            const response = await adminApi.confirmUser({ password, verifypassword: verifyPassword, guid: token });");
            js.AppendLine("            if (!response) {");
            js.AppendLine("                setMessage('O token expirou. Solicite um novo email de confirmação.');");
            js.AppendLine("                setIsMessageOpen(true);");
            js.AppendLine("            } else {");
            js.AppendLine("                toast.success(isRecover ? 'Senha atualizada com sucesso!' : 'Cadastro confirmado com sucesso!');");
            js.AppendLine("                navigate('/');");
            js.AppendLine("            }");
            js.AppendLine("        } catch {");
            js.AppendLine("            toast.error('Erro ao confirmar o usuário.');");
            js.AppendLine("        } finally {");
            js.AppendLine("            dispatch(setLoading(false));");
            js.AppendLine("        }");
            js.AppendLine("    };");
            js.AppendLine("");
            js.AppendLine("    return (");
            js.AppendLine("        <main className=\"login-page\">");
            js.AppendLine("            <MessageModal isOpen={isMessageOpen} click={() => navigate('/')} message={message} />");
            js.AppendLine("            <div className=\"card border-0 shadow-lg login-card\">");
            js.AppendLine("                <div className=\"card-body p-4 p-md-5\">");
            js.AppendLine("                    <h1 className=\"h3 mb-2\">{isRecover ? 'Recuperação de senha' : 'Bem-vindo!'}</h1>");
            js.AppendLine("                    <p className=\"text-body-secondary mb-4\">{isRecover ? 'Defina uma nova senha para acessar sua conta.' : 'Defina uma senha para ativar sua conta.'}</p>");
            js.AppendLine("                    <form onSubmit={handleSubmit}>");
            js.AppendLine("                        <div className=\"mb-3\">");
            js.AppendLine("                            <label htmlFor=\"password\" className=\"form-label\">Senha</label>");
            js.AppendLine("                            <input id=\"password\" type=\"password\" className=\"form-control\" value={password} onChange={(event) => setPassword(event.target.value)} placeholder=\"Digite sua senha\" required />");
            js.AppendLine("                        </div>");
            js.AppendLine("                        <div className=\"mb-4\">");
            js.AppendLine("                            <label htmlFor=\"verifyPassword\" className=\"form-label\">Confirmar senha</label>");
            js.AppendLine("                            <input id=\"verifyPassword\" type=\"password\" className=\"form-control\" value={verifyPassword} onChange={(event) => setVerifyPassword(event.target.value)} placeholder=\"Confirme sua senha\" required />");
            js.AppendLine("                        </div>");
            js.AppendLine("                        <button type=\"submit\" className=\"btn btn-primary w-100\">Confirmar</button>");
            js.AppendLine("                    </form>");
            js.AppendLine("                </div>");
            js.AppendLine("            </div>");
            js.AppendLine("        </main>");
            js.AppendLine("    );");
            js.AppendLine("};");
            js.AppendLine("");
            js.AppendLine("export default ConfirmUserPage;");

            File.WriteAllText(path + "//ConfirmUserPage.jsx", js.ToString());
        }

        private static void CreateDashBoardPage(string path)
        {
            StringBuilder js = new StringBuilder();
            js.AppendLine("const DashboardPage = () => {");
            js.AppendLine("    return (");
            js.AppendLine("        <div className=\"container-fluid py-4\">");
            js.AppendLine("            <div className=\"d-flex flex-wrap justify-content-between align-items-center gap-3 mb-4\">");
            js.AppendLine("                <div>");
            js.AppendLine("                    <h1 className=\"h3 mb-1\">Dashboard</h1>");
            js.AppendLine("                    <p className=\"text-body-secondary mb-0\">Resumo administrativo do sistema.</p>");
            js.AppendLine("                </div>");
            js.AppendLine("            </div>");
            js.AppendLine("            <div className=\"card border-0 shadow-sm\">");
            js.AppendLine("                <div className=\"card-body\">");
            js.AppendLine("                    <p className=\"mb-0 text-body-secondary\">Selecione uma opção no menu lateral para começar.</p>");
            js.AppendLine("                </div>");
            js.AppendLine("            </div>");
            js.AppendLine("        </div>");
            js.AppendLine("    );");
            js.AppendLine("};");
            js.AppendLine("");
            js.AppendLine("export default DashboardPage;");

            File.WriteAllText(path + "//DashboardPage.jsx", js.ToString());
        }

        private static void AppendFormField(StringBuilder js, MD_Campos item)
        {
            string fieldName = NamesHandler.CreateComponentName(item.DAO.Nome);
            string fieldLabel = EscapeJsString(item.Apelido);
            string inputType = GetInputType(item.DAO.TipoCampo.Nome);
            string required = item.DAO.NotNull ? " required" : string.Empty;
            string columnClass = GetBootstrapColumnClass(item);

            if (inputType.Equals("checkbox"))
            {
                js.AppendLine($"                            <div className=\"{columnClass} d-flex align-items-end\">");
                js.AppendLine("                                <div className=\"form-check mb-2\">");
                js.AppendLine($"                                    <input id=\"{fieldName}\" name=\"{fieldName}\" type=\"checkbox\" className=\"form-check-input\" checked={{Boolean(formData.{fieldName})}} onChange={{handleChange}} />");
                js.AppendLine($"                                    <label htmlFor=\"{fieldName}\" className=\"form-check-label\">{fieldLabel}</label>");
                js.AppendLine("                                </div>");
                js.AppendLine("                            </div>");
                return;
            }

            js.AppendLine($"                            <div className=\"{columnClass}\">");
            js.AppendLine($"                                <label htmlFor=\"{fieldName}\" className=\"form-label\">{fieldLabel}</label>");
            js.AppendLine($"                                <input id=\"{fieldName}\" name=\"{fieldName}\" type=\"{inputType}\" className=\"form-control\" value={{formData.{fieldName} ?? ''}} onChange={{handleChange}}{required} />");
            js.AppendLine("                            </div>");
        }

        private static bool IsSystemField(string fieldName)
        {
            return fieldName.Equals("Code", StringComparison.OrdinalIgnoreCase) ||
                fieldName.Equals("Id", StringComparison.OrdinalIgnoreCase) ||
                fieldName.Equals("IsActive", StringComparison.OrdinalIgnoreCase) ||
                fieldName.Equals("IsDeleted", StringComparison.OrdinalIgnoreCase) ||
                fieldName.Equals("Created", StringComparison.OrdinalIgnoreCase) ||
                fieldName.Equals("Updated", StringComparison.OrdinalIgnoreCase);
        }

        private static string EscapeJsString(string value)
        {
            return (value ?? string.Empty).Replace("\\", "\\\\").Replace("'", "\\'");
        }

        private static string GetInputType(string dbType)
        {
            string normalizedDbType = (dbType ?? string.Empty).Trim().ToUpper();

            if (IsDateField(normalizedDbType))
                return "datetime-local";

            if (IsBooleanField(normalizedDbType))
                return "checkbox";

            if (IsNumberField(normalizedDbType))
                return "number";

            return "text";
        }

        private static string GetBootstrapColumnClass(MD_Campos campo)
        {
            string dbType = (campo.DAO.TipoCampo.Nome ?? string.Empty).Trim().ToUpper();

            if (IsLongTextField(dbType))
                return "col-12";

            if (IsBooleanField(dbType) || IsNumberField(dbType) || IsDateField(dbType))
                return "col-md-4";

            return "col-md-6";
        }

        private static bool IsDateField(string dbType)
        {
            string normalizedDbType = (dbType ?? string.Empty).Trim().ToUpper();
            return normalizedDbType.Contains("DATE") || normalizedDbType.Contains("TIME");
        }

        private static bool IsBooleanField(string dbType)
        {
            string normalizedDbType = (dbType ?? string.Empty).Trim().ToUpper();
            return normalizedDbType.Contains("BOOL") || normalizedDbType.Equals("BIT");
        }

        private static bool IsNumberField(string dbType)
        {
            string normalizedDbType = (dbType ?? string.Empty).Trim().ToUpper();
            return normalizedDbType.Contains("INT") ||
                normalizedDbType.Contains("DECIMAL") ||
                normalizedDbType.Contains("NUMERIC") ||
                normalizedDbType.Contains("FLOAT") ||
                normalizedDbType.Contains("DOUBLE") ||
                normalizedDbType.Contains("REAL");
        }

        private static bool IsLongTextField(string dbType)
        {
            string normalizedDbType = (dbType ?? string.Empty).Trim().ToUpper();
            return normalizedDbType.Contains("TEXT") ||
                normalizedDbType.Contains("CLOB") ||
                normalizedDbType.Contains("VARCHAR(MAX)") ||
                normalizedDbType.Contains("NVARCHAR(MAX)");
        }
    }
}
