using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using prjSellor.Models;

namespace prjSellor.Dal
{
    public class DataLayer : DbContext
    {
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>().ToTable("users");
            modelBuilder.Entity<Schedule>().ToTable("schedule");
            modelBuilder.Entity<Breaks>().ToTable("breaks");
            modelBuilder.Entity<DefaultSchedule>().ToTable("defaultSchedule");
        }

        public DbSet<User> users { get; set; }
        public DbSet<Schedule> schedule { get; set; }
        public DbSet<Breaks> breaks { get; set; }
        public DbSet<DefaultSchedule> defaultSchedule { get; set; }

        public bool Add<E>(E entity) where E : class
        {
            Entry(entity).State = System.Data.Entity.EntityState.Added;
            return Save();
        }

        public bool Update<E>(E entity) where E : class
        {
            Entry(entity).State = System.Data.Entity.EntityState.Modified;
            return Save();
        }

        public bool Delete<E>(E entity) where E : class
        {
            Entry(entity).State = System.Data.Entity.EntityState.Deleted;
            return Save();
        }

        private bool Save()
        {
            return SaveChanges() > 0;
        }
    }
}