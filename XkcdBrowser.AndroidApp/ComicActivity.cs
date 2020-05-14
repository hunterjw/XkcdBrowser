using System;
using Android;
using Android.App;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V4.View;
using Android.Support.V4.Widget;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using ImageViews.Photo;
using XkcdBrowser.Models;

namespace XkcdBrowser.AndroidApp
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar", MainLauncher = true,
        ConfigurationChanges = Android.Content.PM.ConfigChanges.Orientation | Android.Content.PM.ConfigChanges.ScreenSize)]
    public class ComicActivity : AppCompatActivity, NavigationView.IOnNavigationItemSelectedListener
    {
        private XkcdAndroid XkcdAndroid { get; set; }
        private Comic CurrentComic { get; set; }
        private TextView ComicTitleView { get; set; }
        private PhotoView ComicPhotoView { get; set; }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.activity_comic);
            Android.Support.V7.Widget.Toolbar toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);

            DrawerLayout drawer = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
            ActionBarDrawerToggle toggle = new ActionBarDrawerToggle(this, drawer, toolbar, Resource.String.navigation_drawer_open, Resource.String.navigation_drawer_close);
            drawer.AddDrawerListener(toggle);
            toggle.SyncState();

            NavigationView navigationView = FindViewById<NavigationView>(Resource.Id.nav_view);
            navigationView.SetNavigationItemSelectedListener(this);

            ComicTitleView = FindViewById<TextView>(Resource.Id.comicTextView);
            ComicPhotoView = FindViewById<PhotoView>(Resource.Id.comicPhotoView);
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

            ComicPhotoView.LongClick += ComicPhotoView_LongClick;

            XkcdAndroid = new XkcdAndroid(this, ComicTitleView, ComicPhotoView);
            CurrentComic = XkcdAndroid.LatestComic();
        }

        public override void OnBackPressed()
        {
            DrawerLayout drawer = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
            if(drawer.IsDrawerOpen(GravityCompat.Start))
            {
                drawer.CloseDrawer(GravityCompat.Start);
            }
            else
            {
                base.OnBackPressed();
            }
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.menu_main, menu);
            return true;
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            int id = item.ItemId;
            if (id == Resource.Id.action_placeholder)
            {
                return true;
            }

            return base.OnOptionsItemSelected(item);
        }

        public bool OnNavigationItemSelected(IMenuItem item)
        {
            int id = item.ItemId;

            if (id == Resource.Id.nav_comic_main)
            {
                // Handle the camera action
            }
            else if (id == Resource.Id.nav_comic_list)
            {

            }
            else if (id == Resource.Id.nav_comic_favs)
            {

            }
            else if (id == Resource.Id.nav_whatif_main)
            {

            }
            else if (id == Resource.Id.nav_whatif_list)
            {

            }
            else if (id == Resource.Id.nav_whatif_favs)
            {

            }
            else if (id == Resource.Id.nav_settings)
            {

            }
            else if (id == Resource.Id.nav_about)
            {

            }

            DrawerLayout drawer = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
            drawer.CloseDrawer(GravityCompat.Start);
            return true;
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        public void FirstButton_Click(object sender, EventArgs args)
        {
            CurrentComic = XkcdAndroid.FirstComic();
        }

        public void PreviousButton_Click(object sender, EventArgs args)
        {
            CurrentComic = XkcdAndroid.PreviousComic(CurrentComic);
        }

        public void RandomButton_Click(object sender, EventArgs args)
        {
            CurrentComic = XkcdAndroid.RandomComic();
        }

        public void NextButton_Click(object sender, EventArgs args)
        {
            CurrentComic = XkcdAndroid.NextComic(CurrentComic);
        }

        public void LatestButton_Click(object sender, EventArgs args)
        {
            CurrentComic = XkcdAndroid.LatestComic();
        }

        public void ComicPhotoView_LongClick(object sender, EventArgs args)
        {
            var popupDialog = new Dialog(this);
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

