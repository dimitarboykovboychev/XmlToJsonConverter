namespace Data.Utilities
{
	public class Utilities
	{
		public Utilities()
		{

		}

		public void TrimXMLTags(ref string content)
		{
			content = content.Replace("<string>", string.Empty).Replace("</string>", string.Empty);
		}
	}
}
