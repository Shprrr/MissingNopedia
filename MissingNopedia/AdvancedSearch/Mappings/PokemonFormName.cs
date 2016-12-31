using System.Collections.Generic;
using SQLite;

namespace MissingNopedia.AdvancedSearch.Mappings
{
	[Table("pokemon_form_names")]
	public class PokemonFormName
	{
		/*
		CREATE TABLE pokemon_form_names (
				pokemon_form_id INTEGER NOT NULL,
				local_language_id INTEGER NOT NULL,
				form_name VARCHAR(79),
				pokemon_name VARCHAR(79),
				PRIMARY KEY (pokemon_form_id, local_language_id),
				FOREIGN KEY(pokemon_form_id) REFERENCES pokemon_forms (id),
				FOREIGN KEY(local_language_id) REFERENCES languages (id)
		);
		CREATE INDEX ix_pokemon_form_names_pokemon_name ON pokemon_form_names (pokemon_name);
		CREATE INDEX ix_pokemon_form_names_form_name ON pokemon_form_names (form_name);
		*/
		[/*PrimaryKey, */Column("pokemon_form_id")]
		public int PokemonFormId { get; set; }

		[/*PrimaryKey, */Column("local_language_id")]
		public int LocalLanguageId { get; set; }

		[Column("form_name"), MaxLength(79), Indexed]
		public string FormName { get; set; }

		[Column("pokemon_name"), MaxLength(79), Indexed]
		public string PokemonName { get; set; }

		public static List<PokemonFormName> GetAllPokemonFormNames(SQLiteConnection sql)
		{
			return sql.Query<PokemonFormName>("SELECT * FROM pokemon_form_names WHERE local_language_id = 9");
		}
	}
}
