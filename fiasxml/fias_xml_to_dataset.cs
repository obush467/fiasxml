using System;
using System.Data;
using System.IO;
using System.Xml;
using System.Data.SqlClient;
using System.Xml.Schema;
using System.Collections.Generic;
using fias.XML.DataSets;
using System.Threading.Tasks;
using System.Linq;
using fias.SQL.DataSets;
using fias.DataSets.dsMainTableAdapters;
using fias.DataSets;

namespace fias.XML
{
    delegate void log(string message);
    class fias_XML_to_dataset
    {
        
        protected SqlConnection Connection { get; set;}
        DirectoryInfo _rootdir;      
        log LogInfo;
        log LogDebug;
        log LogWarning;       
        public fias_XML_to_dataset(DirectoryInfo rootdir, SqlConnection connection)
        {
            Connection = connection;
            _rootdir = rootdir;
            LogInfo = (string message) => { Console.WriteLine(message); };
            LogDebug = (string message) => { Console.WriteLine(message); };
            LogWarning= (string message) => { Console.WriteLine(message); };
        }
        public void loadXmlToDb_Stead(string FileName)
        {
            FileStream ws;
            ws = File.OpenRead(FileName);
            Steads cs = new Steads();
            loadXmlToDb(ws, cs.Stead, "dbo.Stead");
        }
        public void loadXmlToDb_Room(string FileName)
        {
            FileStream ws;
            ws = File.OpenRead(FileName);
            BufferedStream buffered = new BufferedStream(ws, 1000000000);
            Rooms cs = new Rooms();
            loadXmlToDb(buffered, cs.Room, "dbo.Room");
        }
        public void loadXmlToDb_NormativeDocumentTypes(string FileName)
        {
            FileStream ws;
            ws = File.OpenRead(FileName);
            NormativeDocumentTypes cs = new NormativeDocumentTypes();
            loadXmlToDb(ws, cs.NormativeDocumentType, "dbo.NormativeDocumentType");
        }
        public void loadXmlToDb_NormativeDocument(string FileName)
        {
            FileStream ws;
            ws = File.OpenRead(FileName);
            NormativeDocumentes cs = new NormativeDocumentes();
            loadXmlToDb(ws, cs.NormativeDocument, "dbo.NormativeDocument");
        }
        public void loadXmlToDb_Landmark(string FileName)
        {
            FileStream ws;
            ws = File.OpenRead(FileName);
            Landmarks cs = new Landmarks();
            loadXmlToDb(ws, cs.Landmark, "dbo.Landmark");
        }
        public void loadXmlToDb_Landmark(string FileName, string SchemeName)
        {
            FileStream ws;
            ws = File.OpenRead(FileName);
            Landmarks cs = new Landmarks();
            loadXmlToDb(FileName, SchemeName, cs.Landmark, "dbo.Landmark");
        }
        public void loadXmlToDb_StructureStatus(string FileName)
        {
            FileStream ws;
            ws = File.OpenRead(FileName);
            StructureStatuses cs = new StructureStatuses();
            loadXmlToDb(ws, cs.StructureStatus, "dbo.StructureStatus");
        }
        public void loadXmlToDb_RoomTypes(string FileName)
        {
            FileStream ws;
            ws = File.OpenRead(FileName);
            OperationStatuses cs = new OperationStatuses();
            loadXmlToDb(ws, cs.OperationStatus, "dbo.RoomType");
        }

        public void loadXmlToDb_FlatTypes(string FileName)
        {
            FileStream ws;
            ws = File.OpenRead(FileName);
            OperationStatuses cs = new OperationStatuses();
            loadXmlToDb(ws, cs.OperationStatus, "dbo.FlatType");
        }

        public void loadXmlToDb_OperationStatus(string FileName)
        {
            FileStream ws;
            ws = File.OpenRead(FileName);
            OperationStatuses cs = new OperationStatuses();
            loadXmlToDb(ws, cs.OperationStatus, "dbo.OperationStatus");
        }
        public void loadXmlToDb_IntervalStatus(string FileName)
        {
            FileStream ws;
            ws = File.OpenRead(FileName);
            IntervalStatuses cs = new IntervalStatuses();
            loadXmlToDb(ws, cs.IntervalStatus, "dbo.IntervalStatus");
        }
        public void loadXmlToDb_HouseStateStatus(string FileName)
        {
            FileStream ws;
            ws = File.OpenRead(FileName);
            HouseStateStatuses cs = new HouseStateStatuses();
            loadXmlToDb(ws, cs.HouseStateStatus, "dbo.HouseStateStatus");
        }
        public void loadXmlToDb_HousesInterval(string FileName)
        {
            FileStream ws;
            ws = File.OpenRead(FileName);
            HouseIntervals cs = new HouseIntervals();
            loadXmlToDb(ws, cs.HouseInterval, "dbo.HouseInterval");
        }
        public void loadXmlToDb_AddressObjectTypes(string FileName)
        {
            FileStream ws;
            ws = File.OpenRead(FileName);
            AddressObjectTypes cs = new AddressObjectTypes();
            loadXmlToDb(ws, cs.AddressObjectType, "dbo.AddressObjectType");
        }
        public void loadXmlToDb_Houses(string FileName)
        {
            FileStream ws;
            ws = File.OpenRead(FileName);
            Houses cs = new Houses();
            loadXmlToDb(ws, cs.House, "dbo.House");
        }
        public void loadXmlToDb_CenterStatuses(string FileName)
        {
            FileStream ws;
            ws = File.OpenRead(FileName);
            CenterStatuses cs = new CenterStatuses();
            loadXmlToDb(ws, cs.CenterStatus, "dbo.CenterStatus");
        }
        public void loadXmlToDb_EstateStatuses(string FileName)
        {
            FileStream ws;
            ws = File.OpenRead(FileName);
            EstateStatuses cs = new EstateStatuses();
            loadXmlToDb(ws, cs.EstateStatus, "dbo.EstateStatus");
        }
        public void loadXmlToDb_CurrentStatus(string FileName)
        {
            FileStream ws;
            ws = File.OpenRead(FileName);
            CurrentStatuses cs = new CurrentStatuses();
            loadXmlToDb(ws, cs.CurrentStatus, "dbo.CurrentStatus");
        }

        public void loadXmlToDb(string wFile, DataTable wTable, string TableName)
        {
            XmlReader wReader = XmlReader.Create(wFile);
            loadXmlToDb(wReader, wTable, TableName);
        }

        public void loadXmlToDb(string wFile, string wScheme, DataTable wTable, string TableName)
        {
            XmlReader wReader;
            if (wScheme != "")
            {
                XmlReaderSettings wSettings = new XmlReaderSettings();
                wSettings.Schemas.Add("http://www.fias.ru/schemes", wScheme);
                wSettings.ValidationType = ValidationType.Schema;
                wSettings.ValidationEventHandler += new System.Xml.Schema.ValidationEventHandler(SettingsValidationEventHandler);
                wReader = XmlReader.Create(wFile, wSettings);
            }
            else
            {
                wReader = XmlReader.Create(wFile);
            }
            loadXmlToDb(wReader, wTable, TableName);
        }
        void SettingsValidationEventHandler(object sender, ValidationEventArgs e)
        {
            if (e.Severity == XmlSeverityType.Warning)
            {
                Console.Write("Предупреждаю!: ");
                LogInfo(e.Message);
            }
            else if (e.Severity == XmlSeverityType.Error)
            {
                Console.Write("Ошибочка вышла: ");
                LogInfo(e.Message);
            }
        }

        public void loadXmlToDb(Stream wStream, DataTable wTable, string TableName)
        {
            XmlReader wReader = XmlReader.Create(wStream);
            this.loadXmlToDb(wReader, wTable, TableName);
        }
        public void loadDBFToDb(FileInfo dbfFile, string TableName, SqlConnection connection)
        {
            NDbfReader.Table dbfTable = NDbfReader.Table.Open(dbfFile.Open(FileMode.Open));
            NDbfReader.Reader dbfReader = dbfTable.OpenReader(System.Text.Encoding.GetEncoding(866));
            SqlTransaction TRA = connection.BeginTransaction(("Bulk" + TableName));
            SqlBulkCopy da = new SqlBulkCopy(connection, SqlBulkCopyOptions.Default, TRA);
            da.BulkCopyTimeout = 60000;
            da.DestinationTableName = "fias_tmp." + TableName;
            dsMain ds = new dsMain();
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
        delegate Nullable<Guid> guidparse(string x);
        public void loadDBFToDb_House(FileInfo dbfFile, SqlConnection connection)
        {
            NDbfReader.Table dbfTable = NDbfReader.Table.Open(dbfFile.Open(FileMode.Open));
            NDbfReader.Reader dbfReader = dbfTable.OpenReader(System.Text.Encoding.GetEncoding(866));
            QueriesTableAdapter qta = new QueriesTableAdapter();
            int c = 1;
            guidparse gp = (string x) => { if (x == null) { return null; } else { return Guid.Parse(x);}};
            while (dbfReader.Read())
            {
                Int32 ESTSTATUS;
                Int32.TryParse(dbfReader.GetValue("ESTSTATUS").ToString(), out ESTSTATUS);
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
                    Int32.Parse(dbfReader.GetValue("ESTSTATUS").ToString()),
                    dbfReader.GetString("BUILDNUM"),
                    dbfReader.GetString("STRUCNUM"),
                    Int32.Parse(dbfReader.GetValue("STRSTATUS").ToString()),
                    Guid.Parse(dbfReader.GetValue("HOUSEID").ToString()),
                    Guid.Parse(dbfReader.GetValue("HOUSEGUID").ToString()),
                    Guid.Parse(dbfReader.GetValue("AOGUID").ToString()),
                    dbfReader.GetDate("STARTDATE").Value,
                    dbfReader.GetDate("ENDDATE").Value,
                    Int32.Parse(dbfReader.GetValue("STATSTATUS").ToString()),
                    gp(dbfReader.GetString("NORMDOC")),
                    Int32.Parse(dbfReader.GetValue("COUNTER").ToString()),
                    dbfReader.GetString("CADNUM"),
                    Int32.Parse(dbfReader.GetValue("DIVTYPE").ToString()));
                c++;
                LogInfo(c.ToString() + ". " + Guid.Parse(dbfReader.GetValue("HOUSEID").ToString()));
            }
            
            dbfTable.Dispose();
            dbfFile.Delete();
        }
        public void loadXmlToDb(XmlReader wReader, DataTable wTable, string TableName)
        {
            wTable.BeginLoadData();
            wTable.ReadXml(wReader);
            wTable.EndLoadData();
            SqlTransaction TRA = Connection.BeginTransaction(("Bulk" + TableName));
            SqlBulkCopy da = new SqlBulkCopy(Connection, SqlBulkCopyOptions.Default, TRA);
            da.BulkCopyTimeout = 600;
            da.DestinationTableName = TableName;
            try
            {
                LogInfo("Запись в базу данных");
                da.WriteToServer(wTable);
                LogInfo("Завершение транзакции");
                TRA.Commit();
                LogInfo("Завершение транзакции успешно");
                LogInfo("Внесено дохера строк");
            }
            catch (Exception ex)
            {
                LogInfo("Commit Exception Type: {0}" + ex.GetType());
                LogInfo("  Message: {0}" + ex.Message);

                // Attempt to roll back the transaction.
                try
                {
                    TRA.Rollback();
                }
                catch (Exception ex2)
                {
                    // This catch block will handle any errors that may have occurred
                    // on the server that would cause the rollback to fail, such as
                    // a closed connection.
                    LogInfo("Rollback Exception Type: {0}" + ex2.GetType());
                    LogInfo("  Message: {0}" + ex2.Message);
                }
            }
        }

        public void loadXmlToDb_ActualStatus(string FileName)
        {
            FileStream ws;
            ws = File.OpenRead(FileName);
            ActualStatuses cs = new ActualStatuses();
            loadXmlToDb(ws, cs.ActualStatus, "dbo.ActualStatus");
        }


        public void loadXmlToDb_AddressObjects(string FileName)
        {
            FileStream ws;
            ws = File.OpenRead(FileName);
            AddressObjects cs = new AddressObjects();
            loadXmlToDb(ws, cs.Object, "dbo.Object");
        }


        public void LoadXML() {
            //загрузка вспомогательных таблиц
            /* foreach (FileInfo f in rootdir.GetFiles("AS_ACTSTAT*"))
             { fiasXMLDataSetConverter.loadXmlToDb_ActualStatus(f.FullName); }

             foreach (FileInfo f in rootdir.GetFiles("AS_ESTSTAT_*"))
             { fiasXMLDataSetConverter.loadXmlToDb_EstateStatuses(f.FullName); }

             foreach (FileInfo f in rootdir.GetFiles("AS_CENTERST*"))
             { fiasXMLDataSetConverter.loadXmlToDb_CenterStatuses(f.FullName); }

             foreach (FileInfo f in rootdir.GetFiles("AS_CURENTST*"))
             { fiasXMLDataSetConverter.loadXmlToDb_CurrentStatus(f.FullName); }

             foreach (FileInfo f in rootdir.GetFiles("AS_HSTSTAT*"))
             {fiasXMLDataSetConverter.loadXmlToDb_HouseStateStatus(f.FullName);}

             foreach (FileInfo f in rootdir.GetFiles("AS_INTVSTAT*"))
             { fiasXMLDataSetConverter.loadXmlToDb_IntervalStatus(f.FullName); }

             foreach (FileInfo f in rootdir.GetFiles("AS_OPERSTAT*"))

             { fiasXMLDataSetConverter.loadXmlToDb_OperationStatus(f.FullName); }

             foreach (FileInfo f in rootdir.GetFiles("AS_STRSTAT*"))
             { fiasXMLDataSetConverter.loadXmlToDb_StructureStatus(f.FullName); }

             foreach (FileInfo f in rootdir.GetFiles("AS_SOCRBASE*"))
             { fiasXMLDataSetConverter.loadXmlToDb_AddressObjectTypes(f.FullName); }

             foreach (FileInfo f in rootdir.GetFiles("AS_NDOCTYPE*"))
             { fiasXMLDataSetConverter.loadXmlToDb_NormativeDocumentTypes(f.FullName); }

             foreach (FileInfo f in rootdir.GetFiles("AS_ROOMTYPE*"))
             {
                 fiasXMLDataSetConverter.loadXmlToDb_RoomTypes(f.FullName);
             }
             foreach (FileInfo f in rootdir.GetFiles("AS_FLATTYPE*"))
             {
                 fiasXMLDataSetConverter.loadXmlToDb_FlatTypes(f.FullName);
             }

             //загрузка основных таблиц

             foreach (FileInfo f in rootdir.GetFiles("AS_ADDROBJ*"))
             { fiasXMLDataSetConverter.loadXmlToDb_AddressObjects(f.FullName);}

             foreach (FileInfo f in rootdir.GetFiles("AS_HOUSEINT*"))
             {
                 fiasXMLDataSetConverter.loadXmlToDb_HousesInterval(f.FullName);
             }
             foreach (FileInfo f in rootdir.GetFiles("AS_LANDMARK*"))
             {
                 fiasXMLDataSetConverter.loadXmlToDb_Landmark(f.FullName);}

             foreach (FileInfo f in rootdir.GetFiles("AS_STEAD*"))
             {
                 fiasXMLDataSetConverter.loadXmlToDb_Stead(f.FullName);
             }
             foreach (FileInfo f in rootdir.GetFiles("AS_ROOM*"))
             {
                 fiasXMLDataSetConverter.loadXmlToDb_Room(f.FullName);
             }
             foreach (FileInfo f in rootdir.GetFiles("AS_NORMDOC*"))
             {
                 fiasXMLDataSetConverter.loadXmlToDb_NormativeDocument(f.FullName);
             }
             foreach (FileInfo f in rootdir.GetFiles("AS_HOUSE*"))
             {
                 fiasXMLDataSetConverter.loadXmlToDb_Houses(f.FullName);
             }
             foreach (FileInfo f in rootdir.GetFiles("AS_HOUSE*"))
             {
                 fiasXMLDataSetConverter.loadXmlToDb_Houses_Entityes(f.FullName);
             }*/
        }


        public void Load()
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
                        bulkList.Add(new BulkTableListItem("House", "fias_tmp", f));
                    }
                }
                foreach (BulkTableListItem bi in bulkList)
                {
                    DateTime _start = DateTime.Now;
                    LogInfo(bi.File.Name + " запущено");
                    loadDBFToDb(bi.File, bi.TableName,_connection);
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
    


