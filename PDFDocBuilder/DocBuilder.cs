using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DebenuPDFLibraryDLL1114;
using PDFDocBuilder.DAO;
using System.Data;
using System.Xml.Linq;

namespace PDFDocBuilder
{
    /// <summary>
    /// Author: Michel Kmicik
    /// Created On: 05.28.15
    /// Dictates the workflow for building PDf docs.
    /// </summary>
    public class DocBuilder
    {
        private static string DEST_FILENAME = "Form_Fields_Populated.pdf";

        /// <summary>
        /// This method is mostly for testing purposes, as the test 
        /// cases seem to fail trying to instatiate the DebenuPDF 
        /// library. Will populate the PDF by the supplied UID or default
        /// to 1.
        /// </summary>
        /// <param name="args">The first param ca be the UID.</param>
        public static void Main(string[] args)
        {
            DocBuilder p = new DocBuilder();

            int UID;

            try { UID = Int32.Parse(args[0]); }
            catch { UID = 1; }
            
            PDFLibrary doc = p.populatePDF(UID);
            doc.SaveToFile(DEST_FILENAME);
        }

        /// <summary>
        /// Takes a template PDF document and an XML string 
        /// representation of the key value pairs to populate to form
        /// fields. It returns a PDFLibrary object with reference to 
        /// the now populated PDF document.
        /// </summary>
        /// <param name="doc">PDFLibrary object with reference to the 
        /// template PDF document.</param>
        /// <param name="xml">String XML representation of the key 
        /// value pairs.</param>
        /// <returns>PDFLibrary object with reference to the populated 
        /// PDF document.</returns>
        private PDFLibrary populatePDF(int UID)
        {
            PDFLibrary doc = initDoc();
            
            byte[] pdf_template = getPDFTemplate(UID);
            string xml = getXML(UID);

            doc.LoadFromString(pdf_template, "");

            XMLConverter converter = new XMLConverter();
            DataTable key_values = converter.ReadXML(xml);

            foreach (DataRow r in key_values.Rows)
            {
                doc.SetFormFieldValueByTitle(r.ItemArray[0].ToString(), r.ItemArray[1].ToString());
            }

            return doc;
        }

        private PDFLibrary initDoc()
        {
            PDFLibrary lib = new PDFLibrary("DebenuPDFLibraryDLL1114.dll");

            // get product key from configs?
            int valid = lib.UnlockKey("jz3mc41i6bi7e788371z9b35y");

            if (valid != 1) 
            { 
                throw new Exception("Invalid Debenu License Key."); 
            }

            return lib;
        }

        /// <summary>
        /// Returns the PDF template from the SQL query. 
        /// TODO: Add error checking for empty result sets.
        /// </summary>
        /// <param name="UID">UID for document to retrieve</param>
        /// <returns>byte[] of PDF template.</returns>
        private byte[] getPDFTemplate(int UID)
        {
            StatementBuilder sb = new StatementBuilder();
            DataAccess dao = DataAccess.getInstance();

            DataTable dt = dao.ExecuteSelect(sb.Select_PDFTemplate(UID));
            return (byte[]) dt.Rows[0][7];
        }

        /// <summary>
        /// Returns the XML string from the SQL query. 
        /// TODO: Add error checking for empty result sets.
        /// </summary>
        /// <param name="UID">UID for document to retrieve</param>
        /// <returns>XML parameters</returns>
        private string getXML(int UID)
        {
            StatementBuilder sb = new StatementBuilder();
            DataAccess dao = DataAccess.getInstance();

            DataTable dt = dao.ExecuteSelect(sb.Select_XML(UID));
            return (string) dt.Rows[0][5];
        }

    }
}
