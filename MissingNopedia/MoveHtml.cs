using System;
using System.Linq;
using System.Threading.Tasks;

namespace MissingNopedia
{
	public class MoveHtml : DocumentHtml
	{
		public const string WikiMoveSuffix = "_(move)";

		private const string Effect = "Effect";
		private const string Description = "Description";
		private const string Learnset = "Learnset";
		private const string MoveVariations = "Move variations";
		private const string TM = "TM";

		public string MoveName { get; }

		public MoveHtml(string moveName)
		{
			MoveName = moveName;
		}

		public override async Task LoadAsync()
		{
			if (string.IsNullOrWhiteSpace(MoveName))
				return;

			var content = await GetPageContentAsync($"https://bulbapedia.bulbagarden.net/wiki/{Uri.EscapeDataString(MoveName.Replace(' ', '_'))}{WikiMoveSuffix}");
			if (content is null) return;

			doc = GetHtmlDocument(content);
		}

		public override string BuildNewPage()
		{
			if (doc is null) return "";
			var newDoc = ConstructBulbapediaPage();
			var body = newDoc.DocumentNode.LastChild.LastChild.FirstChild;

			var content = doc.GetElementbyId(ContentText);
			var summary = content.SelectSingleNode(".//table[contains(@style,'float')]");
			var rows = summary.SelectNodes(".//tr");
			rows[^1].SetAttributeValue("style", "display: none"); // External Links
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

			section = AddSection(Description);
			if (section != null)
				body.AppendChildren(section);

			section = AddSection(Learnset);
			if (section != null)
				body.AppendChildren(section);

			var variations = content.SelectNodes(".//*[@class='roundy']")?.LastOrDefault(n => n.Descendants().Any(n2 => n2.GetAttributeValue("title", "") == MoveVariations));
			if (variations != null)
				body.AppendChild(variations.Ancestors("table").First().Clone());

			var tms = content.SelectNodes(".//*[@class='toccolours']")?.Where(n => n.Descendants().Any(n2 => n2.GetAttributeValue("title", "") == TM));
			if (tms != null)
				foreach (var tm in tms)
				{
					body.AppendChild(tm);
				}

			return newDoc.DocumentNode.OuterHtml;
		}
	}
}
