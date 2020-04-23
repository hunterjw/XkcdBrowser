namespace XkcdBrowser.Models
{
	/// <summary>
	/// What If article archive entry
	/// </summary>
	public class WhatIfArchiveEntry
	{
		/// <summary>
		/// Article ID
		/// </summary>
		public int Id { get; set; }

		/// <summary>
		/// Link to archive entry
		/// </summary>
		public string PermaLink { get; set; }

		/// <summary>
		/// Image for the archive entry
		/// </summary>
		public string Image { get; set; }

		/// <summary>
		/// Article title
		/// </summary>
		public string Title { get; set; }

		/// <summary>
		/// Article date
		/// </summary>
		public string Date { get; set; }

		/// <summary>
		/// Article for this archive entry
		/// </summary>
		public WhatIfArticle Article => WhatIf.GetArticle(this);
	}
}
