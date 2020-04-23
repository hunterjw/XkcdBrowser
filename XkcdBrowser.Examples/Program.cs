using System.Collections.Generic;
using XkcdBrowser.Models;

namespace XkcdBrowser.Examples
{
	class Program
	{
		static void Main(string[] args)
		{
			ComicExamples();

			WhatIfExamples();

			// Set a custom location for the cache db (uses LiteDB)
			XkcdDatabase.DatabaseLocation = @"C:\Temp\xkcd.db";
		}

		static void ComicExamples()
		{
			// Get the first comic
			Comic first = Xkcd.GetFirstComic();

			// Get a random comic
			Comic random = Xkcd.GetRandomComic();

			// Get the latest comic
			Comic latest = Xkcd.GetLatestComic();

			// Get a specific comic (by ID)
			Comic tenTwentyFour = Xkcd.GetComic(1024);

			// Get the next comic
			Comic tenTwentyFive = tenTwentyFour.Next();

			// Get the previous comic
			Comic tenTwentyThree = tenTwentyFour.Previous();

			// There's nothing before the first comic
			Comic badFirst = first.Previous();

			// ...and nothing after the latest
			Comic badLast = latest.Next();

			// Get the dictionary of comic archive entries, keyed on comic ID
			Dictionary<int, ComicArchiveEntry> comicDict = Xkcd.ComicDictionary;

			// Refresh the comic dictionary
			Xkcd.RefreshComicDictionary();
		}

		static void WhatIfExamples()
		{
			// Get the first What If article
			WhatIfArticle first = WhatIf.GetFirstArticle();

			// Get the latest What If article
			WhatIfArticle latest = WhatIf.GetLatestArticle();

			// Get a random What If article
			WhatIfArticle random = WhatIf.GetRandomArticle();

			// Get a specific What If article by ID
			WhatIfArticle fortyTwo = WhatIf.GetArticle(42);

			// Get the next article
			WhatIfArticle fortyThree = fortyTwo.Next();

			// Get the previous article
			WhatIfArticle fortyOne = fortyTwo.Previous();

			// There's nothing before the first article
			WhatIfArticle badFirst = first.Previous();

			// ...and nothing after the latest
			WhatIfArticle badLast = latest.Next();

			// Get the dictionary of What If article entries, keyed on article ID
			Dictionary<int, WhatIfArchiveEntry> dict = WhatIf.WhatIfDictionary;

			// Refresh the article dictionary
			WhatIf.RefreshWhatIfDictionary();
		}
	}
}
