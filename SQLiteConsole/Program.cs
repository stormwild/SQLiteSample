using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using SQLiteSample.Persistence;

namespace SQLiteConsole
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine("List of Artists");

			var displayArtists = DisplayArtists();
			displayArtists.Wait();

			/*Console.WriteLine("List of Albums");

			var displayAlbums = DisplayAlbums();
			displayAlbums.Wait();*/

			Console.ReadLine();
		}

		private static async Task DisplayAlbums()
		{
			using (var ctx = new ChinookContext())
			{	
				var albums = ctx.Albums.Where(album => album.Title.StartsWith("A"));
				await albums.ForEachAsync(album => Console.WriteLine(album.Title));
			}
		}

		private static async Task DisplayArtists()
		{
			using (var ctx = new ChinookContext())
			{
				var artists = ctx.Artists.Where(artist => artist.Name.StartsWith("A"));
				await artists.ForEachAsync(artist => Console.WriteLine(artist.Name));
			}
		}

	}
}
