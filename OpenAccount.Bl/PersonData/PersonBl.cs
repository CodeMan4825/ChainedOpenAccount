using Microsoft.AspNetCore.Http;
using OpenAccount.BlInterface.PersonData;
using OpenAccount.Entities.PersonData;
using OpenAccount.RepositoryInterface.PersonData;

namespace OpenAccount.Bl.PersonData
{
	internal sealed class PersonBl : BasePersonBl<Person, IPersonRepository>, IPersonBl
	{
		public PersonBl(IPersonRepository logicRepository, IHttpContextAccessor accessor) : base(logicRepository, accessor)
		{
		}
	}
}