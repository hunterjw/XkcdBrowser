using Android.Support.Design.Widget;
using Android.Support.V4.App;

namespace XkcdBrowser.AndroidApp
{
	public static class MainContentSwitcher
	{
		public static void SwitchContent(MainActivity mainActivity, int navBarMenuId)
		{
			if (navBarMenuId == Resource.Id.nav_comic_main)
			{
				var comicFrag = ComicFragment.NewInstance();

				FragmentTransaction tx = mainActivity.SupportFragmentManager.BeginTransaction();
				tx.Replace(Resource.Id.main_activity_container, comicFrag);
				tx.Commit();

				mainActivity.CurrentContentId = Resource.Id.nav_comic_main;
			}
			else if (navBarMenuId == Resource.Id.nav_comic_list)
			{
				var comicListFrag = ComicListFragment.NewInstance(mainActivity);

				FragmentTransaction tx = mainActivity.SupportFragmentManager.BeginTransaction();
				tx.Replace(Resource.Id.main_activity_container, comicListFrag);
				tx.Commit();

				mainActivity.CurrentContentId = Resource.Id.nav_comic_list;
			}
			else if (navBarMenuId == Resource.Id.nav_whatif_main)
			{
				var placeholderFrag = PlaceholderFragment.NewInstance("What If");

				FragmentTransaction tx = mainActivity.SupportFragmentManager.BeginTransaction();
				tx.Replace(Resource.Id.main_activity_container, placeholderFrag);
				tx.Commit();

				mainActivity.CurrentContentId = Resource.Id.nav_whatif_main;
			}
			else if (navBarMenuId == Resource.Id.nav_whatif_list)
			{
				var placeholderFrag = PlaceholderFragment.NewInstance("What If List");

				FragmentTransaction tx = mainActivity.SupportFragmentManager.BeginTransaction();
				tx.Replace(Resource.Id.main_activity_container, placeholderFrag);
				tx.Commit();

				mainActivity.CurrentContentId = Resource.Id.nav_whatif_list;
			}
			else if (navBarMenuId == Resource.Id.nav_settings)
			{
				var placeholderFrag = PlaceholderFragment.NewInstance("Settings");

				FragmentTransaction tx = mainActivity.SupportFragmentManager.BeginTransaction();
				tx.Replace(Resource.Id.main_activity_container, placeholderFrag);
				tx.Commit();

				mainActivity.CurrentContentId = Resource.Id.nav_settings;
			}
			else if (navBarMenuId == Resource.Id.nav_about)
			{
				var placeholderFrag = PlaceholderFragment.NewInstance("About");

				FragmentTransaction tx = mainActivity.SupportFragmentManager.BeginTransaction();
				tx.Replace(Resource.Id.main_activity_container, placeholderFrag);
				tx.Commit();

				mainActivity.CurrentContentId = Resource.Id.nav_about;
			}
		}

		public static void SwitchToComic(MainActivity mainActivity, int comicId)
		{
			var comicFrag = ComicFragment.NewInstance(comicId);

			FragmentTransaction tx = mainActivity.SupportFragmentManager.BeginTransaction();
			tx.Replace(Resource.Id.main_activity_container, comicFrag);
			tx.Commit();

			mainActivity.CurrentContentId = Resource.Id.nav_comic_main;
			NavigationView navigationView = mainActivity.FindViewById<NavigationView>(Resource.Id.nav_view);
			navigationView.SetCheckedItem(Resource.Id.nav_comic_main);
		}
	}
}