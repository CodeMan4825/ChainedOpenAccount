using Microsoft.AspNetCore.Http;
using OpenAccount.BlInterface.Publics.Exceptions;
using OpenAccount.Entities.Publics.Exceptions;
using OpenAccount.RepositoryInterface.Publics.Exceptions;

namespace OpenAccount.Bl.Publics.Exceptions
{
	/// <summary>
	/// مرحله ی اولیه
	/// </summary>
	internal sealed class StartExceptionBl : BaseExceptionBl<StartException, IStartExceptionRepository>, IStartExceptionBl
	{
		public StartExceptionBl(IStartExceptionRepository logicRepository, IHttpContextAccessor accessor) : base(logicRepository, accessor)
		{
		}
	}
}