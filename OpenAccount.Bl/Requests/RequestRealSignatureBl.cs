/*using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using OpenAccount.BlInterface.Requests;
using OpenAccount.Entities.Publics;
using OpenAccount.Entities.Publics.SettingDto;
using OpenAccount.Entities.Requests;
using OpenAccount.Publics;
using OpenAccount.RepositoryInterface.Requests;

namespace OpenAccount.Bl.Requests
{
	/// <summary>
	/// پاراف وافعی
	/// </summary>
	internal sealed class RequestRealSignatureBl : OpenAccountChainedBl<RequestRealSignature, IRequestRealSignatureRepository, Guid>, IRequestRealSignatureBl
	{
		private readonly IRequestBl RequestBl;
		private readonly MinIoSettingDto MinioSetting;

		public RequestRealSignatureBl(
			IRequestRealSignatureRepository logicRepository,
			IRequestDigitalSignatureBl preRequest,
			IHttpContextAccessor accessor,
			IOptions<MinIoSettingDto> minioSetting,
			IRequestBl requestBl,
			IRequestStateLogBl requestLog) : base(logicRepository, preRequest, RequestStateType.RealSignature, accessor, requestLog)
		{
			MinioSetting = minioSetting.Value;
			RequestBl = requestBl;			
		}

		public override RequestStateType GetNextStep() => RequestStateType.UserAccount;

		public override void Validate()
		{
			var data = LogicRepository.AsQuery().Where(x => x.Id == RequestId && x.SignatureArchived && !string.IsNullOrEmpty(x.SignatureFileName));
			if (data == null || !data.Any())
				throw StException.ChainOfRespLevelViolation("امضای مشتری");
		}

		/// <summary>
		/// Uploads sign in MinIo.
		/// </summary>
		/// <param name="sign"></param>
		/// <returns></returns>
		public async Task Upload(RequestRealSignatureDto sign)
		{
			var request = await RequestBl.Get(RequestId);
			if (request == null)
				throw StException.KeyNotFound();

			var id = Guid.NewGuid();
			// پاراف خیس از پیش بارگذاری شده؟
			var entity = await Get(request.Id) ?? new RequestRealSignature
			{
				Id = id,
				SysDate = DateTime.Now
			};
			// بایگانی تصویر پاراف
			var bytes = Convert.FromBase64String(sign.SignatureInBase64);
			//File.WriteAllBytes("App_Data/Contracts/salam.svg", bytes);
			var byteContent = new ByteArrayContent(bytes);
			try
			{
				var result = await HttpClients.PostMinIo<MinIoReturnValue>(
					MinioSetting.MainUrl,
					entity.SignatureArchived ? MinioSetting.ReUpload : MinioSetting.Upload,
					MinioSetting.Id,
					byteContent,
					"image/svg", //MediaTypeNames.Image.Jpeg,
					entity.SignatureFileName);
				entity.SignatureArchived = result != null && !string.IsNullOrEmpty(result.FileName);
				if (result != null && entity.SignatureArchived)
					entity.SignatureFileName = result.FileName;
				else
					throw StException.ServiceUnavailable("خطا در بارگذاری امضاء");
			}
			catch (Exception ex)
			{
				entity.ArchiveError = ex.Message;
			}
			// اگر پاراف بدرستی بایگانی شد
			if (entity.SignatureArchived)
				await GoToNextStep(request); //برو به مرحله ی بعد
			entity.Request = request;
			await (entity.Id == id ? Post(entity) : Put(entity));
		}

		*//*private static byte[] WebpImageToBase64(byte[] webpBytes, float resizeRatio = 1)
		{
			using var inputStream = new MemoryStream(webpBytes);
			using var bitmap = SKBitmap.Decode(inputStream);


			// Resize the bitmap 
			var newWidth = (int)(bitmap.Width * resizeRatio);
			var newHeight = (int)(bitmap.Height * resizeRatio);
			var resizedBitmap = bitmap.Resize(new SKImageInfo(newWidth, newHeight), SKFilterQuality.Medium);

			// Convert the resized bitmap to a JPEG encoded byte array
			using var outputStream = new MemoryStream();
			using var skiaImage = SKImage.FromBitmap(resizedBitmap);
			using var encodedData = skiaImage.Encode(SKEncodedImageFormat.Jpeg, 100);
			encodedData.SaveTo(outputStream);
			return outputStream.ToArray();

			// Convert the JPEG encoded byte array to a base64 string
			//var base64String = Convert.ToBase64String(jpegBytes);
			//return base64String;
		}*//*
	}
}*/