using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Post_api.Migrations
{
    /// <inheritdoc />
    public partial class changeComments : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "id",
                table: "Comments",
                newName: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Comments",
                newName: "id");
        }
    }
}
