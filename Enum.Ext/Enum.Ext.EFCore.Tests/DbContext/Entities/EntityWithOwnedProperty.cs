using Enum.Ext.Tests.Shared;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Enum.Ext.EFCore.Tests.DbContext.Entities
{
    public class EntityWithOwnedProperty
    {
        public int Id { get; set; }

        [Required]
        public OwnedStuff OwnedStuff { get; set; }
    }

    [Owned]
    public class OwnedStuff
    {
        public Weekday Weekday { get; set; }
    }
}