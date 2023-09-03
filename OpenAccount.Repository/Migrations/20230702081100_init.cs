using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace OpenAccount.Repository.Migrations
{
	/// <inheritdoc />
	public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AccountTypeSetting",
                columns: table => new
                {
                    Id = table.Column<short>(type: "smallint", nullable: false, comment: "تنظیمات هر نوع حساب")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccountType = table.Column<byte>(type: "tinyint", nullable: false, comment: "نوع حساب"),
                    AccountGroupId = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "گروه حساب"),
                    AccountTypeTitle = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "شرح نوع حساب"),
                    CardPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false, comment: "هزینه کارت"),
                    OpenAccountPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false, comment: "هزینه افتتاح حساب"),
                    MinBalance = table.Column<decimal>(type: "decimal(18,2)", nullable: false, comment: "کف حساب"),
                    InqueryPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false, comment: "هزینه استعلام"),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, comment: "این تنظیمات برای این نوع حساب فعال است؟"),
                    SysDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountTypeSetting", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Education",
                columns: table => new
                {
                    Id = table.Column<byte>(type: "tinyint", nullable: false, comment: "تحصیلات"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "عنوان")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Education", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EntityException",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "پیام پارسی"),
                    Exception = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "Inner exception"),
                    SysDate = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "زمان وقوع خطا"),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "کاربر باجت")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EntityException", x => x.Id);
                },
                comment: "خطا های موجودیت");

            migrationBuilder.CreateTable(
                name: "JobCategory",
                columns: table => new
                {
                    Id = table.Column<byte>(type: "tinyint", nullable: false, comment: "گروه مشاغل"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobCategory", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Province",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, comment: "استان"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "نام استان"),
                    Code = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Province", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Job",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, comment: "مشاغل"),
                    Code = table.Column<byte>(type: "tinyint", nullable: false, comment: "Tata Id"),
                    JobCategoryId = table.Column<byte>(type: "tinyint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Job", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Job_JobCategory_JobCategoryId",
                        column: x => x.JobCategoryId,
                        principalTable: "JobCategory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "City",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, comment: "شهر"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "نام شهر"),
                    ProvinceId = table.Column<int>(type: "int", nullable: false, comment: "استان"),
                    PostCityCode = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_City", x => x.Id);
                    table.ForeignKey(
                        name: "FK_City_Province_ProvinceId",
                        column: x => x.ProvinceId,
                        principalTable: "Province",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Person",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "شخص حقوقی"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "نام شخص"),
                    LatinName = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "نام لاتین شخص"),
                    NationalCode = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "کد / شناسه ملی شخص حقیقی / حقوقی"),
                    CityId = table.Column<int>(type: "int", nullable: false, comment: "شهر محل تولد / ثبت"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "تاریخ محل تولد/ثبت")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Person", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Person_City_CityId",
                        column: x => x.CityId,
                        principalTable: "City",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Address",
                columns: table => new
                {
                    PersonId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "اشخاص"),
                    PostalCode = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "کدپستی"),
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "آدرس"),
                    BuildingName = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "نام ساختمان"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Floor = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "طبقه"),
                    HouseNumber = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "پلاک"),
                    LocalityCode = table.Column<int>(type: "int", nullable: true, comment: "کد محله"),
                    LocalityType = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "نوع محله"),
                    Province = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "استان"),
                    SideFloor = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "طبقه جانبی"),
                    Street = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "خیابان"),
                    Street2 = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "خیابان"),
                    SubLocality = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "منطقه فرعی"),
                    TownShip = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "شهرستان"),
                    Village = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "روستا"),
                    Zone = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "منطفه - ناحیه"),
                    FullAddress = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "آدرس کامل")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Address", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Address_Person_PersonId",
                        column: x => x.PersonId,
                        principalTable: "Person",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LegalPerson",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "شخص حقوقی"),
                    Brand = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "نام تجاری"),
                    EconomicNo = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "شماره اقتصادی"),
                    RegisterNo = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "شماره ثبت"),
                    CompanyType = table.Column<byte>(type: "tinyint", nullable: false, comment: "نوع شرکت")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LegalPerson", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LegalPerson_Person_Id",
                        column: x => x.Id,
                        principalTable: "Person",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PersonInfo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "اطلاعات تکمیلی شخص حقوقی"),
                    PersonId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "شخص"),
                    ErrorCode = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "کدخطا در دریافت اطلاعات تکمیلی"),
                    ErrorMessage = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "شرح خطا در دریافت اطلاعات تکمیلی"),
                    PostalCode = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "کدپستی"),
                    SysDate = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "زمان ثبت اطلاعات"),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, comment: "آخرین اطلاعات خوانده شده")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonInfo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PersonInfo_Person_PersonId",
                        column: x => x.PersonId,
                        principalTable: "Person",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RealPerson",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "شخص حقوقی"),
                    Family = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "نام خانوادگی"),
                    FatherName = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "نام پدر"),
                    IsMale = table.Column<bool>(type: "bit", nullable: false, comment: "جنسیت"),
                    EducationId = table.Column<byte>(type: "tinyint", nullable: false, comment: "تحصیلات"),
                    LatinFamily = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "نام خانوادگی لاتین")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RealPerson", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RealPerson_Education_EducationId",
                        column: x => x.EducationId,
                        principalTable: "Education",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RealPerson_Person_Id",
                        column: x => x.Id,
                        principalTable: "Person",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Request",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "استعلام تسهیلات سمات"),
                    PersonId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "شخص"),
                    AccountType = table.Column<byte>(type: "tinyint", nullable: false, comment: "نوع حساب"),
                    RequestStateType = table.Column<byte>(type: "tinyint", nullable: false, comment: "آخرین وضعیت (مرحله) در خواست")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Request", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Request_Person_PersonId",
                        column: x => x.PersonId,
                        principalTable: "Person",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LegalPersonInfo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "اطلاعات تکمیلی شخص حقوقی")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LegalPersonInfo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LegalPersonInfo_PersonInfo_Id",
                        column: x => x.Id,
                        principalTable: "PersonInfo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RealPersonInfo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "اطلاعات تکمیلی شخص حقوقی"),
                    IsDead = table.Column<bool>(type: "bit", nullable: false, comment: "شخص مرده است؟"),
                    BirthPlaceAreaCode = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "محل تولد"),
                    BirthPlaceOfficeCode = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "محل صدور"),
                    SocialIdentityExtensionSeries = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "سری حرفی سریال شناسنامه"),
                    SocialIdentityNumber = table.Column<long>(type: "bigint", nullable: false, comment: "شماره شناسنامه"),
                    SocialIdentitySeries = table.Column<int>(type: "int", nullable: false, comment: "سریال شناسنامه")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RealPersonInfo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RealPersonInfo_PersonInfo_Id",
                        column: x => x.Id,
                        principalTable: "PersonInfo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RequestAccountTypeSetting",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "تنظیماتی که برای هر درخواست افتتاح حساب است"),
                    AccountTypeSettingId = table.Column<short>(type: "smallint", nullable: false, comment: "تنظیمات هر نوع حساب")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequestAccountTypeSetting", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RequestAccountTypeSetting_AccountTypeSetting_AccountTypeSettingId",
                        column: x => x.AccountTypeSettingId,
                        principalTable: "AccountTypeSetting",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RequestAccountTypeSetting_Request_Id",
                        column: x => x.Id,
                        principalTable: "Request",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RequestCommitmentLetter",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "دریافت تعهدنامه"),
                    CommitmentLetterGot = table.Column<bool>(type: "bit", nullable: false, comment: "تعهدنامه دریافت شد؟"),
                    CommitmentLetterError = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "مشکل دریافت تعهدنامه"),
                    FileAddress = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "آدرس فایل دریافت شده"),
                    SysDate = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "تاریخ دریافت فایل")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequestCommitmentLetter", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RequestCommitmentLetter_Request_Id",
                        column: x => x.Id,
                        principalTable: "Request",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RequestDigitalSignature",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "امضای دیجیتال هر درخواست"),
                    DigitalSignatureDone = table.Column<bool>(type: "bit", nullable: false, comment: "امضای دیجیتال اعمال شد؟"),
                    DigitalSignatureError = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "مشکل امضای دیجیتال"),
                    SysDate = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "تاریخ دریافت فایل")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequestDigitalSignature", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RequestDigitalSignature_Request_Id",
                        column: x => x.Id,
                        principalTable: "Request",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RequestRealSignature",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "امضای وافعی"),
                    FileAddress = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "آدرس فایل دریافت شده"),
                    SysDate = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "تاریخ دریافت فایل")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequestRealSignature", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RequestRealSignature_Request_Id",
                        column: x => x.Id,
                        principalTable: "Request",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RequestStateLog",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "لاگ تغییر وضعیت درخواست از مرحله ای به مرحله ی دیگر"),
                    RequestId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "شناسه ی درخواست"),
                    RequestState = table.Column<byte>(type: "tinyint", nullable: false),
                    SysDate = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "زمان ثبت رکورد")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequestStateLog", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RequestStateLog_Request_RequestId",
                        column: x => x.RequestId,
                        principalTable: "Request",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SamatChequeInquiryRequest",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "استعلام چک سمات"),
                    RequestId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "درخواست افتتاح حساب"),
                    ActionCode = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "Response code of service"),
                    NationalId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PersonType = table.Column<int>(type: "int", nullable: false, comment: "1: haghighi 2: hoghughi 3: haghighi atba 4: hoghughi atba"),
                    ErrorExMessage = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "خطای سیستمی"),
                    SysDate = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "زمان استعلام")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SamatChequeInquiryRequest", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SamatChequeInquiryRequest_Request_RequestId",
                        column: x => x.RequestId,
                        principalTable: "Request",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SamatLoanInquiryRequest",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "استعلام تسهیلات سمات"),
                    HasError = table.Column<bool>(type: "bit", nullable: false),
                    ActionCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CustomerType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateEstlm = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NationalCd = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ShenaseEstlm = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ShenaseRes = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SumAmBedehiKol = table.Column<int>(type: "int", nullable: false),
                    SumAmTahod = table.Column<int>(type: "int", nullable: false),
                    SumAmBenefit = table.Column<int>(type: "int", nullable: false),
                    SumAmEltezam = table.Column<int>(type: "int", nullable: false),
                    SumAmDirkard = table.Column<int>(type: "int", nullable: false),
                    SumAmMashkuk = table.Column<int>(type: "int", nullable: false),
                    SumAmMoavagh = table.Column<int>(type: "int", nullable: false),
                    SumAmOriginal = table.Column<int>(type: "int", nullable: false),
                    SumAmSarResid = table.Column<int>(type: "int", nullable: false),
                    SumAmSukht = table.Column<int>(type: "int", nullable: false),
                    Fname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Lname = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SamatLoanInquiryRequest", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SamatLoanInquiryRequest_Request_Id",
                        column: x => x.Id,
                        principalTable: "Request",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StartException",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RequestId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "درخواست افتتاح حساب")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StartException", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StartException_EntityException_Id",
                        column: x => x.Id,
                        principalTable: "EntityException",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StartException_Request_RequestId",
                        column: x => x.RequestId,
                        principalTable: "Request",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "خطا های مرحله ی اولیه");

            migrationBuilder.CreateTable(
                name: "UserAccount",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "افتتاح حساب کاربر"),
                    AccountNumber = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "شماره حساب"),
                    SysDate = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "تاریخ افتتاح حساب")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserAccount", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserAccount_Request_Id",
                        column: x => x.Id,
                        principalTable: "Request",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WalletStatus",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "وضعیت کیف پول"),
                    RequestId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "درخواست افتتاح حساب"),
                    Balance = table.Column<decimal>(type: "decimal(18,2)", nullable: false, comment: "موجودی واقعی مشتری"),
                    NeededBalance = table.Column<decimal>(type: "decimal(18,2)", nullable: false, comment: "موجودی مورد نیاز"),
                    SysDate = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "تاریخ استعلام موجودی")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WalletStatus", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WalletStatus_Request_RequestId",
                        column: x => x.RequestId,
                        principalTable: "Request",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SamatBouncedChequeItem",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "اطلاعات چک برگشتی"),
                    SamatChequeInquiryRequestId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Amount = table.Column<int>(type: "int", nullable: false, comment: "مبلغ چک"),
                    BankCode = table.Column<int>(type: "int", nullable: false, comment: "کد بانک"),
                    BouncedAmount = table.Column<int>(type: "int", nullable: false, comment: "مبلغ برگشتی"),
                    BouncedDate = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "تاریخ صدور (ارسال) برگشت"),
                    BranchBounced = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "کد شعبه برگشت زننده"),
                    BranchOrigin = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "کد شعبه افتتاح کننده"),
                    ChannelKind = table.Column<int>(type: "int", nullable: false, comment: "نحوه ارائه چک"),
                    CurrencyCode = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "کد ارز"),
                    CurrencyRate = table.Column<decimal>(type: "decimal(18,2)", nullable: false, comment: "نرخ ارز"),
                    DeadlineDate = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "تاریخ سررسید چک"),
                    Iban = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "شماره شباي حساب"),
                    Serial = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "سریال چک"),
                    BouncedBranchName = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "نام شعبه برگشت زننده"),
                    CustomerType = table.Column<int>(type: "int", nullable: false, comment: "نوع مشتری"),
                    IdCheque = table.Column<int>(type: "int", nullable: false, comment: "کد رهگیري چک"),
                    OriginBranchName = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "نام شعبه افتتاح کننده")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SamatBouncedChequeItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SamatBouncedChequeItem_SamatChequeInquiryRequest_SamatChequeInquiryRequestId",
                        column: x => x.SamatChequeInquiryRequestId,
                        principalTable: "SamatChequeInquiryRequest",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SamatLoanInquiryRequestItem",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "استعلام تسهیلات سمات ریز"),
                    SamatLoanInquiryRequestId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AdamSabtSanadEntezami = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AmBedehiKol = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AmBenefit = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AmDirkard = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AmEltezam = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AmMashkuk = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AmMoavagh = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AmOriginal = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AmSarResid = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AmSukht = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AmTahod = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ArzCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BankCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DasteBandi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Date = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateEstehal = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateSarResid = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Estehal = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HadafAzDaryaft = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MainIdNo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MainLgId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PlaceCdMasraf = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RequestNum = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RequstType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RsrcTamin = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ShobeCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ShobeName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SamatLoanInquiryRequestItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SamatLoanInquiryRequestItem_SamatLoanInquiryRequest_SamatLoanInquiryRequestId",
                        column: x => x.SamatLoanInquiryRequestId,
                        principalTable: "SamatLoanInquiryRequest",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SamatChequeBouncedReason",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "دلایل برگشت سمات"),
                    Int = table.Column<int>(type: "int", nullable: false),
                    SamatBouncedChequeItemId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SamatChequeBouncedReason", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SamatChequeBouncedReason_SamatBouncedChequeItem_SamatBouncedChequeItemId",
                        column: x => x.SamatBouncedChequeItemId,
                        principalTable: "SamatBouncedChequeItem",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AccountTypeSetting",
                columns: new[] { "Id", "AccountGroupId", "AccountType", "AccountTypeTitle", "CardPrice", "InqueryPrice", "IsActive", "MinBalance", "OpenAccountPrice", "SysDate" },
                values: new object[,]
                {
                    { (short)1, "45", (byte)0, "کوتاه مدت", 200000m, 20000m, true, 1000000m, 40000m, new DateTime(2023, 7, 2, 11, 41, 0, 58, DateTimeKind.Local).AddTicks(950) },
                    { (short)2, "22", (byte)1, "قرض الحسنه", 200000m, 20000m, true, 330000m, 40000m, new DateTime(2023, 7, 2, 11, 41, 0, 58, DateTimeKind.Local).AddTicks(989) }
                });

            migrationBuilder.InsertData(
                table: "Education",
                columns: new[] { "Id", "Title" },
                values: new object[,]
                {
                    { (byte)1, "زیر دیپلم" },
                    { (byte)2, "دیپلم" },
                    { (byte)3, "فوق دیپلم" },
                    { (byte)4, "لیسانس" },
                    { (byte)5, "فوق لیسانس" },
                    { (byte)6, "دکتري" },
                    { (byte)7, "بالاتر از دکتري" },
                    { (byte)8, "حوزوي" }
                });

            migrationBuilder.InsertData(
                table: "JobCategory",
                columns: new[] { "Id", "IsActive", "Name" },
                values: new object[,]
                {
                    { (byte)1, true, "حقوق بگیر" },
                    { (byte)2, true, "تولید کنندگان" },
                    { (byte)3, true, "خرید و فروش - صادر کنندگان و وارد کنندگان" },
                    { (byte)4, false, "نیروهاي مسلح" },
                    { (byte)5, true, "خدماتی – حقیقی" },
                    { (byte)6, true, "خدماتی حقوقی" },
                    { (byte)7, false, "بیکار – خانه دار – محصل" }
                });

            migrationBuilder.InsertData(
                table: "Province",
                columns: new[] { "Id", "Code", "Name" },
                values: new object[] { 0, "", "نامشخص" });

            migrationBuilder.InsertData(
                table: "City",
                columns: new[] { "Id", "Name", "PostCityCode", "ProvinceId" },
                values: new object[] { 0, "نامشخص", "0", 0 });

            migrationBuilder.InsertData(
                table: "Job",
                columns: new[] { "Id", "Code", "IsActive", "JobCategoryId", "Name" },
                values: new object[,]
                {
                    { 1, (byte)1, true, (byte)1, "کارگر ساده" },
                    { 2, (byte)2, true, (byte)1, "کارگر فنی" },
                    { 3, (byte)3, true, (byte)1, "کارمند ساده" },
                    { 4, (byte)4, true, (byte)1, "کارمند ارشد" },
                    { 5, (byte)5, true, (byte)1, "بازنشستگان و مستمري بگیرا ن" },
                    { 6, (byte)1, true, (byte)2, "جزء" },
                    { 7, (byte)2, true, (byte)2, "کل" },
                    { 8, (byte)1, true, (byte)3, "جزء" },
                    { 9, (byte)2, true, (byte)3, "کل" },
                    { 10, (byte)1, true, (byte)5, "رانندگان" },
                    { 11, (byte)2, true, (byte)5, "پزشکان و متخصصان علوم پزشکی" },
                    { 12, (byte)3, true, (byte)5, "قضات، وکلا، کارشناسان، مشاوران حقوقی" },
                    { 13, (byte)4, true, (byte)5, "صاحبان مراکز آموزشی و متخصصان آموزشی" },
                    { 14, (byte)5, true, (byte)5, "عاملان فروش و حق العمل کاران" },
                    { 15, (byte)6, true, (byte)5, "تعمیرکاران، صنعتگران و تولید کنندگان طلا" },
                    { 16, (byte)7, true, (byte)5, "هنرمندان و ورزشکاران" },
                    { 17, (byte)8, true, (byte)5, "نویسندگان" },
                    { 18, (byte)9, true, (byte)5, "خدمات مالی" },
                    { 19, (byte)1, true, (byte)6, "آژانس هاي مسافرتی" },
                    { 20, (byte)2, true, (byte)6, "پزشکان و متخصصان علوم پزشکی و پیراپزشکی" },
                    { 21, (byte)3, true, (byte)6, "قضات، وکلا، کارشناسان، مشاوران حقوقی" },
                    { 22, (byte)4, true, (byte)6, "صاحبان مراکز آموزشی و متخصصان آموزشی" },
                    { 23, (byte)5, true, (byte)6, "مشاغل ساختمانی، تاسیسات فنی و صنعتی" },
                    { 24, (byte)6, true, (byte)6, "ناشران و صاحبان موسسات انتشاراتی" },
                    { 25, (byte)7, true, (byte)6, "مهندسین مشاور، دارندگان شرکت ها و مراکز مهندسی" },
                    { 26, (byte)8, true, (byte)6, "دارندگان موسسات تبلیغاتی، مشاوران تبلیغاتی" },
                    { 27, (byte)9, true, (byte)6, "صاحبان موسسات خدماتی و آزمایشگاهی" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Address_PersonId",
                table: "Address",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_City_ProvinceId",
                table: "City",
                column: "ProvinceId");

            migrationBuilder.CreateIndex(
                name: "IX_Job_JobCategoryId",
                table: "Job",
                column: "JobCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Person_CityId",
                table: "Person",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonInfo_PersonId",
                table: "PersonInfo",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_RealPerson_EducationId",
                table: "RealPerson",
                column: "EducationId");

            migrationBuilder.CreateIndex(
                name: "IX_Request_PersonId",
                table: "Request",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_RequestAccountTypeSetting_AccountTypeSettingId",
                table: "RequestAccountTypeSetting",
                column: "AccountTypeSettingId");

            migrationBuilder.CreateIndex(
                name: "IX_RequestStateLog_RequestId",
                table: "RequestStateLog",
                column: "RequestId");

            migrationBuilder.CreateIndex(
                name: "IX_SamatBouncedChequeItem_SamatChequeInquiryRequestId",
                table: "SamatBouncedChequeItem",
                column: "SamatChequeInquiryRequestId");

            migrationBuilder.CreateIndex(
                name: "IX_SamatChequeBouncedReason_SamatBouncedChequeItemId",
                table: "SamatChequeBouncedReason",
                column: "SamatBouncedChequeItemId");

            migrationBuilder.CreateIndex(
                name: "IX_SamatChequeInquiryRequest_RequestId",
                table: "SamatChequeInquiryRequest",
                column: "RequestId");

            migrationBuilder.CreateIndex(
                name: "IX_SamatLoanInquiryRequestItem_SamatLoanInquiryRequestId",
                table: "SamatLoanInquiryRequestItem",
                column: "SamatLoanInquiryRequestId");

            migrationBuilder.CreateIndex(
                name: "IX_StartException_RequestId",
                table: "StartException",
                column: "RequestId");

            migrationBuilder.CreateIndex(
                name: "IX_WalletStatus_RequestId",
                table: "WalletStatus",
                column: "RequestId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Address");

            migrationBuilder.DropTable(
                name: "Job");

            migrationBuilder.DropTable(
                name: "LegalPerson");

            migrationBuilder.DropTable(
                name: "LegalPersonInfo");

            migrationBuilder.DropTable(
                name: "RealPerson");

            migrationBuilder.DropTable(
                name: "RealPersonInfo");

            migrationBuilder.DropTable(
                name: "RequestAccountTypeSetting");

            migrationBuilder.DropTable(
                name: "RequestCommitmentLetter");

            migrationBuilder.DropTable(
                name: "RequestDigitalSignature");

            migrationBuilder.DropTable(
                name: "RequestRealSignature");

            migrationBuilder.DropTable(
                name: "RequestStateLog");

            migrationBuilder.DropTable(
                name: "SamatChequeBouncedReason");

            migrationBuilder.DropTable(
                name: "SamatLoanInquiryRequestItem");

            migrationBuilder.DropTable(
                name: "StartException");

            migrationBuilder.DropTable(
                name: "UserAccount");

            migrationBuilder.DropTable(
                name: "WalletStatus");

            migrationBuilder.DropTable(
                name: "JobCategory");

            migrationBuilder.DropTable(
                name: "Education");

            migrationBuilder.DropTable(
                name: "PersonInfo");

            migrationBuilder.DropTable(
                name: "AccountTypeSetting");

            migrationBuilder.DropTable(
                name: "SamatBouncedChequeItem");

            migrationBuilder.DropTable(
                name: "SamatLoanInquiryRequest");

            migrationBuilder.DropTable(
                name: "EntityException");

            migrationBuilder.DropTable(
                name: "SamatChequeInquiryRequest");

            migrationBuilder.DropTable(
                name: "Request");

            migrationBuilder.DropTable(
                name: "Person");

            migrationBuilder.DropTable(
                name: "City");

            migrationBuilder.DropTable(
                name: "Province");
        }
    }
}
