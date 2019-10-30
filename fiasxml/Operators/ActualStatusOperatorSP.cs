using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;

namespace Fias.Operators
{
    public class ActualStatusOperatorSP:CommandDBFFileOperator
    {
        public ActualStatusOperatorSP(FileInfo file, SqlConnection connection) : base(file, connection)
        {
            Command.CommandText = "fias.merge_ActualStatus";
            Add_Parameters();
        }

        private void Add_Parameters()
        {
            Command.Parameters.Add("@ACTSTATID", SqlDbType.Int).IsNullable = true;
            Command.Parameters.Add("@NAME", SqlDbType.NVarChar).IsNullable = true;
        }
    }
}