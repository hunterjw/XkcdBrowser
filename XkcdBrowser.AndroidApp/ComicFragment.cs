using System;
using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;
using ImageViews.Photo;
using XkcdBrowser.Models;

namespace XkcdBrowser.AndroidApp
{
	public class ComicFragment : Android.Support.V4.App.Fragment
	{
		public Comic CurrentComic { get; set; }
		public TextView ComicTitleView { get; set; }
		public PhotoView ComicPhotoView { get; set; }

		public override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);
		}

		public int ComicId => Arguments.GetInt("current_comic_id", -1);

		public static ComicFragment NewInstance(int comicId = -1)
		{
			var bundle = new Bundle();
			bundle.PutInt("current_comic_id", comicId);
			return new ComicFragment { Arguments = bundle };
		}

		public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			View view = inflater.Inflate(Resource.Layout.comic, container, false);

			ComicTitleView = view.FindViewById<TextView>(Resource.Id.comicTextView);
			ComicPhotoView = view.FindViewById<PhotoView>(Resource.Id.comicPhotoView);
			Button firstButton = view.FindViewById<Button>(Resource.Id.firstButton);
			Button previousButton = view.FindViewById<Button>(Resource.Id.previousButton);
			Button randomButton = view.FindViewById<Button>(Resource.Id.randomButton);
			Button nextButton = view.FindViewById<Button>(Resource.Id.nextButton);
			Button latestButton = view.FindViewById<Button>(Resource.Id.latestButton);

			firstButton.Click += FirstButton_Click;
			previousButton.Click += PreviousButton_Click;
			randomButton.Click += RandomButton_Click;
			nextButton.Click += NextButton_Click;
			latestButton.Click += LatestButton_Click;

			ComicPhotoView.LongClick += ComicPhotoView_LongClick;

			new XkcdAndroidTask(this, XkcdAndroidTaskType.Init).Execute();

			return view;
		}

		public void FirstButton_Click(object sender, EventArgs args)
		{
			new XkcdAndroidTask(this, XkcdAndroidTaskType.First).Execute();
		}

		public void PreviousButton_Click(object sender, EventArgs args)
		{
			new XkcdAndroidTask(this, XkcdAndroidTaskType.Previous).Execute();
		}

		public void RandomButton_Click(object sender, EventArgs args)
		{
			new XkcdAndroidTask(this, XkcdAndroidTaskType.RandomComic).Execute();
		}

		public void NextButton_Click(object sender, EventArgs args)
		{
			new XkcdAndroidTask(this, XkcdAndroidTaskType.Next).Execute();
		}

		public void LatestButton_Click(object sender, EventArgs args)
		{
			new XkcdAndroidTask(this, XkcdAndroidTaskType.Latest).Execute();
		}

		public void ComicPhotoView_LongClick(object sender, EventArgs args)
		{
			var popupDialog = new Dialog(Context);
			popupDialog.SetContentView(Resource.Layout.comic_detail);
			popupDialog.Window.SetSoftInputMode(SoftInput.AdjustResize);
			popupDialog.Show();

			TextView comicNumber = popupDialog.FindViewById<TextView>(Resource.Id.comicNumberTextView);
			TextView comicDate = popupDialog.FindViewById<TextView>(Resource.Id.comicDateTextView);
			TextView comicAltText = popupDialog.FindViewById<TextView>(Resource.Id.altTextView);
			Button explainButton = popupDialog.FindViewById<Button>(Resource.Id.explainButton);

			comicNumber.Text = $"#{CurrentComic.Id}";
			comicDate.Text = CurrentComic.Date;
			comicAltText.Text = CurrentComic.AltText;

			explainButton.Click += (s, a) =>
			{
				Xamarin.Essentials.Browser.OpenAsync(CurrentComic.ExplainLink);
			};
		}
	}
}