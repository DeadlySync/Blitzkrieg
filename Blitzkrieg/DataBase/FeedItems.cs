//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Blitzkrieg.DataBase
{
    using System;
    using System.Collections.Generic;
    
    public partial class FeedItems
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Link { get; set; }
        public string PublishDate { get; set; }
        public string Magnet { get; set; }
        public string FromFeed { get; set; }
        public Nullable<bool> IsDownloaded { get; set; }
        public System.DateTime DateAdd { get; set; }
        public Nullable<System.DateTime> DateUpdate { get; set; }
    }
}
