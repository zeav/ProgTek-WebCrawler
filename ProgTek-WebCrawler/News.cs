using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProgTek_WebCrawler
{
    class News
    {
        private string title;
        private string link;
        private string description;
        private DateTime date;
        private string category;
        private string text;
        private string html;
        /// <summary>
        /// Creates an news object
        /// </summary>
        /// <param name="Title">Title of News</param>
        /// <param name="Description">Description of News</param>
        /// <param name="Date">Date of News published</param>
        /// <param name="Category">Category the News belongs to</param>
        /// <param name="Link">The URL the news is located</param>
        /// <param name="Text">The main text associated with the news</param>
        public News(string Title, string Description, string Date, string Category, string Link, string Text)
        {
            this.title = Title;
            this.description = Description;
            this.date = RFC1123toDate(Date);
            this.category = Category;
            this.link = Link;
            this.html = Text;
            this.text = HTMLTagCleaner(Text);
        }
        /// <summary>
        /// Title of the News
        /// </summary>
        public string Title { get { return title; } }
        /// <summary>
        /// Summary of the News
        /// </summary>
        public string Description { get { return description; } }
        /// <summary>
        /// Date published
        /// </summary>
        public DateTime Date { get { return date; } }
        /// <summary>
        /// Category of the News
        /// </summary>
        public string Category { get { return category; } }
        /// <summary>
        /// The direct URL of the News
        /// </summary>
        public string Link { get { return link; } }
        /// <summary>
        /// The text without HTML tags
        /// </summary>
        public string Text { get { return text; } }
        /// <summary>
        /// The text with HTML tags
        /// </summary>
        public string HTML { get { return html; } }

        public override string ToString()
        {
            return ("-News-\r\n") + ("Title: " + Title + "\r\n") + ("Description: " + Description + "\r\n") + ("Date: " + Date + "\r\n") + ("Category: " + Category + "\r\n") + ("Link: " + Link + "\r\n") + ("Text: " + Text);
        }

        private string HTMLTagCleaner(string input)
        {
            int continueSearchPos = 0;
            //Is ATM kinda uneffective, because it starts at the beginning for each search. Instead of continue to look from where it left off. Will implement soon. FIXED
            while (true)
            {
                int firstPos = input.IndexOf('<', continueSearchPos);
                if (firstPos < 0) return input;
                //Implemented to save searching time, so it will continue where it left off without having to search the entire string again.
                continueSearchPos = firstPos;
                int secondPos = input.IndexOf('>', firstPos) + 1;
                //Now split it up, and merge them together without the <>
                input = input.Substring(0, firstPos) + input.Substring(secondPos);
            }
        }
        /// <summary>
        /// Turns RFC1123 date to DateTime C# object. Example RFC1123: Wed, 21 Nov 2012 04:13:51 GMT
        /// </summary>
        /// <param name="input">RFC1123 Date format</param>
        /// <returns>DateTime object</returns>
        private DateTime RFC1123toDate(string input)
        {
            //Wed, 21 Nov 2012 04:13:51 GMT
            int day     = int.Parse(input.Substring(5, 2));
            string monthString = input.Substring(8, 3);
            int month = 0;
            switch(monthString.ToLower())
            {
                case "jan": month=1; break;
                case "feb": month=2; break;
                case "mar": month=3; break; 
                case "apr": month=4; break;
                case "may": month=5; break;
                case "jun": month=6; break;
                case "jul": month=7; break;
                case "aug": month=8; break; 
                case "sep": month=9; break;
                case "oct": month=10; break;
                case "nov": month=11; break;
                case "des": month=12; break;
            }
            int year    = int.Parse(input.Substring(12, 4));
            int hour    = int.Parse(input.Substring(17, 2));
            int minute  = int.Parse(input.Substring(20, 2));
            int second  = int.Parse(input.Substring(23, 2));

            return new DateTime(year, month, day, hour, minute, second);
        }
    }
}
