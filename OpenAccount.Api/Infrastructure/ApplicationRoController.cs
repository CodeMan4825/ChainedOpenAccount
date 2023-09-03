using Microsoft.AspNetCore.Mvc;
using OpenAccount.BlInterface.Infrastructure;
using OpenAccount.Entities.Infrastructure;

namespace OpenAccount.Api.Infrastructure
{
	/// <summary>
	/// ReadOnly base controller.
	/// </summary>
	/// <typeparam name="TEntity"></typeparam>
	/// <typeparam name="TLogic"></typeparam>
	/// <typeparam name="TKey"></typeparam>
	//[Authorize]
	public abstract class ApplicationRoController<TEntity, TLogic, TKey> : BaseController
		where TEntity : IBaseEntity<TKey>
		where TLogic : IBaseRoLogic<TEntity, TKey>
		where TKey : struct
	{
		/// <summary>
		/// <inheritdoc/>
		/// </summary>
		/// <param name="configuration"></param>
		/// <param name="accessor">to access header.</param>
		/// <param name="baseLogic"></param>
		/// <param name="ThrowUserData">if UserData.UserId, NationalCode not exists, throw exception</param>
		protected ApplicationRoController(IConfiguration configuration, IHttpContextAccessor accessor, TLogic baseLogic, bool ThrowUserData = true) : base(configuration, accessor, ThrowUserData) => ControllerLogic = baseLogic;

		/// <summary>
		/// <typeparamref name="TLogic"/>
		/// </summary>
		protected TLogic ControllerLogic { get; }

		/// <summary>
		/// <typeparamref name="TEntity"/>
		/// </summary>
		/// <returns>IEnumerable of TEntity</returns>
		[HttpGet]
		[ApiExplorerSettings(IgnoreApi = true)]
		public virtual async Task<IActionResult> Get() => await GetAction(async () =>
		{
			ValidateGet();
			return await ControllerLogic.Get();
		});

		/// <summary>
		/// Find 1 entity by Id.
		/// <typeparamref name="TKey"/>
		/// <typeparamref name="TEntity"/>
		/// </summary>
		/// <param name="id">Id to find TEntity</param>
		/// <returns>TEntity or NotFound</returns>
		[HttpGet("{id}")]
		[ApiExplorerSettings(IgnoreApi = true)]
		public virtual async Task<IActionResult> Get(TKey id) => await GetAction(async () =>
		{
			ValidateGet();
			var result = await ControllerLogic.Get(id);
			return result == null ? NotFound() : result;
		});

		/// <summary>
		/// Control accessibility.
		/// </summary>
		protected virtual void ValidateGet()
		{
		}

		/// <summary>
		/// Add user props into dictionary.
		/// </summary>
		/// <returns>Dictionary str str</returns>
		protected Dictionary<string, string> GetUserDataFromHeaderAsDictionary() => ControllerLogic.GetUserDataFromHeaderAsDictionary();

		/// <inheritdoc/>
		public override async Task HandledExceptions([FromBody] HandleExceptionParam param) => await ControllerLogic.HandledExceptions(param.Result, param.Exception);
	}
}