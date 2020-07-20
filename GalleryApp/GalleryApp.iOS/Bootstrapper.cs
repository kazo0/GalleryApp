using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Autofac;
using Foundation;
using UIKit;

namespace GalleryApp.iOS
{
	public class Bootstrapper : GalleryApp.Bootstrapper
	{
		protected override void PlatformInitialize()
		{
			ContainerBuilder.RegisterType<PhotoImporter>()
				.As<IPhotoImporter>()
				.SingleInstance();
		}
	}
}