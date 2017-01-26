using SQLXMLBULKLOADLib;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OleDb;

namespace fiasxml
{
    class xml_to_dataset
    {
        public DataSet dataSet = new DataSet();
        public void Exec() {
            SQLXMLBulkLoad _objBL = new SQLXMLBulkLoad();
            _objBL.ConnectionString = "Provider=sqloledb;server=192.168.10.51;database=GBUMATC;user=Бушмакин;password=453459";
            _objBL.ErrorLogFile = "error.xml";
            _objBL.KeepIdentity = false;
            _objBL.
            _objBL.Execute("E:\\fiasxml\\AS_ACTSTAT_2_250_08_04_01_01.xsd", "E:\\fiasxml\\AS_ACTSTAT_20161016_567eac1b-54dd-4a32-9157-44cfa86fcf28.XML");
                }
    }

}
