using Enum.Ext.Tests.Shared;

using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using NUnit.Framework;
using Shouldly;
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
            var allIds = Weekday.List.Select(x => x.Id);
            var expectedFirstId = allIds.FirstOrDefault();

            var expectedOpenApiEnum = allIds
                        .Select(x => (IOpenApiAny)new OpenApiString(x.ToString()))
                        .ToList();

            // Act
            swaggerGenOptions.ConfigureEnumExt(typeof(Weekday).Assembly);

            // Assert
            swaggerGenOptions.SchemaGeneratorOptions
                .CustomTypeMappings.TryGetValue(typeof(Weekday), out var mapping)
                .ShouldBeTrue();

            var schema = mapping!.Invoke();

            schema.ShouldNotBeNull();
            schema.ShouldBeEquivalentTo(new OpenApiSchema
            {
                Type = "integer",
                Example = new OpenApiLong(expectedFirstId),
                Enum = expectedOpenApiEnum,
            });
        }
    }
}