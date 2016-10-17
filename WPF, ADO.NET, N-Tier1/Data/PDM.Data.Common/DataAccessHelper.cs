using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace PDM.Data.Common
{
    public static class DataAccessHelper
    {
        private static List<KeyValuePair<string, object>?> inputParamList = new List<KeyValuePair<string, object>?>();
        private static List<KeyValuePair<string, object>?> outputParamList = new List<KeyValuePair<string, object>?>();

        public static void AddInputParameters(string key, object value)
        {
            KeyValuePair<string, object>? inputParam = new KeyValuePair<string, object>(key, value);
            inputParamList.Add(inputParam);
        }

        public static void AddOutputParameters(string key, object value)
        {
            KeyValuePair<string, object>? OutputParam = new KeyValuePair<string, object>(key, value);
            outputParamList.Add(OutputParam);
        }

        public static int? ExecuteNonQuery(string storeProcedureName, IDbConnection connection)
        {
            int? result = null;
            IDataParameter parameters = null;
            //check for valid Connection
            if (connection != null)
            {
                using (IDbCommand command = connection.CreateCommand())
                {
                    command.CommandText = storeProcedureName;
                    command.CommandType = CommandType.StoredProcedure;
                    command.Connection = connection;
                    foreach (KeyValuePair<string, object> input in inputParamList)
                    {
                        command.Parameters.Add(new SqlParameter
                        {
                            ParameterName = input.Key,
                            Value = input.Value
                        });
                    }
                    foreach (KeyValuePair<string, object> ouput in outputParamList)
                    {
                        parameters = new SqlParameter
                        {
                            ParameterName = ouput.Key,
                            Value = ouput.Value,
                            Direction = ParameterDirection.Output
                        };
                        command.Parameters.Add(parameters);
                    }
                    try
                    {
                        connection.Open();
                        command.ExecuteNonQuery();
                    }
                    catch (SqlException ex)
                    {
                        command.Dispose();
                        connection.Close();
                    }
                }
                result = parameters.Value as int? ?? default(int);
            }
            inputParamList.Clear();
            outputParamList.Clear();
            return result;
        }

        public static IDataReader ExecuteReader(string storeProcedureName, IDbConnection connection)
        {
            IDataReader reader = null;
            if (connection != null)
            {
                using (IDbCommand command = connection.CreateCommand())
                {
                    command.CommandText = storeProcedureName;
                    command.CommandType = CommandType.StoredProcedure;
                    command.Connection = connection;

                    foreach (KeyValuePair<string, object> input in inputParamList)
                    {
                        command.Parameters.Add(new SqlParameter
                        {
                            ParameterName = input.Key,
                            Value = input.Value
                        });
                    }
                    try
                    {
                        connection.Open();
                        reader = command.ExecuteReader(CommandBehavior.CloseConnection);
                    }
                    catch (SqlException ex)
                    {
                        ex.Message.ToString();
                        connection.Close();
                        reader.Close();
                        command.Dispose();
                    }
                }
            }
            inputParamList.Clear();
            return reader;
        }

    }
}
