namespace OpenAccount.Entities.Publics.BtmsDtos
{
	public sealed class OpenAccountResponseDto
	{
		public OpenAccountMessageResponse Message { get; set; } = new();
		public OpenAccountResultDetail Response { get; set; } = new();
	}

	public sealed class OpenAccountMessageResponse
	{
		public int Code { get; set; }
		public string Text { get; set; } = string.Empty;
	}

	public sealed class OpenAccountResultDetail
	{
		public string AccountNumber { get; set; } = string.Empty;
	}

	public sealed class BtmsOpenAccountRequest
	{
		public string Token { get; set; }
		public BtmsAccount Account { get; set; }
		public BtmsAccountNasimInfo AccountNasimInfo { get; set; }
		public BtmsLegalPerson? LegalPerson { get; set; }
		public BtmsRealPerson RealPerson { get; set; }
		public BtmsSignatory Signatory { get; set; }
	}

	public class BtmsSignatory
	{
        public List<SignatoryList>? SignatoryList { get; set; }
	}

	public class SignatoryList
	{
		public string Address { get; set; }
		public string BirthDate { get; set; }
		public string City { get; set; }
		public int EducationCode { get; set; }
		public string Email { get; set; }
		public string FatherName { get; set; }
		public string Fax { get; set; }
		public string FirstName { get; set; }
		public int InComeCode { get; set; }
		public int IsForeign { get; set; }
		public int JobCode { get; set; }
		public int JobTypeCode { get; set; }
		public string LastName { get; set; }
		public string ManageExpirationDate { get; set; }
		public string NationalCode { get; set; }
		public int OperatorNumber { get; set; }
		public string PhoneNumber { get; set; }
		public string PostalCode { get; set; }
		public int RegisterLocationCode { get; set; }
		public int RegisterLocationOfficeCode { get; set; }
		public string RegisterValidity { get; set; }
		public int RelationCode { get; set; }
		public string RowNumber { get; set; }
		public int SexTypeCode { get; set; }
		public int SharePercentCode { get; set; }
		public string SharesPercent { get; set; }
		public int SignStatusCode { get; set; }
		public string SocialIdentityLetterSeries { get; set; }
		public int SocialIdentityNumber { get; set; }
		public int SocialIdentityNumericSeries { get; set; }
		public int SocialIdentitySeries { get; set; }
		public string TerminalNumber { get; set; }
		public string UpdateDate { get; set; }
	}

	public class BtmsRealPerson
	{
		public string Address { get; set; }
		public string BirthDate { get; set; }
		public string City { get; set; }
		public int EducationCode { get; set; }
		public string Email { get; set; }
		public string FatherName { get; set; }
		public string Fax { get; set; }
		public string FirstName { get; set; }
		public int InComeCode { get; set; }
		public int IsForeign { get; set; }
		public int JobCode { get; set; }
		public int JobTypeCode { get; set; }
		public string LastName { get; set; }
		public string ManageExpirationDate { get; set; }
		public string NationalCode { get; set; }
		public int OperatorNumber { get; set; }
		public string PhoneNumber { get; set; }
		public string PostalCode { get; set; }
		public int RegisterLocationCode { get; set; }
		public int RegisterLocationOfficeCode { get; set; }
		public string RegisterValidity { get; set; }
		public int RelationCode { get; set; }
		public int RowNumber { get; set; }
		public int SexTypeCode { get; set; }
		public int SharePercentCode { get; set; }
		public int SharesPercent { get; set; }
		public int SignStatusCode { get; set; }
		public string SocialIdentityLetterSeries { get; set; }
		public int SocialIdentityNumber { get; set; }
		public int SocialIdentityNumericSeries { get; set; }
		public int SocialIdentitySeries { get; set; }
		public string TerminalNumber { get; set; }
		public string UpdateDate { get; set; }
		public string EnglishName { get; set; }
		public string EnglishFamily { get; set; }
		public string Province { get; set; }
		public string Language { get; set; }
		public string Mobile { get; set; }
	}

	public class BtmsLegalPerson
	{
		public int ActivityTypeCode { get; set; }
		public string Address { get; set; }
		public string City { get; set; }
		public string CompanyName { get; set; }
		public int CompanyTypeCode { get; set; }
		public string Email { get; set; }
		public string Fax { get; set; }
		public int InComeCode { get; set; }
		public int IsForeign { get; set; }
		public int JobCode { get; set; }
		public int JobTypeCode { get; set; }
		public string ManageExpirationDate { get; set; }
		public string NationalId { get; set; }
		public string OperatorNumber { get; set; }
		public int OrganTypeCode { get; set; }
		public int OwnershipTypeCode { get; set; }
		public string PhoneNumber { get; set; }
		public string PostalCode { get; set; }
		public string RegisterDate { get; set; }
		public int RegisterLocationCode { get; set; }
		public int RegisterNumber { get; set; }
		public int RowNumber { get; set; }
		public string TerminalNumber { get; set; }
		public string UpdateDate { get; set; }
	}

	public class BtmsAccountNasimInfo
	{
		public string AccountOpenerName { get; set; }
		public string WithdrawalType { get; set; }
	}

	public class BtmsAccount
	{
		public string AccountGroup { get; set; }
		public string AccountNumber { get; set; }
		public int AccountTypeCode { get; set; }
		public int Ast { get; set; }
		public int BillControl { get; set; }
		public int BillType { get; set; }
		public string BranchCode { get; set; }
		public Cbi? Cbi { get; set; }
		public Cbk? Cbk { get; set; }
		public int Centruy { get; set; }
		public int Combination { get; set; }
		public string CounterCode { get; set; }
		public int CurrencyCode { get; set; }
		public string ExporterBranchCode { get; set; }
		public string Faragir { get; set; }
		public string FaragirAccountNumber { get; set; }
		public string FormNumber { get; set; }
		public string IsicCode { get; set; }
		public string LoanUse { get; set; }
		public int OperatorNumber { get; set; }
		public int ReagentAccount { get; set; }
		public bool SendToNasim { get; set; }
		public int ShareCode { get; set; }
	}

	public class Cbk
	{
	}

	public class Cbi
	{
	}
}