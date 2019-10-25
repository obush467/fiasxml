using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fias.Loaders;
using Fias.Operators;

namespace FiasLoader
{
    class Program
    {


        // [STAThread]

        public static string DBF_Directory = "C:\\Temp";
        //public static string XML_Directory = "C:\\Users\\PEG1\\Downloads\\Compressed\\fias_dbf";
        public static string schemaname = "fias_tmp";
        //public static FiasOperatorXML fiasXMLDataSetConverter = new FiasOperatorXML(new DirectoryInfo(XML_Directory), connection, schemaname);


        static void Main(string[] args)
        {
            try
            {
                SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder()
                {
                    DataSource = "BUSHMAKIN",
                    InitialCatalog = "UNS",
                    IntegratedSecurity = true,
                    ConnectTimeout = 0,
                    NetworkLibrary = "dbmssocn"
                };
                var serverTableType = ServerTableType.GlobalTemp;
                FiasOperatorDBF fiasDBFDataSetConverter = new FiasOperatorDBF(new DirectoryInfo(DBF_Directory), builder.ConnectionString, schemaname);
                //var DownloadTask = Task.Factory.StartNew(() => fiasDBFDataSetConverter.DownloadFromSite(true));
                var LoadTask = Task.Factory.StartNew(() => fiasDBFDataSetConverter.BulkLoad(serverTableType));/*DownloadTask*/
                var MergeTask = LoadTask.ContinueWith((a) => fiasDBFDataSetConverter.MergeDB(serverTableType));
                MergeTask.Wait();
            }
            finally { }
        }
    }
}

