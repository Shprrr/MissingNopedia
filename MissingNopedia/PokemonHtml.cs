namespace MissingNopedia
{
	public class PokemonHtml : DocumentHtml
	{
		public const string WikiPokemonSuffix = "_(Pok%C3%A9mon)";

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

		public PokemonHtml(string pokemonName, string html, string htmlLearnset, bool isCustomPictures) : base(html)
		{
			if (!string.IsNullOrEmpty(htmlLearnset))
				learnsetDocument = GetHtmlDocument(htmlLearnset);
			PokemonName = pokemonName;
			IsCustomPictures = isCustomPictures;
		}

		public string PokemonName { get; }
		public bool IsCustomPictures { get; }

		public override string BuildNewPage()
		{
			var newDoc = ConstructBulbapediaPage();
			var body = newDoc.DocumentNode.LastChild.LastChild.FirstChild;

			var content = doc.GetElementbyId(ContentText);
			var summary = content.SelectSingleNode(".//*[@class='roundy']");
			if (IsCustomPictures) ReplaceProfilePicture(summary);

			var rows = summary.SelectNodes("tbody/tr");
			rows[^6].SetAttributeValue("style", "display: none"); // Mega Stones
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

		private void ReplaceProfilePicture(HtmlAgilityPack.HtmlNode summary)
		{
			var imgProfile = summary.SelectSingleNode(".//img");
			switch (PokemonName)
			{
				case "Scolipede":
					imgProfile.SetAttributeValue("src", "http://24.media.tumblr.com/tumblr_liimqxDoT51qekruyo1_500.png");
					break;

				case "Castform":
					imgProfile.SetAttributeValue("src", AFD.GetImageUrl(PokemonName));
					summary.SelectSingleNode(".//img[@alt='Sunny Form']").SetAttributeValue("src", AFD.GetImageUrl("Sunny Castform"));
					summary.SelectSingleNode(".//img[@alt='Rainy Form']").SetAttributeValue("src", AFD.GetImageUrl("Rainy Castform"));
					summary.SelectSingleNode(".//img[@alt='Snowy Form']").SetAttributeValue("src", AFD.GetImageUrl("Snowy Castform"));
					break;

				case "Deoxys":
					imgProfile.SetAttributeValue("src", AFD.GetImageUrl(PokemonName));
					summary.SelectSingleNode(".//img[@alt='Attack Forme']").SetAttributeValue("src", AFD.GetImageUrl("Attack Deoxys"));
					summary.SelectSingleNode(".//img[@alt='Defense Forme']").SetAttributeValue("src", AFD.GetImageUrl("Defense Deoxys"));
					summary.SelectSingleNode(".//img[@alt='Speed Forme']").SetAttributeValue("src", AFD.GetImageUrl("Speed Deoxys"));
					break;

				case "Wormadam":
					imgProfile.SetAttributeValue("src", AFD.GetImageUrl(PokemonName));
					summary.SelectSingleNode(".//img[@alt='Sandy Cloak']").SetAttributeValue("src", AFD.GetImageUrl("Sandy Wormadam"));
					summary.SelectSingleNode(".//img[@alt='Trash Cloak']").SetAttributeValue("src", AFD.GetImageUrl("Trash Wormadam"));
					break;

				case "Basculin":
					imgProfile.SetAttributeValue("src", AFD.GetImageUrl(PokemonName));
					summary.SelectSingleNode(".//img[@alt='Blue-Striped Form']").SetAttributeValue("src", AFD.GetImageUrl("Blue-Striped Basculin"));
					break;

				case "Zygarde":
					imgProfile.SetAttributeValue("src", AFD.GetImageUrl(PokemonName));
					summary.SelectSingleNode(".//img[@alt='10% Forme']").SetAttributeValue("src", AFD.GetImageUrl("10% Zygarde"));
					summary.SelectSingleNode(".//img[@alt='Complete Forme']").SetAttributeValue("src", AFD.GetImageUrl("Complete Zygarde"));
					break;

				case "Rattata":
					imgProfile.SetAttributeValue("src", AFD.GetImageUrl(PokemonName));
					summary.SelectSingleNode(".//img[@alt='Alolan Rattata']").SetAttributeValue("src", AFD.GetImageUrl("Alolan Rattata"));
					break;

				case "Raticate":
					imgProfile.SetAttributeValue("src", AFD.GetImageUrl(PokemonName));
					summary.SelectSingleNode(".//img[@alt='Alolan Raticate']").SetAttributeValue("src", AFD.GetImageUrl("Alolan Raticate"));
					break;

				case "Raichu":
					imgProfile.SetAttributeValue("src", AFD.GetImageUrl(PokemonName));
					summary.SelectSingleNode(".//img[@alt='Alolan Raichu']").SetAttributeValue("src", AFD.GetImageUrl("Alolan Raichu"));
					break;

				case "Sandshrew":
					imgProfile.SetAttributeValue("src", AFD.GetImageUrl(PokemonName));
					summary.SelectSingleNode(".//img[@alt='Alolan Sandshrew']").SetAttributeValue("src", AFD.GetImageUrl("Alolan Sandshrew"));
					break;

				case "Sandslash":
					imgProfile.SetAttributeValue("src", AFD.GetImageUrl(PokemonName));
					summary.SelectSingleNode(".//img[@alt='Alolan Sandslash']").SetAttributeValue("src", AFD.GetImageUrl("Alolan Sandslash"));
					break;

				case "Vulpix":
					imgProfile.SetAttributeValue("src", AFD.GetImageUrl(PokemonName));
					summary.SelectSingleNode(".//img[@alt='Alolan Vulpix']").SetAttributeValue("src", AFD.GetImageUrl("Alolan Vulpix"));
					break;

				case "Ninetales":
					imgProfile.SetAttributeValue("src", AFD.GetImageUrl(PokemonName));
					summary.SelectSingleNode(".//img[@alt='Alolan Ninetales']").SetAttributeValue("src", AFD.GetImageUrl("Alolan Ninetales"));
					break;

				case "Diglett":
					imgProfile.SetAttributeValue("src", AFD.GetImageUrl(PokemonName));
					summary.SelectSingleNode(".//img[@alt='Alolan Diglett']").SetAttributeValue("src", AFD.GetImageUrl("Alolan Diglett"));
					break;

				case "Dugtrio":
					imgProfile.SetAttributeValue("src", AFD.GetImageUrl(PokemonName));
					summary.SelectSingleNode(".//img[@alt='Alolan Dugtrio']").SetAttributeValue("src", AFD.GetImageUrl("Alolan Dugtrio"));
					break;

				case "Meowth":
					imgProfile.SetAttributeValue("src", AFD.GetImageUrl(PokemonName));
					summary.SelectSingleNode(".//img[@alt='Alolan Meowth']").SetAttributeValue("src", AFD.GetImageUrl("Alolan Meowth"));
					summary.SelectSingleNode(".//img[@alt='Galarian Meowth']").SetAttributeValue("src", AFD.GetImageUrl("Galarian Meowth"));
					break;

				case "Persian":
					imgProfile.SetAttributeValue("src", AFD.GetImageUrl(PokemonName));
					summary.SelectSingleNode(".//img[@alt='Alolan Persian']").SetAttributeValue("src", AFD.GetImageUrl("Alolan Persian"));
					break;

				case "Geodude":
					imgProfile.SetAttributeValue("src", AFD.GetImageUrl(PokemonName));
					summary.SelectSingleNode(".//img[@alt='Alolan Geodude']").SetAttributeValue("src", AFD.GetImageUrl("Alolan Geodude"));
					break;

				case "Graveler":
					imgProfile.SetAttributeValue("src", AFD.GetImageUrl(PokemonName));
					summary.SelectSingleNode(".//img[@alt='Alolan Graveler']").SetAttributeValue("src", AFD.GetImageUrl("Alolan Graveler"));
					break;

				case "Golem":
					imgProfile.SetAttributeValue("src", AFD.GetImageUrl(PokemonName));
					summary.SelectSingleNode(".//img[@alt='Alolan Golem']").SetAttributeValue("src", AFD.GetImageUrl("Alolan Golem"));
					break;

				case "Grimer":
					imgProfile.SetAttributeValue("src", AFD.GetImageUrl(PokemonName));
					summary.SelectSingleNode(".//img[@alt='Alolan Grimer']").SetAttributeValue("src", AFD.GetImageUrl("Alolan Grimer"));
					break;

				case "Muk":
					imgProfile.SetAttributeValue("src", AFD.GetImageUrl(PokemonName));
					summary.SelectSingleNode(".//img[@alt='Alolan Muk']").SetAttributeValue("src", AFD.GetImageUrl("Alolan Muk"));
					break;

				case "Exeggutor":
					imgProfile.SetAttributeValue("src", AFD.GetImageUrl(PokemonName));
					summary.SelectSingleNode(".//img[@alt='Alolan Exeggutor']").SetAttributeValue("src", AFD.GetImageUrl("Alolan Exeggutor"));
					break;

				case "Marowak":
					imgProfile.SetAttributeValue("src", AFD.GetImageUrl(PokemonName));
					summary.SelectSingleNode(".//img[@alt='Alolan Marowak']").SetAttributeValue("src", AFD.GetImageUrl("Alolan Marowak"));
					break;

				case "Oricorio":
					imgProfile.SetAttributeValue("src", AFD.GetImageUrl(PokemonName));
					summary.SelectSingleNode(".//img[@alt='Pom-Pom Style']").SetAttributeValue("src", AFD.GetImageUrl("Pom-pom Oricorio"));
					summary.SelectSingleNode(".//img[contains(@alt, 'u Style')]").SetAttributeValue("src", AFD.GetImageUrl("Pa'u Oricorio"));
					summary.SelectSingleNode(".//img[@alt='Sensu Style']").SetAttributeValue("src", AFD.GetImageUrl("Sensu Oricorio"));
					break;

				case "Urshifu":
					imgProfile.SetAttributeValue("src", AFD.GetImageUrl(PokemonName));
					summary.SelectSingleNode(".//img[@alt='Rapid Strike Style']").SetAttributeValue("src", AFD.GetImageUrl("Rapid Strike Urshifu"));
					break;

				case "Ponyta":
					imgProfile.SetAttributeValue("src", AFD.GetImageUrl(PokemonName));
					summary.SelectSingleNode(".//img[@alt='Galarian Ponyta']").SetAttributeValue("src", AFD.GetImageUrl("Galarian Ponyta"));
					break;

				case "Rapidash":
					imgProfile.SetAttributeValue("src", AFD.GetImageUrl(PokemonName));
					summary.SelectSingleNode(".//img[@alt='Galarian Rapidash']").SetAttributeValue("src", AFD.GetImageUrl("Galarian Rapidash"));
					break;

				case "Slowpoke":
					imgProfile.SetAttributeValue("src", AFD.GetImageUrl(PokemonName));
					summary.SelectSingleNode(".//img[@alt='Galarian Slowpoke']").SetAttributeValue("src", AFD.GetImageUrl("Galarian Slowpoke"));
					break;

				case "Slowbro":
					imgProfile.SetAttributeValue("src", AFD.GetImageUrl(PokemonName));
					summary.SelectSingleNode(".//img[@alt='Galarian Slowbro']").SetAttributeValue("src", AFD.GetImageUrl("Galarian Slowbro"));
					break;

				case "Farfetch'd":
					imgProfile.SetAttributeValue("src", AFD.GetImageUrl(PokemonName));
					summary.SelectSingleNode(".//img[contains(@alt, 'Galarian Farfetch')]").SetAttributeValue("src", AFD.GetImageUrl("Galarian Farfetch'd"));
					break;

				case "Weezing":
					imgProfile.SetAttributeValue("src", AFD.GetImageUrl(PokemonName));
					summary.SelectSingleNode(".//img[@alt='Galarian Weezing']").SetAttributeValue("src", AFD.GetImageUrl("Galarian Weezing"));
					break;

				case "Mr. Mime":
					imgProfile.SetAttributeValue("src", AFD.GetImageUrl(PokemonName));
					summary.SelectSingleNode(".//img[@alt='Galarian Mr. Mime']").SetAttributeValue("src", AFD.GetImageUrl("Galarian Mr. Mime"));
					break;

				case "Articuno":
					imgProfile.SetAttributeValue("src", AFD.GetImageUrl(PokemonName));
					summary.SelectSingleNode(".//img[@alt='Galarian Articuno']").SetAttributeValue("src", AFD.GetImageUrl("Galarian Articuno"));
					break;

				case "Zapdos":
					imgProfile.SetAttributeValue("src", AFD.GetImageUrl(PokemonName));
					summary.SelectSingleNode(".//img[@alt='Galarian Zapdos']").SetAttributeValue("src", AFD.GetImageUrl("Galarian Zapdos"));
					break;

				case "Moltres":
					imgProfile.SetAttributeValue("src", AFD.GetImageUrl(PokemonName));
					summary.SelectSingleNode(".//img[@alt='Galarian Moltres']").SetAttributeValue("src", AFD.GetImageUrl("Galarian Moltres"));
					break;

				case "Slowking":
					imgProfile.SetAttributeValue("src", AFD.GetImageUrl(PokemonName));
					summary.SelectSingleNode(".//img[@alt='Galarian Slowking']").SetAttributeValue("src", AFD.GetImageUrl("Galarian Slowking"));
					break;

				case "Corsola":
					imgProfile.SetAttributeValue("src", AFD.GetImageUrl(PokemonName));
					summary.SelectSingleNode(".//img[@alt='Galarian Corsola']").SetAttributeValue("src", AFD.GetImageUrl("Galarian Corsola"));
					break;

				case "Zigzagoon":
					imgProfile.SetAttributeValue("src", AFD.GetImageUrl(PokemonName));
					summary.SelectSingleNode(".//img[@alt='Galarian Zigzagoon']").SetAttributeValue("src", AFD.GetImageUrl("Galarian Zigzagoon"));
					break;

				case "Linoone":
					imgProfile.SetAttributeValue("src", AFD.GetImageUrl(PokemonName));
					summary.SelectSingleNode(".//img[@alt='Galarian Linoone']").SetAttributeValue("src", AFD.GetImageUrl("Galarian Linoone"));
					break;

				case "Darumaka":
					imgProfile.SetAttributeValue("src", AFD.GetImageUrl(PokemonName));
					summary.SelectSingleNode(".//img[@alt='Galarian Darumaka']").SetAttributeValue("src", AFD.GetImageUrl("Galarian Darumaka"));
					break;

				case "Darmanitan":
					imgProfile.SetAttributeValue("src", AFD.GetImageUrl(PokemonName));
					summary.SelectSingleNode(".//img[@alt='Galarian Standard Mode']").SetAttributeValue("src", AFD.GetImageUrl("Galarian Darmanitan"));
					break;

				case "Yamask":
					imgProfile.SetAttributeValue("src", AFD.GetImageUrl(PokemonName));
					summary.SelectSingleNode(".//img[@alt='Galarian Yamask']").SetAttributeValue("src", AFD.GetImageUrl("Galarian Yamask"));
					break;

				case "Stunfisk":
					imgProfile.SetAttributeValue("src", AFD.GetImageUrl(PokemonName));
					summary.SelectSingleNode(".//img[@alt='Galarian Stunfisk']").SetAttributeValue("src", AFD.GetImageUrl("Galarian Stunfisk"));
					break;

				default:
					if (AFD.HasAFD(PokemonName))
						imgProfile.SetAttributeValue("src", AFD.GetImageUrl(PokemonName));
					break;
			}
		}
	}
}
