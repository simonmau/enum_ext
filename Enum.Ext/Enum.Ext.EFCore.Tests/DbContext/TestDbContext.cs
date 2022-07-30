﻿using Enum.Ext.EFCore.Tests.DbContext.Entities;
using Microsoft.EntityFrameworkCore;

namespace Enum.Ext.EFCore.Tests.DbContext
{
    public class TestDbContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public TestDbContext(DbContextOptions<TestDbContext> options) : base(options)
        { }

        public TestDbContext()
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
                optionsBuilder.UseSqlite("Data Source=:memory:;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ConfigureEnumExt();
        }

        public DbSet<SomeEntity> SomeEntities { get; set; }

        public DbSet<EntityWithOwnedProperty> EntitiesWithOwnedProperties { get; set; }
    }
}