using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Collections;
using System.Data;

namespace Tamasha.Database
{
    static class SQL
    {
        const string connectionString = "Data Source=DESKTOP-1B4R2U4;Initial Catalog=Mytube;Integrated Security=True;";
        
        public static void ExecuteNonQueryStoreProcedure(string storeProcedureName, string[]parameters, object[] values)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Create the Command and Parameter objects.
                SqlCommand command = new SqlCommand(storeProcedureName, connection);
                try
                {
                    command.CommandType = CommandType.StoredProcedure;
                    for (int i = 0; i < values.Length; i++)
                    {
                        command.Parameters.AddWithValue(parameters[i], values[i]);
                    }
                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("We are in Catch Block, the Exception is =>" + ex.Message);
                }
            }
        }

        public static List<string> ExecuteReaderStoreProcedure(string storeProcedureName, string[] parameters=null, object[] values=null)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Create the Command and Parameter objects.
                SqlCommand command = new SqlCommand(storeProcedureName, connection);
                List<string> strings = new List<string>();
                try
                {
                    command.CommandType = CommandType.StoredProcedure;
                    if(values != null)
                    {
                        for (int i = 0; i < values.Length; i++)
                        {
                            command.Parameters.AddWithValue(parameters[i], values[i]);
                        }
                    }
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        string temp = "";
                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            temp+=reader[i].ToString()+";";
                        }
                        strings.Add(temp);
                    }
                    reader.Read();
                    connection.Close();
                    return strings;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("We are in Catch Block, the Exception is =>" + ex.Message);
                    return strings;
                }
            }
        }

    }
}
