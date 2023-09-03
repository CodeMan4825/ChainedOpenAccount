using Newtonsoft.Json;
using System.Net;
using System.Net.Http.Json;

namespace OpenAccount.Publics
{
	public static class HttpClients
	{
		public static HttpClient CreateClientWithCustomHeaders(Dictionary<string, string>? defaultRequestHeaders)
		{
			var httpClient = new HttpClient();
			if (defaultRequestHeaders != null)
				foreach (var item in defaultRequestHeaders)
					httpClient.DefaultRequestHeaders.Add(item.Key, item.Value);
			return httpClient;
		}

		/// <summary>
		/// Post data and return result.
		/// </summary>
		/// <typeparam name="TResponse"></typeparam>
		/// <param name="httpClient"></param>
		/// <param name="baseAddress">httpClient.BaseAddress = new Uri(baseAddress)</param>
		/// <param name="requestUri">HttpRequestMessage(HttpMethod.Post, requestUri)</param>
		/// <param name="request">JsonContent.Create(request)</param>
		/// <param name="userHeaders">httpMessage.Headers.Add</param>
		/// <returns><typeparamref name="TResponse"/></returns>
		public static async Task<TResponse?> Post<TResponse>(HttpClient httpClient, string baseAddress, string requestUri, object request, Dictionary<string, string>? userHeaders = null)
		{
			try
			{
				httpClient.BaseAddress = new Uri(baseAddress);
				var httpMessage = new HttpRequestMessage(HttpMethod.Post, requestUri);
				if (userHeaders != null)
					foreach (var header in userHeaders)
						httpMessage.Headers.Add(header.Key, header.Value);
				httpMessage.Content = JsonContent.Create(request);
				var resp = await httpClient.SendAsync(httpMessage);
				var res = await resp.Content.ReadAsStringAsync();
				var result = JsonConvert.DeserializeObject<TResponse>(res);
				return result;
			}
			finally { httpClient.Dispose(); }
		}

		public static async Task<TResponse?> Get<TResponse>(HttpClient httpClient, string baseAddress, string requestUri, Dictionary<string, string>? userHeaders = null)
		{
			try
			{
				if (!string.IsNullOrEmpty(baseAddress))
					httpClient.BaseAddress = new Uri(baseAddress);
				var httpMessage = new HttpRequestMessage(HttpMethod.Get, requestUri);
				if (userHeaders != null)
					foreach (var header in userHeaders)
						httpMessage.Headers.Add(header.Key, header.Value);

				var resp = await httpClient.SendAsync(httpMessage);
				var res = await resp.Content.ReadAsStringAsync();
				var result = JsonConvert.DeserializeObject<TResponse>(res);
				return result;
			}
			finally { httpClient.Dispose(); }
		}

		/// <summary>
		/// Put data and return result.
		/// </summary>
		/// <typeparam name="TResponse"></typeparam>
		/// <param name="httpClient"></param>
		/// <param name="baseAddress">httpClient.BaseAddress = new Uri(baseAddress)</param>
		/// <param name="requestUri">HttpRequestMessage(HttpMethod.Post, requestUri)</param>
		/// <param name="request">JsonContent.Create(request)</param>
		/// <param name="userHeaders">httpMessage.Headers.Add</param>
		/// <returns><typeparamref name="TResponse"/></returns>
		public static async Task<TResponse?> Put<TResponse>(HttpClient httpClient, string baseAddress, string requestUri, object request, Dictionary<string, string>? userHeaders = null)
		{
			try
			{
				httpClient.BaseAddress = new Uri(baseAddress);
				var httpMessage = new HttpRequestMessage(HttpMethod.Put, requestUri);
				if (userHeaders != null)
					foreach (var header in userHeaders)
						httpMessage.Headers.Add(header.Key, header.Value);
				httpMessage.Content = JsonContent.Create(request);
				var resp = await httpClient.SendAsync(httpMessage);
				var res = await resp.Content.ReadAsStringAsync();
				var result = JsonConvert.DeserializeObject<TResponse>(res);
				return result;
			}
			finally { httpClient.Dispose(); }
		}

		/// <summary>
		/// Post data to Minio
		/// </summary>
		/// <typeparam name="TResponse"></typeparam>
		/// <param name="baseAddress"></param>
		/// <param name="requestUri"></param>
		/// <param name="subSystemId"></param>
		/// <param name="formFile"></param>
		/// <param name="contentType"></param>
		/// <param name="fileName"></param>
		/// <returns></returns>
		public static async Task<TResponse?> PostMinIo<TResponse>(string baseAddress, string requestUri,
			string subSystemId, ByteArrayContent formFile, string contentType, string fileName = "")
		{
			var client = new HttpClient();
			using (var multipartFormContent = new MultipartFormDataContent())
				try
				{
					multipartFormContent.Add(new StringContent(subSystemId), name: "SubSystemId");
					if (!string.IsNullOrEmpty(fileName))
						multipartFormContent.Add(new StringContent(fileName), name: "FileName");

					formFile.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(contentType);
					multipartFormContent.Add(formFile, name: "file", fileName: "file_name");
					var response = await client.PostAsync(baseAddress + requestUri, multipartFormContent);
					var result = await response.Content.ReadAsStringAsync();
					var res1 = JsonConvert.DeserializeObject<TResponse>(result);
					return res1;
				}
				finally { client.Dispose(); }
		}

		/// <summary>
		/// Post data and return result.
		/// </summary>
		/// <param name="httpClient"></param>
		/// <param name="baseAddress">httpClient.BaseAddress = new Uri(baseAddress)</param>
		/// <param name="requestUri">HttpRequestMessage(HttpMethod.Post, requestUri)</param>
		/// <returns><typeparamref name="TResponse"/></returns>
		public static async Task<byte[]> DownloadMinIo(HttpClient httpClient, string baseAddress, string requestUri)
		{
			httpClient.BaseAddress = new Uri(baseAddress);
			var httpMessage = new HttpRequestMessage(HttpMethod.Post, requestUri);
			var resp = await httpClient.SendAsync(httpMessage);
			var res = await resp.Content.ReadAsStreamAsync();
			httpClient.Dispose();
			if (res != null)
			{
				var memoryStream = new MemoryStream();
				res.CopyTo(memoryStream);
				return memoryStream.ToArray();
			}
			else
				throw StException.ServiceUnavailable("");
		}
	}
}