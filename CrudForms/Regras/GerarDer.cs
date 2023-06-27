using System;
using System.Collections.Generic;
using System.Data.Common;
using System.IO;
using System.Linq;
using System.Text;
using Util;
using Visao;

namespace Regras
{
    public class GerarDer
    {
        /// <summary>
        /// Método que gera o html do relatório
        /// </summary>
        /// <param name="project"></param>
        public bool Gerar(List<Model.MD_Tabela> tabelas, List<Model.MD_Campos> campos)
        {
            try
            {
                CopiarIMG();

                CriaCSS();

                ApagaArquivoHtml();

                StringBuilder builder_tableB = new StringBuilder();
                StringBuilder builder_tableR = new StringBuilder();

                PreencheCabecalho(ref builder_tableB, ref builder_tableR);

                PreencheText(tabelas, campos, ref builder_tableB, ref builder_tableR);

                PreencheFinalTexto(ref builder_tableB, ref builder_tableR);

                ArmazenaBuilderHTML(builder_tableB, builder_tableR);
            }
            catch (Exception e)
            {
                Util.CL_Files.LogException(e);
                return false;
            }
            return true;
        }

        #region HTML

        /// <summary>
        /// Método que armazena no arquivo css todo o texto do builder
        /// </summary>
        /// <param name="builderB">Builder</param>
        private void ArmazenaBuilderHTML(StringBuilder builderB, StringBuilder builderR)
        {
            Util.CL_Files.WriteOnTheLog("Document.ArmazenaBuilderHTML", Global.TipoLog.SIMPLES);

            string[] lista = builderB.ToString().Split(Environment.NewLine.ToCharArray());
            File.WriteAllLines(Util.Global.app_DER_file_TableB, lista, Encoding.UTF8);


            lista = builderR.ToString().Split(Environment.NewLine.ToCharArray());
            File.WriteAllLines(Util.Global.app_DER_file_TableR, lista, Encoding.UTF8);

            StringBuilder builder = BuilderTable();

            lista = builder.ToString().Split(Environment.NewLine.ToCharArray());
            File.WriteAllLines(Util.Global.app_DER_file_Table, lista, Encoding.UTF8);
        }

        /// <summary>
        /// Método que cria o buider do table
        /// </summary>
        /// <returns>String builder com o html</returns>
        private StringBuilder BuilderTable()
        {
            Util.CL_Files.WriteOnTheLog("Document.BuilderTable()", Util.Global.TipoLog.DETALHADO);

            StringBuilder builder = new StringBuilder();

            builder.Append("<!DOCTYPE HTML PUBLIC \"-//W3C//DTD HTML 4.01 Frameset//EN\"  \"http://www.w3.org/TR/html4/frameset.dtd\">" + Environment.NewLine);
            builder.Append("<html>" + Environment.NewLine);
            builder.Append("	<head>" + Environment.NewLine);
            builder.Append("		<title>CASE Studio 2 - HTML report</title>" + Environment.NewLine);
            builder.Append("	</head>" + Environment.NewLine);
            builder.Append("		<frameset cols=\"25%,*\" border=\"0\">" + Environment.NewLine);
            builder.Append("			<frame src = \"TableR.html\" name=\"references\" scrolling=\"auto\" frameborder=\"0\" marginheight=\"0\" marginwidth=\"0\">" + Environment.NewLine);
            builder.Append("			<frame src = \"TableB.html\" name=\"body\" scrolling=\"yes\" frameborder=\"0\" marginheight=\"0\" marginwidth=\"0\">" + Environment.NewLine);
            builder.Append("		</frameset>" + Environment.NewLine);
            builder.Append("</html>" + Environment.NewLine);

            return builder;
        }

        /// <summary>
        /// Método que preenche o corpo do html
        /// </summary>
        /// <param name="projeto">Projeto para se preencher o corpo do html</param>
        /// <param name="builder_tableB">string builder para montar o html</param>
        private void PreencheText(List<Model.MD_Tabela> tabelas, List<Model.MD_Campos> campos, ref StringBuilder builder_tableB, ref StringBuilder builder_tableR)
        {
            int quantidade = 0;

            string sentenca = "SELECT COUNT(1) AS contador FROM " + new DAO.MD_Tabela().table.Table_Name;

            DbDataReader reader = DataBase.Connection.Select(sentenca);

            if (reader == null)
                return;

            if (reader.Read())
            {
                quantidade = int.Parse(reader["contador"].ToString());
            }
            reader.Close();

            BarraDeCarregamento barraCarregamento = new BarraDeCarregamento(quantidade, "Criando DER");
            barraCarregamento.Show();

            builder_tableR.Append("<div class=\"bookmark\">" + Environment.NewLine);

            foreach (Model.MD_Tabela tabela in tabelas)
            {
                barraCarregamento.AvancaBarra(1);

                builder_tableB.Append("<a name = \"Table_" + tabela.DAO.Codigo + "\"></a>" + Environment.NewLine);
                builder_tableB.Append("<div class=\"caption1\">" + tabela.DAO.Nome + "</div>" + Environment.NewLine);

                builder_tableB.Append("<div class=\"caption2\">Columns</div>" + Environment.NewLine);
                PreencheColunas(campos.Where(c => c.DAO.Tabela.Codigo.Equals(tabela.DAO.Codigo)).ToList(), ref builder_tableB);

                //builder_tableB.Append("<div class=\"caption2\">Relationships</div>" + Environment.NewLine);
                //PreencheRelationships(tabela, ref builder_tableB);

                builder_tableB.Append("<div class=\"caption2\">Notes</div>" + Environment.NewLine);
                PreencheNotes(tabela, ref builder_tableB);

                builder_tableB.Append("<br><br> " + Environment.NewLine);

                builder_tableR.Append("<a href=\"TableB.html#Table_" + tabela.DAO.Codigo + "\"target=\"body\">" + tabela.DAO.Nome + "</a>" + Environment.NewLine);
            }
            builder_tableR.Append("</div>" + Environment.NewLine);

            barraCarregamento.Hide();
            barraCarregamento.Dispose();
            barraCarregamento = null;

        }

        /// <summary>
        /// Método que preenche o comments da tabela
        /// </summary>
        /// <param name="tabela">Tabela para filtar as colunas</param>
        /// <param name="builder">Builder para montar o HTML</param>
        private void PreencheNotes(Model.MD_Tabela tabela, ref StringBuilder builder)
        {
            Util.CL_Files.WriteOnTheLog("Document.PreencheNotes()", Util.Global.TipoLog.DETALHADO);

            builder.Append("<table class=\"tabformat\" border=\"0\" cellpadding=\"2\" cellspacing=\"1\" width=\"100%\">" + Environment.NewLine);
            builder.Append("<tr>" + Environment.NewLine);
            builder.Append("<td class=\"tabdata\" >" + tabela.DAO.Descricao);
            builder.Append("</td>" + Environment.NewLine);
            builder.Append("</tr>" + Environment.NewLine);
            builder.Append("<tr>" + Environment.NewLine);
            builder.Append("<td class=\"tabdata\" >" + tabela.DAO.Notas);
            builder.Append("</td>" + Environment.NewLine);
            builder.Append("</tr>" + Environment.NewLine);
            builder.Append("</table>" + Environment.NewLine);
        }

        /// <summary>
        /// Método que monta as relações da tabela
        /// </summary>
        /// <param name="tabela">Tabela para filtar as colunas</param>
        /// <param name="builder">Builder para montar o HTML</param>
        private void PreencheRelationships(Model.MD_Tabela tabela, ref StringBuilder builder)
        {
            Util.CL_Files.WriteOnTheLog("Document.PreencheRelationships()", Util.Global.TipoLog.DETALHADO);

            builder.Append("<table class=\"tabformat\" border=\"0\" cellpadding=\"2\" cellspacing=\"1\" width=\"100%\">" + Environment.NewLine);
            builder.Append("" + Environment.NewLine);
            builder.Append("<tr>" + Environment.NewLine);

            builder.Append("<td class=\"tabhead\" > Constraint (relationship) name    </td>" + Environment.NewLine);
            builder.Append("<td class=\"tabhead\" > Relationship type    </td>" + Environment.NewLine);
            builder.Append("<td class=\"tabhead\" > Parent table    </td>" + Environment.NewLine);
            builder.Append("<td class=\"tabhead\" > Child table    </td>" + Environment.NewLine);
            builder.Append("<td class=\"tabhead\" > Card.    </td>" + Environment.NewLine);

            builder.Append("</tr>" + Environment.NewLine);

            string sentenca = new DAO.MD_Relacao().table.CreateCommandSQLTable() + " WHERE TABELAORIGEM = " + tabela.DAO.Codigo + " OR TABELADESTINO = " + tabela.DAO.Codigo + " ORDER BY FOREINGKEY";
            DbDataReader reader = DataBase.Connection.Select(sentenca);

            while (reader.Read())
            {
                int codigo = int.Parse(reader["CODIGO"].ToString());
                Model.MD_Tabela tabelaOrigem = new Model.MD_Tabela(int.Parse(reader["TABELAORIGEM"].ToString()), tabela.DAO.Projeto);
                Model.MD_Tabela tabelaDestino = new Model.MD_Tabela(int.Parse(reader["TABELADESTINO"].ToString()), tabela.DAO.Projeto);
                Model.MD_Campos campoOrigem = new Model.MD_Campos(int.Parse(reader["CAMPOORIGEM"].ToString()), tabelaOrigem.DAO.Codigo, tabelaOrigem.DAO.Projeto);
                Model.MD_Campos campoDestino = new Model.MD_Campos(int.Parse(reader["CAMPODESTINO"].ToString()), tabelaDestino.DAO.Codigo, tabelaDestino.DAO.Projeto);

                Model.MD_Relacao relacao = new Model.MD_Relacao(codigo, tabela.DAO.Projeto, tabelaOrigem.DAO, tabelaDestino.DAO, campoOrigem.DAO, campoDestino.DAO);

                string relationship = relacao.DAO.TabelaOrigem.Descricao + " x " + relacao.DAO.TabelaDestino.Descricao + "(" + relacao.DAO.TabelaOrigem.Nome + " x " + relacao.DAO.TabelaDestino.Nome + ")";
                string card = relacao.DAO.CardinalidadeOrigem + ":" + relacao.DAO.CardinalidadeDestino;
                string parentTable = relacao.DAO.TabelaOrigem.Nome;
                string childTable = relacao.DAO.TabelaDestino.Nome;

                builder.Append("<tr>" + Environment.NewLine);
                builder.Append("    <td class=\"tabdata\" > " + relationship + "   </td>" + Environment.NewLine);
                builder.Append("    <td class=\"tabdata\" > Identifying   </td>" + Environment.NewLine);
                builder.Append("    <td class=\"tabdata\" > " + parentTable + "  </td>" + Environment.NewLine);
                builder.Append("    <td class=\"tabdata\" > " + childTable + "   </td>" + Environment.NewLine);
                builder.Append("    <td class=\"tabdata\" > " + card + "  </td>" + Environment.NewLine);
                builder.Append("</tr>" + Environment.NewLine);

                tabelaOrigem = null;
                tabelaDestino = null;
                campoOrigem = null;
                campoDestino = null;
                relacao = null;
            }

            reader.Close();
            reader = null;

            builder.Append("" + Environment.NewLine);
            builder.Append("</table>" + Environment.NewLine);
        }

        /// <summary>
        /// Método que preenche as colunas
        /// </summary>
        /// <param name="tabela">Tabela para filtar as colunas</param>
        /// <param name="builder">Builder para montar o HTML</param>
        private void PreencheColunas(List<Model.MD_Campos> campos, ref StringBuilder builder)
        {
            Util.CL_Files.WriteOnTheLog("Document.PreencheColunas()", Util.Global.TipoLog.DETALHADO);

            builder.Append("<table class=\"tabformat\" border=\"0\" cellpadding=\"2\" cellspacing=\"1\" width=\"100%\">" + Environment.NewLine);
            builder.Append("" + Environment.NewLine);
            builder.Append("<tr>" + Environment.NewLine);

            builder.Append("    <td class=\"tabhead\" > Key    </td>" + Environment.NewLine);
            builder.Append("    <td class=\"tabhead\" > Column name    </td>" + Environment.NewLine);
            builder.Append("    <td class=\"tabhead\" > Domain    </td>" + Environment.NewLine);
            builder.Append("    <td class=\"tabhead\" > Data type    </td>" + Environment.NewLine);
            builder.Append("    <td class=\"tabhead\" > Not null    </td>" + Environment.NewLine);
            builder.Append("    <td class=\"tabhead\" > Unique    </td>" + Environment.NewLine);
            builder.Append("    <td class=\"tabhead\" > Check    </td>" + Environment.NewLine);
            builder.Append("    <td class=\"tabhead\" > Default    </td>" + Environment.NewLine);
            builder.Append("    <td class=\"tabhead\"  colspan=\"2\"> Comments    </td>  " + Environment.NewLine);

            builder.Append("</tr>" + Environment.NewLine);

            foreach (Model.MD_Campos campo in campos)
            {
                string pfk = (campo.DAO.PrimaryKey ? (campo.DAO.ForeingKey ? "PFK" : "PK") : (campo.DAO.ForeingKey ? "FK" : string.Empty));
                string nome = campo.DAO.Nome;
                string dominio = campo.DAO.Dominio;
                string tipo = campo.DAO.TipoCampo.Nome;

                if (campo.DAO.TipoCampo.Nome.ToUpper().Equals("VARCHAR") || campo.DAO.TipoCampo.Nome.ToUpper().Equals("CHAR"))
                {
                    tipo += "(" + campo.DAO.Tamanho + ")";
                }
                else if (campo.DAO.TipoCampo.Nome.ToUpper().Equals("DECIMAL") || campo.DAO.TipoCampo.Nome.ToUpper().Equals("FLOAT"))
                {
                    tipo += "(" + campo.DAO.Precisao.ToString().Replace(".", ",") + ")";
                }

                string notnull = campo.DAO.NotNull ? "YES" : "NO";
                string unique = campo.DAO.Unique ? "YES" : "NO";
                string check = campo.DAO.Check;
                string _default = campo.DAO.Default == null ? string.Empty : campo.DAO.Default.ToString();
                string comments = campo.DAO.Comentario;

                builder.Append("<tr>" + Environment.NewLine);

                builder.Append("  <td class=\"tabdata\" > " + pfk + " </td>" + Environment.NewLine);
                builder.Append("  <td class=\"tabdata\" > " + nome + " </td>" + Environment.NewLine);
                builder.Append("  <td class=\"tabdata\" > " + dominio + "  </td>" + Environment.NewLine);
                builder.Append("  <td class=\"tabdata\" > " + tipo + "  </td>" + Environment.NewLine);
                builder.Append("  <td class=\"tabdata\" > " + notnull + "   </td>" + Environment.NewLine);
                builder.Append("  <td class=\"tabdata\" > " + unique + "  </td>" + Environment.NewLine);
                builder.Append("  <td class=\"tabdata\" > " + check + "   </td>" + Environment.NewLine);
                builder.Append("  <td class=\"tabdata\" > " + _default + "   </td>" + Environment.NewLine);
                builder.Append("  <td class=\"tabdata\"  colspan=\"2\"> " + comments + " </ td > " + Environment.NewLine);

                builder.Append("</tr>" + Environment.NewLine);
            }

            builder.Append("" + Environment.NewLine);
            builder.Append("</table>" + Environment.NewLine);
        }

        /// <summary>
        /// Método que preenche o cabeçalho
        /// </summary>
        /// <param name="builder_tableB"></param>
        private void PreencheCabecalho(ref StringBuilder builder_tableB, ref StringBuilder builder_tableR)
        {
            Util.CL_Files.WriteOnTheLog("Document.PreencheCabecalho", Global.TipoLog.SIMPLES);

            builder_tableB.Append("" + Environment.NewLine);
            builder_tableB.Append("<!DOCTYPE HTML PUBLIC>" + Environment.NewLine);
            builder_tableB.Append("" + Environment.NewLine);
            builder_tableB.Append("<html>" + Environment.NewLine);
            builder_tableB.Append("	<head>" + Environment.NewLine);
            builder_tableB.Append("		<meta http-equiv=\"Content-Type\" content=\"text/html; charset=iso-8859-1\">" + Environment.NewLine);
            builder_tableB.Append("		<meta name=\"keywords\" content=\"CASE Studio 2, html, report\">" + Environment.NewLine);
            builder_tableB.Append("		<link rel=\"stylesheet\" type=\"text/css\" media=\"screen\" href=\"DER.css\" title=\"report\">" + Environment.NewLine);
            builder_tableB.Append("		<title>CASE Studio 2 - HTML Report</title>" + Environment.NewLine);
            builder_tableB.Append("	</head>" + Environment.NewLine);
            builder_tableB.Append("<body class=\"rightPage\">" + Environment.NewLine);

            builder_tableR.Append("" + Environment.NewLine);
            builder_tableR.Append("<!DOCTYPE HTML PUBLIC>" + Environment.NewLine);
            builder_tableR.Append("" + Environment.NewLine);
            builder_tableR.Append("<html>" + Environment.NewLine);
            builder_tableR.Append("	<head>" + Environment.NewLine);
            builder_tableR.Append("		<meta http-equiv=\"Content-Type\" content=\"text/html; charset=iso-8859-1\">" + Environment.NewLine);
            builder_tableR.Append("		<meta name=\"keywords\" content=\"CASE Studio 2, html, report\">" + Environment.NewLine);
            builder_tableR.Append("		<link rel=\"stylesheet\" type=\"text/css\" media=\"screen\" href=\"DER.css\" title=\"report\">" + Environment.NewLine);
            builder_tableR.Append("		<title>CASE Studio 2 - HTML Report</title>" + Environment.NewLine);
            builder_tableR.Append("	</head>" + Environment.NewLine);
            builder_tableR.Append("<body class=\"leftPage\">" + Environment.NewLine);
        }

        /// <summary>
        /// Método que finaliza o html
        /// </summary>
        private void PreencheFinalTexto(ref StringBuilder builder_tableB, ref StringBuilder builder_tableR)
        {
            Util.CL_Files.WriteOnTheLog("Document.PreencheFinalTexto", Global.TipoLog.SIMPLES);

            builder_tableB.Append("</body>" + Environment.NewLine);
            builder_tableB.Append("</html>" + Environment.NewLine);

            builder_tableR.Append("</body>" + Environment.NewLine);
            builder_tableR.Append("</html>" + Environment.NewLine);
        }

        /// <summary>
        /// Método que apaga o arquivo html
        /// </summary>
        private void ApagaArquivoHtml()
        {
            Util.CL_Files.WriteOnTheLog("Document.ApagaArquivoHtml", Global.TipoLog.SIMPLES);

            if (File.Exists(Util.Global.app_DER_file_TableB))
            {
                File.Delete(Util.Global.app_DER_file_TableB);
            }

            if (File.Exists(Util.Global.app_DER_file_TableR))
            {
                File.Delete(Util.Global.app_DER_file_TableR);
            }

            if (File.Exists(Util.Global.app_DER_file_Table))
            {
                File.Delete(Util.Global.app_DER_file_Table);
            }
        }

        #endregion HTML

        #region Arquivo CSS

        /// <summary>
        /// Método que cria o arquivo css
        /// </summary>
        private void CriaCSS()
        {
            Util.CL_Files.WriteOnTheLog("Document.CriaCSS", Global.TipoLog.SIMPLES);

            ApagaArquivoCSS();

            StringBuilder builder = new StringBuilder();
            PreencheArquivoCSS(ref builder);
            ArmazenaBuilderCSS(builder);
        }

        /// <summary>
        /// Método que armazena no arquivo css todo o texto do builder
        /// </summary>
        /// <param name="builder">Builder</param>
        private static void ArmazenaBuilderCSS(StringBuilder builder)
        {
            Util.CL_Files.WriteOnTheLog("Document.ArmazenaBuilderCSS()", Util.Global.TipoLog.DETALHADO);

            string[] lista = builder.ToString().Split(Environment.NewLine.ToCharArray());
            File.WriteAllLines(Util.Global.app_DERCSS_file, lista);
        }

        /// <summary>
        /// Método que cria o arquivo html
        /// </summary>
        private void ApagaArquivoCSS()
        {
            Util.CL_Files.WriteOnTheLog("Document.ApagaArquivoCSS()", Util.Global.TipoLog.DETALHADO);

            if (File.Exists(Util.Global.app_DERCSS_file))
                File.Delete(Util.Global.app_DERCSS_file);
        }

        /// <summary>
        /// Método que preenche o arquivo css
        /// </summary>
        /// <param name="builder">builder do css</param>
        private void PreencheArquivoCSS(ref StringBuilder builder)
        {
            Util.CL_Files.WriteOnTheLog("Document.PreencheArquivoCSS()", Util.Global.TipoLog.DETALHADO);

            builder.AppendLine("/* --------------------------------------- */");
            builder.AppendLine("");
            builder.AppendLine("/* --------------- GENERAL --------------- */");
            builder.AppendLine("");
            builder.AppendLine("/* --------------------------------------- */");
            builder.AppendLine("");
            builder.AppendLine("");
            builder.AppendLine("");
            builder.AppendLine("body {");
            builder.AppendLine("");
            builder.AppendLine("	color: #000000;");
            builder.AppendLine("");
            builder.AppendLine("	font-family : Verdana, Geneva, Arial, Helvetica, sans-serif;");
            builder.AppendLine("");
            builder.AppendLine("	font-size : 10px ;");
            builder.AppendLine("");
            builder.AppendLine("	background-color : #333333;");
            builder.AppendLine("");
            builder.AppendLine("	visibility : visible;");
            builder.AppendLine("");
            builder.AppendLine("}");
            builder.AppendLine("");
            builder.AppendLine("");
            builder.AppendLine("");
            builder.AppendLine(".topPage {");
            builder.AppendLine("");
            builder.AppendLine("	margin: 0px;");
            builder.AppendLine("");
            builder.AppendLine("	padding: 0px;");
            builder.AppendLine("");
            builder.AppendLine("}");
            builder.AppendLine("");
            builder.AppendLine("");
            builder.AppendLine("");
            builder.AppendLine(".rightPage {");
            builder.AppendLine("");
            builder.AppendLine("	margin: 20px;");
            builder.AppendLine("");
            builder.AppendLine("}");
            builder.AppendLine("");
            builder.AppendLine("");
            builder.AppendLine("");
            builder.AppendLine(".leftPage {");
            builder.AppendLine("");
            builder.AppendLine("	/*background: #EDF2F2; /*onde ficam o nome das tabelas*/");
            builder.AppendLine("background: #333333;");
            builder.AppendLine("}");
            builder.AppendLine("");
            builder.AppendLine("");
            builder.AppendLine("");
            builder.AppendLine("/* --------------------------------------- */");
            builder.AppendLine("");
            builder.AppendLine("/* -------------- TOP FRAME -------------- */");
            builder.AppendLine("");
            builder.AppendLine("/* --------------------------------------- */");
            builder.AppendLine("");
            builder.AppendLine("");
            builder.AppendLine("");
            builder.AppendLine(".title { /* Cabeçalho Master */");
            builder.AppendLine("");
            builder.AppendLine("	font-family : Verdana, Geneva, Arial, Helvetica, sans-serif;");
            builder.AppendLine("");
            builder.AppendLine("	margin: auto;");
            builder.AppendLine("");
            builder.AppendLine("	padding-bottom : 0px;");
            builder.AppendLine("");
            builder.AppendLine("	padding-left : 0pt;");
            builder.AppendLine("");
            builder.AppendLine("	padding-right : 0px;");
            builder.AppendLine("");
            builder.AppendLine("	padding-top : 0px ;");
            builder.AppendLine("");
            builder.AppendLine("	line-height: 54px;");
            builder.AppendLine("");
            builder.AppendLine("	height : 54px;");
            builder.AppendLine("");
            builder.AppendLine("	width : 100%;");
            builder.AppendLine("");
            builder.AppendLine("	/* background : url(Img/verde.gif) left bottom; */");
            builder.AppendLine("");
            builder.AppendLine("	vertical-align : middle;");
            builder.AppendLine("");
            builder.AppendLine("	visibility : visible;");
            builder.AppendLine("");
            builder.AppendLine("}");
            builder.AppendLine("");
            builder.AppendLine("");
            builder.AppendLine("");
            builder.AppendLine(".title1  {");
            builder.AppendLine("");
            builder.AppendLine("	color: #FFFFFF;");
            builder.AppendLine("");
            builder.AppendLine("	font-family : Verdana, Geneva, Arial, Helvetica, sans-serif;");
            builder.AppendLine("");
            builder.AppendLine("	font-size : 20pt;");
            builder.AppendLine("");
            builder.AppendLine("	font-weight : bold;");
            builder.AppendLine("");
            builder.AppendLine("	margin: auto;");
            builder.AppendLine("");
            builder.AppendLine("	padding-left : 10px;");
            builder.AppendLine("");
            builder.AppendLine("	padding-right : 0px;");
            builder.AppendLine("");
            builder.AppendLine("	line-height : 54px;");
            builder.AppendLine("");
            builder.AppendLine("	display : block;");
            builder.AppendLine("");
            builder.AppendLine("	float: left;");
            builder.AppendLine("");
            builder.AppendLine("	vertical-align : middle;");
            builder.AppendLine("");
            builder.AppendLine("	visibility : visible;");
            builder.AppendLine("");
            builder.AppendLine("}");
            builder.AppendLine("");
            builder.AppendLine("");
            builder.AppendLine("");
            builder.AppendLine(".separator1  {");
            builder.AppendLine("");
            builder.AppendLine("	color: #FFFFFF;");
            builder.AppendLine("");
            builder.AppendLine("	font-family : Verdana, Geneva, Arial, Helvetica, sans-serif;");
            builder.AppendLine("");
            builder.AppendLine("	font-size : 5pt;");
            builder.AppendLine("");
            builder.AppendLine("	display : block;");
            builder.AppendLine("");
            builder.AppendLine("	line-height: 4px;");
            builder.AppendLine("");
            builder.AppendLine("	height: 4px;");
            builder.AppendLine("");
            builder.AppendLine("	float: left;");
            builder.AppendLine("");
            builder.AppendLine("	width: 10pt;");
            builder.AppendLine("");
            builder.AppendLine("	vertical-align : middle;");
            builder.AppendLine("");
            builder.AppendLine("	visibility : visible;");
            builder.AppendLine("");
            builder.AppendLine("	background: #FFFFFF;");
            builder.AppendLine("");
            builder.AppendLine("	position: relative;");
            builder.AppendLine("");
            builder.AppendLine("	top: 28px;");
            builder.AppendLine("");
            builder.AppendLine("	left: 6px;");
            builder.AppendLine("");
            builder.AppendLine("	margin-left: 6px;");
            builder.AppendLine("");
            builder.AppendLine("	margin-right: 6px;");
            builder.AppendLine("");
            builder.AppendLine("}");
            builder.AppendLine("");
            builder.AppendLine("");
            builder.AppendLine("");
            builder.AppendLine(".title2  {");
            builder.AppendLine("");
            builder.AppendLine("	color: #FFFFFF;");
            builder.AppendLine("");
            builder.AppendLine("	font-family : Verdana, Geneva, Arial, Helvetica, sans-serif;");
            builder.AppendLine("");
            builder.AppendLine("	font-size : 20pt;");
            builder.AppendLine("");
            builder.AppendLine("	font-weight : normal;");
            builder.AppendLine("");
            builder.AppendLine("	padding-bottom : 0px;");
            builder.AppendLine("");
            builder.AppendLine("	padding-left : 10px;");
            builder.AppendLine("");
            builder.AppendLine("	padding-right : 0px;");
            builder.AppendLine("");
            builder.AppendLine("	padding-top : 0px;");
            builder.AppendLine("");
            builder.AppendLine("	line-height : 54px;");
            builder.AppendLine("");
            builder.AppendLine("	display : block;");
            builder.AppendLine("");
            builder.AppendLine("	float: left;");
            builder.AppendLine("");
            builder.AppendLine("	vertical-align : middle;");
            builder.AppendLine("");
            builder.AppendLine("	visibility : visible;");
            builder.AppendLine("");
            builder.AppendLine("}");
            builder.AppendLine("");
            builder.AppendLine("");
            builder.AppendLine("");
            builder.AppendLine(".separator2 {");
            builder.AppendLine("");
            builder.AppendLine("	color: #FFFFFF;");
            builder.AppendLine("");
            builder.AppendLine("	font-family : Verdana, Geneva, Arial, Helvetica, sans-serif;");
            builder.AppendLine("");
            builder.AppendLine("	font-size : 20pt;");
            builder.AppendLine("");
            builder.AppendLine("	line-height : 54px;");
            builder.AppendLine("");
            builder.AppendLine("	display : block;");
            builder.AppendLine("");
            builder.AppendLine("	float: left;");
            builder.AppendLine("");
            builder.AppendLine("	width: 20pt;");
            builder.AppendLine("");
            builder.AppendLine("	vertical-align : middle;");
            builder.AppendLine("");
            builder.AppendLine("	visibility : visible;");
            builder.AppendLine("");
            builder.AppendLine("}");
            builder.AppendLine("");
            builder.AppendLine("");
            builder.AppendLine("");
            builder.AppendLine(".titleimage{");
            builder.AppendLine("");
            builder.AppendLine("	color: #FFFFFF;");
            builder.AppendLine("");
            builder.AppendLine("	text-align : right;");
            builder.AppendLine("");
            builder.AppendLine("	padding-right : 10px;");
            builder.AppendLine("");
            builder.AppendLine("	visibility : visible;");
            builder.AppendLine("");
            builder.AppendLine("	position: relative;");
            builder.AppendLine("");
            builder.AppendLine("	top: 6pt;");
            builder.AppendLine("");
            builder.AppendLine("	line-height: 20px;");
            builder.AppendLine("");
            builder.AppendLine("}");
            builder.AppendLine("");
            builder.AppendLine("");
            builder.AppendLine("");
            builder.AppendLine("");
            builder.AppendLine("");
            builder.AppendLine(".topMenu {");
            builder.AppendLine("");
            builder.AppendLine("margin: 0pt;");
            builder.AppendLine("");
            builder.AppendLine("padding: 0pt;");
            builder.AppendLine("");
            builder.AppendLine("display: block;");
            builder.AppendLine("");
            builder.AppendLine("width: 100%;");
            builder.AppendLine("");
            builder.AppendLine("line-height: 20px;");
            builder.AppendLine("");
            builder.AppendLine("border-top: 1px solid #33415E;");
            builder.AppendLine("");
            builder.AppendLine("border-bottom: 1px solid #33415E;");
            builder.AppendLine("");
            builder.AppendLine("}");
            builder.AppendLine("");
            builder.AppendLine("");
            builder.AppendLine("");
            builder.AppendLine(".topMenu a  {");
            builder.AppendLine("");
            builder.AppendLine("color: white;");
            builder.AppendLine("");
            builder.AppendLine("padding-left: 6pt;");
            builder.AppendLine("");
            builder.AppendLine("padding-right: 6pt;");
            builder.AppendLine("");
            builder.AppendLine("font-size: 8pt;");
            builder.AppendLine("");
            builder.AppendLine("text-decoration: none;");
            builder.AppendLine("");
            builder.AppendLine("display: block;");
            builder.AppendLine("");
            builder.AppendLine("border-right: 1px solid #33415E;");
            builder.AppendLine("");
            builder.AppendLine("line-height: 20px;");
            builder.AppendLine("");
            builder.AppendLine("white-space: nowrap;");
            builder.AppendLine("");
            builder.AppendLine("height: 20px;");
            builder.AppendLine("");
            builder.AppendLine("vertical-align: middle;");
            builder.AppendLine("");
            builder.AppendLine("}");
            builder.AppendLine("");
            builder.AppendLine("");
            builder.AppendLine("");
            builder.AppendLine(".topMenu a:hover {");
            builder.AppendLine("");
            builder.AppendLine("background: #429c45;");
            builder.AppendLine("");
            builder.AppendLine("display: block;");
            builder.AppendLine("");
            builder.AppendLine("line-height: 20px;");
            builder.AppendLine("");
            builder.AppendLine("height: 20px;");
            builder.AppendLine("");
            builder.AppendLine("border-right: 1px solid #429c45;");
            builder.AppendLine("");
            builder.AppendLine("}");
            builder.AppendLine("");
            builder.AppendLine("");
            builder.AppendLine("");
            builder.AppendLine(".selected {");
            builder.AppendLine("");
            builder.AppendLine("color: white;");
            builder.AppendLine("");
            builder.AppendLine("padding-left: 6pt;");
            builder.AppendLine("");
            builder.AppendLine("padding-right: 6pt;");
            builder.AppendLine("");
            builder.AppendLine("margin: 0pt;");
            builder.AppendLine("");
            builder.AppendLine("font-size: 8pt;");
            builder.AppendLine("");
            builder.AppendLine("display: block;");
            builder.AppendLine("");
            builder.AppendLine("line-height: 20px;");
            builder.AppendLine("");
            builder.AppendLine("background : #5B1D05;");
            builder.AppendLine("");
            builder.AppendLine("white-space: nowrap;");
            builder.AppendLine("");
            builder.AppendLine("border-right: 1px solid #33415E;");
            builder.AppendLine("");
            builder.AppendLine("}");
            builder.AppendLine("");
            builder.AppendLine("");
            builder.AppendLine("");
            builder.AppendLine("/* --------------------------------------- */");
            builder.AppendLine("");
            builder.AppendLine("/* -------------- LEFT FRAME ------------- */");
            builder.AppendLine("");
            builder.AppendLine("/* --------------------------------------- */");
            builder.AppendLine("");
            builder.AppendLine("");
            builder.AppendLine("");
            builder.AppendLine(".bookmark {");
            builder.AppendLine("");
            builder.AppendLine("	font-family : Verdana, Geneva, Arial, Helvetica, sans-serif;");
            builder.AppendLine("");
            builder.AppendLine("	font-size : 8pt;");
            builder.AppendLine("");
            builder.AppendLine("	font-weight : normal;");
            builder.AppendLine("");
            builder.AppendLine("	text-align : left;");
            builder.AppendLine("");
            builder.AppendLine("	padding-top : 14px;");
            builder.AppendLine("");
            builder.AppendLine("	width : 100%;");
            builder.AppendLine("");
            builder.AppendLine("	visibility : visible;");
            builder.AppendLine("");
            builder.AppendLine("");
            builder.AppendLine("}");
            builder.AppendLine("");
            builder.AppendLine("");
            builder.AppendLine("");
            builder.AppendLine(".bookmark a {");
            builder.AppendLine("");
            builder.AppendLine("	color: #FFF;");
            builder.AppendLine("");
            builder.AppendLine("	font-family : Verdana, Geneva, Arial, Helvetica, sans-serif;");
            builder.AppendLine("");
            builder.AppendLine("	font-size : 8pt;");
            builder.AppendLine("");
            builder.AppendLine("	font-weight : normal;");
            builder.AppendLine("");
            builder.AppendLine("	text-align : left;");
            builder.AppendLine("");
            builder.AppendLine("	text-decoration : none;");
            builder.AppendLine("");
            builder.AppendLine("	padding-left : 17px;");
            builder.AppendLine("");
            builder.AppendLine("	padding-right : 2px;");
            builder.AppendLine("");
            builder.AppendLine("	display : block;");
            builder.AppendLine("");
            builder.AppendLine("	padding-top: 2px;");
            builder.AppendLine("");
            builder.AppendLine("	padding-bottom: 2px;");
            builder.AppendLine("");
            builder.AppendLine("	width: 100%;");
            builder.AppendLine("");
            builder.AppendLine("	line-height: 12px;");
            builder.AppendLine("");
            builder.AppendLine("	height: 12px;");
            builder.AppendLine("");
            builder.AppendLine("	visibility : visible;");
            builder.AppendLine("");
            builder.AppendLine("}");
            builder.AppendLine("");
            builder.AppendLine("");
            builder.AppendLine("");
            builder.AppendLine("");
            builder.AppendLine("");
            builder.AppendLine(".bookmark a:link {}");
            builder.AppendLine("");
            builder.AppendLine(".bookmark a:visited {}");
            builder.AppendLine("");
            builder.AppendLine(".bookmark a:active {}");
            builder.AppendLine("");
            builder.AppendLine(".bookmark a:hover {font-weight: bold; color: rgb(232,80,12); background: #ffffff;}");
            builder.AppendLine("");
            builder.AppendLine("");
            builder.AppendLine("");
            builder.AppendLine("");
            builder.AppendLine("");
            builder.AppendLine("/* --------------------------------------- */");
            builder.AppendLine("");
            builder.AppendLine("/* -------------- MAIN FRAME ------------- */");
            builder.AppendLine("");
            builder.AppendLine("/* --------------------------------------- */");
            builder.AppendLine("");
            builder.AppendLine("");
            builder.AppendLine("");
            builder.AppendLine(".caption1 {");
            builder.AppendLine("");
            builder.AppendLine("	color:rgb(232,80,12);");
            builder.AppendLine("");
            builder.AppendLine("	font-family : Verdana, Geneva, Arial, Helvetica, sans-serif;");
            builder.AppendLine("");
            builder.AppendLine("	font-size : 14pt;");
            builder.AppendLine("");
            builder.AppendLine("	font-weight : normal;");
            builder.AppendLine("");
            builder.AppendLine("	text-align : left;");
            builder.AppendLine("");
            builder.AppendLine("	margin-top : 14px;");
            builder.AppendLine("");
            builder.AppendLine("	padding-bottom : 1px;");
            builder.AppendLine("");
            builder.AppendLine("	padding-top : 1px;");
            builder.AppendLine("");
            builder.AppendLine("	line-height : 20px;");
            builder.AppendLine("");
            builder.AppendLine("	visibility : visible;");
            builder.AppendLine("");
            builder.AppendLine("}");
            builder.AppendLine("");
            builder.AppendLine("");
            builder.AppendLine("");
            builder.AppendLine(".width770 {");
            builder.AppendLine("");
            builder.AppendLine("width: 770px;");
            builder.AppendLine("");
            builder.AppendLine("}");
            builder.AppendLine("");
            builder.AppendLine("");
            builder.AppendLine("");
            builder.AppendLine(".caption2 {");
            builder.AppendLine("");
            builder.AppendLine("	color: #FFFFFF;");
            builder.AppendLine("");
            builder.AppendLine("	font-family : Verdana, Geneva, Arial, Helvetica, sans-serif;");
            builder.AppendLine("");
            builder.AppendLine("	font-size : 10pt;");
            builder.AppendLine("");
            builder.AppendLine("	font-weight : bold;");
            builder.AppendLine("");
            builder.AppendLine("	margin-bottom : 5px;");
            builder.AppendLine("");
            builder.AppendLine("	margin-left : 0px;");
            builder.AppendLine("");
            builder.AppendLine("	margin-right : 0px;");
            builder.AppendLine("");
            builder.AppendLine("	margin-top : 15px;");
            builder.AppendLine("");
            builder.AppendLine("	padding-bottom : 5px;");
            builder.AppendLine("");
            builder.AppendLine("	padding-left : 25px;");
            builder.AppendLine("");
            builder.AppendLine("	padding-right : 0px;");
            builder.AppendLine("");
            builder.AppendLine("	padding-top : 5px;");
            builder.AppendLine("");
            builder.AppendLine("	visibility : visible;");
            builder.AppendLine("");
            builder.AppendLine("	background: url(Img/arrow.gif) no-repeat left;");
            builder.AppendLine("");
            builder.AppendLine("}");
            builder.AppendLine("");
            builder.AppendLine("");
            builder.AppendLine("");
            builder.AppendLine(".tabformat {");
            builder.AppendLine("	border:1px solid #CCCCCC;");
            builder.AppendLine("	background-color: #333333;");
            builder.AppendLine("	color: #FFFFFF;");
            builder.AppendLine("}");
            builder.AppendLine("");
            builder.AppendLine("");
            builder.AppendLine("");
            builder.AppendLine(".tabhead { /*  */");
            builder.AppendLine("");
            builder.AppendLine("	color: #FFFFFF;");
            builder.AppendLine("");
            builder.AppendLine("	font-family : Verdana, Geneva, Arial, Helvetica, sans-serif;");
            builder.AppendLine("");
            builder.AppendLine("	font-size : 8pt;");
            builder.AppendLine("");
            builder.AppendLine("	font-weight : normal;");
            builder.AppendLine("");
            builder.AppendLine("	text-align : left;");
            builder.AppendLine("");
            builder.AppendLine("	border: 0pt solid #CCCCCC;");
            builder.AppendLine("");
            builder.AppendLine("	padding-bottom : 3px;");
            builder.AppendLine("");
            builder.AppendLine("	padding-left : 10px;");
            builder.AppendLine("");
            builder.AppendLine("	padding-right : 2px;");
            builder.AppendLine("");
            builder.AppendLine("	padding-top : 3px;");
            builder.AppendLine("");
            builder.AppendLine("	background-color : rgb(0,0,255); ");
            builder.AppendLine("");
            builder.AppendLine("	visibility : visible;");
            builder.AppendLine("");
            builder.AppendLine("}");
            builder.AppendLine("");
            builder.AppendLine("");
            builder.AppendLine("");
            builder.AppendLine(".tabdata {");
            builder.AppendLine("");
            builder.AppendLine("	font-family : Verdana, Geneva, Arial, Helvetica, sans-serif;");
            builder.AppendLine("");
            builder.AppendLine("	font-size : 8pt;");
            builder.AppendLine("");
            builder.AppendLine("	font-weight : normal;");
            builder.AppendLine("");
            builder.AppendLine("	text-align : left;");
            builder.AppendLine("");
            builder.AppendLine("	border: 1px solid #CCCCCC;");
            builder.AppendLine("");
            builder.AppendLine("	padding-bottom : 3px;");
            builder.AppendLine("");
            builder.AppendLine("	padding-left : 10px;");
            builder.AppendLine("");
            builder.AppendLine("	padding-right : 2px;");
            builder.AppendLine("");
            builder.AppendLine("	padding-top : 3px;");
            builder.AppendLine("");
            builder.AppendLine("	visibility : visible;");
            builder.AppendLine("");
            builder.AppendLine("}");
            builder.AppendLine("");
            builder.AppendLine("");
            builder.AppendLine("");
            builder.AppendLine("/* --------------------------------------- */");
            builder.AppendLine("");
            builder.AppendLine("/* -------------- LEFT TOP FRAME --------- */");
            builder.AppendLine("");
            builder.AppendLine("/* --------------------------------------- */");
            builder.AppendLine("");
            builder.AppendLine("");
            builder.AppendLine("");
            builder.AppendLine(".titleLeft { /*  */");
            builder.AppendLine("");
            builder.AppendLine("	font-family : Verdana, Geneva, Arial, Helvetica, sans-serif;");
            builder.AppendLine("");
            builder.AppendLine("	margin: auto;");
            builder.AppendLine("");
            builder.AppendLine("	padding-bottom : 0px;");
            builder.AppendLine("");
            builder.AppendLine("	padding-left : 0pt;");
            builder.AppendLine("");
            builder.AppendLine("	padding-right : 0px;");
            builder.AppendLine("");
            builder.AppendLine("	padding-top : 0px ;");
            builder.AppendLine("");
            builder.AppendLine("	line-height: 32px;");
            builder.AppendLine("");
            builder.AppendLine("	width : 100%;");
            builder.AppendLine("");
            builder.AppendLine("	background-color : rgb(0,0,255);");
            builder.AppendLine("");
            builder.AppendLine("	vertical-align : middle;");
            builder.AppendLine("");
            builder.AppendLine("	visibility : visible;");
            builder.AppendLine("");
            builder.AppendLine("	color: white;");
            builder.AppendLine("");
            builder.AppendLine("}");
            builder.AppendLine("");
            builder.AppendLine("");
            builder.AppendLine("");
            builder.AppendLine(".titleLeft1 {");
            builder.AppendLine("");
            builder.AppendLine("color: white;");
            builder.AppendLine("");
            builder.AppendLine("font-size: 12pt;");
            builder.AppendLine("");
            builder.AppendLine("text-align: center;");
            builder.AppendLine("");
            builder.AppendLine("padding-top: 6px;");
            builder.AppendLine("");
            builder.AppendLine("height: 32px;");
            builder.AppendLine("");
            builder.AppendLine("}");
            builder.AppendLine("");
            builder.AppendLine("");
            builder.AppendLine("");
            builder.AppendLine(".titleLeft2 {");
            builder.AppendLine("");
            builder.AppendLine("color: white;");
            builder.AppendLine("");
            builder.AppendLine("font-size: 10pt;");
            builder.AppendLine("");
            builder.AppendLine("text-align: center;");
            builder.AppendLine("");
            builder.AppendLine("display: none;");
            builder.AppendLine("");
            builder.AppendLine("}");
            builder.AppendLine("");
            builder.AppendLine("");
            builder.AppendLine("");
            builder.AppendLine(".topLeft {");
            builder.AppendLine("");
            builder.AppendLine("background: #333333;");
            builder.AppendLine("");
            builder.AppendLine("height: 100%;");
            builder.AppendLine("");
            builder.AppendLine("}");
            builder.AppendLine("");
            builder.AppendLine("");
            builder.AppendLine("");
            builder.AppendLine(".topMenuLeft {");
            builder.AppendLine("");
            builder.AppendLine("");
            builder.AppendLine("");
            builder.AppendLine("}");
            builder.AppendLine("");
            builder.AppendLine("");
            builder.AppendLine("");
            builder.AppendLine("");
            builder.AppendLine("");
            builder.AppendLine("");
            builder.AppendLine("");
            builder.AppendLine(".topMenuLeft a  {");
            builder.AppendLine("");
            builder.AppendLine("color: #999999;");
            builder.AppendLine("");
            builder.AppendLine("padding-left: 6pt;");
            builder.AppendLine("");
            builder.AppendLine("padding-right: 6pt;");
            builder.AppendLine("");
            builder.AppendLine("background: #333333;");
            builder.AppendLine("");
            builder.AppendLine("font-size: 8pt;");
            builder.AppendLine("");
            builder.AppendLine("text-decoration: none;");
            builder.AppendLine("");
            builder.AppendLine("display: block;");
            builder.AppendLine("");
            builder.AppendLine("border-top: 1px solid #595959;");
            builder.AppendLine("");
            builder.AppendLine("border-left: 1px solid #595959;");
            builder.AppendLine("");
            builder.AppendLine("border-right: 1px solid #000000;");
            builder.AppendLine("");
            builder.AppendLine("border-bottom: 1px solid #000000;");
            builder.AppendLine("");
            builder.AppendLine("line-height: 20px;");
            builder.AppendLine("");
            builder.AppendLine("white-space: nowrap;");
            builder.AppendLine("");
            builder.AppendLine("height: 20px;");
            builder.AppendLine("");
            builder.AppendLine("vertical-align: middle;");
            builder.AppendLine("");
            builder.AppendLine("}");
            builder.AppendLine("");
            builder.AppendLine("");
            builder.AppendLine("");
            builder.AppendLine(".topMenuLeft a:hover {");
            builder.AppendLine("");
            builder.AppendLine("color: white;");
            builder.AppendLine("");
            builder.AppendLine("}");
            builder.AppendLine("");
            builder.AppendLine("");
            builder.AppendLine("");
            builder.AppendLine(".selectedLeft {");
            builder.AppendLine("");
            builder.AppendLine("color: #ffffff;");
            builder.AppendLine("");
            builder.AppendLine("padding-left: 15px;");
            builder.AppendLine("");
            builder.AppendLine("font-size: 8pt;");
            builder.AppendLine("");
            builder.AppendLine("line-height: 20px;");
            builder.AppendLine("");
            builder.AppendLine("width: 185px;");
            builder.AppendLine("");
            builder.AppendLine("display: block;");
            builder.AppendLine("");
            builder.AppendLine("vertical-align: middle;");
            builder.AppendLine("");
            builder.AppendLine("text-decoration: none;");
            builder.AppendLine("");
            builder.AppendLine("border-top: 1px solid #595959;");
            builder.AppendLine("");
            builder.AppendLine("border-bottom: 1px solid #000000;");
            builder.AppendLine("");
            builder.AppendLine("}");
            builder.AppendLine("");
            builder.AppendLine("");
            builder.AppendLine("");
            builder.AppendLine(".leftPageLeft {");
            builder.AppendLine("");
            builder.AppendLine("	background: #EDF2F2;");
            builder.AppendLine("");
            builder.AppendLine("}");
            builder.AppendLine("");
            builder.AppendLine("");
            builder.AppendLine("");
            builder.AppendLine("");
            builder.AppendLine("");
        }

        #endregion Arquivo CSS

        #region Imagens

        /// <summary>
        /// Método que copia os arquivos para a pasta do DER
        /// </summary>
        private void CopiarIMG()
        {
            Util.CL_Files.WriteOnTheLog("Document.CopiarIMG", Global.TipoLog.SIMPLES);

            if (Directory.Exists(Util.Global.app_Img_directory))
            {
                if (Directory.Exists(Util.Global.app_DER_directory + "Img\\"))
                    Directory.Delete(Util.Global.app_DER_directory + "Img\\", true);

                Directory.CreateDirectory(Util.Global.app_DER_directory + "Img\\");

                foreach (string file in Directory.GetFiles(Util.Global.app_Img_directory))
                {
                    FileInfo ff = new FileInfo(file);
                    File.Copy(file, Util.Global.app_DER_directory + "Img\\" + ff.Name);
                }
            }
        }

        #endregion Imagens
    }
}
