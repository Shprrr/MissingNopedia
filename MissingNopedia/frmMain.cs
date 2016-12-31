using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MissingNopedia
{
	public partial class frmMain : Form
	{
		private const string WikiPokemonSuffix = "_(Pok%C3%A9mon)";
		private const string WikiMoveSuffix = "_(move)";
		private const string WikiAbilitySuffix = "_(Ability)";
		private string _DefaultText;
		private HttpClient client = new HttpClient();
		private Stack<Uri> history = new Stack<Uri>();

		private AdvancedSearch.Pokemon[] pokemons;

		public frmMain()
		{
			// Desactivate IriParsing to parse quotes "'".
			typeof(Uri).GetField("s_IriParsing", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static).SetValue(null, false);
			InitializeComponent();
			_DefaultText = Text;
			Text += " Loading...";
			tabControlEx_Selected(tabControlEx, new TabControlEventArgs(tabControlEx.SelectedTab, tabControlEx.SelectedIndex, TabControlAction.Selected));
		}

		private async void frmMain_Load(object sender, EventArgs e)
		{
			var listPokemon = GetListPokemon();
			var listPokemonDB = GetListPokemonDB();
			var listMove = GetListMove();
			var listAbility = GetListAbility();

			cboPokemon.Items.Clear();
			if ((await listPokemon) != null || (await listPokemonDB) != null)
			{
				var listPokemonNames = (await listPokemon);
				var listPokemonDBNames = (await listPokemonDB);
				if (listPokemonNames == null || (listPokemonDBNames != null && listPokemonNames.Length < listPokemonDBNames.Length))
					listPokemonNames = listPokemonDBNames;

				cboPokemon.Items.AddRange(listPokemonNames);
			}

			cboMove.Items.Clear();
			if ((await listMove) != null)
				cboMove.Items.AddRange((await listMove));

			cboAbility.Items.Clear();
			if ((await listAbility) != null)
				cboAbility.Items.AddRange((await listAbility));

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

			if (history.Count == 0 || history.Peek() != e.Url)
				history.Push(e.Url);
			btnBackPokemon.Enabled = history.Count > 1;
			btnBackMove.Enabled = history.Count > 1;
			btnBackAbility.Enabled = history.Count > 1;

			var i = e.Url.Segments.Last().LastIndexOf(WikiPokemonSuffix);
			if (i != -1)
				await ShowInfoPokemon(Uri.UnescapeDataString(e.Url.Segments.Last().Remove(i)));

			i = e.Url.Segments.Last().LastIndexOf(WikiMoveSuffix);
			if (i != -1)
				await ShowInfoMove(Uri.UnescapeDataString(e.Url.Segments.Last().Remove(i)));

			i = e.Url.Segments.Last().LastIndexOf(WikiAbilitySuffix);
			if (i != -1)
				await ShowInfoAbility(Uri.UnescapeDataString(e.Url.Segments.Last().Remove(i)));
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
			string content = "";
			HttpResponseMessage response;
			try
			{
				response = await client.GetAsync("http://www.dragonflycave.com/resources/pokemon-list-generator?format=%25%5Bname%5D%25&linebreaks=1");
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
			content = await response.Content.ReadAsStringAsync();

			var doc = DocumentHtml.GetHtmlDocument(content);
			return doc.DocumentNode.SelectSingleNode("//textarea").InnerText.Split('\n');
		}

		private async Task<string[]> GetListPokemonDB()
		{
			string content = "";
			HttpResponseMessage response;
			try
			{
				response = await client.GetAsync("http://pokemondb.net/pokedex/all");
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
			content = await response.Content.ReadAsStringAsync();

			var doc = DocumentHtml.GetHtmlDocument(content);
			return doc.DocumentNode.SelectNodes("//*[@class='ent-name']").Select(n => n.InnerText).Distinct().ToArray();
		}

		private async Task<string[]> GetListMove()
		{
			string content = "";
			HttpResponseMessage response;
			try
			{
				response = await client.GetAsync("http://pokemondb.net/move/all");
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
			content = await response.Content.ReadAsStringAsync();

			var doc = DocumentHtml.GetHtmlDocument(content);
			return doc.DocumentNode.SelectNodes("//*[@class='ent-name']").Select(n => n.InnerText).ToArray();
		}

		private async Task<string[]> GetListAbility()
		{
			string content = "";
			HttpResponseMessage response;
			try
			{
				response = await client.GetAsync("http://pokemondb.net/ability");
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
			content = await response.Content.ReadAsStringAsync();

			var doc = DocumentHtml.GetHtmlDocument(content);
			return doc.DocumentNode.SelectNodes("//*[@class='ent-name']").Select(n => n.InnerText).ToArray();
		}

		private void ShowNewPage(string newDoc)
		{
			webBrowser.DocumentText = newDoc;
		}

		private async Task ShowInfoPokemon(string pokemonName)
		{
			if (string.IsNullOrWhiteSpace(pokemonName))
			{
				ShowNewPage("");
				return;
			}

			HttpResponseMessage response;
			try
			{
				response = await client.GetAsync("http://bulbapedia.bulbagarden.net/wiki/" + Uri.EscapeDataString(pokemonName.Replace(' ', '_')) + WikiPokemonSuffix);
			}
			catch (HttpRequestException ex)
			{
				MessageBox.Show("Error getting informations : " + ex.Message);
				return;
			}

			if (!response.IsSuccessStatusCode)
			{
				MessageBox.Show("Error getting informations : " + response.StatusCode);
				return;
			}

			var content = await response.Content.ReadAsStringAsync();
			var doc = new PokemonHtml(content);

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

			HttpResponseMessage response;
			try
			{
				response = await client.GetAsync("http://bulbapedia.bulbagarden.net/wiki/" + Uri.EscapeDataString(moveName.Replace(' ', '_')) + WikiMoveSuffix);
			}
			catch (HttpRequestException ex)
			{
				MessageBox.Show("Error getting informations : " + ex.Message);
				return;
			}

			if (!response.IsSuccessStatusCode)
			{
				MessageBox.Show("Error getting informations : " + response.StatusCode);
				return;
			}

			var content = await response.Content.ReadAsStringAsync();
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

			HttpResponseMessage response;
			try
			{
				response = await client.GetAsync("http://bulbapedia.bulbagarden.net/wiki/" + Uri.EscapeDataString(abilityName.Replace(' ', '_')) + WikiAbilitySuffix);
			}
			catch (HttpRequestException ex)
			{
				MessageBox.Show("Error getting informations : " + ex.Message);
				return;
			}

			if (!response.IsSuccessStatusCode)
			{
				MessageBox.Show("Error getting informations : " + response.StatusCode);
				return;
			}

			var content = await response.Content.ReadAsStringAsync();
			var doc = new AbilityHtml(content);

			ShowNewPage(doc.BuildNewPage());
		}

		private void btnAdvancedSearch_Click(object sender, EventArgs e)
		{
			dgvResult.Rows.Clear();
			if (pokemons != null)
				dgvResult.Rows.AddRange(pokemons.Select(p => p.ConvertRow()).ToArray());
		}
	}
}
