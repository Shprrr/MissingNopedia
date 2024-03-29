﻿namespace MissingNopedia
{
	public class EggGroupHtml : DocumentHtml
	{
		public const string WikiEggGroupSuffix = "_(Egg_Group)";

		private const string Characteristics = "Characteristics";
		private const string Pokemon = "Pok.C3.A9mon";
		private const string Trivia = "Trivia";

		public EggGroupHtml(string html) : base(html)
		{
		}

		public override string BuildNewPage()
		{
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

			var section = AddSection(Characteristics);
			if (section != null)
				body.AppendChildren(section);

			section = AddSection(Pokemon);
			if (section != null)
				body.AppendChildren(section);

			section = AddSection(Trivia);
			if (section != null)
				body.AppendChildren(section);

			return newDoc.DocumentNode.OuterHtml;
		}
	}
}
