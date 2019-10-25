using System;
using System.Data;
using System.IO;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Text;
using System.Diagnostics;
using fias.SQL.DataSets;
using Fias.Loaders;
using Logger;
using System.Reflection;

namespace Fias.Operators
{
    public enum ServerTableType
    {
        Schema,
        GlobalTemp,
        ConnectionTemp
    }

    public class FiasOperatorDBF : FiasOperator
    {
        #region SqlCommands
        public SqlCommand ClearTables { get; set; } = new SqlCommand()
        {
            CommandType = CommandType.Text,
            CommandText = Properties.Resources.ClearTables_fias_tmp.ToString()
        };
        protected SqlCommand MERGE_fias_ActualStatus { get; set; } = new SqlCommand()
        {
            CommandType = CommandType.Text,
            CommandText = Properties.Resources.MERGE_fias_ActualStatus.ToString()
        };
        public SqlCommand MERGE_fias_AddressObjectType { get; set; } = new SqlCommand()
        {
            CommandType = CommandType.Text,
            CommandText = Properties.Resources.MERGE_fias_AddressObjectType.ToString()
        };
        public SqlCommand MERGE_fias_CenterStatus { get; set; } = new SqlCommand()
        {
            CommandType = CommandType.Text,
            CommandText = Properties.Resources.MERGE_fias_CenterStatus.ToString()
        };
        public SqlCommand MERGE_fias_CurrentStatus { get; set; } = new SqlCommand()
        {
            CommandType = CommandType.Text,
            CommandText = Properties.Resources.MERGE_fias_CurrentStatus.ToString()
        };
       public SqlCommand MERGE_fias_EstateStatus { get; set; } = new SqlCommand()
        {
            CommandType = CommandType.Text,
            CommandText = Properties.Resources.MERGE_fias_EstateStatus.ToString()
        };
       public SqlCommand MERGE_fias_HouseStateStatus { get; set; } = new SqlCommand()
        {
            CommandType = CommandType.Text,
            CommandText = Properties.Resources.MERGE_fias_HouseStateStatus.ToString()
        };
       public SqlCommand MERGE_fias_House { get; set; } = new SqlCommand()
        {
            CommandTimeout = 0,
            CommandType = CommandType.Text,
            CommandText = Properties.Resources.MERGE_fias_House.ToString()
        };
       public SqlCommand MERGE_fias_NormativeDocumentType { get; set; } = new SqlCommand()
        {
            CommandType = CommandType.Text,
            CommandText = Properties.Resources.MERGE_fias_NormativeDocumentType.ToString()
        };
       public SqlCommand MERGE_fias_NormativeDocument { get; set; } = new SqlCommand()
        {
            CommandTimeout = 0,
            CommandType = CommandType.Text,
            CommandText = Properties.Resources.MERGE_fias_NormativeDocument.ToString()
        };
       public SqlCommand MERGE_fias_Object { get; set; } = new SqlCommand()
        {
            CommandTimeout = 0,
            CommandType = CommandType.Text,
            CommandText = Properties.Resources.MERGE_fias_Object.ToString()
        };
        public SqlCommand MERGE_fias_OperationStatus { get; set; } = new SqlCommand()
        {
            CommandType = CommandType.Text,
            CommandText = Properties.Resources.MERGE_fias_OperationStatus.ToString()
        };
        public SqlCommand MERGE_fias_Room { get; set; } = new SqlCommand()
        {
            CommandTimeout = 0,
            CommandType = CommandType.Text,
            CommandText = Properties.Resources.MERGE_fias_Room.ToString()
        };
        public SqlCommand MERGE_fias_RoomType { get; set; } = new SqlCommand()
        {
            CommandType = CommandType.Text,
            CommandText = Properties.Resources.MERGE_fias_RoomType.ToString()
        };
        public SqlCommand MERGE_fias_Stead { get; set; } = new SqlCommand()
        {
            CommandTimeout = 0,
            CommandType = CommandType.Text,
            CommandText = Properties.Resources.MERGE_fias_Stead.ToString()
        };
        public SqlCommand MERGE_fias_StructureStatus { get; set; } = new SqlCommand()
        {
            CommandType = CommandType.Text,
            CommandText = Properties.Resources.MERGE_fias_StructureStatus.ToString()
        };
        public SqlCommand CreateTempFiasTables { get; set; } = new SqlCommand()
        {
            CommandType = CommandType.Text,
            CommandText = Properties.Resources.CreateTempFiasTables.ToString()
        };
        public SqlCommand DropTempFiasTables { get; set; } = new SqlCommand()
        {
            CommandTimeout = 0,
            CommandType = CommandType.Text,
            CommandText = Properties.Resources.DropTempFiasTables.ToString()
        };
        public SqlCommand MERGE_GlobalTempActualStatus { get; set; } = new SqlCommand()
        {
            CommandType = CommandType.Text,
            CommandText = Properties.Resources.MERGE_GlobalTempActualStatus.ToString()
        };
        public SqlCommand MERGE_GlobalTempAddressObjectType { get; set; } = new SqlCommand()
        {
            CommandType = CommandType.Text,
            CommandText = Properties.Resources.MERGE_GlobalTempAddressObjectType.ToString()
        };
        public SqlCommand MERGE_GlobalTempCenterStatus { get; set; } = new SqlCommand()
        {
            CommandType = CommandType.Text,
            CommandText = Properties.Resources.MERGE_GlobalTempCenterStatus.ToString()
        };
        public SqlCommand MERGE_GlobalTempCurrentStatus { get; set; } = new SqlCommand()
        {
            CommandType = CommandType.Text,
            CommandText = Properties.Resources.MERGE_GlobalTempCurrentStatus.ToString()
        };
        public SqlCommand MERGE_GlobalTempEstateStatus { get; set; } = new SqlCommand()
        {
            CommandType = CommandType.Text,
            CommandText = Properties.Resources.MERGE_GlobalTempEstateStatus.ToString()
        };
        public SqlCommand MERGE_GlobalTempHouse { get; set; } = new SqlCommand()
        {
            CommandTimeout=0,
            CommandType = CommandType.Text,
            CommandText = Properties.Resources.MERGE_GlobalTempHouse.ToString()
        };
        public SqlCommand MERGE_GlobalTempHouseStateStatus { get; set; } = new SqlCommand()
        {
            CommandType = CommandType.Text,
            CommandText = Properties.Resources.MERGE_GlobalTempHouseStateStatus.ToString()
        };
        public SqlCommand MERGE_GlobalTempNormativeDocument { get; set; } = new SqlCommand()
        {
            CommandTimeout =0,
            CommandType = CommandType.Text,
            CommandText = Properties.Resources.MERGE_GlobalTempNormativeDocument.ToString()
        };
        public SqlCommand MERGE_GlobalTempNormativeDocumentType { get; set; } = new SqlCommand()
        {
            CommandType = CommandType.Text,
            CommandText = Properties.Resources.MERGE_GlobalTempNormativeDocumentType.ToString()
        };
        public SqlCommand MERGE_GlobalTempObject { get; set; } = new SqlCommand()
        {
            CommandTimeout = 0,
            CommandType = CommandType.Text,
            CommandText = Properties.Resources.MERGE_GlobalTempObject.ToString()
        };
        public SqlCommand MERGE_GlobalTempOperationStatus { get; set; } = new SqlCommand()
        {
            CommandType = CommandType.Text,
            CommandText = Properties.Resources.MERGE_GlobalTempOperationStatus.ToString()
        };
        public SqlCommand MERGE_GlobalTempRoom { get; set; } = new SqlCommand()
        {
            CommandTimeout = 0,
            CommandType = CommandType.Text,
            CommandText = Properties.Resources.MERGE_GlobalTempRoom.ToString()
        };
        public SqlCommand MERGE_GlobalTempRoomType { get; set; } = new SqlCommand()
        {
            CommandType = CommandType.Text,
            CommandText = Properties.Resources.MERGE_GlobalTempRoomType.ToString()
        };
        public SqlCommand MERGE_GlobalTempStead { get; set; } = new SqlCommand()
        {
            CommandTimeout = 0,
            CommandType = CommandType.Text,
            CommandText = Properties.Resources.MERGE_GlobalTempStead.ToString()
        };
        public SqlCommand MERGE_GlobalTempStructureStatus { get; set; } = new SqlCommand()
        {
            CommandType = CommandType.Text,
            CommandText = Properties.Resources.MERGE_GlobalTempStructureStatus.ToString()
        };
        public SqlCommand MERGE_LocalTempActualStatus { get; set; } = new SqlCommand()
        {
            CommandType = CommandType.Text,
            CommandText = Properties.Resources.MERGE_LocalTempActualStatus.ToString()
        };
        public SqlCommand MERGE_LocalTempAddressObjectType { get; set; } = new SqlCommand()
        {
            CommandType = CommandType.Text,
            CommandText = Properties.Resources.MERGE_LocalTempAddressObjectType.ToString()
        };
        public SqlCommand MERGE_LocalTempCenterStatus { get; set; } = new SqlCommand()
        {
            CommandType = CommandType.Text,
            CommandText = Properties.Resources.MERGE_LocalTempCenterStatus.ToString()
        };
        public SqlCommand MERGE_LocalTempCurrentStatus { get; set; } = new SqlCommand()
        {
            CommandType = CommandType.Text,
            CommandText = Properties.Resources.MERGE_LocalTempCurrentStatus.ToString()
        };
        public SqlCommand MERGE_LocalTempEstateStatus { get; set; } = new SqlCommand()
        {
            CommandType = CommandType.Text,
            CommandText = Properties.Resources.MERGE_LocalTempEstateStatus.ToString()
        };
        public SqlCommand MERGE_LocalTempHouse { get; set; } = new SqlCommand()
        {
            CommandTimeout = 0,
            CommandType = CommandType.Text,
            CommandText = Properties.Resources.MERGE_LocalTempHouse.ToString()
        };
        public SqlCommand MERGE_LocalTempHouseStateStatus { get; set; } = new SqlCommand()
        {
            CommandType = CommandType.Text,
            CommandText = Properties.Resources.MERGE_LocalTempHouseStateStatus.ToString()
        };
        public SqlCommand MERGE_LocalTempNormativeDocument { get; set; } = new SqlCommand()
        {
            CommandTimeout = 0,
            CommandType = CommandType.Text,
            CommandText = Properties.Resources.MERGE_LocalTempNormativeDocument.ToString()
        };
        public SqlCommand MERGE_LocalTempNormativeDocumentType { get; set; } = new SqlCommand()
        {
            CommandType = CommandType.Text,
            CommandText = Properties.Resources.MERGE_LocalTempNormativeDocumentType.ToString()
        };
        public SqlCommand MERGE_LocalTempObject { get; set; } = new SqlCommand()
        {
            CommandTimeout = 0,
            CommandType = CommandType.Text,
            CommandText = Properties.Resources.MERGE_LocalTempObject.ToString()
        };
        public SqlCommand MERGE_LocalTempOperationStatus { get; set; } = new SqlCommand()
        {
            CommandType = CommandType.Text,
            CommandText = Properties.Resources.MERGE_LocalTempOperationStatus.ToString()
        };
        public SqlCommand MERGE_LocalTempRoom { get; set; } = new SqlCommand()
        {
            CommandTimeout = 0,
            CommandType = CommandType.Text,
            CommandText = Properties.Resources.MERGE_LocalTempRoom.ToString()
        };
        public SqlCommand MERGE_LocalTempRoomType { get; set; } = new SqlCommand()
        {
            CommandType = CommandType.Text,
            CommandText = Properties.Resources.MERGE_LocalTempRoomType.ToString()
        };
        public SqlCommand MERGE_LocalTempStead { get; set; } = new SqlCommand()
        {
            CommandTimeout = 0,
            CommandType = CommandType.Text,
            CommandText = Properties.Resources.MERGE_LocalTempStead.ToString()
        };
       public SqlCommand MERGE_LocalTempStructureStatus { get; set; } = new SqlCommand()
        {
            CommandType = CommandType.Text,
            CommandText = Properties.Resources.MERGE_LocalTempStructureStatus.ToString()
        };

        #endregion
        #region Properties
        protected  WebLoader WebLoader = new WebLoader();
        public string[,] patterns = new string[,] { { "ActualStatus", "ACTSTAT.DBF" }, { "CenterStatus", "CENTERST.DBF" }, {"CurrentStatus","CURENTST.DBF"},
            {"EstateStatus","ESTSTAT.DBF"},{"FlatType","FLATTYPE.DBF"},{"IntervalStatus","INTVSTAT.DBF"},{"HouseStateStatus","HSTSTAT.DBF"},
            {"NormativeDocumentType","NDOCTYPE.DBF"},{"OperationStatus","OPERSTAT.DBF"}, {"RoomType","ROOMTYPE.DBF"},{"AddressObjectType","SOCRBASE.DBF"},
            {"StructureStatus","STRSTAT.DBF"},
            { "Object", "ADDROB??.DBF"},{ "House", "HOUSE??.DBF"},{ "NormativeDocument","NORDOC??.DBF"},
            { "Stead", "STEAD??.DBF"},{"Room", "ROOM??.DBF"},
            { "Del_House", "DHOUSE.DBF"}, { "Del_NormativeDocument","DNORDOC.DBF"},{ "Del_Object", "DADDROB.DBF"}
        };
        protected List<List<BulkTableListItem>> bulkLists = new List<List<BulkTableListItem>>();
        #endregion
        #region Constructors

        public FiasOperatorDBF(DirectoryInfo rootdir, SqlConnection connection, string schemaname) : base(rootdir, connection, schemaname)
        {
            var commands = this.GetType()
                .GetProperties()
                .Where(w => w.PropertyType == typeof(SqlCommand)).ToList();
            foreach(var p in commands)
            {
                ((SqlCommand)p.GetValue(this)).Connection = Connection;
            }
            /*ClearTables.Connection = connection;
            CreateTempFiasTables.Connection = Connection;
            MERGE_fias_ActualStatus.Connection = Connection;
            MERGE_fias_AddressObjectType.Connection = Connection;
            MERGE_fias_CenterStatus.Connection = Connection;
            MERGE_fias_CurrentStatus.Connection = Connection;
            MERGE_fias_EstateStatus.Connection = Connection;
            MERGE_fias_HouseStateStatus.Connection = Connection;
            MERGE_fias_NormativeDocumentType.Connection = Connection;
            MERGE_fias_NormativeDocument.Connection = Connection;
            MERGE_fias_OperationStatus.Connection = Connection;
            MERGE_fias_RoomType.Connection = Connection;
            MERGE_fias_StructureStatus.Connection = Connection;
            MERGE_fias_Object.Connection = Connection;
            MERGE_fias_House.Connection = Connection;
            MERGE_fias_Room.Connection = Connection;
            MERGE_fias_Stead.Connection = Connection;
            MERGE_GlobalTempActualStatus.Connection = Connection;
            MERGE_GlobalTempAddressObjectType.Connection = Connection;
            MERGE_GlobalTempCenterStatus.Connection = Connection;
            MERGE_GlobalTempCurrentStatus.Connection = Connection;
            MERGE_GlobalTempEstateStatus.Connection = Connection;
            MERGE_GlobalTempHouseStateStatus.Connection = Connection;
            MERGE_GlobalTempNormativeDocumentType.Connection = Connection;
            MERGE_GlobalTempNormativeDocument.Connection = Connection;
            MERGE_GlobalTempOperationStatus.Connection = Connection;
            MERGE_GlobalTempRoomType.Connection = Connection;
            MERGE_GlobalTempStructureStatus.Connection = Connection;
            MERGE_GlobalTempObject.Connection = Connection;
            MERGE_GlobalTempHouse.Connection = Connection;
            MERGE_GlobalTempRoom.Connection = Connection;
            MERGE_GlobalTempStead.Connection = Connection;
            MERGE_LocalTempActualStatus.Connection = Connection;
            MERGE_LocalTempAddressObjectType.Connection = Connection;
            MERGE_LocalTempCenterStatus.Connection = Connection;
            MERGE_LocalTempCurrentStatus.Connection = Connection;
            MERGE_LocalTempEstateStatus.Connection = Connection;
            MERGE_LocalTempHouseStateStatus.Connection = Connection;
            MERGE_LocalTempNormativeDocumentType.Connection = Connection;
            MERGE_LocalTempNormativeDocument.Connection = Connection;
            MERGE_LocalTempOperationStatus.Connection = Connection;
            MERGE_LocalTempRoomType.Connection = Connection;
            MERGE_LocalTempStructureStatus.Connection = Connection;
            MERGE_LocalTempObject.Connection = Connection;
            MERGE_LocalTempHouse.Connection = Connection;
            MERGE_LocalTempRoom.Connection = Connection;
            MERGE_LocalTempStead.Connection = Connection;*/
        }
        public FiasOperatorDBF(DirectoryInfo rootdir, string connectionString, string schemaname) : this(rootdir, new SqlConnection(connectionString), schemaname)
        {
        }
        #endregion
        #region Methods
        public void BulkLoad(ServerTableType serverTableType)
        {
            try
            {
                SetBulkLists(serverTableType);
                switch (serverTableType)
                {
                    case ServerTableType.Schema:
                        //ClearTables.ExecuteNonQuery();
                        break;
                    case ServerTableType.GlobalTemp:
                        CreateTempFiasTables.ExecuteNonQuery();
                        break;
                    case ServerTableType.ConnectionTemp:
                        break;
                }
                foreach (var bulkList in bulkLists)
                {
                    foreach (BulkTableListItem btlItem in bulkList)
                    {
                        if (btlItem.File != null && btlItem.TableName != null && btlItem.TableSchema != null)
                        {
                            DateTime _start = DateTime.Now;
                            Logger.Logger.Info(btlItem.File.Name + " запущено");
                            btlItem.Load_DBFToDb();
                            DateTime _end = DateTime.Now;
                            Logger.Logger.Info(btlItem.File.Name + " закончено за " + (_end - _start).TotalSeconds.ToString() + " секунд");
                        }
                    }
                }
            }
            finally
            {
                switch (serverTableType)
                {
                    case ServerTableType.Schema:
                        break;
                    case ServerTableType.GlobalTemp:
                        break;
                    case ServerTableType.ConnectionTemp:
                        break;
                }
            }
        }
        public void DownloadFromSite(bool fullDB)
        {
            DownloadFromSite(fullDB, LastDownloadDate);
        }
        public void DownloadFromSite(bool fullDB, DateTime LastDownload)
        {
            var dateStart = DateTime.Now;
            Logger.Logger.Info("Начата загрузка базы ФИАС с сайта ");
            var loaddate = WebLoader.Load(fullDB, Rootdir, LastDownload);
            var dateEnd = DateTime.Now;
            Logger.Logger.Info("Завершена загрузка базы ФИАС с сайта ");
            if (loaddate != null)
            {
                SetLastDownloadDate(fullDB ? "Полная ФИАС" : "Обновление ФИАС", (DateTime)loaddate, true, dateStart, dateEnd);
            }
        }
        public void SetBulkLists(ServerTableType serverTableType)
        {

            List<BulkTableListItem> bulkList = new List<BulkTableListItem>();
            for (int n = 0; n <= (patterns.Length / patterns.Rank - 1); n++)
            {
                foreach (FileInfo file in Rootdir.GetFiles(patterns[n, 1]).OrderBy(file => file.Length))
                {
                    bulkList.Add(new BulkTableListItem(patterns[n, 0], SchemaName, file,serverTableType,Connection));
                }
            }
            bulkLists.Add(bulkList);
        }
        public void Load_DBFToDb(FileInfo dbfFile, string TableName)
        {
            SqlTransaction TRA = Connection.BeginTransaction(("Bulk" + TableName));
            SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(Connection, SqlBulkCopyOptions.Default, TRA)
            {
                BulkCopyTimeout = 60000,
                DestinationTableName = TableName.Contains("#") ? TableName : string.Concat(SchemaName, ".", TableName)
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
            {
                sqlBulkCopy.Close();
            }

        }
        public void MergeDB(ServerTableType serverTableType)
        {
            switch (serverTableType)
            {
                case ServerTableType.Schema:
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
                    MERGE_fias_NormativeDocument.ExecuteNonQuery();
                    MERGE_fias_Object.ExecuteNonQuery();
                    MERGE_fias_House.ExecuteNonQuery();
                    MERGE_fias_Room.ExecuteNonQuery();
                    MERGE_fias_Stead.ExecuteNonQuery();
                    break;
                case ServerTableType.GlobalTemp:
                    MERGE_GlobalTempActualStatus.ExecuteNonQuery();
                    MERGE_GlobalTempAddressObjectType.ExecuteNonQuery();
                    MERGE_GlobalTempCenterStatus.ExecuteNonQuery();
                    MERGE_GlobalTempCurrentStatus.ExecuteNonQuery();
                    MERGE_GlobalTempEstateStatus.ExecuteNonQuery();
                    MERGE_GlobalTempHouseStateStatus.ExecuteNonQuery();
                    MERGE_GlobalTempNormativeDocumentType.ExecuteNonQuery();
                    MERGE_GlobalTempOperationStatus.ExecuteNonQuery();
                    MERGE_GlobalTempRoomType.ExecuteNonQuery();
                    MERGE_GlobalTempStructureStatus.ExecuteNonQuery();
                    MERGE_GlobalTempNormativeDocument.ExecuteNonQuery();
                    MERGE_GlobalTempObject.ExecuteNonQuery();
                    MERGE_GlobalTempHouse.ExecuteNonQuery();
                    MERGE_GlobalTempRoom.ExecuteNonQuery();
                    MERGE_GlobalTempStead.ExecuteNonQuery();
                    break;
                case ServerTableType.ConnectionTemp:
                    MERGE_LocalTempActualStatus.ExecuteNonQuery();
                    MERGE_LocalTempAddressObjectType.ExecuteNonQuery();
                    MERGE_LocalTempCenterStatus.ExecuteNonQuery();
                    MERGE_LocalTempCurrentStatus.ExecuteNonQuery();
                    MERGE_LocalTempEstateStatus.ExecuteNonQuery();
                    MERGE_LocalTempHouseStateStatus.ExecuteNonQuery();
                    MERGE_LocalTempNormativeDocumentType.ExecuteNonQuery();
                    MERGE_LocalTempOperationStatus.ExecuteNonQuery();
                    MERGE_LocalTempRoomType.ExecuteNonQuery();
                    MERGE_LocalTempStructureStatus.ExecuteNonQuery();
                    MERGE_LocalTempNormativeDocument.ExecuteNonQuery();
                    MERGE_LocalTempObject.ExecuteNonQuery();
                    MERGE_LocalTempHouse.ExecuteNonQuery();
                    MERGE_LocalTempRoom.ExecuteNonQuery();
                    MERGE_LocalTempStead.ExecuteNonQuery();
                    break;
            }
        }
        #endregion
    }
}


