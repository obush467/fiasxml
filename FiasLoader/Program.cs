using System;
using System.Collections.Generic;
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
            public static string ConnectionString = "Data Source=BUSHMAKIN;Initial Catalog=UNS;Integrated Security=True;Connection Timeout=1000000";
            public static string DBF_Directory = "C:\\Temp2";
            public static string XML_Directory = "C:\\Users\\PEG1\\Downloads\\Compressed\\fias_dbf";
            public static string schemaname = "fias_tmp";
            //public static FiasOperatorXML fiasXMLDataSetConverter = new FiasOperatorXML(new DirectoryInfo(XML_Directory), connection, schemaname);
            private static FiasOperatorDBF fiasDBFDataSetConverter = new FiasOperatorDBF(new DirectoryInfo(DBF_Directory), ConnectionString, schemaname);
        
        static void Main(string[] args)
            {



                try
                {
                    fiasDBFDataSetConverter.DownloadFromSite(false,);
                    fiasDBFDataSetConverter.Load();
                    // fiasXMLDataSetConverter.Load();
                }
                finally { }
                
            }

        }
    }
    
