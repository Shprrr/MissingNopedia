using System.Collections.Generic;
using SQLite;

namespace MissingNopedia.AdvancedSearch.Mappings
{
	[Table("stats")]
	public class Stat
	{
		/*
		CREATE TABLE stats (
				id INTEGER NOT NULL,
				damage_class_id INTEGER,
				identifier VARCHAR(79) NOT NULL,
				is_battle_only BOOLEAN NOT NULL,
				--game_index INTEGER,
				PRIMARY KEY (id),
				FOREIGN KEY(damage_class_id) REFERENCES move_damage_classes (id),
				CHECK (is_battle_only IN (0, 1))
		);
		*/
		[PrimaryKey]
		public int Id { get; set; }

		[Column("damage_class_id")]
		public int? DamageClassId { get; set; }

		[MaxLength(79), NotNull]
		public string Identifier { get; set; }

		[Column("is_battle_only")]
		public bool IsBattleOnly { get; set; }


		public bool IsHP { get { return Identifier == "hp"; } }
		public bool IsAttack { get { return Identifier == "attack"; } }
		public bool IsDefense { get { return Identifier == "defense"; } }
		public bool IsSpecialAttack { get { return Identifier == "special-attack"; } }
		public bool IsSpecialDefense { get { return Identifier == "special-defense"; } }
		public bool IsSpeed { get { return Identifier == "speed"; } }

		public static List<Stat> GetAllStats(SQLiteConnection sql)
		{
			return sql.Query<Stat>("SELECT * FROM stats");
		}
	}
}
