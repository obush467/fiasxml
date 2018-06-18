using System;
using System.Data;
using System.IO;
using System.Data.SqlClient;
using fias.SQL.DataSets;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using fias.SQL.DataSets.dsMainTableAdapters;

namespace fias.DBF
{
    class fias_DBF_to_dataset
    {
        
        public SqlConnection Connection { get; set;}
        protected DirectoryInfo _rootdir;
        protected string schemaName;
        protected dsMain ds = new dsMain();
        public fias_DBF_to_dataset(DirectoryInfo rootdir,SqlConnection connection)
        {
            Connection = connection;
            _rootdir = rootdir;
        }
        protected void LogInfo(string message)
        { Console.WriteLine(message); }
        public void loadDBFToDb(FileInfo dbfFile, string TableName,string SchemaName,SqlConnection connection)
        {
            NDbfReader.Table dbfTable = NDbfReader.Table.Open(dbfFile.Open(FileMode.Open));
            NDbfReader.Reader dbfReader = dbfTable.OpenReader(System.Text.Encoding.GetEncoding(866));
            SqlTransaction TRA = connection.BeginTransaction(("Bulk" + TableName));
            SqlBulkCopy da = new SqlBulkCopy(connection, SqlBulkCopyOptions.Default, TRA);
            da.BulkCopyTimeout = 60000;
            da.DestinationTableName = string.Concat(SchemaName,".",TableName);        
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
        public void loadDBFToDb_House(FileInfo dbfFile, SqlConnection connection)
        {
            NDbfReader.Table dbfTable = NDbfReader.Table.Open(dbfFile.Open(FileMode.Open));
            NDbfReader.Reader dbfReader = dbfTable.OpenReader(System.Text.Encoding.GetEncoding(866));
            QueriesTableAdapter qta = new QueriesTableAdapter();
            int c = 1;
            guidparse gp = (string x) => { if (x == null) { return null; } else { return Guid.Parse(x);}};
            while (dbfReader.Read())
            {
                int ESTSTATUS;
                int.TryParse(dbfReader.GetValue("ESTSTATUS").ToString(), out ESTSTATUS);
                if(!(bool)qta.CanInsert_tmpHouse_Query(Guid.Parse(dbfReader.GetValue("HOUSEID").ToString()))
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
        

         public void Load(string SchemaName)
{
    var task1 = Task.Factory.StartNew(() =>
    {
        using (SqlConnection _connection = Connection)
        {
            _connection.Open();
            List<BulkTableListItem> bulkList = new List<BulkTableListItem>();
            try
            {

                for (int i = 57; i <= 99; i++)
                {
                    FileInfo[] flist = (_rootdir).GetFiles("HOUSE" + i.ToString() + ".DBF");
                    if (flist != null && flist.Count() > 0)
                    {
                        FileInfo f = flist[0];
                        bulkList.Add(new BulkTableListItem("House", SchemaName, f));
                    }
                }
                foreach (BulkTableListItem bi in bulkList)
                {
                    DateTime _start = DateTime.Now;
                    LogInfo(bi.File.Name + " запущено");
                    loadDBFToDb(bi.File, bi.TableName, SchemaName, _connection);
                    //loadDBFToDb_House(bi.File, _connection);
                    DateTime _end = DateTime.Now;
                    LogInfo(bi.File.Name + " закончено за " + (_end - _start).TotalSeconds.ToString() + " секунд");
                }
            }
            finally
            {
                _connection.Close();
            }
        }
    });

           /* var task2 = Task.Factory.StartNew(() =>
            {
                using (SqlConnection _connection = new SqlConnection(ConnectionString))
                {
                    _connection.Open();
                    List<BulkTableListItem> bulkList = new List<BulkTableListItem>();
                    try
                    {
                        foreach (FileInfo f in (rootdir).GetFiles("ROOM??.DBF"))
                        {
                            bulkList.Add(new BulkTableListItem("Room", "fias_tmp", f));
                        }
                        foreach (BulkTableListItem bi in bulkList)
                        {
                            DateTime _start = DateTime.Now;
                            LogInfo(bi.File.Name + " запущено");
                            loadDBFToDb(bi.File, bi.TableName,_connection);
                            DateTime _end = DateTime.Now;
                            LogInfo(bi.File.Name + " закончено за " + (_end - _start).TotalSeconds.ToString() + " секунд");
                        }
                    }
                    finally
                    {
                        _connection.Close();
                    }
                }
            });


            var task3 = Task.Factory.StartNew(() =>
            {
                using (SqlConnection _connection = new SqlConnection(ConnectionString))
                {
                    _connection.Open();
                    List<BulkTableListItem> bulkList = new List<BulkTableListItem>();
                    try
                    {
                        foreach (FileInfo f in (rootdir).GetFiles("STEAD??.DBF"))
                        {
                            bulkList.Add(new BulkTableListItem("Stead", "fias_tmp", f));
                        }
                        foreach (BulkTableListItem bi in bulkList)
                        {
                            DateTime _start = DateTime.Now;
                            LogInfo(bi.File.Name + " запущено");
                            loadDBFToDb(bi.File, bi.TableName,_connection);
                            DateTime _end = DateTime.Now;
                            LogInfo(bi.File.Name + " закончено за " + (_end - _start).TotalSeconds.ToString() + " секунд");
                        }
                    }
                    finally
                    {
                        _connection.Close();
                    }
                }
            });*/
            task1.Wait();
           // task2.Wait();
           // task3.Wait();

        }


        /*foreach (FileInfo f in rootdir.GetFiles("*"))
        {
            fiasXMLDataSetConverter.loadXmlToDb_NormativeDocumentTypes(f.FullName);
        }*/

        //bulkList.Add(new BulkTableListItem("ActualStatus", "fias_tmp", (rootdir).GetFiles("ACTSTAT.DBF")[0]));
        //bulkList.Add(new BulkTableListItem("CenterStatus", "fias_tmp", (rootdir).GetFiles("CENTERST.DBF")[0]));
        //bulkList.Add(new BulkTableListItem("CurrentStatus", "fias_tmp", (rootdir).GetFiles("CURENTST.DBF")[0]));
        //bulkList.Add(new BulkTableListItem("EstateStatus", "fias_tmp", (rootdir).GetFiles("ESTSTAT.DBF")[0]));
        //bulkList.Add(new BulkTableListItem("FlatType", "fias_tmp", (rootdir).GetFiles("FLATTYPE.DBF")[0]));
        //bulkList.Add(new BulkTableListItem("HouseStateStatus", "fias_tmp", (rootdir).GetFiles("HSTSTAT.DBF")[0]));
        //bulkList.Add(new BulkTableListItem("IntervalStatus", "fias_tmp", (rootdir).GetFiles("INTVSTAT.DBF")[0]));
        //bulkList.Add(new BulkTableListItem("NormativeDocumentType", "fias_tmp", (rootdir).GetFiles("NDOCTYPE.DBF")[0]));
        //bulkList.Add(new BulkTableListItem("OperationStatus", "fias_tmp", (rootdir).GetFiles("OPERSTAT.DBF")[0]));
        //bulkList.Add(new BulkTableListItem("RoomType", "fias_tmp", (rootdir).GetFiles("ROOMTYPE.DBF")[0]));
        //bulkList.Add(new BulkTableListItem("AddressObjectType", "fias_tmp", (rootdir).GetFiles("SOCRBASE.DBF")[0]));
        //bulkList.Add(new BulkTableListItem("StructureStatus", "fias_tmp", (rootdir).GetFiles("STRSTAT.DBF")[0]));

        //foreach (FileInfo f in (rootdir).GetFiles("NORDOC??.DBF"))
        //{
        //   //  bulkList.Add(new BulkTableListItem("NormativeDocument", "fias_tmp", f));
        // }
        // foreach (FileInfo f in (rootdir).GetFiles("ADDROB??.DBF"))
        // {
        //     bulkList.Add(new BulkTableListItem("Object", "fias_tmp", f));
        // }



        /* foreach (FileInfo f in (rootdir).GetFiles("HOUSE??.DBF"))
         {
             bulkList.Add(new BulkTableListItem("House", "fias_tmp", f));
         }*/



    }
}
    


