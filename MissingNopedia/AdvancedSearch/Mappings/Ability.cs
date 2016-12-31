using System.Collections.Generic;
using SQLite;

namespace MissingNopedia.AdvancedSearch.Mappings
{
	[Table("abilities")]
	public class Ability
	{
		/*
		CREATE TABLE abilities (
				id INTEGER NOT NULL,
				identifier VARCHAR(79) NOT NULL,
				generation_id INTEGER NOT NULL,
				--is_main_series BOOLEAN NOT NULL,
				PRIMARY KEY (id),
				FOREIGN KEY(generation_id) REFERENCES generations (id),
				--CHECK (is_main_series IN (0, 1))
		);
		--CREATE INDEX ix_abilities_is_main_series ON abilities (is_main_series);
		*/
		[PrimaryKey]
		public int Id { get; set; }

		[MaxLength(79), NotNull]
		public string Identifier { get; set; }

		[Column("generation_id")]
		public int GenerationId { get; set; }

		public static List<Ability> GetAllAbilities(SQLiteConnection sql)
		{
			return sql.Query<Ability>("SELECT * FROM abilities WHERE is_main_series = 1");
		}
	}
}
