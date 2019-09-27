using System;
using System.Data;
using System.IO;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Text;
//using DbfDataReader;
//using Microsoft.Extensions.Options;
using System.Diagnostics;
using fias.SQL.DataSets;
using Fias.Loaders;
using Logger;
using System.Reflection;

namespace Fias.Operators
{


    public class FiasOperatorDBF : FiasOperator
    {
        SqlCommand ClearTables;
        readonly SqlCommand MERGE_fias_ActualStatus = new SqlCommand()
        {
            CommandType = CommandType.Text,
            CommandText = Properties.Resources.MERGE_fias_ActualStatus.ToString()
        };
        readonly SqlCommand MERGE_fias_AddressObjectType = new SqlCommand()
        {
            CommandType = CommandType.Text,
            CommandText = Properties.Resources.MERGE_fias_AddressObjectType.ToString()
        };
        readonly SqlCommand MERGE_fias_CenterStatus = new SqlCommand()
        {
            CommandType = CommandType.Text,
            CommandText = Properties.Resources.MERGE_fias_CenterStatus.ToString()
        };
        readonly SqlCommand MERGE_fias_CurrentStatus = new SqlCommand()
        {
            CommandType = CommandType.Text,
            CommandText = Properties.Resources.MERGE_fias_CurrentStatus.ToString()
        };
        SqlCommand MERGE_fias_EstateStatus = new SqlCommand()
        {
            CommandType = CommandType.Text,
            CommandText = Properties.Resources.MERGE_fias_EstateStatus.ToString()
        };
        SqlCommand MERGE_fias_HouseStateStatus = new SqlCommand()
        {
            CommandType = CommandType.Text,
            CommandText = Properties.Resources.MERGE_fias_HouseStateStatus.ToString()
        };
        SqlCommand MERGE_fias_House = new SqlCommand()
        {
            CommandType = CommandType.Text,
            CommandText = Properties.Resources.MERGE_fias_House.ToString()
        };
        SqlCommand MERGE_fias_NormativeDocumentType = new SqlCommand()
        {
            CommandType = CommandType.Text,
            CommandText = Properties.Resources.MERGE_fias_NormativeDocumentType.ToString()
        };
        SqlCommand MERGE_fias_Object = new SqlCommand()
        {
            CommandType = CommandType.Text,
            CommandText = Properties.Resources.MERGE_fias_Object.ToString()
        };
        readonly SqlCommand MERGE_fias_OperationStatus = new SqlCommand()
        {
            CommandType = CommandType.Text,
            CommandText = Properties.Resources.MERGE_fias_OperationStatus.ToString()
        };
        readonly SqlCommand MERGE_fias_Room = new SqlCommand()
        {
            CommandType = CommandType.Text,
            CommandText = Properties.Resources.MERGE_fias_Room.ToString()
        };
        readonly SqlCommand MERGE_fias_RoomType = new SqlCommand()
        {
            CommandType = CommandType.Text,
            CommandText = Properties.Resources.MERGE_fias_RoomType.ToString()
        };
        readonly SqlCommand MERGE_fias_Stead = new SqlCommand()
        {
            CommandType = CommandType.Text,
            CommandText = Properties.Resources.MERGE_fias_Stead.ToString()
        };
        readonly SqlCommand MERGE_fias_StructureStatus = new SqlCommand()
        {
            CommandType = CommandType.Text,
            CommandText = Properties.Resources.MERGE_fias_StructureStatus.ToString()
        };
        protected readonly WebLoader WebLoader = new WebLoader();
        public string[,] patterns = new string[,] { { "ActualStatus", "ACTSTAT.DBF" }, { "CenterStatus", "CENTERST.DBF" }, {"CurrentStatus","CURENTST.DBF"},
            {"EstateStatus","ESTSTAT.DBF"},{"FlatType","FLATTYPE.DBF"},{"IntervalStatus","INTVSTAT.DBF"},{"HouseStateStatus","HSTSTAT.DBF"},
            {"NormativeDocumentType","NDOCTYPE.DBF"},{"OperationStatus","OPERSTAT.DBF"}, {"RoomType","ROOMTYPE.DBF"},{"AddressObjectType","SOCRBASE.DBF"},
            {"StructureStatus","STRSTAT.DBF"},
            { "Object", "ADDROB??.DBF"},{ "House", "HOUSE??.DBF"},{ "NormativeDocument","NORDOC??.DBF"},
            { "Stead", "STEAD??.DBF"},{"Room", "ROOM??.DBF"},
            { "Del_House", "DHOUSE.DBF"}, { "Del_NormativeDocument","DNORDOC.DBF"},{ "Del_Object", "DADDROB.DBF"}
        };
        protected List<List<BulkTableListItem>> bulkLists = new List<List<BulkTableListItem>>();

        public FiasOperatorDBF(DirectoryInfo rootdir, SqlConnection connection, string schemaname) : base(rootdir, connection, schemaname)
        {
            ClearTables = new SqlCommand()
        {
            Connection=Connection,
            CommandType = CommandType.Text,
            CommandText = Properties.Resources.ClearTables_fias_tmp.ToString()
        };
            MERGE_fias_ActualStatus.Connection = Connection;
            MERGE_fias_AddressObjectType.Connection = Connection;
            MERGE_fias_CenterStatus.Connection = Connection;
            MERGE_fias_CurrentStatus.Connection = Connection;
            MERGE_fias_EstateStatus.Connection = Connection;
            MERGE_fias_HouseStateStatus.Connection = Connection;
            MERGE_fias_NormativeDocumentType.Connection = Connection;
            MERGE_fias_OperationStatus.Connection = Connection;
            MERGE_fias_RoomType.Connection = Connection;
            MERGE_fias_StructureStatus.Connection = Connection;
            MERGE_fias_Object.Connection = Connection;
            MERGE_fias_House.Connection = Connection;
            MERGE_fias_Room.Connection = Connection;
            MERGE_fias_Stead.Connection = Connection;
        }
        public FiasOperatorDBF(DirectoryInfo rootdir, string connectionString, string schemaname) : this(rootdir, new SqlConnection(connectionString), schemaname)
        {
        }

        public void Load()
        {
            ClearTables.ExecuteNonQuery();
            SetBulkLists();
            foreach (var bulkList in bulkLists)
            {
                try
                {
                    foreach (BulkTableListItem btlItem in bulkList)
                    {
                        if (btlItem.File != null && btlItem.TableName != null && btlItem.TableSchema != null)
                        {
                            DateTime _start = DateTime.Now;
                            Logger.Logger.Info(btlItem.File.Name + " запущено");
                            LoadToDb(btlItem.File, btlItem.TableName);
                            DateTime _end = DateTime.Now;
                            Logger.Logger.Info(btlItem.File.Name + " закончено за " + (_end - _start).TotalSeconds.ToString() + " секунд");
                        }
                    }
                }
                finally
                {

                }

            };
        }
        public void DownloadFromSite(bool fullDB)
        {
            DownloadFromSite(fullDB, LastDownloadDate);
        }
        public void DownloadFromSite(bool fullDB, DateTime LastDownload)
        {
            var dateStart = DateTime.Now;
            var loaddate = WebLoader.Load(fullDB, Rootdir, LastDownload);
            var dateEnd = DateTime.Now;
            if (loaddate != null)
            {
                SetLastDownloadDate(fullDB ? "Полная ФИАС" : "Обновление ФИАС", (DateTime)loaddate, true, dateStart, dateEnd);
            }
        }
        public void SetBulkLists()
        {

            List<BulkTableListItem> bulkList = new List<BulkTableListItem>();
            for (int n = 0; n <= (patterns.Length / patterns.Rank - 1); n++)
            {
                foreach (FileInfo file in Rootdir.GetFiles(patterns[n, 1]).OrderBy(file => file.Length))
                {
                    bulkList.Add(new BulkTableListItem(patterns[n, 0], SchemaName, file));
                }
            }
            bulkLists.Add(bulkList);
        }
        public void LoadToDb(FileInfo dbfFile, string TableName)
        {
            SqlTransaction TRA = Connection.BeginTransaction(("Bulk" + TableName));
            SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(Connection, SqlBulkCopyOptions.Default, TRA)
            {
                BulkCopyTimeout = 60000,
                DestinationTableName = string.Concat(SchemaName, ".", TableName)
            };
            try
            {
                var errors = new List<object>();
                using (NDbfReader.Table dbfTable = NDbfReader.Table.Open(dbfFile.Open(FileMode.Open)))
                {
                    List<DataRow> rows = new List<DataRow>();
                    DBFDataReader dbfReader = new DBFDataReader(dbfTable, Encoding.GetEncoding(866));
                    foreach (var c in dbfTable.Columns)
                        sqlBulkCopy.ColumnMappings.Add(c.Name, c.Name);
                    sqlBulkCopy.WriteToServer(dbfReader);
                    TRA.Commit();
                }
                if (errors.Count == 0)
                    dbfFile.Delete();
            }
            finally
            { }

        }
        public void MergeDB()
        {
            MERGE_fias_ActualStatus.ExecuteNonQuery();
            MERGE_fias_AddressObjectType.ExecuteNonQuery();
            MERGE_fias_CenterStatus.ExecuteNonQuery();
            MERGE_fias_CurrentStatus.ExecuteNonQuery();
            MERGE_fias_EstateStatus.ExecuteNonQuery();
            MERGE_fias_HouseStateStatus.ExecuteNonQuery();
            MERGE_fias_NormativeDocumentType.ExecuteNonQuery();
            MERGE_fias_OperationStatus.ExecuteNonQuery();
            MERGE_fias_RoomType.ExecuteNonQuery();
            MERGE_fias_StructureStatus.ExecuteNonQuery();
            MERGE_fias_Object.ExecuteNonQuery();
            MERGE_fias_House.ExecuteNonQuery();
            MERGE_fias_Room.ExecuteNonQuery();
            MERGE_fias_Stead.ExecuteNonQuery();
        }
    }
}


