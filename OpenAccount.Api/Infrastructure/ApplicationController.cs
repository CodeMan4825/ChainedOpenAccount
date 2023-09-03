using Microsoft.AspNetCore.Mvc;
using OpenAccount.BlInterface.Infrastructure;
using OpenAccount.Entities.Infrastructure;
using OpenAccount.Publics;

namespace OpenAccount.Api.Infrastructure
{
	/// <summary>
	/// Base controller
	/// </summary>
	/// <typeparam name="TEntity"></typeparam>
	/// <typeparam name="TLogic"></typeparam>
	/// <typeparam name="TKey"></typeparam>
	public abstract class ApplicationController<TEntity, TLogic, TKey> : ApplicationRoController<TEntity, TLogic, TKey>
		where TEntity : IBaseEntity<TKey>
		where TLogic : IBaseLogic<TEntity, TKey>
		where TKey : struct
	{
		/// <summary>
		/// </summary>
		/// <param name="configuration"></param>
		/// <param name="accessor">to access header.</param>
		/// <param name="baseLogic"></param>
		/// <param name="ThrowUserData">if UserData.UserId, NationalCode not exists, throw exception</param>
		protected ApplicationController(IConfiguration configuration, IHttpContextAccessor accessor, TLogic baseLogic, bool ThrowUserData = true) : base(configuration, accessor, baseLogic, ThrowUserData)
		{
		}

		/// <summary>
		/// <typeparamref name="TEntity"/>
		/// </summary>
		/// <param name="entity"></param>
		/// <returns></returns>
		protected virtual Task ValidatePost(TEntity entity) => Task.CompletedTask;

		/// <summary>
		/// <typeparamref name="TEntity"/>
		/// </summary>
		/// <param name="entity"></param>
		/// <returns></returns>
		protected virtual Task ValidatePut(TEntity entity) => Task.CompletedTask;

		/// <summary>
		/// <typeparamref name="TKey"/>
		/// </summary>
		/// <param name="id"></param>
		protected virtual void ValidateDelete(TKey id)
		{
		}

		/// <summary>
		/// Post entity to db.
		/// <typeparamref name="TEntity"/>
		/// </summary>
		/// <param name="entity"></param>
		/// <returns></returns>
		[HttpPost()]
		[ApiExplorerSettings(IgnoreApi = true)]
		public virtual async Task<IActionResult> Post([FromBody] TEntity entity) =>
			await DoAction(async () =>
			{
				await ValidatePost(entity);
				await ControllerLogic.Post(entity);
			});

		/// <summary>
		/// Post all entities to db.
		/// <typeparamref name="TEntity"/>
		/// </summary>
		/// <param name="entities"></param>
		/// <returns></returns>
		[HttpPost("PostAll")]
		[ApiExplorerSettings(IgnoreApi = true)]
		public virtual async Task<IActionResult> PostAll([FromBody] IEnumerable<TEntity> entities) =>
			await DoAction(async () =>
			{
				if (entities == null)
					throw StException.ArgumentNull("");
				var baseDtos = entities as TEntity[] ?? entities.ToArray();
				baseDtos.ToList().ForEach(async dto => await ValidatePost(dto));
				await ControllerLogic.PostAll(baseDtos);
			});

		/// <summary>
		/// Update entity to db.
		/// <typeparamref name="TEntity"/>
		/// </summary>
		/// <param name="entity"></param>
		/// <returns></returns>
		[HttpPost("Put")]
		[ApiExplorerSettings(IgnoreApi = true)]
		public virtual async Task<IActionResult> Put([FromBody] TEntity entity) =>
			await DoAction(async () =>
			{
				await ValidatePut(entity);
				await ControllerLogic.Put(entity);
			});

		/// <summary>
		/// Update all entites to db.
		/// <typeparamref name="TEntity"/>
		/// </summary>
		/// <param name="entities"></param>
		/// <returns></returns>
		[HttpPost("PutAll")]
		[ApiExplorerSettings(IgnoreApi = true)]
		public virtual async Task<IActionResult> PutAll([FromBody] IEnumerable<TEntity> entities) =>
			await DoAction(async () =>
			{
				if (entities == null)
					throw StException.ArgumentNull("");
				var baseDtos = entities as TEntity[] ?? entities.ToArray();
				baseDtos.ToList().ForEach(async dto => await ValidatePut(dto));
				await ControllerLogic.PutAll(baseDtos);
			});

		/// <summary>
		/// Delete entity by Id.
		/// <typeparamref name="TKey"/>
		/// </summary>
		/// <param name="id">Find entity by Id</param>
		/// <returns></returns>
		[HttpPost("{id}")]
		[ApiExplorerSettings(IgnoreApi = true)]
		public virtual async Task<IActionResult> Delete(TKey id) =>
			await DoAction(async () => { ValidateDelete(id); await ControllerLogic.Delete(id); });
	}
}