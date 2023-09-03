namespace OpenAccount.Entities.Publics.BtmsDtos
{
	/// <summary>
	/// Btms.InsertSign response.
	/// </summary>
	public sealed class SanamInsertSignRequestDto
	{
		public long AccountNumber { get; set; }
		public int Radif { get; set; }
		public string SignFile { get; set; } = string.Empty;
		public long NationalCode { get; set; }
		public bool EmbossStamp { get; set; }
	}
}