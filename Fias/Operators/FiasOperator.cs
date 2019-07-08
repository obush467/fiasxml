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
        public SqlConnection Connection { get; set; }
        public string ConnectionString { get; set; }
        protected DirectoryInfo Rootdir;
        protected string SchemaName;
        protected delegate void log(string message);
        protected log LogDebug;
        protected log LogWarning;
        protected void LogInfo(string message)
        { Console.WriteLine(message); }
        public FiasOperator(DirectoryInfo rootdir, SqlConnection connection, string schemaname)
        {
            Connection = connection;
            ConnectionString = connection.ConnectionString;
            Rootdir = rootdir;
            SchemaName = schemaname;
        }
        public FiasOperator(DirectoryInfo rootdir, string connectionString, string schemaname):this(rootdir,new SqlConnection(connectionString),schemaname)
        { }
    }
}
