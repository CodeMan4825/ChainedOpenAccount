using Microsoft.AspNetCore.Http;
using OpenAccount.BlInterface.Publics.Exceptions;
using OpenAccount.Entities.Publics.Exceptions;
using OpenAccount.RepositoryInterface.Publics.Exceptions;

namespace OpenAccount.Bl.Publics.Exceptions
{
	internal sealed class PersonExceptionBl : BaseExceptionBl<PersonException, IPersonExceptionRepository>, IPersonExceptionBl
	{
		public PersonExceptionBl(IPersonExceptionRepository logicRepository, IHttpContextAccessor accessor) : base(logicRepository, accessor)
		{
		}
	}
}