using System.Collections.Generic;
using SQLite;

namespace MissingNopedia.AdvancedSearch.Mappings
{
	[Table("pokemon_species")]
	public class PokemonSpecie
	{
		/*
		CREATE TABLE pokemon_species (
				id INTEGER NOT NULL,
				identifier VARCHAR(79) NOT NULL,
				generation_id INTEGER,
				evolves_from_species_id INTEGER,
				evolution_chain_id INTEGER,
				color_id INTEGER NOT NULL,
				shape_id INTEGER NOT NULL,
				habitat_id INTEGER,
				gender_rate INTEGER NOT NULL,
				capture_rate INTEGER NOT NULL,
				base_happiness INTEGER NOT NULL,
				is_baby BOOLEAN NOT NULL,
				hatch_counter INTEGER NOT NULL,
				has_gender_differences BOOLEAN NOT NULL,
				growth_rate_id INTEGER NOT NULL,
				forms_switchable BOOLEAN NOT NULL,
				"order" INTEGER NOT NULL,
				--conquest_order INTEGER,
				PRIMARY KEY (id),
				FOREIGN KEY(generation_id) REFERENCES generations (id),
				FOREIGN KEY(evolves_from_species_id) REFERENCES pokemon_species (id),
				FOREIGN KEY(evolution_chain_id) REFERENCES evolution_chains (id),
				FOREIGN KEY(color_id) REFERENCES pokemon_colors (id),
				FOREIGN KEY(shape_id) REFERENCES pokemon_shapes (id),
				FOREIGN KEY(habitat_id) REFERENCES pokemon_habitats (id),
				CHECK (is_baby IN (0, 1)),
				CHECK (has_gender_differences IN (0, 1)),
				FOREIGN KEY(growth_rate_id) REFERENCES growth_rates (id),
				CHECK (forms_switchable IN (0, 1))
		);
		CREATE INDEX ix_pokemon_species_order ON pokemon_species ("order");
		--CREATE INDEX ix_pokemon_species_conquest_order ON pokemon_species (conquest_order);
		*/
		[PrimaryKey]
		public int Id { get; set; }

		[MaxLength(79), NotNull]
		public string Identifier { get; set; }

		[Column("generation_id")]
		public int? GenerationId { get; set; }

		[Column("evolves_from_species_id")]
		public int? EvolvesFromSpeciesId { get; set; }

		public static List<PokemonSpecie> GetAllPokemonSpecies(SQLiteConnection sql)
		{
			return sql.Query<PokemonSpecie>("SELECT * FROM pokemon_species ORDER BY \"order\"");
		}
	}
}
