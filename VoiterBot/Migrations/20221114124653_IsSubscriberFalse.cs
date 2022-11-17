using Microsoft.EntityFrameworkCore.Migrations;

namespace VoterBot.Migrations
{
    public partial class IsSubscriberFalse : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsSubscriber",
                table: "Users",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsSubscriber",
                table: "Users");
        }
    }
}
