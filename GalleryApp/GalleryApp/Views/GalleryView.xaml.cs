using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalleryApp.Models;
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

		private void MenuItem_OnClicked(object sender, EventArgs e)
		{
			if (!Photos.SelectedItems.Any())
			{
				DisplayAlert("No photos", "No photos selected", "OK");
				return;
			}

			var vm = (GalleryViewModel) BindingContext;
			vm.AddFavorites.Execute(Photos.SelectedItems.Cast<Photo>().ToList());

			DisplayAlert("Added", "Selected photos has been added to favorites", "OK");
		}
	}
}