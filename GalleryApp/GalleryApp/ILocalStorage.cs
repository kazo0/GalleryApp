using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GalleryApp
{
	public interface ILocalStorage
	{
		Task Store(string fileName);
		Task<List<string>> Get();
	}
}
