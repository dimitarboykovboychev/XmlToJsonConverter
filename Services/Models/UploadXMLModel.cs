namespace Data.Models
{
	public class UploadXMLModel
	{
		public UploadXMLModel(string content, string path, string fileName)
		{
			this.Content = content;
			this.Path = path;
			this.FileName = fileName;
		}

		public string Content;

		public string Path;

		public string FileName;
	}
}