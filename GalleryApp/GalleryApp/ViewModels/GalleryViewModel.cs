using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using GalleryApp.Models;

namespace GalleryApp.ViewModels
{
	public class GalleryViewModel : ViewModel
	{
		private readonly IPhotoImporter _photoImporter;

		public ObservableCollection<Photo> Photos { get; set; }
		public bool IsBusy { get; set; }

		public GalleryViewModel(
			IPhotoImporter photoImporter)
		{
			_photoImporter = photoImporter;
			Task.Run(Initialize);
		}

		private async Task Initialize()
		{
			IsBusy = true;
			await Task.Delay(3000);
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
