using System.Collections.Generic;
using SQLite;

namespace MissingNopedia.AdvancedSearch.Mappings
{
	[Table("pokemon_types")]
	public class PokemonType
	{
		/*
		CREATE TABLE pokemon_types (
				pokemon_id INTEGER NOT NULL,
				type_id INTEGER NOT NULL,
				slot INTEGER NOT NULL,
				PRIMARY KEY (pokemon_id, slot),
				FOREIGN KEY(pokemon_id) REFERENCES pokemon (id),
				FOREIGN KEY(type_id) REFERENCES types (id)
		);
		*/
		[/*PrimaryKey, */Column("pokemon_id")]
		public int PokemonId { get; set; }

		[Column("type_id")]
		public int TypeId { get; set; }

		//[PrimaryKey]
		public int Slot { get; set; }

		public static List<PokemonType> GetAllPokemonTypes(SQLiteConnection sql)
		{
			return sql.Query<PokemonType>("SELECT * FROM pokemon_types");
		}
	}
}
