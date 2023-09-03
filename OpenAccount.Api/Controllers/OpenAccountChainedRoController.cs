using OpenAccount.Api.Infrastructure.ChainOfResp;
using OpenAccount.BlInterface;
using OpenAccount.Entities.Infrastructure;
using OpenAccount.Entities.Requests;
using OpenAccount.Publics;

namespace OpenAccount.Api.Controllers
{
	/// <summary>
	/// <inheritdoc/>
	/// With RequestStateType as TEnum.
	/// </summary>
	/// <typeparam name="TEntity"></typeparam>
	/// <typeparam name="TLogic"></typeparam>
	/// <typeparam name="TKey"></typeparam>
	public abstract class OpenAccountChainedRoController<TEntity, TLogic, TKey> : ChainOfRespRoController<TEntity, TLogic, TKey, RequestStateType>
		where TEntity : IBaseEntity<TKey>
		where TLogic : IOpenAccountChainedRoBl<TEntity, TKey>
		where TKey : struct
	{
		protected OpenAccountChainedRoController(IConfiguration configuration, IHttpContextAccessor accessor, TLogic baseLogic, RequestStateType controllerEnum, bool ThrowUserData = true) : base(configuration, accessor, baseLogic, controllerEnum, ThrowUserData)
		{
		}

		/// <summary>
		/// شماره درخواست از هدر دریافت شده و برمی گردد
		/// </summary>
		/// <exception cref="StException.ArgumentNull(string)"
		protected Guid RequestId
		{
			get
			{
				var reqIdFromHdr = GetHeader("RequestId");
				if (!Guid.TryParse(reqIdFromHdr, out var reqId))
					throw StException.ArgumentNull("شناسه ی درخواست");
				return reqId;
			}
		}

		protected bool RequestIdExists()
		{
			try
			{
				return RequestId != Guid.Empty;
			}
			catch (Exception)
			{
				return false;
			}
		}
	}
}