using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ManagementNote.Migrations
{
    public partial class AddRefreshToken : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "Lastlogin",
                table: "User",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 9, 4, 20, 41, 30, 784, DateTimeKind.Local).AddTicks(6112),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 9, 3, 11, 30, 59, 461, DateTimeKind.Local).AddTicks(7162));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateCreated",
                table: "User",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 9, 4, 20, 41, 30, 784, DateTimeKind.Local).AddTicks(5837),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 9, 3, 11, 30, 59, 461, DateTimeKind.Local).AddTicks(6836));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateUpdate",
                table: "Note",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 9, 4, 20, 41, 30, 784, DateTimeKind.Local).AddTicks(6604),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 9, 3, 11, 30, 59, 461, DateTimeKind.Local).AddTicks(8045));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateCreate",
                table: "Note",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 9, 4, 20, 41, 30, 784, DateTimeKind.Local).AddTicks(6414),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 9, 3, 11, 30, 59, 461, DateTimeKind.Local).AddTicks(7652));

            migrationBuilder.CreateTable(
                name: "RefreshToken",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserNameToken = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Token = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    JwtId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsUsed = table.Column<bool>(type: "bit", nullable: false),
                    IsRevoked = table.Column<bool>(type: "bit", nullable: false),
                    IsSueAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExpiredAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefreshToken", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RefreshToken_User_UserNameToken",
                        column: x => x.UserNameToken,
                        principalTable: "User",
                        principalColumn: "UserName",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RefreshToken_UserNameToken",
                table: "RefreshToken",
                column: "UserNameToken");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RefreshToken");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Lastlogin",
                table: "User",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 9, 3, 11, 30, 59, 461, DateTimeKind.Local).AddTicks(7162),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 9, 4, 20, 41, 30, 784, DateTimeKind.Local).AddTicks(6112));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateCreated",
                table: "User",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 9, 3, 11, 30, 59, 461, DateTimeKind.Local).AddTicks(6836),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 9, 4, 20, 41, 30, 784, DateTimeKind.Local).AddTicks(5837));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateUpdate",
                table: "Note",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 9, 3, 11, 30, 59, 461, DateTimeKind.Local).AddTicks(8045),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 9, 4, 20, 41, 30, 784, DateTimeKind.Local).AddTicks(6604));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateCreate",
                table: "Note",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 9, 3, 11, 30, 59, 461, DateTimeKind.Local).AddTicks(7652),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 9, 4, 20, 41, 30, 784, DateTimeKind.Local).AddTicks(6414));
        }
    }
}
