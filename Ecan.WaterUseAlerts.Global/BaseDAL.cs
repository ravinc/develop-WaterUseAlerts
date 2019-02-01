using System;
using System.Data;

namespace Ecan.WaterUseAlerts.Global
{
    public abstract class BaseDAL
    {

        public int? GetNullableInt(string columnName, IDataReader reader)
        {
            int? val = null;
            object oValue = reader[columnName];
            if (!Convert.IsDBNull(oValue))
                val = Convert.ToInt32(oValue);
            return val;
        }

        public int GetSmallInt(string columnName, IDataReader dataReader)
        {
            return Convert.ToInt16(dataReader[columnName]);
        }

        #region Data Conversion Methods
        protected T GetValueFromReader<T>(string columnName, IDataReader reader)
        {
            T Value;
            try
            {
                int Index = reader.GetOrdinal(columnName);
                object obj = reader.GetValue(Index);

                if (obj.GetType() == typeof(decimal))
                {
                    Value = (T)((object)int.Parse(obj.ToString()));
                }
                else
                {
                    if (obj is System.DBNull)
                    {
                        Value = default(T);
                    }
                    else
                    {
                        Value = (T)obj;
                    }

                }
                //Value = (T)reader[columnName];
            }
            catch
            {
                return default(T);
            }
            return Value;
        }

        protected object nullToDbNull(object value)
        {
            if (value == null)
            {
                return DBNull.Value;
            }

            return value;
        }

        protected object NullToEmptyString(object value)
        {
            if (value == null)
            {
                return string.Empty;
            }

            return value;
        }

        protected bool GetBool(string columnName, IDataReader reader)
        {
            return (bool)reader[columnName];
        }

        protected byte[] GetByteArray(string columnName, IDataReader reader)
        {
            return (byte[])reader[columnName];
        }

        protected char GetChar(string columnName, IDataReader reader)
        {
            return (char)reader[columnName];
        }

        protected DateTime GetDateTime(string columnName, IDataReader reader)
        {
            return (DateTime)reader[columnName];
        }

        protected decimal GetDecimal(string columnName, IDataReader reader)
        {
            return (decimal)reader[columnName];
        }

        protected double GetDouble(string columnName, IDataReader reader)
        {
            return (double)reader[columnName];
        }

        protected float GetFloat(string columnName, IDataReader reader)
        {
            return (float)reader[columnName];
        }

        protected int GetInt(string columnName, IDataReader reader)
        {
            return (int)reader[columnName];
        }

        protected long GetLong(string columnName, IDataReader reader)
        {
            return (long)reader[columnName];
        }

        protected bool? GetNullableBool(string columnName, IDataReader reader)
        {
            bool? val = null;
            object oValue = reader[columnName];
            if (!Convert.IsDBNull(oValue))
                val = Convert.ToBoolean(oValue);
            return val;
        }

        protected byte?[] GetNullableByteArray(string columnName, IDataReader reader)
        {
            byte?[] val = null;
            object oValue = reader[columnName];
            if (!Convert.IsDBNull(oValue))
                val = (byte?[])oValue;
            return val;
        }

        protected char? GetNullableChar(string columnName, IDataReader reader)
        {
            char? val = null;
            object oValue = reader[columnName];
            if (!Convert.IsDBNull(oValue))
                val = Convert.ToChar(oValue);
            return val;
        }

        protected DateTime? GetNullableDateTime(string columnName, IDataReader reader)
        {
            DateTime? val = null;
            object oValue = reader[columnName];
            if (!Convert.IsDBNull(oValue))
                val = Convert.ToDateTime(oValue);
            return val;
        }

        protected decimal? GetNullableDecimal(string columnName, IDataReader reader)
        {
            decimal? val = null;
            object oValue = reader[columnName];
            if (!Convert.IsDBNull(oValue))
                val = Convert.ToDecimal(oValue);
            return val;
        }

        protected double? GetNullableDouble(string columnName, IDataReader reader)
        {
            double? val = null;
            object oValue = reader[columnName];
            if (!Convert.IsDBNull(oValue))
                val = Convert.ToDouble(oValue);
            return val;
        }

        protected float? GetNullableFloat(string columnName, IDataReader reader)
        {
            float? val = null;
            object oValue = reader[columnName];
            if (!Convert.IsDBNull(oValue))
                return (float)oValue;
            return val;
        }

        protected long? GetNullableLong(string columnName, IDataReader reader)
        {
            long? val = null;
            object oValue = reader[columnName];
            if (!Convert.IsDBNull(oValue))
                val = Convert.ToInt64(oValue);
            return val;
        }

        protected string GetString(string columnName, IDataReader reader)
        {
            string val = null;
            object oValue = reader[columnName];
            if (!Convert.IsDBNull(oValue))
                val = Convert.ToString(oValue);
            return val;
        }

        protected bool GetBool(string columnName, DataRow dataRow)
        {
            return Convert.ToBoolean(dataRow[columnName]);
        }

        protected byte[] GetByteArray(string columnName, DataRow dataRow)
        {
            return (byte[])dataRow[columnName];
        }

        protected char GetChar(string columnName, DataRow dataRow)
        {
            return Convert.ToChar(dataRow[columnName]);
        }

        protected DateTime GetDateTime(string columnName, DataRow dataRow)
        {
            return Convert.ToDateTime(dataRow[columnName]);
        }

        protected decimal GetDecimal(string columnName, DataRow dataRow)
        {
            return Convert.ToDecimal(dataRow[columnName]);
        }

        protected double GetDouble(string columnName, DataRow dataRow)
        {
            return Convert.ToDouble(dataRow[columnName]);
        }

        protected float GetFloat(string columnName, DataRow dataRow)
        {
            return Convert.ToSingle(dataRow[columnName]);
        }

        protected int GetInt(string columnName, DataRow dataRow)
        {
            return Convert.ToInt32(dataRow[columnName]);
        }

        protected long GetLong(string columnName, DataRow dataRow)
        {
            return Convert.ToInt64(dataRow[columnName]);
        }

        protected bool? GetNullableBool(string columnName, DataRow dataRow)
        {
            bool? val = null;
            object oValue = dataRow[columnName];
            if (!Convert.IsDBNull(oValue))
                val = Convert.ToBoolean(oValue);
            return val;
        }

        protected static byte?[] GetNullableByteArray(string columnName, DataRow dataRow)
        {
            byte?[] val = null;
            object oValue = dataRow[columnName];
            if (!Convert.IsDBNull(oValue))
                val = (byte?[])oValue;
            return val;
        }

        protected char? GetNullableChar(string columnName, DataRow dataRow)
        {
            char? val = null;
            object oValue = dataRow[columnName];
            if (!Convert.IsDBNull(oValue))
                val = Convert.ToChar(oValue);
            return val;
        }

        protected DateTime? GetNullableDateTime(string columnName, DataRow dataRow)
        {
            DateTime? val = null;
            object oValue = dataRow[columnName];
            if (!Convert.IsDBNull(oValue))
                val = Convert.ToDateTime(oValue);
            return val;
        }

        protected decimal? GetNullableDecimal(string columnName, DataRow dataRow)
        {
            decimal? val = null;
            object oValue = dataRow[columnName];
            if (!Convert.IsDBNull(oValue))
                val = Convert.ToDecimal(oValue);
            return val;
        }

        protected double? GetNullableDouble(string columnName, DataRow dataRow)
        {
            double? val = null;
            object oValue = dataRow[columnName];
            if (!Convert.IsDBNull(oValue))
                val = Convert.ToDouble(oValue);
            return val;
        }

        protected float? GetNullableFloat(string columnName, DataRow dataRow)
        {
            float? val = null;
            object oValue = dataRow[columnName];
            if (!Convert.IsDBNull(oValue))
                val = Convert.ToSingle(oValue);
            return val;
        }

        protected int? GetNullableInt(string columnName, DataRow dataRow)
        {
            int? val = null;
            object oValue = dataRow[columnName];
            if (!Convert.IsDBNull(oValue))
                val = Convert.ToInt32(oValue);
            return val;
        }

        protected long? GetNullableLong(string columnName, DataRow dataRow)
        {
            long? val = null;
            object oValue = dataRow[columnName];
            if (!Convert.IsDBNull(oValue))
                val = Convert.ToInt64(oValue);
            return val;
        }

        protected string GetString(string columnName, DataRow dataRow)
        {
            string val = null;
            object oValue = dataRow[columnName];
            if (!Convert.IsDBNull(oValue))
                val = Convert.ToString(oValue);
            return val;
        }

    protected Guid GetGuid(string columnName, IDataReader reader)
    {
        Guid g = new Guid(reader[columnName].ToString());
        return g;
    }
        protected string Truncate(string source, int length)
        {
            if (source.Length > length)
            {
                source = source.Substring(0, length);
            }
            return source;
        }


        #endregion
    }
}
