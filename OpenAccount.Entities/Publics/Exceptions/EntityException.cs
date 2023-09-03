using Microsoft.EntityFrameworkCore;
using OpenAccount.Entities.Infrastructure;
using OpenAccount.Entities.Requests;
using OpenAccount.Publics;
using System.ComponentModel;

namespace OpenAccount.Entities.Publics.Exceptions
{
	/// <summary>
	/// خطا های موجودیت
	/// </summary>
	[Description("خطا های موجودیت")]
	public class EntityException : BaseEntity<Guid>
	{
		/// <summary>
		/// پیام پارسی
		/// </summary>
		[Comment("پیام پارسی")]
		public string Message { get; set; } = string.Empty;

		/// <summary>
		/// Exception status code
		/// </summary>
		[Comment("Status code")]
		public int StatusCode { get; set; }

        /// <summary>
        /// Inner exception
        /// </summary>
        [Comment("Inner exception")]
        public string Exception { get; set; } = string.Empty;

		public void SetException(Exception? exception)
		{
			if (exception == null)
				return; 
			
			var msg = exception.Message;
			while(exception.InnerException != null)
			{
				exception = exception.InnerException;
				msg += $" - {exception.Message}";
			}
			Exception = msg;
		}

		public void SetMessageAndStatusCode(HttpStResult? result)
		{
			if (result == null)
				return;

			Message = result.Message;
			StatusCode = result.StatusCode ?? 0;
		}

		/// <summary>
		/// زمان وقوع خطا
		/// </summary>
		[Comment("زمان وقوع خطا")]
        public DateTime SysDate { get; set; }

		/// <summary>
		/// کاربر
		/// </summary>
		[Comment("کاربر باجت")]
		public Guid UserId { get; set; }
    }

	/// <summary>
	/// خطاهای درخواست
	/// </summary>
	public abstract class RequestException : EntityException
	{
		/// <summary>
		/// درخواست افتتاح حساب
		/// </summary>
		public Request Request { get; set; } = new();

		/// <summary>
		/// درخواست افتتاح حساب
		/// </summary>
		[Comment("درخواست افتتاح حساب")]
		public Guid RequestId { get; set; }
	}
}