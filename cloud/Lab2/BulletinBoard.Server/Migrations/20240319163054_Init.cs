using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BulletinBoard.Server.Migrations;

/// <inheritdoc />
public partial class Init : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "Ads",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "TEXT", nullable: false),
                Message = table.Column<string>(type: "TEXT", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Ads", x => x.Id);
            });
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "Ads");
    }
}
