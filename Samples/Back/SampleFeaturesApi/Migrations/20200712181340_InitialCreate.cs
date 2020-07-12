using Microsoft.EntityFrameworkCore.Migrations;

namespace SampleFeaturesApi.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "FeatureManagement");

            migrationBuilder.CreateTable(
                name: "Feature",
                schema: "FeatureManagement",
                columns: table => new
                {
                    Name = table.Column<string>(maxLength: 150, nullable: false),
                    Enabled = table.Column<bool>(nullable: false),
                    Description = table.Column<string>(maxLength: 1000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Feature", x => x.Name);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Feature",
                schema: "FeatureManagement");
        }
    }
}
