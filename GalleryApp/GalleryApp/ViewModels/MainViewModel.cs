using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using GalleryApp.Models;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace GalleryApp.ViewModels
{
	public class MainViewModel : ViewModel
	{
		private readonly IPhotoImporter _photoImporter;
		private readonly ILocalStorage _localStorage;

		public ObservableCollection<Photo> Recent { get; set; }
		public ObservableCollection<Photo> Favorites { get; set; }

		public MainViewModel(
			IPhotoImporter photoImporter,
			ILocalStorage localStorage)
		{
			_photoImporter = photoImporter;
			_localStorage = localStorage;
		}

		public async Task Initialize()
		{
			Recent = await _photoImporter.Get(0, 20);
			await LoadFavorites();

			MessagingCenter.Subscribe<GalleryViewModel>(this, "FavoritesAdded", (sender) =>
			{
				MainThread.BeginInvokeOnMainThread(async () =>
				{
					await LoadFavorites();
				});
			});
		}

		private async Task LoadFavorites()
		{
			var filenames = await _localStorage.Get();
			var favorites = await _photoImporter.Get(filenames, Quality.Low);
			Favorites = favorites;
		}
	}
}
