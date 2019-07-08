using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fias.SQL.DataSets
{
    public class BulkTableListItem
    {
        public string TableName { get; set; }
        public string TableSchema { get; set; }
        public FileInfo File { get; set; }
        public BulkTableListItem(string tableName, string tableSchema,FileInfo file)
        {
            TableName = tableName;
            TableSchema = tableSchema;
            File = file;
        }
    }
}
