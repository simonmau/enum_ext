using Enum.Ext.EFCore.Tests.DbContext;
using Enum.Ext.Tests.Shared;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using Shouldly;
using System.Linq;

namespace Tests
{
    public class Tests
    {
        [Test]
        public void Test_SaveData()
        {
            // In-memory database only exists while the connection is open
            using (var connection = new SqliteConnection("DataSource=:memory:"))
            {
                connection.Open();

                var options = new DbContextOptionsBuilder<TestDbContext>()
                    .UseSqlite(connection)
                    .Options;

                using (var db = new TestDbContext(options))
                {
                    db.Database.EnsureCreated();

                    db.SomeEntities.Add(new Enum.Ext.EFCore.Tests.DbContext.Entities.SomeEntity
                    {
                        Weekday = Weekday.Thursday,
                    });

                    db.SaveChanges();
                }

                using (var db = new TestDbContext(options))
                {
                    var entities = db.SomeEntities.ToList();

                    entities.Count.ShouldBe(1);
                    entities.Single().Weekday.ShouldBe(Weekday.Thursday);
                }
            }
        }

        [Test]
        public void Test_SaveData_WithOwnedEntity()
        {
            // In-memory database only exists while the connection is open
            using (var connection = new SqliteConnection("DataSource=:memory:"))
            {
                connection.Open();

                var options = new DbContextOptionsBuilder<TestDbContext>()
                    .UseSqlite(connection)
                    .Options;

                using (var db = new TestDbContext(options))
                {
                    db.Database.EnsureCreated();

                    db.EntitiesWithOwnedProperties.Add(new Enum.Ext.EFCore.Tests.DbContext.Entities.EntityWithOwnedProperty()
                    {
                        OwnedStuff = new Enum.Ext.EFCore.Tests.DbContext.Entities.OwnedStuff()
                        {
                            Weekday = Weekday.Thursday
                        }
                    });

                    db.SaveChanges();
                }

                using (var db = new TestDbContext(options))
                {
                    var entities = db.EntitiesWithOwnedProperties.ToList();

                    entities.Count.ShouldBe(1);
                    entities.Single().OwnedStuff.ShouldNotBeNull();
                    entities.Single().OwnedStuff!.Weekday.ShouldBe(Weekday.Thursday);
                }
            }
        }
    }
}