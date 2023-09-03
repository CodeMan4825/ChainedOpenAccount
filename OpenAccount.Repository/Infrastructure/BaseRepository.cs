using Microsoft.EntityFrameworkCore;
using OpenAccount.Entities.Infrastructure;
using System.Data.Common;

namespace OpenAccount.Repository.Infrastructure
{
	/// <summary>
	/// Base of all repositories.
	/// </summary>
	/// <typeparam name="TContext"></typeparam>
	/// <typeparam name="TEntity"></typeparam>
	/// <typeparam name="TKey"></typeparam>
	public abstract class BaseRepository<TContext, TEntity, TKey> : IDisposable
		where TContext : DbContext
		where TEntity : IBaseEntity<TKey>
		where TKey : struct
	{
		protected TContext Context;

		/// <summary>
		/// <paramref name="context"/>
		/// </summary>
		/// <param name="context"></param>
		protected BaseRepository(TContext context)
		{
			Context = context;
			Context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
		}

		/// <summary>
		/// <inheritdoc/>
		/// </summary>
		public void Dispose()
		{
			try
			{
				Context.Dispose();
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
			}
		}

		/// <summary>
		/// command.ExecuteReaderAsync()
		/// </summary>
		/// <param name="sql"></param>
		/// <returns></returns>
		/// <exception cref="Exception"></exception>
		public async Task<DbDataReader> ExecuteReader(string sql)
		{
			await using var command = Context.Database.GetDbConnection().CreateCommand();
			if (command.Connection == null)
				throw new Exception("");

			if (command.Connection.State != System.Data.ConnectionState.Closed)
				await command.Connection.CloseAsync();
			await command.Connection.OpenAsync();
			command.CommandText = sql;
			return await command.ExecuteReaderAsync();
		}

		/// <summary>
		/// returns command.Parameters[paramName].Value.ToString().Trim();
		/// </summary>
		/// <param name="command"></param>
		/// <param name="paramName"></param>
		/// <returns>Trimed string</returns>
		public static string ValueParamString(DbCommand command, string paramName)
		{
			var value = command.Parameters[paramName].Value;
			if (value == null)
				throw new Exception("");
			var result = value.ToString();
			if (result == null)
				throw new Exception("");
			return result.Trim();
		}

		/// <summary>
		/// Context.SaveChangesAsync()
		/// </summary>
		/// <returns></returns>
		public async Task SaveChangesAsync() => await Context.SaveChangesAsync();
	}
}