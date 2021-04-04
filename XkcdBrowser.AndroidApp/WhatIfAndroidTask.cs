using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using XkcdBrowser.Models;

namespace XkcdBrowser.AndroidApp
{
	public class WhatIfAndroidTask : AsyncTask
	{
		private AlertDialog Dialog { get; set; }
		private ArticleFragment Parent { get; set; }
		private WhatIfAndroidTaskType TaskType { get; set; }

		public WhatIfAndroidTask(ArticleFragment parent, WhatIfAndroidTaskType taskType)
		{
			Parent = parent;
			TaskType = taskType;
			XkcdAndroid.SetupDatabase();
		}

		protected override Java.Lang.Object DoInBackground(params Java.Lang.Object[] @params)
		{
			switch (TaskType)
			{
				case WhatIfAndroidTaskType.Latest:
					XkcdAndroid.RefreshComicDatabase();
					Parent.CurrentArticle = WhatIf.GetLatestArticle();
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
			LoadArticle(Parent.CurrentArticle);
			Dialog.Dismiss();
			base.OnPostExecute(result);
		}

		private void LoadArticle(WhatIfArticle article)
		{
			Parent.ArticleTitleView.Text = string.Empty;

			try
			{
				Parent.ArticleTitleView.Text = article.Title;


			}
			catch
			{
				Parent.ArticleTitleView.Text = "Error!";
			}
		}
	}

	public enum WhatIfAndroidTaskType
	{
		First,
		Previous,
		RandomComic,
		Next,
		Latest
	}
}