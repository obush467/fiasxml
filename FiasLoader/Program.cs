using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
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
                var ct=new CancellationTokenSource();
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
                var LoadTaskOld = Task.Factory.StartNew(() =>
                {
                    fiasDBFDataSetConverter.SPLoad();                    
                });
                var DownloadTask = LoadTaskOld.ContinueWith(a => fiasDBFDataSetConverter.DownloadFromSite(false));
                //var LoadTask = Task.Factory.StartNew(() => fiasDBFDataSetConverter.BulkLoad(serverTableType));/*DownloadTask*/
                var LoadTask = DownloadTask.ContinueWith(a => fiasDBFDataSetConverter.SPLoad());
                /*var MergeTask = LoadTask.ContinueWith(a => 
                    { 
                        if (a.IsCompleted) 
                        {   
                            fiasDBFDataSetConverter.MergeDB(serverTableType); 
                        }
                    });*/
                LoadTask.Wait();
            }
            finally { }
        }
    }
}

