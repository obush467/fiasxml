using System;
using System.Data;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using System.Data.SqlClient;
using System.Xml.Schema;

namespace fiasxml
{
    class Program
    {
        // [STAThread]

        private static DataSets.fias_xml_to_dataset fiasXMLDataSetConverter = new DataSets.fias_xml_to_dataset();
        static void Main(string[] args)
        {
            fiasXMLDataSetConverter.Connection.Open();

            
            try
            {
                DirectoryInfo rootdir=new DirectoryInfo("E:\\Новая папка");
                foreach (FileInfo f in (rootdir).GetFiles("HOUSE??.DBF"))
                { fiasXMLDataSetConverter.loadDBFToDb(f,"House");}
                //загрузка вспомогательных таблиц
                /*foreach (FileInfo f in rootdir.GetFiles("AS_ACTSTAT*"))
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
                }*/

                //загрузка основных таблиц

                /*foreach (FileInfo f in rootdir.GetFiles("AS_ADDROBJ*"))
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
                }*/
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
                /*foreach (FileInfo f in rootdir.GetFiles("AS_HOUSE*"))
                {
                    fiasXMLDataSetConverter.loadXmlToDb_Houses_Entityes(f.FullName);
                }*/
                /*foreach (FileInfo f in rootdir.GetFiles("*"))
                {
                    fiasXMLDataSetConverter.loadXmlToDb_NormativeDocumentTypes(f.FullName);
                }*/
                //DBFimport();
            }
            finally { fiasXMLDataSetConverter.Connection.Close(); }

        }
        private static void DBFimport()
        {
            DataSet3 dbf_dataset = new DataSet3();
            DataSet3.HOUSE01DataTable h1 = dbf_dataset.HOUSE01;
            System.Data.Odbc.OdbcConnection conn1 = new System.Data.Odbc.OdbcConnection("Dsn=333;Driver={Microsoft dBASE Driver (*.dbf, *.ndx, *.mdx)};DriverID=277;Dbq=E:\\Новая папка;");
            DataSet3TableAdapters.TableAdapterManager man = new DataSet3TableAdapters.TableAdapterManager();
            man.Connection=conn1;
            man.HOUSE01TableAdapter = new DataSet3TableAdapters.HOUSE01TableAdapter();
            man.Connection.Open();
            
            man.HOUSE01TableAdapter.Fill(h1);
            var t = dbf_dataset.HOUSE01.Count;
        }

       
    }
}
