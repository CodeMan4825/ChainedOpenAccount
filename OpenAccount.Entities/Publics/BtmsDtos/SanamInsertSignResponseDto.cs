namespace OpenAccount.Entities.Publics.BtmsDtos
{
	public sealed class SanamInsertSignResponseDto
    {
        public RequestStatus RequestStatus { get; set; } = new();
        public int Code { get; set; }
        public string Message { get; set; } = string.Empty;
    }

    public sealed class RequestStatus
    {
        /// <summary>
        /// کد پیام
        /// </summary>
        public int ReturnCode { get; set; }

        /// <summary>
        /// شرح پیام
        /// </summary>
        public string Description { get; set; } = string.Empty;
    }

    public class InsertSignData
    {
        public UserData UserData { get; set; } = new();

        /// <summary>
        /// شماره حساب
        /// </summary>
        public long AccountNumber { get; set; }

        /// <summary>
        /// شماره ردیف فرد در حساب
        /// ردیف صفر براي تعریف مهر شرکت است واجباری
        /// </summary>
        public int Radif { get; set; }

        /// <summary>
        /// فایل امضا یا مهر فرد
        /// Jpg
        /// </summary>
        public string SignFile { get; set; } = string.Empty;

        /// <summary>
        /// کدملی ، شناسه ملی و یا کد فراگیر براي اتباع
        /// </summary>
        public long NationalCode { get; set; }

        /// <summary>
        /// مهر برجسته
        /// true = دارد
        /// </summary>
        public bool EmbossStamp { get; set; }
    }
}