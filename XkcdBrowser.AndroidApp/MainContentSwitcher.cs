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
				var placeholderFrag = PlaceholderFragment.NewInstance("Comic List");

				FragmentTransaction tx = mainActivity.SupportFragmentManager.BeginTransaction();
				tx.Replace(Resource.Id.main_activity_container, placeholderFrag);
				tx.Commit();

				mainActivity.CurrentContentId = Resource.Id.nav_comic_list;
			}
			else if (navBarMenuId == Resource.Id.nav_comic_favs)
			{
				var placeholderFrag = PlaceholderFragment.NewInstance("Comic Favorite List");

				FragmentTransaction tx = mainActivity.SupportFragmentManager.BeginTransaction();
				tx.Replace(Resource.Id.main_activity_container, placeholderFrag);
				tx.Commit();

				mainActivity.CurrentContentId = Resource.Id.nav_comic_favs;
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
			else if (navBarMenuId == Resource.Id.nav_whatif_favs)
			{
				var placeholderFrag = PlaceholderFragment.NewInstance("What If Favorite List");

				FragmentTransaction tx = mainActivity.SupportFragmentManager.BeginTransaction();
				tx.Replace(Resource.Id.main_activity_container, placeholderFrag);
				tx.Commit();

				mainActivity.CurrentContentId = Resource.Id.nav_whatif_favs;
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
	}
}