using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OpenAccount.Repository.Migrations
{
	/// <inheritdoc />
	public partial class WithdrawalFromWallet : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WalletTransferRequest");

            migrationBuilder.CreateTable(
                name: "WithdrawalFromWallet",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "برداشت از کیف پول مشتری"),
                    RequestId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "درخواست افتتاح حساب"),
                    SourceAccount = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "یوزر آیدی کاربر مبدا"),
                    DestinationAccount = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "یوزر آیدی حساب مقصد"),
                    OrganUserId = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "یوزر آیدی سازمان"),
                    EventType = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "نوع تراکنش"),
                    Amount = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "مبلغ درخواستی تراکنش"),
                    DeviceId = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "شناسه دستگاه"),
                    RequestDate = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "زمان ارسال درخواست"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "شرح تراکنش"),
                    Channel = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "کانال درخواست"),
                    Used = table.Column<bool>(type: "bit", nullable: false, comment: "مبلغ از کیف پول کسر شد"),
                    UseDate = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "زمان استفاده از مبلغ"),
                    ReferenceNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TraceNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HostRrn = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "پاسخ به سرویس گیرنده"),
                    ActionCode = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "پاسخ سرویس"),
                    ActionMessage = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "شرح پاسخ"),
                    ErrorExMessage = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "سایر خطاها")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WithdrawalFromWallet", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WithdrawalFromWallet_Request_RequestId",
                        column: x => x.RequestId,
                        principalTable: "Request",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "AccountTypeSetting",
                keyColumn: "Id",
                keyValue: (short)1,
                column: "SysDate",
                value: new DateTime(2023, 7, 12, 7, 52, 49, 444, DateTimeKind.Local).AddTicks(9696));

            migrationBuilder.UpdateData(
                table: "AccountTypeSetting",
                keyColumn: "Id",
                keyValue: (short)2,
                column: "SysDate",
                value: new DateTime(2023, 7, 12, 7, 52, 49, 444, DateTimeKind.Local).AddTicks(9735));

            migrationBuilder.CreateIndex(
                name: "IX_WithdrawalFromWallet_RequestId",
                table: "WithdrawalFromWallet",
                column: "RequestId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WithdrawalFromWallet");

            migrationBuilder.CreateTable(
                name: "WalletTransferRequest",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RequestId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "درخواست افتتاح حساب"),
                    ActionCode = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "پاسخ سرویس"),
                    ActionMessage = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "شرح پاسخ"),
                    Amount = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "مبلغ درخواستی تراکنش"),
                    Channel = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "کانال درخواست"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "شرح تراکنش"),
                    DestinationAccount = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "یوزر آیدی حساب مقصد"),
                    DeviceId = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "شناسه دستگاه"),
                    ErrorExMessage = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "سایر خطاها"),
                    EventType = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "نوع تراکنش"),
                    HostRrn = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "پاسخ به سرویس گیرنده"),
                    OrganUserId = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "یوزر آیدی سازمان"),
                    ReferenceNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RequestDate = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "زمان ارسال درخواست"),
                    SourceAccount = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "یوزر آیدی کاربر مبدا"),
                    TraceNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UsedDate = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "زمان استفاده از مبلغ"),
                    WalletDesCharged = table.Column<bool>(type: "bit", nullable: false, comment: "مبلغ از کیف پول کسر شد")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WalletTransferRequest", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WalletTransferRequest_Request_RequestId",
                        column: x => x.RequestId,
                        principalTable: "Request",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "برداشت از کیف پول مشتری");

            migrationBuilder.UpdateData(
                table: "AccountTypeSetting",
                keyColumn: "Id",
                keyValue: (short)1,
                column: "SysDate",
                value: new DateTime(2023, 7, 11, 16, 31, 13, 370, DateTimeKind.Local).AddTicks(7548));

            migrationBuilder.UpdateData(
                table: "AccountTypeSetting",
                keyColumn: "Id",
                keyValue: (short)2,
                column: "SysDate",
                value: new DateTime(2023, 7, 11, 16, 31, 13, 370, DateTimeKind.Local).AddTicks(7584));

            migrationBuilder.CreateIndex(
                name: "IX_WalletTransferRequest_RequestId",
                table: "WalletTransferRequest",
                column: "RequestId");
        }
    }
}
