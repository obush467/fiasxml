using Fias.XML;
using Fias.Operators;
using System.IO;
using System.Data.SqlClient;

namespace Fias
{
    class Program
    {
        // [STAThread]
        public static string ConnectionString  = "Data Source=BUSHMAKIN;Initial Catalog=UNS;Integrated Security=True";
        public static SqlConnection connection = new SqlConnection(ConnectionString);
        public static string DBF_Directory = "C:\\Users\\Bushmakin\\Documents\\Новая папка\\Compressed\\fias_dbf";
        public static string XML_Directory = "C:\\Users\\PEG1\\Downloads\\Compressed\\fias_dbf";
        public static string schemaname="fias_tmp";
        private static FiasOperatorXML fiasXMLDataSetConverter = new FiasOperatorXML(new DirectoryInfo(XML_Directory),connection, schemaname);
        private static FiasOperatorDBF fiasDBFDataSetConverter = new FiasOperatorDBF(new DirectoryInfo(DBF_Directory),connection,schemaname);
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
