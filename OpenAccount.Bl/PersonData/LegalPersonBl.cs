using Microsoft.AspNetCore.Http;
using OpenAccount.BlInterface.PersonData;
using OpenAccount.Entities.PersonData;
using OpenAccount.RepositoryInterface.PersonData;

namespace OpenAccount.Bl.PersonData
{
	internal sealed class LegalPersonBl : BasePersonBl<LegalPerson, ILegalPersonRepository>, ILegalPersonBl
	{
		public LegalPersonBl(ILegalPersonRepository logicRepository, IHttpContextAccessor accessor) : base(logicRepository, accessor)
		{
		}
	}
}