using System.Collections.Generic;
using SQLite;

namespace MissingNopedia.AdvancedSearch.Mappings
{
	public class Pokemon
	{
		/*
		CREATE TABLE pokemon (
				id INTEGER NOT NULL,
				identifier VARCHAR(79) NOT NULL,
				species_id INTEGER,
				height INTEGER NOT NULL,
				weight INTEGER NOT NULL,
				base_experience INTEGER NOT NULL,
				"order" INTEGER NOT NULL,
				is_default BOOLEAN NOT NULL,
				PRIMARY KEY (id),
				FOREIGN KEY(species_id) REFERENCES pokemon_species (id),
				CHECK (is_default IN (0, 1))
		);
		CREATE INDEX ix_pokemon_order ON pokemon ("order");
		CREATE INDEX ix_pokemon_is_default ON pokemon (is_default);
		*/
		[PrimaryKey]
		public int Id { get; set; }

		[MaxLength(79), NotNull]
		public string Identifier { get; set; }

		[Column("species_id")]
		public int SpeciesId { get; set; }

		[NotNull]
		public int Height { get; set; }

		[NotNull]
		public int Weight { get; set; }

		[Column("base_experience"), NotNull]
		public int BaseExperience { get; set; }

		[NotNull, Indexed]
		public int Order { get; set; }

		[Column("is_default"), NotNull, Indexed]
		public bool IsDefault { get; set; }

		public static List<Pokemon> GetAllPokemons(SQLiteConnection sql)
		{
			return sql.Query<Pokemon>("SELECT * FROM pokemon ORDER BY \"order\"");
		}
	}
}
