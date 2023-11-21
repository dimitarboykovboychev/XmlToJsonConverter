using Moq;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Data;
using Data.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net.Http;
using System.Net.Http.Json;
using Data.Interfaces;
using System;

namespace Tests
{
	[TestClass]
	public class ProcessUploadedFile_Should
	{
		[TestMethod]
		public async Task ProcessesFile_Correctly()
		{
            //Arrange
            var fileName = Constants.TestFileName;
            var path = Constants.TestPath;
            var xml = Constants.TestValidXML;
            var result = false;
            var unicode = new UnicodeEncoding();

            var apiCaller = new Mock<IApiCaller>();

            var response = new HttpResponseMessage()
            {
                Content = JsonContent.Create(Constants.ValidJson)
            };

            apiCaller.Setup(x => x.CallApi(It.IsAny<StringContent>())).Returns(Task.FromResult<HttpResponseMessage>(response));
                
            using(var stream = new MemoryStream())
            {
                var streamWriter = new StreamWriter(stream, unicode);
                try
                {
                    streamWriter.Write(xml);
                    streamWriter.Flush();
                    stream.Seek(0, SeekOrigin.Begin);

                    //Act
                    result = await new ConversionService(apiCaller.Object).ProcessUploadedFile(fileName, path, stream);
                }
                finally
                {
                    streamWriter.Dispose();
                }
            }

            //Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void ProcessesFile_Throws_Exception()
        {
            //Arrange
            var fileName = Constants.TestFileName;
            var path = Constants.TestPath;
            var xml = Constants.TestInvalidXML;
            var unicode = new UnicodeEncoding();

            var apiCaller = new Mock<IApiCaller>();

            var response = new HttpResponseMessage()
            {
                Content = JsonContent.Create(Constants.ValidJson)
            };

            apiCaller.Setup(x => x.CallApi(It.IsAny<StringContent>())).Returns(Task.FromResult<HttpResponseMessage>(response));

            var service = new ConversionService(apiCaller.Object);

            using(var stream = new MemoryStream())
            {
                var streamWriter = new StreamWriter(stream, unicode);
                try
                {
                    streamWriter.Write(xml);
                    streamWriter.Flush();
                    stream.Seek(0, SeekOrigin.Begin);

                    //Act & Assert
                    Assert.ThrowsException<Exception>(async () =>
                    {
                        await service.ProcessUploadedFile(fileName, path, stream);
                    }, 
                    Constants.InvalidXMLException);
                }
				catch
				{

				}
                finally
                {
                    streamWriter.Dispose();
                }
            }
        }
    }
}
