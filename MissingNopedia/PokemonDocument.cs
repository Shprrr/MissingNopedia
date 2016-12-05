using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace MissingNopedia
{
	[Obsolete]
	public class PokemonDocument
	{
		private const string AttributeClass = "className";
		private const string FirstHeader = "firstHeading";
		private const string ContentText = "mw-content-text";
		private const string TableOfContents = "toc";
		private const string Generation = "Generation_VI";
		private const string Stats = "Stats";
		private const string PokeathlonStats = "Pok.C3.A9athlon_stats";
		private const string TypeEffectiveness = "Type_effectiveness";
		private const string Learnset = "Learnset";

		private HtmlDocument doc;

		public PokemonDocument(string html)
		{
			doc = GetHtmlDocument(html);
		}

		public string BuildNewPage()
		{
			// Build a new page to reorder informations.
			string newDoc = "<!DOCTYPE html><html lang=\"en\" dir=\"ltr\" class=\"client-js desktop landscape\">";

			var element = doc.GetElementsByTagName("title")[0];
			newDoc += "<head><meta http-equiv=\"X-UA-Compatible\" content=\"IE=Edge\" />" + element.OuterHtml
				+ "<link rel=\"stylesheet\" href=\"http://bulbapedia.bulbagarden.net/w/load.php?debug=false&lang=en&modules=mediawiki.legacy.commonPrint%2Cshared%7Cmediawiki.sectionAnchor%7Cmediawiki.skinning.content.externallinks%7Cmediawiki.skinning.interface%7Cskins.monobook.styles&only=styles&skin=monobook\">"
				+ "<link rel=\"stylesheet\" href=\"http://bulbapedia.bulbagarden.net/w/load.php?debug=false&lang=en&modules=site&only=styles&skin=monobook\">"
				//+ "<script>alert(\"compatMode: \" + document.compatMode + \", documentMode: \" + document.documentMode)</script>"
				+ "</head>"
				+ "<body class=\"" + doc.Body.GetAttribute(AttributeClass) + "\"><div id=\"content\" class=\"mw-body\" role=\"main\" style=\"margin: 0\">";

			element = doc.GetElementById(FirstHeader);
			newDoc += element.OuterHtml;

			element = doc.GetElementById(ContentText).FirstChild;
			// Don't want the first child until the summary.
			while ((element = element.NextSibling) != null && element.GetAttribute(AttributeClass) != "roundy")
				continue;

			// Changing the summary table.
			if (doc.Title.StartsWith("Scolipede"))
				element.FirstChild.Children[0].GetElementsByTagName("img")[0].SetAttribute("src", "http://24.media.tumblr.com/tumblr_liimqxDoT51qekruyo1_500.png");
			element.FirstChild.Children[element.FirstChild.Children.Count - 6].Style = "display: none";
			element.FirstChild.Children[element.FirstChild.Children.Count - 5].Style = "display: none";
			element.FirstChild.Children[element.FirstChild.Children.Count - 3].Style = "display: none";
			element.FirstChild.Children[element.FirstChild.Children.Count - 2].Style = "display: none";
			element.FirstChild.Children[element.FirstChild.Children.Count - 1].Style = "display: none";

			do
			{
				if (element.Id == TableOfContents)
					break;

				newDoc += element.OuterHtml;
			} while ((element = element.NextSibling) != null);

			newDoc += AddSection(Stats);
			newDoc += AddSection(TypeEffectiveness);
			newDoc += AddSection(Learnset);

			newDoc += "</div></body></html>";
			return newDoc;
		}

		public string AddSection(string sectonName)
		{
			string html = "";
			HtmlElement element = doc.GetElementById(sectonName);
			if (element != null)
			{
				element = element.Parent;
				string tag = element.TagName;
				do
				{
					//if(element
					html += element.OuterHtml;
				} while ((element = element.NextSibling) != null && element.TagName != tag);
			}

			return html;
		}

		public static HtmlDocument GetHtmlDocument(string html)
		{
			using (WebBrowser browser = new WebBrowser())
			{
				browser.ScriptErrorsSuppressed = true;
				browser.DocumentText = html;
				browser.Document.OpenNew(true);
				browser.Document.Write(html);
				return browser.Document;
			}
		}
	}
}
