using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DebenuPDFLibraryDLL1114;
using PDFDocBuilder.DAO;
using System.Data;

namespace PDFDocBuilder
{
    public class Program
    {
        PDFLibrary pdf_builder;

        static void Main(string[] args)
        {
            Program p = new Program();

            try
            {
                p.run(12345);
            }
            catch { }
        }


        private void run(int UID)
        {
            setPDFBuilder();
            DataAccess dao = DataAccess.getInstance();

            DataTable values = dao.getValues(UID);

            /* Do some sort of validation on the values, ensure
             * that they are associated with a non-null PDF doc id */

            byte[] pdf_byte_stream = dao.getPDFDoc(UID);

            /* Do some sort of validation on the PDF */

            pdf_builder.LoadFromString(pdf_byte_stream, "");

            buildPDFDoc(values);
        }

        private void setPDFBuilder()
        {
            PDFLibrary lib = new PDFLibrary("DebenuPDFLibraryDLL1114.dll");

            // get product key from configs?
            int valid = lib.UnlockKey("jz3mc41i6bi7e788371z9b35y");

            if (valid != 1) 
            { 
                throw new Exception("Invalid Debenu License Key."); 
            }
            else
            {
                pdf_builder = lib;
            }
        }

        private void buildPDFDoc(DataTable values)
        {

        }

    }
}
