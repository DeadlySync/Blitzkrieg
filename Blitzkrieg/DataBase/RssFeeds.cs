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
    
    public partial class RssFeeds
    {
        public long Id { get; set; }
        public string FeedUrl { get; set; }
        public string FeedAlias { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<long> FeedPriority { get; set; }
        public string FeedIcon { get; set; }
        public System.DateTime DateAdd { get; set; }
        public Nullable<System.DateTime> DateUpdate { get; set; }
    }
}
