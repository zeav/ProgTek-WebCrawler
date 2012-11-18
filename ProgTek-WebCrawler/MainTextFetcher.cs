using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;

namespace ProgTek_WebCrawler
{
    class MainTextFetcher
    {
        private string mainText;
        public MainTextFetcher(string URL, string startingEnclosure, string endingEnclosure)
        {
            //Download the HTML code from given URL
            string fetchedHTML = new WebClient().DownloadString(URL).ToString();
            //Search for the first occurance of first tag
            int firstPos = fetchedHTML.IndexOf(startingEnclosure);
            int secondPos = fetchedHTML.IndexOf(endingEnclosure, firstPos);
            //Fetch only the text of interest
            mainText = fetchedHTML.Substring(firstPos + startingEnclosure.Length, secondPos - firstPos - startingEnclosure.Length);
        }

        public string MainText
        {
            get { return mainText; }
        }
    }
}
