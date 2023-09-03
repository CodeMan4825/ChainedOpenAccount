namespace OpenAccount.Entities.PersonData
{
	public sealed class RealPersonInfoCompletionDto
	{
		/// <summary>
		/// شهر محل تولد
		/// </summary>
		public int CityId { get; set; }

		/// <summary>
		/// شغل
		/// </summary>
		public int JobId { get; set; }

		/// <summary>
		/// تحصیلات
		/// </summary>
		public byte EducationId { get; set; }
    }
}