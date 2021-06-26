using CSharp_VincentHadinata_2301850430.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Text;

namespace CSharp_VincentHadinata_2301850430.Repositories
{
    class HeaderTransactionRepository
    {
        public static List<HeaderTransaction> viewTransaction_header()
        {
            SqlConnection connection = new SqlConnection(@"Data Source=VINCENT;Initial Catalog=marketDB;Integrated Security=True");
            SqlCommand command = new SqlCommand();
            SqlDataReader reader;

            List<HeaderTransaction> list_header = new List<HeaderTransaction>();

            string query = "SELECT ht.TransactionID,PaymentMethod,SUM(Price * dt.Quantity) AS Total FROM DetailTransaction dt JOIN HeaderTransaction ht ON ht.TransactionID = dt.TransactionID JOIN Product pr ON pr.ProductID = dt.ProductID GROUP BY ht.TransactionID, PaymentMethod";
            command.Connection = connection;
            command.CommandType = System.Data.CommandType.Text;
            command.CommandText = query;

            connection.Open();
            reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    HeaderTransaction header = new HeaderTransaction();
                    header.tr_id = Convert.ToInt32(reader["TransactionID"].ToString());
                    header.method = reader["PaymentMethod"].ToString();
                    header.total = Convert.ToInt32(reader["Total"].ToString());
                    list_header.Add(header);
                }
            }

            reader.Close();
            connection.Close();
            command.Dispose();

            return list_header;
        }
    }
}
