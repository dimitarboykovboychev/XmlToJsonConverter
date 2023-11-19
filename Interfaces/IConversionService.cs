using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Xml;

namespace Interfaces
{
	public interface IConversionService
	{
		Task<IEnumerable<string>> ProcessUploadedFile(string fileName, string path, Stream stream);
	}
}
