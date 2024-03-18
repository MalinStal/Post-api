using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Post_api.Migrations
{
    /// <inheritdoc />
    public partial class uppdateinContextForFiles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FileModel_Posts_PostId",
                table: "FileModel");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FileModel",
                table: "FileModel");

            migrationBuilder.RenameTable(
                name: "FileModel",
                newName: "FileModels");

            migrationBuilder.RenameIndex(
                name: "IX_FileModel_PostId",
                table: "FileModels",
                newName: "IX_FileModels_PostId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FileModels",
                table: "FileModels",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FileModels_Posts_PostId",
                table: "FileModels",
                column: "PostId",
                principalTable: "Posts",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FileModels_Posts_PostId",
                table: "FileModels");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FileModels",
                table: "FileModels");

            migrationBuilder.RenameTable(
                name: "FileModels",
                newName: "FileModel");

            migrationBuilder.RenameIndex(
                name: "IX_FileModels_PostId",
                table: "FileModel",
                newName: "IX_FileModel_PostId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FileModel",
                table: "FileModel",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FileModel_Posts_PostId",
                table: "FileModel",
                column: "PostId",
                principalTable: "Posts",
                principalColumn: "Id");
        }
    }
}
