using Microsoft.AspNetCore.Mvc;
using OpenAccount.Entities.Publics;
using OpenAccount.Publics;

namespace OpenAccount.Api.Infrastructure
{
	/// <summary>
	/// Base controller with user data in header.
	/// </summary>
	[Route("api/[controller]")]
	[Produces("application/json")]
	[ApiController]
	public abstract class BaseController : ControllerBase
	{
		/// <summary>
		/// Access to appConfig
		/// </summary>
		protected readonly IConfiguration Configuration;
		/// <summary>
		/// Access to header.
		/// </summary>
		protected readonly IHttpContextAccessor Accessor;
		/// <summary>
		/// User data from header.
		/// </summary>
		protected readonly UserData UserData = new();
		/// <summary>
		/// For catch exceptions of Post/Put/Del methods.
		/// </summary>
		/// <returns></returns>
		protected delegate Task ActionDelegate();
		/// <summary>
		/// For catch exceptions of Get methods.
		/// </summary>
		/// <returns></returns>
		protected delegate Task<object> GetDelegate();

		/// <summary>
		/// Read header value.
		/// </summary>
		/// <param name="key"></param>
		/// <returns></returns>
		protected string GetHeader(string key) => Accessor.HttpContext != null ? Accessor.HttpContext.Request.Headers[key].ToString() ?? string.Empty : string.Empty;

		/// <summary>
		/// Do u want to know your real exception before catch ?
		/// Override this method and watch it.
		/// Internal use in IActionFilter.
		/// </summary>
		/// <param name="param"></param>
		/// <returns></returns>
		[HttpPost("Nothing")]
		[ApiExplorerSettings(IgnoreApi = true)]
		public abstract Task HandledExceptions([FromBody] HandleExceptionParam param);

		/// <summary>
		/// Do u want to manage chain and catch exception in IActionFilter ?
		/// Override this method.
		/// Internal use in IActionFilter.
		/// </summary>
		/// <param name="param"></param>
		/// <returns></returns>
		[HttpPost("NothingChain")]
		[ApiExplorerSettings(IgnoreApi = true)]
		public virtual void ManageChain() { }

		/// <summary>
		/// Ctor
		/// </summary>
		/// <param name="configuration"></param>
		/// <param name="accessor">to access header.</param>
		/// <param name="ThrowUserData">if UserData.UserId, NationalCode not exists, throw exception</param>
		protected BaseController(IConfiguration configuration, IHttpContextAccessor accessor, bool ThrowUserData = true)
		{
			Configuration = configuration;
			Accessor = accessor;

			if (accessor.HttpContext != null)
			{
				UserData.ClientId = GetHeader("clientId");
				_ = Guid.TryParse(GetHeader("userId"), out Guid userId);
				UserData.UserId = userId;
				UserData.Roles = GetHeader("roles");
				UserData.UserName = GetHeader("userName");
				_ = Guid.TryParse(GetHeader("organId"), out Guid OrganizationId);
				UserData.OrganizationId = OrganizationId;
				UserData.Ip = GetHeader("X-Forwarded-For");
				_ = Guid.TryParse(GetHeader("referenceNumber"), out Guid ReferenceNumber);
				UserData.ReferenceNumber = ReferenceNumber;
				UserData.Channel = GetHeader("channel");
				//from client
				UserData.TraceNumber = GetHeader("traceNumber");
				UserData.DeviceId = GetHeader("deviceId");
				UserData.NationalCode = GetHeader("nationalCode");
			}
			if (ThrowUserData && (Guid.Empty == UserData.UserId || !Utility.ValidateNationalCode(UserData.NationalCode)))
				throw StException.ArgumentNull("اطلاعات کاربر");
		}

		/// <summary>
		/// Reads value from appConfig
		/// </summary>
		/// <param name="key">Key of configuration.</param>
		/// <returns></returns>
		protected string CnfgValue(string key)
		{
			if (Configuration != null)
			{
				var result = Configuration[key];
				if (result != null)
					return result;
			}
			return string.Empty;
		}

		/// <summary>
		/// Reads byte value from appConfig
		/// </summary>
		/// <param name="key">Key of configuration.</param>
		/// <returns></returns>
		protected byte CnfgValueByte(string key) => byte.Parse(CnfgValue(key));

		/// <summary>
		/// Reads byte value from appConfig
		/// </summary>
		/// <param name="key">Key of configuration.</param>
		/// <returns></returns>
		protected long CnfgValueLong(string key) => long.Parse(CnfgValue(key));

		/// <summary>
		/// Reads int value from appConfig
		/// </summary>
		/// <param name="key">Key of configuration.</param>
		/// <returns></returns>
		protected int CnfgValueInt(string key) => int.Parse(CnfgValue(key));

		/// <summary>
		/// Catches all exceptions in persist methods.
		/// </summary>
		/// <param name="actionDelegate"></param>
		/// <returns>HttpStResult result.</returns>
		protected async Task<IActionResult> DoAction(ActionDelegate actionDelegate)
		{
			//try
			//{
				await actionDelegate();
				return Ok();
			//}
			//catch (Exception e)
			//{
			//	var result = CatchedException(e);
			//	try
			//	{
			//		await SeeYourExceptionBeforeHandle(result, e);
			//	}
			//	catch { }
			//	throw new StException(result);
			//}
		}

		/*/// <summary>
		/// Do u want to know your real exception before catch ?
		/// Override this method and watch it.
		/// </summary>
		/// <param name="result"></param>
		/// <param name="e"></param>
		/// <returns>Task.CompletedTask</returns>
		private Task SeeYourExceptionBeforeHandle(HttpStResult result, Exception e) => Task.CompletedTask;

		/// <summary>
		/// Exception to ObjectResult.
		/// </summary>
		/// <param name="e"></param>
		/// <returns></returns>
		protected HttpStResult CatchedException(Exception e)
		{
			WriteExceptionInConsole(e);
			return e switch
			{
				StException => ((StException)e).HttpResult,
				ArgumentNullException => HttpStResult.ArgumentNull(e.Message),
				ArgumentOutOfRangeException => HttpStResult.ArgumentOutOfRange(e.Message),
				DivideByZeroException => HttpStResult.ManagedError(StMessages.DivideByZeroMessage),
				NullReferenceException => HttpStResult.ArgumentNull(e.Message),
				StackOverflowException => HttpStResult.ManagedError(StMessages.StackOverflowMessage),
				EndOfStreamException => HttpStResult.ManagedError(StMessages.EndOfStreamMessage),
				FileNotFoundException => HttpStResult.FileNotFound(((FileNotFoundException)e).FileName),
				FileLoadException => HttpStResult.ManagedError($"{StMessages.FileLoadMessage} : '{((FileNotFoundException)e).FileName}'"),
				KeyNotFoundException => HttpStResult.KeyNotFound(),
				DirectoryNotFoundException => HttpStResult.DirectoryNotFound(),
				InvalidOperationException => HttpStResult.HttpCallInvalidOperationException(),
				HttpRequestException => HttpStResult.HttpCallRequestException(),
				TaskCanceledException => HttpStResult.HttpCallTaskCanceledException(),
				UriFormatException => HttpStResult.HttpCallUriFormatException(),
				_ => HttpStResult.ManagedError(e.Message),
			};
		}*/

		/// <summary>
		/// Catches all exceptions in Get methods.
		/// </summary>
		/// <param name="getDelegate"></param>
		/// <returns>HttpStResult result.</returns>
		protected async Task<IActionResult> GetAction(GetDelegate getDelegate)
		{
			//try
			//{
				var result = await getDelegate();
				return Ok(result);
			//}
			//catch (Exception e)
			//{
			//	var result = CatchedException(e);
			//	try
			//	{
			//		await SeeYourExceptionBeforeHandle(result, e);
			//	}
			//	catch { }
			//	throw new StException(result);
			//}
		}

		/*private static void WriteExceptionInConsole(Exception e)
		{
			Console.ForegroundColor = ConsoleColor.DarkRed;
			Console.WriteLine($"Error Occurred on : {DateTime.Now:d}");
			Console.WriteLine(new string('-', 100));
			Console.ForegroundColor = ConsoleColor.White;
			Console.WriteLine(e.StackTrace);
			Console.ForegroundColor = ConsoleColor.DarkYellow;
			do
			{
				Console.WriteLine(new string('-', 100));
				Console.OutputEncoding = System.Text.Encoding.UTF8;
				Console.WriteLine(e.Message);
				if (e.InnerException == null)
					break;
				else
					e = e.InnerException;
			} while (e != null);

			Console.WriteLine(new string('#', 100));
			Console.ForegroundColor = ConsoleColor.White;
		}*/
	}

	public sealed class HandleExceptionParam
	{
		public HttpStResult? Result { get; set; }
		public Exception? Exception { get; set; }

		public string ExceptionMessage => Exception == null ? string.Empty : Exception.Message;
	}
}