using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OpenAccount.Repository.Migrations
{
    /// <inheritdoc />
    public partial class age : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "CardPrice",
                table: "AccountTypeSetting",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                comment: "هزینه صدور کارت");

            migrationBuilder.AddColumn<long>(
                name: "CardSendPrice",
                table: "AccountTypeSetting",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                comment: "هزینه ارسال کارت");

            migrationBuilder.AddColumn<byte>(
                name: "MaxAge",
                table: "AccountTypeSetting",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0,
                comment: "بیشینه ی زندگی");

            migrationBuilder.AddColumn<byte>(
                name: "MinAge",
                table: "AccountTypeSetting",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0,
                comment: "کمینه ی زندگی");

            migrationBuilder.CreateTable(
                name: "RequestCard",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "سفارش کارت"),
                    KeyCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CardPro = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TemplateName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Layout = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequestCard", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RequestCard_Request_Id",
                        column: x => x.Id,
                        principalTable: "Request",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "AccountTypeSetting",
                keyColumn: "Id",
                keyValue: (short)1,
                columns: new[] { "AccountGroupId", "CardPrice", "CardSendPrice", "MaxAge", "MinAge", "SysDate" },
                values: new object[] { "045", 36000L, 250000L, (byte)70, (byte)18, new DateTime(2023, 8, 16, 8, 56, 18, 411, DateTimeKind.Local).AddTicks(9391) });

            migrationBuilder.UpdateData(
                table: "AccountTypeSetting",
                keyColumn: "Id",
                keyValue: (short)2,
                columns: new[] { "AccountGroupId", "CardPrice", "CardSendPrice", "MaxAge", "MinAge", "SysDate" },
                values: new object[] { "022", 36000L, 250000L, (byte)70, (byte)18, new DateTime(2023, 8, 16, 8, 56, 18, 411, DateTimeKind.Local).AddTicks(9426) });

            migrationBuilder.InsertData(
                table: "AccountTypeSetting",
                columns: new[] { "Id", "AccountGroupId", "AccountType", "AccountTypeTitle", "CardPrice", "CardSendPrice", "IdentificationInquiry", "InqueryPrice", "IsActive", "MaxAge", "MinAge", "MinBalance", "PostalCodeInquiry", "Stamp", "SysDate" },
                values: new object[] { (short)4, "000", (byte)2, "مرابحه", 36000L, 250000L, 4500L, 0L, true, (byte)70, (byte)18, 500000L, 15000L, 10000, new DateTime(2023, 8, 16, 8, 56, 18, 411, DateTimeKind.Local).AddTicks(9444) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RequestCard");

            migrationBuilder.DeleteData(
                table: "AccountTypeSetting",
                keyColumn: "Id",
                keyValue: (short)4);

            migrationBuilder.DropColumn(
                name: "CardPrice",
                table: "AccountTypeSetting");

            migrationBuilder.DropColumn(
                name: "CardSendPrice",
                table: "AccountTypeSetting");

            migrationBuilder.DropColumn(
                name: "MaxAge",
                table: "AccountTypeSetting");

            migrationBuilder.DropColumn(
                name: "MinAge",
                table: "AccountTypeSetting");

            migrationBuilder.UpdateData(
                table: "AccountTypeSetting",
                keyColumn: "Id",
                keyValue: (short)1,
                columns: new[] { "AccountGroupId", "SysDate" },
                values: new object[] { "45", new DateTime(2023, 8, 7, 16, 14, 2, 86, DateTimeKind.Local).AddTicks(767) });

            migrationBuilder.UpdateData(
                table: "AccountTypeSetting",
                keyColumn: "Id",
                keyValue: (short)2,
                columns: new[] { "AccountGroupId", "SysDate" },
                values: new object[] { "22", new DateTime(2023, 8, 7, 16, 14, 2, 86, DateTimeKind.Local).AddTicks(809) });
        }
    }
}
