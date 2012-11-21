using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Oracle.DataAccess.Client;
using Oracle.DataAccess.Types;
using System.Security;
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
        public void insertNews(News input, int NyhetsbyraaId)
        {
            if (con.State.ToString() != "Open") 
            {
                con.Open(); 
            }
            OracleCommand oc = con.CreateCommand();
            oc.CommandText = ("INSERT INTO NYHET (NYHETSBYRAA_ID, URL, OVERSKRIFT, INGRESS, TIDSPUNKT, KATEGORI, BRODTEKST, BRODTEKSTHTML) VALUES ('" + NyhetsbyraaId.ToString() + "', '" + input.Link + "', '" + SecurityElement.Escape( input.Title ) + "', '" + SecurityElement.Escape( input.Description) + "', " + DateTimeToOracleDate( input.Date ) + ", '" + SecurityElement.Escape( input.Category) + "', '" + SecurityElement.Escape(input.Text) + "', '" + SecurityElement.Escape(input.HTML) + "')");
            oc.ExecuteNonQuery();
        }
        private string DateTimeToOracleDate(DateTime input)
        {
            return "to_date('" + input.ToString() + "', 'DD.MM.YYYY HH24:MI:SS')";
            //return "to_date('" + input.Day.ToString() + input.Month.ToString() + input.Year.ToString() + input.Hour.ToString() + input.Minute.ToString() + input.Second.ToString() + "','DDMMYYYYHH24MISS')";
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
