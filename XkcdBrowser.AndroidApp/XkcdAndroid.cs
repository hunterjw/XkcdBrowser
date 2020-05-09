using System;
using System.IO;
using System.Net;
using Android.Content;
using Android.Graphics;
using Android.Widget;
using XkcdBrowser.Models;

namespace XkcdBrowser.AndroidApp
{
	public static class XkcdAndroid
	{
		private static void SetupDatabase(Context context, out string dataFolder)
		{
			dataFolder = System.IO.Path.Combine(context.GetExternalFilesDir("").ToString(), "XkcdData");
			if (!Directory.Exists(dataFolder))
			{
				Directory.CreateDirectory(dataFolder);
			}

			XkcdDatabase.DatabaseLocation = System.IO.Path.Combine(dataFolder, "xkcd.db");
		}

		private static void DownloadComic(Comic comic, string dataFolder, out string comicPath)
		{
			comicPath = System.IO.Path.Combine(dataFolder, System.IO.Path.GetFileName(comic.ImageUrl));

			if (!File.Exists(comicPath))
			{
				using WebClient client = new WebClient();
				client.DownloadFile(new Uri(comic.ImageUrl), comicPath);
			}
		}

		private static void RefreshDatabase(Context context)
		{
			// todo: make this smarter so we aren't always refreshing the database
			SetupDatabase(context, out _);
			Xkcd.RefreshComicDictionary();
		}

		public static void LoadComic(Context context, TextView comicTitleView, ImageView comicImageView, Comic comic)
		{
			// Clear out displays
			comicTitleView.Text = string.Empty;
			comicImageView.SetImageDrawable(null);

			try
			{
				SetupDatabase(context, out string dataFolder);

				DownloadComic(comic, dataFolder, out string comicPath);

				comicTitleView.Text = comic.Title;
				Bitmap bitmap = BitmapFactory.DecodeFile(comicPath);
				comicImageView.SetImageBitmap(bitmap);
			}
			catch
			{
				comicTitleView.Text = "Error!";
			}
		}

		public static Comic FirstComic(Context context, TextView comicTitleView, ImageView comicImageView)
		{
			SetupDatabase(context, out _);
			Comic first = Xkcd.GetFirstComic();

			LoadComic(context, comicTitleView, comicImageView, first);

			return first;
		}

		public static Comic PreviousComic(Context context, TextView comicTitleView, ImageView comicImageView, Comic current)
		{
			SetupDatabase(context, out _);
			Comic previous = current.Previous();
			if (previous != null)
			{
				LoadComic(context, comicTitleView, comicImageView, previous);
				return previous;
			}
			return current;
		}

		public static Comic RandomComic(Context context, TextView comicTitleView, ImageView comicImageView)
		{
			SetupDatabase(context, out _);
			Comic random = Xkcd.GetRandomComic();

			LoadComic(context, comicTitleView, comicImageView, random);

			return random;
		}

		public static Comic NextComic(Context context, TextView comicTitleView, ImageView comicImageView, Comic current)
		{
			SetupDatabase(context, out _);
			Comic next = current.Next();
			if (next != null)
			{
				LoadComic(context, comicTitleView, comicImageView, next);
				return next;
			}
			return current;
		}

		public static Comic LatestComic(Context context, TextView comicTitleView, ImageView comicImageView)
		{
			SetupDatabase(context, out _);
			RefreshDatabase(context);
			Comic latest = Xkcd.GetLatestComic();

			LoadComic(context, comicTitleView, comicImageView, latest);

			return latest;
		}
	}
}