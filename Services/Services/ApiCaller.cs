using Data.Interfaces;
using System.Net.Http;
using System.Threading.Tasks;

namespace Data.Services
{
	public class ApiCaller: IApiCaller
	{
		public async Task<HttpResponseMessage> CallApi(StringContent stringContent)
		{
			using(var client = new HttpClient())
			{
				return await client.PostAsync(Constants.ApiUrl, stringContent);
			}
		}
	}
}
