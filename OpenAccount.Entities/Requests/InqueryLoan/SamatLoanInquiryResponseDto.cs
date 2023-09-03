namespace OpenAccount.Entities.Requests.InqueryLoan
{
    public sealed class SamatLoanInquiryResponseDto
    {
        public object? Errors { get; set; }
        public bool HasError { get; set; }
        public SamatLoanInquiryRequest? ReturnValue { get; set; }
    }

    public sealed record SamatUnAcceptableLoanDto
    {
        public long AmountLated { get; set; }
        public string BankCode { get; set; } = string.Empty;
        public string BranchCode { get; set; } = string.Empty;
        public string BranchName { get; set; } = string.Empty;
        public DateTime LastInstallmentDate { get; set; }
	}
}