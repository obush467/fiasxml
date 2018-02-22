using System;
using System.Data;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using System.Data.SqlClient;
using System.Xml.Schema;
using fiasxml.Models;
using fiasxml.DataSets.dsMainTableAdapters;
using static fiasxml.DataSets.dsMain;
using System.Collections.Generic;
using System.Collections;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using fiasxml.DataSets;

namespace fiasxml
{
    class fias_xml_to_dataset
    {
        private SqlConnection cONN = new SqlConnection("Data Source=MAKSIMOV;Initial Catalog=GBUMATC2;Persist Security Info=True;User ID=Бушмакин;Password=453459");
        public SqlConnection Connection
        { get
            {
                return this.cONN;
            }
          set
            {
                cONN = value;
            }
        }
        public void loadXmlToDb_Test1(string FileName)
        {
            //FileStream ws;
            //ws = File.OpenRead(FileName);
            SqlBulkCopyColumnMapping mappingID = new SqlBulkCopyColumnMapping("ID", "ID");

            DataSets.Test1es cs = new DataSets.Test1es();
            loadXmlToDb(FileName, cs.Test1, "dbo.Table_3");
        }
        public void loadXmlToDb_Stead(string FileName)
        {
            //FileStream ws;
            //ws = File.OpenRead(FileName);
            Steads cs = new DataSets.Steads();
            loadXmlToDb(FileName, cs.Stead, "dbo.Stead");
        }
        public void loadXmlToDb_Room(string FileName)
        {
            FileStream ws;
            ws = File.OpenRead(FileName);
            BufferedStream buffered = new BufferedStream(ws,1000000000);
            DataSets.Rooms cs = new DataSets.Rooms();
            loadXmlToDb(buffered, cs.Room, "dbo.Room");
        }
        public void loadXmlToDb_NormativeDocumentTypes(string FileName)
        {
            //FileStream ws;
            //ws = File.OpenRead(FileName);
            NormativeDocumentTypes cs = new DataSets.NormativeDocumentTypes();
            loadXmlToDb(FileName, cs.NormativeDocumentType, "dbo.NormativeDocumentType");
        }
        public void loadXmlToDb_NormativeDocument(string FileName)
        {
            //FileStream ws;
            //ws = File.OpenRead(FileName);
            DataSets.NormativeDocumentes cs = new DataSets.NormativeDocumentes();
            loadXmlToDb(FileName, cs.NormativeDocument, "dbo.NormativeDocument");
        }
        public void loadXmlToDb_Landmark(string FileName)
        {
            //FileStream ws;
            //ws = File.OpenRead(FileName);
            DataSets.Landmarks cs = new DataSets.Landmarks();
            loadXmlToDb(FileName, cs.Landmark, "dbo.Landmark");
        }
        public void loadXmlToDb_Landmark(string FileName, string SchemeName)
        {
            //FileStream ws;
            //ws = File.OpenRead(FileName);
            DataSets.Landmarks cs = new DataSets.Landmarks();
            loadXmlToDb(FileName, SchemeName, cs.Landmark, "dbo.Landmark");
        }
        public void loadXmlToDb_StructureStatus(string FileName)
        {
            //FileStream ws;
            //ws = File.OpenRead(FileName);
            DataSets.StructureStatuses cs = new DataSets.StructureStatuses();
            loadXmlToDb(FileName, cs.StructureStatus, "dbo.StructureStatus");
        }
        public void loadXmlToDb_RoomTypes(string FileName)
        {
            DataSets.OperationStatuses cs = new DataSets.OperationStatuses();
            loadXmlToDb(FileName, cs.OperationStatus, "dbo.RoomType");
        }

        public void loadXmlToDb_FlatTypes(string FileName)
        {
            DataSets.OperationStatuses cs = new DataSets.OperationStatuses();
            loadXmlToDb(FileName, cs.OperationStatus, "dbo.FlatType");
        }

        public void loadXmlToDb_OperationStatus(string FileName)
        {
            //FileStream ws;
            //ws = File.OpenRead(FileName);
            DataSets.OperationStatuses cs = new DataSets.OperationStatuses();
            loadXmlToDb(FileName, cs.OperationStatus, "dbo.OperationStatus");
        }
        public void loadXmlToDb_IntervalStatus(string FileName)
        {
            //FileStream ws;
            //ws = File.OpenRead(FileName);
            DataSets.IntervalStatuses cs = new DataSets.IntervalStatuses();
            loadXmlToDb(FileName, cs.IntervalStatus, "dbo.IntervalStatus");
        }
        public void loadXmlToDb_HouseStateStatus(string FileName)
        {
            //FileStream ws;
            //ws = File.OpenRead(FileName);
            DataSets.HouseStateStatuses cs = new DataSets.HouseStateStatuses();
            loadXmlToDb(FileName, cs.HouseStateStatus, "dbo.HouseStateStatus");
        }
        public void loadXmlToDb_HousesInterval(string FileName)
        {
            //FileStream ws;
            //ws = File.OpenRead(FileName);
            DataSets.HouseIntervals cs = new DataSets.HouseIntervals();
            loadXmlToDb(FileName, cs.HouseInterval, "dbo.HouseInterval");
        }
        public void loadXmlToDb_Houses_Entityes(string FileName)
        {
            fiasEntities ef = new Models.fiasEntities();

            XmlDocument xdoc = new XmlDocument();
            XmlReader wReader = XmlReader.Create(FileName);
            XmlSerializer serializer = new XmlSerializer(typeof(Entityes.Houses));//)
            wReader.ReadStartElement("Houses");
            Console.WriteLine(wReader.Name);
            while (1==1 /*wReader.ReadToFollowing("House")*/)
            {
                XmlElement u = (XmlElement)xdoc.ReadNode(wReader);
                if (u == null)
                    { break; }
                Console.WriteLine((u.Attributes[0].Name + " " + u.Attributes[0].Value.ToString()));
                //for (int i = 0; i < u.Attributes.Count; i++)
                // {
                //    Console.WriteLine(u.Attributes[i].Name);
                //     Console.WriteLine(u.Attributes[i].Value);
                // }
                XmlDocument newd = new XmlDocument();
                XmlElement t = (XmlElement)u.Clone();
                var rootEl = newd.CreateNode(XmlNodeType.Element, "Houses", "");
                newd.AppendChild(rootEl);
                XmlElement newel = (XmlElement)newd.ImportNode(u, true);
                rootEl.AppendChild(newel);
                XmlNodeReader nr = new XmlNodeReader(newd);
                Entityes.Houses tempHouses = (Entityes.Houses)serializer.Deserialize(nr);
                Entityes.HousesHouse tHouse = tempHouses.House[0];
                //int insertRes = ef.insertHouse(tHouse.POSTALCODE, tHouse.IFNSFL, tHouse.TERRIFNSFL, tHouse.IFNSUL, tHouse.TERRIFNSUL, tHouse.OKATO, tHouse.OKTMO, tHouse.UPDATEDATE, tHouse.HOUSENUM, int.Parse(tHouse.ESTSTATUS), tHouse.BUILDNUM, tHouse.STRUCNUM, int.Parse(tHouse.STRSTATUS), tHouse.HOUSEID, tHouse.HOUSEGUID, tHouse.AOGUID, tHouse.STARTDATE, tHouse.ENDDATE, int.Parse(tHouse.STATSTATUS), tHouse.NORMDOC, int.Parse(tHouse.COUNTER), tHouse.CADNUM, tHouse.DIVTYPE);
            }

        }

        public void loadXmlToDb_AddressObjectTypes(string FileName)
        {
            //FileStream ws;
            //ws = File.OpenRead(FileName);
            DataSets.AddressObjectTypes cs = new DataSets.AddressObjectTypes();
            loadXmlToDb(FileName, cs.AddressObjectType, "dbo.AddressObjectType");
        }
        public  void loadXmlToDb_Houses(string FileName)
        {
            //FileStream ws;
            //ws = File.OpenRead(FileName);
            DataSets.Houses cs = new DataSets.Houses();
            loadXmlToDb(FileName, cs.House, "dbo.House");
        }
        public void loadXmlToDb_CenterStatuses(string FileName)
        {
            FileStream ws;
            ws = File.OpenRead(FileName);
            DataSets.CenterStatuses cs = new DataSets.CenterStatuses();
            loadXmlToDb(ws, cs.CenterStatus, "dbo.CenterStatus");
        }
        public void loadXmlToDb_EstateStatuses(string FileName)
        {
            FileStream ws;
            ws = File.OpenRead(FileName);
            DataSets.EstateStatuses cs = new DataSets.EstateStatuses();
            loadXmlToDb(ws, cs.EstateStatus, "dbo.EstateStatus");
        }
        public void loadXmlToDb_CurrentStatus(string FileName)
        {
            FileStream ws;
            ws = File.OpenRead(FileName);
            DataSets.CurrentStatuses cs = new DataSets.CurrentStatuses();
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
        static void SettingsValidationEventHandler(object sender, ValidationEventArgs e)
        {
            if (e.Severity == XmlSeverityType.Warning)
            {
                Console.Write("Предупреждаю!: ");
                Console.WriteLine(e.Message);
            }
            else if (e.Severity == XmlSeverityType.Error)
            {
                Console.Write("Ошибочка вышла: ");
                Console.WriteLine(e.Message);
            }
        }

        public void loadXmlToDb(Stream wStream, DataTable wTable, string TableName)
        {
            XmlReader wReader = XmlReader.Create(wStream);
            this.loadXmlToDb(wReader, wTable, TableName);
        }
        public void loadDBFToDb(FileInfo dbfFile, string TableName)
        {
            NDbfReader.Table dbfTable = NDbfReader.Table.Open(dbfFile.Open(FileMode.Open));
            NDbfReader.Reader dbfReader = dbfTable.OpenReader(System.Text.Encoding.GetEncoding(866));

            SqlTransaction TRA = cONN.BeginTransaction(("Bulk" + TableName));
            SqlBulkCopy da = new SqlBulkCopy(this.cONN, SqlBulkCopyOptions.TableLock,TRA);
            da.BulkCopyTimeout = 60000;
            da.DestinationTableName = TableName;
            dsMain ds = new dsMain();
            HouseDataTable servertable = (HouseDataTable)ds.Tables[TableName];
            List<DataRow> rows = new List<DataRow>();
            while (dbfReader.Read())
            {
                DataRow newrow = servertable.NewRow();
                ;
                foreach (NDbfReader.Column c in dbfTable.Columns)
                {
                    newrow.SetField(c.Name, dbfReader.GetValue(c.Name));
                    rows.Add(newrow);
                }
            }
            da.WriteToServer(rows.ToArray());
            TRA.Commit();
        }


        public void loadXmlToDb(XmlReader wReader, DataTable wTable, string TableName)
        {
            wTable.BeginLoadData();
            wTable.ReadXml(wReader);
            wTable.EndLoadData();
            SqlTransaction TRA = cONN.BeginTransaction(("Bulk"+ TableName));
            SqlBulkCopy da = new SqlBulkCopy(this.cONN, SqlBulkCopyOptions.Default, TRA);
            da.BulkCopyTimeout = 600;
            da.DestinationTableName = TableName;
            try
            {
                Console.WriteLine("Запись в базу данных");
                da.WriteToServer(wTable);
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

        public  void loadXmlToDb_ActualStatus(string FileName)
        {
            FileStream ws;
            ws = File.OpenRead(FileName);
            DataSets.ActualStatuses cs = new DataSets.ActualStatuses();
            loadXmlToDb(ws, cs.ActualStatus, "dbo.ActualStatus");
        }


        public  void loadXmlToDb_AddressObjects(string FileName)
        {
            FileStream ws;
            ws = File.OpenRead(FileName);
            DataSets.AddressObjects cs = new DataSets.AddressObjects();
            loadXmlToDb(ws, cs.Object, "dbo.Object");
        }
       
        }
    }


