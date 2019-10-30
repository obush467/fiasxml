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
    public class SteadOperatorSP : CommandDBFFileOperator
    {
        public SteadOperatorSP(FileInfo file, SqlConnection connection) : base(file, connection)
        {
            Command.CommandText = "fias.merge_Stead";
            Add_Parameters();
        }
        public void Add_Parameters()
        {
            Command.Parameters.Add("@STEADGUID", SqlDbType.VarChar).IsNullable = true;
            Command.Parameters.Add("@NUMBER", SqlDbType.VarChar).IsNullable = true;
            Command.Parameters.Add("@REGIONCODE", SqlDbType.VarChar).IsNullable = true;
            Command.Parameters.Add("@POSTALCODE", SqlDbType.VarChar).IsNullable = true;
            Command.Parameters.Add("@IFNSFL", SqlDbType.VarChar).IsNullable = true;
            Command.Parameters.Add("@TERRIFNSFL", SqlDbType.VarChar).IsNullable = true;
            Command.Parameters.Add("@IFNSUL", SqlDbType.VarChar).IsNullable = true;
            Command.Parameters.Add("@TERRIFNSUL", SqlDbType.VarChar).IsNullable = true;
            Command.Parameters.Add("@OKATO", SqlDbType.VarChar).IsNullable = true;
            Command.Parameters.Add("@OKTMO", SqlDbType.VarChar).IsNullable = true;
            Command.Parameters.Add("@UPDATEDATE", SqlDbType.VarChar).IsNullable = true;
            Command.Parameters.Add("@PARENTGUID", SqlDbType.VarChar).IsNullable = true;
            Command.Parameters.Add("@STEADID", SqlDbType.VarChar).IsNullable = true;
            Command.Parameters.Add("@PREVID", SqlDbType.VarChar).IsNullable = true;
            Command.Parameters.Add("@NEXTID", SqlDbType.VarChar).IsNullable = true;
            Command.Parameters.Add("@OPERSTATUS", SqlDbType.BigInt).IsNullable = true;
            Command.Parameters.Add("@STARTDATE", SqlDbType.Date).IsNullable = true;
            Command.Parameters.Add("@ENDDATE", SqlDbType.Date).IsNullable = true;
            Command.Parameters.Add("@NORMDOC", SqlDbType.VarChar).IsNullable = true;
            Command.Parameters.Add("@LIVESTATUS", SqlDbType.Int).IsNullable = true;
            Command.Parameters.Add("@CADNUM", SqlDbType.VarChar).IsNullable = true;
            Command.Parameters.Add("@DIVTYPE", SqlDbType.Int).IsNullable = true;
            Command.Parameters.Add("@COUNTER", SqlDbType.Int).IsNullable = true;
        }
    }
}
