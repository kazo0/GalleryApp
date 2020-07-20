using System;
using System.Collections.Generic;
using System.Text;
using Autofac;

namespace GalleryApp
{
	public static class Resolver
	{
		private static IContainer Container;

		public static void Initialize(IContainer container)
		{
			Container = container;
		}

		public static T Resolve<T>()
		{
			return Container.Resolve<T>();
		}
	}
}
