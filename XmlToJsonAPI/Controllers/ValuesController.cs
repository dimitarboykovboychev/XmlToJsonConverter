using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Xml;

namespace XmlToJsonAPI.Controllers
{
	public class ValuesController: ApiController
	{
		// GET api/values
		public IEnumerable<string> Get()
		{
			return new string[] { "value1", "value2" };
		}

		// POST api/values
		public async Task<string> Post([FromBody] string content)
		{
			var doc = new XmlDocument();
			doc.LoadXml(content);

			string jsonText = JsonConvert.SerializeXmlNode(doc);

			var Toshko = 21;

			return jsonText;
		}
	}
}
