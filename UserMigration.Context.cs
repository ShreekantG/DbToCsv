﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ConsoleApp2
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class m5dbEntities : DbContext
    {
        public m5dbEntities()
            : base("name=m5dbEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
    
        public virtual ObjectResult<UspBossUserMigrationToUC_Result> UspBossUserMigrationToUC()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UspBossUserMigrationToUC_Result>("UspBossUserMigrationToUC");
        }
    }
}
