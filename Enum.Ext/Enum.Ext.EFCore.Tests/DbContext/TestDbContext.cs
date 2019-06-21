﻿using Enum.Ext.EFCore.Tests.DbContext.Entities;
using Microsoft.EntityFrameworkCore;

namespace Enum.Ext.EFCore.Tests.DbContext
{
    public class TestDbContext : Microsoft.EntityFrameworkCore.DbContext
    {
        static TestDbContext()
        {
            Enum.Ext.Initialize.InitStaticFields<Weekday>();
        }

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
            var converter = new TypeSafeEnumConverter<Weekday, int>();

            modelBuilder.Entity<SomeEntity>()
                .Property(e => e.Weekday)
                .HasConversion(converter);
        }

        public DbSet<SomeEntity> SomeEntities { get; set; }
    }
}