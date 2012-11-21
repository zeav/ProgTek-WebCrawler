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
        private string date;
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
            this.date = Date;
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
        public string Date { get { return date; } }
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
    }
}
