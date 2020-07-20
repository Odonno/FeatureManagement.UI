using Microsoft.EntityFrameworkCore.Migrations;

namespace AspNetCore.FeatureManagement.UI.SqlServer.Storage.Migrations
{
    public partial class FeatureChoices : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "StringValue",
                schema: "FeatureManagement",
                table: "Feature",
                type: "NVARCHAR(MAX)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "DecimalFeatureChoice",
                schema: "FeatureManagement",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FeatureName = table.Column<string>(maxLength: 150, nullable: false),
                    Choice = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DecimalFeatureChoice", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DecimalFeatureChoice_Feature_FeatureName",
                        column: x => x.FeatureName,
                        principalSchema: "FeatureManagement",
                        principalTable: "Feature",
                        principalColumn: "Name",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "IntFeatureChoice",
                schema: "FeatureManagement",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FeatureName = table.Column<string>(maxLength: 150, nullable: false),
                    Choice = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IntFeatureChoice", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IntFeatureChoice_Feature_FeatureName",
                        column: x => x.FeatureName,
                        principalSchema: "FeatureManagement",
                        principalTable: "Feature",
                        principalColumn: "Name",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StringFeatureChoice",
                schema: "FeatureManagement",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FeatureName = table.Column<string>(maxLength: 150, nullable: false),
                    Choice = table.Column<string>(type: "NVARCHAR(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StringFeatureChoice", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StringFeatureChoice_Feature_FeatureName",
                        column: x => x.FeatureName,
                        principalSchema: "FeatureManagement",
                        principalTable: "Feature",
                        principalColumn: "Name",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DecimalFeatureChoice_FeatureName_Choice",
                schema: "FeatureManagement",
                table: "DecimalFeatureChoice",
                columns: new[] { "FeatureName", "Choice" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_IntFeatureChoice_FeatureName_Choice",
                schema: "FeatureManagement",
                table: "IntFeatureChoice",
                columns: new[] { "FeatureName", "Choice" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_StringFeatureChoice_FeatureName_Choice",
                schema: "FeatureManagement",
                table: "StringFeatureChoice",
                columns: new[] { "FeatureName", "Choice" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DecimalFeatureChoice",
                schema: "FeatureManagement");

            migrationBuilder.DropTable(
                name: "IntFeatureChoice",
                schema: "FeatureManagement");

            migrationBuilder.DropTable(
                name: "StringFeatureChoice",
                schema: "FeatureManagement");

            migrationBuilder.AlterColumn<string>(
                name: "StringValue",
                schema: "FeatureManagement",
                table: "Feature",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "NVARCHAR(MAX)",
                oldNullable: true);
        }
    }
}
