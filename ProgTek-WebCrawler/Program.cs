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
            RSSreader reader = new RSSreader("http://www.vg.no/rss/create.php");
            List<News> list = reader.NewsList;
            foreach (News news in list)
            {
                Console.WriteLine(news.ToString());
                Console.WriteLine();
            }

            OracleHandler test = new OracleHandler();
            test.testConnection();

            Console.ReadKey();
        }
    }
}
