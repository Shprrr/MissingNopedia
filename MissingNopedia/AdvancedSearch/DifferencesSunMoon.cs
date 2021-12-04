using SQLite;

namespace MissingNopedia.AdvancedSearch
{
	class DifferencesSunMoon
	{
		internal static void ExecuteDifferences()
		{
			using var sql = new SQLiteConnection("pokedex.sqlite");
			sql.Execute("INSERT OR REPLACE INTO regions VALUES(?, ?)", 7, "alola");
		}

		internal static void ExecuteRemoveConquest()
		{
			using var sql = new SQLiteConnection("pokedex.sqlite");
			sql.Execute("DROP TABLE IF EXISTS conquest_pokemon_abilities");
			sql.Execute("DROP TABLE IF EXISTS conquest_pokemon_evolution");
			sql.Execute("DROP TABLE IF EXISTS conquest_pokemon_moves");
			sql.Execute("DROP TABLE IF EXISTS conquest_pokemon_stats");
			sql.Execute("DROP TABLE IF EXISTS conquest_kingdom_names");
			sql.Execute("DROP TABLE IF EXISTS conquest_kingdoms");
			sql.Execute("DROP TABLE IF EXISTS conquest_max_links");
			sql.Execute("DROP TABLE IF EXISTS conquest_move_data");
			sql.Execute("DROP TABLE IF EXISTS conquest_move_displacement_prose");
			sql.Execute("DROP TABLE IF EXISTS conquest_move_displacements");
			sql.Execute("DROP TABLE IF EXISTS conquest_move_effect_prose");
			sql.Execute("DROP TABLE IF EXISTS conquest_move_effects");
			sql.Execute("DROP TABLE IF EXISTS conquest_move_range_prose");
			sql.Execute("DROP TABLE IF EXISTS conquest_move_ranges");
			sql.Execute("DROP TABLE IF EXISTS conquest_stat_names");
			sql.Execute("DROP TABLE IF EXISTS conquest_stats");
			sql.Execute("DROP TABLE IF EXISTS conquest_transformation_pokemon");
			sql.Execute("DROP TABLE IF EXISTS conquest_transformation_warriors");
			sql.Execute("DROP TABLE IF EXISTS conquest_warrior_names");
			sql.Execute("DROP TABLE IF EXISTS conquest_warrior_rank_stat_map");
			sql.Execute("DROP TABLE IF EXISTS conquest_warrior_transformation");
			sql.Execute("DROP TABLE IF EXISTS conquest_warrior_ranks");
			sql.Execute("DROP TABLE IF EXISTS conquest_warrior_skill_names");
			sql.Execute("DROP TABLE IF EXISTS conquest_warrior_skills");
			sql.Execute("DROP TABLE IF EXISTS conquest_warrior_specialties");
			sql.Execute("DROP TABLE IF EXISTS conquest_warrior_stat_names");
			sql.Execute("DROP TABLE IF EXISTS conquest_warrior_stats");
			sql.Execute("DROP TABLE IF EXISTS conquest_episode_names");
			sql.Execute("DROP TABLE IF EXISTS conquest_episode_warriors");
			sql.Execute("DROP TABLE IF EXISTS conquest_episodes");
			sql.Execute("DROP TABLE IF EXISTS conquest_warriors");
			sql.Execute("DROP TABLE IF EXISTS conquest_warrior_archetypes");
		}
	}
}
