using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.EfDataAccess
{
    public class EfDbContext : DbContext
    {
        public EfDbContext(DbContextOptions<EfDbContext> options) : base(options)
        {

        }
        public DbSet<PersistenceObject.GoodsCategory> GoodsCategory { get; set; }
        public DbSet<PersistenceObject.Goods> Goods { get; set; }
        public DbSet<PersistenceObject.LimitedTimeActivitie> LimitedTimeActivitie { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //����Guid����������չ
            modelBuilder.HasPostgresExtension("uuid-ossp");
            base.OnModelCreating(modelBuilder);
        }
    }
}
