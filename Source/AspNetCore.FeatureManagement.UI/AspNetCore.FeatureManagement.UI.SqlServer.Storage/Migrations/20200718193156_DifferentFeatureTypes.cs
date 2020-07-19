using Microsoft.EntityFrameworkCore.Migrations;

namespace AspNetCore.FeatureManagement.UI.SqlServer.Storage.Migrations
{
    public partial class DifferentFeatureTypes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "BooleanValue",
                schema: "FeatureManagement",
                table: "Feature",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "DecimalValue",
                schema: "FeatureManagement",
                table: "Feature",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "IntValue",
                schema: "FeatureManagement",
                table: "Feature",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StringValue",
                schema: "FeatureManagement",
                table: "Feature",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Type",
                schema: "FeatureManagement",
                table: "Feature",
                maxLength: 10,
                nullable: false,
                defaultValue: "");

            migrationBuilder.Sql(@"
UPDATE [FeatureManagement].[Feature]
SET 
    BooleanValue = Enabled,
    Type = 'BOOLEAN'
");

            migrationBuilder.DropColumn(
                name: "Enabled",
                schema: "FeatureManagement",
                table: "Feature");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Enabled",
                schema: "FeatureManagement",
                table: "Feature",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.Sql(@"
UPDATE [FeatureManagement].[Feature]
SET Enabled = BooleanValue
WHERE Type = 'BOOLEAN'
");

            migrationBuilder.DropColumn(
                name: "BooleanValue",
                schema: "FeatureManagement",
                table: "Feature");

            migrationBuilder.DropColumn(
                name: "DecimalValue",
                schema: "FeatureManagement",
                table: "Feature");

            migrationBuilder.DropColumn(
                name: "IntValue",
                schema: "FeatureManagement",
                table: "Feature");

            migrationBuilder.DropColumn(
                name: "StringValue",
                schema: "FeatureManagement",
                table: "Feature");

            migrationBuilder.DropColumn(
                name: "Type",
                schema: "FeatureManagement",
                table: "Feature");
        }
    }
}
