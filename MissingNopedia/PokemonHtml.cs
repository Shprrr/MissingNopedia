namespace MissingNopedia
{
	public class PokemonHtml : DocumentHtml
	{
		private const string Generation1to5 = "Generation_I-V";
		private const string GameData = "Game_data";
		private const string GameLocations = "Game_locations";
		private const string HeldItems = "Held_items";
		private const string Stats = "Stats";
		private const string BaseStats = "Base_stats";
		private const string PokeathlonStats = "Pok.C3.A9athlon_stats";
		private const string TypeEffectiveness = "Type_effectiveness";
		private const string Learnset = "Learnset";
		private const string AnimeOnly = "Anime-only_moves";
		private const string TCGOnly = "TCG-only_moves";
		private const string TCGExclusive = "TCG-exclusive_moves";
		private const string Evolution = "Evolution";
		private const string Sprites = "Sprites";
		private const string Trivia = "Trivia";
		private const string InOtherLanguages = "In_other_languages";

		private readonly HtmlAgilityPack.HtmlDocument learnsetDocument;

		public PokemonHtml(string html, string htmlLearnset) : base(html)
		{
			if (!string.IsNullOrEmpty(htmlLearnset))
				learnsetDocument = GetHtmlDocument(htmlLearnset);
		}

		public override string BuildNewPage()
		{
			var title = doc.DocumentNode.SelectSingleNode("//title").InnerHtml;
			var newDoc = ConstructBulbapediaPage();
			var body = newDoc.DocumentNode.LastChild.LastChild.FirstChild;

			var content = doc.GetElementbyId(ContentText);
			var summary = content.SelectSingleNode(".//*[@class='roundy']");
			if (title.StartsWith("Scolipede"))
				summary.SelectSingleNode(".//img").SetAttributeValue("src", "http://24.media.tumblr.com/tumblr_liimqxDoT51qekruyo1_500.png");
			var rows = summary.SelectNodes(".//tr");
			rows[^7].SetAttributeValue("style", "display: none"); // Mega Stones
			rows[^6].SetAttributeValue("style", "display: none"); // Pokédex Numbers
			rows[^5].SetAttributeValue("style", "display: none"); // Experience
			rows[^3].SetAttributeValue("style", "display: none"); // Body/Footprint
			rows[^2].SetAttributeValue("style", "display: none"); // Color/Friendship
			rows[^1].SetAttributeValue("style", "display: none"); // External Links
			body.AppendChild(summary.Clone()); // Clone to keep link with the old document.

			var node = summary.NextSibling;
			do
			{
				if (node.Id == TableOfContents)
					break;

				body.AppendChild(node.Clone());
			} while ((node = node.NextSibling) != null);

			node = doc.GetElementbyId(GameData)?.ParentNode;
			if (node != null)
				body.AppendChild(node);

			var section = AddSection(TypeEffectiveness);
			if (section != null)
				body.AppendChildren(section);

			var stats = AddSection(Stats);
			if (stats == null) stats = AddSection(BaseStats); // Some pages have the BaseStats subsection for the Stats section.
			RemoveSection(stats, Generation1to5);
			RemoveSection(stats, PokeathlonStats);
			if (stats != null)
				body.AppendChildren(stats);

			var learnset = AddSection(Learnset);
			if (learnset != null && learnsetDocument != null)
			{
				var learnsetTitle = learnset[0]; // Title of the section
				learnset.Clear();
				learnset.Add(learnsetTitle);
				var learnsetContentNode = learnsetDocument.GetElementbyId(ContentText);
				for (int i = 1; i < learnsetContentNode.FirstChild.ChildNodes.Count; i++)
				{
					learnset.Append(learnsetContentNode.FirstChild.ChildNodes[i]);
				}
			}
			RemoveSection(learnset, AnimeOnly);
			RemoveSection(learnset, TCGOnly);
			RemoveSection(learnset, TCGExclusive);
			if (learnset != null)
				body.AppendChildren(learnset);

			section = AddSection(Evolution);
			if (section != null)
				body.AppendChildren(section);

			section = AddSection(GameLocations);
			if (section != null)
				body.AppendChildren(section);

			section = AddSection(HeldItems);
			if (section != null)
				body.AppendChildren(section);

			section = AddSection(Trivia);
			if (section != null)
				body.AppendChildren(section);

			section = AddSection(InOtherLanguages);
			if (section != null)
				body.AppendChildren(section);

			section = AddSection(Sprites);
			if (section != null)
				body.AppendChildren(section);

			return newDoc.DocumentNode.OuterHtml;
		}
	}
}
