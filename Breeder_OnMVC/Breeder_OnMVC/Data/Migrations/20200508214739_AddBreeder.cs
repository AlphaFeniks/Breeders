using Microsoft.EntityFrameworkCore.Migrations;

namespace Breeder_OnMVC.Data.Migrations
{
    public partial class AddBreeder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Breeder",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false),
                    Author = table.Column<string>(nullable: true),
                    ParentVarieties = table.Column<string>(nullable: true),
                    Productivity = table.Column<string>(nullable: true),
                    Characteristic = table.Column<string>(nullable: true),
                    FrostResistance = table.Column<string>(nullable: true),
                    DiseaseResistance = table.Column<string>(nullable: true),
                    Funds = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Breeder", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Breeder");
        }
    }
}
