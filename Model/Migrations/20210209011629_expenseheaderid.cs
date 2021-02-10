﻿using Microsoft.EntityFrameworkCore.Migrations;

namespace Model.Migrations
{
    public partial class expenseheaderid : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ExpenseHeaderId",
                table: "ExpenseLines",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ExpenseLines_ExpenseHeaderId",
                table: "ExpenseLines",
                column: "ExpenseHeaderId");

            migrationBuilder.AddForeignKey(
                name: "FK_ExpenseLines_ExpenseHeaders_ExpenseHeaderId",
                table: "ExpenseLines",
                column: "ExpenseHeaderId",
                principalTable: "ExpenseHeaders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExpenseLines_ExpenseHeaders_ExpenseHeaderId",
                table: "ExpenseLines");

            migrationBuilder.DropIndex(
                name: "IX_ExpenseLines_ExpenseHeaderId",
                table: "ExpenseLines");

            migrationBuilder.DropColumn(
                name: "ExpenseHeaderId",
                table: "ExpenseLines");
        }
    }
}
