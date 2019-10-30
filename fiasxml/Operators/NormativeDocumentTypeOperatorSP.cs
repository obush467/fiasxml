using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;

namespace Fias.Operators
{
    internal class NormativeDocumentTypeOperatorSP:CommandDBFFileOperator
    {
        public NormativeDocumentTypeOperatorSP(FileInfo file, SqlConnection connection) : base(file, connection)
        {
            Command.CommandText = "fias.merge_NormativeDocumentType";
            Add_Parameters();
        }

        private void Add_Parameters()
        {
            Command.Parameters.Add("@NDTYPEID", SqlDbType.Int).IsNullable = false;
            Command.Parameters.Add("@NAME", SqlDbType.NVarChar).IsNullable = false;
        }
    }
}