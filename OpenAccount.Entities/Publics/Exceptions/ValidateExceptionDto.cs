using OpenAccount.Entities.Requests;

namespace OpenAccount.Entities.Publics.Exceptions
{
	public sealed record ValidateExceptionDto(RequestStateType RequestState)
	{
		public ValidateExceptionDto(RequestStateType requestState, string error) : this(requestState) => Error = error;

		public string Error { get; } = string.Empty;
	}

	public sealed record ValidateBalanceExceptionDto(RequestStateType RequestState, long balance)
	{
		public ValidateBalanceExceptionDto(RequestStateType requestState, string error, long balance) : this(requestState, balance) => Error = error;

		public string Error { get; } = string.Empty;
		public long Balance { get; }
	}
}