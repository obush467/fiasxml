using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;

namespace Fias.Operators
{
    public class IntervalStatusOperatorSP:CommandDBFFileOperator
    {
        public IntervalStatusOperatorSP(FileInfo file, SqlConnection connection) : base(file, connection)
        {
            Command.CommandText = "fias.merge_IntervalStatus";
            Add_Parameters();
        }

        private void Add_Parameters()
        {
            Command.Parameters.Add("@INTVSTATID", SqlDbType.Int).IsNullable = false;
            Command.Parameters.Add("@NAME", SqlDbType.NVarChar).IsNullable = false;
        }
    }
}