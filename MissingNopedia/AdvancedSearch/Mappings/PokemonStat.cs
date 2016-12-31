using System.Collections.Generic;
using SQLite;

namespace MissingNopedia.AdvancedSearch.Mappings
{
	[Table("pokemon_stats")]
	public class PokemonStat
	{
		/*
		CREATE TABLE pokemon_stats (
				pokemon_id INTEGER NOT NULL,
				stat_id INTEGER NOT NULL,
				base_stat INTEGER NOT NULL,
				effort INTEGER NOT NULL,
				PRIMARY KEY (pokemon_id, stat_id),
				FOREIGN KEY(pokemon_id) REFERENCES pokemon (id),
				FOREIGN KEY(stat_id) REFERENCES stats (id)
		);
		*/
		[/*PrimaryKey, */Column("pokemon_id")]
		public int PokemonId { get; set; }

		[/*PrimaryKey, */Column("stat_id")]
		public int StatId { get; set; }

		[Column("base_stat")]
		public int BaseStat { get; set; }

		public int Effort { get; set; }

		public static List<PokemonStat> GetAllPokemonStats(SQLiteConnection sql)
		{
			return sql.Query<PokemonStat>("SELECT * FROM pokemon_stats");
		}
	}
}
