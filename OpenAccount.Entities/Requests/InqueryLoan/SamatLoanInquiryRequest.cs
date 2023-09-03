using Microsoft.EntityFrameworkCore;
using OpenAccount.Entities.Infrastructure;
using OpenAccount.Publics;
using System.ComponentModel;

namespace OpenAccount.Entities.Requests.InqueryLoan
{
	/// <summary>
	/// استعلام تسهیلات سمات
	/// </summary>
	[Description("استعلام تسهیلات سمات")]
	public sealed class SamatLoanInquiryRequest : BaseEntity<Guid>, ISamatLoanInquiryRequest
    {
		/// <summary>
		/// درخواست افتتاح حساب
		/// </summary>
		[Comment("درخواست افتتاح حساب")]
		public Guid RequestId { get; set; }
		public Request Request { get; set; } = new();
		
        public bool HasError { get; set; }

        /// <summary>
        /// Response code of service.
        /// </summary>
        [Comment("Response code of service")]
        public string ActionCode { get; set; } = string.Empty;

        public bool ActionCodeOk => ActionCode == "00000";

		/// <summary>
		/// Inquiry responses ok.
		/// EstelamAsliRows.AmMashkuk+AmSarResid+AmMoavagh == 0?
		/// </summary>
		public bool ActionCodeIsValid
		{
			get
			{
                if (ActionCodeOk)
                {
                    if (EstelamAsliRows != null)
                        return !EstelamAsliRows.Any(x => CastUtils.StrToLong(x.AmMashkuk) > 0 || CastUtils.StrToLong(x.AmSarResid) > 0 || CastUtils.StrToLong(x.AmMoavagh) > 0);
                    return true;
                }
                return false;
			}
		}

		/// <summary>
		/// خطای سیستمی
		/// </summary>
		[Comment("خطای سیستمی")]
		public string ErrorExMessage { get; set; } = string.Empty;

		public string Country { get; set; } = string.Empty;

        public string CustomerType { get; set; } = string.Empty;

        public string DateEstlm { get; set; } = string.Empty;

        public List<SamatLoanInquiryRequestItem> EstelamAsliRows { get; set; } = new();

        public string NationalCd { get; set; } = string.Empty;

        public string ShenaseEstlm { get; set; } = string.Empty;

        public string ShenaseRes { get; set; } = string.Empty;

        public int SumAmBedehiKol { get; set; }

        public int SumAmTahod { get; set; }

        public int SumAmBenefit { get; set; }

        public int SumAmEltezam { get; set; }

        public int SumAmDirkard { get; set; }

        public int SumAmMashkuk { get; set; }

        public int SumAmMoavagh { get; set; }

        public int SumAmOriginal { get; set; }

        public int SumAmSarResid { get; set; }

        public int SumAmSukht { get; set; }

        public string Fname { get; set; } = string.Empty;

        public string Lname { get; set; } = string.Empty;

        public DateTime SysDate { get; set; }
    }
}