using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace GalleryApp
{
	public class FormsLocalStorage : ILocalStorage
	{
		public const string FavoritePhotosKey = "FavoritePhotos";

		public async Task Store(string fileName)
		{
			var fileNames = await Get();
			fileNames.Add(fileName);

			Application.Current.Properties[FavoritePhotosKey] = JsonConvert.SerializeObject(fileNames);
			await Application.Current.SavePropertiesAsync();
		}

		public async Task<List<string>> Get()
		{
			if (Application.Current.Properties.ContainsKey(FavoritePhotosKey))
			{
				var fileNames = (string) Application.Current.Properties[FavoritePhotosKey];

				return JsonConvert.DeserializeObject<List<string>>(fileNames);
			}

			return new List<string>();
		}
	}
}
