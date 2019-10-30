using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;

namespace Fias.Operators
{
    public class FlatTypeOperatorSP:CommandDBFFileOperator
    {
        public FlatTypeOperatorSP(FileInfo file, SqlConnection connection) : base(file, connection)
        {
            Command.CommandText = "fias.merge_FlatType";
            Add_Parameters();
        }

        private void Add_Parameters()
        {
            Command.Parameters.Add("@FLTYPEID", SqlDbType.Int).IsNullable = false;
            Command.Parameters.Add("@NAME", SqlDbType.NVarChar).IsNullable = true;
            Command.Parameters.Add("@SHORTNAME", SqlDbType.NVarChar).IsNullable = true;
        }
    }
}