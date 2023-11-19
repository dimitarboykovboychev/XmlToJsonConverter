using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml;

namespace Dynamo_task.Models
{
	public class UploadXMLModel
	{
		public XmlDocument Content;

		public string Path;

		public string FileName;
	}
}