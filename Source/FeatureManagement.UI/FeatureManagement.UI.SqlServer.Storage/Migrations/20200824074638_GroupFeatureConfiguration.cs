using Microsoft.EntityFrameworkCore.Migrations;

namespace FeatureManagement.UI.SqlServer.Storage.Migrations
{
    public partial class GroupFeatureConfiguration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ConfigurationType",
                schema: "FeatureManagement",
                table: "Feature",
                maxLength: 20,
                nullable: true);

            migrationBuilder.CreateTable(
                name: "GroupFeature",
                schema: "FeatureManagement",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FeatureId = table.Column<int>(nullable: false),
                    Group = table.Column<string>(maxLength: 100, nullable: false),
                    BooleanValue = table.Column<bool>(nullable: true),
                    IntValue = table.Column<int>(nullable: true),
                    DecimalValue = table.Column<decimal>(nullable: true),
                    StringValue = table.Column<string>(type: "NVARCHAR(MAX)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupFeature", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GroupFeature_Feature_FeatureId",
                        column: x => x.FeatureId,
                        principalSchema: "FeatureManagement",
                        principalTable: "Feature",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GroupFeature_FeatureId_Group",
                schema: "FeatureManagement",
                table: "GroupFeature",
                columns: new[] { "FeatureId", "Group" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GroupFeature",
                schema: "FeatureManagement");

            migrationBuilder.DropColumn(
                name: "ConfigurationType",
                schema: "FeatureManagement",
                table: "Feature");
        }
    }
}
