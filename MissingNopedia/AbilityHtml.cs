using System;
using System.Threading.Tasks;

namespace MissingNopedia
{
	public class AbilityHtml : DocumentHtml
	{
		public const string WikiAbilitySuffix = "_(Ability)";

		private const string Effect = "Effect";
		private const string PokemonWith = "Pok.C3.A9mon_with_";

		public string AbilityName { get; }

		public AbilityHtml(string abilityName) 
		{
			AbilityName = abilityName;
		}

		public override async Task LoadAsync()
		{
			if (string.IsNullOrWhiteSpace(AbilityName))
				return;

			var content = await GetPageContentAsync($"https://bulbapedia.bulbagarden.net/wiki/{Uri.EscapeDataString(AbilityName.Replace(' ', '_'))}{WikiAbilitySuffix}");
			if (content is null) return;

			doc = GetHtmlDocument(content);
		}

		public override string BuildNewPage()
		{
			if (doc is null) return "";
			var newDoc = ConstructBulbapediaPage();
			var body = newDoc.DocumentNode.LastChild.LastChild.FirstChild;

			var content = doc.GetElementbyId(ContentText);
			var summary = content.SelectSingleNode(".//table");
			body.AppendChild(summary.Clone());

			var node = summary.NextSibling;
			do
			{
				if (node.Id == TableOfContents)
					break;

				body.AppendChild(node.Clone());
			} while ((node = node.NextSibling) != null);

			var section = AddSection(Effect);
			if (section != null)
				body.AppendChildren(section);

			var title = doc.DocumentNode.SelectSingleNode("//title").InnerHtml;
			var abilityName = title.Remove(title.IndexOf('(') - 1).Replace(' ', '_');

			section = AddSection(PokemonWith + abilityName);
			if (section != null)
				body.AppendChildren(section);

			return newDoc.DocumentNode.OuterHtml;
		}
	}
}
