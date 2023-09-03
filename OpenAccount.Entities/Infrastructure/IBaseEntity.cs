namespace OpenAccount.Entities.Infrastructure
{
	/// <summary>
	/// Base of all Ef entities.
	/// </summary>
	/// <typeparam name="TKey"></typeparam>
	public interface IBaseEntity<TKey>
		where TKey : struct
	{
		/// <summary>
		/// شناسه
		/// <typeparamref name="TKey"/>
		/// </summary>
		TKey Id { get; set; }

		/// <summary>
		/// <typeparamref name="TKey"/>
		/// </summary>
		/// <returns></returns>
		IBaseEntity<TKey> Clone();
	}
}