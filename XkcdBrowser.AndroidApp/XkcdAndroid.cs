using System.IO;
using Android.Content;

namespace XkcdBrowser.AndroidApp
{
	public class XkcdAndroid
	{
		public string DataFolder { get; set; }

		public XkcdAndroid(Context context)
		{
			DataFolder = Path.Combine(context.GetExternalFilesDir("").ToString(), "XkcdData");
		}

		public void SetupDatabase()
		{
			if (!Directory.Exists(DataFolder))
			{
				Directory.CreateDirectory(DataFolder);
			}
			XkcdDatabase.DatabaseLocation = Path.Combine(DataFolder, "xkcd.db");
		}

		public void RefreshDatabase()
		{
			// todo: make this smarter so we aren't always refreshing the database
			Xkcd.RefreshComicDictionary();
		}
	}
}