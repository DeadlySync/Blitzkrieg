﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class dataEntities : DbContext
    {
        public dataEntities()
            : base("name=dataEntities")
        {
        }
    
    	public dataEntities(string connectionString) : base(connectionString){    }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<DnsConfig> DnsConfig { get; set; }
        public virtual DbSet<DnsLog> DnsLog { get; set; }
        public virtual DbSet<DomainList> DomainList { get; set; }
        public virtual DbSet<FeedFilters> FeedFilters { get; set; }
        public virtual DbSet<FeedItems> FeedItems { get; set; }
        public virtual DbSet<RssFeeds> RssFeeds { get; set; }
        public virtual DbSet<TorrentClient> TorrentClient { get; set; }
        public virtual DbSet<UserConfig> UserConfig { get; set; }
    }
}
