﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TaskAgendaProj.Migrations
{
    public partial class AddDataRegisteredInUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DataRegistered",
                table: "Users",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DataRegistered",
                table: "Users");
        }
    }
}
