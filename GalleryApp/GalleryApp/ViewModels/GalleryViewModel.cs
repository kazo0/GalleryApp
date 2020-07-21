using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using GalleryApp.Models;
using Xamarin.Forms;

namespace GalleryApp.ViewModels
{
	public class GalleryViewModel : ViewModel
	{
		private readonly IPhotoImporter _photoImporter;
		private readonly ILocalStorage _localStorage;

		private int _itemsAdded = 0;
		private int _currentStartIndex = 0;

		public ObservableCollection<Photo> Photos { get; set; }
		public bool IsBusy { get; set; }

		public GalleryViewModel(
			IPhotoImporter photoImporter,
			ILocalStorage localStorage)
		{
			_photoImporter = photoImporter;
			_localStorage = localStorage;
			Task.Run(Initialize);
		}

		public ICommand LoadMore => new Command(async () =>
		{
			_currentStartIndex += 20;
			_itemsAdded = 0;
			var collection = await _photoImporter.Get(_currentStartIndex, 20);
			collection.CollectionChanged += Collection_CollectionChanged;
		});

		public ICommand AddFavorites => new Command<List<Photo>>((photos) =>
		{
			foreach (var photo in photos)
			{
				_localStorage.Store(photo.Filename);
			}
			MessagingCenter.Send(this, "FavoritesAdded");
		});

		private void Collection_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
		{
			foreach (Photo photo in e.NewItems)
			{
				_itemsAdded++;
				Photos.Add(photo);
			}

			if (_itemsAdded == 20)
			{
				var collection = sender as ObservableCollection<Photo>;
				collection.CollectionChanged -= Collection_CollectionChanged;
			}

		}

		private async Task Initialize()
		{
			IsBusy = true;
			Photos = await _photoImporter.Get(0, 20);
			Photos.CollectionChanged += Photos_CollectionChanged;
			await Task.Delay(3000);
			IsBusy = false;
		}

		private void Photos_CollectionChanged(object sender,
			System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
		{
			if (e.NewItems != null && e.NewItems.Count > 0)
			{
				IsBusy = false;
				Photos.CollectionChanged -= Photos_CollectionChanged;
			}
		}
	}
}
