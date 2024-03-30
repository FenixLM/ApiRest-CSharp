using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BackendCurso.Migrations
{
    /// <inheritdoc />
    public partial class AlcoholInBerr : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Alcohol",
                table: "Berrs",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Alcohol",
                table: "Berrs");
        }
    }
}
