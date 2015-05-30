using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace PDFDocBuilder
{
    /// <summary>
    /// Author: Michel Kmicik
    /// Created On: 05.28.15
    /// Handles conversions to and from XML.
    /// </summary>
    public class XMLConverter
    {

        /// <summary>
        /// Takes an XML string representation of key value pairs
        /// and returns a datatable representation of the pairs.
        /// </summary>
        /// <param name="xml">XML string representation of the key
        /// value pairs</param>
        /// <returns>DataTable representation of the key value
        /// pairs.</returns>
        public DataTable ReadXML(string xml)
        {
            // Create Datatable and init columns
            DataTable dt = new DataTable("FormFieldValues");
            dt.Columns.Add("Key", typeof(string));
            dt.Columns.Add("Value", typeof(string));

            System.IO.MemoryStream xmlStream = StringToStream(xml);
            dt.ReadXml(xmlStream);
            xmlStream.Close();
            return dt;
        }

        public string WriteXML(DataTable dt)
        {
            System.IO.StringWriter writer = new System.IO.StringWriter();
            dt.WriteXml(writer, XmlWriteMode.WriteSchema, false);
            return writer.ToString();
        }

        /// <summary>
        /// Takes an XML string and returns a memory stream so 
        /// that it can be loaded into a datatable.
        /// </summary>
        /// <param name="s">XML string to be turned into a 
        /// memory stream.</param>
        /// <returns>Memory stream representation of XML.</returns>
        private MemoryStream StringToStream(string s)
        {
            MemoryStream stream = new MemoryStream();
            StreamWriter writer = new StreamWriter(stream);
            writer.Write(s);
            writer.Flush();
            stream.Position = 0;
            //writer.Close();
            return stream;
        }


    }
}
