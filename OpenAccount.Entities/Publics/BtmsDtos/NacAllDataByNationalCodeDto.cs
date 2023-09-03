namespace OpenAccount.Entities.Publics.BtmsDtos
{
    public sealed class NacAllDataByNationalCodeDto
    {
        public AccountDto Account { get; set; } = new();
        public RealPersonDto RealPerson { get; set; } = new();
    }

    public class RealPersonDto
    {
        public string rowNo { get; set; } = string.Empty;
        public string idNo { get; set; } = string.Empty;
        public string birthDate { get; set; } = string.Empty;
        public string locationCode { get; set; } = string.Empty;
        public string nationalCode { get; set; } = string.Empty;
        public string sexType { get; set; } = string.Empty;
        public string name { get; set; } = string.Empty;
        public string family { get; set; } = string.Empty;
        public string fatherName { get; set; } = string.Empty;
        public string officeCode { get; set; } = string.Empty;
        public string city { get; set; } = string.Empty;
        public string tel { get; set; } = string.Empty;
        public string postalCode { get; set; } = string.Empty;
        public string address { get; set; } = string.Empty;
        public string fax { get; set; } = string.Empty;
        public string signCode { get; set; } = string.Empty;
        public string sharePercentCode { get; set; } = string.Empty;
        public string manageExpDate { get; set; } = string.Empty;
        public string seriX { get; set; } = string.Empty;
        public string seriI { get; set; } = string.Empty;
        public string serial { get; set; } = string.Empty;
        public string registerValidity { get; set; } = string.Empty;
        public string relationCode { get; set; } = string.Empty;
        public string sharesPercent { get; set; } = string.Empty;
        public string updateDate { get; set; } = string.Empty;
        public int isForeign { get; set; }
        public int isExists { get; set; }
    }

    public class AccountDto
    {
        public string accNo { get; set; } = string.Empty;
        public string accType { get; set; } = string.Empty;
        public string accGrp { get; set; } = string.Empty;
        public string billType { get; set; } = string.Empty;
        public string billControl { get; set; } = string.Empty;
        public string cBK { get; set; } = string.Empty;
        public string cBI { get; set; } = string.Empty;
        public string loanUse { get; set; } = string.Empty;
        public string combination { get; set; } = string.Empty;
        public string currencyCode { get; set; } = string.Empty;
        public string ast { get; set; } = string.Empty;
        public string formNo { get; set; } = string.Empty;
        public string accReagent { get; set; } = string.Empty;
        public string terminalNo { get; set; } = string.Empty;
        public string operatorNo { get; set; } = string.Empty;
        public string exporterBranchCode { get; set; } = string.Empty;
        public string isicCode { get; set; } = string.Empty;
        public string branchCode { get; set; } = string.Empty;
        public string counterCode { get; set; } = string.Empty;
        public string openingDate { get; set; } = string.Empty;
        public string operationCode { get; set; } = string.Empty;
    }
}