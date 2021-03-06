﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;

namespace ProgTek_WebCrawler
{
    class RSSreader
    {
        private string downloadedRSS;
        private List<News> newsList = new List<News>();
        /// <summary>
        /// Will download the RSS feed, and then parse it into a list
        /// </summary>
        /// <param name="URL">URL string to RSS feed</param>
        /// <param name="fetchText">Y or N on fetching text</param>
        /// <param name="brodTagStart">Text Tag Start</param>
        /// <param name="brodTagStop">Text Tag Stop</param>
        public RSSreader(string URL, string fetchText, string brodTagStart, string brodTagStop)
        {
            downloadedRSS = new WebClient().DownloadString(URL);
            List<string> itemList = getValuesOfXMLvar(downloadedRSS, "item");
            foreach (string news in itemList)
            {
                string title = getFirstValueOfXMLvar(news, "title");
                string description = getFirstValueOfXMLvar(news, "description");
                string date = getFirstValueOfXMLvar(news, "pubDate");
                string category = getFirstValueOfXMLvar(news, "category");
                string link = getFirstValueOfXMLvar(news, "link");
                string text = "";
                if (fetchText.ToUpper() == "Y")
                {
                    text = MainTextFetcher(link, brodTagStart, brodTagStop);
                }
                newsList.Add(new News(title, description, date, category, link, text));
            }
        }

        /// <summary>
        /// Takes XML input, and gets information between searched object.
        /// </summary>
        /// <param name="inputString">Input XML</param>
        /// <param name="XMLvar">XML variable</param>
        /// <returns>Returns a string List of all matching</returns>
        private List<string> getValuesOfXMLvar(string inputString, string XMLvar)
        {
            int currentSearchPos = 0;
            List<string> returnList = new List<string>();
            while (true)
            {
                int varStart = inputString.IndexOf("<" + XMLvar + ">", currentSearchPos);
                if (varStart < 0) return returnList;
                int varEnd = inputString.IndexOf(@"</" + XMLvar + ">", currentSearchPos);
                currentSearchPos = varEnd + 1;
                returnList.Add(inputString.Substring(varStart + (XMLvar.Length + 2), varEnd - varStart - (XMLvar.Length + 2)));
            }
        }

        /// <summary>
        /// Takes XML input, and gets information between searched object.
        /// </summary>
        /// <param name="inputString">Input XML</param>
        /// <param name="XMLvar">XML variable</param>
        /// <returns>A single string with the value</returns>
        private string getFirstValueOfXMLvar(string inputString, string XMLvar)
        {
            int varStart = inputString.IndexOf("<" + XMLvar + ">");
            if (varStart < 0) { Console.WriteLine("Item not found: "+XMLvar); return ""; }
            int varEnd = inputString.IndexOf(@"</" + XMLvar + ">");
            return inputString.Substring(varStart + (XMLvar.Length + 2), varEnd - varStart - (XMLvar.Length + 2));
        }

        /// <summary>
        /// Fetches text from a website and gets only the text between the two Tags.
        /// </summary>
        /// <param name="URL">Website URL (with HTTP)</param>
        /// <param name="startingEnclosure">Starting Tag</param>
        /// <param name="endingEnclosure">Stop Tag</param>
        /// <returns></returns>
        public string MainTextFetcher(string URL, string startingEnclosure, string endingEnclosure)
        {
            //Download the HTML code from given URL
            string fetchedHTML = new WebClient().DownloadString(URL).ToString();
            //Search for the first occurance of first tag
            int firstPos = fetchedHTML.IndexOf(startingEnclosure);
            if (firstPos < 0) { Console.WriteLine("Main text fetch failed (1)."); return ""; }
            int secondPos = fetchedHTML.IndexOf(endingEnclosure, firstPos);
            if (secondPos < 0) { Console.WriteLine("Main text fetch failed (2)."); return ""; }
            //Fetch only the text of interest
            return fetchedHTML.Substring(firstPos + startingEnclosure.Length, secondPos - firstPos - startingEnclosure.Length);
        }

        /// <summary>
        /// Returns the news fetched in a List
        /// </summary>
        public List<News> NewsList
        {
            get { return newsList; }
        }
    }
}
