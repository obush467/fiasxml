using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fias.Operators
{
    public class DBFFileOperator : IMerge
    {
        public FileInfo File { get; set; }
        protected SqlConnection Connection { get; set; }
        public DBFFileOperator(FileInfo file, SqlConnection connection)
        {
            Connection = connection;
            File = file;
        }
        public virtual void Load_DBFToDb() { }
    }
        public class CommandDBFFileOperator: DBFFileOperator
        {
        public SqlCommand Command; 
       public CommandDBFFileOperator(FileInfo file, SqlConnection connection, SqlCommand command):base(file,connection)
        {
            Command = command;
        }
        public CommandDBFFileOperator(FileInfo file, SqlConnection connection) : base(file, connection)
        {
            this.Command = new SqlCommand()
            {
                CommandTimeout = 0,
                CommandType = System.Data.CommandType.StoredProcedure,
                Connection = connection
            };
        }
        public override void Load_DBFToDb()
        {
            try
            {
                using (NDbfReader.Table dbfTable = NDbfReader.Table.Open(File.Open(FileMode.Open)))
                using (DBFDataReader dbfReader = new DBFDataReader(dbfTable, Encoding.GetEncoding(866)))
                {
                    while (dbfReader.Read())
                    {
                        foreach (var c in dbfTable.Columns)
                            Command.Parameters["@" + c.Name].Value = dbfReader.GetValue(c);
                        Command.ExecuteNonQuery();
                    }
                }
                File.Delete();
            }
            catch (Exception e)
            {
                Logger.Logger.Error(e.Message);
            }
        }
    }
}
