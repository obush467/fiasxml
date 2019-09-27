using System;
using System.Collections.Generic;
using System.Data;
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
        protected SqlCommand commandLastDownloadDate;
        protected SqlCommand commandLastDownloadDateInsert;
        public DateTime LastDownloadDate { get { return (DateTime)commandLastDownloadDate.ExecuteScalar(); }}
        public int SetLastDownloadDate(string UploadType, DateTime UploadData, bool Success, DateTime Startdate, DateTime Enddate)
        {
            commandLastDownloadDateInsert.Parameters["@UploadType"].Value=UploadType;
            commandLastDownloadDateInsert.Parameters["@UploadData"].Value=UploadData;
            commandLastDownloadDateInsert.Parameters["@Startdate"].Value=Startdate;
            commandLastDownloadDateInsert.Parameters["@Enddate"].Value=Enddate;
            commandLastDownloadDateInsert.Parameters["@Success"].Value=Success;
            return commandLastDownloadDateInsert.ExecuteNonQuery();
        }
        public FiasOperator(DirectoryInfo rootdir, SqlConnection connection, string schemaname)
        {
            Connection = connection;           
            Rootdir = rootdir;
            SchemaName = schemaname;
            ConnectionString = connection.ConnectionString;
            Logger.Logger.InitLogger();
            Connection.Open();
            commandLastDownloadDate = Connection.CreateCommand();
            commandLastDownloadDate.CommandType = CommandType.Text;
            commandLastDownloadDate.CommandText = "SELECT top 1 [UploadData] FROM [fias].[Uploads] order by UploadData desc";
            commandLastDownloadDateInsert = Connection.CreateCommand();
            commandLastDownloadDateInsert.CommandType = CommandType.Text;
            commandLastDownloadDateInsert.CommandText = "INSERT INTO[fias].[Uploads] ([UploadType],[UploadData],[Success],[Startdate],[Enddate]) VALUES (@UploadType,@UploadData,@Success,@Startdate,@Enddate)";
            commandLastDownloadDateInsert.Parameters.Add("@UploadType", SqlDbType.NVarChar);
            commandLastDownloadDateInsert.Parameters.Add("@UploadData", SqlDbType.DateTime);
            commandLastDownloadDateInsert.Parameters.Add("@Startdate", SqlDbType.DateTime);
            commandLastDownloadDateInsert.Parameters.Add("@Enddate", SqlDbType.DateTime);
            commandLastDownloadDateInsert.Parameters.Add("@Success", SqlDbType.Bit);
        }

        public FiasOperator(DirectoryInfo rootdir, string connectionString, string schemaname):this (rootdir,new SqlConnection(connectionString),schemaname)
        { }
    }
}
