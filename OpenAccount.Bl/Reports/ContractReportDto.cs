namespace OpenAccount.Bl.Reports
{
	internal sealed class ContractReportDto
	{
		public string PersianDateTime { get; set; } = string.Empty;
		public string BranchCode { get; set; } = string.Empty;
		public string BranchName { get; set; } = string.Empty;
		public bool IsGharzolhasaneh { get; set; }
		public bool IsJari { get; set; }
		public bool IsKootahModat { get; set; }
		public string AccountNumber { get; set; } = string.Empty;
		public string ContractId { get; set; } = string.Empty;
		public PersonalInformationDto PersonalInformation { get; set; } = new();
		public string ContractUrl { get; set; } = string.Empty;
	}

	internal sealed class PersonalInformationDto
	{
		public string NationalCode { get; set; } = string.Empty;
		public string BirthCertificateSeries { get; set; } = string.Empty;
		public string BirthCertificateNumber { get; set; } = string.Empty;
		public string BirthDatePersian { get; set; } = string.Empty;
		public string BirthCertificateIssuePlace { get; set; } = string.Empty;
		public string BirthCertificateIssuePlaceCode { get; set; } = string.Empty;
		public bool IsMale { get; set; }
		public bool IsFemale { get; set; }
		public string FirstNamePersian { get; set; } = string.Empty;
		public string LastNamePersian { get; set; } = string.Empty;
		public string FirstNamePersianReversed { get; set; } = string.Empty;
		public string LastNamePersianReversed { get; set; } = string.Empty;
		public string FirstNameEnglish { get; set; } = string.Empty;
		public string LastNameEnglish { get; set; } = string.Empty;
		public string FatherName { get; set; } = string.Empty;
		public string Address { get; set; } = string.Empty;
		public string PostalCode { get; set; } = string.Empty;
		public string PhoneNumber { get; set; } = string.Empty;
		public string MobileNumber { get; set; } = string.Empty;
		public string EducationGrade { get; set; } = string.Empty;
		public string Job { get; set; } = string.Empty;
		public string IncomeAmount { get; set; } = string.Empty;
		public string Email { get; set; } = string.Empty;
		public string FirstNameEnglishUpper { get; set; } = string.Empty;
		public string LastNameEnglishUpper { get; set; } = string.Empty;
		public string FullNamePersian { get; set; } = string.Empty;
	}
}