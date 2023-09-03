using OpenAccount.Entities.PersonData;

namespace OpenAccount.Entities.Publics
{
	public sealed class HttpSimorghApiResponseDto<T> : HttpSimorghGeneralApiResponseDto<T>
	{
	}

	public sealed class HttpSimorghCardApiResponseDto<T> : HttpSimorghGeneralApiResponseDto<T>
	{
        public bool OperationResult { get; set; }
		public string Message { get; set; } = string.Empty;
    }

	public abstract class HttpSimorghGeneralApiResponseDto<T>
	{
		public T? Data { get; set; }

		public string ActionMessage { get; set; } = string.Empty;

		public string ActionCode { get; set; } = string.Empty;
		public bool ActionCodeOk => ActionCode == "00000";

		public string TraceNumber { get; set; } = string.Empty;

		public string ReferenceNumber { get; set; } = string.Empty;

		public object? ErrorMessages { get; set; }
	}

	public sealed class HttpUidsApiResponseDto<T>
	{
		public T? Data { get; set; }
		public UidsResult Result { get; set; } = new();
    }

	public sealed class UidsResult
	{
		public string ActionMessage { get; set; } = string.Empty;

		public string ActionCode { get; set; } = string.Empty;
		public bool ActionCodeOk => ActionCode == "00000";
	}
}