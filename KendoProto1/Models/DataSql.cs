using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace KendoProto1.Models
{
    public class DataSql
    {
        public static object ScalarCommand(string textCommand, SqlParameter[] spParameters = null)
        {
            object obResult = null;

            using (SqlConnection Conn = new SqlConnection(ConfigurationManager.ConnectionStrings["SqlConnString"].ConnectionString))
            {
                try
                {
                    Conn.Open();

                    using (SqlCommand Comm = new SqlCommand(textCommand, Conn))
                    {
                        Comm.CommandTimeout = 120;
                        Comm.CommandType = CommandType.StoredProcedure;

                        if (spParameters != null)
                        {
                            for (int i = 0; i < spParameters.Length; i++)
                            {

                                if (spParameters[i].Value == null)
                                {
                                    spParameters[i].Value = DBNull.Value;
                                }
                            }
                            Comm.Parameters.AddRange(spParameters);
                        }
                        obResult = Comm.ExecuteScalar();
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }

            return obResult;
        }
        public static List<object[]> GetAll(string textCommand, SqlParameter[] spParameters = null)
        {
            List<object[]> list = new List<object[]>();

            using (SqlConnection Conn = new SqlConnection(ConfigurationManager.ConnectionStrings["SqlConnString"].ConnectionString))
            {
                try
                {
                    Conn.Open();
                    using (SqlCommand Comm = new SqlCommand(textCommand, Conn))
                    {
                        Comm.CommandTimeout = 120;
                        Comm.CommandType = CommandType.StoredProcedure;

                        if (spParameters != null)
                        {
                            for (int i = 0; i < spParameters.Length; i++)
                            {
                                if (spParameters[i].Value == null)
                                {
                                    spParameters[i].Value = DBNull.Value;
                                }
                            }
                            Comm.Parameters.AddRange(spParameters);
                        }
                        using (SqlDataReader reader = Comm.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                object[] t = new object[reader.FieldCount];
                                for (int i = 0; i < reader.FieldCount; i++)
                                {
                                    t[i] = reader[i];

                                    if (System.DBNull.Value.Equals(t[i]))
                                    {
                                        t[i] = null;
                                    }
                                }
                                list.Add(t);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
                finally
                {
                    Conn.Close();
                }
            }

            return list;
        }
    }
}