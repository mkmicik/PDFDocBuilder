using System;
using PDFDocBuilder;
using PDFDocBuilder.DAO;
using DebenuPDFLibraryDLL1114;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data;
using System.IO;
using System.Collections.Generic;

namespace PDFDocBuilderTest
{
    /// <summary>
    /// Author: Michel Kmicik
    /// Created On: 05.28.15
    /// Mainly just used to populate the relevant tables with data.
    /// </summary>
    [TestClass]
    public class UnitTest1
    {
        PDFLibrary lib;
        int UID = 1;
        string FILE_NAME = "TestFile1.pdf";
        string FILE_PATH = "C:/Users/mkmicik/Documents/Visual Studio 2013/Projects/PDFDocBuilder/PDFDocBuilder/bin/Debug/";

        public void Init()
        {
            DataAccess dao = DataAccess.getInstance();
            PDFDocBuilder.StatementBuilder sb = new PDFDocBuilder.StatementBuilder();
            
            byte[] bytes = GetBytesFromFile(FILE_PATH, FILE_NAME);
            string values = CreateXML();
        
            // Truncate Tables
            //dao.ExecuteNonQuery(sb.Truncate_Document());
            //dao.ExecuteNonQuery(sb.Truncate_TemplatedDocument());

            // Insert Data
            dao.ExecuteNonQuery(sb.Insert_PDFTemplate(UID, bytes));
            dao.ExecuteNonQuery(sb.Insert_XML(UID, values));

        }

        private string CreateXML()
        {
            XMLConverter converter = new XMLConverter();
            DataTable dt = new DataTable("FormFieldValues");

            dt.Columns.Add("Key");
            dt.Columns.Add("Value");

            List<string> keys = new List<string> { "AskNum_1", "SubAgreement_CDN1", "SubAgreement_SubscriberName" };
            List<string> vals = new List<string> { "123", "1.24", "George Washington"};

            for (int i = 0; i < keys.Count; i++) 
            {
                dt.Rows.Add(new Object[] { keys[i], vals[i] });
            }
            return converter.WriteXML(dt);
        }

        private byte[] GetBytesFromFile(string FILE_PATH, string FILE_NAME)
        {
            FileStream fs = File.OpenRead(FILE_PATH + FILE_NAME);
            byte[] bytes;
            try
            {
                bytes = new byte[fs.Length];
                fs.Read(bytes, 0, Convert.ToInt32(fs.Length));
            }
            catch
            {
                throw new IOException();
            }
            finally 
            {
                fs.Close();                
            }
            return bytes;
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


        [TestMethod]
        public void TestRunThrough()
        {
            //Init();
            DocBuilder.Main(new string[1] { "1" });

        }
    }
}
