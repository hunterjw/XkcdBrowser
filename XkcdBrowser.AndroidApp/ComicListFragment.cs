using System;
using System.Collections.Generic;
using System.Linq;
using Android.OS;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using XkcdBrowser.Models;

namespace XkcdBrowser.AndroidApp
{
	public class ComicListFragment : Android.Support.V4.App.Fragment
	{
		private RecyclerView RecyclerView { get; set; }
		private RecyclerView.Adapter Adapter { get; set; }
		private RecyclerView.LayoutManager LayoutManager { get; set; }
		private List<ComicArchiveEntry> DataSet { get; set; }

		public override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			if (Xkcd.ComicDictionary.Count < 1)
			{
				XkcdAndroid.RefreshDatabase();
			}
			DataSet = Xkcd.ComicDictionary.Values.OrderByDescending(_ => _.Id).ToList();
		}

		public static ComicListFragment NewInstance()
		{
			var bundle = new Bundle();
			return new ComicListFragment
			{
				Arguments = bundle
			};
		}

		public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
			base.OnCreateView(inflater, container, savedInstanceState);

			View view = inflater.Inflate(Resource.Layout.comic_list, container, false);
			RecyclerView = view.FindViewById<RecyclerView>(Resource.Id.comicListRecyclerView);
			LayoutManager = new LinearLayoutManager(Activity);
			RecyclerView.SetLayoutManager(LayoutManager);
			Adapter = new ComicListAdapter(DataSet);
			RecyclerView.SetAdapter(Adapter);

			return view;
		}
	}

	public class ComicListAdapter : RecyclerView.Adapter
	{
		private readonly List<ComicArchiveEntry> DataSet;

		public class ComicListViewHolder : RecyclerView.ViewHolder
		{
			private LinearLayout ComicListRowLayout { get; set; }

			public TextView TitleTextView { get; set; }
			public TextView IdTextView { get; set; }
			public TextView DateTextView { get; set; }
			public ComicArchiveEntry CurrentComic { get; set; }

			public ComicListViewHolder(View view) : base(view)
			{
				TitleTextView = view.FindViewById<TextView>(Resource.Id.comicTitleTextView);
				IdTextView = view.FindViewById<TextView>(Resource.Id.comicNumberTextView);
				DateTextView = view.FindViewById<TextView>(Resource.Id.comicDateTextView);
				ComicListRowLayout = view.FindViewById<LinearLayout>(Resource.Id.comicListRowLayout);

				ComicListRowLayout.Click += ComicListViewHolder_Click;
			}

			public void ComicListViewHolder_Click(object sender, EventArgs args)
			{
				MainContentSwitcher.SwitchToComic(CurrentComic.Id);
			}
		}

		public ComicListAdapter(List<ComicArchiveEntry> dataSet)
		{
			DataSet = dataSet;
		}

		public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup viewGroup, int position)
		{
			View view = LayoutInflater.From(viewGroup.Context).Inflate(Resource.Layout.comic_list_row, viewGroup, false);
			ComicListViewHolder viewHolder = new ComicListViewHolder(view);
			return viewHolder;
		}

		public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
		{
			var currentComic = DataSet[position];

			var comicHolder = holder as ComicListViewHolder;
			comicHolder.CurrentComic = currentComic;
			comicHolder.TitleTextView.SetText(currentComic.Title, TextView.BufferType.Normal);
			comicHolder.IdTextView.SetText($"#{currentComic.Id}", TextView.BufferType.Normal);
			comicHolder.DateTextView.SetText(currentComic.Date, TextView.BufferType.Normal);
		}

		public override int ItemCount
		{
			get
			{
				return DataSet.Count;
			}
		}
	}
}