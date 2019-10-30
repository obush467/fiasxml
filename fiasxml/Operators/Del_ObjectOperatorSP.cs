using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;

namespace Fias.Operators
{
    public class Del_ObjectOperatorSP:CommandDBFFileOperator
    {
        public Del_ObjectOperatorSP(FileInfo file, SqlConnection connection) : base(file, connection)
        {
            Command.CommandText = "fias.merge_Del_Object";
            Add_Parameters();
        }

        private void Add_Parameters()
        {
            Command.Parameters.Add("@AOID", SqlDbType.NVarChar).IsNullable = true;
            Command.Parameters.Add("@AOGUID", SqlDbType.NVarChar).IsNullable = true;
            Command.Parameters.Add("@FORMALNAME", SqlDbType.NVarChar).IsNullable = true;
            Command.Parameters.Add("@REGIONCODE", SqlDbType.NVarChar).IsNullable = true;
            Command.Parameters.Add("@AUTOCODE", SqlDbType.NVarChar).IsNullable = true;
            Command.Parameters.Add("@AREACODE", SqlDbType.NVarChar).IsNullable = true;
            Command.Parameters.Add("@CITYCODE", SqlDbType.NVarChar).IsNullable = true;
            Command.Parameters.Add("@CTARCODE", SqlDbType.NVarChar).IsNullable = true;
            Command.Parameters.Add("@PLACECODE", SqlDbType.NVarChar).IsNullable = true;
            Command.Parameters.Add("@STREETCODE", SqlDbType.NVarChar).IsNullable = true;
            Command.Parameters.Add("@EXTRCODE", SqlDbType.NVarChar).IsNullable = true;
            Command.Parameters.Add("@SEXTCODE", SqlDbType.NVarChar).IsNullable = true;
            Command.Parameters.Add("@OFFNAME", SqlDbType.NVarChar).IsNullable = true;
            Command.Parameters.Add("@POSTALCODE", SqlDbType.NVarChar).IsNullable = true;
            Command.Parameters.Add("@IFNSFL", SqlDbType.NVarChar).IsNullable = true;
            Command.Parameters.Add("@TERRIFNSFL", SqlDbType.NVarChar).IsNullable = true;
            Command.Parameters.Add("@IFNSUL", SqlDbType.NVarChar).IsNullable = true;
            Command.Parameters.Add("@TERRIFNSUL", SqlDbType.NVarChar).IsNullable = true;
            Command.Parameters.Add("@OKATO", SqlDbType.NVarChar).IsNullable = true;
            Command.Parameters.Add("@OKTMO", SqlDbType.NVarChar).IsNullable = true;
            Command.Parameters.Add("@UPDATEDATE", SqlDbType.Date).IsNullable = true;
            Command.Parameters.Add("@SHORTNAME", SqlDbType.NVarChar).IsNullable = true;
            Command.Parameters.Add("@AOLEVEL", SqlDbType.Int).IsNullable = true;
            Command.Parameters.Add("@PARENTGUID", SqlDbType.NVarChar).IsNullable = true;
            Command.Parameters.Add("@PREVID", SqlDbType.NVarChar).IsNullable = true;
            Command.Parameters.Add("@NEXTID", SqlDbType.NVarChar).IsNullable = true;
            Command.Parameters.Add("@CODE", SqlDbType.NVarChar).IsNullable = true;
            Command.Parameters.Add("@PLAINCODE", SqlDbType.NVarChar).IsNullable = true;
            Command.Parameters.Add("@ACTSTATUS", SqlDbType.Int).IsNullable = true;
            Command.Parameters.Add("@CENTSTATUS", SqlDbType.Int).IsNullable = true;
            Command.Parameters.Add("@OPERSTATUS", SqlDbType.Int).IsNullable = true;
            Command.Parameters.Add("@CURRSTATUS", SqlDbType.Int).IsNullable = true;
            Command.Parameters.Add("@STARTDATE", SqlDbType.Date).IsNullable = true;
            Command.Parameters.Add("@ENDDATE", SqlDbType.Date).IsNullable = true;
            Command.Parameters.Add("@NORMDOC", SqlDbType.NVarChar).IsNullable = true;
            Command.Parameters.Add("@LIVESTATUS", SqlDbType.Bit).IsNullable = true;
            Command.Parameters.Add("@CADNUM", SqlDbType.NVarChar).IsNullable = true;
            Command.Parameters.Add("@DIVTYPE", SqlDbType.Int).IsNullable = true;
            Command.Parameters.Add("@PLANCODE", SqlDbType.NVarChar).IsNullable = true;
        }
    }
}