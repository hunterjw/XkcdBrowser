namespace XkcdBrowser
{
	/// <summary>
	/// A xkcd comic
	/// </summary>
	public class Comic
	{
		/// <summary>
		/// Comic ID
		/// </summary>
		public int Id { get; set; }

		/// <summary>
		/// Comic Title (the part that is directly visible usually)
		/// </summary>
		public string Title { get; set; }

		/// <summary>
		/// Comic image hover text
		/// </summary>
		public string AltText { get; set; }

		/// <summary>
		/// URL to the comic image
		/// </summary>
		public string ImageUrl { get; set; }

		/// <summary>
		/// Link to to the comic webpage
		/// </summary>
		public string PermaLink { get; set; }

		/// <summary>
		/// Date text string (YYYY-M-D)
		/// </summary>
		public string Date { get; set; }
	}
}
