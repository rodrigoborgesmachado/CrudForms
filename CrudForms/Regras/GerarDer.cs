using System;
using System.Collections.Generic;
using System.Data.Common;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

            builder.Append("/* --------------------------------------- */" + Environment.NewLine);
            builder.Append("/* --------------- GENERAL --------------- */" + Environment.NewLine);
            builder.Append("/* --------------------------------------- */" + Environment.NewLine);
            builder.Append("" + Environment.NewLine);
            builder.Append("body {" + Environment.NewLine);
            builder.Append("	color: #000000;" + Environment.NewLine);
            builder.Append("	font-family : Verdana, Geneva, Arial, Helvetica, sans-serif;" + Environment.NewLine);
            builder.Append("	font-size : 10px ;" + Environment.NewLine);
            builder.Append("	background : #FFFFFF;" + Environment.NewLine);
            builder.Append("	visibility : visible;" + Environment.NewLine);
            builder.Append("}" + Environment.NewLine);
            builder.Append("" + Environment.NewLine);
            builder.Append(".topPage {" + Environment.NewLine);
            builder.Append("	margin: 0px;" + Environment.NewLine);
            builder.Append("	padding: 0px;" + Environment.NewLine);
            builder.Append("background: url(Img/fBg2.png) ;" + Environment.NewLine);
            builder.Append("}" + Environment.NewLine);
            builder.Append("" + Environment.NewLine);
            builder.Append(".rightPage {" + Environment.NewLine);
            builder.Append("	margin: 20px;" + Environment.NewLine);
            builder.Append("}" + Environment.NewLine);
            builder.Append("" + Environment.NewLine);
            builder.Append(".leftPage {" + Environment.NewLine);
            builder.Append("	background: #EDF2F2; /*onde ficam o nome das tabelas*/" + Environment.NewLine);
            builder.Append("}" + Environment.NewLine);
            builder.Append("" + Environment.NewLine);
            builder.Append("/* --------------------------------------- */" + Environment.NewLine);
            builder.Append("/* -------------- TOP FRAME -------------- */" + Environment.NewLine);
            builder.Append("/* --------------------------------------- */" + Environment.NewLine);
            builder.Append("" + Environment.NewLine);
            builder.Append(".title { /* Cabeçalho Master */" + Environment.NewLine);
            builder.Append("	font-family : Verdana, Geneva, Arial, Helvetica, sans-serif;" + Environment.NewLine);
            builder.Append("	margin: auto;" + Environment.NewLine);
            builder.Append("	padding-bottom : 0px;" + Environment.NewLine);
            builder.Append("	padding-left : 0pt;" + Environment.NewLine);
            builder.Append("	padding-right : 0px;" + Environment.NewLine);
            builder.Append("	padding-top : 0px ;" + Environment.NewLine);
            builder.Append("	line-height: 54px;" + Environment.NewLine);
            builder.Append("	height : 54px;" + Environment.NewLine);
            builder.Append("	width : 100%;" + Environment.NewLine);
            builder.Append("	/* background : url(Img/verde.gif) left bottom; */" + Environment.NewLine);
            builder.Append("	vertical-align : middle;" + Environment.NewLine);
            builder.Append("	visibility : visible;" + Environment.NewLine);
            builder.Append("}" + Environment.NewLine);
            builder.Append("" + Environment.NewLine);
            builder.Append(".title1  {" + Environment.NewLine);
            builder.Append("	color: #FFFFFF;" + Environment.NewLine);
            builder.Append("	font-family : Verdana, Geneva, Arial, Helvetica, sans-serif;" + Environment.NewLine);
            builder.Append("	font-size : 20pt;" + Environment.NewLine);
            builder.Append("	font-weight : bold;" + Environment.NewLine);
            builder.Append("	margin: auto;" + Environment.NewLine);
            builder.Append("	padding-left : 10px;" + Environment.NewLine);
            builder.Append("	padding-right : 0px;" + Environment.NewLine);
            builder.Append("	line-height : 54px;" + Environment.NewLine);
            builder.Append("	display : block;" + Environment.NewLine);
            builder.Append("	float: left;" + Environment.NewLine);
            builder.Append("	vertical-align : middle;" + Environment.NewLine);
            builder.Append("	visibility : visible;" + Environment.NewLine);
            builder.Append("}" + Environment.NewLine);
            builder.Append("" + Environment.NewLine);
            builder.Append(".separator1  {" + Environment.NewLine);
            builder.Append("	color: #FFFFFF;" + Environment.NewLine);
            builder.Append("	font-family : Verdana, Geneva, Arial, Helvetica, sans-serif;" + Environment.NewLine);
            builder.Append("	font-size : 5pt;" + Environment.NewLine);
            builder.Append("	display : block;" + Environment.NewLine);
            builder.Append("	line-height: 4px;" + Environment.NewLine);
            builder.Append("	height: 4px;" + Environment.NewLine);
            builder.Append("	float: left;" + Environment.NewLine);
            builder.Append("	width: 10pt;" + Environment.NewLine);
            builder.Append("	vertical-align : middle;" + Environment.NewLine);
            builder.Append("	visibility : visible;" + Environment.NewLine);
            builder.Append("	background: #FFFFFF;" + Environment.NewLine);
            builder.Append("	position: relative;" + Environment.NewLine);
            builder.Append("	top: 28px;" + Environment.NewLine);
            builder.Append("	left: 6px;" + Environment.NewLine);
            builder.Append("	margin-left: 6px;" + Environment.NewLine);
            builder.Append("	margin-right: 6px;" + Environment.NewLine);
            builder.Append("}" + Environment.NewLine);
            builder.Append("" + Environment.NewLine);
            builder.Append(".title2  {" + Environment.NewLine);
            builder.Append("	color: #FFFFFF;" + Environment.NewLine);
            builder.Append("	font-family : Verdana, Geneva, Arial, Helvetica, sans-serif;" + Environment.NewLine);
            builder.Append("	font-size : 20pt;" + Environment.NewLine);
            builder.Append("	font-weight : normal;" + Environment.NewLine);
            builder.Append("	padding-bottom : 0px;" + Environment.NewLine);
            builder.Append("	padding-left : 10px;" + Environment.NewLine);
            builder.Append("	padding-right : 0px;" + Environment.NewLine);
            builder.Append("	padding-top : 0px;" + Environment.NewLine);
            builder.Append("	line-height : 54px;" + Environment.NewLine);
            builder.Append("	display : block;" + Environment.NewLine);
            builder.Append("	float: left;" + Environment.NewLine);
            builder.Append("	vertical-align : middle;" + Environment.NewLine);
            builder.Append("	visibility : visible;" + Environment.NewLine);
            builder.Append("}" + Environment.NewLine);
            builder.Append("" + Environment.NewLine);
            builder.Append(".separator2 {" + Environment.NewLine);
            builder.Append("	color: #FFFFFF;" + Environment.NewLine);
            builder.Append("	font-family : Verdana, Geneva, Arial, Helvetica, sans-serif;" + Environment.NewLine);
            builder.Append("	font-size : 20pt;" + Environment.NewLine);
            builder.Append("	line-height : 54px;" + Environment.NewLine);
            builder.Append("	display : block;" + Environment.NewLine);
            builder.Append("	float: left;" + Environment.NewLine);
            builder.Append("	width: 20pt;" + Environment.NewLine);
            builder.Append("	vertical-align : middle;" + Environment.NewLine);
            builder.Append("	visibility : visible;" + Environment.NewLine);
            builder.Append("}" + Environment.NewLine);
            builder.Append("" + Environment.NewLine);
            builder.Append(".titleimage{" + Environment.NewLine);
            builder.Append("	color: #FFFFFF;" + Environment.NewLine);
            builder.Append("	text-align : right;" + Environment.NewLine);
            builder.Append("	padding-right : 10px;" + Environment.NewLine);
            builder.Append("	visibility : visible;" + Environment.NewLine);
            builder.Append("	position: relative;" + Environment.NewLine);
            builder.Append("	top: 6pt;" + Environment.NewLine);
            builder.Append("	line-height: 20px;" + Environment.NewLine);
            builder.Append("}" + Environment.NewLine);
            builder.Append("" + Environment.NewLine);
            builder.Append("" + Environment.NewLine);
            builder.Append(".topMenu {" + Environment.NewLine);
            builder.Append("margin: 0pt;" + Environment.NewLine);
            builder.Append("padding: 0pt;" + Environment.NewLine);
            builder.Append("display: block;" + Environment.NewLine);
            builder.Append("width: 100%;" + Environment.NewLine);
            builder.Append("line-height: 20px;" + Environment.NewLine);
            builder.Append("border-top: 1px solid #33415E;" + Environment.NewLine);
            builder.Append("border-bottom: 1px solid #33415E;" + Environment.NewLine);
            builder.Append("}" + Environment.NewLine);
            builder.Append("" + Environment.NewLine);
            builder.Append(".topMenu a  {" + Environment.NewLine);
            builder.Append("color: white;" + Environment.NewLine);
            builder.Append("padding-left: 6pt;" + Environment.NewLine);
            builder.Append("padding-right: 6pt;" + Environment.NewLine);
            builder.Append("font-size: 8pt;" + Environment.NewLine);
            builder.Append("text-decoration: none;" + Environment.NewLine);
            builder.Append("display: block;" + Environment.NewLine);
            builder.Append("border-right: 1px solid #33415E;" + Environment.NewLine);
            builder.Append("line-height: 20px;" + Environment.NewLine);
            builder.Append("white-space: nowrap;" + Environment.NewLine);
            builder.Append("height: 20px;" + Environment.NewLine);
            builder.Append("vertical-align: middle;" + Environment.NewLine);
            builder.Append("}" + Environment.NewLine);
            builder.Append("" + Environment.NewLine);
            builder.Append(".topMenu a:hover {" + Environment.NewLine);
            builder.Append("background: #429c45;" + Environment.NewLine);
            builder.Append("display: block;" + Environment.NewLine);
            builder.Append("line-height: 20px;" + Environment.NewLine);
            builder.Append("height: 20px;" + Environment.NewLine);
            builder.Append("border-right: 1px solid #429c45;" + Environment.NewLine);
            builder.Append("}" + Environment.NewLine);
            builder.Append("" + Environment.NewLine);
            builder.Append(".selected {" + Environment.NewLine);
            builder.Append("color: white;" + Environment.NewLine);
            builder.Append("padding-left: 6pt;" + Environment.NewLine);
            builder.Append("padding-right: 6pt;" + Environment.NewLine);
            builder.Append("margin: 0pt;" + Environment.NewLine);
            builder.Append("font-size: 8pt;" + Environment.NewLine);
            builder.Append("display: block;" + Environment.NewLine);
            builder.Append("line-height: 20px;" + Environment.NewLine);
            builder.Append("background : #5B1D05;" + Environment.NewLine);
            builder.Append("white-space: nowrap;" + Environment.NewLine);
            builder.Append("border-right: 1px solid #33415E;" + Environment.NewLine);
            builder.Append("}" + Environment.NewLine);
            builder.Append("" + Environment.NewLine);
            builder.Append("/* --------------------------------------- */" + Environment.NewLine);
            builder.Append("/* -------------- LEFT FRAME ------------- */" + Environment.NewLine);
            builder.Append("/* --------------------------------------- */" + Environment.NewLine);
            builder.Append("" + Environment.NewLine);
            builder.Append(".bookmark {" + Environment.NewLine);
            builder.Append("	font-family : Verdana, Geneva, Arial, Helvetica, sans-serif;" + Environment.NewLine);
            builder.Append("	font-size : 8pt;" + Environment.NewLine);
            builder.Append("	font-weight : normal;" + Environment.NewLine);
            builder.Append("	text-align : left;" + Environment.NewLine);
            builder.Append("	padding-top : 14px;" + Environment.NewLine);
            builder.Append("	width : 100%;" + Environment.NewLine);
            builder.Append("	visibility : visible;" + Environment.NewLine);
            builder.Append("}" + Environment.NewLine);
            builder.Append("" + Environment.NewLine);
            builder.Append(".bookmark a {" + Environment.NewLine);
            builder.Append("	color: #333333;" + Environment.NewLine);
            builder.Append("	font-family : Verdana, Geneva, Arial, Helvetica, sans-serif;" + Environment.NewLine);
            builder.Append("	font-size : 8pt;" + Environment.NewLine);
            builder.Append("	font-weight : normal;" + Environment.NewLine);
            builder.Append("	text-align : left;" + Environment.NewLine);
            builder.Append("	text-decoration : none;" + Environment.NewLine);
            builder.Append("	padding-left : 17px;" + Environment.NewLine);
            builder.Append("	padding-right : 2px;" + Environment.NewLine);
            builder.Append("	display : block;" + Environment.NewLine);
            builder.Append("	padding-top: 2px;" + Environment.NewLine);
            builder.Append("	padding-bottom: 2px;" + Environment.NewLine);
            builder.Append("	width: 100%;" + Environment.NewLine);
            builder.Append("	line-height: 12px;" + Environment.NewLine);
            builder.Append("	height: 12px;" + Environment.NewLine);
            builder.Append("	visibility : visible;" + Environment.NewLine);
            builder.Append("}" + Environment.NewLine);
            builder.Append("" + Environment.NewLine);
            builder.Append("" + Environment.NewLine);
            builder.Append(".bookmark a:link {}" + Environment.NewLine);
            builder.Append(".bookmark a:visited {}" + Environment.NewLine);
            builder.Append(".bookmark a:active {}" + Environment.NewLine);
            builder.Append(".bookmark a:hover {font-weight: bold; color: rgb(232,80,12); background: #ffffff;}" + Environment.NewLine);
            builder.Append("" + Environment.NewLine);
            builder.Append("" + Environment.NewLine);
            builder.Append("/* --------------------------------------- */" + Environment.NewLine);
            builder.Append("/* -------------- MAIN FRAME ------------- */" + Environment.NewLine);
            builder.Append("/* --------------------------------------- */" + Environment.NewLine);
            builder.Append("" + Environment.NewLine);
            builder.Append(".caption1 {" + Environment.NewLine);
            builder.Append("	color:rgb(232,80,12);" + Environment.NewLine);
            builder.Append("	font-family : Verdana, Geneva, Arial, Helvetica, sans-serif;" + Environment.NewLine);
            builder.Append("	font-size : 14pt;" + Environment.NewLine);
            builder.Append("	font-weight : normal;" + Environment.NewLine);
            builder.Append("	text-align : left;" + Environment.NewLine);
            builder.Append("	margin-top : 14px;" + Environment.NewLine);
            builder.Append("	padding-bottom : 1px;" + Environment.NewLine);
            builder.Append("	padding-top : 1px;" + Environment.NewLine);
            builder.Append("	line-height : 20px;" + Environment.NewLine);
            builder.Append("	visibility : visible;" + Environment.NewLine);
            builder.Append("}" + Environment.NewLine);
            builder.Append("" + Environment.NewLine);
            builder.Append(".width770 {" + Environment.NewLine);
            builder.Append("width: 770px;" + Environment.NewLine);
            builder.Append("}" + Environment.NewLine);
            builder.Append("" + Environment.NewLine);
            builder.Append(".caption2 {" + Environment.NewLine);
            builder.Append("	color: #5B1D05;" + Environment.NewLine);
            builder.Append("	font-family : Verdana, Geneva, Arial, Helvetica, sans-serif;" + Environment.NewLine);
            builder.Append("	font-size : 10pt;" + Environment.NewLine);
            builder.Append("	font-weight : bold;" + Environment.NewLine);
            builder.Append("	margin-bottom : 5px;" + Environment.NewLine);
            builder.Append("	margin-left : 0px;" + Environment.NewLine);
            builder.Append("	margin-right : 0px;" + Environment.NewLine);
            builder.Append("	margin-top : 15px;" + Environment.NewLine);
            builder.Append("	padding-bottom : 5px;" + Environment.NewLine);
            builder.Append("	padding-left : 25px;" + Environment.NewLine);
            builder.Append("	padding-right : 0px;" + Environment.NewLine);
            builder.Append("	padding-top : 5px;" + Environment.NewLine);
            builder.Append("	visibility : visible;" + Environment.NewLine);
            builder.Append("	background: url(Img/arrow.gif) no-repeat left;" + Environment.NewLine);
            builder.Append("}" + Environment.NewLine);
            builder.Append("" + Environment.NewLine);
            builder.Append(".tabformat {border:1px solid #CCCCCC;" + Environment.NewLine);
            builder.Append("}" + Environment.NewLine);
            builder.Append("" + Environment.NewLine);
            builder.Append(".tabhead { /*  */" + Environment.NewLine);
            builder.Append("	color: #FFFFFF;" + Environment.NewLine);
            builder.Append("	font-family : Verdana, Geneva, Arial, Helvetica, sans-serif;" + Environment.NewLine);
            builder.Append("	font-size : 8pt;" + Environment.NewLine);
            builder.Append("	font-weight : normal;" + Environment.NewLine);
            builder.Append("	text-align : left;" + Environment.NewLine);
            builder.Append("	border: 0pt solid #000000;" + Environment.NewLine);
            builder.Append("	padding-bottom : 3px;" + Environment.NewLine);
            builder.Append("	padding-left : 10px;" + Environment.NewLine);
            builder.Append("	padding-right : 2px;" + Environment.NewLine);
            builder.Append("	padding-top : 3px;" + Environment.NewLine);
            builder.Append("	background-color : rgb(0,0,255); " + Environment.NewLine);
            builder.Append("	visibility : visible;" + Environment.NewLine);
            builder.Append("}" + Environment.NewLine);
            builder.Append("" + Environment.NewLine);
            builder.Append(".tabdata {" + Environment.NewLine);
            builder.Append("	color: #333333;" + Environment.NewLine);
            builder.Append("	font-family : Verdana, Geneva, Arial, Helvetica, sans-serif;" + Environment.NewLine);
            builder.Append("	font-size : 8pt;" + Environment.NewLine);
            builder.Append("	font-weight : normal;" + Environment.NewLine);
            builder.Append("	text-align : left;" + Environment.NewLine);
            builder.Append("	border: 0pt solid #000000;" + Environment.NewLine);
            builder.Append("	padding-bottom : 3px;" + Environment.NewLine);
            builder.Append("	padding-left : 10px;" + Environment.NewLine);
            builder.Append("	padding-right : 2px;" + Environment.NewLine);
            builder.Append("	padding-top : 3px;" + Environment.NewLine);
            builder.Append("	background :#EDF2F2;" + Environment.NewLine);
            builder.Append("	visibility : visible;" + Environment.NewLine);
            builder.Append("}" + Environment.NewLine);
            builder.Append("" + Environment.NewLine);
            builder.Append("/* --------------------------------------- */" + Environment.NewLine);
            builder.Append("/* -------------- LEFT TOP FRAME --------- */" + Environment.NewLine);
            builder.Append("/* --------------------------------------- */" + Environment.NewLine);
            builder.Append("" + Environment.NewLine);
            builder.Append(".titleLeft { /*  */" + Environment.NewLine);
            builder.Append("	font-family : Verdana, Geneva, Arial, Helvetica, sans-serif;" + Environment.NewLine);
            builder.Append("	margin: auto;" + Environment.NewLine);
            builder.Append("	padding-bottom : 0px;" + Environment.NewLine);
            builder.Append("	padding-left : 0pt;" + Environment.NewLine);
            builder.Append("	padding-right : 0px;" + Environment.NewLine);
            builder.Append("	padding-top : 0px ;" + Environment.NewLine);
            builder.Append("	line-height: 32px;" + Environment.NewLine);
            builder.Append("	width : 100%;" + Environment.NewLine);
            builder.Append("	background-color : rgb(0,0,255);" + Environment.NewLine);
            builder.Append("	vertical-align : middle;" + Environment.NewLine);
            builder.Append("	visibility : visible;" + Environment.NewLine);
            builder.Append("	color: white;" + Environment.NewLine);
            builder.Append("}" + Environment.NewLine);
            builder.Append("" + Environment.NewLine);
            builder.Append(".titleLeft1 {" + Environment.NewLine);
            builder.Append("color: white;" + Environment.NewLine);
            builder.Append("font-size: 12pt;" + Environment.NewLine);
            builder.Append("text-align: center;" + Environment.NewLine);
            builder.Append("padding-top: 6px;" + Environment.NewLine);
            builder.Append("height: 32px;" + Environment.NewLine);
            builder.Append("}" + Environment.NewLine);
            builder.Append("" + Environment.NewLine);
            builder.Append(".titleLeft2 {" + Environment.NewLine);
            builder.Append("color: white;" + Environment.NewLine);
            builder.Append("font-size: 10pt;" + Environment.NewLine);
            builder.Append("text-align: center;" + Environment.NewLine);
            builder.Append("display: none;" + Environment.NewLine);
            builder.Append("}" + Environment.NewLine);
            builder.Append("" + Environment.NewLine);
            builder.Append(".topLeft {" + Environment.NewLine);
            builder.Append("background: #333333;" + Environment.NewLine);
            builder.Append("height: 100%;" + Environment.NewLine);
            builder.Append("}" + Environment.NewLine);
            builder.Append("" + Environment.NewLine);
            builder.Append(".topMenuLeft {" + Environment.NewLine);
            builder.Append("" + Environment.NewLine);
            builder.Append("}" + Environment.NewLine);
            builder.Append("" + Environment.NewLine);
            builder.Append("" + Environment.NewLine);
            builder.Append("" + Environment.NewLine);
            builder.Append(".topMenuLeft a  {" + Environment.NewLine);
            builder.Append("color: #999999;" + Environment.NewLine);
            builder.Append("padding-left: 6pt;" + Environment.NewLine);
            builder.Append("padding-right: 6pt;" + Environment.NewLine);
            builder.Append("background: #333333;" + Environment.NewLine);
            builder.Append("font-size: 8pt;" + Environment.NewLine);
            builder.Append("text-decoration: none;" + Environment.NewLine);
            builder.Append("display: block;" + Environment.NewLine);
            builder.Append("border-top: 1px solid #595959;" + Environment.NewLine);
            builder.Append("border-left: 1px solid #595959;" + Environment.NewLine);
            builder.Append("border-right: 1px solid #000000;" + Environment.NewLine);
            builder.Append("border-bottom: 1px solid #000000;" + Environment.NewLine);
            builder.Append("line-height: 20px;" + Environment.NewLine);
            builder.Append("white-space: nowrap;" + Environment.NewLine);
            builder.Append("height: 20px;" + Environment.NewLine);
            builder.Append("vertical-align: middle;" + Environment.NewLine);
            builder.Append("}" + Environment.NewLine);
            builder.Append("" + Environment.NewLine);
            builder.Append(".topMenuLeft a:hover {" + Environment.NewLine);
            builder.Append("color: white;" + Environment.NewLine);
            builder.Append("}" + Environment.NewLine);
            builder.Append("" + Environment.NewLine);
            builder.Append(".selectedLeft {" + Environment.NewLine);
            builder.Append("color: #ffffff;" + Environment.NewLine);
            builder.Append("padding-left: 15px;" + Environment.NewLine);
            builder.Append("font-size: 8pt;" + Environment.NewLine);
            builder.Append("line-height: 20px;" + Environment.NewLine);
            builder.Append("width: 185px;" + Environment.NewLine);
            builder.Append("display: block;" + Environment.NewLine);
            builder.Append("vertical-align: middle;" + Environment.NewLine);
            builder.Append("text-decoration: none;" + Environment.NewLine);
            builder.Append("border-top: 1px solid #595959;" + Environment.NewLine);
            builder.Append("border-bottom: 1px solid #000000;" + Environment.NewLine);
            builder.Append("}" + Environment.NewLine);
            builder.Append("" + Environment.NewLine);
            builder.Append(".leftPageLeft {" + Environment.NewLine);
            builder.Append("	background: #EDF2F2;" + Environment.NewLine);
            builder.Append("}" + Environment.NewLine);
            builder.Append("" + Environment.NewLine);
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
