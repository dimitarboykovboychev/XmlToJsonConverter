using Newtonsoft.Json;
using System.Web.Http;
using System.Xml;

namespace XmlToJsonAPI.Controllers
{
	public class ValuesController: ApiController
	{
		[HttpPost]
		public string Index([FromBody] XmlDocument content)
		{
			return JsonConvert.SerializeXmlNode(content);
		}
	}
}
