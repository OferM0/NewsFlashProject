using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace server.Dal.Migrations
{
    /// <inheritdoc />
    public partial class UpdatenewsItem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ItemId",
                table: "NewsItems",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ItemId",
                table: "NewsItems");
        }
    }
}
