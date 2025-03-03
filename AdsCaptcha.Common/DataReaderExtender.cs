using System;
using System.Collections.Generic;
using System.Data;

namespace Inqwise.AdsCaptcha.Common
{
    /// <SUMMARY>
    /// Contains extension methods for the IDataReader interface. 
    /// </SUMMARY>
    public static class DataReaderExtender
    {
        /// <SUMMARY>
        /// This method will return the value of the specified columnName, cast to 
        /// the type specified in T. However, if the value found in the reader is 
        /// DBNull, this method will return the default value of the type T. 
        /// </SUMMARY>
        /// <TYPEPARAM name="T">The type to which the value found in the reader should be cast.</TYPEPARAM> 
        /// <PARAM name="reader">The reader in which columnName is found.</PARAM> 
        /// <PARAM name="columnName">The columnName to retrieve.</PARAM> 
        /// <RETURNS>The column value within the reader typed as T.</RETURNS> 
        public static T GetValueOrDefault<T>(this IDataReader reader, string columnName)
        {
            object columnValue = reader[columnName];
            T returnValue = default(T);
            if (!(columnValue is DBNull))
            {
                returnValue = (T) Convert.ChangeType(columnValue, typeof (T));
            }
            return returnValue;
        }

        /// <summary>
        /// Determines if the specified object is null or is a DBNull value
        /// </summary>
        /// <param name="rec">Object to test</param>
        /// <returns>True if the object is NULL or is a DBNull value</returns>
        private static bool IsDbNull(this Object rec) {
            return ((rec == null) || (rec == DBNull.Value));
        }

        /// <summary>
        /// Reads an Int32 value from the specified IDataReader. If the value is null, the
        /// defaultValue is returned
        /// </summary>
        /// <param name="rdr">IDataReader containing an integer field</param>
        /// <param name="field">The name of the field to read</param>
        /// <param name="defaultValue">Value to return in the case of a NULL value from the IDataReader</param>
        /// <returns>Int32 value contained in the specified field or defaultValue if the field is null</returns>
        public static int ReadInt(this IDataReader rdr, string field, int defaultValue) {
            int idx = rdr.GetOrdinal(field);
            if (rdr[idx].IsDbNull()) {
                return defaultValue;
            } else {
                return rdr.GetInt32(idx);
            }
        }

        /// <summary>
        /// Reads an int value from the specified IDataReader
        /// </summary>
        /// <param name="rdr">IDataReader containing the field to read</param>
        /// <param name="field">Name of the field to read</param>
        /// <returns>The int value contained in the field. Note: This method will cause an exception if the 
        /// field contains a null value.</returns>
        public static int ReadInt(this IDataReader rdr, string field) {
            int idx = rdr.GetOrdinal(field);
            return rdr.GetInt32(idx);
        }

        /// <summary>
        /// Reads a long int (MS SQL "BigInt") from the specified data reader
        /// </summary>
        /// <param name="rdr">IDataReader containing the field to read</param>
        /// <param name="field">Name of the field to read</param>
        /// <returns>Value of the referenced field</returns>
        public static Int64 ReadInt64(this IDataReader rdr, string field) {
            int idx = rdr.GetOrdinal(field);
            return rdr.GetInt64(idx);
        }

        /// <summary>
        /// Reads a long int (MS SQL "BigInt") from the specified data reader or returns defaultValue if the
        /// field is null
        /// </summary>
        /// <param name="rdr">IDataReader containing the field to read</param>
        /// <param name="field">Name of the field to read</param>
        /// <param name="defaultValue"></param>
        /// <returns>Int64 value contained in the specified field or defaultValue if the field is null</returns>
        public static Int64 ReadInt64(this IDataReader rdr, string field, Int64 defaultValue) {
            int idx = rdr.GetOrdinal(field);
            if (rdr[idx].IsDbNull()) {
                return defaultValue;
            } else {
                return rdr.GetInt64(idx);
            }
        }

        /// <summary>
        /// Reads a string from the specified IDataReaer
        /// </summary>
        /// <param name="rdr">IDataReader containing the field to read</param>
        /// <param name="field">Name of the field to read</param>
        /// <returns>Value of the referenced field</returns>
        public static string ReadString(this IDataReader rdr, string field) {
            int idx = rdr.GetOrdinal(field);
            return rdr[idx].ToString();
        }

        public static string SafeGetString(this IDataReader rdr, string field)
        {
            int idx = rdr.GetOrdinal(field);
            return rdr.IsDBNull(idx) ? null : rdr[idx].ToString();
        }

        /// <summary>
        /// Reads a boolean (MS SQL "Bit") value from the specified IDataReader
        /// </summary>
        /// <param name="rdr">IDataReader containing the field to read</param>
        /// <param name="field">Name of the field to read</param>
        /// <returns>Value of the referenced field or a default of false if the field is null</returns>
        public static bool ReadBool(this IDataReader rdr, string field) {
            int idx = rdr.GetOrdinal(field);
            if (rdr[idx].IsDbNull()) {
                return false;
            } else {
                return (bool)rdr[idx];
            }
        }

        /// <summary>
        /// Reads a DateTime value from the specified IDataReader
        /// </summary>
        /// <param name="rdr">IDataReader containing the field to read</param>
        /// <param name="field">Name of the field to read</param>
        /// <returns></returns>
        public static DateTime ReadDateTime(this IDataReader rdr, string field) {
            int idx = rdr.GetOrdinal(field);
            if (rdr[idx].IsDbNull()) {
                return DateTime.MinValue;
            } else {
                return (DateTime)rdr[idx];
            }
        }

        /// <summary>
        /// Reads a GUID (UniqueIdentifier MS SQL type) from an IDataReader
        /// </summary>
        /// <param name="rdr">IDataReader containing the field to read</param>
        /// <param name="field">Name of the field to read</param>
        /// <returns>Value of the referenced field or Guid.Empty if the field is null</returns>
        public static Guid ReadGuid(this IDataReader rdr, string field) {
            int idx = rdr.GetOrdinal(field);
            if (rdr[idx].IsDbNull()) {
                return Guid.Empty;
            } else {
                return (Guid)rdr[idx];
            }
        }

        /// <summary>
        /// Reads a single precision, floating point value from an IDataReader
        /// </summary>
        /// <param name="rdr">IDataReader containing the field to read</param>
        /// <param name="field">Name of the field to read</param>
        /// <returns>Value of the referenced field or 0 if the field is null</returns>
        public static float ReadFloat(this IDataReader rdr, string field) {
            int idx = rdr.GetOrdinal(field);
            if (rdr[idx].IsDbNull()) {
                return 0.0f;
            } else {
                return Convert.ToSingle(rdr[idx]);
            }
        }

        public static decimal ReadDecimal(this IDataReader rdr, string field)
        {
            int idx = rdr.GetOrdinal(field);
            if (rdr[idx].IsDbNull())
            {
                return default(decimal);
            }
            else
            {
                return Convert.ToDecimal(rdr[idx]);
            }
        }

        /// <summary>
        /// Executes the specified command and returns a list of entities from the reader that
        /// is built.
        /// This method should be used when the command is expected to return multiple records.
        /// </summary>
        /// <typeparam name="T">The type of the objects in the list.</typeparam>
        /// <param name="command">The command to execute.</param>
        /// <param name="getEntityMethod">A method that returns one entity from the
        /// current reader value.</param>
        /// <returns>List of entities.</returns>
        public static IList<T> GetEntities<T>(this IDbCommand command, Func<IDataReader, T> getEntityMethod)
        {
            List<T> list = new List<T>();
            using (IDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    T entity = getEntityMethod(reader);
                    if (entity != null)
                    {
                        list.Add(entity);
                    }
                }
            }
            return list;
        }

        /// <summary>
        /// Executes the specified command and returns one entity from the reader that is built.
        /// This method should be used when the command is expected to return a single record.
        /// </summary>
        /// <typeparam name="T">The type of the object to return.</typeparam>
        /// <param name="command">The command to execute.</param>
        /// <param name="getEntityMethod">A method that returns an entity from the
        /// current reader value.</param>        
        /// <returns>Entity.</returns>
        public static T GetEntity<T>(this IDbCommand command, Func<IDataReader, T> getEntityMethod)
        {
            using (IDataReader reader = command.ExecuteReader(CommandBehavior.SingleRow))
            {
                return reader.Read() ? getEntityMethod(reader) : default(T);
            }
        }

        /// <summary>
        /// Gets an integer value at the specified index from the specified reader,
        /// or 0 if the value is null.
        /// </summary>
        /// <param name="reader">Reader to get the value from.</param>
        /// <param name="columnIndex">Column index of the value.</param>
        /// <returns>Integer.</returns>
        public static int SafeGetInt32(this IDataReader reader, int columnIndex)
        {
            return !reader.IsDBNull(columnIndex) ? reader.GetInt32(columnIndex) : 0;
        }

        /// <summary>
        /// Gets a string value at the specified index from the specified reader,
        /// or null if the value is null.
        /// </summary>
        /// <param name="reader">Reader to get the value from.</param>
        /// <param name="columnIndex">Column index of the value.</param>
        /// <returns>String.</returns>
        public static string SafeGetString(this IDataReader reader, int columnIndex)
        {
            return !reader.IsDBNull(columnIndex) ? reader.GetString(columnIndex) : null;
        }

        /// <summary>
        /// Gets a float value at the specified index from the specified reader,
        /// or 0 if the value is null.
        /// </summary>
        /// <param name="reader">Reader to get the value from.</param>
        /// <param name="columnIndex">Column index of the value.</param>
        /// <returns>Float.</returns>
        public static float SafeGetFloat(this IDataReader reader, int columnIndex)
        {
            return !reader.IsDBNull(columnIndex) ? reader.GetFloat(columnIndex) : 0;
        }

        /// <summary>
        /// Gets a boolean value at the specified index from the specified reader,
        /// or false if the value is null.
        /// </summary>
        /// <param name="reader">Reader to get the value from.</param>
        /// <param name="columnIndex">Column index of the value.</param>
        /// <returns>Boolean.</returns>
        public static bool SafeGetBool(this IDataReader reader, int columnIndex)
        {
            return !reader.IsDBNull(columnIndex) ? reader.GetBoolean(columnIndex) : false;
        }

        /// <summary>
        /// Gets a byte array at the specified index from the specified reader,
        /// or null if the value is null.
        /// </summary>
        /// <param name="reader">Reader to get the value from.</param>
        /// <param name="columnIndex">Column index of the value.</param>
        /// <returns>Byte array.</returns>
        public static byte[] SafeGetByteArray(this IDataReader reader, int columnIndex)
        {
            return !reader.IsDBNull(columnIndex) ? (byte[])reader[columnIndex] : null;
        }

        /// <summary>
        /// Gets a DateTime value at the specified index from the specified reader,
        /// or null if the value is null.
        /// </summary>
        /// <param name="reader">Reader to get the value from.</param>
        /// <param name="columnIndex">Column index of the value.</param>
        /// <returns>DateTime.</returns>
        public static DateTime? SafeGetDateTime(this IDataReader reader, int columnIndex)
        {
            if (!reader.IsDBNull(columnIndex))
            {
                return reader.GetDateTime(columnIndex);
            }
            else
            {
                return null;
            }
        }

        public static int? OptInt(this IDataReader reader, string columnName)
        {
            int idx = reader.GetOrdinal(columnName);
            if (reader[idx].IsDbNull())
            {
                return null;
            }
            
            return Convert.ToInt32(reader[idx]); 
        }

        public static long? OptLong(this IDataReader reader, string columnName)
        {
            int idx = reader.GetOrdinal(columnName);
            if (reader[idx].IsDbNull())
            {
                return null;
            }

            return Convert.ToInt64(reader[idx]);
        }
    }
}