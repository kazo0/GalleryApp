using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalleryApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GalleryApp.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class GalleryView : ContentPage
	{
		public GalleryView()
		{
			InitializeComponent();
			BindingContext = Resolver.Resolve<GalleryViewModel>();
		}
	}
}