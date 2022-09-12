using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GraphQL;
using GraphQL.Client.Abstractions;
using GraphQL.Client.Http;
using GraphQL.Client.Serializer.Newtonsoft;

namespace MissingNopedia.AdvancedSearch
{
	public static class AdvancedSearch
	{
		private static readonly GraphQLHttpClient client = new("https://beta.pokeapi.co/graphql/v1beta", new NewtonsoftJsonSerializer());

		private static Pokemon[] PokemonsCache { get; set; }

		public static async Task<Pokemon[]> LoadAsync()
		{
			if (PokemonsCache is not null) return PokemonsCache;

			GraphQLRequest request = new(@"query PokeAPIquery {
  pokemons: pokemon_v2_pokemonspecies(order_by: {id: asc}) {
    number: id
    order
    species: pokemon_v2_pokemonspeciesnames(where: {language_id: {_eq: 9}}) {
      name
      genus
    }
    pokedexNumbers: pokemon_v2_pokemondexnumbers(where: {pokemon_v2_pokedex: {is_main_series: {_eq: true}}}) {
      pokedex_number
      pokedexName: pokemon_v2_pokedex {
        name
      }
    }
    pokedexEntries: pokemon_v2_pokemonspeciesflavortexts(where: {language_id: {_eq: 9}}, order_by: {version_id: asc}) {
      version_id
      version: pokemon_v2_version {
        name
      }
      flavor_text
    }
    forms: pokemon_v2_pokemons {
      id
      height
      weight
      base_experience
      form: pokemon_v2_pokemonforms {
        form_order
        form_name
        is_default
        is_battle_only
        is_mega
        name: pokemon_v2_pokemonformnames(where: {language_id: {_eq: 9}}) {
          name
        }
      }
      types: pokemon_v2_pokemontypes {
        type: pokemon_v2_type {
          name
          efficacies: pokemonV2TypeefficaciesByTargetTypeId {
            damage_type: pokemon_v2_type {
              name
            }
            target_type: pokemonV2TypeByTargetTypeId {
              name
            }
            damage_factor
          }
        }
      }
      baseStats: pokemon_v2_pokemonstats {
        stat_id
        base_stat
        effort
      }
      abilities: pokemon_v2_pokemonabilities {
        slot
        is_hidden
        ability: pokemon_v2_ability {
          name: pokemon_v2_abilitynames(where: {language_id: {_eq: 9}}) {
            name
          }
          descriptions: pokemon_v2_abilityflavortexts(where: {language_id: {_eq: 9}}) {
            version_group_id
            versionGroup: pokemon_v2_versiongroup {
              name
            }
            flavor_text
          }
        }
      }
    }
    capture_rate
    base_happiness
    growthRate: pokemon_v2_growthrate {
      name
    }
    gender_rate
    hatch_counter
    eggGroups: pokemon_v2_pokemonegggroups {
      eggGroup: pokemon_v2_egggroup {
        name
      }
    }
    evolvesFrom: pokemon_v2_pokemonevolutions {
      specy: pokemon_v2_pokemonspecy {
        number: evolves_from_species_id
      }
      evolutionTrigger: pokemon_v2_evolutiontrigger {
        name
      }
      min_level
      min_happiness
      time_of_day
      relative_physical_stats
      min_beauty
      location_id
      location: pokemon_v2_location {
        region: pokemon_v2_region {
          name: pokemon_v2_regionnames(where: {language_id: {_eq: 9}}) {
            name
          }
        }
        name: pokemon_v2_locationnames(where: {language_id: {_eq: 9}}) {
          name
        }
      }
      known_move_id
      knownMove: pokemon_v2_move {
        name: pokemon_v2_movenames(where: {language_id: {_eq: 9}}) {
          name
        }
      }
      party_species_id
      gender_id
      min_affection
      known_move_type_id
      knownMoveType: pokemon_v2_type {
        name
      }
      party_type_id
      partyType: pokemonV2TypeByPartyTypeId {
        name
      }
      turn_upside_down
      needs_overworld_rain
      trade_species_id
      held_item_id
      heldItem: pokemonV2ItemByHeldItemId {
        name: pokemon_v2_itemnames(where: {language_id: {_eq: 9}}) {
          name
        }
      }
      evolution_item_id
      evolutionItem: pokemon_v2_item {
        name: pokemon_v2_itemnames(where: {language_id: {_eq: 9}}) {
          name
        }
      }
    }
    babyBreeding: pokemon_v2_evolutionchain {
      baby_trigger_item_id
      babyTriggerItem: pokemon_v2_item {
        name: pokemon_v2_itemnames(where: {language_id: {_eq: 9}}) {
          name
        }
      }
    }
  }
}", null, "PokeAPIquery");
			var response = await client.SendQueryAsync(request, () => new { pokemons = new List<Pokemon>() });

			List<Pokemon> pokemons = response.Data.pokemons;
			PokemonsCache = pokemons.ToArray();
			pokemons.ForEach(p => p.SetEvolutions(PokemonsCache));
			return PokemonsCache;
		}

		public static async Task<Pokemon[]> RequestAsync(IEnumerable<Criteria.Criterion> criteria, bool includeForms)
		{
			Pokemon[] results = await LoadAsync();
			if (includeForms) results = results.SelectMany(p => p.GetForms()).ToArray();

			var filter = (Pokemon pokemon) => true;
			foreach (var criterion in criteria)
			{
				filter = criterion.ApplyCriterion(filter);
			}

			return results.Where(filter).ToArray();
		}
	}
}
