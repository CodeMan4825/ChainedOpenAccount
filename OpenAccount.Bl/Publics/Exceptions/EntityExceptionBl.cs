using Microsoft.AspNetCore.Http;
using OpenAccount.BlInterface.Publics.Exceptions;
using OpenAccount.Entities.Publics.Exceptions;
using OpenAccount.RepositoryInterface.Publics.Exceptions;

namespace OpenAccount.Bl.Publics.Exceptions
{
	/// <summary>
	/// خطا های موجودیت
	/// </summary>
	internal sealed class EntityExceptionBl : BaseExceptionBl<EntityException, IEntityExceptionRepository>, IEntityExceptionBl
	{
		public EntityExceptionBl(IEntityExceptionRepository logicRepository, IHttpContextAccessor accessor) : base(logicRepository, accessor)
		{
		}
	}
}