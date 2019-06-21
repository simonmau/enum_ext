using Enum.Ext.EFCore.Tests;
using Enum.Ext.EFCore.Tests.DbContext;
using NUnit.Framework;
using System.Linq;

namespace Tests
{
    public class Tests
    {
        [Test]
        public void Test_SaveData()
        {
            using (var db = new TestDbContext())
            {
                db.Database.EnsureCreated();

                db.SomeEntities.Add(new Enum.Ext.EFCore.Tests.DbContext.Entities.SomeEntity
                {
                    Weekday = Weekday.Thursday,
                });

                db.SaveChanges();

                var entities = db.SomeEntities.ToList();

                Assert.AreEqual(1, entities.Count);
                Assert.AreEqual(Weekday.Thursday, entities.Single().Weekday);
            }
        }
    }
}