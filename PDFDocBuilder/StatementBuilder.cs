using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDFDocBuilder
{
    /// <summary>
    /// Author: Michel Kmicik
    /// Created On: 05.28.15
    /// Builds statements from given parameters.
    /// </summary>
    public class StatementBuilder
    {
        public SqlCommand Select_PDFTemplate(int UID)
        {
            SqlCommand cmd = new SqlCommand(Constants.SELECT_PDF_TEMPLATE);
            cmd.Parameters.Add("@iDocumentID", SqlDbType.Int).Value = UID;
            return cmd;
        }

        public SqlCommand Select_XML(int UID)
        {
            SqlCommand cmd = new SqlCommand(Constants.SELECT_XML);
            cmd.Parameters.Add("@iDocumentID", SqlDbType.Int).Value = UID;
            return cmd;
        }

        public SqlCommand Truncate_Document()
        {
            SqlCommand cmd = new SqlCommand(Constants.TRUNCATE_DOCUMENT);
            return cmd;
        }

        public SqlCommand Truncate_TemplatedDocument()
        {
            SqlCommand cmd = new SqlCommand(Constants.TRUNCATE_TEMPLATED_DOCUMENT);
            return cmd;
        }

        public SqlCommand Insert_PDFTemplate(int UID, byte[] vbDocument)
        {
            SqlCommand cmd = new SqlCommand(Constants.INSERT_PDF_TEMPLATE);
            //cmd.Parameters.Add("@iDocumentID", SqlDbType.Int).Value = null;
            cmd.Parameters.Add("@iDocumentTypeID", SqlDbType.Int).Value = 1;
            cmd.Parameters.Add("@iUserIDCreatedBy", SqlDbType.Int).Value = 1;
            cmd.Parameters.Add("@bTemplate", SqlDbType.Bit).Value = 1;
            cmd.Parameters.Add("@vDocumentName", SqlDbType.NVarChar).Value = "TestDoc";
            cmd.Parameters.Add("@vMimeType", SqlDbType.NVarChar).Value = "Some Mime Type";
            cmd.Parameters.Add("@dVersion", SqlDbType.Decimal).Value = 1.00m;
            cmd.Parameters.Add("@vbDocument", SqlDbType.VarBinary).Value = vbDocument;
            cmd.Parameters.Add("@dtCreated", SqlDbType.DateTime).Value = DateTime.Now;
            return cmd;
        }

        public SqlCommand Insert_XML(int UID, string xml)
        {
            SqlCommand cmd = new SqlCommand(Constants.INSERT_XML);
            //cmd.Parameters.Add("@iTemplatedDocumentID", SqlDbType.Int).Value = null;
            cmd.Parameters.Add("@iUserIDCreatedBy", SqlDbType.Int).Value = 1;
            cmd.Parameters.Add("@vTableName", SqlDbType.NVarChar).Value = 1;
            cmd.Parameters.Add("@iTableKey", SqlDbType.Int).Value = 1.0;
            cmd.Parameters.Add("@iDocumentID", SqlDbType.Int).Value = UID;
            cmd.Parameters.Add("@vXMLFieldDictionary", SqlDbType.NVarChar).Value = xml;
            cmd.Parameters.Add("@dVersion", SqlDbType.Decimal).Value = 1.0m;
            cmd.Parameters.Add("@dtCreated", SqlDbType.DateTime).Value = DateTime.Now;
            return cmd;
        }
    }
}
