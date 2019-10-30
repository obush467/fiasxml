using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;

namespace Fias.Operators
{
    internal class CurrentStatusOperatorSP:CommandDBFFileOperator
    {
        public CurrentStatusOperatorSP(FileInfo file, SqlConnection connection) : base(file, connection)
        {
            Command.CommandText = "fias.merge_CurrentStatus";
            Add_Parameters();
        }

        private void Add_Parameters()
        {
            Command.Parameters.Add("@CURENTSTID", SqlDbType.Int).IsNullable = false;
            Command.Parameters.Add("@NAME", SqlDbType.VarChar).IsNullable = false;
        }
    }
}