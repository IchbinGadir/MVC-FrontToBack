using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProniaA.Migrations
{
    /// <inheritdoc />
    public partial class asdfgh : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "IsDelited",
                table: "Slides",
                type: "bit",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<bool>(
                name: "IsDelited",
                table: "Products",
                type: "bit",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<bool>(
                name: "IsDelited",
                table: "ProductImages",
                type: "bit",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<bool>(
                name: "IsDelited",
                table: "Categories",
                type: "bit",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<bool>(
                name: "IsDelited",
                table: "Blogs",
                type: "bit",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "IsDelited",
                table: "Slides",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<DateTime>(
                name: "IsDelited",
                table: "Products",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<DateTime>(
                name: "IsDelited",
                table: "ProductImages",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<DateTime>(
                name: "IsDelited",
                table: "Categories",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<DateTime>(
                name: "IsDelited",
                table: "Blogs",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit");
        }
    }
}
