using Enum.Ext.Tests.Shared;
using FluentAssertions;
using Microsoft.OpenApi.Models;
using NUnit.Framework;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Enum.Ext.Swashbuckle.AspNetCore.Tests
{
    [TestFixture]
    public class ExtensionsTests
    {
        [Test]
        public void ConfigureEnumExt_Should_Add_TypeMappers()
        {
            // Arrange
            var swaggerGenOptions = new SwaggerGenOptions();

            // Act
            swaggerGenOptions.ConfigureEnumExt(typeof(Weekday).Assembly);

            // Assert
            swaggerGenOptions.SchemaGeneratorOptions
                .CustomTypeMappings.TryGetValue(typeof(Weekday), out var mapping)
                .Should().BeTrue();

            var schema = mapping!.Invoke();

            schema.Should().NotBeNull();
            schema.Should().BeEquivalentTo(new OpenApiSchema
            {
                Type = "integer",
            });
        }
    }
}