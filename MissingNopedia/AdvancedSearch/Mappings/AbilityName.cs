using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace MissingNopedia.AdvancedSearch.Mappings
{
	[Table("ability_names")]
	public class AbilityName
	{
		/*
		CREATE TABLE ability_names (
				ability_id INTEGER NOT NULL,
				local_language_id INTEGER NOT NULL,
				name VARCHAR(79) NOT NULL,
				PRIMARY KEY (ability_id, local_language_id),
				FOREIGN KEY(ability_id) REFERENCES abilities (id),
				FOREIGN KEY(local_language_id) REFERENCES languages (id)
		);
		CREATE INDEX ix_ability_names_name ON ability_names (name);
		*/
		[/*PrimaryKey, */Column("ability_id")]
		public int AbilityId { get; set; }

		[/*PrimaryKey, */Column("local_language_id")]
		public int LocalLanguageId { get; set; }

		[MaxLength(79), NotNull, Indexed]
		public string Name { get; set; }

		public static List<AbilityName> GetAllAbilityNames(SQLiteConnection sql)
		{
			return sql.Query<AbilityName>("SELECT * FROM ability_names WHERE local_language_id = 9");
		}
	}
}
