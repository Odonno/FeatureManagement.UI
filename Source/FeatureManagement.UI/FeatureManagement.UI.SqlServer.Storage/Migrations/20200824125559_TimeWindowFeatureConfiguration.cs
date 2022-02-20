using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FeatureManagement.UI.SqlServer.Storage.Migrations
{
    public partial class TimeWindowFeatureConfiguration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TimeWindowFeature",
                schema: "FeatureManagement",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FeatureId = table.Column<int>(nullable: false),
                    StartDate = table.Column<DateTime>(nullable: true),
                    EndDate = table.Column<DateTime>(nullable: true),
                    BooleanValue = table.Column<bool>(nullable: true),
                    IntValue = table.Column<int>(nullable: true),
                    DecimalValue = table.Column<decimal>(nullable: true),
                    StringValue = table.Column<string>(type: "NVARCHAR(MAX)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TimeWindowFeature", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TimeWindowFeature_Feature_FeatureId",
                        column: x => x.FeatureId,
                        principalSchema: "FeatureManagement",
                        principalTable: "Feature",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TimeWindowFeature_FeatureId_StartDate_EndDate",
                schema: "FeatureManagement",
                table: "TimeWindowFeature",
                columns: new[] { "FeatureId", "StartDate", "EndDate" },
                unique: true,
                filter: "[StartDate] IS NOT NULL AND [EndDate] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TimeWindowFeature",
                schema: "FeatureManagement");
        }
    }
}
