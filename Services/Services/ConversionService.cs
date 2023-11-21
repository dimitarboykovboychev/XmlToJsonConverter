using Interfaces;
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
			var response = new HttpResponseMessage();
			var content = this.StreamToString(stream);

			this.ValidateXML(content);

			var httpContent = new StringContent(content, Encoding.UTF8, "application/xml");

			using(var client = new HttpClient())
			{
				response = await client.PostAsync(Constants.ApiUrl, httpContent);

				if(response.IsSuccessStatusCode)
				{
					var json = await response.Content.ReadAsStringAsync();

					var fullPath = Path.Combine(path, Path.ChangeExtension(fileName, Constants.JsonExtension));

					System.IO.File.WriteAllText(fullPath, json.Replace("<string>", string.Empty).Replace("</string>", string.Empty));
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

		private void ValidateXML(string content)
		{
			try
			{
				XmlDocument doc = new XmlDocument();
				doc.LoadXml(content);
			}
			catch
			{
				throw new System.Exception("Invalid XML!");
			}
		}
	}
}
