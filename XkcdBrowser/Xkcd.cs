﻿using HtmlAgilityPack;
using System.Collections.Generic;
using System.Linq;

namespace XkcdBrowser
{
	/// <summary>
	/// Browse the xkcd comics
	/// </summary>
	public static class Xkcd
	{
		#region Properties
		private static Dictionary<int, ComicArchiveEntry> _comicDictionary { get; set; }

		/// <summary>
		/// A dictionary of all comics, keyed by comic ID
		/// </summary>
		public static Dictionary<int, ComicArchiveEntry> ComicDictionary
		{
			get
			{
				if (_comicDictionary == null)
				{
					_comicDictionary = GetComicDictionary();
				}
				return _comicDictionary;
			}
		}
		#endregion

		#region Private Methods
		/// <summary>
		/// Gets a dictionary of comic from the xkcd archive
		/// </summary>
		/// <returns>Dictionary of comics, keyed on comic ID</returns>
		private static Dictionary<int, ComicArchiveEntry> GetComicDictionary()
		{
			var web = new HtmlWeb();
			HtmlDocument doc = web.Load("https://xkcd.com/archive/");
			HtmlNode middleContainer = doc.DocumentNode.Descendants().FirstOrDefault(x => x.Id == "middleContainer");
			return middleContainer.Descendants().Where(x => x.Name == "a").Select(a => new ComicArchiveEntry
			{
				Id = int.Parse(a.Attributes["href"].Value.Split('/')[1]),
				Date = a.Attributes["title"].Value,
				Title = a.InnerText
			}).ToDictionary(x => x.Id);
		}

		/// <summary>
		/// Gets a comic from a comic archive entry, comic url, and html document
		/// </summary>
		/// <param name="comicArchiveEntry">Comic archive entry</param>
		/// <param name="url">Comic permalink</param>
		/// <param name="doc">Comic webpage html document</param>
		/// <returns>Comic object</returns>
		private static Comic GetComic(ComicArchiveEntry comicArchiveEntry, string url, HtmlDocument doc)
		{
			string imageUrl = string.Empty;
			string altText = string.Empty;

			switch (comicArchiveEntry.Id)
			{
				case 1350:
					imageUrl = "https://imgs.xkcd.com/comics/shouldnt_be_hard.png";
					altText = "Every choice, no matter how small, begins a new story.";
					break;
				case 1416:
					imageUrl = "https://imgs.xkcd.com/comics/pixels.png";
					altText = "It's turtles all the way down.";
					break;
				case 1506:
					imageUrl = "https://imgs.xkcd.com/comics/xkcloud.png";
					altText = string.Empty; // No alt text for this comic
					break;
				case 1525:
					imageUrl = "https://imgs.xkcd.com/comics/emojic_8_ball.png";
					altText = string.Empty; // No alt text for this comic
					break;
				case 1608:
					// This comic is interactive, and difficult.
					// TODO: I should find a better way to display this one
					imageUrl = string.Empty;
					altText = string.Empty;
					break;
				case 1663:
					imageUrl = string.Empty; // Interactive with no static image
					altText = "Relax";
					break;
				case 2067:
					imageUrl = "https://imgs.xkcd.com/comics/challengers.png";
					altText = "Use your mouse or fingers to pan + zoom. To edit the map, submit your ballot on November 6th.";
					break;
				case 2198:
					imageUrl = "https://imgs.xkcd.com/comics/throw.png";
					altText = "The keys to successfully throwing a party are location, planning, and one of those aircraft carrier steam catapults.";
					break;
				default:
					HtmlNode headNode = doc.DocumentNode.Descendants().Where(x => x.Name == "head").FirstOrDefault();
					HtmlNode metaImageNode = headNode.Descendants().Where(x =>
						x.Name == "meta" && x.Attributes["property"] != null && x.Attributes["property"].DeEntitizeValue == "og:image")
						.FirstOrDefault();
					HtmlNode comicNode = doc.DocumentNode.Descendants().Where(y => y.Id == "comic").FirstOrDefault();
					HtmlNode comicImageNode = comicNode.Descendants().Where(x => x.Name == "img").FirstOrDefault();

					if (metaImageNode != null && metaImageNode.Attributes["content"] != null)
					{
						imageUrl = metaImageNode.Attributes["content"].DeEntitizeValue;
					}
					if (imageUrl == string.Empty && comicImageNode != null && comicImageNode.Attributes["src"] != null)
					{
						imageUrl = $"https:{comicImageNode.Attributes["src"].DeEntitizeValue}";
					}
					if (comicImageNode != null && comicImageNode.Attributes["title"] != null)
					{
						altText = comicImageNode.Attributes["title"].DeEntitizeValue;
					}
					break;
			}

			return new Comic
			{
				Id = comicArchiveEntry.Id,
				ImageUrl = imageUrl,
				AltText = altText,
				Title = comicArchiveEntry.Title,
				PermaLink = url,
				Date = comicArchiveEntry.Date
			};
		}
		#endregion

		#region Public Methods
		/// <summary>
		/// Gets a comic from a comic archive entry
		/// </summary>
		/// <param name="comicArchiveEntry">Comic archive entry</param>
		/// <returns>Comic object</returns>
		public static Comic GetComic(ComicArchiveEntry comicArchiveEntry)
		{
			var permaLink = $"https://xkcd.com/{comicArchiveEntry.Id}/";
			var web = new HtmlWeb();
			HtmlDocument doc = web.Load(permaLink);
			return GetComic(comicArchiveEntry, permaLink, doc);
		}

		/// <summary>
		/// Gets a comic by ID
		/// </summary>
		/// <param name="id">Comic ID</param>
		/// <returns>Requested comic</returns>
		public static Comic GetComic(int id)
		{
			if (ComicDictionary.Keys.Contains(id))
			{
				return GetComic(ComicDictionary[id]);
			}
			return null;
		}

		/// <summary>
		/// Gets the latest comic
		/// </summary>
		/// <returns>The latest comic</returns>
		public static Comic GetLatestComic()
		{
			ComicArchiveEntry latest = ComicDictionary.OrderByDescending(x => x.Key).FirstOrDefault().Value;
			return GetComic(latest);
		}

		/// <summary>
		/// Gets the first comic (ID=1)
		/// </summary>
		/// <returns>The first comic</returns>
		public static Comic GetFirstComic()
		{
			ComicArchiveEntry first = ComicDictionary.OrderBy(x => x.Key).FirstOrDefault().Value;
			return GetComic(first);
		}

		/// <summary>
		/// Gets a random comic, using the xkcd random comic url
		/// TODO: Replace this with a local random getter, using the archive dictionary
		/// </summary>
		/// <returns>Random comic</returns>
		public static Comic GetRandomComic()
		{
			var randomUrl = "https://c.xkcd.com/random/comic/";
			var web = new HtmlWeb();
			HtmlDocument doc = web.Load(randomUrl);
			HtmlNode headNode = doc.DocumentNode.Descendants().Where(x => x.Name == "head").FirstOrDefault();
			HtmlNode metaUrlNode = headNode.Descendants().Where(x =>
				x.Name == "meta" && x.Attributes["property"] != null && x.Attributes["property"].DeEntitizeValue == "og:url")
				.FirstOrDefault();
			var url = metaUrlNode.Attributes["content"].DeEntitizeValue;
			var urlPieces = url.Split('/');
			var id = int.Parse(urlPieces[urlPieces.Length - 2]);
			ComicArchiveEntry comicListEntry = ComicDictionary[id];
			return GetComic(comicListEntry, url, doc);
		}

		/// <summary>
		/// Refresh the comic dictionary, reloading the entire archive webpage
		/// </summary>
		public static void RefreshComicDictionary()
		{
			_comicDictionary = GetComicDictionary();
		}
		#endregion
	}
}
