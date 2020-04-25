using System.Linq;

namespace XkcdBrowser.Models
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

		/// <summary>
		/// Gets the next comic
		/// </summary>
		/// <returns>Next comic, or null if no next comic</returns>
		public Comic Next()
		{
			ComicArchiveEntry nextArchiveEntry = Xkcd.ComicDictionary.OrderBy(x => x.Key).SkipWhile(x => x.Key <= Id).FirstOrDefault().Value;
			if (nextArchiveEntry != null)
			{
				return Xkcd.GetComic(nextArchiveEntry);
			}
			return null;
		}

		/// <summary>
		/// Gets the previous comic
		/// </summary>
		/// <returns>Previous comic, or null if no previous comic</returns>
		public Comic Previous()
		{
			ComicArchiveEntry nextArchiveEntry = Xkcd.ComicDictionary.OrderByDescending(x => x.Key).SkipWhile(x => x.Key >= Id).FirstOrDefault().Value;
			if (nextArchiveEntry != null)
			{
				return Xkcd.GetComic(nextArchiveEntry);
			}
			return null;
		}

		/// <summary>
		/// Link to the Explain xkcd article for this comic
		/// </summary>
		public string ExplainLink => $"https://www.explainxkcd.com/wiki/index.php/{Id}";
	}
}
