using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Oracle.DataAccess.Client;
using Oracle.DataAccess.Types;

namespace ProgTek_WebCrawler
{
    class OracleHandler
    {
        OracleConnection con;
        public OracleHandler()
        {
            con = new OracleConnection("Data Source=( DESCRIPTION = ( ADDRESS_LIST = ( ADDRESS = ( PROTOCOL = TCP )( HOST = sitas.hin.no )( PORT = 1521 ) ) )( CONNECT_DATA = ( SERVER = DEDICATED )( SERVICE_NAME = orcl.sitas.hin.no ) ) ); User Id= M42; Password = marvin;");
            con.Open();
        }
        public void insertNews(News input)
        {
            OracleCommand oc = con.CreateCommand();
            System.Data.SqlClient.SqlCommand dataCommand = new System.Data.SqlClient.SqlCommand();
            oc.CommandText = ("INSERT NYHET (URL, OVERSKRIFT, INGRESS, TIDSPUNKT, KATEGORI, BRODTEKST, BRODTEKSTHTML) VALUES (@input.Link, @input.Title, @input.Description, @input.Date, @input.Category, @input.Text, @input.???)");
            dataCommand.Parameters.AddWithValue("@input.Link", input.Link);
            dataCommand.Parameters.AddWithValue("@input.Title", input.Title);
            dataCommand.Parameters.AddWithValue("@input.Description", input.Description);
            dataCommand.Parameters.AddWithValue("@input.Date", input.Date);
            dataCommand.Parameters.AddWithValue("@input.Category", input.Category);
            dataCommand.Parameters.AddWithValue("@input.Text", input.Text);
 //brodtekst html?  dataCommand.Parameters.AddWithValue("@");
            dataCommand.ExecuteNonQuery();

        }
        public void testConnection()
        {
            if (con.State.ToString() != "Open")
            {
                con.Open();
            }
            Console.WriteLine("The Oracle connection is: "+con.State.ToString());

            OracleCommand oc = con.CreateCommand();
            oc.CommandText = "SELECT * FROM NYHETSBYRAA";

            OracleDataReader reader = oc.ExecuteReader();

            while (reader.Read())
            {
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    Console.Write(reader[i] + " ");
                }
                Console.WriteLine();
            }
        }
    }
}
