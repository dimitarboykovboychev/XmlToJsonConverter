using System.IO;
using System.Threading.Tasks;

namespace Data.Interfaces
{
	public interface IConversionService
	{
		Task<bool> ProcessUploadedFile(string fileName, string path, Stream stream);
	}
}
