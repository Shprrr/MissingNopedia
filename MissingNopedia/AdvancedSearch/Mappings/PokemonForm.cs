using System.Collections.Generic;
using SQLite;

namespace MissingNopedia.AdvancedSearch.Mappings
{
	[Table("pokemon_forms")]
	public class PokemonForm
	{
		/*
		CREATE TABLE pokemon_forms (
				id INTEGER NOT NULL,
				identifier VARCHAR(79) NOT NULL,
				form_identifier VARCHAR(79),
				pokemon_id INTEGER NOT NULL,
				introduced_in_version_group_id INTEGER,
				is_default BOOLEAN NOT NULL,
				is_battle_only BOOLEAN NOT NULL,
				is_mega BOOLEAN NOT NULL,
				form_order INTEGER NOT NULL,
				"order" INTEGER NOT NULL,
				PRIMARY KEY (id),
				FOREIGN KEY(pokemon_id) REFERENCES pokemon (id),
				FOREIGN KEY(introduced_in_version_group_id) REFERENCES version_groups (id),
				CHECK (is_default IN (0, 1)),
				CHECK (is_battle_only IN (0, 1)),
				CHECK (is_mega IN (0, 1))
		);
		*/
		[PrimaryKey]
		public int Id { get; set; }

		[MaxLength(79), NotNull]
		public string Identifier { get; set; }

		[Column("form_identifier"), MaxLength(79)]
		public string FormIdentifier { get; set; }

		[Column("pokemon_id")]
		public int PokemonId { get; set; }

		[Column("introduced_in_version_group_id")]
		public int? IntroducedInVersionGroupId { get; set; }

		[Column("is_default")]
		public bool IsDefault { get; set; }

		[Column("is_battle_only")]
		public bool IsBattleOnly { get; set; }

		[Column("is_mega")]
		public bool IsMega { get; set; }

		public static List<PokemonForm> GetAllPokemonForms(SQLiteConnection sql)
		{
			return sql.Query<PokemonForm>("SELECT * FROM pokemon_forms ORDER BY \"order\"");
		}
	}
}
