using Fias;
using Fias.Operators;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fias.SQL.DataSets
{
    public class BulkTableListItem
    {
        public string TableName { get; set; }

        public string DestinationTableName
        {
            get
            {
                string result;
                switch (ServerTableType)
                {
                    case ServerTableType.Schema:
                        result = string.Concat(TableSchema, ".", TableName);
                        break;
                    case ServerTableType.GlobalTemp:
                        result = string.Concat("##", TableName);
                        break;
                    case ServerTableType.ConnectionTemp:
                        result = string.Concat("#", TableName);
                        break;
                    default:
                        result = null;
                        break;
                }
                return result;
            }
        }
        public string TableSchema { get; set; }
        public FileInfo File { get; set; }
        public SqlConnection Connection { get; set; }
        public ServerTableType ServerTableType { get; set; }
        public BulkTableListItem(string tableName, string tableSchema, FileInfo file,ServerTableType serverTableType =ServerTableType.Schema,SqlConnection connection=null)
        {
            TableName = tableName;
            TableSchema = tableSchema;
            File = file;
            ServerTableType = serverTableType;
            Connection = connection;
        }
        public void Load_DBFToDb()
        {
            using (SqlTransaction TRA = Connection.BeginTransaction(("Bulk" + TableName)))
            {
                SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(Connection, SqlBulkCopyOptions.Default, TRA)
                {
                    BulkCopyTimeout = 60000,
                    DestinationTableName = DestinationTableName
                };
                try
                {
                    var errors = new List<object>();
                    using (NDbfReader.Table dbfTable = NDbfReader.Table.Open(File.Open(FileMode.Open)))
                    {
                        List<DataRow> rows = new List<DataRow>();
                        DBFDataReader dbfReader = new DBFDataReader(dbfTable, Encoding.GetEncoding(866));
                        foreach (var c in dbfTable.Columns)
                            sqlBulkCopy.ColumnMappings.Add(c.Name, c.Name);
                        sqlBulkCopy.WriteToServer(dbfReader);
                        TRA.Commit();
                    }
                    if (errors.Count == 0)
                        File.Delete();
                }
                catch (Exception e)
                {
                    Logger.Logger.Error(e.Message);
                }
                finally
                {
                    sqlBulkCopy.Close();
                }
            }

        }
    }
}

