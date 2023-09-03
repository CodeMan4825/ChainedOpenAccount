using OpenAccount.Entities.Infrastructure;
using System.ComponentModel;

namespace OpenAccount.Entities.Requests.InqueryLoan
{
	/// <summary>
	/// استعلام تسهیلات سمات ریز
	/// </summary>
	[Description("استعلام تسهیلات سمات ریز")]
    public sealed class SamatLoanInquiryRequestItem : BaseEntity<Guid>
    {
        public Guid SamatLoanInquiryRequestId { get; set; }
        public string AdamSabtSanadEntezami { get; set; } = string.Empty;

        public string AmBedehiKol { get; set; } = string.Empty;

        public string AmBenefit { get; set; } = string.Empty;

        public string AmDirkard { get; set; } = string.Empty;

        public string AmEltezam { get; set; } = string.Empty;

        public string AmMashkuk { get; set; } = string.Empty;

        public string AmMoavagh { get; set; } = string.Empty;

        public string AmOriginal { get; set; } = string.Empty;

        public string AmSarResid { get; set; } = string.Empty;

        public string AmSukht { get; set; } = string.Empty;

        public string AmTahod { get; set; } = string.Empty;

        public string ArzCode { get; set; } = string.Empty;

        public string BankCode { get; set; } = string.Empty;

        public string DasteBandi { get; set; } = string.Empty;

        public string Date { get; set; } = string.Empty;

        public string DateEstehal { get; set; } = string.Empty;

        public string DateSarResid { get; set; } = string.Empty;

        public string Estehal { get; set; } = string.Empty;

        public string HadafAzDaryaft { get; set; } = string.Empty;

        public string MainIdNo { get; set; } = string.Empty;

        public string MainLgId { get; set; } = string.Empty;

        public string PlaceCdMasraf { get; set; } = string.Empty;

        public string RequestNum { get; set; } = string.Empty;

        public string RequstType { get; set; } = string.Empty;

        public string RsrcTamin { get; set; } = string.Empty;

        public string ShobeCode { get; set; } = string.Empty;

        public string ShobeName { get; set; } = string.Empty;

        public string Type { get; set; } = string.Empty;
    }
}