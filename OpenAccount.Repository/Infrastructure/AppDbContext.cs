using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OpenAccount.Entities.Accounts;
using OpenAccount.Entities.PersonData;
using OpenAccount.Entities.Publics;
using OpenAccount.Entities.Publics.Exceptions;
using OpenAccount.Entities.Publics.Wallets;
using OpenAccount.Entities.Requests;
using OpenAccount.Entities.Requests.InqueryCheque;
using OpenAccount.Entities.Requests.InqueryLoan;
using System.ComponentModel;
using static System.Net.Mime.MediaTypeNames;

namespace OpenAccount.Repository.Infrastructure
{
	public sealed class AppDbContext : DbContext
	{
		public AppDbContext()
		{
		}

		public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
		{
			//_ = Database.EnsureCreated();
		}

		public DbSet<Request> Requests { get; set; }
		public DbSet<SamatChequeInquiryRequest> SamatChequeInquiryRequests { get; set; }
		public DbSet<SamatBouncedChequeItem> SamatBouncedChequeItems { get; set; }
		public DbSet<SamatChequeBouncedReason> SamatChequeBouncedReasons { get; set; }
		public DbSet<SamatLoanInquiryRequest> LoanInquiryRequests { get; set; }
		public DbSet<SamatLoanInquiryRequestItem> SamatLoanInquiryRequestItems { get; set; }
		public DbSet<RequestStateLog> RequestStateLogs { get; set; }
		public DbSet<AccountTypeSetting> AccountTypeSettings { get; set; }
		public DbSet<RequestAccountTypeSetting> RequestAccountTypeSettings { get; set; }
		public DbSet<WalletStatus> WalletStatuses { get; set; }
		public DbSet<Person> People { get; set; }
		public DbSet<RealPerson> RealPeople { get; set; }
		public DbSet<LegalPerson> LegalPeople { get; set; }
		public DbSet<PersonInfo> PersonInfos { get; set; }
		public DbSet<RealPersonInfo> RealPersonInfos { get; set; }
		public DbSet<LegalPersonInfo> LegalPersonInfos { get; set; }
		public DbSet<Address> Addresses { get; set; }
		public DbSet<City> Cities { get; set; }
		public DbSet<Province> Provinces { get; set; }
		public DbSet<Job> Jobs { get; set; }
		public DbSet<JobCategory> JobCategories { get; set; }
		public DbSet<Education> Educations { get; set; }
		public DbSet<RequestDigitalSignature> RequestDigitalSignatures { get; set; }
		public DbSet<UserAccount> UserAccounts { get; set; }
		#region Exceptions
		public DbSet<EntityException> EntityExceptions { get; set; }
		public DbSet<StartException> StartExceptions { get; set; }
		public DbSet<InquiryException> InquiryExceptions { get; set; }
		public DbSet<PersonException> PersonExceptions { get; set; }
		#endregion
		public DbSet<WithdrawalFromWallet> WithdrawalFromWallets { get; set; }
		public DbSet<RequestDigitalSignatureLog> RequestDigitalSignatureLogs { get; set; }
		public DbSet<RequestRealSignatureToBank> RequestRealSignatureToBanks { get; set; }
		public DbSet<UserAccountLog> UserAccountLogs { get; set; }
		public DbSet<RequestCard> RequestCards { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseSqlServer("Data Source=192.168.5.110,1433;Initial Catalog=ChainedOpenAccount;User ID=sa;password=123456;multipleactiveresultsets=True;TrustServerCertificate=True;");
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Request>().ToTable("Request").IdComment()
				.HasOne(x => x.RequestAccountTypeSetting)
				.WithOne(x => x.Request)
				.HasForeignKey<RequestAccountTypeSetting>(x => x.Id)
				.IsRequired();
			modelBuilder.Entity<Request>()
				.HasOne(x => x.RequestDigitalSignature)
				.WithOne(x => x.Request)
				.HasForeignKey<RequestDigitalSignature>(x => x.Id);
			modelBuilder.Entity<Request>()
				.HasOne(x => x.UserAccount)
				.WithOne(x => x.Request)
				.HasForeignKey<UserAccount>(x => x.Id);
			modelBuilder.Entity<Request>()
				.HasOne(x => x.RequestRealSignatureToBank)
				.WithOne(x => x.Request)
				.HasForeignKey<RequestRealSignatureToBank>(x => x.Id);
			modelBuilder.Entity<Request>()
				.HasOne(x => x.RequestCards)
				.WithOne(x => x.Request)
				.HasForeignKey<RequestCard>(x => x.Id);

			modelBuilder.Entity<SamatChequeInquiryRequest>().ToTable("SamatChequeInquiryRequest").IdComment();
			modelBuilder.Entity<SamatBouncedChequeItem>(x =>
			{
				x.Property(x => x.CurrencyRate).HasColumnType("decimal(18, 2)");
				x.ToTable("SamatBouncedChequeItem").IdComment();
			});
			modelBuilder.Entity<SamatChequeBouncedReason>().ToTable("SamatChequeBouncedReason").IdComment();
			modelBuilder.Entity<SamatLoanInquiryRequest>().ToTable("SamatLoanInquiryRequest").IdComment();
			modelBuilder.Entity<SamatLoanInquiryRequestItem>().ToTable("SamatLoanInquiryRequestItem")
				.IdComment().Property(x => x.RsrcTamin).IsRequired(false);
			modelBuilder.Entity<RequestStateLog>().ToTable("RequestStateLog").IdComment();
			modelBuilder.Entity<AccountTypeSetting>().ToTable("AccountTypeSetting").IdComment();
			modelBuilder.Entity<RequestAccountTypeSetting>().ToTable("RequestAccountTypeSetting").IdComment();
			modelBuilder.Entity<WalletStatus>(x =>
			{
				x.ToTable("WalletStatus").IdComment();
				x.Property(x => x.Balance).HasColumnType("decimal(18, 2)");
				x.Property(x => x.NeededBalance).HasColumnType("decimal(18, 2)");
			});
			modelBuilder.Entity<Person>().ToTable("Person").IdComment();
			modelBuilder.Entity<RealPerson>().ToTable("RealPerson").IdComment();
			modelBuilder.Entity<LegalPerson>().ToTable("LegalPerson").IdComment();
			modelBuilder.Entity<PersonInfo>().ToTable("PersonInfo").IdComment();
			modelBuilder.Entity<RealPersonInfo>().ToTable("RealPersonInfo").IdComment();
			modelBuilder.Entity<LegalPersonInfo>().ToTable("LegalPersonInfo").IdComment();
			modelBuilder.Entity<Address>().ToTable("Address").IdComment();
			modelBuilder.Entity<City>().ToTable("City").IdComment().Property("Id").ValueGeneratedNever();
			modelBuilder.Entity<Province>(x =>
			{
				x.ToTable("Province").IdComment().Property("Id").ValueGeneratedNever();
				x.Property("Code").HasMaxLength(3);
			});
			modelBuilder.Entity<Job>().ToTable("Job").IdComment().Property("Id").ValueGeneratedNever();
			modelBuilder.Entity<JobCategory>(x =>
			{
				x.ToTable("JobCategory").IdComment().Property("Id").ValueGeneratedNever();
				x.HasMany(j => j.Jobs).WithOne(j => j.JobCategory).HasForeignKey(x => x.JobCategoryId);
			});
			modelBuilder.Entity<Education>().ToTable("Education").IdComment().Property("Id").ValueGeneratedNever();
			modelBuilder.Entity<RequestDigitalSignatureLog>().ToTable("RequestDigitalSignatureLog").IdComment();
			modelBuilder.Entity<RequestDigitalSignature>().ToTable("RequestDigitalSignature").IdComment();
			modelBuilder.Entity<UserAccount>().ToTable("UserAccount").IdComment();
			modelBuilder.Entity<UserAccountLog>().ToTable("UserAccountLog").IdComment();
			#region Exceptions
			modelBuilder.Entity<EntityException>().ToTable("EntityException").IdComment();
			modelBuilder.Entity<StartException>().ToTable("StartException").IdComment();
			modelBuilder.Entity<InquiryException>().ToTable("InquiryException").IdComment();
			modelBuilder.Entity<PersonException>().ToTable("PersonException").IdComment();
			#endregion
			modelBuilder.Entity<WithdrawalFromWallet>().ToTable("WithdrawalFromWallet").IdComment();
			modelBuilder.Entity<RequestRealSignatureToBank>().ToTable("RequestRealSignatureToBank").IdComment();
			modelBuilder.Entity<RequestCard>().ToTable("RequestCard").IdComment();

			#region Seed

			modelBuilder.Entity<AccountTypeSetting>().HasData(new AccountTypeSetting
			{
				Id = 1,
				AccountType = AccountType.ShortTermAccount,
				AccountGroupId = "045",
				AccountTypeTitle = OpenAccount.Publics.Utility.GetEnumDescription(AccountType.ShortTermAccount),
				MinBalance = 1000000,
				InqueryPrice = 0,
				IsActive = true,
				SysDate = DateTime.Now,
				Stamp = 10000,
				IdentificationInquiry = 4500,
				PostalCodeInquiry = 15000,
				CardPrice = 36000,
				CardSendPrice = 250000,
				MaxAge = 70,
				MinAge = 18,
				CardToAccount = 19800
			}, new AccountTypeSetting
			{
				Id = 2,
				AccountType = AccountType.LoanAccount,
				AccountGroupId = "022",
				AccountTypeTitle = OpenAccount.Publics.Utility.GetEnumDescription(AccountType.LoanAccount),
				MinBalance = 500000,
				InqueryPrice = 0,
				IsActive = true,
				SysDate = DateTime.Now,
				Stamp = 10000,
				IdentificationInquiry = 4500,
				PostalCodeInquiry = 15000,
				CardPrice = 36000,
				CardSendPrice = 250000,
				MaxAge = 70,
				MinAge = 18,
				CardToAccount = 19800
			}, new AccountTypeSetting
			{
				Id = 3,
				AccountType = AccountType.ProfitAccount,
				AccountGroupId = "000",
				AccountTypeTitle = OpenAccount.Publics.Utility.GetEnumDescription(AccountType.ProfitAccount),
				MinBalance = 0,
				InqueryPrice = 0,
				IsActive = true,
				SysDate = DateTime.Now,
				Stamp = 10000,
				IdentificationInquiry = 4500,
				PostalCodeInquiry = 15000,
				CardPrice = 36000,
				CardSendPrice = 250000,
				MaxAge = 70,
				MinAge = 18,
				CardToAccount = 19800
			});

			var cats = new List<JobCategory>()
			{
				new JobCategory
				{
					IsActive = true,
					Name = "حقوق بگیر",
					Id = 1
				}, new JobCategory
				{
					IsActive = true,
					Name = "تولید کنندگان",
					Id = 2
				}, new JobCategory
				{
					IsActive = true,
					Name = "خرید و فروش - صادر کنندگان و وارد کنندگان",
					Id = 3
				}, new JobCategory
				{
					IsActive = false,
					Name = "نیروهاي مسلح",
					Id = 4
				}, new JobCategory
				{
					IsActive = true,
					Name = "خدماتی – حقیقی",
					Id = 5
				}, new JobCategory
				{
					IsActive = true,
					Name = "خدماتی حقوقی",
					Id = 6
				}, new JobCategory
				{
					IsActive = false,
					Name = "بیکار – خانه دار – محصل",
					Id = 7
				}
			};

			modelBuilder.Entity<JobCategory>().HasData(cats);

			modelBuilder.Entity<Job>().HasData(
				new { Id = 1, Code = (byte)1, IsActive = true, Name = "کارگر ساده", JobCategoryId = (byte)1 },
				new { Id = 2, Code = (byte)2, IsActive = true, Name = "کارگر فنی", JobCategoryId = (byte)1 },
				new { Id = 3, Code = (byte)3, IsActive = true, Name = "کارمند ساده", JobCategoryId = (byte)1 },
				new { Id = 4, Code = (byte)4, IsActive = true, Name = "کارمند ارشد", JobCategoryId = (byte)1 },
				new { Id = 5, Code = (byte)5, IsActive = true, Name = "بازنشستگان و مستمري بگیرا ن", JobCategoryId = (byte)1 },
				new { Id = 6, Code = (byte)1, IsActive = true, Name = "جزء", JobCategoryId = (byte)2 },
				new { Id = 7, Code = (byte)2, IsActive = true, Name = "کل", JobCategoryId = (byte)2 },
				new { Id = 8, Code = (byte)1, IsActive = true, Name = "جزء", JobCategoryId = (byte)3 },
				new { Id = 9, Code = (byte)2, IsActive = true, Name = "کل", JobCategoryId = (byte)3 },
				new { Id = 10, Code = (byte)1, IsActive = true, Name = "رانندگان", JobCategoryId = (byte)5 },
				new { Id = 11, Code = (byte)2, IsActive = true, Name = "پزشکان و متخصصان علوم پزشکی", JobCategoryId = (byte)5 },
				new { Id = 12, Code = (byte)3, IsActive = true, Name = "قضات، وکلا، کارشناسان، مشاوران حقوقی", JobCategoryId = (byte)5 },
				new { Id = 13, Code = (byte)4, IsActive = true, Name = "صاحبان مراکز آموزشی و متخصصان آموزشی", JobCategoryId = (byte)5 },
				new { Id = 14, Code = (byte)5, IsActive = true, Name = "عاملان فروش و حق العمل کاران", JobCategoryId = (byte)5 },
				new { Id = 15, Code = (byte)6, IsActive = true, Name = "تعمیرکاران، صنعتگران و تولید کنندگان طلا", JobCategoryId = (byte)5 },
				new { Id = 16, Code = (byte)7, IsActive = true, Name = "هنرمندان و ورزشکاران", JobCategoryId = (byte)5 },
				new { Id = 17, Code = (byte)8, IsActive = true, Name = "نویسندگان", JobCategoryId = (byte)5 },
				new { Id = 18, Code = (byte)9, IsActive = true, Name = "خدمات مالی", JobCategoryId = (byte)5 },
				new { Id = 19, Code = (byte)1, IsActive = true, Name = "آژانس هاي مسافرتی", JobCategoryId = (byte)6 },
				new { Id = 20, Code = (byte)2, IsActive = true, Name = "پزشکان و متخصصان علوم پزشکی و پیراپزشکی", JobCategoryId = (byte)6 },
				new { Id = 21, Code = (byte)3, IsActive = true, Name = "قضات، وکلا، کارشناسان، مشاوران حقوقی", JobCategoryId = (byte)6 },
				new { Id = 22, Code = (byte)4, IsActive = true, Name = "صاحبان مراکز آموزشی و متخصصان آموزشی", JobCategoryId = (byte)6 },
				new { Id = 23, Code = (byte)5, IsActive = true, Name = "مشاغل ساختمانی، تاسیسات فنی و صنعتی", JobCategoryId = (byte)6 },
				new { Id = 24, Code = (byte)6, IsActive = true, Name = "ناشران و صاحبان موسسات انتشاراتی", JobCategoryId = (byte)6 },
				new { Id = 25, Code = (byte)7, IsActive = true, Name = "مهندسین مشاور، دارندگان شرکت ها و مراکز مهندسی", JobCategoryId = (byte)6 },
				new { Id = 26, Code = (byte)8, IsActive = true, Name = "دارندگان موسسات تبلیغاتی، مشاوران تبلیغاتی", JobCategoryId = (byte)6 },
				new { Id = 27, Code = (byte)9, IsActive = true, Name = "صاحبان موسسات خدماتی و آزمایشگاهی", JobCategoryId = (byte)6 }
			);

			modelBuilder.Entity<Education>().HasData(new Education()
			{
				Id = 1,
				Title = "زیر دیپلم"
			}, new Education()
			{
				Id = 2,
				Title = "دیپلم"
			}, new Education()
			{
				Id = 3,
				Title = "فوق دیپلم"
			}, new Education()
			{
				Id = 4,
				Title = "لیسانس"
			}, new Education()
			{
				Id = 5,
				Title = "فوق لیسانس"
			}, new Education()
			{
				Id = 6,
				Title = "دکتري"
			}, new Education()
			{
				Id = 7,
				Title = "بالاتر از دکتري"
			}, new Education()
			{
				Id = 8,
				Title = "حوزوي"
			});

			modelBuilder.Entity<Province>().HasData(new Province { Id = 0, Name = "نامشخص" });
			modelBuilder.Entity<City>().HasData(new { Id = 0, Name = "نامشخص", ProvinceId = 0, PostCityCode = "0" });

			#endregion

			base.OnModelCreating(modelBuilder);
		}
	}

	public static class ManageComment
	{
		public static EntityTypeBuilder<TEntity> IdComment<TEntity>(this EntityTypeBuilder<TEntity> builder)
			where TEntity : class
		{
			var attrib = Attribute.GetCustomAttribute(typeof(TEntity), typeof(DescriptionAttribute));
			if (attrib != null)
				builder.Property("Id").HasComment(((DescriptionAttribute)attrib).Description);
			return builder;
		}
	}
}