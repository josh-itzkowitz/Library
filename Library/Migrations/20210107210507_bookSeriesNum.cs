using Microsoft.EntityFrameworkCore.Migrations;

namespace Library.Migrations
{
    public partial class bookSeriesNum : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SeriesNum",
                table: "Books",
                type: "int",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SeriesNum",
                table: "Books");
        }
    }
}
