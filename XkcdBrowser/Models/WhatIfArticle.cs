using System.Linq;

namespace XkcdBrowser.Models
{
	public class WhatIfArticle
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
		/// Article title
		/// </summary>
		public string Title { get; set; }

		/// <summary>
		/// Article date
		/// </summary>
		public string Date { get; set; }

		/// <summary>
		/// Article HTML
		/// </summary>
		public string Html { get; set; }

		/// <summary>
		/// Gets the next What If article
		/// </summary>
		/// <returns>Next What If article, null if no next article</returns>
		public WhatIfArticle Next()
		{
			WhatIfArchiveEntry nextArchiveEntry = WhatIf.WhatIfDictionary.OrderBy(x => x.Key).SkipWhile(x => x.Key <= Id).FirstOrDefault().Value;
			if (nextArchiveEntry != null)
			{
				return WhatIf.GetArticle(nextArchiveEntry);
			}
			return null;
		}

		/// <summary>
		/// Gets the previous What If article
		/// </summary>
		/// <returns>Previous What If article, null if no previous article</returns>
		public WhatIfArticle Previous()
		{
			WhatIfArchiveEntry prevArchiveEntry = WhatIf.WhatIfDictionary.OrderByDescending(x => x.Key).SkipWhile(x => x.Key >= Id).FirstOrDefault().Value;
			if (prevArchiveEntry != null)
			{
				return WhatIf.GetArticle(prevArchiveEntry);
			}
			return null;
		}
	}
}
