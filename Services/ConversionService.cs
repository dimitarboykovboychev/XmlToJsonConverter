using Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Xml;
using System.Web;
using System.Net.Http;

namespace Services
{
	public class ConversionService: IConversionService
	{
		public ConversionService()
		{
		}

		public async Task<IEnumerable<string>> ProcessUploadedFile(string fileName, string path, Stream stream)
		{
			var content = this.StreamToString(stream);

			string apiUrl = "http://localhost:44343/api/values";


			using(var client = new HttpClient())
			{
				client.BaseAddress = new Uri(apiUrl);
				client.DefaultRequestHeaders.Accept.Clear();

				var httpContent = new StringContent(content);

				HttpResponseMessage response = await client.PostAsync(apiUrl, httpContent);

				if(response.IsSuccessStatusCode)
				{
					var data = await response.Content.ReadAsStringAsync();
					var table = Newtonsoft.Json.JsonConvert.DeserializeObject<System.Data.DataTable>(data);
				}
			}

			return null;
		}

		public string StreamToString(Stream stream)
		{
			using(StreamReader reader = new StreamReader(stream))
			{
				return reader.ReadToEnd();
			}
		}
	}
}
