using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ManagementNote.Migrations
{
    public partial class newDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "Lastlogin",
                table: "User",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 9, 3, 11, 30, 59, 461, DateTimeKind.Local).AddTicks(7162),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 2, 5, 16, 3, 6, 446, DateTimeKind.Local).AddTicks(4754));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateCreated",
                table: "User",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 9, 3, 11, 30, 59, 461, DateTimeKind.Local).AddTicks(6836),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 2, 5, 16, 3, 6, 446, DateTimeKind.Local).AddTicks(4508));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateUpdate",
                table: "Note",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 9, 3, 11, 30, 59, 461, DateTimeKind.Local).AddTicks(8045),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 2, 5, 16, 3, 6, 446, DateTimeKind.Local).AddTicks(5251));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateCreate",
                table: "Note",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 9, 3, 11, 30, 59, 461, DateTimeKind.Local).AddTicks(7652),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 2, 5, 16, 3, 6, 446, DateTimeKind.Local).AddTicks(5046));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "Lastlogin",
                table: "User",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 2, 5, 16, 3, 6, 446, DateTimeKind.Local).AddTicks(4754),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 9, 3, 11, 30, 59, 461, DateTimeKind.Local).AddTicks(7162));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateCreated",
                table: "User",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 2, 5, 16, 3, 6, 446, DateTimeKind.Local).AddTicks(4508),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 9, 3, 11, 30, 59, 461, DateTimeKind.Local).AddTicks(6836));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateUpdate",
                table: "Note",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 2, 5, 16, 3, 6, 446, DateTimeKind.Local).AddTicks(5251),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 9, 3, 11, 30, 59, 461, DateTimeKind.Local).AddTicks(8045));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateCreate",
                table: "Note",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 2, 5, 16, 3, 6, 446, DateTimeKind.Local).AddTicks(5046),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 9, 3, 11, 30, 59, 461, DateTimeKind.Local).AddTicks(7652));
        }
    }
}
