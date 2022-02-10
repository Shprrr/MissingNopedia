using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;
using MissingNopedia.AdvancedSearch.Criteria;

namespace MissingNopedia
{
	public partial class frmMain : Form
	{
		private const string WikiPokemonSuffix = "_(Pok%C3%A9mon)";
		private const string WikiMoveSuffix = "_(move)";
		private const string WikiAbilitySuffix = "_(Ability)";
		private readonly string _DefaultText;
		private readonly HttpClient client = new();
		private readonly Stack<Uri> history = new();

		private AdvancedSearch.Pokemon[] pokemons;

		public frmMain()
		{
			InitializeComponent();

			// Remove auto horizontal scroll.
			tlpCriteria.AutoScroll = false;
			tlpCriteria.HorizontalScroll.Enabled = false;
			tlpCriteria.HorizontalScroll.Visible = false;
			tlpCriteria.HorizontalScroll.Maximum = 0;
			tlpCriteria.AutoScroll = true;

			lblFound.Text = "";

			// Loading text.
			_DefaultText = Text;
			Text += " Loading...";

			tabControlEx_Selected(tabControlEx, new TabControlEventArgs(tabControlEx.SelectedTab, tabControlEx.SelectedIndex, TabControlAction.Selected));
		}

		private async void frmMain_Load(object sender, EventArgs e)
		{
			var taskListPokemon = GetListPokemon();
			var taskListPokemonGen8 = GetListPokemonGen8();
			var taskListMove = GetListMove();
			var taskListMoveGen8 = GetListMoveGen8();
			var taskListAbility = GetListAbility();

			cboPokemon.Items.Clear();
			cboPokemon.Items.AddRange((await taskListPokemon).Union(await taskListPokemonGen8).ToArray());

			cboMove.Items.Clear();
			cboMove.Items.AddRange((await taskListMove).Union(await taskListMoveGen8).ToArray());

			cboAbility.Items.Clear();
			cboAbility.Items.AddRange(await taskListAbility);

#if DEBUG
#pragma warning disable CS4014 // Dans la mesure où cet appel n'est pas attendu, l'exécution de la méthode actuelle continue avant la fin de l'appel
			AdvancedSearch.Pokemon[] pokemonsWeb = null;
			AdvancedSearch.Pokemon.GetListPokemonWeb().ContinueWith(p =>
			{
				pokemonsWeb = p.Result;
			});
#pragma warning restore CS4014 // Dans la mesure où cet appel n'est pas attendu, l'exécution de la méthode actuelle continue avant la fin de l'appel
#endif

			pokemons = AdvancedSearch.Pokemon.GetListPokemonDB();

			Text = _DefaultText;
		}

		private async void tabControlEx_Selected(object sender, TabControlEventArgs e)
		{
			if (e.TabPage.Name == tabPagePokemon.Name)
				await ShowInfoPokemon(cboPokemon?.Text);

			if (e.TabPage.Name == tabPageType.Name)
				ShowTypeEffectiveness();

			if (e.TabPage.Name == tabPageMove.Name)
				await ShowInfoMove(cboMove?.Text);

			if (e.TabPage.Name == tabPageAbility.Name)
				await ShowInfoAbility(cboAbility?.Text);

			webBrowser.Visible = e.TabPage.Name != tabPageSearch.Name;
			splitSearch.Visible = e.TabPage.Name == tabPageSearch.Name;
		}

		private void btnSearchPokemon_Click(object sender, EventArgs e)
		{
			webBrowser.Navigate("about:/wiki/" + cboPokemon.Text + WikiPokemonSuffix);
		}

		private void cboPokemon_KeyUp(object sender, KeyEventArgs e)
		{
			if (e.KeyData == Keys.Enter && cboPokemon.SelectedIndex < 0)
			{
				btnSearchPokemon_Click(sender, e);
				e.SuppressKeyPress = true;
			}
		}

		private void btnSearchMove_Click(object sender, EventArgs e)
		{
			webBrowser.Navigate("about:/wiki/" + cboMove.Text + WikiMoveSuffix);
		}

		private void cboMove_KeyUp(object sender, KeyEventArgs e)
		{
			if (e.KeyData == Keys.Enter && cboMove.SelectedIndex < 0)
			{
				btnSearchMove_Click(sender, e);
				e.SuppressKeyPress = true;
			}
		}

		private void btnSearchAbility_Click(object sender, EventArgs e)
		{
			webBrowser.Navigate("about:/wiki/" + cboAbility.Text + WikiAbilitySuffix);
		}

		private void cboAbility_KeyUp(object sender, KeyEventArgs e)
		{
			if (e.KeyData == Keys.Enter && cboAbility.SelectedIndex < 0)
			{
				btnSearchAbility_Click(sender, e);
				e.SuppressKeyPress = true;
			}
		}

		private async void webBrowser_Navigating(object sender, WebBrowserNavigatingEventArgs e)
		{
			if (e.Url.AbsolutePath != "blank")
				e.Cancel = true;

			if (e.Url.AbsoluteUri == "about:blank") return;

			var i = e.Url.Segments[^1].LastIndexOf(WikiPokemonSuffix);
			bool pageProcessed = false;
			if (i != -1)
			{
				pageProcessed = true;
				await ShowInfoPokemon(Uri.UnescapeDataString(e.Url.Segments[^1].Remove(i)));
			}

			i = e.Url.Segments[^2].LastIndexOf(WikiPokemonSuffix);
			if (i != -1)
			{
				pageProcessed = true;
				await ShowInfoPokemon(Uri.UnescapeDataString(e.Url.Segments[^2].Remove(i)), e.Url.Segments[^1]);
			}

			i = e.Url.Segments[^1].LastIndexOf(WikiMoveSuffix);
			if (i != -1)
			{
				pageProcessed = true;
				await ShowInfoMove(Uri.UnescapeDataString(e.Url.Segments[^1].Remove(i)));
			}

			i = e.Url.Segments[^1].LastIndexOf(WikiAbilitySuffix);
			if (i != -1)
			{
				pageProcessed = true;
				await ShowInfoAbility(Uri.UnescapeDataString(e.Url.Segments[^1].Remove(i)));
			}

			if (pageProcessed && (history.Count == 0 || history.Peek() != e.Url))
				history.Push(e.Url);
			btnBackPokemon.Enabled = history.Count > 1;
			btnBackMove.Enabled = history.Count > 1;
			btnBackAbility.Enabled = history.Count > 1;
		}

		private void btnBack_Click(object sender, EventArgs e)
		{
			history.Pop();
			webBrowser_Navigating(sender, new WebBrowserNavigatingEventArgs(history.Peek(), ""));
			btnBackPokemon.Enabled = history.Count > 1;
			btnBackMove.Enabled = history.Count > 1;
			btnBackAbility.Enabled = history.Count > 1;
		}

		private async Task<string[]> GetListPokemon()
		{
			var content = await GetPageContentAsync("http://pokemondb.net/pokedex/all");
			if (content is null) return Array.Empty<string>();

			var doc = DocumentHtml.GetHtmlDocument(content);
			return doc.DocumentNode.SelectNodes("//*[@class='ent-name']").Select(n => n.InnerText).Distinct().ToArray();
		}

		private async Task<string[]> GetListPokemonGen8()
		{
			var content = await GetPageContentAsync("https://www.serebii.net/pokemon/gen8pokemon.shtml");
			if (content is null) return Array.Empty<string>();

			var doc = DocumentHtml.GetHtmlDocument(content);
			return doc.DocumentNode.SelectNodes(".//*[@class='dextable']//*[@class='fooinfo'][position()=3]/a").Select(n => n.InnerText).Distinct().ToArray();
		}

		private async Task<string[]> GetListMove()
		{
			string content = await GetPageContentAsync("http://pokemondb.net/move/all");
			if (content is null) return Array.Empty<string>();

			var doc = DocumentHtml.GetHtmlDocument(content);
			return doc.DocumentNode.SelectNodes("//*[@class='ent-name']").Select(n => n.InnerText).ToArray();
		}

		private async Task<string[]> GetListMoveGen8()
		{
			string content = await GetPageContentAsync("https://www.serebii.net/attackdex-swsh/");
			if (content is null) return Array.Empty<string>();

			var doc = DocumentHtml.GetHtmlDocument(content);
			return doc.DocumentNode.SelectNodes(".//select[@name='SelectURL']//option[@value!='']").Select(n => n.InnerText).ToArray();
		}

		private async Task<string[]> GetListAbility()
		{
			string content = await GetPageContentAsync("http://pokemondb.net/ability");
			if (content is null) return Array.Empty<string>();

			var doc = DocumentHtml.GetHtmlDocument(content);
			return doc.DocumentNode.SelectNodes("//*[@class='ent-name']").Select(n => n.InnerText).ToArray();
		}

		private void ShowNewPage(string newDoc)
		{
			webBrowser.DocumentText = newDoc;
		}

		private async Task<string> GetPageContentAsync(string url)
		{
			HttpResponseMessage response;
			try
			{
				response = await client.GetAsync(url);
			}
			catch (HttpRequestException ex)
			{
				MessageBox.Show("Error getting informations : " + ex.Message);
				return null;
			}

			if (!response.IsSuccessStatusCode)
			{
				MessageBox.Show("Error getting informations : " + response.StatusCode);
				return null;
			}
			return await response.Content.ReadAsStringAsync();
		}

		private async Task ShowInfoPokemon(string pokemonName, string generationLearnset = null)
		{
			if (string.IsNullOrWhiteSpace(pokemonName))
			{
				ShowNewPage("");
				return;
			}

			var content = await GetPageContentAsync($"http://bulbapedia.bulbagarden.net/wiki/{Uri.EscapeDataString(pokemonName.Replace(' ', '_'))}{WikiPokemonSuffix}");
			if (content is null) return;

			string contentGenerationLearnset = null;
			if (!string.IsNullOrEmpty(generationLearnset))
			{
				contentGenerationLearnset = await GetPageContentAsync($"http://bulbapedia.bulbagarden.net/wiki/{Uri.EscapeDataString(pokemonName.Replace(' ', '_'))}{WikiPokemonSuffix}/{generationLearnset}");
				if (contentGenerationLearnset is null) return;
			}

			var doc = new PokemonHtml(content, contentGenerationLearnset);

			ShowNewPage(doc.BuildNewPage());
		}

		private void ShowTypeEffectiveness()
		{
			ShowNewPage("<!DOCTYPE html><html style=\"height: 100%;\">"
				+ "<head><meta http-equiv=\"X-UA-Compatible\" content=\"IE=Edge\" /><meta charset=\"UTF-8\"><title>Type Effectiveness Chart</title></head>"
				+ "<body style=\"background-image: url(http://i.imgur.com/s0YOJfD.png); background-repeat: no-repeat; background-size: contain; background-color: #e7e7e7;\" /></body>"
				+ "</html>");
		}

		private async Task ShowInfoMove(string moveName)
		{
			if (string.IsNullOrWhiteSpace(moveName))
			{
				ShowNewPage("");
				return;
			}

			var content = await GetPageContentAsync($"http://bulbapedia.bulbagarden.net/wiki/{Uri.EscapeDataString(moveName.Replace(' ', '_'))}{WikiMoveSuffix}");
			if (content is null) return;

			var doc = new MoveHtml(content);

			ShowNewPage(doc.BuildNewPage());
		}

		private async Task ShowInfoAbility(string abilityName)
		{
			if (string.IsNullOrWhiteSpace(abilityName))
			{
				ShowNewPage("");
				return;
			}

			var content = await GetPageContentAsync($"http://bulbapedia.bulbagarden.net/wiki/{Uri.EscapeDataString(abilityName.Replace(' ', '_'))}{WikiAbilitySuffix}");
			if (content is null) return;

			var doc = new AbilityHtml(content);

			ShowNewPage(doc.BuildNewPage());
		}

		private void btnAddCriterion_Click(object sender, EventArgs e)
		{
			flpCriteria.Controls.Add(new Criterion());
		}

		private void btnRemoveCriterion_Click(object sender, EventArgs e)
		{
			if (flpCriteria.Controls.Count > 0)
				flpCriteria.Controls.RemoveAt(flpCriteria.Controls.Count - 1);
		}

		private void btnAdvancedSearch_Click(object sender, EventArgs e)
		{
			dgvResult.Rows.Clear();
			if (pokemons != null)
			{
				var results = pokemons;
				foreach (var criterion in flpCriteria.Controls)
				{
					results = ((Criterion)criterion).ApplyCriterion(results);
				}
				dgvResult.Rows.AddRange(results.Select(p => p.ConvertRow()).ToArray());
				lblFound.Text = results.Length + " found";
			}
		}
	}
}
