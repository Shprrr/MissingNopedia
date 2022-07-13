using System;
using System.Threading.Tasks;

namespace MissingNopedia
{
	public class PokemonDbPokemonHtml : DocumentHtml
	{
		public const string WikiPokemonSuffix = "_(Pok%C3%A9mon)";

		public PokemonDbPokemonHtml(string pokemonName, string generationLearnset, bool isCustomPictures)
		{
			PokemonName = pokemonName;
			GenerationLearnset = generationLearnset;
			IsCustomPictures = isCustomPictures;
		}

		public string PokemonName { get; }
		public string GenerationLearnset { get; }
		public bool IsCustomPictures { get; }

		public override async Task LoadAsync()
		{
			if (string.IsNullOrWhiteSpace(PokemonName))
				return;

			var content = await GetPageContentAsync($"https://pokemondb.net/pokedex/{Uri.EscapeDataString(PokemonName.Replace(' ', '_'))}");
			if (content is null) return;

			//string contentGenerationLearnset = null;
			//if (!string.IsNullOrEmpty(GenerationLearnset))
			//{
			//	contentGenerationLearnset = await GetPageContentAsync($"https://bulbapedia.bulbagarden.net/wiki/{Uri.EscapeDataString(PokemonName.Replace(' ', '_'))}{WikiPokemonSuffix}/{GenerationLearnset}");
			//	if (contentGenerationLearnset is null) return;
			//}

			doc = GetHtmlDocument(content);
			//if (!string.IsNullOrEmpty(contentGenerationLearnset))
			//	learnsetDocument = GetHtmlDocument(contentGenerationLearnset);
		}

		public override string BuildNewPage()
		{
			if (doc is null) return "";
			var newDoc = ConstructPokemonDbPage();
			var body = newDoc.DocumentNode.LastChild.LastChild.FirstChild;

			var main = doc.GetElementbyId("main");
			body.AppendChild(main.Clone());

			return newDoc.DocumentNode.OuterHtml;
		}
	}
}
