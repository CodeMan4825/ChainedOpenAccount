using Microsoft.AspNetCore.Http;
using OpenAccount.Bl.Infrastructure;
using OpenAccount.Entities.Publics.Exceptions;
using OpenAccount.RepositoryInterface.Infrastructure;

namespace OpenAccount.Bl.Publics.Exceptions
{
	internal abstract class BaseExceptionBl<TEntity, TRepository> : BaseLogic<TEntity, TRepository, Guid>
		  where TEntity : EntityException
		  where TRepository : IBaseRepository<TEntity, Guid>
	{
		protected BaseExceptionBl(TRepository logicRepository, IHttpContextAccessor accessor) : base(logicRepository, accessor)
		{
		}

		/// <summary>
		/// <inheritdoc/>
		/// No error raises.
		/// </summary>
		/// <param name="entity"></param>
		/// <param name="save"></param>
		/// <returns></returns>
		public override async Task Post(TEntity entity, bool save = true)
		{
			try
			{
				entity.Id = Guid.NewGuid();
				entity.SysDate = DateTime.Now;
				entity.UserId = UserData.UserId;
				await base.Post(entity, save);
			}
			catch (Exception)
			{
				// Do not raise error.
			}
		}

		/// <summary>
		/// No put allowed.
		/// </summary>
		/// <param name="entity"></param>
		/// <param name="save"></param>
		/// <returns></returns>
		public override Task Put(TEntity entity, bool save = true) => Task.CompletedTask;
	}
}