using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;

namespace Fias.Operators
{
    internal class AddressObjectTypeOperatorSP:CommandDBFFileOperator
    {
        public AddressObjectTypeOperatorSP(FileInfo file, SqlConnection connection) : base(file, connection)
        {
            Command.CommandText = "fias.merge_AddressObjectType";
            Add_Parameters();
        }

        private void Add_Parameters()
        {
            Command.Parameters.Add("@LEVEL", SqlDbType.Int).IsNullable = true;
            Command.Parameters.Add("@SCNAME", SqlDbType.NVarChar).IsNullable = true;
            Command.Parameters.Add("@SOCRNAME", SqlDbType.NVarChar).IsNullable = true;
            Command.Parameters.Add("@KOD_T_ST", SqlDbType.NVarChar).IsNullable = true;
        }
    }
}