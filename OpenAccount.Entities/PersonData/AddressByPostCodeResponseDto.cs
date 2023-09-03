namespace OpenAccount.Entities.PersonData
{
	public sealed class AddressByPostCodeResponseDto
	{
		public List<Data> Data { get; set; } = new();
		public int? ResCode { get; set; }
		public string ResMsg { get; set; } = string.Empty;
	}

	public sealed class Data
	{
		public int? ClientRowID { get; set; }
		public Errors Errors { get; set; } = new();
		public string Postcode { get; set; } = string.Empty;
		public Result Result { get; set; } = new();
		public string Succ { get; set; } = string.Empty;
	}

	public sealed class Result
	{
		public string BuildingName { get; set; } = string.Empty;
		public string Description { get; set; } = string.Empty;
		public int? ErrorCode { get; set; }
		public string Floor { get; set; } = string.Empty;
		public string HouseNumber { get; set; } = string.Empty;
		public int? LocalityCode { get; set; }
		public string LocalityName { get; set; } = string.Empty;
		public string LocalityType { get; set; } = string.Empty;
		public string PostCode { get; set; } = string.Empty;
		public string Province { get; set; } = string.Empty;
		public string SideFloor { get; set; } = string.Empty;
		public string Street { get; set; } = string.Empty;
		public string Street2 { get; set; } = string.Empty;
		public string SubLocality { get; set; } = string.Empty;
		public string TownShip { get; set; } = string.Empty;
		public int? TraceID { get; set; }	
		public string Village { get; set; } = string.Empty;
		public string Zone { get; set; } = string.Empty;
		public string FullAddress { get; set; } = string.Empty;
	}

	public sealed class Errors
	{
		public int? ErrorCode { get; set; }
		public string ErrorMessage { get; set; } = string.Empty;
	}
}