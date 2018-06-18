using fias.DBF;
using fias.XML;
using System.IO;

namespace fias
{
    class Program
    {
        // [STAThread]

        private static fias_XML_to_dataset fiasXMLDataSetConverter = new fias_XML_to_dataset(new DirectoryInfo("E:\\fias\\fias_DBF"));
        private static fias_DBF_to_dataset fiasDBFDataSetConverter = new fias_DBF_to_dataset(new DirectoryInfo("E:\\fias\\fias_DBF"),new System.Data.SqlClient.SqlConnection("Data Source=MAKSIMOV;Initial Catalog=GBUMATC;Persist Security Info=True;User ID=Бушмакин;Password=453459"));
        static void Main(string[] args)
        {
            try { 
                fiasXMLDataSetConverter.Load();
            }
            finally { }

        }

        }
}
