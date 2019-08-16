using System;
using System.Data.SqlClient;
using Fias.Operators;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestFias
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var conn = new SqlConnection("Data Source=BUSHMAKIN;Initial Catalog=UNS;Integrated Security=True");
            conn.Open();
            var b = new FiasOperatorDBF(new System.IO.DirectoryInfo("C:\\Users\\Bushmakin\\Documents\\Новая папка\\Compressed\\fias_dbf"), conn, "dbo");
            b.Load("ttt");
        }
    }
}
