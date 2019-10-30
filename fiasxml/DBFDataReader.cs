using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NDbfReader;

namespace Fias
{
    public class DBFDataReader : Reader, IDataReader
    {
        Dictionary<string, Column> vvv = new Dictionary<string, Column>(); 
        public DBFDataReader(Table table, Encoding encoding):base(table,encoding)
        {

        }

        public object this[int i] => base.GetValue(base.Table.Columns[i].Name);

        public object this[string name] => base.GetValue(name);

        public int Depth => 0;

        public bool IsClosed => false;

        public int RecordsAffected => throw new NotImplementedException();

        public int FieldCount => base.Table.Columns.Count;

        public void Close()
        {
            base.Table.Dispose();
        }

        public bool GetBoolean(int i)
        {
            return (bool)base.GetBoolean(base.Table.Columns[i].Name);
        }

        public byte GetByte(int i)
        {            
            return (byte)base.GetValue(base.Table.Columns[i].Name);
        }

        public long GetBytes(int i, long fieldOffset, byte[] buffer, int bufferoffset, int length)
        {
            base.GetBytes(base.Table.Columns[i].Name, buffer, bufferoffset);
            return buffer.Length;
        }

        public char GetChar(int i)
        {
            return base.GetString(base.Table.Columns[i].Name)[0];
        }

        public long GetChars(int i, long fieldoffset, char[] buffer, int bufferoffset, int length)
        {
            buffer = base.GetString(base.Table.Columns[i].Name).ToCharArray();
            return buffer.Length;
        }

        public IDataReader GetData(int i)
        {
            return null;
        }

        public string GetDataTypeName(int i)
        {
            return base.Table.Columns[i].Type.Name;
        }

        public DateTime GetDateTime(int i)
        {
            return (DateTime)base.GetDateTime(base.Table.Columns[i].Name);
        }

        public decimal GetDecimal(int i)
        {
            return (Decimal)base.GetDecimal(base.Table.Columns[i].Name);
        }

        public double GetDouble(int i)
        {
            return base.GetValue<double>(base.Table.Columns[i].Name);
        }

        public Type GetFieldType(int i)
        {
            return base.Table.Columns[i].Type;
        }

        public float GetFloat(int i)
        {
            return (float)base.GetValue(base.Table.Columns[i].Name);
        }

        public Guid GetGuid(int i)
        {
            return base.GetValue<Guid>(base.Table.Columns[i].Name);
        }

        public short GetInt16(int i)
        {
            return base.GetValue<short>(base.Table.Columns[i].Name);
        }

        public int GetInt32(int i)
        {
            return base.GetInt32(base.Table.Columns[i].Name);
        }

        public long GetInt64(int i)
        {
            return base.GetValue<Int64>(base.Table.Columns[i].Name);
        }

        public string GetName(int i)
        {
            return base.Table.Columns[i].Name;
        }

        public int GetOrdinal(string name)
        {
            return base.Table.Columns.ToList().IndexOf(base.Table.Columns[name]);            
        }

        public DataTable GetSchemaTable()
        {
            return base.Table.AsDataTable();
        }

        public string GetString(int i)
        {
            return base.GetString(base.Table.Columns[i]);
        }

        public object GetValue(int i)
        {
            return base.GetValue(base.Header.Columns[i]);
        }

        public int GetValues(object[] values)
        {
            for (var n = 0; n < base.Table.Columns.Count;n++)
                values[n] = GetValue(n);
            return base.Table.Columns.Count;
        }

        public bool IsDBNull(int i)
        {
            return  base.GetValue(base.Table.Columns[i])==null? true:false;
        }

        public bool NextResult()
        {
            return base.Read();
        }

        #region IDisposable Support
        private bool disposedValue = false; // Для определения избыточных вызовов

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: освободить управляемое состояние (управляемые объекты).
                }

                // TODO: освободить неуправляемые ресурсы (неуправляемые объекты) и переопределить ниже метод завершения.
                // TODO: задать большим полям значение NULL.

                disposedValue = true;
            }
        }

        // TODO: переопределить метод завершения, только если Dispose(bool disposing) выше включает код для освобождения неуправляемых ресурсов.
        // ~DBFDataReader()
        // {
        //   // Не изменяйте этот код. Разместите код очистки выше, в методе Dispose(bool disposing).
        //   Dispose(false);
        // }

        // Этот код добавлен для правильной реализации шаблона высвобождаемого класса.
        public void Dispose()
        {
            // Не изменяйте этот код. Разместите код очистки выше, в методе Dispose(bool disposing).
            Dispose(true);
            // TODO: раскомментировать следующую строку, если метод завершения переопределен выше.
            // GC.SuppressFinalize(this);
        }
        #endregion


    }
}
