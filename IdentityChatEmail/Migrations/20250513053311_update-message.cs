using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IdentityChatEmail.Migrations
{
    /// <inheritdoc />
    public partial class updatemessage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RecieverFullName",
                table: "Messages",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SenderFullName",
                table: "Messages",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RecieverFullName",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "SenderFullName",
                table: "Messages");
        }
    }
}
