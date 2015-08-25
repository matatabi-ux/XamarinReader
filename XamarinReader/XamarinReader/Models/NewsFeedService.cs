#region License
//-----------------------------------------------------------------------
// <copyright>
//     Copyright matatabi-ux 2015.
// </copyright>
//-----------------------------------------------------------------------
#endregion

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace XamarinReader.Models
{
    /// <summary>
    /// News feed management service class
    /// </summary>
    public class NewsFeedService : INewsFeedService
    {
        /// <summary>
        /// RSS feed endpoint uri
        /// </summary>
        private static readonly string Endpoint = @"http://feeds.feedburner.com/TechCrunch/";

        /// <summary>
        /// Feed items
        /// </summary>
        public Rss Feed { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public NewsFeedService()
        {
            this.Feed = new Rss();
        }

        /// <summary>
        /// Update feeds
        /// </summary>
        /// <returns>Task</returns>
        public async Task Update()
        {
            var client = new HttpClient();
            Rss latest;

            using (var reader = new StringReader(await client.GetStringAsync(Endpoint)))
            {
                var desirializer = new XmlSerializer(typeof(Rss));
                latest = desirializer.Deserialize(reader) as Rss;
            }

            if (latest == null || latest.Channel.LastBuildDate.CompareTo(this.Feed.Channel.LastBuildDate) <= 0)
            {
                return;
            }

            // Merge old feed items and new feed items
            var oldItems = (from f in this.Feed.Channel.Items
                            where !latest.Channel.Items.Any(l => l.Equals(f.Guid))
                            orderby f.PubDate descending 
                            select f).ToList();
            foreach (var old in oldItems)
            {
                latest.Channel.Items.Add(old);
            }

            this.Feed = latest;
        }
    }
}
