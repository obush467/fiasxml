using System;
using System.Data;
using System.IO;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Text;
using Fias.DataSets;
using Fias.DataSets.dsMainTableAdapters;
using DbfDataReader;
using Microsoft.Extensions.Options;
using System.Diagnostics;

namespace Fias.Operators
{
    public class FiasOperatorDBF : FiasOperator
    {
        dsMain ds = new dsMain();

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
            SetBulkLists();
        }
        public FiasOperatorDBF(DirectoryInfo rootdir, string connectionString, string schemaname) : base(rootdir, connectionString, schemaname)
        {
            SetBulkLists();
        }

        
        delegate Guid? guidparse(string x);
        public void Load()
        {
            var options = new ParallelOptions();
            options.MaxDegreeOfParallelism = 10;
            foreach (var bulkList in bulkLists)
            {
                    Connection.Open();
                    try
                    {
                        foreach (BulkTableListItem btlItem in bulkList)
                        {
                            if (btlItem.File != null && btlItem.TableName != null && btlItem.TableSchema != null)
                            {
                                DateTime _start = DateTime.Now;
                                LogInfo(btlItem.File.Name + " запущено");
                                LoadToDb1(btlItem.File, btlItem.TableName);
                                DateTime _end = DateTime.Now;
                                LogInfo(btlItem.File.Name + " закончено за " + (_end - _start).TotalSeconds.ToString() + " секунд");
                            }
                        }
                    }
                    finally
                    {
                        Connection.Close();
                    }
                
            };
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
        public void LoadToDb1(FileInfo dbfFile, string TableName)
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
                    var n = 1;
                    var counter = 10000;
                    List<DataRow> rows = new List<DataRow>();
                    DBFDataReader dbfReader = new DBFDataReader(dbfTable,Encoding.GetEncoding(866));
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
        public void MergeTmp()
        {
            QueriesTableAdapter queriesTableAdapter = new QueriesTableAdapter();
            queriesTableAdapter.MergeActualStatusQuery();
            queriesTableAdapter.MergeAddressObjectTypeQuery();
            queriesTableAdapter.MergeCenterStatusQuery();
            queriesTableAdapter.MergeCurrentStatusQuery();
            queriesTableAdapter.MergeEstateStatusQuery();
            queriesTableAdapter.MergeHouseStateStatusQuery();
            queriesTableAdapter.MergeNormativeDocumentTypeQuery();
            queriesTableAdapter.MergeOperationStatusQuery();
            queriesTableAdapter.MergeRoomTypeQuery();
            queriesTableAdapter.MergeStructureStatusQuery();
            queriesTableAdapter.MergeAddressObjectsQuery();
            queriesTableAdapter.MergeHouseQuery();
        }
    }
}


