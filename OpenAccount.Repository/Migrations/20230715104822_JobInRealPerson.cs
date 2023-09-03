using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OpenAccount.Repository.Migrations
{
	/// <inheritdoc />
	public partial class JobInRealPerson : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "JobId",
                table: "RealPersonInfo",
                type: "int",
                nullable: true,
                comment: "شغل");

            migrationBuilder.UpdateData(
                table: "AccountTypeSetting",
                keyColumn: "Id",
                keyValue: (short)1,
                column: "SysDate",
                value: new DateTime(2023, 7, 15, 14, 18, 22, 613, DateTimeKind.Local).AddTicks(6815));

            migrationBuilder.UpdateData(
                table: "AccountTypeSetting",
                keyColumn: "Id",
                keyValue: (short)2,
                column: "SysDate",
                value: new DateTime(2023, 7, 15, 14, 18, 22, 613, DateTimeKind.Local).AddTicks(6851));

            migrationBuilder.CreateIndex(
                name: "IX_RealPersonInfo_JobId",
                table: "RealPersonInfo",
                column: "JobId");

            migrationBuilder.AddForeignKey(
                name: "FK_RealPersonInfo_Job_JobId",
                table: "RealPersonInfo",
                column: "JobId",
                principalTable: "Job",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RealPersonInfo_Job_JobId",
                table: "RealPersonInfo");

            migrationBuilder.DropIndex(
                name: "IX_RealPersonInfo_JobId",
                table: "RealPersonInfo");

            migrationBuilder.DropColumn(
                name: "JobId",
                table: "RealPersonInfo");

            migrationBuilder.UpdateData(
                table: "AccountTypeSetting",
                keyColumn: "Id",
                keyValue: (short)1,
                column: "SysDate",
                value: new DateTime(2023, 7, 15, 11, 13, 34, 565, DateTimeKind.Local).AddTicks(6616));

            migrationBuilder.UpdateData(
                table: "AccountTypeSetting",
                keyColumn: "Id",
                keyValue: (short)2,
                column: "SysDate",
                value: new DateTime(2023, 7, 15, 11, 13, 34, 565, DateTimeKind.Local).AddTicks(6651));
        }
    }
}
