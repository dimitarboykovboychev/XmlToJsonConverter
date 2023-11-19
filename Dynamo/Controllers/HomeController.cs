using Dynamo_task.Models;
using Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Dynamo_task.Controllers
{
	public class HomeController: Controller
	{
		private readonly IConversionService mainService;

		public HomeController(IConversionService mainService)
		{
			this.mainService = mainService;
		}

		public ActionResult Index()
		{
			return View();
		}

		[HttpPost]
		public async Task<ActionResult> Index(IEnumerable<HttpPostedFileBase> models, string filePath)
		{
			foreach (var file in models)
			{
				await this.mainService.ProcessUploadedFile(file.FileName, filePath, file.InputStream);
			}

			return View();
		}
	}
}