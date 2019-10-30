using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fias.Operators
{
    public interface IMerge
    {
        void Load_DBFToDb();
        FileInfo File { get; set; }
    }
}
