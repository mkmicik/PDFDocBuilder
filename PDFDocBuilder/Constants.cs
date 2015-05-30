using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDFDocBuilder
{
    /// <summary>
    /// Author: Michel Kmicik
    /// Created On: 05.28.15
    /// </summary>
    public class Constants
    {
        public static string SELECT_PDF_TEMPLATE = "SELECT * FROM [AVIF].[dbo].[Document] WHERE iDocumentID = @iDocumentID ;";
        public static string SELECT_XML = "SELECT * FROM [AVIF].[dbo].[TemplatedDocument] WHERE iTemplatedDocumentID = @iDocumentID ;";

        public static string TRUNCATE_DOCUMENT = "TRUNCATE TABLE [AVIF].[dbo].[Document];";
        public static string TRUNCATE_TEMPLATED_DOCUMENT = "TRUNCATE TABLE [AVIF].[dbo].[TemplatedDocument];";

        public static string INSERT_PDF_TEMPLATE = "INSERT INTO [AVIF].[dbo].[Document] (iDocumentTypeID, iUserIDCreatedBy, bTemplate, vDocumentName, vMimeType, dVersion, vbDocument, dtCreated ) " 
            + " VALUES ( @iDocumentTypeID, @iUserIDCreatedBy, @bTemplate, @vDocumentName, @vMimeType, @dVersion, @vbDocument, @dtCreated ) ;";
        public static string INSERT_XML = "INSERT INTO [AVIF].[dbo].[TemplatedDocument] (iUserIDCreatedBy, vTableName, iTableKey, iDocumentID, vXMLFieldDictionary, dVersion, dtCreated) "
            + " VALUES ( @iUserIDCreatedBy, @vTableName, @iTableKey, @iDocumentID, @vXMLFieldDictionary, @dVersion, @dtCreated ) ;";
    }
}
