using System.Collections.Generic;
using SQLite;

namespace MissingNopedia.AdvancedSearch.Mappings
{
	[Table("pokemon_abilities")]
	public class PokemonAbility
	{
		/*
		CREATE TABLE pokemon_abilities (
				pokemon_id INTEGER NOT NULL,
				ability_id INTEGER NOT NULL,
				is_hidden BOOLEAN NOT NULL,
				slot INTEGER NOT NULL,
				PRIMARY KEY (pokemon_id, slot),
				FOREIGN KEY(pokemon_id) REFERENCES pokemon (id),
				FOREIGN KEY(ability_id) REFERENCES abilities (id),
				CHECK (is_hidden IN (0, 1))
		);
		CREATE INDEX ix_pokemon_abilities_is_hidden ON pokemon_abilities (is_hidden);
		*/
		[/*PrimaryKey, */Column("pokemon_id")]
		public int PokemonId { get; set; }

		[Column("ability_id")]
		public int AbilityId { get; set; }

		[Column("is_hidden"), Indexed]
		public bool IsHidden { get; set; }

		//[PrimaryKey]
		public int Slot { get; set; }

		public static List<PokemonAbility> GetAllPokemonAbilities(SQLiteConnection sql)
		{
			return sql.Query<PokemonAbility>("SELECT * FROM pokemon_abilities");
		}
	}
}
