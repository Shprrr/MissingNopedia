using System.Collections.Generic;
using SQLite;

namespace MissingNopedia.AdvancedSearch.Mappings
{
	[Table("pokemon_species_names")]
	public class PokemonSpecieName
	{
		/*
		CREATE TABLE pokemon_species_names (
				pokemon_species_id INTEGER NOT NULL,
				local_language_id INTEGER NOT NULL,
				name VARCHAR(79),
				genus TEXT,
				PRIMARY KEY (pokemon_species_id, local_language_id),
				FOREIGN KEY(pokemon_species_id) REFERENCES pokemon_species (id),
				FOREIGN KEY(local_language_id) REFERENCES languages (id)
		);
		CREATE INDEX ix_pokemon_species_names_name ON pokemon_species_names (name);
		*/
		[/*PrimaryKey, */Column("pokemon_species_id")]
		public int PokemonSpeciesId { get; set; }

		[/*PrimaryKey, */Column("local_language_id")]
		public int LocalLanguageId { get; set; }

		[MaxLength(79), Indexed]
		public string Name { get; set; }

		public string Genus { get; set; }

		public static List<PokemonSpecieName> GetAllPokemonSpeciesNames(SQLiteConnection sql)
		{
			return sql.Query<PokemonSpecieName>("SELECT * FROM pokemon_species_names WHERE local_language_id = 9");
		}
	}
}
