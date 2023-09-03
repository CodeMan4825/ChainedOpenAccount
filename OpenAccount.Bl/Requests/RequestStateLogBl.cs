using Microsoft.AspNetCore.Http;
using OpenAccount.Bl.Infrastructure;
using OpenAccount.BlInterface.Requests;
using OpenAccount.Entities.Requests;
using OpenAccount.Publics;
using OpenAccount.RepositoryInterface.Requests;

namespace OpenAccount.Bl.Requests
{
	/// <summary>
	/// لاگ تغییر وضعیت درخواست از مرحله ای به مرحله ی دیگر
	/// </summary>
	internal sealed class RequestStateLogBl : BaseLogic<RequestStateLog, IRequestStateLogRepository, Guid>, IRequestStateLogBl
	{
		public RequestStateLogBl(IRequestStateLogRepository logicRepository, IHttpContextAccessor accessor) : base(logicRepository, accessor)
		{
		}

		/// <summary>
		/// لاگ یک درخواست
		/// </summary>
		/// <param name="requestId">شناسه درخواست</param>
		/// <exception cref="StException.DataNotFound()"></exception>
		/// <returns>IEnumerable<RequestStateLog></returns>
		public Task<IEnumerable<RequestStateLog>> GetByRequest(Guid requestId)
		{
			var result = LogicRepository.AsQuery().Where(x => x.RequestId == requestId).ToList();
			if (result.Count == 0)
				throw StException.DataNotFound("شناسه ی درخواست نامعتبر می باشد");

			return Task.FromResult(result.AsEnumerable());
		}

		/// <summary>
		/// آخرین مرحله ی یک درخواست
		/// </summary>
		/// <param name="requestId">شناسه درخواست</param>
		/// <returns>RequestStateLog</returns>
		public Task<RequestStateLog?> GetLastStateOfRequest(Guid requestId) =>
			Task.FromResult(LogicRepository.AsQuery().Where(x => x.RequestId == requestId).OrderByDescending(x => x.SysDate).FirstOrDefault());
	}
}