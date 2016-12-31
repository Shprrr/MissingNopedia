using System.Collections.Generic;
using SQLite;

namespace MissingNopedia.AdvancedSearch.Mappings
{
	[Table("types")]
	public class Type
	{
		/*
		CREATE TABLE types (
				id INTEGER NOT NULL,
				identifier VARCHAR(79) NOT NULL,
				generation_id INTEGER NOT NULL,
				--damage_class_id INTEGER,
				PRIMARY KEY (id),
				FOREIGN KEY(generation_id) REFERENCES generations (id),
				--FOREIGN KEY(damage_class_id) REFERENCES move_damage_classes (id)
		);
		*/
		[PrimaryKey]
		public int Id { get; set; }

		[MaxLength(79), NotNull]
		public string Identifier { get; set; }

		[Column("generation_id")]
		public int GenerationId { get; set; }

		public static List<Type> GetAllTypes(SQLiteConnection sql)
		{
			return sql.Query<Type>("SELECT * FROM types WHERE id < 10000");
		}
	}
}
