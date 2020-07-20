using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Autofac;
using Autofac.Core;
using GalleryApp.ViewModels;
using Xamarin.Forms;

namespace GalleryApp
{
	public abstract class Bootstrapper
	{
		protected ContainerBuilder ContainerBuilder { get; private set; }

		protected Bootstrapper()
		{
			Initialize();
			FinishInitialization();
		}

		protected abstract void PlatformInitialize();
		
		protected void Initialize()
		{
			ContainerBuilder = new ContainerBuilder();
			ContainerBuilder.RegisterType<MainShell>();

			var currentAssembly = Assembly.GetExecutingAssembly();

			var viewTypes = currentAssembly.DefinedTypes
				.Where(t => t.IsSubclassOf(typeof(ViewModel)) ||
				            t.IsSubclassOf(typeof(ContentPage)));

			foreach (var type in viewTypes)
			{
				ContainerBuilder.RegisterType(type.AsType());
			}

			PlatformInitialize();
		}

		private void FinishInitialization()
		{
			var container = ContainerBuilder.Build();

			Resolver.Initialize(container);
		}
	}
}
