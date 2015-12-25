using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Connection
{
    public class Post
    {
        public string Title { get; set; }
        public string Link { get; set; }
        public string Description { get; set; }
        public string PublishedDate { get; set; }
        public string Magnet { get; set; }
        public string FromFeed { get; set; }
        public bool Downloaded { get; set; }
    }
}
