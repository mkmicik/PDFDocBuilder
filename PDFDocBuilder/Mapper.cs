using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DebenuPDFLibraryDLL1114;

namespace PDFDocBuilder
{
    /// <summary>
    /// Responsible for filling in the fields in a given PDF document.
    /// </summary>
    public class Mapper
    {
        public Mapper() { }

        /// <summary>
        /// Maps the values to the given PDF document. Assumes that the 
        /// DataTable column names correspond to the form field names in 
        /// the PDF document.
        /// </summary>
        /// <param name="values">DataTable containing one row of values
        /// to be entered into the PDF document.</param>
        /// <param name="doc">PDF document containing the form fields</param>
        /// <returns>True if all values mapped successfully, false if one
        /// or more values could not be mapped.</returns>
        public Boolean mapValuesToFields(DataTable values, PDFLibrary doc)
        {
            Dictionary<string, string> key_vals = getKeyValuePairs(values);

            int success = 0;
            foreach (KeyValuePair<string, string> pair in key_vals)
            {
                success += doc.SetFormFieldValueByTitle(pair.Key, pair.Value);
            }

            if (success > 0)
                return false;
            else return true;
        }

        private Dictionary<string, string> getKeyValuePairs(DataTable values)
        {
            // Col name paired with first row value
            Dictionary<string, string> key_val = new Dictionary<string, string>();

            // Get the items in the first row
            Object[] row = values.Rows[0].ItemArray;

            int i = 0;
            foreach (DataColumn col in values.Columns)
            {
                key_val.Add(col.ColumnName, row[i].ToString());
                i++;
            }

            return key_val;
        }
    }
}
