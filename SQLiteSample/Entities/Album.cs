namespace SQLiteSample.Entities
{
	public class Album
	{
		public long AlbumId { get; set; }

		public string Title { get; set; }

		public long ArtistId { get; set; }

		public virtual Artist Artist { get; set; }
	}
}
