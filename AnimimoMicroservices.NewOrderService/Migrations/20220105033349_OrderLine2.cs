using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AnimimoMicroservices.NewOrderService.Migrations
{
    public partial class OrderLine2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OrderLine",
                columns: table => new
                {
                    OrderID = table.Column<int>(type: "int", nullable: false),
                    Identifier = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Customer = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderLine");
        }
    }
}
