using Api;
using DevExpress.DataAccess.ObjectBinding;
using DevExpress.XtraReports.UI;
using iTextSharp.text.pdf;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using OpenAccount.Bl.Reports;
using OpenAccount.BlInterface.PersonData;
using OpenAccount.BlInterface.Requests;
using OpenAccount.Entities.PersonData;
using OpenAccount.Entities.Publics;
using OpenAccount.Entities.Publics.Exceptions;
using OpenAccount.Entities.Publics.SettingDto;
using OpenAccount.Entities.Requests;
using OpenAccount.Publics;
using OpenAccount.RepositoryInterface.Requests;
using System.Net.Mime;
using System.Security.Cryptography.X509Certificates;

namespace OpenAccount.Bl.Requests
{
	/// <summary>
	/// امضای دیجیتال هر درخواست
	/// </summary>
	internal sealed class RequestDigitalSignatureBl : OpenAccountChainedBl<RequestDigitalSignature, IRequestDigitalSignatureRepository, Guid>, IRequestDigitalSignatureBl
	{
		public RequestDigitalSignatureBl(
			IRequestDigitalSignatureRepository logicRepository,
			IRequestFacilityInqueryBl facilityInqueryBl,
			IRequestChequeInqueryBl chequeInqueryBl,
			IRequestCardBl preRequest,
			IHttpContextAccessor accessor,
			IOptions<DssSettingDto> dssSetting,
			IOptions<MinIoSettingDto> minioSetting,
			IRequestBl requestBl,
			IRealPersonBl realPersonBl,
			IRequestStateLogBl requestLog) : base(logicRepository, preRequest, RequestStateType.DigitalSignature, accessor, requestLog)
		{
			DssSetting = dssSetting.Value;
			MinioSetting = minioSetting.Value;
			FacilityInqueryBl = facilityInqueryBl;
			ChequeInqueryBl = chequeInqueryBl;
			RequestBl = requestBl;
			RealPersonBl = realPersonBl;
		}

		private readonly IRequestBl RequestBl;
		private readonly DssSettingDto DssSetting;
		private readonly MinIoSettingDto MinioSetting;
		private readonly IRequestFacilityInqueryBl FacilityInqueryBl;
		private readonly IRequestChequeInqueryBl ChequeInqueryBl;
		private readonly IRealPersonBl RealPersonBl;

		/// <summary>
		/// فایل تعهدنامه را برمی گرداند
		/// </summary>
		/// <param name="requestId"></param>
		/// <returns></returns>
		public async Task<byte[]> GeneratePdf(Guid requestId)
		{
			//درخواست را بده
			var request = await RequestBl.GetWithLogPersonInfo(requestId);
			if (request == null || request.Person == null || request.Person.Addresses == null || !request.Person.Addresses.Any() ||
				request.Person is not RealPerson ||
				((RealPerson)request.Person).RealPersonInfos == null || !((RealPerson)request.Person).RealPersonInfos.Any())
				throw StException.DataNotFound("شناسه ی درخواست نامعتبر می باشد");

			var address = request.Person.Addresses.FirstOrDefault();
			if (address == null)
				throw StException.DataNotFound("آدرس");

			var personInfo = ((RealPerson)request.Person).RealPersonInfos.FirstOrDefault();
			if (personInfo == null || personInfo.Job == null || personInfo.Education == null)
				throw StException.DataNotFound("مشخصات فردی");

			var r = new AccountCreationContract();
			r.DataSource = new ObjectDataSource()
			{
				Name = "contractData",
				DataSource = new ContractReportDto()
				{
					ContractId = request.Id.ToString(),
					ContractUrl = "https://open-account.tejarat-bank.ir/contracts/" + request.Id,
					IsGharzolhasaneh = request.AccountType == Entities.Accounts.AccountType.LoanAccount,
					IsKootahModat = request.AccountType == Entities.Accounts.AccountType.ShortTermAccount,
					BranchCode = "1",
					BranchName = "مجازی",
					PersianDateTime = CastUtils.DateTimeToFarsi(request.RequestStateLogs.Where(x => x.RequestState == RequestStateType.Start).First().SysDate).ToString("yyyy/MM/dd"), //new PersianDateTime(request.CreatedOn).ToString("yyyy/MM/dd"),
					PersonalInformation = new PersonalInformationDto()
					{
						NationalCode = request.Person.NationalCode,
						IsFemale = !((RealPerson)request.Person).IsMale,
						IsMale = ((RealPerson)request.Person).IsMale,
						FatherName = ((RealPerson)request.Person).FatherName,
						Address = address.Description,
						PostalCode = address.PostalCode,
						FirstNameEnglish = request.Person.LatinName,
						LastNameEnglish = ((RealPerson)request.Person).LatinFamily,
						FirstNameEnglishUpper = request.Person.LatinName.ToUpper(),
						LastNameEnglishUpper = ((RealPerson)request.Person).LatinFamily.ToUpper(),
						MobileNumber = address.MobileNumber ?? string.Empty,
						BirthDatePersian = CastUtils.DateTimeToFarsi(request.Person.Date).ToString("yyyy/MM/dd"),
						BirthCertificateNumber = personInfo.SocialIdentityNumber.ToString(),
						BirthCertificateSeries = personInfo.SocialIdentityExtensionSeries + " - " + personInfo.SocialIdentitySeries,
						BirthCertificateIssuePlace = "",
						FirstNamePersian = request.Person.Name,
						LastNamePersian = ((RealPerson)request.Person).Family,
						FullNamePersian = $"{request.Person.Name} {((RealPerson)request.Person).Family}",
						FirstNamePersianReversed = new string(request.Person.Name.Take(Math.Min(10, request.Person.Name.Length)).Reverse().ToArray()),
						LastNamePersianReversed = new string(((RealPerson)request.Person).Family.Take(Math.Min(11, ((RealPerson)request.Person).Family.Length)).Reverse().ToArray()),
						Job = personInfo.Job.Name,
						EducationGrade = personInfo.Education.Id.ToString(),
					},
				}
			};
			var reportBand = ((DetailReportBand)r.Bands[BandKind.DetailReport]);
			if (reportBand != null)
				reportBand.DataSource = r.DataSource;
			var ms = new MemoryStream();
			await r.ExportToPdfAsync(ms);
			return ms.ToArray();
		}

		public override RequestStateType GetNextStep() => RequestStateType.UserAccount;

		public override void Validate()
		{
			var result = Get(RequestId).Result ?? throw StException.ChainOfRespLevelViolation(new ValidateExceptionDto(LogicType, "امضای دیجیتال تولید نشده است"));
			if (!result.PdfSignedByBank || !result.PdfGenerated)
				throw StException.ChainOfRespLevelViolation(new ValidateExceptionDto(LogicType, "امضای دیجیتال کامل نشده است"));
			if (!RealPersonBl.IsUserAlive(RequestId).Result)
				throw StException.ChainOfRespLevelViolation(new ValidateExceptionDto(LogicType, "کاربر در قید حیات نمی باشد"));
		}

		public override Task Put(RequestDigitalSignature entity, bool save = true)
		{
			if (!string.IsNullOrEmpty(entity.FileNameInDms))
				_ = GoToNextStep(entity.Request);

			return base.Put(entity, save);
		}

		/// <summary>
		/// گواهی باراف اولیه ی کاربر
		/// </summary>
		/// <param name="dto"></param>
		/// <returns></returns>
		public async Task<string> GetFirstDigest(RequestDigitalSignatureRequestDto dto)
		{
			var request = await RequestBl.Get(RequestId) ?? throw StException.RequestIdNotFound();

			// آیا از پیش گواهی نامه بوسیله ی بانک باراف شده است ؟
			var data = await Get(RequestId);
			if (data != null && data.PdfSignedByBank)
				throw StException.DataDublicate("گواهی نامه بوسیله ی بانک امضاء شده است");

			// آیا از پیش گواهی باراف اولیه ی کاربر تولید شده است ؟
			if (data != null && !string.IsNullOrEmpty(data.FirstDigest))
				return data.FirstDigest;

			StException? stException = null;
			var entity = data ?? new RequestDigitalSignature
			{
				Id = Guid.NewGuid(),// اگر فایل قبلا با خطا ذخیره شده بود باید بروز شود
				Request = request,
				SysDate = DateTime.Now
			};

			entity.TempPdfFile = await GeneratePdf(RequestId);
			File.WriteAllBytes(@"d:\q\first.pdf", entity.TempPdfFile);

			/*// فایل تولید و پر شده میرود تا بانک باراف کند
			var bankSign = await HttpClients.Post<DigitalSignatureResultDto>(
				new HttpClient(),
				DssSetting.MainUrl,
				DssSetting.PdfSign,
				new
				{
					data = entity.TempPdfFile,
					dsProfile = "dsProfile1"
				});

			if (bankSign == null)
				stException = StException.ServiceUnavailable("ارسال امضاء کاربر به بانک");
			else if (string.IsNullOrEmpty(bankSign.Result) || !string.IsNullOrEmpty(bankSign.Error))
				stException = StException.ServiceUnavailable(string.IsNullOrEmpty(bankSign.Error) ? "نتیجه نامشخص در ارسال امضاء کاربر به بانک" : bankSign.Error);
			else
			{
				entity.PdfSignedByBank = true;
				entity.TempPdfFile = Convert.FromBase64String(bankSign.Result);
			}*/

			var pdf = File.ReadAllBytes(@"d:\q\first.pdf");
			//var pdf = entity.TempPdfFile;
			entity.PdfGenerated = pdf != null;
			if (pdf != null)
			{
				var _pdfData = Convert.ToBase64String(pdf);
				var bytes = Convert.FromBase64String(dto.SignerCertificate);
				var cert = new X509Certificate2(bytes);
				var certificationItems = cert.Subject.Split(',');
				var serialNumber = certificationItems.FirstOrDefault(x => x.Contains("SERIALNUMBER"));
				if (serialNumber != null)
				{
					var nationalCode = serialNumber[(serialNumber.IndexOf('=') + 1)..];
					if (nationalCode == UserData.NationalCode)
					{
						// فایل تولید شده میرود برای بانک و یک گواهی برمی گردد
						var result = await HttpClients.Post<DigitalSignatureResultDto>(
							new HttpClient(),
							DssSetting.MainUrl,
							DssSetting.PdfUserSign,
							new
							{
								pdfData = _pdfData,
								signerCertificate = dto.SignerCertificate,
								certificationLevel = 0,
								dateTime = entity.SysDate,
								reason = string.Empty,
								location = string.Empty,
								imageDataUrl = string.Empty,
								page = 1,
								lowerLeftX = 0,
								lowerLeftY = 0,
								upperRightX = 0,
								upperRightY = 0,
								hashAlgorithm = 0,
								signatureFieldName = UserData.NationalCode
							});

						if (result == null)
							stException = StException.ServiceUnavailable("عدم دریافت پاسخ مناسب از سرویس گواهی امضاء");
						else if (string.IsNullOrEmpty(result.Result) || !string.IsNullOrEmpty(result.Error))
							stException = StException.ServiceUnavailable(string.IsNullOrEmpty(result.Error) ? "نتیجه نامشخص در دریافت گواهی نخست" : result.Error);
						else
						{
							entity.RootCertification = dto.SignerCertificate;
							entity.FirstDigest = result.Result;
						}
					}
					else
						stException = StException.AccessDenied("گواهی امضاء برای این کاربر نیست");
				}
				else
					stException = StException.IncorrectData("فرمت امضای دیجیتال اشکال دارد");
			}
			else
				stException = StException.FileNotFound("تولید فایل امضاء دیجیتال کاربر با مشکل مواجه شد");

			if (stException != null)
				entity.RequestDigitalSignatureLogs = new List<RequestDigitalSignatureLog>
				{
					new RequestDigitalSignatureLog
					{
						ErrorMessage = stException.Message,
						RequestDigitalSignature = entity,
						SysDate = DateTime.Now
					}
				};

			if (data == null)
				await Post(entity);
			else
				await Put(entity);

			if (stException != null)
				throw stException;
			return entity.FirstDigest;
		}

		/// <summary>
		/// گواهی باراف پایانی کاربر
		/// </summary>
		/// <param name="dto"></param>
		/// <returns></returns>
		public async Task PostFinalDigest(RequestDigitalSignatureRequestDto dto)
		{
			var request = await RequestBl.Get(RequestId) ?? throw StException.RequestIdNotFound();
			var data = await Get(RequestId);
			// آیا قبلا گواهی اولیه ی باراف دریافت شده ؟
			if (data == null || !data.PdfGenerated || string.IsNullOrEmpty(data.FirstDigest))
				throw StException.DataNotFound("گواهی اولیه ی امضاء تولید نشده است");
			data.Request = request;
			// فایل گواهی باراف خالیست ؟
			if (data.TempPdfFile == null)
				throw StException.FileNotFound("گواهی اولیه ی امضاء تولید نشده است");
			// آیا گواهی باراف از پیش ارسال شده است ؟
			if (!string.IsNullOrEmpty(data.FinalDigest))
				return;

			StException? stException = null;
			var pdf = File.ReadAllBytes(@"d:\q\first.pdf");//
			var pdfData = Convert.ToBase64String(pdf);
			//var pdfData = Convert.ToBase64String(data.TempPdfFile);

			// گواهی و باراف کاربر میرود برای حساب بانکی مشتری
			var result = await HttpClients.Post<DigitalSignatureResultDto>(
				new HttpClient(),
				DssSetting.MainUrl,
				DssSetting.PdfPutUserSign,
				new
				{
					DateTime = data.SysDate,
					SignatureFieldName = UserData.NationalCode,
					SignerCertificate = data.RootCertification,
					PdfData = pdfData,
					Signature = dto.SignerCertificate,
					CertificationLevel = 0,
					Reason = string.Empty,
					Location = string.Empty,
					ImageDataUrl = string.Empty,
					Page = 1,
					LowerLeftX = 0,
					LowerLeftY = 0,
					UpperRightX = 0,
					UpperRightY = 0,
					HashAlgorithm = 0
				});

			if (result == null)
				stException = StException.ServiceUnavailable("عدم دریافت پاسخ مناسب از سرویس گواهی امضاء");
			else if (string.IsNullOrEmpty(result.Result) || !string.IsNullOrEmpty(result.Error))
				stException = StException.ServiceUnavailable(string.IsNullOrEmpty(result.Error) ? "نتیجه نامشخص در دریافت گواهی نهایی" : result.Error);
			else
			{//
				File.WriteAllBytes(@"d:\q\last.pdf", Convert.FromBase64String(result.Result));//
				data.FinalDigest = result.Result ?? string.Empty;
				pdf = File.ReadAllBytes(@"d:\q\last.pdf");//
			}//
			if (stException == null) // فایل تولید و پر شده میرود تا بانک باراف کند
			{
				var bytes = pdf; //Convert.FromBase64String(result.Result);
				var ms = new MemoryStream(bytes);
				ms.Seek(0, SeekOrigin.Begin);
				var reader = new PdfReader(ms.ToArray());
				var ms1 = new MemoryStream();
				var stamper = new PdfStamper(reader, ms1, '7');
				stamper.Writer.CloseStream = false;
				stamper.Close();
				ms1.Seek(0, SeekOrigin.Begin);

				var bankSign = await HttpClients.Post<DigitalSignatureResultDto>(new HttpClient(), DssSetting.MainUrl, DssSetting.PdfSign,
				new
				{
					data = Convert.ToBase64String(ms1.ToArray()),
					dsProfile = "dsProfile1"
				});

				if (bankSign == null)
					stException = StException.ServiceUnavailable("عدم دریافت پاسخ مناسب از سرویس گواهی امضاء بانک");
				else if (string.IsNullOrEmpty(bankSign.Result) || !string.IsNullOrEmpty(bankSign.Error))
					stException = StException.ServiceUnavailable(string.IsNullOrEmpty(bankSign.Error) ? "نتیجه نامشخص در ارسال امضاء کاربر به بانک" : bankSign.Error);
				else
				{
					data.PdfSignedByBank = true;
					File.WriteAllBytes(@"d:\q\bank.pdf", Convert.FromBase64String(bankSign.Result));//
					data.TempPdfFile = Convert.FromBase64String(bankSign.Result);
				}
			}
			if (stException != null)
				data.RequestDigitalSignatureLogs = new List<RequestDigitalSignatureLog>
				{
					new RequestDigitalSignatureLog
					{
						Id = Guid.NewGuid(),
						ErrorMessage = stException.HttpResult.Message,
						RequestDigitalSignature = data,
						SysDate = DateTime.Now,
						RequestDigitalSignatureId = data.Id
					}
				};
			else
				data.SignGeneratedByApp = dto.SignerCertificate;
			await Put(data);

			if (stException != null)
				throw stException;
		}

		/// <summary>
		/// ارسال فایل به سرویس مدیریت فایل
		/// </summary>
		/// <param name="formFile"></param>
		/// <returns></returns>
		public async Task PostToMinIo(ByteArrayContent formFile)
		{
			var data = await Get(RequestId) ?? throw StException.DataNotFound("گواهی امضاء موجود نمی باشد");
			var request = await RequestBl.Get(RequestId) ?? throw StException.RequestIdNotFound();
			data.Request = request;

			var result = await HttpClients.PostMinIo<MinIoReturnValue>(
				MinioSetting.MainUrl,
				MinioSetting.Upload,
				MinioSetting.Id,
				formFile,
				MediaTypeNames.Application.Pdf);

			data.FileNameInDms = result.FileName;
			data.RemoveUnusableData();
			await Put(data);
		}

		/// <summary>
		/// ارسال گواهی به بانک برای پاراف
		/// </summary>
		/// <param name="requestId"></param>
		/// <returns></returns>
		public async Task PostToBank(Guid requestId)
		{
			var data = await Get(requestId);
			// آیا قبلا گواهی اولیه ی باراف دریافت شده ؟
			if (data == null || !data.PdfGenerated || string.IsNullOrEmpty(data.FirstDigest))
				throw StException.DataNotFound("گواهی امضاء");

			// فایل گواهی باراف خالیست ؟
			if (string.IsNullOrEmpty(data.SignGeneratedByApp))
				throw StException.FileNotFound("گواهی امضاء");
			// آیا گواهی باراف از پیش ارسال شده است ؟
			if (string.IsNullOrEmpty(data.FinalDigest))
				throw StException.FileNotFound("امضاء مشتری");
			// قبلا فایل را بانک امضاء کرده است
			if (data.PdfSignedByBank /*&& data.TempPdfFile != null*/)//اگر خطای بانک را محمودی درست کرد این فرایند باز شود
				return;

			StException? stException = null;
			var pdfData = data.TempPdfFile; //data.FinalDigest; //اگر خطای بانک را محمودی درست کرد این فرایند باز شود
											// فایل تولید و پر شده میرود تا بانک باراف کند
			var bankSign = await HttpClients.Post<DigitalSignatureResultDto>(
				new HttpClient(),
				DssSetting.MainUrl,
				DssSetting.PdfSign,
				new
				{
					data = pdfData,
					dsProfile = "dsProfile1"
				});

			if (bankSign == null)
				stException = StException.ServiceUnavailable("ارسال امضاء کاربر به بانک");
			else if (string.IsNullOrEmpty(bankSign.Result) || !string.IsNullOrEmpty(bankSign.Error))
				stException = StException.ServiceUnavailable(string.IsNullOrEmpty(bankSign.Error) ? "نتیجه نامشخص" : bankSign.Error);
			else
			{
				data.PdfSignedByBank = true;
				data.TempPdfFile = Convert.FromBase64String(bankSign.Result);
			}
			if (stException != null)
				data.RequestDigitalSignatureLogs = new List<RequestDigitalSignatureLog>
				{
					new RequestDigitalSignatureLog
					{
						ErrorMessage = stException.Message,
						RequestDigitalSignature = data,
						SysDate = DateTime.Now
					}
				};

			await Put(data);

			if (stException != null)
				throw stException;
		}

		/// <summary>
		/// گواهی پاراف شده ی بانک را برمی گرداند
		/// </summary>
		/// <param name="requestId"></param>
		/// <returns></returns>
		public async Task<byte[]> GetPdfSignedByBank(Guid requestId)
		{
			var data = await Get(requestId) ?? throw StException.DataNotFound("گواهی امضاء");
			// قبلا فایل را بانک امضاء کرده است
			if (!data.PdfSignedByBank || data.TempPdfFile == null)
				throw StException.DataNotFound("گواهی امضاء شده ی بانک");

			return data.TempPdfFile;
		}

		/// <summary>
		/// آدرس فیزیکی گواهی
		/// </summary>
		/// <param name="requestId"></param>
		/// <returns>آدرس</returns>
		public async Task<string> GetFileAddress(Guid requestId)
		{
			var data = await Get(requestId) ?? throw StException.KeyNotFound("اطلاعات امضاء یافت نشد");

			if (!data.PdfSignedByBank)
				throw StException.DataNotFound("گواهی امضاء شده ی بانک موجود نمی باشد");
			if (string.IsNullOrEmpty(data.FileNameInDms))
				throw StException.DataNotFound("گواهی امضاء باگذاری نشده است");

			var result = await HttpClients.Get<MinIoReturnAddress>(new HttpClient(), string.Empty,
				string.Format($"{MinioSetting.MainUrl}{MinioSetting.DownloadPath}", data.FileNameInDms, MinioSetting.Id));

			return result.FileAddress;
		}

		/// <summary>
		/// وضعیت گواهی بارگذاری شده
		/// </summary>
		/// <param name="requestId"></param>
		/// <returns></returns>
		/// <exception cref="NotImplementedException"></exception>
		public async Task<StatusOfRequestDigitalSignature> GetProcessStatus(Guid requestId)
		{
			var data = await Get(requestId) ?? throw StException.KeyNotFound("اطلاعات امضاء یافت نشد");
			return new StatusOfRequestDigitalSignature
			{
				FileNameInDms = data.FileNameInDms,
				PdfGenerated = data.PdfGenerated,
				PdfSignedByBank = data.PdfSignedByBank
			};
		}

		/// <summary>
		/// وضعیت استعلام چک و تسهیلات را کنترل می کند که منقضی نشده باشد
		/// </summary>
		/// <param name="requestId"></param>
		/// <exception cref="StException.InquiryNoAcceptable"></exception>
		/// <returns></returns>
		public async Task GetInqueryExpireState(Guid requestId)
		{   // آخرین استعلام تسهیلات یک درخواست را برمی گرداند
			var facility = await FacilityInqueryBl.GetLastInquiry(requestId);
			if (facility == null || !facility.ActionCodeIsValid || facility.SysDate < DateTime.Now.AddHours(-24))
				throw StException.InquiryNotAcceptable();

			var cheque = await ChequeInqueryBl.GetLastInquiry(requestId);
			if (cheque == null || !cheque.ActionCodeOk || cheque.SysDate < DateTime.Now.AddHours(-24) || cheque.SamatBouncedChequeItems.Any())
				throw StException.InquiryNotAcceptable();
		}

		/// <summary>
		/// اطلاعات پرسنلی مورد نیاز امضای دیجیتال
		/// </summary>
		/// <param name="personId"></param>
		/// <returns></returns>
		public async Task<RealPersonForDSignDto?> GetPersonNeededData(Guid personId) => await LogicRepository.GetPersonNeededData(personId);
	}
}