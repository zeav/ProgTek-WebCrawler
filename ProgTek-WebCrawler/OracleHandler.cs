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
        public List<Nyhetsbyraa> getNyhetsbyraa()
        {
            if (con.State.ToString() != "Open")
            {
                con.Open();
            }

            List<Nyhetsbyraa> byraaList = new List<Nyhetsbyraa>();

            OracleCommand oc = con.CreateCommand();
            oc.CommandText = "select id, navn, sokefrekvens, type, url, KANVISEBRODTEKST, brodtagstart, brodtagstop from nyhetsbyraa";
            OracleDataReader reader = oc.ExecuteReader();

            while (reader.Read())
            {
                int id = (Int16)reader[0];
                string navn = (string)reader[1];
                int sokefrekvens = 5;
                if (reader[2] != DBNull.Value)
                    sokefrekvens = (Int16)reader[2];
                string brodTagStart = "";
                if (reader[6] != DBNull.Value)
                    brodTagStart = (string)reader[6];
                string brodTagStop = "";
                if (reader[7] != DBNull.Value)
                    brodTagStop = (string)reader[7];
                string kanvisebrodtekst = "N";
                if (reader[5] != DBNull.Value)
                    kanvisebrodtekst = (string)reader[5];

                byraaList.Add(new Nyhetsbyraa(id, sokefrekvens, navn, (string)reader[3], (string)reader[4], brodTagStart, brodTagStop, kanvisebrodtekst));
            }
            return byraaList;
        }

        public void insertNews(News input)
        {
            if (con.State.ToString() != "Open")
            {
                con.Open();
            }
            OracleCommand oc = con.CreateCommand();
            System.Data.SqlClient.SqlCommand dataCommand = new System.Data.SqlClient.SqlCommand();
            oc.CommandText = ("INSERT NYHET (URL, OVERSKRIFT, INGRESS, TIDSPUNKT, KATEGORI, BRODTEKST, BRODTEKSTHTML) VALUES (@input.Link, @input.Title, @input.Description, @input.Date, @input.Category, @input.Text, @input.html)");
            dataCommand.Parameters.AddWithValue("@input.Link", input.Link);
            dataCommand.Parameters.AddWithValue("@input.Title", input.Title);
            dataCommand.Parameters.AddWithValue("@input.Description", input.Description);
            dataCommand.Parameters.AddWithValue("@input.Date", input.Date);
            dataCommand.Parameters.AddWithValue("@input.Category", input.Category);
            dataCommand.Parameters.AddWithValue("@input.Text", input.Text);
            dataCommand.Parameters.AddWithValue("@input.html", input.HTML);
            //brodtekst html?  dataCommand.Parameters.AddWithValue("@");
            dataCommand.ExecuteNonQuery();

        }

        public string getToDateFromRFC1123(string input)
        {
            //Wed, 21 Nov 2012 04:13:51 GMT
            string day = input.Substring(5, 2);
            string month = input.Substring(8, 3);
            string year = input.Substring(12, 4);
            string hour = input.Substring(17, 2);
            string minute = input.Substring(20, 2);
            string second = input.Substring(23, 2);

            return "to_date('"+day+month+year+hour+minute+second+"', 'ddmmyyyyHH24MISS')";
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
