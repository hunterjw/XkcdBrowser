using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Webkit;
using Android.Widget;
using XkcdBrowser.Models;

namespace XkcdBrowser.AndroidApp
{
	public class ArticleFragment : Android.Support.V4.App.Fragment
	{
		public WhatIfArticle CurrentArticle { get; set; }
		public TextView ArticleTitleView { get; set; }
		public WebView ArticleWebView { get; set; }

		public override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);
		}

		public int ArticleId => Arguments.GetInt("current_article_id", -1);

		public static ArticleFragment NewInstance(int articleId = -1)
		{
			var bundle = new Bundle();
			bundle.PutInt("current_article_id", articleId);
			return new ArticleFragment { Arguments = bundle };
		}

		public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			View view = inflater.Inflate(Resource.Layout.article, container, false);

			ArticleTitleView = view.FindViewById<TextView>(Resource.Id.articleTextView);
			ArticleWebView = view.FindViewById<WebView>(Resource.Id.articleTextView);

			new WhatIfAndroidTask(this, WhatIfAndroidTaskType.Latest).Execute();

			//ComicTitleView = view.FindViewById<TextView>(Resource.Id.comicTextView);
			//ComicPhotoView = view.FindViewById<PhotoView>(Resource.Id.comicPhotoView);
			//Button firstButton = view.FindViewById<Button>(Resource.Id.firstButton);
			//Button previousButton = view.FindViewById<Button>(Resource.Id.previousButton);
			//Button randomButton = view.FindViewById<Button>(Resource.Id.randomButton);
			//Button nextButton = view.FindViewById<Button>(Resource.Id.nextButton);
			//Button latestButton = view.FindViewById<Button>(Resource.Id.latestButton);

			//firstButton.Click += FirstButton_Click;
			//previousButton.Click += PreviousButton_Click;
			//randomButton.Click += RandomButton_Click;
			//nextButton.Click += NextButton_Click;
			//latestButton.Click += LatestButton_Click;

			//new XkcdAndroidTask(this, XkcdAndroidTaskType.Init).Execute();

			return view;
		}

		public void FirstButton_Click(object sender, EventArgs args)
		{
			//new XkcdAndroidTask(this, XkcdAndroidTaskType.First).Execute();
		}

		public void PreviousButton_Click(object sender, EventArgs args)
		{
			//new XkcdAndroidTask(this, XkcdAndroidTaskType.Previous).Execute();
		}

		public void RandomButton_Click(object sender, EventArgs args)
		{
			//new XkcdAndroidTask(this, XkcdAndroidTaskType.RandomComic).Execute();
		}

		public void NextButton_Click(object sender, EventArgs args)
		{
			//new XkcdAndroidTask(this, XkcdAndroidTaskType.Next).Execute();
		}

		public void LatestButton_Click(object sender, EventArgs args)
		{
			//new XkcdAndroidTask(this, XkcdAndroidTaskType.Latest).Execute();
		}
	}
}