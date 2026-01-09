using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BTKECommerce_Domain.Migrations
{
    /// <inheritdoc />
    public partial class addedCreatedByFieldToCategoryClass : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Categories",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Categories");
        }
    }
}
