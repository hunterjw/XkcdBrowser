using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using XkcdBrowser.Models;

namespace XkcdBrowser
{
	/// <summary>
	/// Browse What If articles
	/// </summary>
	public static class WhatIf
	{
		#region Properties
		private static Dictionary<int, WhatIfArchiveEntry> PrivateWhatIfDictionary { get; set; }

		/// <summary>
		/// A dictionary of articles, keyed by article ID
		/// </summary>
		public static Dictionary<int, WhatIfArchiveEntry> WhatIfDictionary
		{
			get
			{
				if (PrivateWhatIfDictionary == null)
				{
					PrivateWhatIfDictionary = GetWhatIfDictionary(false);
				}
				return PrivateWhatIfDictionary;
			}
		}
		#endregion

		#region Private Methods
		/// <summary>
		/// Gets a dictionary of articles from the What If archive
		/// </summary>
		/// <param name="forceRefresh"></param>
		/// <returns></returns>
		private static Dictionary<int, WhatIfArchiveEntry> GetWhatIfDictionary(bool forceRefresh)
		{
			List<WhatIfArchiveEntry> articleArchiveList = XkcdDatabase.WhatIfArchiveEntries;
			if (articleArchiveList.Count() < 1 || forceRefresh)
			{
				var web = new HtmlWeb();
				HtmlDocument doc = web.Load("https://whatif.xkcd.com/archive/");
				HtmlNode archiveContainer = doc.DocumentNode.Descendants().FirstOrDefault(x => x.Id == "archive-wrapper");
				articleArchiveList = new List<WhatIfArchiveEntry>();
				foreach (HtmlNode node in archiveContainer.Descendants().Where(x => x.HasClass("archive-entry")))
				{
					HtmlNode archiveTitleLinkNode = node.Descendants().FirstOrDefault(x => x.HasClass("archive-title")).Descendants().FirstOrDefault(y => y.Name == "a");
					var permaLink = $"https:{archiveTitleLinkNode.Attributes["href"].DeEntitizeValue}";
					var permaLinkParts = permaLink.Split('/');
					articleArchiveList.Add(new WhatIfArchiveEntry
					{
						Id = int.Parse(permaLinkParts[permaLinkParts.Length - 2]),
						PermaLink = permaLink,
						Image = $"https://what-if.xkcd.com{node.Descendants().FirstOrDefault(x => x.HasClass("archive-image")).Attributes["src"].DeEntitizeValue}",
						Title = archiveTitleLinkNode.InnerText,
						Date = node.Descendants().FirstOrDefault(x => x.HasClass("archive-date")).InnerText
					});
				}
				XkcdDatabase.WhatIfArchiveEntries = articleArchiveList;
			}
			return articleArchiveList.ToDictionary(x => x.Id);
		}
		#endregion

		#region Public Methods
		/// <summary>
		/// Gets a What If Article from an archive entry
		/// </summary>
		/// <param name="archiveEntry">Archive entry</param>
		/// <returns>Whar If Article</returns>
		public static WhatIfArticle GetArticle(WhatIfArchiveEntry archiveEntry)
		{
			WhatIfArticle article = XkcdDatabase.GetWhatIfArticle(archiveEntry.Id);
			if (article != null)
			{
				return article;
			}
			var web = new HtmlWeb();
			HtmlDocument doc = web.Load(archiveEntry.PermaLink);
			HtmlNode articleContentNode = doc.DocumentNode.Descendants().FirstOrDefault(x => x.Name == "article" && x.HasClass("entry"));
			article = new WhatIfArticle
			{
				Id = archiveEntry.Id,
				PermaLink = archiveEntry.PermaLink,
				Title = archiveEntry.Title,
				Date = archiveEntry.Date,
				Html = articleContentNode.OuterHtml
			};
			XkcdDatabase.InsertOrUpdateWhatIfArticle(article);
			return article;
		}

		/// <summary>
		/// Get a What If article by ID
		/// </summary>
		/// <param name="id">Article ID</param>
		/// <returns>What If article if found, null otherwise</returns>
		public static WhatIfArticle GetArticle(int id)
		{
			WhatIfArticle article = XkcdDatabase.GetWhatIfArticle(id);
			if (article != null)
			{
				return article;
			}
			if (WhatIfDictionary.Keys.Contains(id))
			{
				return GetArticle(WhatIfDictionary[id]);
			}
			return null;
		}

		/// <summary>
		/// Gets the latest What If article
		/// </summary>
		/// <returns>Latest What If article</returns>
		public static WhatIfArticle GetLatestArticle()
		{
			WhatIfArchiveEntry latest = WhatIfDictionary.OrderByDescending(x => x.Key).FirstOrDefault().Value;
			return GetArticle(latest);
		}

		/// <summary>
		/// Gets the first What If article
		/// </summary>
		/// <returns>First What If article</returns>
		public static WhatIfArticle GetFirstArticle()
		{
			WhatIfArchiveEntry first = WhatIfDictionary.OrderBy(x => x.Key).FirstOrDefault().Value;
			return GetArticle(first);
		}

		/// <summary>
		/// Gets a random What If article
		/// </summary>
		/// <returns>Random What If article</returns>
		public static WhatIfArticle GetRandomArticle()
		{
			var rand = new Random();
			var values = WhatIfDictionary.Values.ToList();
			int size = values.Count;
			return GetArticle(values[rand.Next(size)]);
		}
		
		/// <summary>
		/// Forces a refresh of the What If article archive dictionary
		/// </summary>
		public static void RefreshWhatIfDictionary()
		{
			PrivateWhatIfDictionary = GetWhatIfDictionary(true);
		}
		#endregion
	}
}
