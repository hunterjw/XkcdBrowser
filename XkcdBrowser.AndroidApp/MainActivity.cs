﻿using Android.App;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V4.View;
using Android.Support.V4.Widget;
using Android.Support.V7.App;
using Android.Views;

namespace XkcdBrowser.AndroidApp
{
	[Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar", MainLauncher = true,
		ConfigurationChanges = Android.Content.PM.ConfigChanges.Orientation | Android.Content.PM.ConfigChanges.ScreenSize)]
	public class MainActivity : AppCompatActivity, NavigationView.IOnNavigationItemSelectedListener
	{
		public int CurrentContentId { get; set; }

		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);
			Xamarin.Essentials.Platform.Init(this, savedInstanceState);
			SetContentView(Resource.Layout.activity_main);
			Android.Support.V7.Widget.Toolbar toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
			SetSupportActionBar(toolbar);

			DrawerLayout drawer = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
			ActionBarDrawerToggle toggle = new ActionBarDrawerToggle(this, drawer, toolbar, Resource.String.navigation_drawer_open, Resource.String.navigation_drawer_close);
			drawer.AddDrawerListener(toggle);
			toggle.SyncState();

			NavigationView navigationView = FindViewById<NavigationView>(Resource.Id.nav_view);
			navigationView.SetNavigationItemSelectedListener(this);
			
			navigationView.SetCheckedItem(Resource.Id.nav_comic_main);
			MainContentSwitcher.MainActivity = this;
			MainContentSwitcher.SwitchContentFromNavBar(Resource.Id.nav_comic_main);
		}

		public override void OnBackPressed()
		{
			DrawerLayout drawer = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
			if (drawer.IsDrawerOpen(GravityCompat.Start))
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

			if (id != CurrentContentId)
			{
				MainContentSwitcher.SwitchContentFromNavBar(id);
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
	}
}

