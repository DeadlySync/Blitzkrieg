using System;
using System.ComponentModel;
using System.Globalization;

namespace Blitzkrieg.Connection
{
    public class Post
    {
        private string publishedDate;
        [DisplayName("Name")]
        public string Title { get; set; }

        [Browsable(false)]
        public string Link { get; set; }

        [DisplayName("Published Date")]
        public string PublishedDate
        {
            get
            {
                if (publishedDate != string.Empty)
                {
                    
                    return Convert.ToDateTime(publishedDate).ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);
                }
                else
                    return string.Empty;
            }

            set
            {
                publishedDate = value;
            }
        }

        [Browsable(false)]
        public string Magnet { get; set; }

        [Browsable(false)]
        public string Done { get; set; }

        [DisplayName("Feed")]
        public string FromFeed { get; set; }

        [Browsable(false)]
        public bool Downloaded { get; set; }
    }
}
