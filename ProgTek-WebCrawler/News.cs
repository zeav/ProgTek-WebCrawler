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
        /// <summary>
        /// Creates an news object
        /// </summary>
        /// <param name="Title">Title of News</param>
        /// <param name="Description">Description of News</param>
        /// <param name="Date">Date of News published</param>
        /// <param name="Category">Category the News belongs to</param>
        /// <param name="Link">The URL the news is located</param>
        public News(string Title, string Description, string Date, string Category, string Link)
        {
            this.title = Title;
            this.description = Description;
            this.date = Date;
            this.category = Category;
            this.link = Link;
        }

        public string Title { get { return title; } }
        public string Description { get { return description; } }
        public string Date { get { return date; } }
        public string Category { get { return category; } }
        public string Link { get { return link; } }

        public override string ToString()
        {
            return ("-News-\r\n") + ("Title: " + Title + "\r\n") + ("Description: " + Description + "\r\n") + ("Date: " + Date + "\r\n") + ("Category: " + Category + "\r\n") + ("Link: " + Link);
        }
    }
}
