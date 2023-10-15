﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MVC.Budget.JsPeanut.Migrations
{
    /// <inheritdoc />
    public partial class AddedCurrencyToCategories : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CurrencyNativeSymbol",
                table: "Categories",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CurrencyNativeSymbol",
                table: "Categories");
        }
    }
}
