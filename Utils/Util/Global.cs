﻿using System;
using System.Collections.Generic;
using System.Data.Common;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Util
{
    public static class Global
    {
        public static string tempTable = "TESTE";

        // Caminho principal da aplicação
        public static string app_main_directoty = System.IO.Directory.GetCurrentDirectory() + "\\";

        // Caminho da pasta de logs do sistema
        public static string app_logs_directoty = app_main_directoty + "Log\\";

        // Caminho da pasta de arquivos temporários
        public static string app_temp_directory = app_main_directoty + "TEMP\\";

        // Nome do diretório de saída
        public static string app_out_directory = app_main_directoty + "OUT\\";

        // Nome do diretório do banco de dados
        public static string app_base_directory = app_main_directoty + "BASE\\";

        // Nome do diretório do DER
        public static string app_DER_directory = app_main_directoty + "DER\\";

        // Nome do diretório dos scripts
        public static string app_Script_directory = app_main_directoty + "Script\\";

        // Nome do diretório do Img do html
        public static string app_Img_directory = app_main_directoty + "Img\\";

        // Nome do diretório de EXPORTAÇÃO
        public static string app_exportacao_directory = app_main_directoty + "EXPORTACAO\\";

        // Nome do diretório de Classes
        public static string app_classes_directory = app_main_directoty + "Classes\\";

        // Nome do diretório de relatórios
        public static string app_rel_directory = app_main_directoty + "Relatorios\\";

        // Nome do diretório do Img do html
        public static string app_Files_directory = app_rel_directory + "Files\\";

        // Nome do arquivo de EXPORTAÇÃO 
        public static string app_exportacao_tabela_file = app_exportacao_directory + "tabela.json";

        // Nome do arquivo de EXPORTAÇÃO 
        public static string app_exportacao_campos_file = app_exportacao_directory + "campos.json";

        // Nome do arquivo de EXPORTAÇÃO 
        public static string app_exportacao_relacionamentos_file = app_exportacao_directory + "relacionamentos.json";

        // Nome do diretório de IMPORTACAO
        public static string app_importacao_directory = app_main_directoty + "IMPORTACAO\\";

        // Nome do aruqivo do DER
        public static string app_DER_file_TableB = app_DER_directory + "TableB.html";

        // Nome do aruqivo do DER
        public static string app_DER_file_TableR = app_DER_directory + "TableR.html";

        // Nome do aruqivo do DER
        public static string app_DER_file_Table = app_DER_directory + "Table.html";

        // Nome do programa de importação
        public static string app_IMPORT_file = app_main_directoty + "DbExtractor.exe";

        // Nome do arquivo CSS
        public static string app_DERCSS_file = app_DER_directory + "DER.css";

        // Nome do arquivo sqlite de configuração
        public static string app_base_file = app_base_directory + "pckdb.db3";

        // Nome do arquivo html temporário
        public static string app_temp_html_file = app_temp_directory + "file_html.html";

        // Nome da classe de CL_File
        public static string app_claseCLFile_file = "CL_File.cs";

        // Nome da classe DataBase
        public static string app_claseDataBase_file = "DataBase.cs";

        // Nome da classe Enumerator
        public static string app_claseEnumerator_file = "Enumerator.cs";

        // Nome da classe Global
        public static string app_claseGlobal_file = "Global.cs";

        // Nome da classe de MDN_Model
        public static string app_claseMDNModel_file = "MDN_Model.cs";

        // Nome da classe de MDN_Table
        public static string app_claseMDNTable_file = "MDN_Table.cs";

        // Nome da classe de MDN_Campo
        public static string app_claseMDNCampo_file = "MDN_Campo.cs";

        // Parametro do connections
        public static string parametro_connectionStrings = "CONNECTIONSTRING";

        /// <summary>
        /// Enumerador referente ao tipo de log que o sistema irá persistir
        /// </summary>
        public enum TipoLog
        {
            DETALHADO = 0,
            SIMPLES = 1
        }

        /// <summary>
        /// Enum responsável por verificar se deve carregar o tree view automaticamente ou não
        /// </summary>
        public enum Automatico
        {
            Automatico = 0,
            Manual = 1
        }

        /// <summary>
        /// Enum responsável por verificar se deve carregar o tree view automaticamente ou não
        /// </summary>
        public enum Informacao
        {
            NAOAPRESENTAR = 0,
            APRESETAR = 1
        }

        /// <summary>
        /// Tipo mde log que o sistema está utilizando
        /// </summary>
        public static TipoLog log_system = TipoLog.SIMPLES;

        /// <summary>
        /// Carregar automaticamente ou não o tree view da tela principal
        /// </summary>
        public static Automatico CarregarAutomaticamente = Automatico.Automatico;

        /// <summary>
        /// Método que valida se deve apresentar mensagem de apresentação ou não 
        /// </summary>
        public static Informacao ApresentaInformacao = Informacao.APRESETAR;

        #region Métodos globais

        /// <summary>
        /// Método que insere os dados iniciais
        /// </summary>
        public static void InsereDadosIniciais()
        {
            DAO.MD_Parametros parametro = new DAO.MD_Parametros(parametro_connectionStrings);
            if (parametro.Empty)
                parametro.Insert();
        }

        #endregion Métodos globais

    }
}
