using System.IO;
using System.Threading.Tasks;
using System.Net.Http;
using System.Text;
using System.Xml;
using Data.Interfaces;

namespace Data.Services
{
	public class ConversionService: IConversionService
	{
		private readonly IApiCaller apiCaller;

		public ConversionService(IApiCaller apiCaller)
		{
			this.apiCaller = apiCaller;
		}

		public async Task<bool> ProcessUploadedFile(string fileName, string path, Stream stream)
		{
			var content = this.StreamToString(stream);

			if(!this.ValidateXML(content))
			{
				throw new System.Exception(Constants.InvalidXMLException);
			}

			var httpContent = new StringContent(content, Encoding.UTF8, "application/xml");

			var response = await apiCaller.CallApi(httpContent);
			
			if(response.IsSuccessStatusCode)
			{
				var json = await response.Content.ReadAsStringAsync();
				var fullPath = Path.Combine(path, Path.ChangeExtension(fileName, Constants.JsonExtension));

				new Utilities.Utilities().TrimXMLTags(ref json);

				try
				{
					System.IO.File.WriteAllText(fullPath, json);
				}
				catch
				{
					throw new System.Exception(Constants.FileSystemErrorException);
				}
			}
			else
			{
				throw new System.Exception(Constants.InternalServerErrorException);
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

		private bool ValidateXML(string content)
		{
			try
			{
				XmlDocument doc = new XmlDocument();
				doc.LoadXml(content);
			}
			catch
			{
				return false;
			}

			return true;
		}
	}
}
