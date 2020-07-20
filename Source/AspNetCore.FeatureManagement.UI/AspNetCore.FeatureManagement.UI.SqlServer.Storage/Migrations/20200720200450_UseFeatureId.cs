using Microsoft.EntityFrameworkCore.Migrations;

namespace AspNetCore.FeatureManagement.UI.SqlServer.Storage.Migrations
{
    public partial class UseFeatureId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DecimalFeatureChoice_Feature_FeatureName",
                schema: "FeatureManagement",
                table: "DecimalFeatureChoice");

            migrationBuilder.DropForeignKey(
                name: "FK_IntFeatureChoice_Feature_FeatureName",
                schema: "FeatureManagement",
                table: "IntFeatureChoice");

            migrationBuilder.DropForeignKey(
                name: "FK_StringFeatureChoice_Feature_FeatureName",
                schema: "FeatureManagement",
                table: "StringFeatureChoice");

            migrationBuilder.DropIndex(
                name: "IX_StringFeatureChoice_FeatureName_Choice",
                schema: "FeatureManagement",
                table: "StringFeatureChoice");

            migrationBuilder.DropIndex(
                name: "IX_IntFeatureChoice_FeatureName_Choice",
                schema: "FeatureManagement",
                table: "IntFeatureChoice");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Feature",
                schema: "FeatureManagement",
                table: "Feature");

            migrationBuilder.DropIndex(
                name: "IX_DecimalFeatureChoice_FeatureName_Choice",
                schema: "FeatureManagement",
                table: "DecimalFeatureChoice");

            migrationBuilder.DropColumn(
                name: "FeatureName",
                schema: "FeatureManagement",
                table: "StringFeatureChoice");

            migrationBuilder.DropColumn(
                name: "FeatureName",
                schema: "FeatureManagement",
                table: "IntFeatureChoice");

            migrationBuilder.DropColumn(
                name: "FeatureName",
                schema: "FeatureManagement",
                table: "DecimalFeatureChoice");

            migrationBuilder.AddColumn<int>(
                name: "FeatureId",
                schema: "FeatureManagement",
                table: "StringFeatureChoice",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "FeatureId",
                schema: "FeatureManagement",
                table: "IntFeatureChoice",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Id",
                schema: "FeatureManagement",
                table: "Feature",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "FeatureId",
                schema: "FeatureManagement",
                table: "DecimalFeatureChoice",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Feature",
                schema: "FeatureManagement",
                table: "Feature",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_StringFeatureChoice_FeatureId_Choice",
                schema: "FeatureManagement",
                table: "StringFeatureChoice",
                columns: new[] { "FeatureId", "Choice" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_IntFeatureChoice_FeatureId_Choice",
                schema: "FeatureManagement",
                table: "IntFeatureChoice",
                columns: new[] { "FeatureId", "Choice" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Feature_Name",
                schema: "FeatureManagement",
                table: "Feature",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DecimalFeatureChoice_FeatureId_Choice",
                schema: "FeatureManagement",
                table: "DecimalFeatureChoice",
                columns: new[] { "FeatureId", "Choice" },
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_DecimalFeatureChoice_Feature_FeatureId",
                schema: "FeatureManagement",
                table: "DecimalFeatureChoice",
                column: "FeatureId",
                principalSchema: "FeatureManagement",
                principalTable: "Feature",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_IntFeatureChoice_Feature_FeatureId",
                schema: "FeatureManagement",
                table: "IntFeatureChoice",
                column: "FeatureId",
                principalSchema: "FeatureManagement",
                principalTable: "Feature",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StringFeatureChoice_Feature_FeatureId",
                schema: "FeatureManagement",
                table: "StringFeatureChoice",
                column: "FeatureId",
                principalSchema: "FeatureManagement",
                principalTable: "Feature",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DecimalFeatureChoice_Feature_FeatureId",
                schema: "FeatureManagement",
                table: "DecimalFeatureChoice");

            migrationBuilder.DropForeignKey(
                name: "FK_IntFeatureChoice_Feature_FeatureId",
                schema: "FeatureManagement",
                table: "IntFeatureChoice");

            migrationBuilder.DropForeignKey(
                name: "FK_StringFeatureChoice_Feature_FeatureId",
                schema: "FeatureManagement",
                table: "StringFeatureChoice");

            migrationBuilder.DropIndex(
                name: "IX_StringFeatureChoice_FeatureId_Choice",
                schema: "FeatureManagement",
                table: "StringFeatureChoice");

            migrationBuilder.DropIndex(
                name: "IX_IntFeatureChoice_FeatureId_Choice",
                schema: "FeatureManagement",
                table: "IntFeatureChoice");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Feature",
                schema: "FeatureManagement",
                table: "Feature");

            migrationBuilder.DropIndex(
                name: "IX_Feature_Name",
                schema: "FeatureManagement",
                table: "Feature");

            migrationBuilder.DropIndex(
                name: "IX_DecimalFeatureChoice_FeatureId_Choice",
                schema: "FeatureManagement",
                table: "DecimalFeatureChoice");

            migrationBuilder.DropColumn(
                name: "FeatureId",
                schema: "FeatureManagement",
                table: "StringFeatureChoice");

            migrationBuilder.DropColumn(
                name: "FeatureId",
                schema: "FeatureManagement",
                table: "IntFeatureChoice");

            migrationBuilder.DropColumn(
                name: "Id",
                schema: "FeatureManagement",
                table: "Feature");

            migrationBuilder.DropColumn(
                name: "FeatureId",
                schema: "FeatureManagement",
                table: "DecimalFeatureChoice");

            migrationBuilder.AddColumn<string>(
                name: "FeatureName",
                schema: "FeatureManagement",
                table: "StringFeatureChoice",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FeatureName",
                schema: "FeatureManagement",
                table: "IntFeatureChoice",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FeatureName",
                schema: "FeatureManagement",
                table: "DecimalFeatureChoice",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Feature",
                schema: "FeatureManagement",
                table: "Feature",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_StringFeatureChoice_FeatureName_Choice",
                schema: "FeatureManagement",
                table: "StringFeatureChoice",
                columns: new[] { "FeatureName", "Choice" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_IntFeatureChoice_FeatureName_Choice",
                schema: "FeatureManagement",
                table: "IntFeatureChoice",
                columns: new[] { "FeatureName", "Choice" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DecimalFeatureChoice_FeatureName_Choice",
                schema: "FeatureManagement",
                table: "DecimalFeatureChoice",
                columns: new[] { "FeatureName", "Choice" },
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_DecimalFeatureChoice_Feature_FeatureName",
                schema: "FeatureManagement",
                table: "DecimalFeatureChoice",
                column: "FeatureName",
                principalSchema: "FeatureManagement",
                principalTable: "Feature",
                principalColumn: "Name",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_IntFeatureChoice_Feature_FeatureName",
                schema: "FeatureManagement",
                table: "IntFeatureChoice",
                column: "FeatureName",
                principalSchema: "FeatureManagement",
                principalTable: "Feature",
                principalColumn: "Name",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StringFeatureChoice_Feature_FeatureName",
                schema: "FeatureManagement",
                table: "StringFeatureChoice",
                column: "FeatureName",
                principalSchema: "FeatureManagement",
                principalTable: "Feature",
                principalColumn: "Name",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
