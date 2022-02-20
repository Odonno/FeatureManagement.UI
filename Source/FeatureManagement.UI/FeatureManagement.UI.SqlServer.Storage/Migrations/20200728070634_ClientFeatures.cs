using Microsoft.EntityFrameworkCore.Migrations;

namespace FeatureManagement.UI.SqlServer.Storage.Migrations
{
    public partial class ClientFeatures : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ClientFeatureData",
                schema: "FeatureManagement",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClientId = table.Column<string>(maxLength: 100, nullable: false),
                    FeatureId = table.Column<int>(nullable: false),
                    BooleanValue = table.Column<bool>(nullable: true),
                    IntValue = table.Column<int>(nullable: true),
                    DecimalValue = table.Column<decimal>(nullable: true),
                    StringValue = table.Column<string>(type: "NVARCHAR(MAX)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientFeatureData", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClientFeatureData_Feature_FeatureId",
                        column: x => x.FeatureId,
                        principalSchema: "FeatureManagement",
                        principalTable: "Feature",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ServerFeatureData",
                schema: "FeatureManagement",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FeatureId = table.Column<int>(nullable: false),
                    BooleanValue = table.Column<bool>(nullable: true),
                    IntValue = table.Column<int>(nullable: true),
                    DecimalValue = table.Column<decimal>(nullable: true),
                    StringValue = table.Column<string>(type: "NVARCHAR(MAX)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServerFeatureData", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ServerFeatureData_Feature_FeatureId",
                        column: x => x.FeatureId,
                        principalSchema: "FeatureManagement",
                        principalTable: "Feature",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ClientFeatureData_FeatureId_ClientId",
                schema: "FeatureManagement",
                table: "ClientFeatureData",
                columns: new[] { "FeatureId", "ClientId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ServerFeatureData_FeatureId",
                schema: "FeatureManagement",
                table: "ServerFeatureData",
                column: "FeatureId",
                unique: true);

            migrationBuilder.Sql(@"
INSERT INTO [FeatureManagement].[ServerFeatureData]
    SELECT 
        f.[Id] AS FeatureId,
        f.[BooleanValue] AS BooleanValue,
        f.[IntValue] AS IntValue,
        f.[DecimalValue] AS DecimalValue,
        f.[StringValue] AS StringValue
    FROM [FeatureManagement].[Feature] f
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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClientFeatureData",
                schema: "FeatureManagement");

            migrationBuilder.DropTable(
                name: "ServerFeatureData",
                schema: "FeatureManagement");

            migrationBuilder.AddColumn<bool>(
                name: "BooleanValue",
                schema: "FeatureManagement",
                table: "Feature",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "DecimalValue",
                schema: "FeatureManagement",
                table: "Feature",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "IntValue",
                schema: "FeatureManagement",
                table: "Feature",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StringValue",
                schema: "FeatureManagement",
                table: "Feature",
                type: "NVARCHAR(MAX)",
                nullable: true);
        }
    }
}
