using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MissingNopedia
{
	public partial class frmMain : Form
	{
		private string _DefaultText;
		private HttpClient client = new HttpClient();

		public frmMain()
		{
			// Desactivate IriParsing to parse quotes "'".
			typeof(Uri).GetField("s_IriParsing", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static).SetValue(null, false);
			InitializeComponent();
			_DefaultText = Text;
			Text += " Loading...";
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
		}

		private async void btnSearchPokemon_Click(object sender, EventArgs e)
		{
			await ShowInfoPokemon(cboPokemon.Text);
		}

		private async void btnSearchMove_Click(object sender, EventArgs e)
		{
			await ShowInfoMove(cboMove.Text);
		}

		private async void btnSearchAbility_Click(object sender, EventArgs e)
		{
			await ShowInfoAbility(cboAbility.Text);
		}

		private async void webBrowser_Navigating(object sender, WebBrowserNavigatingEventArgs e)
		{
			if (e.Url.AbsolutePath != "blank")
				e.Cancel = true;

			var i = e.Url.Segments.Last().LastIndexOf("_(Pok%C3%A9mon)");
			if (i != -1)
				await ShowInfoPokemon(Uri.UnescapeDataString(e.Url.Segments.Last().Remove(i)));

			i = e.Url.Segments.Last().LastIndexOf("_(move)");
			if (i != -1)
				await ShowInfoMove(Uri.UnescapeDataString(e.Url.Segments.Last().Remove(i)));

			i = e.Url.Segments.Last().LastIndexOf("_(Ability)");
			if (i != -1)
				await ShowInfoAbility(Uri.UnescapeDataString(e.Url.Segments.Last().Remove(i)));
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
				response = await client.GetAsync("http://bulbapedia.bulbagarden.net/wiki/" + Uri.EscapeDataString(pokemonName.Replace(' ', '_')) + "_(Pok%C3%A9mon)");
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
				response = await client.GetAsync("http://bulbapedia.bulbagarden.net/wiki/" + Uri.EscapeDataString(moveName.Replace(' ', '_')) + "_(move)");
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
				response = await client.GetAsync("http://bulbapedia.bulbagarden.net/wiki/" + Uri.EscapeDataString(abilityName.Replace(' ', '_')) + "_(Ability)");
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
	}
}
