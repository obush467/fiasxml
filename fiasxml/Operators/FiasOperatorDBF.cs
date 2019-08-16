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
        public string[,] patterns = new string[,] { { "ActualStatus", "ACTSTAT.DBF" }, { "CenterStatus", "CENTERST.DBF" }, {"CurrentStatus","CURENTST.DBF"},
            {"EstateStatus","ESTSTAT.DBF"},{"FlatType","FLATTYPE.DBF"},{"IntervalStatus","INTVSTAT.DBF"},{"HouseStateStatus","HSTSTAT.DBF"},
            {"NormativeDocumentType","NDOCTYPE.DBF"},{"OperationStatus","OPERSTAT.DBF"}, {"RoomType","ROOMTYPE.DBF"},{"AddressObjectType","SOCRBASE.DBF"},
            {"StructureStatus","STRSTAT.DBF"},
            //{ "Del_House", "DHOUSE.DBF"}, { "Del_NormativeDocument","DNORDOC.DBF"},{ "Del_Object", "DADDROB.DBF"},
            { "House", "HOUSE??.DBF"},{ "NormativeDocument","NORDOC??.DBF"}, { "Object", "ADDROB??.DBF"},
            { "Stead", "STEAD??.DBF"},{"Room", "ROOM??.DBF"}};

        protected List<List<BulkTableListItem>> bulkLists = new List<List<BulkTableListItem>>();
        public FiasOperatorDBF(DirectoryInfo rootdir, SqlConnection connection, string schemaname) : base(rootdir, connection, schemaname)
        {
            SetBulkLists();
        }
        public FiasOperatorDBF(DirectoryInfo rootdir, string connectionString, string schemaname) : base(rootdir, connectionString, schemaname)
        {
            SetBulkLists();
        }

        public void LoadToDb(FileInfo dbfFile, string TableName, string schemaName, SqlConnection connection)
        {
            SqlTransaction TRA = connection.BeginTransaction(("Bulk" + TableName));
            SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(connection, SqlBulkCopyOptions.Default, TRA)
            {
                BulkCopyTimeout = 60000,
                DestinationTableName = string.Concat(SchemaName, ".", TableName)
            };
            DataTable servertable = ds.Tables[TableName];
            try
            {
                var errors = new List<object>();
                using (NDbfReader.Table dbfTable = NDbfReader.Table.Open(dbfFile.Open(FileMode.Open)))
                {
                    var n = 1;
                    var counter = 10000;
                    List<DataRow> rows = new List<DataRow>();
                    NDbfReader.Reader dbfReader = dbfTable.OpenReader(Encoding.GetEncoding(866));
                    while (dbfReader.Read())
                    {
                        try
                        {
                            DataRow newrow = servertable.NewRow();
                            foreach (NDbfReader.Column c in dbfTable.Columns)
                                newrow.SetField(c.Name, dbfReader.GetValue(c.Name));
                            rows.Add(newrow);
                        }
                        catch (Exception e)
                        {
                            LogInfo("Ошибка преобразования " + e.Message);
                            errors.Add(e);
                        }
                        if (n == counter)
                        {
                            sqlBulkCopy.WriteToServer(rows.ToArray());
                            //TRA.Commit();
                            rows.Clear();
                            n = 0;
                        }
                        n++;
                    }
                    sqlBulkCopy.WriteToServer(rows.ToArray());
                    TRA.Commit();
                }
                if (errors.Count == 0)
                    dbfFile.Delete();
            }
            finally
            { }

        }

        delegate Guid? guidparse(string x);
        public void Load(string SchemaName)
        {
            var options = new ParallelOptions();
            options.MaxDegreeOfParallelism = 10;
            foreach (var bulkList in bulkLists)
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();
                    try
                    {
                        foreach (BulkTableListItem btlItem in bulkList)
                        {
                            if (btlItem.File != null && btlItem.TableName != null && btlItem.TableSchema != null)
                            {
                                DateTime _start = DateTime.Now;
                                LogInfo(btlItem.File.Name + " запущено");
                                LoadToDb1(btlItem.File, btlItem.TableName, SchemaName, connection);
                                DateTime _end = DateTime.Now;
                                LogInfo(btlItem.File.Name + " закончено за " + (_end - _start).TotalSeconds.ToString() + " секунд");
                            }
                        }
                    }
                    finally
                    {
                        connection.Close();
                    }
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
        public void LoadToDb1(FileInfo dbfFile, string TableName, string schemaName, SqlConnection connection)
        {
            SqlTransaction TRA = connection.BeginTransaction(("Bulk" + TableName));
            SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(connection, SqlBulkCopyOptions.Default, TRA)
            {
                BulkCopyTimeout = 60000,
                DestinationTableName = string.Concat(schemaName, ".", TableName)
            };
            DataTable servertable = ds.Tables[TableName];
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
            dsMain dataset = new dsMain();
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


