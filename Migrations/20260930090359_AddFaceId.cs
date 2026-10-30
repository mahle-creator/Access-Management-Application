using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Access_Management_Application.Migrations
{
    /// <inheritdoc />
    public partial class AddFaceId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FaceId",
                table: "Users",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FaceId",
                table: "Users");
        }
    }
}
