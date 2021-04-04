using System;
using System.IO;
using System.Net;
using Android.App;
using Android.OS;
using XkcdBrowser.Models;

namespace XkcdBrowser.AndroidApp
{
	public class XkcdAndroidTask : AsyncTask
	{
		private AlertDialog Dialog { get; set; }
		private ComicFragment Parent { get; set; }
		private XkcdAndroidTaskType TaskType { get; set; }

		public XkcdAndroidTask(ComicFragment parent, XkcdAndroidTaskType taskType)
		{
			Parent = parent;
			TaskType = taskType;
			XkcdAndroid.SetupDatabase();
		}

		protected override Java.Lang.Object DoInBackground(params Java.Lang.Object[] @params)
		{
			switch (TaskType)
			{
				case XkcdAndroidTaskType.Init:
					if (Parent.ComicId != -1)
					{
						Parent.CurrentComic = Xkcd.GetComic(Parent.ComicId);
					}
					else
					{
						XkcdAndroid.RefreshComicDatabase();
						Parent.CurrentComic = Xkcd.GetLatestComic();
					}
					break;
				case XkcdAndroidTaskType.First:
					Parent.CurrentComic = Xkcd.GetFirstComic();
					break;
				case XkcdAndroidTaskType.Previous:
					var previous = Parent.CurrentComic.Previous();
					if (previous != null)
					{
						Parent.CurrentComic = previous;
					}
					break;
				case XkcdAndroidTaskType.RandomComic:
					Parent.CurrentComic = Xkcd.GetRandomComic();
					break;
				case XkcdAndroidTaskType.Next:
					var next = Parent.CurrentComic.Next();
					if (next != null)
					{
						Parent.CurrentComic = next;
					}
					break;
				case XkcdAndroidTaskType.Latest:
					XkcdAndroid.RefreshComicDatabase();
					Parent.CurrentComic = Xkcd.GetLatestComic();
					break;
			}
			return null;
		}

		protected override void OnPreExecute()
		{
			var builder = new AlertDialog.Builder(Parent.Context);
			builder.SetCancelable(false);
			builder.SetMessage("Loading...");
			Dialog = builder.Create();
			Dialog.Show();
			base.OnPreExecute();
		}

		protected override void OnPostExecute(Java.Lang.Object result)
		{
			LoadComic(Parent.CurrentComic);
			Dialog.Dismiss();
			base.OnPostExecute(result);
		}

		private void LoadComic(Comic comic)
		{
			// Clear out displays
			Parent.ComicTitleView.Text = string.Empty;
			Parent.ComicPhotoView.SetImageDrawable(null);

			try
			{
				DownloadComic(comic, out string comicPath);

				Parent.ComicTitleView.Text = comic.Title;
				Parent.ComicPhotoView.SetImageURI(Android.Net.Uri.Parse(comicPath));
			}
			catch
			{
				Parent.ComicTitleView.Text = "Error!";
			}
		}

		private void DownloadComic(Comic comic, out string comicPath)
		{
			comicPath = Path.Combine(XkcdAndroid.DataFolder, Path.GetFileName(comic.ImageUrl));

			if (!File.Exists(comicPath))
			{
				using (WebClient client = new WebClient())
				{
					client.DownloadFile(new Uri(comic.ImageUrl), comicPath);
				}
			}
		}
	}

	public enum XkcdAndroidTaskType
	{
		Init,
		First,
		Previous,
		RandomComic,
		Next,
		Latest
	}
}