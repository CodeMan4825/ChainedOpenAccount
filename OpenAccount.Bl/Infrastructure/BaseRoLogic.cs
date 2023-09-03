using Microsoft.AspNetCore.Http;
using OpenAccount.BlInterface.Infrastructure;
using OpenAccount.Entities.Infrastructure;
using OpenAccount.Entities.Publics;
using OpenAccount.Publics;
using OpenAccount.RepositoryInterface.Infrastructure;

namespace OpenAccount.Bl.Infrastructure
{
	/// <summary>
	/// <inheritdoc/>
	/// </summary>
	/// <typeparam name="TEntity"></typeparam>
	/// <typeparam name="TRepository"></typeparam>
	/// <typeparam name="TKey"></typeparam>
	public abstract class BaseRoLogic<TEntity, TRepository, TKey> : IBaseRoLogic<TEntity, TKey>
		where TEntity : IBaseEntity<TKey>
		where TRepository : IBaseRoRepository<TEntity, TKey>
		where TKey : struct
	{
		/// <summary>
		/// <typeparamref name="TRepository"/>
		/// </summary>
		/// <param name="logicRepository"></param>
		/// <param name="accessor"></param>
		/// <param name="userDataNeeded">if true, controls header for user data and raises error if not found.</param>
		protected BaseRoLogic(TRepository logicRepository, IHttpContextAccessor accessor, bool userDataNeeded = true)
		{
			LogicRepository = logicRepository;
			Accessor = accessor;
			if (userDataNeeded)
				GetUserDataFromHeader();
		}

		/// <summary>
		/// Get user data from Accessor.HttpContext.Request.Headers.
		/// </summary>
		protected void GetUserDataFromHeader()
		{
			if (Accessor.HttpContext != null)
			{
				UserData.ClientId = GetHeader("clientId");
				UserData.UserId = CastUtils.StrToGuid(GetHeader("userId"));
				UserData.Roles = GetHeader("roles");
				UserData.UserName = GetHeader("userName");
				UserData.OrganizationId = CastUtils.StrToGuid(GetHeader("organId"));
				UserData.Ip = GetHeader("X-Forwarded-For");
				UserData.ReferenceNumber = CastUtils.StrToGuid(GetHeader("referenceNumber")); ;
				UserData.Channel = GetHeader("channel");
				//from client
				UserData.TraceNumber = GetHeader("traceNumber");
				UserData.DeviceId = GetHeader("deviceId");
				UserData.NationalCode = GetHeader("nationalCode");
			}
			else
				throw StException.ArgumentNull("اطلاعات کاربر");
		}

		/// <summary>
		/// Add user props into dictionary.
		/// </summary>
		/// <returns>Dictionary str str</returns>
		public Dictionary<string, string> GetUserDataFromHeaderAsDictionary() => UserData.UserId == default
				? new()
				: new Dictionary<string, string>
				{
					{ "clientId", UserData.ClientId },
					{ "userId", UserData.UserId.ToString() },
					{ "userName", UserData.UserName },
					{ "organId", UserData.OrganizationId.ToString() },
					{ "X-Forwarded-For", UserData.Ip },
					{ "referenceNumber", UserData.ReferenceNumber.ToString() },
					{ "channel", UserData.Channel },
					{ "traceNumber", UserData.TraceNumber },
					{ "deviceId", UserData.DeviceId },
					{ "nationalCode", UserData.NationalCode }
				};

		/// <summary>
		/// <typeparamref name="TRepository"/>
		/// </summary>
		protected TRepository LogicRepository { get; }

		/// <summary>
		/// <inheritdoc/>
		/// <typeparamref name="TEntity"/>
		/// </summary>
		/// <returns></returns>
		public virtual async Task<IEnumerable<TEntity>> Get() => await LogicRepository.Get();

		/// <summary>
		/// <inheritdoc/>
		/// <typeparamref name="TKey"/>
		/// </summary>
		/// <returns></returns>
		public virtual async Task<TEntity?> Get(TKey id) => await LogicRepository.Get(id);

		/// <summary>
		/// get user data.
		/// </summary>
		protected IHttpContextAccessor Accessor { get; }

		/// <summary>
		/// get data from Accessor
		/// </summary>
		/// <param name="key"></param>
		/// <returns></returns>
		protected string GetHeader(string key) => Accessor.HttpContext != null ? Accessor.HttpContext.Request.Headers[key].ToString() ?? string.Empty : string.Empty;

		/// <inheritdoc/>
		public virtual Task HandledExceptions(HttpStResult? result, Exception? exception) => Task.CompletedTask;

		/// <summary>
		/// User data from header.
		/// </summary>
		protected readonly UserData UserData = new();
	}
}