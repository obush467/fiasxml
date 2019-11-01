using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;

namespace Fias.Operators
{
    internal class Del_NormativeDocumentOperatorSP:CommandDBFFileOperator
    {
        public Del_NormativeDocumentOperatorSP(FileInfo file, SqlConnection connection) : base(file, connection)
        {
            Command.CommandText = "fias.merge_Del_NormativeDocument";
            Add_Parameters();
        }

        private void Add_Parameters()
        {
            Command.Parameters.Add("@NORMDOCID", SqlDbType.NVarChar).IsNullable = true;
            Command.Parameters.Add("@DOCNAME", SqlDbType.NVarChar).IsNullable = true;
            Command.Parameters.Add("@DOCDATE", SqlDbType.Date).IsNullable = true;
            Command.Parameters.Add("@DOCNUM", SqlDbType.NVarChar).IsNullable = true;
            Command.Parameters.Add("@DOCTYPE", SqlDbType.BigInt).IsNullable = true;
            Command.Parameters.Add("@DOCIMGID", SqlDbType.NVarChar).IsNullable = true;
        }
    }
}