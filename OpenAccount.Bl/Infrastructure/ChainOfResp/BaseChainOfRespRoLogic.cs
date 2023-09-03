using Microsoft.AspNetCore.Http;
using OpenAccount.BlInterface.Infrastructure.ChainOfResp;
using OpenAccount.Entities.Infrastructure;
using OpenAccount.RepositoryInterface.Infrastructure;

namespace OpenAccount.Bl.Infrastructure.ChainOfResp
{
	/// <summary>
	/// Base of ChainOfResp readonly business.
	/// <typeparamref name="TEntity"/>
	/// <typeparamref name="TRepository"/>
	/// <typeparamref name="TKey"/>
	/// <typeparamref name="TEnum"/>
	/// </summary>
	/// <typeparam name="TEntity"></typeparam>
	/// <typeparam name="TRepository"></typeparam>
	/// <typeparam name="TKey"></typeparam>
	/// <typeparam name="TEnum"></typeparam>
	public abstract class BaseChainOfRespRoLogic<TEntity, TRepository, TKey, TEnum> : BaseRoLogic<TEntity, TRepository, TKey>, IBaseChainOfRespRoLogic<TEntity, TKey, TEnum>
		where TEntity : IBaseEntity<TKey>
		where TRepository : IBaseRoRepository<TEntity, TKey>
		where TKey : struct
		where TEnum : Enum
	{
		/// <summary>
		/// مرحله ی قبل
		/// </summary>
		protected IBaseChainOfRespBl<TEnum>? PreRequest { get; set; }

		/// <summary>
		/// نوع مرحله ها
		/// </summary>
		protected TEnum LogicType { get; }

		/// <summary>
		/// <inheritdoc/>
		/// </summary>
		public abstract TEnum GetNextStep();

		/// <summary>
		/// <paramref name="logicRepository"/>
		/// <paramref name="preRequest"/>
		/// <paramref name="logicType"/>
		/// </summary>
		/// <param name="logicRepository"></param>
		/// <param name="preRequest"></param>
		/// <param name="logicType"></param>
		/// <param name="accessor">user data</param>
		protected BaseChainOfRespRoLogic(TRepository logicRepository, IBaseChainOfRespBl<TEnum>? preRequest, TEnum logicType, IHttpContextAccessor accessor) : base(logicRepository, accessor)
		{
			PreRequest = preRequest;
			LogicType = logicType;
		}

		/// <summary>
		/// اعتباراین مرحله کنترل می شود
		/// </summary>
		/// <returns>exception if false</returns>
		public abstract void Validate();

		/// <summary>
		/// Calls CustomValidate();
		/// </summary>
		/// <returns>exception if false</returns>
		public void ValidateChain()
		{
			if (PreRequest != null)
				PreRequest.ValidateChain();
			CustomValidate();
		}

		/// <summary>
		/// Run Validate.
		/// customiza all validates with this method.
		/// </summary>
		protected virtual void CustomValidate() => Validate();
	}
}