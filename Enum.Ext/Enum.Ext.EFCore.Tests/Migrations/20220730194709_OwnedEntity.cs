using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Enum.Ext.EFCore.Tests.Migrations
{
    public partial class OwnedEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EntitiesWithOwnedProperties",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false),
                    OwnedStuff_Weekday = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EntitiesWithOwnedProperties", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EntitiesWithOwnedProperties");
        }
    }
}
