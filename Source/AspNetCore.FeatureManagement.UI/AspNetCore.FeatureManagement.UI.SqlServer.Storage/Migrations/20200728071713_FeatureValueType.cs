using Microsoft.EntityFrameworkCore.Migrations;

namespace AspNetCore.FeatureManagement.UI.SqlServer.Storage.Migrations
{
    public partial class FeatureValueType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ValueType",
                schema: "FeatureManagement",
                table: "Feature",
                maxLength: 10,
                nullable: false,
                defaultValue: "");

            migrationBuilder.Sql(@"
UPDATE [FeatureManagement].[Feature]
SET ValueType = Type
");

            migrationBuilder.DropColumn(
                name: "Type",
                schema: "FeatureManagement",
                table: "Feature");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Type",
                schema: "FeatureManagement",
                table: "Feature",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: false,
                defaultValue: "");

            migrationBuilder.Sql(@"
UPDATE [FeatureManagement].[Feature]
SET Type = ValueType
");
            migrationBuilder.DropColumn(
                name: "ValueType",
                schema: "FeatureManagement",
                table: "Feature");
        }
    }
}
