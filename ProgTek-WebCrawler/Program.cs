using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProgTek_WebCrawler
{
    class Program
    {
        static void Main(string[] args)
        {
            //Test rutine
            OracleHandler OH = new OracleHandler();
            foreach (Nyhetsbyraa NB in OH.getNyhetsbyraa())
            {
                foreach (News news in new RSSreader(NB.URL, NB.KanViseBrodtekst, NB.BrodTagStart, NB.BrodTagStop).NewsList)
                {
                    Console.WriteLine(news.Date);
                    Console.WriteLine();
                }
            }

            //End of test rutine
            //RSSreader reader = new RSSreader("http://www.vg.no/rss/create.php", 'Y', "<!-- Article text -->", "<!-- End of \"artikkel_felt\" -->");
            //foreach (News news in reader.NewsList)
            //{
            //    Console.WriteLine(news.ToString());
            //    Console.WriteLine();
            //}

            //OracleHandler test = new OracleHandler();
            //test.testConnection();

            Console.ReadKey();
        }
    }
}
