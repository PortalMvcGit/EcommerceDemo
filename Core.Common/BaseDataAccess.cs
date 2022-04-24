using Microsoft.Extensions.Configuration;
using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.IO;
using System.Text;

namespace Core.Common
{
    public static class BaseDataAccess
    {
        public static string ConnectionString { get; set; }

        public static SqlConnection GetConnection()
        {
            SqlConnection connection = new SqlConnection(ConnectionString);
            if (connection.State != ConnectionState.Open)
                connection.Open();
            return connection;
        }

        public static DbCommand GetCommand(DbConnection connection, string commandText, CommandType commandType)
        {
            SqlCommand command = new SqlCommand(commandText, connection as SqlConnection);
            command.CommandType = commandType;
            return command;
        }

        public static SqlParameter GetParameter(string parameter, object value)
        {
            SqlParameter parameterObject = new SqlParameter(parameter, value != null ? value : DBNull.Value);
            parameterObject.Direction = ParameterDirection.Input;
            if (value is Dictionary<int, string>)
            {
                parameterObject.SqlDbType = SqlDbType.Structured;
                parameterObject.TypeName = "dbo.ProdAttrDictionary";
                parameterObject.Value = SendRows((Dictionary<int, string>)value);
            }
            return parameterObject;
        }

        private static IEnumerable<SqlDataRecord> SendRows(Dictionary<int, string> RowData)
        {
            SqlMetaData[] _TvpSchema = new SqlMetaData[] {
      new SqlMetaData("ID", SqlDbType.Int),
      new SqlMetaData("ProdAttr", SqlDbType.NVarChar,300)
   };
            SqlDataRecord _DataRecord = new SqlDataRecord(_TvpSchema);
            StreamReader _FileReader = null;

            // read a row, send a row
            foreach (var _CurrentRow in RowData)
            {
                // You shouldn't need to call "_DataRecord = new SqlDataRecord" as
                // SQL Server already received the row when "yield return" was called.
                // Unlike BCP and BULK INSERT, you have the option here to create an
                // object, do manipulation(s) / validation(s) on the object, then pass
                // the object to the DB or discard via "continue" if invalid.
                _DataRecord.SetInt32(0, _CurrentRow.Key);
                _DataRecord.SetString(1, _CurrentRow.Value.ToString());

                yield return _DataRecord;
            }
        }

        public static SqlParameter GetParameterOut(string parameter, SqlDbType type, object value = null, ParameterDirection parameterDirection = ParameterDirection.InputOutput)
        {
            SqlParameter parameterObject = new SqlParameter(parameter, type); ;

            if (type == SqlDbType.NVarChar || type == SqlDbType.VarChar || type == SqlDbType.NText || type == SqlDbType.Text)
            {
                parameterObject.Size = -1;
            }

            parameterObject.Direction = parameterDirection;

            if (value != null)
            {
                parameterObject.Value = value;
            }
            else
            {
                parameterObject.Value = DBNull.Value;
            }

            return parameterObject;
        }

        public static int ExecuteNonQuery(string procedureName, List<DbParameter> parameters, CommandType commandType = CommandType.StoredProcedure)
        {
            int returnValue = -1;

            try
            {
                using (SqlConnection connection = BaseDataAccess.GetConnection())
                {
                    DbCommand cmd = BaseDataAccess.GetCommand(connection, procedureName, commandType);

                    if (parameters != null && parameters.Count > 0)
                    {
                        cmd.Parameters.AddRange(parameters.ToArray());
                    }

                    returnValue = cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                //LogException("Failed to ExecuteNonQuery for " + procedureName, ex, parameters);
                throw;
            }

            return returnValue;
        }

        public static object ExecuteScalar(string procedureName, List<SqlParameter> parameters)
        {
            object returnValue = null;

            try
            {
                using (DbConnection connection = BaseDataAccess.GetConnection())
                {
                    DbCommand cmd = BaseDataAccess.GetCommand(connection, procedureName, CommandType.StoredProcedure);

                    if (parameters != null && parameters.Count > 0)
                    {
                        cmd.Parameters.AddRange(parameters.ToArray());
                    }

                    returnValue = cmd.ExecuteScalar();
                }
            }
            catch (Exception ex)
            {
                //LogException("Failed to ExecuteScalar for " + procedureName, ex, parameters);
                throw;
            }

            return returnValue;
        }

        public static DbDataReader GetDataReader(string procedureName, List<DbParameter> parameters, CommandType commandType = CommandType.StoredProcedure)
        {
            DbDataReader ds;
            DataTable table = new DataTable();
            try
            {
                DbConnection connection = BaseDataAccess.GetConnection();
                {
                    DbCommand cmd = BaseDataAccess.GetCommand(connection, procedureName, commandType);
                    if (parameters != null && parameters.Count > 0)
                    {
                        cmd.Parameters.AddRange(parameters.ToArray());
                    }

                    ds = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                }
            }
            catch (Exception ex)
            {
                //LogException("Failed to GetDataReader for " + procedureName, ex, parameters);
                throw;
            }

            return ds;
        }
    }
}