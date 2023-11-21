namespace Data
{
	public static class Constants
	{
		public const string ApiUrl = "http://localhost:44343/api/values";

		public const string JsonExtension = ".json";

		public const string SuccessMessage = "Operation successful!";

		public const string TestFileName = "TestFileName";

		public const string TestPath = "C:\\Users\\PC\\Desktop\\";

		public const string TestInvalidXML = "<note><to>TommyssM<ddds><from>Jani</from><heading>Reminder</heading><body>Don't forget to call!</body></note>";

		public const string TestValidXML = "<note><to>Tommy</to><from>Jani</from><heading>Reminder</heading><body>Don't forget me this weekend!</body></note>";

		public const string ValidJson = "{\"note\":{\"to\":\"Tommy\",\"from\":\"Jani\",\"heading\":\"Reminder\",\"body\":\"Don't forget me this weekend!\"}}";

		public const string InvalidXMLException = "Invalid XML!";
	}
}
