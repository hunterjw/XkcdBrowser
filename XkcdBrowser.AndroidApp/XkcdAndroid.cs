using System;
using System.IO;
using System.Net;
using Android.Content;
using Android.Graphics;
using Android.Widget;
using XkcdBrowser.Models;

namespace XkcdBrowser.AndroidApp
{
	public class XkcdAndroid
	{
		private Context Context { get; set; }
		private TextView ComicTitleView { get; set; }
		private ImageView ComicImageView { get; set; }
		private string DataFolder { get; set; }

		public XkcdAndroid(Context context, TextView comicTitleView, ImageView comicImageView)
		{
			Context = context;
			ComicTitleView = comicTitleView;
			ComicImageView = comicImageView;
			DataFolder = System.IO.Path.Combine(Context.GetExternalFilesDir("").ToString(), "XkcdData");
			SetupDatabase();
		}

		private void SetupDatabase()
		{
			if (!Directory.Exists(DataFolder))
			{
				Directory.CreateDirectory(DataFolder);
			}
			XkcdDatabase.DatabaseLocation = System.IO.Path.Combine(DataFolder, "xkcd.db");
		}

		private void DownloadComic(Comic comic, out string comicPath)
		{
			comicPath = System.IO.Path.Combine(DataFolder, System.IO.Path.GetFileName(comic.ImageUrl));

			if (!File.Exists(comicPath))
			{
				using WebClient client = new WebClient();
				client.DownloadFile(new Uri(comic.ImageUrl), comicPath);
			}
		}

		private void RefreshDatabase()
		{
			// todo: make this smarter so we aren't always refreshing the database
			Xkcd.RefreshComicDictionary();
		}

		public void LoadComic(Comic comic)
		{
			// Clear out displays
			ComicTitleView.Text = string.Empty;
			ComicImageView.SetImageDrawable(null);

			try
			{
				DownloadComic(comic, out string comicPath);

				ComicTitleView.Text = comic.Title;
				ComicImageView.SetImageURI(Android.Net.Uri.Parse(comicPath));
			}
			catch
			{
				ComicTitleView.Text = "Error!";
			}
		}

		public Comic FirstComic()
		{
			Comic first = Xkcd.GetFirstComic();

			LoadComic(first);

			return first;
		}

		public Comic PreviousComic(Comic current)
		{
			Comic previous = current.Previous();
			if (previous != null)
			{
				LoadComic(previous);
				return previous;
			}
			return current;
		}

		public Comic RandomComic()
		{
			Comic random = Xkcd.GetRandomComic();

			LoadComic(random);

			return random;
		}

		public Comic NextComic(Comic current)
		{
			Comic next = current.Next();
			if (next != null)
			{
				LoadComic(next);
				return next;
			}
			return current;
		}

		public Comic LatestComic()
		{
			RefreshDatabase();
			Comic latest = Xkcd.GetLatestComic();

			LoadComic(latest);

			return latest;
		}
	}
}