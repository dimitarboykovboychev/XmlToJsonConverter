using System.IO;
using System.Threading.Tasks;

namespace Interfaces
{
	public interface IConversionService
	{
		Task<bool> ProcessUploadedFile(string fileName, string path, Stream stream);
	}
}
