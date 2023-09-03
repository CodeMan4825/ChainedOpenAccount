using Microsoft.AspNetCore.Mvc.Filters;
using OpenAccount.Publics;

namespace OpenAccount.Api.Infrastructure
{
	/// <summary>
	/// A filter that surrounds execution of the action.
	/// A filter that specifies the relative order it should run.
	/// </summary>
	public sealed class HttpResponseExceptionFilter : IActionFilter, IOrderedFilter
	{
		/// <inheritdoc/>
		public int Order => int.MaxValue - 10;

		/// <inheritdoc/>
		public void OnActionExecuted(ActionExecutedContext context)
		{
			if (context.Exception == null)
				return;
			
			switch (context.Exception)
			{
				case StException stException: context.Result = stException.HttpResult; break;
				case ArgumentNullException nullException: context.Result = HttpStResult.ArgumentNull(nullException.Message); break;
				case ArgumentOutOfRangeException rangeException: context.Result = HttpStResult.ArgumentOutOfRange(rangeException.Message); break;
				case DivideByZeroException dbz: context.Result = HttpStResult.ManagedError(dbz.Message); break;
				case NullReferenceException referenceException: context.Result = HttpStResult.ArgumentNull(referenceException.Message); break;
				case StackOverflowException sof: context.Result = HttpStResult.ManagedError(sof.Message); break;
				case EndOfStreamException eos: context.Result = HttpStResult.ManagedError(eos.Message); break;
				case FileNotFoundException fileNotFoundException: context.Result = HttpStResult.FileNotFound(fileNotFoundException.FileName ?? string.Empty); break;
				case FileLoadException fileLoadException: context.Result = HttpStResult.ManagedError(fileLoadException.FileName ?? fileLoadException.Message); break;
				case KeyNotFoundException keyException: context.Result = HttpStResult.KeyNotFound(keyException.Message); break;
				case DirectoryNotFoundException: context.Result = HttpStResult.DirectoryNotFound(StMessages.UnknownException); break;
				case InvalidOperationException: context.Result = HttpStResult.HttpCallInvalidOperationException(); break;
				case HttpRequestException: context.Result = HttpStResult.HttpCallRequestException(); break;
				case TaskCanceledException: context.Result = HttpStResult.HttpCallTaskCanceledException(); break;
				case UriFormatException: context.Result = HttpStResult.HttpCallUriFormatException(); break;
				default: context.Result = HttpStResult.ManagedError(context.Exception.Message); break;
			}

			context.ExceptionHandled = true;

			if (context.Controller is BaseController baseController)
				baseController.HandledExceptions(new HandleExceptionParam { Result = (context.Result as HttpStResult), Exception = context.Exception });
		}

		/// <inheritdoc/>
		public void OnActionExecuting(ActionExecutingContext context)
		{
			if (context.Controller is BaseController baseController)
				try
				{
					baseController.ManageChain();
				}
				catch (StException ex)
				{
					context.Result = ex.HttpResult;
				}
				catch (Exception ex)
				{
					context.Result = HttpStResult.ManagedError(ex.Message);
				}
		}
	}
}