using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;

namespace Fias.Operators
{
    public class CenterStatusOperatorSP:CommandDBFFileOperator
    {
        public CenterStatusOperatorSP(FileInfo file, SqlConnection connection) : base(file, connection)
        {
            Command.CommandText = "fias.merge_CenterStatus";
            Add_Parameters();
        }

        private void Add_Parameters()
        {
            Command.Parameters.Add("@CENTERSTID", SqlDbType.Int).IsNullable = true;
            Command.Parameters.Add("@NAME", SqlDbType.VarChar).IsNullable = true;
        }
    }
}