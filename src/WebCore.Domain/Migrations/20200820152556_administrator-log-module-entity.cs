using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebCore.Domain.Migrations
{
    public partial class administratorlogmoduleentity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CreatedById",
                table: "Logs",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Logs",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsVisible",
                table: "Logs",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "LastModifiedById",
                table: "Logs",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModifyDate",
                table: "Logs",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedById",
                table: "AdministratorActions",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "AdministratorActions",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsVisible",
                table: "AdministratorActions",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "LastModifiedById",
                table: "AdministratorActions",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModifyDate",
                table: "AdministratorActions",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Logs_CreatedById",
                table: "Logs",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Logs_LastModifiedById",
                table: "Logs",
                column: "LastModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_AdministratorActions_CreatedById",
                table: "AdministratorActions",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_AdministratorActions_LastModifiedById",
                table: "AdministratorActions",
                column: "LastModifiedById");

            migrationBuilder.AddForeignKey(
                name: "FK_AdministratorActions_AspNetUsers_CreatedById",
                table: "AdministratorActions",
                column: "CreatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AdministratorActions_AspNetUsers_LastModifiedById",
                table: "AdministratorActions",
                column: "LastModifiedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Logs_AspNetUsers_CreatedById",
                table: "Logs",
                column: "CreatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Logs_AspNetUsers_LastModifiedById",
                table: "Logs",
                column: "LastModifiedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AdministratorActions_AspNetUsers_CreatedById",
                table: "AdministratorActions");

            migrationBuilder.DropForeignKey(
                name: "FK_AdministratorActions_AspNetUsers_LastModifiedById",
                table: "AdministratorActions");

            migrationBuilder.DropForeignKey(
                name: "FK_Logs_AspNetUsers_CreatedById",
                table: "Logs");

            migrationBuilder.DropForeignKey(
                name: "FK_Logs_AspNetUsers_LastModifiedById",
                table: "Logs");

            migrationBuilder.DropIndex(
                name: "IX_Logs_CreatedById",
                table: "Logs");

            migrationBuilder.DropIndex(
                name: "IX_Logs_LastModifiedById",
                table: "Logs");

            migrationBuilder.DropIndex(
                name: "IX_AdministratorActions_CreatedById",
                table: "AdministratorActions");

            migrationBuilder.DropIndex(
                name: "IX_AdministratorActions_LastModifiedById",
                table: "AdministratorActions");

            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "Logs");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Logs");

            migrationBuilder.DropColumn(
                name: "IsVisible",
                table: "Logs");

            migrationBuilder.DropColumn(
                name: "LastModifiedById",
                table: "Logs");

            migrationBuilder.DropColumn(
                name: "LastModifyDate",
                table: "Logs");

            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "AdministratorActions");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "AdministratorActions");

            migrationBuilder.DropColumn(
                name: "IsVisible",
                table: "AdministratorActions");

            migrationBuilder.DropColumn(
                name: "LastModifiedById",
                table: "AdministratorActions");

            migrationBuilder.DropColumn(
                name: "LastModifyDate",
                table: "AdministratorActions");
        }
    }
}
