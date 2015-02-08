using System.Collections.Generic;

namespace SQLiteSample.Entities
{
	public class Artist
	{
		public long ArtistId { get; set; }

		public string Name { get; set; }

		public virtual ICollection<Album> Albums { get; set; }
	}
}
