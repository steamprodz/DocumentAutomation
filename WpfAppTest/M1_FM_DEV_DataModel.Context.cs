﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WpfAppTest
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class M1_FM_DEV_DataEntities : DbContext
    {
        public M1_FM_DEV_DataEntities()
            : base("name=M1_FM_DEV_DataEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Employees> Employees { get; set; }
        public virtual DbSet<JobAssemblies> JobAssemblies { get; set; }
        public virtual DbSet<JobMaterials> JobMaterials { get; set; }
        public virtual DbSet<JobOperations> JobOperations { get; set; }
        public virtual DbSet<Jobs> Jobs { get; set; }
        public virtual DbSet<OrganizationContacts> OrganizationContacts { get; set; }
        public virtual DbSet<OrganizationLocations> OrganizationLocations { get; set; }
        public virtual DbSet<Organizations> Organizations { get; set; }
        public virtual DbSet<PartClasses> PartClasses { get; set; }
        public virtual DbSet<PartRevisions> PartRevisions { get; set; }
        public virtual DbSet<Parts> Parts { get; set; }
        public virtual DbSet<SalesOrderJobLinks> SalesOrderJobLinks { get; set; }
        public virtual DbSet<SalesOrderLines> SalesOrderLines { get; set; }
        public virtual DbSet<SalesOrders> SalesOrders { get; set; }
        public virtual DbSet<SalesOrderSalesPeople> SalesOrderSalesPeople { get; set; }
    }
}
