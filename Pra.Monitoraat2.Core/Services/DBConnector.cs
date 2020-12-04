using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace Pra.Monitoraat2.Core.Services
{
    public class DBConnector
    {
        public static DataTable ExecuteSelect(string selectInstruction)
        {
            string constring = Helper.GetConnectionString();
            DataSet ds = new DataSet();
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(selectInstruction, constring);
            try
            {
                sqlDataAdapter.Fill(ds);
            }
            catch (Exception fout)
            {
                //string foutmelding = fout.Message;
                return null;
            }
            return ds.Tables[0];
        }
        public static bool ExecuteCommand(string insertUpdateDeleteInstruction)
        {
            string constring = Helper.GetConnectionString();
            SqlConnection sqlConnection = new SqlConnection(constring);
            SqlCommand sqlCommand = new SqlCommand(insertUpdateDeleteInstruction, sqlConnection);
            try
            {
                sqlCommand.Connection.Open();
                sqlCommand.ExecuteNonQuery();
                return true;
            }
            catch (Exception fout)
            {
                //string foutmelding = fout.Message;
                return false;
            }
            finally
            {
                if (sqlConnection != null)
                    sqlConnection.Close();

            }
        }
        public static string ExecuteScalaire(string scalarSelectInstruction)
        {
            string constring = Helper.GetConnectionString();
            SqlConnection sqlConnection = new SqlConnection(constring);
            SqlCommand sqlCommand = new SqlCommand(scalarSelectInstruction, sqlConnection);
            try
            {
                sqlConnection.Open();
                return sqlCommand.ExecuteScalar().ToString();

            }
            catch (Exception fout)
            {
                //string foutmelding = fout.Message;
                return null;
            }
            finally
            {
                if (sqlConnection != null)
                    sqlConnection.Close();
            }
        }
    }
}
