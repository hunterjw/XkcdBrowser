namespace XkcdBrowser.Examples
{
	class Program
	{
		static void Main(string[] args)
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
			var comicDict = Xkcd.ComicDictionary;

			// Refresh the comic dictionary
			Xkcd.RefreshComicDictionary();

			// Set a custom location for the cache db (uses LiteDB)
			Xkcd.DatabaseLocation = @"C:\Temp\xkcd.db";
		}
	}
}
