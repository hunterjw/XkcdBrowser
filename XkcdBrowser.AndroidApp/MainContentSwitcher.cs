using Android.Support.Design.Widget;
using Android.Support.V4.App;

namespace XkcdBrowser.AndroidApp
{
	public static class MainContentSwitcher
	{
		public static MainActivity MainActivity { get; set; }

		public static void SwitchContentFromNavBar(int navBarMenuId)
		{
			if (navBarMenuId == Resource.Id.nav_comic_main)
			{
				var comicFrag = ComicFragment.NewInstance();

				FragmentTransaction tx = MainActivity.SupportFragmentManager.BeginTransaction();
				tx.Replace(Resource.Id.main_activity_container, comicFrag);
				tx.Commit();

				MainActivity.CurrentContentId = Resource.Id.nav_comic_main;
			}
			else if (navBarMenuId == Resource.Id.nav_comic_list)
			{
				var comicListFrag = ComicListFragment.NewInstance();

				FragmentTransaction tx = MainActivity.SupportFragmentManager.BeginTransaction();
				tx.Replace(Resource.Id.main_activity_container, comicListFrag);
				tx.Commit();

				MainActivity.CurrentContentId = Resource.Id.nav_comic_list;
			}
			else if (navBarMenuId == Resource.Id.nav_whatif_main)
			{
				var placeholderFrag = PlaceholderFragment.NewInstance("What If");

				FragmentTransaction tx = MainActivity.SupportFragmentManager.BeginTransaction();
				tx.Replace(Resource.Id.main_activity_container, placeholderFrag);
				tx.Commit();

				MainActivity.CurrentContentId = Resource.Id.nav_whatif_main;
			}
			else if (navBarMenuId == Resource.Id.nav_whatif_list)
			{
				var placeholderFrag = PlaceholderFragment.NewInstance("What If List");

				FragmentTransaction tx = MainActivity.SupportFragmentManager.BeginTransaction();
				tx.Replace(Resource.Id.main_activity_container, placeholderFrag);
				tx.Commit();

				MainActivity.CurrentContentId = Resource.Id.nav_whatif_list;
			}
			else if (navBarMenuId == Resource.Id.nav_settings)
			{
				var placeholderFrag = PlaceholderFragment.NewInstance("Settings");

				FragmentTransaction tx = MainActivity.SupportFragmentManager.BeginTransaction();
				tx.Replace(Resource.Id.main_activity_container, placeholderFrag);
				tx.Commit();

				MainActivity.CurrentContentId = Resource.Id.nav_settings;
			}
			else if (navBarMenuId == Resource.Id.nav_about)
			{
				var placeholderFrag = PlaceholderFragment.NewInstance("About");

				FragmentTransaction tx = MainActivity.SupportFragmentManager.BeginTransaction();
				tx.Replace(Resource.Id.main_activity_container, placeholderFrag);
				tx.Commit();

				MainActivity.CurrentContentId = Resource.Id.nav_about;
			}
		}

		public static void SwitchToComic(int comicId)
		{
			var comicFrag = ComicFragment.NewInstance(comicId);

			FragmentTransaction tx = MainActivity.SupportFragmentManager.BeginTransaction();
			tx.Replace(Resource.Id.main_activity_container, comicFrag);
			tx.Commit();

			MainActivity.CurrentContentId = Resource.Id.nav_comic_main;
			NavigationView navigationView = MainActivity.FindViewById<NavigationView>(Resource.Id.nav_view);
			navigationView.SetCheckedItem(Resource.Id.nav_comic_main);
		}
	}
}