using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fias.Operators
{
    public class FiasOperator
    {
        protected SqlConnection Connection { get; set; }
        protected DirectoryInfo Rootdir;
        protected string SchemaName;
        protected string ConnectionString;
        public FiasOperator(DirectoryInfo rootdir, SqlConnection connection, string schemaname)
        {
            Connection = connection;           
            Rootdir = rootdir;
            SchemaName = schemaname;
            ConnectionString = connection.ConnectionString;
        }

        public FiasOperator(DirectoryInfo rootdir, string connectionString, string schemaname):this (rootdir,new SqlConnection(connectionString),schemaname)
        { ConnectionString = connectionString;  }
        protected void LogInfo(string message)
        { Console.WriteLine(message); }
    }
}
