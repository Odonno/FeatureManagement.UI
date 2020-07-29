using Microsoft.EntityFrameworkCore.Migrations;

namespace AspNetCore.FeatureManagement.UI.SqlServer.Storage.Migrations
{
    public partial class UiPrefixSuffix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UiPrefix",
                schema: "FeatureManagement",
                table: "Feature",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UiSuffix",
                schema: "FeatureManagement",
                table: "Feature",
                maxLength: 20,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UiPrefix",
                schema: "FeatureManagement",
                table: "Feature");

            migrationBuilder.DropColumn(
                name: "UiSuffix",
                schema: "FeatureManagement",
                table: "Feature");
        }
    }
}
