using System.Collections.Generic;
using System.Threading.Tasks;
using GraphQL;
using GraphQL.Client.Abstractions;
using GraphQL.Client.Http;
using GraphQL.Client.Serializer.Newtonsoft;

namespace MissingNopedia.AdvancedSearch
{
	public class AdvancedSearch
	{
		private readonly GraphQLHttpClient client = new("https://beta.pokeapi.co/graphql/v1beta", new NewtonsoftJsonSerializer());

		public async Task<Pokemon[]> RequestAsync(IEnumerable<Criteria.Criterion> criteria)
		{
			GraphQLRequest request = new(@"query PokeAPIquery {
  pokemons: pokemon_v2_pokemonspecies(order_by: {id: asc}) {
    number: id
    order
    name: pokemon_v2_pokemonspeciesnames(where: {language_id: {_eq: 9}}) {
      name
    }
  }
}", null, "PokeAPIquery");
			var response = await client.SendQueryAsync(request, () => new { pokemons = new List<Pokemon>() });

			Pokemon[] results = response.Data.pokemons.ToArray();
			foreach (var criterion in criteria)
			{
				results = criterion.ApplyCriterion(results);
			}

			return results;
		}
	}
}
