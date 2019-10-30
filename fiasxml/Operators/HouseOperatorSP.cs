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
    public class HouseOperatorSP :CommandDBFFileOperator
    {
        public HouseOperatorSP(FileInfo file, SqlConnection connection) :base(file, connection)
        {
            Command.CommandText = "fias.merge_House";
            Add_Parameters();
        }
       public void Add_Parameters()
        {
            Command.Parameters.Add("@POSTALCODE", SqlDbType.VarChar).IsNullable = true;
            Command.Parameters.Add("@IFNSFL", SqlDbType.VarChar).IsNullable = true;
            Command.Parameters.Add("@TERRIFNSFL", SqlDbType.VarChar).IsNullable = true;
            Command.Parameters.Add("@IFNSUL", SqlDbType.VarChar).IsNullable = true;
            Command.Parameters.Add("@TERRIFNSUL", SqlDbType.VarChar).IsNullable = true;
            Command.Parameters.Add("@OKATO", SqlDbType.VarChar).IsNullable = true;
            Command.Parameters.Add("@OKTMO", SqlDbType.VarChar).IsNullable = true;
            Command.Parameters.Add("@UPDATEDATE", SqlDbType.Date).IsNullable = true;
            Command.Parameters.Add("@HOUSENUM", SqlDbType.VarChar).IsNullable = true;
            Command.Parameters.Add("@ESTSTATUS", SqlDbType.Int).IsNullable = true;
            Command.Parameters.Add("@BUILDNUM", SqlDbType.VarChar).IsNullable = true;
            Command.Parameters.Add("@STRUCNUM", SqlDbType.VarChar).IsNullable = true;
            Command.Parameters.Add("@STRSTATUS", SqlDbType.Int).IsNullable = true;
            Command.Parameters.Add("@HOUSEID", SqlDbType.VarChar).IsNullable = true;
            Command.Parameters.Add("@HOUSEGUID", SqlDbType.VarChar).IsNullable = true;
            Command.Parameters.Add("@AOGUID", SqlDbType.VarChar).IsNullable = true;
            Command.Parameters.Add("@STARTDATE", SqlDbType.Date).IsNullable = true;
            Command.Parameters.Add("@ENDDATE", SqlDbType.Date).IsNullable = true;
            Command.Parameters.Add("@STATSTATUS", SqlDbType.Int).IsNullable = true;
            Command.Parameters.Add("@NORMDOC", SqlDbType.VarChar).IsNullable = true;
            Command.Parameters.Add("@COUNTER", SqlDbType.Int).IsNullable = true;
            Command.Parameters.Add("@CADNUM", SqlDbType.VarChar).IsNullable = true;
            Command.Parameters.Add("@DIVTYPE", SqlDbType.Int).IsNullable = true;
        }
    }
}
