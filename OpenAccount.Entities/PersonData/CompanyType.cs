namespace OpenAccount.Entities.PersonData
{
	/// <summary>
	/// انواع شرکت ها
	/// </summary>
	public enum CompanyType : byte
	{
		/// <summary>
		/// سهامی خاص
		/// </summary>
		SahamiKhas = 0,

		/// <summary>
		/// سهامی عام
		/// </summary>
		SahamiAm = 1,

		/// <summary>
		/// مسئولیت محدود
		/// </summary>
		MasouliatMahdud = 2,

		/// <summary>
		/// نسبی
		/// </summary>
		Nasabi = 3,

		/// <summary>
		/// تضامنی
		/// </summary>
		Tazamoni = 4,

		/// <summary>
		/// مختلط سهامی
		/// </summary>
		SahamiMokhtalet = 5,

		/// <summary>
		/// مختلط غیرسهامی
		/// </summary>
		GheirSahamiMokhtalet = 6,

		/// <summary>
		/// تعاونی تولید
		/// </summary>
		TaavoniTolid = 7,

		/// <summary>
		/// تعاونی مصرف
		/// </summary>
		TaavoniMasraf = 8,

		/// <summary>
		/// تعاونی توزیعی
		/// </summary>
		TaavoniTozie = 9
	}
}