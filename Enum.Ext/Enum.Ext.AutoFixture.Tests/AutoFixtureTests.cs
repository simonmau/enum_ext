using AutoFixture;
using Enum.Ext.Tests.Shared;
using FluentAssertions;
using NUnit.Framework;

namespace Enum.Ext.AutoFixture.Tests
{
    public class Tests
    {
        [Test]
        public void AutoFixture_ShouldReturnValidEnum()
        {
            var fixture = new Fixture();
            fixture.WithEnumExt();

            var weekday = fixture.Create<Weekday>();

            Weekday.List.Should().Contain(weekday);
        }
    }
}