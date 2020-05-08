using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using ImageViews.Photo;
using Android.Content;
using System;
using XkcdBrowser.Models;

namespace XkcdBrowser.AndroidApp
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        private Comic CurrentComic { get; set; }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);

            TextView comicTitleView = FindViewById<TextView>(Resource.Id.comicTextView);
            PhotoView comicPhotoView = FindViewById<PhotoView>(Resource.Id.comicPhotoView);
            Button firstButton = FindViewById<Button>(Resource.Id.firstButton);
            Button previousButton = FindViewById<Button>(Resource.Id.previousButton);
            Button randomButton = FindViewById<Button>(Resource.Id.randomButton);
            Button nextButton = FindViewById<Button>(Resource.Id.nextButton);
            Button latestButton = FindViewById<Button>(Resource.Id.latestButton);

            firstButton.Click += FirstButton_Click;
            previousButton.Click += PreviousButton_Click;
            randomButton.Click += RandomButton_Click;
            nextButton.Click += NextButton_Click;
            latestButton.Click += LatestButton_Click;

            CurrentComic = XkcdAndroid.LatestComic(ApplicationContext, comicTitleView, comicPhotoView);
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        public void FirstButton_Click(object sender, EventArgs args)
        {
            TextView comicTitleView = FindViewById<TextView>(Resource.Id.comicTextView);
            PhotoView comicPhotoView = FindViewById<PhotoView>(Resource.Id.comicPhotoView);

            CurrentComic = XkcdAndroid.FirstComic(ApplicationContext, comicTitleView, comicPhotoView);
        }

        public void PreviousButton_Click(object sender, EventArgs args)
        {
            TextView comicTitleView = FindViewById<TextView>(Resource.Id.comicTextView);
            PhotoView comicPhotoView = FindViewById<PhotoView>(Resource.Id.comicPhotoView);

            CurrentComic = XkcdAndroid.PreviousComic(ApplicationContext, comicTitleView, comicPhotoView, CurrentComic);
        }

        public void RandomButton_Click(object sender, EventArgs args)
        {
            TextView comicTitleView = FindViewById<TextView>(Resource.Id.comicTextView);
            PhotoView comicPhotoView = FindViewById<PhotoView>(Resource.Id.comicPhotoView);

            CurrentComic = XkcdAndroid.RandomComic(ApplicationContext, comicTitleView, comicPhotoView);
        }

        public void NextButton_Click(object sender, EventArgs args)
        {
            TextView comicTitleView = FindViewById<TextView>(Resource.Id.comicTextView);
            PhotoView comicPhotoView = FindViewById<PhotoView>(Resource.Id.comicPhotoView);

            CurrentComic = XkcdAndroid.NextComic(ApplicationContext, comicTitleView, comicPhotoView, CurrentComic);
        }

        public void LatestButton_Click(object sender, EventArgs args)
        {
            TextView comicTitleView = FindViewById<TextView>(Resource.Id.comicTextView);
            PhotoView comicPhotoView = FindViewById<PhotoView>(Resource.Id.comicPhotoView);

            CurrentComic = XkcdAndroid.LatestComic(ApplicationContext, comicTitleView, comicPhotoView);
        }
    }
}