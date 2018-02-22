//AIzaSyDDSBEDtC_VJ1er-LYdDMpbAxwC3fXgyAA

using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using prjSellor.Models;

namespace prjSellor.Dal
{
    public class SaltDal : DbContext
    {
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Salt>().ToTable("saltTable");
        }
        public DbSet<Salt> saltData { get; set; }
    }
}