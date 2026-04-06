using Enum.Ext.Tests.Shared;
using NUnit.Framework;
using Shouldly;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Text.Json.Nodes;

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
            var allIds = Weekday.List.Select(x => (long)x.Id).ToList();
            var expectedFirstId = allIds.FirstOrDefault();

            var expectedOpenApiEnum = allIds
                    .Select(x => (JsonNode)JsonValue.Create(x))
                    .ToList();

            // Act
            swaggerGenOptions.ConfigureEnumExt(typeof(Weekday).Assembly);

            // Assert
            swaggerGenOptions.SchemaGeneratorOptions
                .CustomTypeMappings.TryGetValue(typeof(Weekday), out var mapping)
                .ShouldBeTrue();

            var schema = mapping!.Invoke();

            schema.ShouldNotBeNull();

            schema.Example.ShouldNotBeNull();
            schema.Example.GetValue<long>().ShouldBe(expectedFirstId);

            schema.Enum.ShouldNotBeNull();
            schema.Enum.Count.ShouldBe(allIds.Count);

            var actualEnumValues = schema.Enum.Select(x => x.GetValue<long>()).ToList();
            actualEnumValues.ShouldBe(allIds);
        }
    }
}