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
		}
	}
}
