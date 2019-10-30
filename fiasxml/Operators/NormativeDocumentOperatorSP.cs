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
    public class NormativeDocumentOperatorSP : CommandDBFFileOperator
    {
        public NormativeDocumentOperatorSP(FileInfo file, SqlConnection connection) : base(file, connection)
        {
            Command.CommandText = "fias.merge_NormativeDocument";
            Add_Parameters();
        }
        public void Add_Parameters()
        {
            Command.Parameters.Add("@NORMDOCID", SqlDbType.NVarChar).IsNullable=true;
            Command.Parameters.Add("@DOCNAME", SqlDbType.NVarChar).IsNullable=true;
            Command.Parameters.Add("@DOCDATE", SqlDbType.Date).IsNullable = true;
            Command.Parameters.Add("@DOCNUM", SqlDbType.NVarChar).IsNullable = true;
            Command.Parameters.Add("@DOCTYPE", SqlDbType.BigInt).IsNullable = true;
            Command.Parameters.Add("@DOCIMGID", SqlDbType.NVarChar).IsNullable = true;
        }
    }
}
