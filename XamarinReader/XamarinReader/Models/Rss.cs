#region License
//-----------------------------------------------------------------------
// <copyright>
//     Copyright matatabi-ux 2015.
// </copyright>
//-----------------------------------------------------------------------
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace XamarinReader.Models
{
    /// <summary>
    /// RSS 2.0 enitity root class
    /// </summary>
    [XmlRoot("rss")]
    public class Rss
    {
        /// <summary>
        /// RSS channel
        /// </summary>
        [XmlElement("channel")]
        public RssChannel Channel { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public Rss()
        {
            this.Channel = new RssChannel();
        }
    }

    /// <summary>
    /// RSS channel entity class
    /// </summary>
    public class RssChannel
    {
        /// <summary>
        /// Title
        /// </summary>
        [XmlElement("title")]
        public string Title { get; set; }

        /// <summary>
        /// Description
        /// </summary>
        [XmlElement("description")]
        public string Description { get; set; }

        /// <summary>
        /// Link uri
        /// </summary>
        [XmlElement("link")]
        public string Link { get; set; }

        /// <summary>
        /// Last update datetime
        /// </summary>
        [XmlIgnore]
        public DateTime LastBuildDate { get; set; }

        /// <summary>
        /// Serializable last update datetime
        /// </summary>
        [XmlElement("lastBuildDate")]
        public string LastBuildDateString
        {
            get { return this.LastBuildDate.ToString(); }
            set
            {
                DateTime date;
                if (DateTime.TryParse(value, out date))
                {
                    this.LastBuildDate = date;
                }
            }
        }

        /// <summary>
        /// Editor
        /// </summary>
        [XmlElement("managingEditor")]
        public string ManagingEditor { get; set; }

        /// <summary>
        /// Feed items
        /// </summary>
        [XmlElement("item")]
        public List<RssItem> Items { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public RssChannel()
        {
            this.Items = new List<RssItem>();
            this.LastBuildDate = DateTime.MinValue;
        }
    }

    /// <summary>
    /// RSS feed item entity class
    /// </summary>
    public class RssItem
    {
        /// <summary>
        /// Title
        /// </summary>
        [XmlElement("title")]
        public string Title { get; set; }

        /// <summary>
        /// Link uri
        /// </summary>
        [XmlElement("link")]
        public string Link { get; set; }

        /// <summary>
        /// Item ID
        /// </summary>
        [XmlElement("guid")]
        public string Guid { get; set; }

        /// <summary>
        /// Publish datetime
        /// </summary>
        [XmlIgnore]
        public DateTime PubDate { get; set; }

        /// <summary>
        /// Serializable publish datetime
        /// </summary>
        [XmlElement("pubDate")]
        public string PubDateString
        {
            get { return this.PubDate.ToString(); }
            set
            {
                DateTime date;
                if (DateTime.TryParse(value, out date))
                {
                    this.PubDate = date;
                }
            }
        }

        /// <summary>
        /// Description
        /// </summary>
        [XmlElement("description")]
        public string Description { get; set; }

        /// <summary>
        /// Categories
        /// </summary>
        [XmlElement("category")]
        public List<string> Categories { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public RssItem()
        {
            this.Categories = new List<string>();
        }
    }
}
