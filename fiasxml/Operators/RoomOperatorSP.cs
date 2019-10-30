using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fias.Operators
{
    public class RoomOperatorSP : CommandDBFFileOperator
    {
        public RoomOperatorSP(FileInfo file, SqlConnection connection) : base(file, connection)
        {
            Command.CommandText = "fias.merge_Room";
            Add_Parameters();
        }
        public void Add_Parameters()
        {
            Command.Parameters.Add("@ROOMGUID", SqlDbType.NVarChar);
            Command.Parameters.Add("@FLATNUMBER", SqlDbType.NVarChar);
            Command.Parameters.Add("@FLATTYPE", SqlDbType.Int);
            Command.Parameters.Add("@ROOMNUMBER", SqlDbType.NVarChar);
            Command.Parameters.Add("@ROOMTYPE", SqlDbType.Int);
            Command.Parameters.Add("@REGIONCODE", SqlDbType.NVarChar);
            Command.Parameters.Add("@POSTALCODE", SqlDbType.NVarChar);
            Command.Parameters.Add("@UPDATEDATE", SqlDbType.Date);
            Command.Parameters.Add("@HOUSEGUID", SqlDbType.NVarChar);
            Command.Parameters.Add("@ROOMID", SqlDbType.NVarChar);
            Command.Parameters.Add("@PREVID", SqlDbType.NVarChar);
            Command.Parameters.Add("@NEXTID", SqlDbType.NVarChar);
            Command.Parameters.Add("@STARTDATE", SqlDbType.Date);
            Command.Parameters.Add("@ENDDATE", SqlDbType.Date);
            Command.Parameters.Add("@LIVESTATUS", SqlDbType.Bit);
            Command.Parameters.Add("@NORMDOC", SqlDbType.NVarChar);
            Command.Parameters.Add("@OPERSTATUS", SqlDbType.BigInt);
            Command.Parameters.Add("@CADNUM", SqlDbType.NVarChar);
            Command.Parameters.Add("@ROOMCADNUM", SqlDbType.NVarChar);
        }
    }
}
