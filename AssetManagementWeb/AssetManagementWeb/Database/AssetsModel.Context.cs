﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AssetManagementWeb.Database
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class PointSQLSrv1Entities : DbContext
    {
        public PointSQLSrv1Entities()
            : base("name=PointSQLSrv1Entities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<AssetLocation> AssetLocation { get; set; }
        public virtual DbSet<AssetLocations> AssetLocations { get; set; }
        public virtual DbSet<Assets> Assets { get; set; }
    }
}
