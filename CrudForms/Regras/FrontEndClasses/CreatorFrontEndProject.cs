using Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Regras.FrontEndClasses
{
    public static class CreatorFrontEndProject
    {
        public static bool CreateProject(List<MD_Tabela> tabelas, string directory, string projectName, out string message)
        {
            bool success = true;
            message = string.Empty;
            StringBuilder errors = new StringBuilder();
            string projectPath = Path.Combine(directory, projectName);
            Visao.BarraDeCarregamento barra = new Visao.BarraDeCarregamento(10, "Criando classes");
            barra.Show();

            try
            {
                if (!CmdCommands.IsNodeInstalled())
                {
                    message = "Node não está instalado!";
                    return false;
                }

                if (Directory.Exists(projectPath))
                {
                    Directory.Delete(projectPath, true);
                }

                success &= CmdCommands.CreateReactAppAndFolders(projectName, directory);
                if (!success)
                {
                    errors.AppendLine("Erro ao criar o projeto!");
                }
                barra.AvancaBarra(1);

                success &= AssetsCreator.Create(projectPath);
                if (!success)
                {
                    errors.AppendLine("Erro ao criar o assets!");
                }
                barra.AvancaBarra(1);

                success &= ComponentsCreator.Create(tabelas, projectPath);
                if (!success)
                {
                    errors.AppendLine("Erro ao criar o components!");
                }
                barra.AvancaBarra(1);

                success &= ConfigCreator.Create(projectPath);
                if (!success)
                {
                    errors.AppendLine("Erro ao criar o config!");
                }
                barra.AvancaBarra(1);

                success &= LayoutsCreator.Create(projectPath);
                if (!success)
                {
                    errors.AppendLine("Erro ao criar o layouts!");
                }
                barra.AvancaBarra(1);

                success &= PagesCreator.Create(tabelas, projectPath);
                if (!success)
                {
                    errors.AppendLine("Erro ao criar o pages!");
                }

                success &= RoutesCreator.Create(tabelas, projectPath);
                if (!success)
                {
                    errors.AppendLine("Erro ao criar o routes!");
                }
                barra.AvancaBarra(1);

                success &= ServicesCreator.Create(tabelas, projectPath);
                if (!success)
                {
                    errors.AppendLine("Erro ao criar o service!");
                }
                barra.AvancaBarra(1);

                success &= UtilCreator.Create(projectPath);
                if (!success)
                {
                    errors.AppendLine("Erro ao criar o utils!");
                }
                barra.AvancaBarra(1);

                string basePath = Path.Combine(projectPath, "src");

                CreateIndex(basePath);
                CreateApp(basePath);
                barra.AvancaBarra(2);
                barra.Dispose();

                message = errors.ToString();
            }
            catch(Exception ex)
            {
                success = false;
                message = ex.Message;
            }

            return success;
        }

        private static void CreateIndex(string path)
        {
            StringBuilder js = new StringBuilder();
            js.AppendLine("import React from 'react';");
            js.AppendLine("import ReactDOM from 'react-dom/client';");
            js.AppendLine("import './assets/styles/index.css';");
            js.AppendLine("import App from './App';");
            js.AppendLine("import { Provider } from 'react-redux';");
            js.AppendLine("import store from './services/redux/store';");
            js.AppendLine("");
            js.AppendLine("const root = ReactDOM.createRoot(document.getElementById('root'));");
            js.AppendLine("root.render(");
            js.AppendLine("  <Provider store={store}>");
            js.AppendLine("    <App />");
            js.AppendLine("  </Provider>");
            js.AppendLine(");");

            File.WriteAllText(path + "//index.js", js.ToString());
        }

        private static void CreateApp(string path)
        {
            StringBuilder js = new StringBuilder();
            js.AppendLine("import { BrowserRouter as Router, Route, Routes } from 'react-router-dom';");
            js.AppendLine("import { useSelector } from 'react-redux';");
            js.AppendLine("import { ToastContainer } from 'react-toastify';");
            js.AppendLine("import 'react-toastify/dist/ReactToastify.css';");
            js.AppendLine("import LoadingModal from './components/common/Modals/LoadingModal/LoadingModal';");
            js.AppendLine("import AdminRoutes from './routes/admin/AdminRoutes';");
            js.AppendLine("import LoginPage from './pages/common/LoginPage/LoginPage';");
            js.AppendLine("import ConfirmUserPage from './pages/common/ConfirmUserPage/ConfirmUserPage';");
            js.AppendLine("import RecoverPasswordPage from './pages/common/RecoverPasswordPage/RecoverPasswordPage';");
            js.AppendLine("");
            js.AppendLine("function App() {");
            js.AppendLine("  const isLoading = useSelector((state) => state.loading.isLoading);");
            js.AppendLine("  const isAuthenticated = useSelector((state) => state.auth.isAuthenticated);");
            js.AppendLine("");
            js.AppendLine("  return (");
            js.AppendLine("    <Router>");
            js.AppendLine("      <div className=\"App\">");
            js.AppendLine("        <ToastContainer autoClose={2000} />");
            js.AppendLine("        {isLoading && <LoadingModal />}");
            js.AppendLine("        <Routes>");
            js.AppendLine("          {");
            js.AppendLine("            !isAuthenticated?");
            js.AppendLine("            <>");
            js.AppendLine("              <Route path=\"/\" element={<LoginPage />} />");
            js.AppendLine("              <Route path=\"/confirma\" element={<ConfirmUserPage isRecover={false}/>} />");
            js.AppendLine("              <Route path=\"/recuperar-senha\" element={<RecoverPasswordPage isRecover={true}/>} />");
            js.AppendLine("              <Route path=\"/recover\" element={<ConfirmUserPage isRecover={true}/>} />");
            js.AppendLine("            </>");
            js.AppendLine("            :");
            js.AppendLine("            <Route path=\"/*\" element={<AdminRoutes />} />");
            js.AppendLine("          }");
            js.AppendLine("          </Routes>");
            js.AppendLine("      </div>");
            js.AppendLine("    </Router>");
            js.AppendLine("  );");
            js.AppendLine("}");
            js.AppendLine("");
            js.AppendLine("export default App;");

            File.WriteAllText(path + "//App.js", js.ToString());
        }
    }
}
