using System.IO;
using Android.App;

namespace XkcdBrowser.AndroidApp
{
	public static class XkcdAndroid
	{
		private static string _dataFolder;

		public static string DataFolder
		{
			get
			{
				if (string.IsNullOrWhiteSpace(_dataFolder))
				{
					_dataFolder = Path.Combine(Application.Context.GetExternalFilesDir("").ToString(), "XkcdData");
				}
				return _dataFolder;
			}
			set
			{
				_dataFolder = value;
			}
		}

		public static void SetupDatabase()
		{
			if (!Directory.Exists(DataFolder))
			{
				Directory.CreateDirectory(DataFolder);
			}
			XkcdDatabase.DatabaseLocation = Path.Combine(DataFolder, "xkcd.db");
		}

		public static void RefreshComicDatabase()
		{
			// todo: make this smarter so we aren't always refreshing the database
			Xkcd.RefreshComicDictionary();
		}

		public static void RefreshArticleDatabase()
		{
			// todo: make this smarter
			WhatIf.RefreshWhatIfDictionary();
		}
	}
}