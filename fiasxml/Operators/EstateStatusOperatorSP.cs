using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;

namespace Fias.Operators
{
    public class EstateStatusOperatorSP:CommandDBFFileOperator
    {
        public EstateStatusOperatorSP(FileInfo file, SqlConnection connection) : base(file, connection)
        {
            Command.CommandText = "fias.merge_EstateStatus";
            Add_Parameters();
        }

        private void Add_Parameters()
        {
            Command.Parameters.Add("@ESTSTATID", SqlDbType.Int).IsNullable = false;
            Command.Parameters.Add("@NAME", SqlDbType.NVarChar).IsNullable = true;
            Command.Parameters.Add("@SHORTNAME", SqlDbType.NVarChar).IsNullable = true;
        }
    }
}