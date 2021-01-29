using Domain.ValueObject;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace Infrastructure.EfDataAccess
{
    public class EfDbContext : DbContext
    {
        public EfDbContext(DbContextOptions<EfDbContext> options) : base(options)
        {

        }
        //Dbset<Po>
        public DbSet<PersistenceObject.Order> Order { get; set; }
        public DbSet<PersistenceObject.OrderItem> OrderItem { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //����Guid����������չ
            modelBuilder.HasPostgresExtension("uuid-ossp");
            modelBuilder.Entity<PersistenceObject.OrderItem>().Property(x => x.GoodsSnapshot).HasConversion(x => JsonSerializer.Serialize(x, null), x => JsonSerializer.Deserialize<OrderGoodsSnapshot>(x, null));
            modelBuilder.Entity<OrderGoodsSnapshot>(builder => builder.HasNoKey());
            base.OnModelCreating(modelBuilder);
        }
    }
}
