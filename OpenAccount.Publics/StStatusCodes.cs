using System.Net;

namespace OpenAccount.Publics
{
	/*
	 * https://dotnettutorials.net/lesson/http-status-codes-in-asp-net-core-web-api/
	 * 1XX: Informational Response (Example: 100, 101, 102, etc.)
	 * 2XX: Successful, whenever you get 2XX as the response code, it means the request is successful. 
	 *		For example, we get 200 HTTP Status Code for the success of a GET request, 201 if a new resource has been successfully created. 
	 *		204 status code is also for success but in return, it does not return anything just like if the client has performed a delete operation and in return doesn’t really expect something back.
	 * 3XX: 3XX HTTP status codes are basically used for redirection. 
	 *		Whenever you get 3XX as the response code, it means it is re-directional. 
	 *		for example, to tell a client that the requested resource like page, the image has been moved to another location.
	 * 4XX: 4XX HTTP status codes are meant to state errors or Client Error. 
	 *		Whenever you get 4XX as the response code, it means there is some problem with your request. 
	 *		For example, status code 400 means Bad Request, 
	 *		401 is Unauthorized that is invalid authentication credentials or details have been provided by the client, 
	 *		403 HTTP Status code means that authentication is a success, but the user is not authorized. 
	 *		404 HTTP Status code means the requested resource is not available.
	 * 5XX: 5XX HTTP status codes are meant for Server Error. 
	 *		Whenever you get 5XX as the response code, it means there is some problem in the server. 
	 *		Internal Server Error exception is very common, which contains code 500. 
	 *		This error means that there is some unexpected error on the server and the client cannot do anything about it.
	 */
	/// <summary>
	/// HttpStatusCode Enum
	/// Contains the values of status codes defined for HTTP defined in RFC 2616 for HTTP 1.1.
	/// <see cref="https://learn.microsoft.com/en-us/dotnet/api/system.net.httpstatuscode?view=net-7.0]"/>
	/// </summary>
	public enum StStatusCodes : int
	{
		#region General codes.

		#region 1XX : 100..103
		/// <summary>
		///	Equivalent to HTTP status 100.
		///	<see cref="HttpStatusCode.Continue"/> indicates that the client can continue with its request.
		/// </summary>
		Continue = HttpStatusCode.Continue,

		/// <summary>
		///	Equivalent to HTTP status 101.
		///	<see cref="HttpStatusCode.SwitchingProtocols"/> indicates that the protocol version or protocol is being changed.
		/// </summary>
		SwitchingProtocols = HttpStatusCode.SwitchingProtocols,

		/// <summary>
		///	Equivalent to HTTP status 102.
		///	<see cref="HttpStatusCode.Processing"/> indicates that the server has accepted the complete request but hasn't completed it yet.
		/// </summary>
		Processing = HttpStatusCode.Processing,

		/// <summary>	
		///	Equivalent to HTTP status 103.
		///	<see cref="HttpStatusCode.EarlyHints"/> indicates to the client that the server is likely to send a final response with the header fields
		///	included in the informational response.
		/// </summary>
		EarlyHints = HttpStatusCode.EarlyHints,

		#endregion

		#region 2XX : 200..208, 226
		/// <summary>	
		///	Equivalent to HTTP status 200
		///	<see cref="HttpStatusCode.OK"/> indicates that the request succeeded and that the requested information is in the response.
		///	This is the most common status code to receive.
		/// </summary>
		OK = HttpStatusCode.OK,

		/// <summary>	
		///	Equivalent to HTTP status 201.
		///	<see cref="HttpStatusCode.Created"/> indicates that the request resulted in a new resource created before the response was sent.
		/// </summary>
		Created = HttpStatusCode.Created,

		/// <summary>	
		///	Equivalent to HTTP status 202.
		///	<see cref="HttpStatusCode.Accepted"/> indicates that the request has been accepted for further processing.
		///	</summary>	
		Accepted = HttpStatusCode.Accepted,

		/// <summary>	
		///	Equivalent to HTTP status 203.
		///	<see cref="HttpStatusCode.NonAuthoritativeInformation"/> indicates that the returned meta information is from a cached copy instead of the origin server and therefore may be incorrect.
		/// </summary>
		NonAuthoritativeInformation = HttpStatusCode.NonAuthoritativeInformation,

		/// <summary>	
		///	Equivalent to HTTP status 204.
		///	<see cref="HttpStatusCode.NoContent"/> indicates that the request has been successfully processed and that the response is intentionally blank.
		/// </summary>
		NoContent = HttpStatusCode.NoContent,

		/// <summary>	
		///	Equivalent to HTTP status 205.
		///	<see cref="HttpStatusCode.ResetContent"/> indicates that the client should reset (not reload) the current resource.
		/// </summary>
		ResetContent = HttpStatusCode.ResetContent,

		/// <summary>	
		///	Equivalent to HTTP status 206.
		///	<see cref="HttpStatusCode.PartialContent"/> indicates that the response is a partial response as requested by a GET request that includes a byte range.
		/// </summary>
		PartialContent = HttpStatusCode.PartialContent,

		/// <summary>	
		///	Equivalent to HTTP status 207.
		///	<see cref="HttpStatusCode.MultiStatus"/> indicates multiple status codes for a single response during a Web Distributed Authoring and Versioning (WebDAV) operation.
		///	The response body contains XML that describes the status codes.
		/// </summary>
		MultiStatus = HttpStatusCode.MultiStatus,

		/// <summary>	
		///	Equivalent to HTTP status 208.
		///	<see cref="HttpStatusCode.AlreadyReported"/> indicates that the members of a WebDAV binding have already been enumerated in a preceding part of the multistatus response,
		///	and are not being included again.
		/// </summary>
		AlreadyReported = HttpStatusCode.AlreadyReported,

		/// <summary>	
		///	Equivalent to HTTP status 226.
		///	<see cref="HttpStatusCode.IMUsed"/> indicates that the server has fulfilled a request for the resource,
		///	and the response is a representation of the result of one or more instance-manipulations applied to the current instance.
		/// </summary>
		IMUsed = HttpStatusCode.IMUsed,

		#endregion

		#region 3XX : 300..308
		/// <summary>	
		///	Equivalent to HTTP status 300.
		///	<see cref="HttpStatusCode.Ambiguous"/> indicates that the requested information has multiple representations.
		///	The default action is to treat this status as a redirect and follow the contents of the Location header associated with this response.
		///	<see cref="HttpStatusCode.Ambiguous"/> is a synonym for <see cref="HttpStatusCode.MultipleChoices"/>.
		/// </summary>
		Ambiguous = HttpStatusCode.Ambiguous,

		/// <summary>	
		///	Equivalent to HTTP status 300.
		///	<see cref="HttpStatusCode.MultipleChoices"/> indicates that the requested information has multiple representations.
		///	The default action is to treat this status as a redirect and follow the contents of the Location header associated with this response.
		///	<see cref="HttpStatusCode.MultipleChoices"/> is a synonym for <see cref="HttpStatusCode.Ambiguous"/>.
		/// </summary>
		MultipleChoices = HttpStatusCode.MultipleChoices,

		/// <summary>	
		///	Equivalent to HTTP status 301.
		///	<see cref="HttpStatusCode.Moved"/> indicates that the requested information has been moved to the URI specified in the Location header.
		///	The default action when this status is received is to follow the Location header associated with the response.
		///	When the original request method was POST, the redirected request will use the GET method.
		///	<see cref="HttpStatusCode.Moved"/> is a synonym for <see cref="HttpStatusCode.MovedPermanently"/>.
		/// </summary>
		Moved = HttpStatusCode.Moved,

		/// <summary>	
		///	Equivalent to HTTP status 301.
		///	<see cref="HttpStatusCode.MovedPermanently"/> indicates that the requested information has been moved to the URI specified in the Location header.
		///	The default action when this status is received is to follow the Location header associated with the response.
		///	<see cref="HttpStatusCode.MovedPermanently"/> is a synonym for <see cref="HttpStatusCode.Moved"/>.
		/// </summary>
		MovedPermanently = HttpStatusCode.MovedPermanently,

		/// <summary>	
		///	Equivalent to HTTP status 302.
		///	<see cref="HttpStatusCode.Found"/> indicates that the requested information is located at the URI specified in the Location header.
		///	The default action when this status is received is to follow the Location header associated with the response.
		///	When the original request method was POST, the redirected request will use the GET method.
		///	<see cref="HttpStatusCode.Found"/> is a synonym for <see cref="HttpStatusCode.Redirect"/>.
		/// </summary>
		Found = HttpStatusCode.Found,

		/// <summary>	
		///	Equivalent to HTTP status 302.
		///	<see cref="HttpStatusCode.Redirect"/> indicates that the requested information is located at the URI specified in the Location header.
		///	The default action when this status is received is to follow the Location header associated with the response.
		///	When the original request method was POST, the redirected request will use the GET method.
		///	<see cref="HttpStatusCode.Redirect"/> is a synonym for <see cref="HttpStatusCode.Found"/>.
		/// </summary>
		Redirect = HttpStatusCode.Redirect,

		/// <summary>	
		///	Equivalent to HTTP status 303.
		///	<see cref="HttpStatusCode.RedirectMethod"/> automatically redirects the client to the URI specified in the Location header as the result of a POST.
		///	The request to the resource specified by the Location header will be made with a GET.
		///	<see cref="HttpStatusCode.RedirectMethod"/> is a synonym for <see cref="HttpStatusCode.SeeOther"/>.
		/// </summary>
		RedirectMethod = HttpStatusCode.RedirectMethod,

		/// <summary>	
		///	Equivalent to HTTP status 303.
		///	<see cref="HttpStatusCode.SeeOther"/> automatically redirects the client to the URI specified in the Location header as the result of a POST.
		///	The request to the resource specified by the Location header will be made with a GET.
		///	<see cref="HttpStatusCode.SeeOther"/> is a synonym for <see cref="HttpStatusCode.RedirectMethod"/>.
		/// </summary>
		SeeOther = HttpStatusCode.SeeOther,

		/// <summary>	
		///	Equivalent to HTTP status 304.
		///	<see cref="HttpStatusCode.NotModified"/> indicates that the client's cached copy is up to date.
		///	The contents of the resource are not transferred.
		/// </summary>
		NotModified = HttpStatusCode.NotModified,

		/// <summary>	
		///	Equivalent to HTTP status 305.
		///	<see cref="HttpStatusCode.UseProxy"/> indicates that the request should use the proxy server at the URI specified in the Location header.
		/// </summary>
		UseProxy = HttpStatusCode.UseProxy,

		/// <summary>	
		///	Equivalent to HTTP status 306.
		///	<see cref="HttpStatusCode.Unused"/> is a proposed extension to the HTTP/1.1 specification that is not fully specified.
		/// </summary>
		Unused = HttpStatusCode.Unused,

		/// <summary>	
		///	Equivalent to HTTP status 307.
		///	<see cref="HttpStatusCode.RedirectKeepVerb"/> indicates that the request information is located at the URI specified in the Location header.
		///	The default action when this status is received is to follow the Location header associated with the response.
		///	When the original request method was POST, the redirected request will also use the POST method.
		///	<see cref="HttpStatusCode.RedirectKeepVerb"/> is a synonym for <see cref="HttpStatusCode.TemporaryRedirect"/>.
		/// </summary>
		RedirectKeepVerb = HttpStatusCode.RedirectKeepVerb,

		/// <summary>	
		///	Equivalent to HTTP status 307.
		///	<see cref="HttpStatusCode.TemporaryRedirect"/> indicates that the request information is located at the URI specified in the Location header.
		///	The default action when this status is received is to follow the Location header associated with the response.
		///	When the original request method was POST, the redirected request will also use the POST method.
		///	<see cref="HttpStatusCode.TemporaryRedirect"/> is a synonym for <see cref="HttpStatusCode.RedirectKeepVerb"/>.
		/// </summary>
		TemporaryRedirect = HttpStatusCode.TemporaryRedirect,

		/// <summary>	
		///	Equivalent to HTTP status 308.
		///	<see cref="HttpStatusCode.PermanentRedirect"/> indicates that the request information is located at the URI specified in the Location header.
		///	The default action when this status is received is to follow the Location header associated with the response.
		///	When the original request method was POST, the redirected request will also use the POST method.
		/// </summary>
		PermanentRedirect = HttpStatusCode.PermanentRedirect,

		#endregion

		#region 4XX : 400..417, 421..424, 426, 428, 429, 431, 451
		/// <summary>	
		///	Equivalent to HTTP status 400.
		///	<see cref="HttpStatusCode.BadRequest"/> indicates that the request could not be understood by the server.
		///	BadRequest is sent when no other error is applicable, or if the exact error is unknown or does not have its own error code.
		/// </summary>
		BadRequest = HttpStatusCode.BadRequest,

		/// <summary>	
		///	Equivalent to HTTP status 401.
		///	<see cref="HttpStatusCode.Unauthorized"/> indicates that the requested resource requires authentication.
		///	The WWW-Authenticate header contains the details of how to perform the authentication.
		/// </summary>
		Unauthorized = HttpStatusCode.Unauthorized,

		/// <summary>	
		///	Equivalent to HTTP status 402.
		///	<see cref="HttpStatusCode.PaymentRequired"/> is reserved for future use.
		/// </summary>
		PaymentRequired = HttpStatusCode.PaymentRequired,

		/// <summary>	
		///	Equivalent to HTTP status 403.
		///	<see cref="HttpStatusCode.Forbidden"/> indicates that the server refuses to fulfill the request.
		/// </summary>
		Forbidden = HttpStatusCode.Forbidden,

		/// <summary>	
		///	Equivalent to HTTP status 404.
		///	<see cref="HttpStatusCode.NotFound"/> indicates that the requested resource does not exist on the server.
		/// </summary>
		NotFound = HttpStatusCode.NotFound,

		/// <summary>	
		///	Equivalent to HTTP status 405.
		///	<see cref="HttpStatusCode.MethodNotAllowed"/> indicates that the request method (POST or GET) is not allowed on the requested resource.
		/// </summary>
		MethodNotAllowed = HttpStatusCode.MethodNotAllowed,

		/// <summary>	
		///	Equivalent to HTTP status 406.
		///	<see cref="HttpStatusCode.NotAcceptable"/> indicates that the client has indicated with Accept headers that it will not accept any of the
		///	available representations of the resource.
		/// </summary>
		NotAcceptable = HttpStatusCode.NotAcceptable,

		/// <summary>	
		///	Equivalent to HTTP status 407.
		///	<see cref="HttpStatusCode.ProxyAuthenticationRequired"/> indicates that the requested proxy requires authentication.
		///	The Proxy-authenticate header contains the details of how to perform the authentication.
		/// </summary>
		ProxyAuthenticationRequired = HttpStatusCode.ProxyAuthenticationRequired,

		/// <summary>	
		///	Equivalent to HTTP status 408.
		///	<see cref="HttpStatusCode.RequestTimeout"/> indicates that the client did not send a request within the time the server was expecting the request.
		/// </summary>
		RequestTimeout = HttpStatusCode.RequestTimeout,

		/// <summary>	
		///	Equivalent to HTTP status 409.
		///	<see cref="HttpStatusCode.Conflict"/> indicates that the request could not be carried out because of a conflict on the server.
		/// </summary>
		Conflict = HttpStatusCode.Conflict,

		/// <summary>	
		///	Equivalent to HTTP status 410.
		///	<see cref="HttpStatusCode.Gone"/> indicates that the requested resource is no longer available.
		/// </summary>
		Gone = HttpStatusCode.Gone,

		/// <summary>	
		///	Equivalent to HTTP status 411.
		///	<see cref="HttpStatusCode.LengthRequired"/> indicates that the required Content-length header is missing.
		/// </summary>
		LengthRequired = HttpStatusCode.LengthRequired,

		/// <summary>	
		///	Equivalent to HTTP status 412.
		///	<see cref="HttpStatusCode.PreconditionFailed"/> indicates that a condition set for this request failed, and the request cannot be carried out.
		///	Conditions are set with conditional request headers like If-Match, If-None-Match, or If-Unmodified-Since.
		/// </summary>
		PreconditionFailed = HttpStatusCode.PreconditionFailed,

		/// <summary>	
		///	Equivalent to HTTP status 413.
		///	<see cref="HttpStatusCode.RequestEntityTooLarge"/> indicates that the request is too large for the server to process.
		/// </summary>
		RequestEntityTooLarge = HttpStatusCode.RequestEntityTooLarge,

		/// <summary>	
		///	Equivalent to HTTP status 414.
		///	<see cref="HttpStatusCode.RequestUriTooLong"/> indicates that the URI is too long.
		/// </summary>
		RequestUriTooLong = HttpStatusCode.RequestUriTooLong,

		/// <summary>	
		///	Equivalent to HTTP status 415.
		///	<see cref="HttpStatusCode.UnsupportedMediaType"/> indicates that the request is an unsupported type.
		/// </summary>
		UnsupportedMediaType = HttpStatusCode.UnsupportedMediaType,

		/// <summary>	
		///	Equivalent to HTTP status 416. 
		///	<see cref="HttpStatusCode.RequestedRangeNotSatisfiable"/> indicates that the range of data requested from the resource cannot be returned, 
		///	either because the beginning of the range is before the beginning of the resource, or the end of the range is after the end of the resource.
		/// </summary>
		RequestedRangeNotSatisfiable = HttpStatusCode.RequestedRangeNotSatisfiable,

		/// <summary>	
		///	Equivalent to HTTP status 417. 
		///	<see cref="HttpStatusCode.ExpectationFailed"/> indicates that an expectation given in an Expect header could not be met by the server.
		/// </summary>
		ExpectationFailed = HttpStatusCode.ExpectationFailed,

		/// <summary>	
		///	Equivalent to HTTP status 421.
		///	<see cref="HttpStatusCode.MisdirectedRequest"/> indicates that the request was directed at a server that is not able to produce a response.
		/// </summary>
		MisdirectedRequest = HttpStatusCode.MisdirectedRequest,

		/// <summary>	
		///	Equivalent to HTTP status 422.
		///	<see cref="HttpStatusCode.UnprocessableEntity"/> indicates that the request was well-formed but was unable to be followed due to semantic errors.
		/// </summary>
		UnprocessableEntity = HttpStatusCode.UnprocessableEntity,

		/// <summary>	
		///	Equivalent to HTTP status 423.
		///	<see cref="HttpStatusCode.Locked"/> indicates that the source or destination resource is locked.
		/// </summary>
		Locked = HttpStatusCode.Locked,

		/// <summary>	
		///	Equivalent to HTTP status 424.
		///	<see cref="HttpStatusCode.FailedDependency"/> indicates that the method couldn't be performed on the resource because the requested action depended on another action and that action failed.
		/// </summary>
		FailedDependency = HttpStatusCode.FailedDependency,

		/// <summary>	
		///	Equivalent to HTTP status 426.
		///	<see cref="HttpStatusCode.UpgradeRequired"/> indicates that the client should switch to a different protocol such as TLS/1.0.
		/// </summary>
		UpgradeRequired = HttpStatusCode.UpgradeRequired,

		/// <summary>	
		///	Equivalent to HTTP status 428.
		///	<see cref="HttpStatusCode.PreconditionRequired"/> indicates that the server requires the request to be conditional.
		/// </summary>
		PreconditionRequired = HttpStatusCode.PreconditionRequired,

		/// <summary>	
		///	Equivalent to HTTP status 429.
		///	<see cref="HttpStatusCode.TooManyRequests"/> indicates that the user has sent too many requests in a given amount of time.
		/// </summary>
		TooManyRequests = HttpStatusCode.TooManyRequests,

		/// <summary>	
		///	Equivalent to HTTP status 431.
		///	<see cref="HttpStatusCode.RequestHeaderFieldsTooLarge"/> indicates that the server is unwilling to process the request because its header fields
		///	(either an individual header field or all the header fields collectively) are too large.
		/// </summary>
		RequestHeaderFieldsTooLarge = HttpStatusCode.RequestHeaderFieldsTooLarge,

		/// <summary>	
		///	Equivalent to HTTP status 451.
		///	<see cref="HttpStatusCode.UnavailableForLegalReasons"/> indicates that the server is denying access to the resource as a consequence of a legal demand.
		/// </summary>
		UnavailableForLegalReasons = HttpStatusCode.UnavailableForLegalReasons,

		#endregion

		#region 5XX : 500..508, 510, 511
		/// <summary>
		///	Equivalent to HTTP status 500.
		///	<see cref="HttpStatusCode.InternalServerError"/> indicates that a generic error has occurred on the server.
		/// </summary>
		InternalServerError = HttpStatusCode.InternalServerError,

		/// <summary>
		///	Equivalent to HTTP status 501.
		///	<see cref="HttpStatusCode.NotImplemented"/> indicates that the server does not support the requested function.
		/// </summary>
		NotImplemented = HttpStatusCode.NotImplemented,

		/// <summary>	
		///	Equivalent to HTTP status 502.
		///	<see cref="HttpStatusCode.BadGateway"/> indicates that an intermediate proxy server received a bad response from another proxy or the origin server.
		/// </summary>
		BadGateway = HttpStatusCode.BadGateway,

		/// <summary>	
		///	Equivalent to HTTP status 503.
		///	<see cref="HttpStatusCode.ServiceUnavailable"/> indicates that the server is temporarily unavailable, usually due to high load or maintenance.
		/// </summary>
		ServiceUnavailable = HttpStatusCode.ServiceUnavailable,

		/// <summary>	
		///	Equivalent to HTTP status 504.
		///	<see cref="HttpStatusCode.GatewayTimeout"/> indicates that an intermediate proxy server timed out while waiting for a response from another proxy or the origin server.
		/// </summary>
		GatewayTimeout = HttpStatusCode.GatewayTimeout,

		/// <summary>	
		///	Equivalent to HTTP status 505.
		///	<see cref="HttpStatusCode.HttpVersionNotSupported"/> indicates that the requested HTTP version is not supported by the server.
		/// </summary>
		HttpVersionNotSupported = HttpStatusCode.HttpVersionNotSupported,

		/// <summary>	
		///	Equivalent to HTTP status 506.
		///	<see cref="HttpStatusCode.VariantAlsoNegotiates"/> indicates that the chosen variant resource is configured to engage in transparent content negotiation itself and,
		///	therefore, isn't a proper endpoint in the negotiation process.
		/// </summary>
		VariantAlsoNegotiates = HttpStatusCode.VariantAlsoNegotiates,

		/// <summary>	
		///	Equivalent to HTTP status 507.
		///	<see cref="HttpStatusCode.InsufficientStorage"/> indicates that the server is unable to store the representation needed to complete the request.
		/// </summary>
		InsufficientStorage = HttpStatusCode.InsufficientStorage,

		/// <summary>	
		///	Equivalent to HTTP status 508.
		///	<see cref="HttpStatusCode.LoopDetected"/> indicates that the server terminated an operation because it encountered an infinite loop while
		///	processing a WebDAV request with "Depth: infinity".
		///	This status code is meant for backward compatibility with clients not aware of the 208 status code AlreadyReported appearing in multistatus response bodies.
		/// </summary>
		LoopDetected = HttpStatusCode.LoopDetected,

		/// <summary>	
		///	Equivalent to HTTP status 510.
		///	<see cref="HttpStatusCode.NotExtended"/> indicates that further extensions to the request are required for the server to fulfill it.
		/// </summary>
		NotExtended = HttpStatusCode.NotExtended,

		/// <summary>	
		///	Equivalent to HTTP status 511.
		///	<see cref="HttpStatusCode.NetworkAuthenticationRequired"/> indicates that the client needs to authenticate to gain network access;
		///	it's intended for use by intercepting proxies used to control access to the network.
		/// </summary>	
		NetworkAuthenticationRequired = HttpStatusCode.NetworkAuthenticationRequired,

		#endregion

		#endregion

		/// <summary>
		/// <see cref="StArgumentNull"/> Equivalent to HTTP status 418.
		/// ورودی خالی پذیرفته نمی باشد
		/// </summary>
		StArgumentNull = 418,

		/// <summary>
		/// <see cref="StAccessDenied"/> Equivalent to HTTP status 419.
		/// دسترسی غیرمجاز
		/// </summary>
		StAccessDenied = 419,

		/// <summary>
		/// <see cref="StDataDublicate"/> Equivalent to HTTP status 420.
		/// کاربر گرامی: "اطلاعات" تکراری می باشد
		/// </summary>
		StDataDublicate = 420,

		/// <summary>
		/// <see cref="StIncorrectData"/> Equivalent to HTTP status 425.
		/// کاربر گرامی: "اطلاعات" نادرست می باشد
		/// </summary>
		StIncorrectData = 425,

		/// <summary>
		/// <see cref="StSaveError"/> Equivalent to HTTP status 509.
		/// خطا در ذخیره سازی اطلاعات
		/// </summary>
		StSaveError = 509,

		/// <summary>
		/// <see cref="StDataNotFound"/> Equivalent to HTTP status 512.
		/// داده ای موجود نمی باشد
		/// </summary>
		StDataNotFound = 512,

		/// <summary>
		/// <see cref="StFileNotFound"/> Equivalent to HTTP status 513.
		/// فایل پیدا نشد
		/// </summary>
		StFileNotFound = 513,

		/// <summary>
		/// <see cref="StKeyNotFound"/> Equivalent to HTTP status 514.
		/// داده ای با این کلید یافت نشد
		/// </summary>
		StKeyNotFound = 514,

		/// <summary>
		/// <see cref="StDirectoryNotFound"/> Equivalent to HTTP status 515.
		/// سامانه مسیر را پیدا نمی کند
		/// </summary>
		StDirectoryNotFound = 515,

		/// <summary>
		/// <see cref="StHttpCallInvalidOperationException"/> Equivalent to HTTP status 516.
		/// سامانه نتوانست نشانی را هماهنگ کند
		/// </summary>
		StHttpCallInvalidOperationException = 516,

		/// <summary>
		/// <see cref="StHttpCallRequestException"/> Equivalent to HTTP status 517.
		/// سامانه نتوانست درخواست را بفرستد : مشکل شبکه ,دسترسی یا دی ان اس
		/// </summary>
		StHttpCallRequestException = 517,

		/// <summary>
		/// <see cref="StHttpCallTaskCanceledException"/> Equivalent to HTTP status 518.
		/// فرستادن درخواست با درنگ زیاد انجام نشد
		/// </summary>
		StHttpCallTaskCanceledException = 518,

		/// <summary>
		/// <see cref="StHttpCallUriFormatException"/> Equivalent to HTTP status 519.
		/// نشانی ناهمگون است
		/// </summary>
		StHttpCallUriFormatException = 519,

		/// <summary>
		/// <see cref="StManagedError"/> Equivalent to HTTP status 520.
		/// خطائی شناخته شده را سامانه مدیریت کرده است
		/// </summary>
		StManagedError = 520,

		/// <summary>
		/// <see cref="StChainOfRespLevelViolationError"/> Equivalent to HTTP status 521.
		/// خطائی در پیشرفت مراحل کار رخ داده است
		/// </summary>
		StChainOfRespLevelViolationError = 521,

		/// <summary>
		/// <see cref="StNotAcceptedResult"/> Equivalent to HTTP status 522.
		/// نتیجه پذیرفته نمی باشد
		/// </summary>
		StNotAcceptedResult = 522,

		/// <summary>
		/// <see cref="StInquiryNotAcceptable"/> Equivalent to HTTP status 523.
		/// نتیجه ی اعتبارسنجی پذیرفته نمی باشد
		/// </summary>
		StInquiryNotAcceptable = 523,

		/*/// <summary>
		/// <see cref="StChainOfRespLevelViolationNoInquiryError"/> Equivalent to HTTP status 524.
		/// خطائی در پیشرفت مراحل کار رخ داده است
		/// اعتبارسنجی انجام نشده است
		/// </summary>
		StChainOfRespLevelViolationNoInquiryError = 524,*/
	}
}