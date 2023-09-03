namespace OpenAccount.Entities.Infrastructure
{
	/// <summary>
	/// Base of all Ef entities.
	/// <typeparamref name="TKey"/>
	/// </summary>
	/// <typeparam name="TKey"></typeparam>
	public abstract class BaseEntity<TKey> : IBaseEntity<TKey>
		where TKey : struct
	{
		/// <summary>
		/// <inheritdoc/>
		/// <typeparamref name="TKey"/>
		/// </summary>
		public TKey Id { get; set; }

		/// <summary>
		/// Must override if need to use.
		/// </summary>
		/// <returns></returns>
		/// <example>public override YourClass Clone() => (YourClass)MemberwiseClone();</example>
		/// <exception cref="NotImplementedException"></exception>
		public virtual IBaseEntity<TKey> Clone() => throw new NotImplementedException();
	}
}