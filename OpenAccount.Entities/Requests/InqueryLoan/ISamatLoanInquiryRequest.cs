namespace OpenAccount.Entities.Requests.InqueryLoan
{
    public interface ISamatLoanInquiryRequest
    {
        string Country { get; set; }

        string CustomerType { get; set; }

        string DateEstlm { get; set; }

        List<SamatLoanInquiryRequestItem> EstelamAsliRows { get; set; }

        string NationalCd { get; set; }

        string ShenaseEstlm { get; set; }

        string ShenaseRes { get; set; }

        int SumAmBedehiKol { get; set; }

        int SumAmTahod { get; set; }

        int SumAmBenefit { get; set; }

        int SumAmEltezam { get; set; }

        int SumAmDirkard { get; set; }

        int SumAmMashkuk { get; set; }

        int SumAmMoavagh { get; set; }

        int SumAmOriginal { get; set; }

        int SumAmSarResid { get; set; }

        int SumAmSukht { get; set; }

        string Fname { get; set; }

        string Lname { get; set; }
    }
}