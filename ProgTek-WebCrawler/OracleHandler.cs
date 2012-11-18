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
