using Enum.Ext.Tests.Shared;
using System;
using System.Collections.Generic;
using System.Text;

namespace Enum.Ext.EFCore.Tests.DbContext.Entities
{
    public class SomeEntity
    {
        public int Id { get; set; }

        public Weekday Weekday { get; set; }
    }
}