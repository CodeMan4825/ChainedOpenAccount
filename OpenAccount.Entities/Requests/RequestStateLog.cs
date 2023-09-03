using Microsoft.EntityFrameworkCore;
using OpenAccount.Entities.Infrastructure;
using System.ComponentModel;

namespace OpenAccount.Entities.Requests
{
	/// <summary>
	/// لاگ تغییر وضعیت درخواست از مرحله ای به مرحله ی دیگر
	/// </summary>
	[Description("لاگ تغییر وضعیت درخواست از مرحله ای به مرحله ی دیگر")]
    public sealed class RequestStateLog : BaseEntity<Guid>
	{
		public RequestStateLog()
		{
		}

		public RequestStateLog(RequestStateType requestState)
		{
			RequestState = requestState;
            SysDate = DateTime.Now;
            Id = Guid.NewGuid();
		}

		/// <summary>
		/// شناسه ی درخواست
		/// </summary>
		[Comment("شناسه ی درخواست")]
        public Guid RequestId { get; set; }
        public Request? Request { get; set; }

        /// <summary>
        /// مرحله
        /// </summary>
        public RequestStateType RequestState { get; set; }

        /// <summary>
        /// زمان ثبت رکورد
        /// </summary>
        [Comment("زمان ثبت رکورد")]
		public DateTime SysDate { get; set; } = DateTime.Now;

        /*/// <summary>
        /// این مرحله با موفقیت به پایان رسید
        /// </summary>
        [Comment("این مرحله با موفقیت به پایان رسید")]
        public bool Passed { get; set; } = false;*/
    }
}