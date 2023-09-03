using Microsoft.EntityFrameworkCore;
using OpenAccount.Entities.Infrastructure;
using OpenAccount.Publics;
using OpenAccount.RepositoryInterface.Infrastructure;

namespace OpenAccount.Repository.Infrastructure
{
	/// <summary>
	/// repository with crud.
	/// </summary>
	/// <typeparam name="TContext"></typeparam>
	/// <typeparam name="TEntity"></typeparam>
	/// <typeparam name="TKey"></typeparam>
	public abstract class BaseDbRepository<TContext, TEntity, TKey> : BaseRoDbRepository<TContext, TEntity, TKey>, IBaseRepository<TEntity, TKey>
		where TContext : DbContext
		where TEntity : BaseEntity<TKey>
		where TKey : struct
	{
		/// <summary>
		/// ctor
		/// </summary>
		/// <param name="context"></param>
		protected BaseDbRepository(TContext context) : base(context)
		{
		}

		/// <summary>
		/// add an entity.
		/// </summary>
		/// <param name="entity"></param>
		/// <param name="save"></param>
		/// <returns></returns>
		public virtual async Task Add(TEntity entity, bool save = true)
		{
			Context.Attach(entity).State = EntityState.Added;
			//await Entities.AddAsync(entity);
			if (save)
				await SaveChangesAsync();
		}

		/// <summary>
		/// add multiple entities.
		/// </summary>
		/// <param name="entities"></param>
		/// <param name="save"></param>
		/// <returns></returns>
		public virtual async Task AddRange(IEnumerable<TEntity> entities, bool save = true)
		{
			entities.ToList().ForEach(x => Context.Attach(x).State = EntityState.Added);
			//await Entities.AddRangeAsync(entities);
			if (save)
				await SaveChangesAsync();
		}

		/// <summary>
		/// delete by id.
		/// </summary>
		/// <param name="id"></param>
		/// <param name="save"></param>
		/// <returns></returns>
		public virtual async Task Delete(TKey id, bool save = true)
		{
			var data = await FindById(id);
			if (data == null)
				throw StException.KeyNotFound("شناسه ی درخواست نامعتبر می باشد");

			Entities.Remove(data);
			if (save)
				await SaveChangesAsync();
		}

		/// <summary>
		/// You must update foundObj.fields with data;
		/// </summary>
		/// <param name="foundObj">Object that must change by "data".</param>
		/// <param name="data">Data to update "foundObj".</param>
		/// <returns>foundObj</returns>
		protected virtual void SetFieldsForUpdate(TEntity foundObj, TEntity data) => throw new NotImplementedException();

		/// <summary>
		/// Update
		/// </summary>
		/// <param name="entity"></param>
		/// <param name="save">true = SaveChangesAsync</param>
		/// <returns></returns>
		public virtual async Task Update(TEntity entity, bool save = true)
		{
			var upd = await Entities.Where(x => x.Id.Equals(entity.Id)).AsTracking().FirstAsync();
			//var upd = await FindById(entity.Id);
			if (upd == null)
				throw StException.DataNotFound("شناسه ی درخواست نامعتبر می باشد");
			SetFieldsForUpdate(upd, entity);
			//Context.Attach(upd).State = EntityState.Modified; //dont need update : (AsTracking)
			//Entities.Update(upd);
			if (save)
				await SaveChangesAsync();
		}

		/// <summary>
		/// UpdateRange
		/// </summary>
		/// <param name="entities"></param>
		/// <param name="save"></param>
		/// <returns></returns>
		public virtual async Task UpdateRange(IEnumerable<TEntity> entities, bool save = true)
		{
			foreach (var baseEntity in entities)
				await Update(baseEntity, false);

			if (save)
				await SaveChangesAsync();
		}
	}
}