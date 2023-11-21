using Data;
using Data.Interfaces;
using Data.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Dynamo_task.Controllers
{
	public class HomeController: Controller
	{
		private readonly IConversionService conversionService;

		public HomeController(IConversionService conversionService)
		{
			this.conversionService = conversionService;
		}

		public ActionResult Index()
		{
			return View();
		}

		[HttpPost]
		public async Task<ActionResult> Index(IEnumerable<HttpPostedFileBase> models, string filePath)
		{
			var success = true;

			foreach (var file in models)
			{
				try
				{
					success = await this.conversionService.ProcessUploadedFile(file.FileName, filePath, file.InputStream);
				}
				catch (Exception ex)
				{
					success = false;

					Log.Error(ex.Message, ex);

					TempData["Error"] = ex.Message;
				}
			}

			if (success)
			{
				Log.Info(Constants.SuccessMessage);

				TempData["Success"] = Constants.SuccessMessage;
			}

			return View();
		}
	}
}