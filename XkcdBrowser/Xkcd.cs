using HtmlAgilityPack;
using System.Linq;

namespace XkcdBrowser
{
	/// <summary>
	/// Browse the xkcd comics
	/// </summary>
	public static class Xkcd
	{
		/// <summary>
		/// Gets a comic by URL
		/// </summary>
		/// <param name="url">URL to the comic</param>
		/// <returns>Requested comic</returns>
		public static Comic GetComic(string url = "https://xkcd.com/")
		{
			// This feels *bad* parsing the HTML, but I'm too lazy to do anything about it
			var web = new HtmlWeb();
			HtmlDocument doc = web.Load(url);
			HtmlNode comicNode = doc.DocumentNode.Descendants().Where(y => y.Id == "comic").FirstOrDefault();
			HtmlNode comicImageNode = comicNode.Descendants().Where(x => x.Name == "img").FirstOrDefault();
			HtmlNode headNode = doc.DocumentNode.Descendants().Where(x => x.Name == "head").FirstOrDefault();
			HtmlNode metaTitleNode = headNode.Descendants().Where(x =>
				x.Name == "meta" && x.Attributes["property"] != null && x.Attributes["property"].DeEntitizeValue == "og:title")
				.FirstOrDefault();
			HtmlNode metaUrlNode = headNode.Descendants().Where(x =>
				x.Name == "meta" && x.Attributes["property"] != null && x.Attributes["property"].DeEntitizeValue == "og:url")
				.FirstOrDefault();
			var permaLink = metaUrlNode.Attributes["content"].DeEntitizeValue;
			var permaLinkPieces = permaLink.Split('/');
			var comic = new Comic
			{
				Id = int.Parse(permaLinkPieces[permaLinkPieces.Length - 2]),
				ImageUrl = $"https:{comicImageNode.Attributes["src"].Value}",
				AltText = comicImageNode.Attributes["title"].DeEntitizeValue,
				Title = metaTitleNode.Attributes["content"].DeEntitizeValue,
				PermaLink = permaLink
			};

			return comic;
		}

		/// <summary>
		/// Gets a comic by ID
		/// </summary>
		/// <param name="id">Comic ID</param>
		/// <returns>Requested comic</returns>
		public static Comic GetComic(int id)
		{
			return GetComic($"https://xkcd.com/{id}/");
		}

		/// <summary>
		/// Gets the latest comic
		/// </summary>
		/// <returns>The latest comic</returns>
		public static Comic GetLatestComic()
		{
			return GetComic();
		}

		/// <summary>
		/// Gets the first comic (ID=1)
		/// </summary>
		/// <returns>The first comic</returns>
		public static Comic GetFirstComic()
		{
			return GetComic(1);
		}

		/// <summary>
		/// Gets a random comic
		/// </summary>
		/// <returns>Random comic</returns>
		public static Comic GetRandomComic()
		{
			return GetComic("https://c.xkcd.com/random/comic/");
		}
	}
}
