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
    pokedexEntries: pokemon_v2_pokemonspeciesflavortexts(where: {language_id: {_eq: 9}}) {
      version_id
      version: pokemon_v2_version {
        name
      }
      flavor_text
    }
    forms: pokemon_v2_pokemons {
      id
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
      }
      abilities: pokemon_v2_pokemonabilities {
        slot
        is_hidden
        ability: pokemon_v2_ability {
          name: pokemon_v2_abilitynames(where: {language_id: {_eq: 9}}) {
            name
          }
        }
      }
    }
  }
}", null, "PokeAPIquery");
			var response = await client.SendQueryAsync(request, () => new { pokemons = new List<Pokemon>() });

			PokemonsCache = response.Data.pokemons.ToArray();
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
