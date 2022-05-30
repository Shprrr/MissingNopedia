namespace MissingNopedia
{
	public class ItemHtml : DocumentHtml
	{
		public const string ItemSuffix = "_(Item)";

		public ItemHtml(string html) : base(html)
		{
		}

		public override string BuildNewPage()
		{
			var newDoc = ConstructBulbapediaPage();
			var body = newDoc.DocumentNode.LastChild.LastChild.FirstChild;

			var content = doc.GetElementbyId(ContentText);

			body.AppendChild(content.Clone());

			return newDoc.DocumentNode.OuterHtml;
		}
	}
}
