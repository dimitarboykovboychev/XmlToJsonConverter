using Interfaces;
using Newtonsoft.Json;
using System.IO;
using System.Threading.Tasks;
using System.Net.Http;
using System.Text;
using System.Xml;

namespace Data.Services
{
	public class ConversionService: IConversionService
	{
		public async Task<bool> ProcessUploadedFile(string fileName, string path, Stream stream)
		{
			//var validationStream = new MemoryStream();

			//await stream.CopyToAsync(validationStream);

			this.ValidateXML(stream);

			var response = new HttpResponseMessage();
			var content = this.StreamToString(stream);

			var httpContent = new StringContent(content, Encoding.UTF8, "application/xml");

			using(var client = new HttpClient())
			{
				response = await client.PostAsync(Constants.ApiUrl, httpContent);

				if(response.IsSuccessStatusCode)
				{
					var responseContent = await response.Content.ReadAsStringAsync();

					var json = JsonConvert.SerializeObject(responseContent);
					var fullPath = Path.Combine(path, Path.ChangeExtension(fileName, Constants.JsonExtension));

					System.IO.File.WriteAllText(fullPath, json);
				}
			}

			return response.IsSuccessStatusCode;
		}

		private string StreamToString(Stream stream)
		{
			using(StreamReader reader = new StreamReader(stream))
			{
				return reader.ReadToEnd();
			}
		}

		private void ValidateXML(Stream validationStream)
		{
			try
			{
				XmlDocument doc = new XmlDocument();

				XmlReaderSettings settings = new XmlReaderSettings();
				settings.ConformanceLevel = ConformanceLevel.Fragment;
				settings.IgnoreWhitespace = true;

				using(XmlReader reader = XmlReader.Create(validationStream, settings))
				{
					while(reader.Read())
					{
						if(reader.NodeType == XmlNodeType.Element)
						{
							doc.ReadNode(reader);
						}
					}
				}
			}
			catch
			{
				throw new System.Exception("Invalid XML!");
			}
		}
	}
}
