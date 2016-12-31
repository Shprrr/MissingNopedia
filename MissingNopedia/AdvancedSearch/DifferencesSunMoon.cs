using SQLite;

namespace MissingNopedia.AdvancedSearch
{
	class DifferencesSunMoon
	{
		internal static void ExecuteDifferences()
		{
			using (var sql = new SQLiteConnection("pokedex.sqlite"))
			{
				sql.Execute("INSERT OR REPLACE INTO regions VALUES(?, ?)", 7, "alola");
			}
		}
	}
}
