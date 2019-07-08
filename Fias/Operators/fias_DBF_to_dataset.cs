using System;
using System.Data;
using System.IO;
using System.Data.SqlClient;
using Fias.SQL.DataSets;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Text;
using Fias.DataSets;
using Fias.DataSets.dsMainTableAdapters;

namespace Fias.Operators
{
    public class FiasOperatorDBF : FiasOperator
    {
        public string[,] patterns =new string[,] { { "ActualStatus", "ACTSTAT.DBF" }, { "CenterStatus", "CENTERST.DBF" }, {"CurrentStatus","CURENTST.DBF"},
            {"EstateStatus","ESTSTAT.DBF"},{"FlatType","FLATTYPE.DBF"},{"IntervalStatus","INTVSTAT.DBF"},{"HouseStateStatus","HSTSTAT.DBF"},
            {"NormativeDocumentType","NDOCTYPE.DBF"},{"OperationStatus","OPERSTAT.DBF"}, {"RoomType","ROOMTYPE.DBF"},{"AddressObjectType","SOCRBASE.DBF"},
            {"StructureStatus","STRSTAT.DBF"}, { "Del_House", "DHOUSE.DBF"}, { "Del_NormativeDocument","DNORDOC.DBF"},{ "Del_Object", "DADDROB.DBF"},
            { "House", "HOUSE??.DBF"},{ "NormativeDocument","NORDOC??.DBF"}, { "Object", "ADDROB??.DBF"}, 
            { "Stead", "STEAD??.DBF"},{"Room", "ROOM??.DBF"}};

        protected dsMain ds = new dsMain();
        protected List<List<BulkTableListItem>> bulkLists = new List<List<BulkTableListItem>>();
        public FiasOperatorDBF(DirectoryInfo rootdir, SqlConnection connection, string schemaname):base(rootdir,connection,schemaname)
        {
            SetBulkLists();
        }

        public void LoadToDb(FileInfo dbfFile, string TableName, SqlConnection connection)
        {
            NDbfReader.Table dbfTable = NDbfReader.Table.Open(dbfFile.Open(FileMode.Open));
            NDbfReader.Reader dbfReader = dbfTable.OpenReader(Encoding.GetEncoding(866));
            SqlTransaction TRA = connection.BeginTransaction(("Bulk" + TableName));
            SqlBulkCopy da = new SqlBulkCopy(connection, SqlBulkCopyOptions.Default, TRA);
            da.BulkCopyTimeout = 60000;
            da.DestinationTableName = string.Concat(SchemaName, ".", TableName);
            DataTable servertable = ds.Tables[TableName];
            List<DataRow> rows = new List<DataRow>();
            while (dbfReader.Read())
            {
                DataRow newrow = servertable.NewRow();
                foreach (NDbfReader.Column c in dbfTable.Columns)
                {
                    newrow.SetField(c.Name, dbfReader.GetValue(c.Name));
                }
                rows.Add(newrow);
            }
            da.WriteToServer(rows.ToArray());
            TRA.Commit();
            dbfTable.Dispose();
            dbfFile.Delete();
        }
        delegate Guid? guidparse(string x);
        public void LoadDBFToDb_House(FileInfo dbfFile, SqlConnection connection)
        {
            NDbfReader.Table dbfTable = NDbfReader.Table.Open(dbfFile.Open(FileMode.Open));
            NDbfReader.Reader dbfReader = dbfTable.OpenReader(Encoding.GetEncoding(866));
            QueriesTableAdapter qta = new QueriesTableAdapter();
            int c = 1;
            guidparse gp = (string x) => { if (x == null) { return null; } else { return Guid.Parse(x); } };
            while (dbfReader.Read())
            {
                int ESTSTATUS;
                int.TryParse(dbfReader.GetValue("ESTSTATUS").ToString(), out ESTSTATUS);
                if (!(bool)qta.CanInsert_tmpHouse_Query(Guid.Parse(dbfReader.GetValue("HOUSEID").ToString()))
                   && (bool)qta.CanInsert_tmpHouseAO_Query(Guid.Parse(dbfReader.GetValue("AOGUID").ToString()))
                    )
                    qta.Insert_tmpHouse_Query(
                        dbfReader.GetString("POSTALCODE"),
                        dbfReader.GetString("IFNSFL"),
                        dbfReader.GetString("TERRIFNSFL"),
                        dbfReader.GetString("IFNSUL"),
                        dbfReader.GetString("TERRIFNSUL"),
                        dbfReader.GetString("OKATO"),
                        dbfReader.GetString("OKTMO"),
                        dbfReader.GetDate("UPDATEDATE"),
                        dbfReader.GetString("HOUSENUM"),
                        int.Parse(dbfReader.GetValue("ESTSTATUS").ToString()),
                        dbfReader.GetString("BUILDNUM"),
                        dbfReader.GetString("STRUCNUM"),
                        int.Parse(dbfReader.GetValue("STRSTATUS").ToString()),
                        Guid.Parse(dbfReader.GetValue("HOUSEID").ToString()),
                        Guid.Parse(dbfReader.GetValue("HOUSEGUID").ToString()),
                        Guid.Parse(dbfReader.GetValue("AOGUID").ToString()),
                        dbfReader.GetDate("STARTDATE").Value,
                        dbfReader.GetDate("ENDDATE").Value,
                        int.Parse(dbfReader.GetValue("STATSTATUS").ToString()),
                        gp(dbfReader.GetString("NORMDOC")),
                        int.Parse(dbfReader.GetValue("COUNTER").ToString()),
                        dbfReader.GetString("CADNUM"),
                        int.Parse(dbfReader.GetValue("DIVTYPE").ToString()));
                c++;
                LogInfo(c.ToString() + ". " + Guid.Parse(dbfReader.GetValue("HOUSEID").ToString()));
            }
            dbfTable.Dispose();
            dbfFile.Delete();
        }
/// <summary>
/// 
/// </summary>
        public void Load()
        {
            List<Task> taskList = new List<Task>();            
            foreach (List<BulkTableListItem> bulkList in bulkLists)
            {
                bulkList.Sort(CompareBulkTableListItem);
                taskList.Add(Task.Factory.StartNew(() =>
                    {
                        using (SqlConnection _connection = new SqlConnection(ConnectionString))
                        {
                            _connection.Open();
                            try
                            {
                                foreach (BulkTableListItem btlItem in bulkList)
                                {
                                    if (btlItem.File != null && btlItem.TableName != null && btlItem.TableSchema != null)
                                    {
                                        DateTime _start = DateTime.Now;
                                        LogInfo(btlItem.File.Name + " запущено");
                                        LoadToDb(btlItem.File, btlItem.TableName, _connection);
                                        DateTime _end = DateTime.Now;
                                        LogInfo(btlItem.File.Name + " закончено за " + (_end - _start).TotalSeconds.ToString() + " секунд");
                                    }
                                }
                            }
                            finally
                            {
                                _connection.Close();
                            }
                        }
                    }));
            }
           
            foreach (Task task in taskList)
            { task.Wait();}
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
            //bulkLists.Sort(CompareBulkTableListItem);
        }

        public static int CompareBulkTableListItem(BulkTableListItem c1, BulkTableListItem c2)
            { if (c1.File.Length == c1.File.Length) { return 0; }
            else
                if (c1.File.Length > c1.File.Length) { return 1; }
            else { return -1; }
        }
    }
}

    


