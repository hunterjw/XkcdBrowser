namespace XkcdBrowser.Models
{
	/// <summary>
	/// A comic archive list entry
	/// </summary>
	public class ComicArchiveEntry
	{
		/// <summary>
		/// Comic ID
		/// </summary>
		public int Id { get; set; }

		/// <summary>
		/// Date text string (YYYY-M-D)
		/// </summary>
		public string Date { get; set; }

		/// <summary>
		/// Comic title
		/// </summary>
		public string Title { get; set; }

		/// <summary>
		/// Comic for this archive entry
		/// </summary>
		public Comic Comic => Xkcd.GetComic(this);
	}
}
