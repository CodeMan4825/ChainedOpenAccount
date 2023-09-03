using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OpenAccount.Repository.Migrations
{
	/// <inheritdoc />
	public partial class useraccountlog : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserAccountLog",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "لاگ افتتاح حساب کاربر"),
                    UserAccountId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ActionMessage = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ActionCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ErrorMessages = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TraceNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReferenceNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ResponseCode = table.Column<int>(type: "int", nullable: false, comment: "Data response code"),
                    ResponseText = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "Data response text"),
                    SysDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserAccountLog", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserAccountLog_UserAccount_UserAccountId",
                        column: x => x.UserAccountId,
                        principalTable: "UserAccount",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "AccountTypeSetting",
                keyColumn: "Id",
                keyValue: (short)1,
                column: "SysDate",
                value: new DateTime(2023, 7, 25, 16, 37, 34, 708, DateTimeKind.Local).AddTicks(7642));

            migrationBuilder.UpdateData(
                table: "AccountTypeSetting",
                keyColumn: "Id",
                keyValue: (short)2,
                column: "SysDate",
                value: new DateTime(2023, 7, 25, 16, 37, 34, 708, DateTimeKind.Local).AddTicks(7676));

            migrationBuilder.CreateIndex(
                name: "IX_UserAccountLog_UserAccountId",
                table: "UserAccountLog",
                column: "UserAccountId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserAccountLog");

            migrationBuilder.UpdateData(
                table: "AccountTypeSetting",
                keyColumn: "Id",
                keyValue: (short)1,
                column: "SysDate",
                value: new DateTime(2023, 7, 24, 14, 52, 12, 414, DateTimeKind.Local).AddTicks(543));

            migrationBuilder.UpdateData(
                table: "AccountTypeSetting",
                keyColumn: "Id",
                keyValue: (short)2,
                column: "SysDate",
                value: new DateTime(2023, 7, 24, 14, 52, 12, 414, DateTimeKind.Local).AddTicks(579));
        }
    }
}
