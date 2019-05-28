using fias.DBF;
using fias.XML;
using System.IO;
using System.Data.SqlClient;

namespace fias
{
    class Program
    {
        // [STAThread]
        public static string ConnectionString  = "Data Source=BUSHMAKIN;Initial Catalog=UNS;Integrated Security=True";
        public static SqlConnection connection = new SqlConnection(ConnectionString);
        public static string DBF_Directory = "C:\\Users\\Bushmakin\\Documents\\Новая папка\\Compressed\\fias_dbf";
        public static string XML_Directory = "C:\\Users\\PEG1\\Downloads\\Compressed\\fias_dbf";
        public static string schemaname="fias_tmp";
        private static fias_XML_to_dataset fiasXMLDataSetConverter = new fias_XML_to_dataset(new DirectoryInfo(XML_Directory),connection);
        private static fias_DBF_to_dataset fiasDBFDataSetConverter = new fias_DBF_to_dataset(new DirectoryInfo(DBF_Directory),connection,schemaname);
        static void Main(string[] args)
        {
            try {
                fiasDBFDataSetConverter.Load("fias_tmp");
// fiasXMLDataSetConverter.Load();
            }
            finally { }

        }

        }
}
