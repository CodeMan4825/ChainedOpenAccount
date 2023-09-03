using OpenAccount.Entities.Requests;
using OpenAccount.Publics;
using System.ComponentModel;

namespace OpenAccount.Entities.Publics.Exceptions
{
	/// <summary>
	/// خطا های مراحل اطلاعات پرسنلی
	/// </summary>
	[Description("خطا های مراحل اطلاعات پرسنلی")]
	public sealed class PersonException : RequestException
	{
		public PersonException(RequestStateType personState)
		{
			PersonState = personState;
			switch (personState)
			{
				case RequestStateType.PersonIdentification:
				case RequestStateType.PersonInfoCompletion:
				case RequestStateType.PersonPostInquery:
					PersonStateCaption = Utility.GetEnumDescription(personState);
					break;
				default: throw new ArgumentException();
			}
		}

		public RequestStateType PersonState { get; private set; }
		public string PersonStateCaption { get; private set; } = string.Empty;
	}
}