using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;

namespace Fias.Operators
{
    public class HouseStateStatusOperatorSP:CommandDBFFileOperator
    {
        public HouseStateStatusOperatorSP(FileInfo file, SqlConnection connection) : base(file, connection)
        {
            Command.CommandText = "fias.merge_HouseStateStatus";
            Add_Parameters();
        }

        private void Add_Parameters()
        {
            Command.Parameters.Add("@HOUSESTID", SqlDbType.Int).IsNullable = false;
            Command.Parameters.Add("@NAME", SqlDbType.NVarChar).IsNullable = false;
        }
    }
}