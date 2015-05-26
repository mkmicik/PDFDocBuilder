using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace PDFDocBuilder.DAO
{

    public class DataAccess
    {
        private static DataAccess _instance;
        private String conn_string;

        private DataAccess()
        {
            conn_string = ConfigurationManager.ConnectionStrings["PDF_DATABASE"].ConnectionString;
        }

        public static DataAccess getInstance()
        {
            if (_instance == null)
            {
                _instance = new DataAccess();
            }
            return _instance;
        }

        public DataTable getValues(int UID)
        {
            throw new NotImplementedException();
        }

        public byte[] getPDFDoc(int UID)
        {
            throw new NotImplementedException();
        }


    }
}
