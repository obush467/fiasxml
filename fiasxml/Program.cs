using SQLXMLBULKLOADLib;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using System.Data.SqlClient;


namespace fiasxml
{
    class Program
    {
        // [STAThread]
        private static SqlConnection CONN = new SqlConnection("Data Source = BUSHMAKINOY\\SQLEXPRESS2; Initial Catalog = ttt2; Integrated Security = True");
        
        static void Main(string[] args)
        {
            CONN.Open();
            // xml_to_dataset xml1 = new xml_to_dataset();

            // xml1.Exec();
            // xml1.dataSet.ReadXmlSchema("E:\\fiasxml\\AS_ACTSTAT_2_250_08_04_01_01.xsd");
            // xml1.dataSet.ReadXmlSchema("E:\\fiasxml\\AS_ADDROBJ_2_250_01_04_01_01.xsd");
            /*foreach (DataTable dataTable in xml1.dataSet.Tables)
             dataTable.BeginLoadData();*/
            // xml1.dataSet.ReadXml("E:\\fiasxml\\AS_ACTSTAT_20161016_567eac1b-54dd-4a32-9157-44cfa86fcf28.XML", System.Data.XmlReadMode.ReadSchema);
            // xml1.dataSet.ReadXml("E:\\fiasxml\\AS_ADDROBJ_20161016_c540c854-4d57-4d84-ba16-5c238e478ac4.XML", System.Data.XmlReadMode.ReadSchema);
            /* foreach (DataTable dataTable in xml1.dataSet.Tables)
                 dataTable.EndLoadData();*/
            /* xml1.dataSet.Tables[].*/
            //AddressObjects adressdataset = new AddressObjects();
            //adressdataset.ReadXml("E:\\fiasxml\\AS_ADDROBJ_20161016_c540c854-4d57-4d84-ba16-5c238e478ac4.XML");
            //object[] r = adressdataset.Tables[0].Select();





            //DataSet1 ds = new DataSet1();
            //var tt = ds.Object.ToList();
            //var t = ds.Tables[0].Select();
            //ds.Object.ReadXml("E:\\fiasxml\\AS_ADDROBJ_20161016_c540c854-4d57-4d84-ba16-5c238e478ac4.XML");
            //ds.Object.AcceptChanges();
            //ds.Tables[0].ReadXml("E:\\fiasxml\\AS_ADDROBJ_20161016_c540c854-4d57-4d84-ba16-5c238e478ac4.XML");


            //SQLXMLBulkLoad4Class objBL = new SQLXMLBulkLoad4Class();
            //objBL.ConnectionString ="Provider=sqloledb;server=BUSHMAKINOY\\SQLEXPRESS2;database=ttt2;integrated security=SSPI";
            //objBL.ErrorLogFile = "error.xml";
            //objBL.KeepIdentity = false;
            // objBL.SchemaGen = true;
            //objBL.SGDropTables = false;
            //objBL.BulkLoad = true;
            //objBL.Execute("E:\\fiasxml\\AS_ADDROBJ1.xsd", "E:\\fiasxml\\AS_ADDROBJ_20161016_c540c854-4d57-4d84-ba16-5c238e478ac4.XML"); //
            //



            //XDocument doc = XDocument.Load("E:\\fiasxml\\AS_ADDROBJ_20161016_c540c854-4d57-4d84-ba16-5c238e478ac4.XML"); 
            //var tracks = from t in doc.Root.Descendants("Object")
            //             select new {aoguid = t.Attribute("AOGUID"), FORMALNAME= t.Attribute("FORMALNAME") };
            //var eee = tracks.Count();
            //foreach (var  ttt in tracks)
            //{ Console.WriteLine(ttt.ToString() ); };

            //var rr = tracks.First();
            //Obj1 o1 = new Obj1();
            //XmlSerializer serializer = new XmlSerializer(typeof(AddressObjects));
            //FileStream fff = File.OpenRead("E:\\fiasxml\\AS_ADDROBJ_20161016_c540c854-4d57-4d84-ba16-5c238e478ac4.XML");
            //var t = serializer.Deserialize(fff);
            // fff.Close();
            try
            {
                //FileStream ws;
                // ws = File.OpenRead("E:\\fiasxml\\AS_ADDROBJ_20161016_c540c854-4d57-4d84-ba16-5c238e478ac4.XML");
                // GetAddressObjects(ws);
                // ws = File.OpenRead("E:\\fiasxml\\AS_ACTSTAT_20161016_567eac1b-54dd-4a32-9157-44cfa86fcf28.XML");
                // GetAC_STATUSes(ws);
                //loadXmlToDb_CurrentStatus("E:\\fiasxml\\AS_CURENTST_20161016_74ce1a82-a6eb-4a9b-9a3d-e22d63cda8b9.XML");
                //loadXmlToDb_EstateStatuses("E:\\fiasxml\\AS_ESTSTAT_20161016_480f97ce-7bb1-40c9-be79-cbd6418b701c.XML");
                //loadXmlToDb_CenterStatuses("E:\\fiasxml\\AS_CENTERST_20161016_e75028af-1269-4395-86d9-c64a99088e97.XML");
                loadXmlToDb_Houses("E:\\fiasxml\\AS_HOUSE_20161016_3cbdd025-227d-4793-97ce-e3e2221a6666.XML");
            }
            finally { CONN.Close(); }

        }
        public static void loadXmlToDb_Houses(string FileName)
        {
            //FileStream ws;
            //ws = File.OpenRead(FileName);
            Houses cs = new Houses();
            loadXmlToDb(FileName, cs.House, "dbo.House");
        }
        public static void loadXmlToDb_CenterStatuses(string FileName)
        {
            FileStream ws;
            ws = File.OpenRead(FileName);
            CenterStatuses cs = new CenterStatuses();
            loadXmlToDb(ws, cs.CenterStatus, "dbo.CenterStatus");
        }
        public static void loadXmlToDb_EstateStatuses(string FileName)
        {
            FileStream ws;
            ws = File.OpenRead(FileName);
            EstateStatuses cs = new EstateStatuses();
            loadXmlToDb(ws, cs.EstateStatus, "dbo.EstateStatus");
        }
        public static void loadXmlToDb_CurrentStatus(string FileName)
        {
            FileStream ws;
            ws = File.OpenRead(FileName);
            CurrentStatuses cs = new CurrentStatuses();
            loadXmlToDb(ws, cs.CurrentStatus, "dbo.CurrentStatus");
        }

        public static void loadXmlToDb(string wFile, DataTable wTable, string TableName)
        {
            XmlReader wReader = XmlReader.Create(wFile);
            loadXmlToDb(wReader, wTable, TableName);
        }
        public static void loadXmlToDb(XmlReader wReader, DataTable wTable, string TableName)
        {
            wTable.BeginLoadData();
            wTable.ReadXml(wReader);
            wTable.EndLoadData();
            //SqlTransaction TRA = CONN.BeginTransaction(("Bulk"+ TableName));
            SqlBulkCopy da = new SqlBulkCopy(CONN);//, SqlBulkCopyOptions.Default, TRA);
            da.BulkCopyTimeout = 600;
            da.DestinationTableName = TableName;
            try
            {
                Console.WriteLine("Запись в базу данных");
                da.WriteToServer(wTable);
                Console.WriteLine("Завершение транзакции");
                //TRA.Commit();
                Console.WriteLine("Завершение транзакции успешно");
                Console.WriteLine("Внесено дохера строк");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Commit Exception Type: {0}", ex.GetType());
                Console.WriteLine("  Message: {0}", ex.Message);

                // Attempt to roll back the transaction.
                try
                {
                    //TRA.Rollback();
                }
                catch (Exception ex2)
                {
                    // This catch block will handle any errors that may have occurred
                    // on the server that would cause the rollback to fail, such as
                    // a closed connection.
                    Console.WriteLine("Rollback Exception Type: {0}", ex2.GetType());
                    Console.WriteLine("  Message: {0}", ex2.Message);
                }
            }
        }
        public static void loadXmlToDb(Stream wStream,DataTable wTable,string TableName)
        {

            wTable.BeginLoadData();
            wTable.ReadXml(wStream);
            wTable.EndLoadData();
            //SqlTransaction TRA = CONN.BeginTransaction(("Bulk"+ TableName));
            SqlBulkCopy da = new SqlBulkCopy(CONN);//, SqlBulkCopyOptions.Default, TRA);
            da.BulkCopyTimeout = 600;
            da.DestinationTableName = TableName;
            try
            {
                Console.WriteLine("Запись в базу данных");
                da.WriteToServer(wTable);
                Console.WriteLine("Завершение транзакции");
                //TRA.Commit();
                Console.WriteLine("Завершение транзакции успешно");
                Console.WriteLine("Внесено дохера строк");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Commit Exception Type: {0}", ex.GetType());
                Console.WriteLine("  Message: {0}", ex.Message);

                // Attempt to roll back the transaction.
                try
                {
                    //TRA.Rollback();
                }
                catch (Exception ex2)
                {
                    // This catch block will handle any errors that may have occurred
                    // on the server that would cause the rollback to fail, such as
                    // a closed connection.
                    Console.WriteLine("Rollback Exception Type: {0}", ex2.GetType());
                    Console.WriteLine("  Message: {0}", ex2.Message);
                }
            }
        }
        public static void loadXmlToDb_ActualStatus(string FileName)
        {
            FileStream ws;
            ws = File.OpenRead(FileName);
            ActualStatuses cs = new ActualStatuses();
            loadXmlToDb(ws, cs.ActualStatus, "dbo.ActualStatus");
        }
        public static void GetAddressObjects(Stream wStream)
        {
            AddressObjects ad = new AddressObjects();
            Console.WriteLine("Начато считывание XML");
            ad.Object.ReadXml(wStream);
            ad.Object.AcceptChanges();
            Console.WriteLine("Закончено считывание XML");
            Console.WriteLine(("Считано " + ad.Object.Count + " элементов AddressObject"));
            Console.WriteLine("Подключение к базе данных");
            SqlTransaction TRA = CONN.BeginTransaction("AdressBulk");
            SqlBulkCopy da = new SqlBulkCopy(CONN, SqlBulkCopyOptions.Default, TRA);
            da.BulkCopyTimeout = 600;
            da.DestinationTableName = "dbo.Object";
            da.ColumnMappings.Add("AOGUID", "AOGUID");
            da.ColumnMappings.Add("FORMALNAME", "FORMALNAME");
            da.ColumnMappings.Add("REGIONCODE", "REGIONCODE");
            da.ColumnMappings.Add("AUTOCODE", "AUTOCODE");
            da.ColumnMappings.Add("AREACODE", "AREACODE");
            da.ColumnMappings.Add("CITYCODE", "CITYCODE");
            da.ColumnMappings.Add("CTARCODE", "CTARCODE");
            da.ColumnMappings.Add("PLACECODE", "PLACECODE");
            da.ColumnMappings.Add("STREETCODE", "STREETCODE");
            da.ColumnMappings.Add("EXTRCODE", "EXTRCODE");
            da.ColumnMappings.Add("SEXTCODE", "SEXTCODE");
            da.ColumnMappings.Add("OFFNAME", "OFFNAME");
            da.ColumnMappings.Add("POSTALCODE", "POSTALCODE");
            da.ColumnMappings.Add("IFNSFL", "IFNSFL");
            da.ColumnMappings.Add("TERRIFNSFL", "TERRIFNSFL");
            da.ColumnMappings.Add("IFNSUL", "IFNSUL");
            da.ColumnMappings.Add("TERRIFNSUL", "TERRIFNSUL");
            da.ColumnMappings.Add("OKATO", "OKATO");
            da.ColumnMappings.Add("OKTMO", "OKTMO");
            da.ColumnMappings.Add("UPDATEDATE", "UPDATEDATE");
            da.ColumnMappings.Add("SHORTNAME", "SHORTNAME");
            da.ColumnMappings.Add("AOLEVEL", "AOLEVEL");
            da.ColumnMappings.Add("PARENTGUID", "PARENTGUID");
            da.ColumnMappings.Add("AOID", "AOID");
            da.ColumnMappings.Add("PREVID", "PREVID");
            da.ColumnMappings.Add("NEXTID", "NEXTID");
            da.ColumnMappings.Add("CODE", "CODE");
            da.ColumnMappings.Add("PLAINCODE", "PLAINCODE");
            da.ColumnMappings.Add("ACTSTATUS", "ACTSTATUS");
            da.ColumnMappings.Add("CENTSTATUS", "CENTSTATUS");
            da.ColumnMappings.Add("OPERSTATUS", "OPERSTATUS");
            da.ColumnMappings.Add("CURRSTATUS", "CURRSTATUS");
            da.ColumnMappings.Add("STARTDATE", "STARTDATE");
            da.ColumnMappings.Add("ENDDATE", "ENDDATE");
            da.ColumnMappings.Add("NORMDOC", "NORMDOC");
            da.ColumnMappings.Add("LIVESTATUS", "LIVESTATUS");
            da.ColumnMappings.Add("CADNUM", "CADNUM");
            da.ColumnMappings.Add("DIVTYPE", "DIVTYPE");

            try
            {
                Console.WriteLine("Запись в базу данных");
                da.WriteToServer(ad.Object);
                Console.WriteLine("Завершение транзакции");
                TRA.Commit();
                Console.WriteLine("Завершение транзакции успешно");
                Console.WriteLine("Внесено дохера строк");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Commit Exception Type: {0}", ex.GetType());
                Console.WriteLine("  Message: {0}", ex.Message);

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
                    Console.WriteLine("Rollback Exception Type: {0}", ex2.GetType());
                    Console.WriteLine("  Message: {0}", ex2.Message);
                }
            }

            
        }
    }
}
