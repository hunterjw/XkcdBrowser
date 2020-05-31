using Android.OS;
using Android.Views;
using Android.Widget;
using Android.Support.V4.App;

namespace XkcdBrowser.AndroidApp
{
	public class PlaceholderFragment : Fragment
	{
		public override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);
		}

		public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
			View view = inflater.Inflate(Resource.Layout.placeholder_layout, container, false);

			TextView textView = view.FindViewById<TextView>(Resource.Id.placeholderTextView);
			textView.Text = PlaceholderString;

			return view;
		}

		public string PlaceholderString => Arguments.GetString("placeholder_string", "Placeholder");

		public static PlaceholderFragment NewInstance(string placeholderString = "")
		{
			var bundle = new Bundle();
			if (!string.IsNullOrEmpty(placeholderString))
			{
				bundle.PutString("placeholder_string", placeholderString);
			}
			return new PlaceholderFragment { Arguments = bundle };
		}
	}
}