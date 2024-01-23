using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using Newtonsoft;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Regras
{
    public static class TransformaTipos
    {
        /// <summary>
        /// Método que identa o json
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        public static string IdentaJson(string json, ref string mensageErro)
        {
            mensageErro = string.Empty;

            try
            {
                string newJson = JValue.Parse(json).ToString(Newtonsoft.Json.Formatting.Indented);

                return newJson;
            }
            catch(Exception ex)
            {
                mensageErro = ex.Message;
            }

            return string.Empty;
        }

        /// <summary>
        /// Método que identa xml
        /// </summary>
        /// <param name="xmlContent"></param>
        /// <returns></returns>
        public static string IdentaXml(string xmlContent, ref string mensageErro)
        {
            try
            {
                mensageErro = string.Empty;
                // Load the XML content into an XDocument
                XDocument xDocument = XDocument.Parse(xmlContent);

                // Use a StringWriter to store the formatted XML
                using (StringWriter stringWriter = new StringWriter())
                {
                    // Create an XmlWriter with indentation settings
                    XmlWriterSettings settings = new XmlWriterSettings
                    {
                        OmitXmlDeclaration = true,
                        Indent = true,
                        IndentChars = "   ", // Adjust indentation as needed
                        NewLineChars = Environment.NewLine
                    };

                    // Write the XDocument to the XmlWriter
                    using (XmlWriter xmlWriter = XmlWriter.Create(stringWriter, settings))
                    {
                        xDocument.Save(xmlWriter);
                    }

                    // Get the formatted XML string from the StringWriter
                    string formattedXml = stringWriter.ToString();

                    return formattedXml;
                }
            }
            catch (Exception ex)
            {
                mensageErro = $"An error occurred: {ex.Message}";
                return null;
            }
        }

        /// <summary>
        /// Método que converte json to Xml
        /// </summary>
        /// <param name="jsonText"></param>
        /// <returns></returns>
        public static string ConvertJsonToXml(string jsonText, ref string mensageErro)
        {
            try
            {
                mensageErro = string.Empty;
                // Parse the JSON text into a JObject
                JObject jsonObject = JObject.Parse(jsonText);

                // Convert the JObject to an XNode (XElement or XDocument)
                XNode xmlNode = JsonConvert.DeserializeXNode(jsonObject.ToString(), "root");

                // Get the XML string representation
                string xmlString = xmlNode.ToString();

                return IdentaXml(xmlString, ref mensageErro);
            }
            catch (Exception ex)
            {
                mensageErro = $"An error occurred: {ex.Message}";
                return null;
            }
        }

        /// <summary>
        /// Método que transforma xml to json
        /// </summary>
        /// <param name="xmlString"></param>
        /// <param name="mensageErro"></param>
        /// <returns></returns>
        public static string ConvertXmlToJson(string xmlString, ref string mensageErro)
        {
            try
            {
                mensageErro = string.Empty;
                // Load the XML string into an XmlDocument
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(xmlString);

                // Use JsonConvert.SerializeXmlNode to convert XML to JSON
                string jsonString = JsonConvert.SerializeXmlNode(xmlDoc, Newtonsoft.Json.Formatting.Indented);

                return IdentaJson(jsonString, ref mensageErro);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return null;
            }
        }
    }
}
