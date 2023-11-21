using System.Net.Http;
using System.Threading.Tasks;

namespace Data.Interfaces
{
	public interface IApiCaller
	{
		Task<HttpResponseMessage> CallApi(StringContent stringContent);
	}
}
