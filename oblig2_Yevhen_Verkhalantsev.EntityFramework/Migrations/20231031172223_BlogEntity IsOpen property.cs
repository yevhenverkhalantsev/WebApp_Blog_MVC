using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace oblig2_Yevhen_Verkhalantsev.EntityFramework.Migrations
{
    /// <inheritdoc />
    public partial class BlogEntityIsOpenproperty : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsOpen",
                table: "Blogs",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsOpen",
                table: "Blogs");
        }
    }
}
