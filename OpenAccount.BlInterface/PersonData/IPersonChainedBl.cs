using OpenAccount.Entities.PersonData;

namespace OpenAccount.BlInterface.PersonData
{
	public interface IPersonChainedBl<TPerson> : IOpenAccountChainedRoBl<TPerson, Guid>
		where TPerson : Person
	{
	}
}