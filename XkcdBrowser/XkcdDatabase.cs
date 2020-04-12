using LiteDB;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using XkcdBrowser.Models;

namespace XkcdBrowser
{
	/// <summary>
	/// Database wrapper around a LiteDatabase for managing xkcd comics and archive entires
	/// </summary>
	public static class XkcdDatabase
	{
		private const string ComicArchiveCollectionName = "ComicArchive";
		private const string ComicCollectionName = "Comic";

		private static string PrivateDatabaseLocation { get; set; }

		/// <summary>
		/// Location of the database file
		/// </summary>
		public static string DatabaseLocation
		{
			get
			{
				if (string.IsNullOrEmpty(PrivateDatabaseLocation))
				{
					PrivateDatabaseLocation = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "xkcd.db");
				}
				return PrivateDatabaseLocation;
			}
			set
			{
				PrivateDatabaseLocation = value;
			}
		}

		/// <summary>
		/// List of comic archive entries
		/// </summary>
		internal static List<ComicArchiveEntry> ComicArchiveEntries
		{
			get
			{
				using (var db = new LiteDatabase(DatabaseLocation))
				{
					ILiteCollection<ComicArchiveEntry> collection = db.GetCollection<ComicArchiveEntry>(ComicArchiveCollectionName);
					return collection.Query().ToList();
				}
			}
			set
			{
				using (var db = new LiteDatabase(DatabaseLocation))
				{
					ILiteCollection<ComicArchiveEntry> collection = db.GetCollection<ComicArchiveEntry>(ComicArchiveCollectionName);
					foreach (ComicArchiveEntry entry in value)
					{
						if (collection.FindById(entry.Id) != null)
						{
							collection.Update(entry);
						}
						else
						{
							collection.Insert(entry);
						}
					}
					collection.EnsureIndex(x => x.Id);
				}
			}
		}

		/// <summary>
		/// Inserts or updates a comic in the database
		/// </summary>
		/// <param name="comic">Comic to insert or update</param>
		internal static void InsertOrUpdateComic(Comic comic)
		{
			using (var db = new LiteDatabase(DatabaseLocation))
			{
				ILiteCollection<Comic> collection = db.GetCollection<Comic>(ComicCollectionName);
				if (collection.FindById(comic.Id) != null)
				{
					collection.Update(comic);
				}
				else
				{
					collection.Insert(comic);
				}
				collection.EnsureIndex(x => x.Id);
			}
		}

		/// <summary>
		/// Gets a comic from the database
		/// </summary>
		/// <param name="comicId">Comic ID</param>
		/// <returns>Comic if found, null otherwise</returns>
		internal static Comic GetComic(int comicId)
		{
			using (var db = new LiteDatabase(DatabaseLocation))
			{
				ILiteCollection<Comic> collection = db.GetCollection<Comic>(ComicCollectionName);
				return collection.FindById(comicId);
			}
		}
	}
}
