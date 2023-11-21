using System.Web.Mvc;
using Microsoft.Practices.Unity;
using Data.Services;
using Unity.Mvc4;
using Data.Interfaces;

namespace Dynamo_task
{
	public static class Bootstrapper
	{
		public static IUnityContainer Initialise()
		{
			var container = BuildUnityContainer();

			DependencyResolver.SetResolver(new UnityDependencyResolver(container));

			return container;
		}

		private static IUnityContainer BuildUnityContainer()
		{
			var container = new UnityContainer();

			RegisterTypes(container);

			return container;
		}

		public static void RegisterTypes(IUnityContainer container)
		{
			container.RegisterType<IConversionService, ConversionService>();
			container.RegisterType<IApiCaller, ApiCaller>();
		}
	}
}