using System;
using System.Collections.Generic;
using System.Linq;
using HtmlAgilityPack;

namespace MissingNopedia
{
	public abstract class DocumentHtml
	{
		protected const string FirstHeader = "firstHeading";
		protected const string SiteSource = "siteSub";
		protected const string ContentText = "mw-content-text";
		protected const string TableOfContents = "toc";

		protected HtmlDocument doc;

		public DocumentHtml(string html)
		{
			doc = GetHtmlDocument(html);

			// Add scheme to url in src attributes.
			var nodesSrc = doc.DocumentNode.SelectNodes("//*[starts-with(@src, '//')]");
			foreach (var node in nodesSrc)
			{
				node.SetAttributeValue("src", "https:" + node.GetAttributeValue("src", ""));
			}
		}

		protected HtmlDocument ConstructBulbapediaPage()
		{
			var newDoc = new HtmlDocument();
			newDoc.DocumentNode.InnerHtml = "<!DOCTYPE html><html lang=\"en\" dir=\"ltr\" class=\"client-js desktop landscape\"><head></head><body></body></html>";

			var head = newDoc.DocumentNode.LastChild.FirstChild;
			head.AppendChild(HtmlNode.CreateNode("<meta http-equiv=\"X-UA-Compatible\" content=\"IE=Edge\" />"));
			head.AppendChild(HtmlNode.CreateNode("<meta charset=\"UTF-8\">"));
			var title = doc.DocumentNode.SelectSingleNode("//title").InnerHtml;
			head.AppendChild(HtmlNode.CreateNode("<title>" + title + "</title>"));
			head.AppendChild(HtmlNode.CreateNode("<link rel=\"stylesheet\" href=\"http://bulbapedia.bulbagarden.net/w/load.php?debug=false&lang=en&modules=mediawiki.legacy.commonPrint%2Cshared%7Cmediawiki.sectionAnchor%7Cmediawiki.skinning.content.externallinks%7Cmediawiki.skinning.interface%7Cskins.monobook.styles&only=styles&skin=monobook\">"));
			head.AppendChild(HtmlNode.CreateNode("<link rel=\"stylesheet\" href=\"http://bulbapedia.bulbagarden.net/w/load.php?debug=false&lang=en&modules=site&only=styles&skin=monobook\">"));

			var body = newDoc.DocumentNode.LastChild.LastChild;
			body.SetAttributeValue("class", doc.DocumentNode.SelectSingleNode("//body").GetAttributeValue("class", ""));
			body.AppendChild(HtmlNode.CreateNode("<div id=\"content\" class=\"mw-body\" role=\"main\" style=\"margin: 0\"></div>"));
			body.FirstChild.AppendChild(doc.GetElementbyId(FirstHeader));
			body.FirstChild.AppendChild(doc.GetElementbyId(SiteSource));

			return newDoc;
		}

		public abstract string BuildNewPage();

		public HtmlNodeCollection AddSection(string sectionName)
		{
			var node = doc.GetElementbyId(sectionName);
			if (node == null) return null;

			var nodes = new HtmlNodeCollection(null);

			node = node.ParentNode;
			string tag = node.Name;
			int headerLevelTemp = 0;
			int headerLevel = 0;
			int.TryParse(tag.TrimStart('h'), out headerLevel);
			do
			{
				// Cancel width 100%.
				if (node.Name == "table" && node.GetAttributeValue("width", "") != "")
					node.SetAttributeValue("width", "");

				// Cancel float left.
				if (node.Name == "table" && node.GetAttributeValue("align", "") == "left")
					node.SetAttributeValue("align", "");

				// Cancel clear both.
				if (node.DescendantsAndSelf().Any(n => n.GetAttributeValue("clear", "") == "all"))
					continue;

				nodes.Append(node.Clone());
				// Loop as long as the next isn't the same type of tag or the next isn't a higher level of header.
			} while ((node = node.NextSibling) != null && (node.Name != tag && (!int.TryParse(node.Name.TrimStart('h'), out headerLevelTemp) || headerLevel < headerLevelTemp)));

			return nodes;
		}

		public HtmlNodeCollection RemoveSection(HtmlNodeCollection nodes, string sectionName)
		{
			var node = doc.GetElementbyId(sectionName);
			if (node == null) return nodes;

			node = node.ParentNode;
			if (nodes.GetNodeIndex(node) == -1) return nodes;

			// Remember indexes to remove because it can't loop with NextSibling while it removes.
			var indexesToRemove = new List<int>();
			string tag = node.Name;
			do
			{
				indexesToRemove.Insert(0, nodes[node]);
			} while ((node = node.NextSibling) != null && node.Name != tag);

			foreach (var index in indexesToRemove)
			{
				nodes.RemoveAt(index);
			}

			return nodes;
		}

		public static HtmlDocument GetHtmlDocument(string html)
		{
			var doc = new HtmlDocument();
			doc.LoadHtml(html);
			return doc;
		}
	}
}
