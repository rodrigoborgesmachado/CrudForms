using Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Regras.FrontEndClasses
{
    public static class ServicesCreator
    {
        public static bool Create(List<MD_Tabela> tabelas, string projectPath)
        {
            bool success = true;

            try
            {
                string apiServices = NamesHandler.GetDirectoryByType(projectPath, NamesHandler.FileType.ServicesApiServices);
                CreateServiceApi(apiServices);
                CreateServiceTokenApi(apiServices);
                CreateServiceApiForEntities(tabelas, apiServices);

                string reduxServicePath = NamesHandler.GetDirectoryByType(projectPath, NamesHandler.FileType.ServicesRedux);
                CreateAuthSliceRedux(reduxServicePath);
                CreateLoadingSliceRedux(reduxServicePath);
                CreateStoreRedux(reduxServicePath);

                string servicePath = NamesHandler.GetDirectoryByType(projectPath, NamesHandler.FileType.Services);
                CreateConfigService(servicePath);
                CreateStorageService(servicePath);
            }
            catch (Exception ex)
            {
                Util.CL_Files.LogException(ex);
                success = false;
            }

            return success;
        }

        private static void CreateStorageService(string path)
        {
            StringBuilder js = new StringBuilder();
            js.AppendLine("const storageService = {");
            js.AppendLine("    setJsonItem(key, value) {");
            js.AppendLine("      localStorage.setItem(key, JSON.stringify(value));");
            js.AppendLine("    },");
            js.AppendLine("");
            js.AppendLine("    setItem(key, value) {");
            js.AppendLine("      localStorage.setItem(key, value);");
            js.AppendLine("    },");
            js.AppendLine("");
            js.AppendLine("    getItem(key) {");
            js.AppendLine("      const value = localStorage.getItem(key);");
            js.AppendLine("      return value ?? JSON.parse(value);");
            js.AppendLine("    },");
            js.AppendLine("");
            js.AppendLine("    removeItem(key) {");
            js.AppendLine("      localStorage.removeItem(key);");
            js.AppendLine("    },");
            js.AppendLine("");
            js.AppendLine("    clear() {");
            js.AppendLine("      localStorage.clear();");
            js.AppendLine("    },");
            js.AppendLine("");
            js.AppendLine("    // Optionally, add expiration handling");
            js.AppendLine("    setItemWithExpiry(key, value, expiryTimeInMinutes) {");
            js.AppendLine("      const now = new Date();");
            js.AppendLine("      const item = {");
            js.AppendLine("        value: value,");
            js.AppendLine("        expiry: now.getTime() + expiryTimeInMinutes * 60000");
            js.AppendLine("      };");
            js.AppendLine("      localStorage.setItem(key, JSON.stringify(item));");
            js.AppendLine("    },");
            js.AppendLine("");
            js.AppendLine("    getItemWithExpiry(key) {");
            js.AppendLine("      const item = JSON.parse(localStorage.getItem(key));");
            js.AppendLine("      if (!item) return null;");
            js.AppendLine("");
            js.AppendLine("      const now = new Date();");
            js.AppendLine("      if (now.getTime() > item.expiry) {");
            js.AppendLine("        localStorage.removeItem(key); // Remove expired item");
            js.AppendLine("        return null;");
            js.AppendLine("      }");
            js.AppendLine("      return item.value;");
            js.AppendLine("    }");
            js.AppendLine("  };");
            js.AppendLine("");
            js.AppendLine("export default storageService;");

            File.WriteAllText(path + "//storageService.js", js.ToString());
        }

        private static void CreateConfigService(string path)
        {
            StringBuilder js = new StringBuilder();
            js.AppendLine("import config from '../config/config.json';");
            js.AppendLine("");
            js.AppendLine("class ConfigService {");
            js.AppendLine("  constructor() {");
            js.AppendLine("    this.config = config;");
            js.AppendLine("  }");
            js.AppendLine("");
            js.AppendLine("  getApiUrl() {");
            js.AppendLine("    return this.config.apiUrl;");
            js.AppendLine("  }");
            js.AppendLine("");
            js.AppendLine("  getDefaultNumberOfItemsTable() {");
            js.AppendLine("    return this.config.DefaultNumberOfItemsTable;");
            js.AppendLine("  }");
            js.AppendLine("}");
            js.AppendLine("");
            js.AppendLine("var configService =  new ConfigService();");
            js.AppendLine("export default configService;");

            File.WriteAllText(path + "//configService.js", js.ToString());
        }

        private static void CreateStoreRedux(string path)
        {
            StringBuilder js = new StringBuilder();
            js.AppendLine("// services/redux/store.js");
            js.AppendLine("import { configureStore } from '@reduxjs/toolkit';");
            js.AppendLine("import authReducer from './authSlice'; // Import the authentication slice");
            js.AppendLine("import loadingReducer from './loadingSlice';");
            js.AppendLine("");
            js.AppendLine("const store = configureStore({");
            js.AppendLine("  reducer: {");
            js.AppendLine("    auth: authReducer,");
            js.AppendLine("    loading: loadingReducer,");
            js.AppendLine("  },");
            js.AppendLine("});");
            js.AppendLine("");
            js.AppendLine("export default store;");

            File.WriteAllText(path + "//store.js", js.ToString());
        }

        private static void CreateLoadingSliceRedux(string path)
        {
            StringBuilder js = new StringBuilder();
            js.AppendLine("// src/services/redux/loadingSlice.js");
            js.AppendLine("import { createSlice } from '@reduxjs/toolkit';");
            js.AppendLine("");
            js.AppendLine("const loadingSlice = createSlice({");
            js.AppendLine("  name: 'loading',");
            js.AppendLine("  initialState: {");
            js.AppendLine("    isLoading: false,");
            js.AppendLine("  },");
            js.AppendLine("  reducers: {");
            js.AppendLine("    setLoading: (state, action) => {");
            js.AppendLine("      state.isLoading = action.payload;");
            js.AppendLine("    },");
            js.AppendLine("  },");
            js.AppendLine("});");
            js.AppendLine("");
            js.AppendLine("export const { setLoading } = loadingSlice.actions;");
            js.AppendLine("export default loadingSlice.reducer;");

            File.WriteAllText(path + "//loadingSlice.js", js.ToString());
        }

        private static void CreateAuthSliceRedux(string path)
        {
            StringBuilder js = new StringBuilder();
            js.AppendLine("import { createSlice } from '@reduxjs/toolkit';");
            js.AppendLine("import storageService from '../../services/storageService';");
            js.AppendLine("import Config from '../../config/storageConfiguration';");
            js.AppendLine("");
            js.AppendLine("const token = storageService.getItem(Config.TOKEN);");
            js.AppendLine("const code = storageService.getItem(Config.CODE);");
            js.AppendLine("const name = storageService.getItem(Config.NAME);");
            js.AppendLine("");
            js.AppendLine("const authSlice = createSlice({");
            js.AppendLine("  name: 'auth',");
            js.AppendLine("  initialState: {");
            js.AppendLine("    token: token || null,");
            js.AppendLine("    code: code || null,");
            js.AppendLine("    isAuthenticated: !!token,");
            js.AppendLine("    name: name || '', ");
            js.AppendLine("    isAdmin: true");
            js.AppendLine("  },");
            js.AppendLine("  reducers: {");
            js.AppendLine("    login: (state, action) => {");
            js.AppendLine("      const { access_token, code, name } = action.payload;");
            js.AppendLine("      state.token = access_token;");
            js.AppendLine("      state.code = code;");
            js.AppendLine("      state.isAuthenticated = true;");
            js.AppendLine("      state.name = name;");
            js.AppendLine("      state.isAdmin = true;");
            js.AppendLine("");
            js.AppendLine("      storageService.setItem(Config.TOKEN, access_token);");
            js.AppendLine("      storageService.setItem(Config.NAME, name);");
            js.AppendLine("      storageService.setItem(Config.CODE, code);");
            js.AppendLine("    },");
            js.AppendLine("    logout: (state) => {");
            js.AppendLine("      state.token = null;");
            js.AppendLine("      state.isAuthenticated = false;");
            js.AppendLine("      state.name = '';");
            js.AppendLine("      state.isAdmin = false;");
            js.AppendLine("      state.isClient = false;");
            js.AppendLine("      storageService.clear();");
            js.AppendLine("    },");
            js.AppendLine("  },");
            js.AppendLine("});");
            js.AppendLine("");
            js.AppendLine("export const { login, logout } = authSlice.actions;");
            js.AppendLine("export const selectUserName = (state) => state.auth.name;");
            js.AppendLine("export const selectUserCode = (state) => state.auth.code;");
            js.AppendLine("export const isAuthenticated = (state) => state.isAuthenticated;");
            js.AppendLine("export default authSlice.reducer;");
            js.AppendLine("");

            File.WriteAllText(path + "//authSlice.js", js.ToString());
        }

        private static void CreateServiceApi(string path)
        {
            StringBuilder js = new StringBuilder();
            js.AppendLine("import axios from \"axios\";");
            js.AppendLine("import getCurrentEnvConfig from \"../../config/envConfig\";");
            js.AppendLine("import Config from \"../../config/storageConfiguration\";");
            js.AppendLine("import storageService from \"../storageService\";");
            js.AppendLine("");
            js.AppendLine("const apiUrl = getCurrentEnvConfig();");
            js.AppendLine("");
            js.AppendLine("const api = axios.create({");
            js.AppendLine("  baseURL: apiUrl,");
            js.AppendLine("  headers: {");
            js.AppendLine("    \"Content-Type\": \"application/json\",");
            js.AppendLine("  },");
            js.AppendLine("});");
            js.AppendLine("");
            js.AppendLine("/**");
            js.AppendLine(" * Retrieves token from storage safely.");
            js.AppendLine(" */");
            js.AppendLine("const getToken = () => {");
            js.AppendLine("  return storageService.getItem(Config[\"TOKEN\"]);");
            js.AppendLine("};");
            js.AppendLine("");
            js.AppendLine("/**");
            js.AppendLine(" * Request Interceptor: Adds Authorization header dynamically.");
            js.AppendLine(" */");
            js.AppendLine("api.interceptors.request.use(");
            js.AppendLine("  (config) => {");
            js.AppendLine("    const token = getToken();");
            js.AppendLine("    if (token) {");
            js.AppendLine("      config.headers[\"Authorization\"] = `Bearer ${token}`;");
            js.AppendLine("    }");
            js.AppendLine("    return config;");
            js.AppendLine("  },");
            js.AppendLine("  (error) => Promise.reject(error)");
            js.AppendLine(");");
            js.AppendLine("");
            js.AppendLine("/**");
            js.AppendLine(" * Response Interceptor: Handles errors such as 401 and network failures.");
            js.AppendLine(" */");
            js.AppendLine("api.interceptors.response.use(");
            js.AppendLine("  (response) => response,");
            js.AppendLine("  (error) => {");
            js.AppendLine("    if (error.code === \"ERR_NETWORK\") {");
            js.AppendLine("      console.error(\"Network error: Unable to connect to the server.\");");
            js.AppendLine("      return Promise.reject({ message: \"Network error. Please try again later.\" });");
            js.AppendLine("    }");
            js.AppendLine("");
            js.AppendLine("    if (error.response) {");
            js.AppendLine("      const { status } = error.response;");
            js.AppendLine("");
            js.AppendLine("      if (status === 401) {");
            js.AppendLine("        console.warn(\"Unauthorized: Clearing session and redirecting to login.\");");
            js.AppendLine("        storageService.clear();");
            js.AppendLine("");
            js.AppendLine("        // Avoid redirecting forcefully, instead use an event");
            js.AppendLine("        window.dispatchEvent(new Event(\"unauthorized\")); ");
            js.AppendLine("      }");
            js.AppendLine("    }");
            js.AppendLine("    return Promise.reject(error);");
            js.AppendLine("  }");
            js.AppendLine(");");
            js.AppendLine("");
            js.AppendLine("/**");
            js.AppendLine(" * Example: Listen for unauthorized events globally");
            js.AppendLine(" */");
            js.AppendLine("window.addEventListener(\"unauthorized\", () => {");
            js.AppendLine("  window.location.href = \"/\"; // Redirect to login page");
            js.AppendLine("});");
            js.AppendLine("");
            js.AppendLine("export default api;");

            File.WriteAllText(path + "//serviceApi.js", js.ToString());
        }

        private static void CreateServiceTokenApi(string path)
        {
            StringBuilder js = new StringBuilder();
            js.AppendLine("import api from './serviceApi'; // Import the axios instance");
            js.AppendLine("");
            js.AppendLine("const tokenApi = {");
            js.AppendLine("    /**");
            js.AppendLine("     * Authenticate a user and retrieve a token.");
            js.AppendLine("     * @param {Object} credentials - The user credentials containing `userName` and `password`.");
            js.AppendLine("     * @returns {Promise<Object>} - The authentication response containing the access token.");
            js.AppendLine("     */");
            js.AppendLine("    getToken: async (credentials) => {");
            js.AppendLine("        try {");
            js.AppendLine("            const response = await api.post('/token', credentials);");
            js.AppendLine("            return response.data;");
            js.AppendLine("        } catch (error) {");
            js.AppendLine("            console.error('Error fetching token:', error);");
            js.AppendLine("            throw error;");
            js.AppendLine("        }");
            js.AppendLine("    },");
            js.AppendLine("};");
            js.AppendLine("");
            js.AppendLine("export default tokenApi;");

            File.WriteAllText(path + "//tokenApi.js", js.ToString());
        }

        private static void CreateServiceApiForEntities(List<MD_Tabela> tabelas, string path)
        {
            foreach(var table in tabelas)
            {
                StringBuilder js = new StringBuilder();
                js.AppendLine("import api from './serviceApi'; // Import the axios instance");
                js.AppendLine("");
                js.AppendLine($"const {NamesHandler.GetApiName(table.DAO.Nome)} = {{");
                js.AppendLine("    /**");
                js.AppendLine("     * Fetch all with optional filters.");
                js.AppendLine("     * @param {Object} params - Query parameters such as `include`.");
                js.AppendLine("     * @returns {Promise<Object>} - The list.");
                js.AppendLine("     */");
                js.AppendLine("    getAll: async (params = {}) => {");
                js.AppendLine("        try {");
                js.AppendLine($"            const response = await api.get('/{NamesHandler.CreateName(table.DAO.Nome)}', {{ params }});");
                js.AppendLine("            return response.data;");
                js.AppendLine("        } catch (error) {");
                js.AppendLine("            console.error('Error fetching all items es:', error);");
                js.AppendLine("            throw error;");
                js.AppendLine("        }");
                js.AppendLine("    },");
                js.AppendLine("");
                js.AppendLine("    /**");
                js.AppendLine("     * Fetch a item by its code.");
                js.AppendLine("     * @param {string} code - The item's unique code.");
                js.AppendLine("     * @param {Object} params - Query parameters such as `include`.");
                js.AppendLine("     * @returns {Promise<Object>} - The item data.");
                js.AppendLine("     */");
                js.AppendLine("    getByCode: async (code, params = {}) => {");
                js.AppendLine("        try {");
                js.AppendLine($"            const response = await api.get(`/{NamesHandler.CreateName(table.DAO.Nome)}/${{code}}`, {{ params }});");
                js.AppendLine("            return response.data;");
                js.AppendLine("        } catch (error) {");
                js.AppendLine("            console.error('Error fetching items by code:', error);");
                js.AppendLine("            throw error;");
                js.AppendLine("        }");
                js.AppendLine("    },");
                js.AppendLine("");
                js.AppendLine("    /**");
                js.AppendLine("     * Create a new item.");
                js.AppendLine("     * @param {Object} itemsData - The items data to create.");
                js.AppendLine("     * @returns {Promise<Object>} - The created items data.");
                js.AppendLine("     */");
                js.AppendLine("    create: async (itemsData) => {");
                js.AppendLine("        try {");
                js.AppendLine($"            const response = await api.post('/{NamesHandler.CreateName(table.DAO.Nome)}', itemsData);");
                js.AppendLine("            return response.data;");
                js.AppendLine("        } catch (error) {");
                js.AppendLine("            console.error('Error creating items:', error);");
                js.AppendLine("            throw error;");
                js.AppendLine("        }");
                js.AppendLine("    },");
                js.AppendLine("");
                js.AppendLine("    /**");
                js.AppendLine("     * Update an existing items.");
                js.AppendLine("     * @param {Object} itemsData - The items data to update.");
                js.AppendLine("     * @returns {Promise<Object>} - The updated items data.");
                js.AppendLine("     */");
                js.AppendLine("    update: async (itemsData) => {");
                js.AppendLine("        try {");
                js.AppendLine($"            const response = await api.put('/{NamesHandler.CreateName(table.DAO.Nome)}', itemsData);");
                js.AppendLine("            return response.data;");
                js.AppendLine("        } catch (error) {");
                js.AppendLine("            console.error('Error updating items :', error);");
                js.AppendLine("            throw error;");
                js.AppendLine("        }");
                js.AppendLine("    },");
                js.AppendLine("");
                js.AppendLine("    /**");
                js.AppendLine("     * Delete a item by its code.");
                js.AppendLine("     * @param {string} itemsCode - The item code to delete.");
                js.AppendLine("     * @returns {Promise<Object>} - The result of the deletion.");
                js.AppendLine("     */");
                js.AppendLine("    delete: async (itemsCode) => {");
                js.AppendLine("        try {");
                js.AppendLine($"            const response = await api.delete('/{NamesHandler.CreateName(table.DAO.Nome)}', {{");
                js.AppendLine("                data: { code: itemsCode },");
                js.AppendLine("            });");
                js.AppendLine("            return response.data;");
                js.AppendLine("        } catch (error) {");
                js.AppendLine("            console.error('Error deleting item:', error);");
                js.AppendLine("            throw error;");
                js.AppendLine("        }");
                js.AppendLine("    },");
                js.AppendLine("");
                js.AppendLine("    /**");
                js.AppendLine("     * Update object status by params.");
                js.AppendLine("     * @param {Object} params - Query parameters such as `id`, `isActive`.");
                js.AppendLine("     * @returns {Promise<Blob>} - The updated object.");
                js.AppendLine("     */");
                js.AppendLine("    updateStatus: async (params) => {");
                js.AppendLine("        try {");
                js.AppendLine($"            const response = await api.put('/{NamesHandler.CreateName(table.DAO.Nome)}/UpdateStatus', {{}}, {{");
                js.AppendLine("                params");
                js.AppendLine("            });");
                js.AppendLine("            return response.data;");
                js.AppendLine("        } catch (error) {");
                js.AppendLine("            console.error('Error updating item:', error);");
                js.AppendLine("            throw error;");
                js.AppendLine("        }");
                js.AppendLine("    },");
                js.AppendLine("");
                js.AppendLine("    /**");
                js.AppendLine("     * Export items es based on filters.");
                js.AppendLine("     * @param {Object} params - Query parameters such as `quantityMax`, `isActive`, `term`, `orderBy`, `include`.");
                js.AppendLine("     * @returns {Promise<Blob>} - The exported file data.");
                js.AppendLine("     */");
                js.AppendLine("    export: async (params) => {");
                js.AppendLine("        try {");
                js.AppendLine($"            const response = await api.get('/{NamesHandler.CreateName(table.DAO.Nome)}/export', {{");
                js.AppendLine("                params,");
                js.AppendLine("");
                js.AppendLine("            });");
                js.AppendLine("            return response.data;");
                js.AppendLine("        } catch (error) {");
                js.AppendLine("            console.error('Error exporting items es:', error);");
                js.AppendLine("            throw error;");
                js.AppendLine("        }");
                js.AppendLine("    },");
                js.AppendLine("");
                js.AppendLine("    /**");
                js.AppendLine("     * Fetch a paginated list of items with optional filters.");
                js.AppendLine("     * @param {Object} params - Query parameters such as `page`, `quantity`, `isActive`, `term`, `orderBy`, `include`.");
                js.AppendLine("     * @returns {Promise<Object>} - The paginated list of items.");
                js.AppendLine("     */");
                js.AppendLine("    getPaginated: async (params) => {");
                js.AppendLine("        try {");
                js.AppendLine($"            const response = await api.get('/{NamesHandler.CreateName(table.DAO.Nome)}/pagged', {{ params }});");
                js.AppendLine("            return response.data;");
                js.AppendLine("        } catch (error) {");
                js.AppendLine("            console.error('Error fetching paginated items:', error);");
                js.AppendLine("            throw error;");
                js.AppendLine("        }");
                js.AppendLine("    },");
                js.AppendLine("};");
                js.AppendLine("");
                js.AppendLine($"export default {NamesHandler.GetApiName(table.DAO.Nome)};");

                File.WriteAllText(path + $"//{NamesHandler.GetApiName(table.DAO.Nome)}.js", js.ToString());

            }
        }
    }
}
