namespace MissingNopedia
{
	public class ItemHtml : DocumentHtml
	{
		public const string ItemSuffix = "_(item)";

		public ItemHtml(string html) : base(html)
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
					continue;

				body.AppendChild(node.Clone());
			} while ((node = node.NextSibling) != null);

			return newDoc.DocumentNode.OuterHtml;
		}
	}
}
