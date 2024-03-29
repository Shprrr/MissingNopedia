﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;
using MissingNopedia.AdvancedSearch.Criteria;

namespace MissingNopedia
{
	public partial class frmMain : Form
	{
		private readonly string _DefaultText;
		private readonly HttpClient client = new();
		private readonly Stack<Uri> history = new();

		private Options options = new();
		private readonly AdvancedSearch.AdvancedSearch advancedSearch = new();

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
			var taskListMove = GetListMove();
			var taskListMoveGen8 = GetListMoveGen8();
			var taskListAbility = GetListAbility();

			cboPokemon.Items.Clear();
			cboPokemon.Items.AddRange(await taskListPokemon);

			cboMove.Items.Clear();
			cboMove.Items.AddRange((await taskListMove).Union(await taskListMoveGen8).ToArray());

			cboAbility.Items.Clear();
			cboAbility.Items.AddRange(await taskListAbility);

			Text = _DefaultText;
		}

		private async void tabControlEx_Selected(object sender, TabControlEventArgs e)
		{
			if (e.TabPage.Name == tabPagePokemon.Name)
				await ShowInfoPokemon(cboPokemon?.Text, GetGenerationLearnset());

			if (e.TabPage.Name == tabPageType.Name)
				ShowTypeEffectiveness();

			if (e.TabPage.Name == tabPageMove.Name)
				await ShowInfoMove(cboMove?.Text);

			if (e.TabPage.Name == tabPageAbility.Name)
				await ShowInfoAbility(cboAbility?.Text);

			webBrowser.Visible = e.TabPage.Name != tabPageSearch.Name;
			splitSearch.Visible = e.TabPage.Name == tabPageSearch.Name;
		}

		private string GetGenerationLearnset()
		{
			return options.DefaultGeneration switch
			{
				OptionGenerations.Generation1 => "Generation_I_learnset",
				OptionGenerations.Generation2 => "Generation_II_learnset",
				OptionGenerations.Generation3 => "Generation_III_learnset",
				OptionGenerations.Generation4 => "Generation_IV_learnset",
				OptionGenerations.Generation5 => "Generation_V_learnset",
				OptionGenerations.Generation6 => "Generation_VI_learnset",
				OptionGenerations.Generation7 => "Generation_VII_learnset",
				_ => null
			};
		}

		private void btnSearchPokemon_Click(object sender, EventArgs e)
		{
			var url = $"about:/wiki/{cboPokemon.Text}{PokemonHtml.WikiPokemonSuffix}";
			var generationLearnset = GetGenerationLearnset();
			if (!string.IsNullOrEmpty(generationLearnset))
				url += "/" + generationLearnset;
			webBrowser.Navigate(url);
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
			webBrowser.Navigate("about:/wiki/" + cboMove.Text + MoveHtml.WikiMoveSuffix);
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
			webBrowser.Navigate("about:/wiki/" + cboAbility.Text + AbilityHtml.WikiAbilitySuffix);
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

			var i = e.Url.Segments[^1].LastIndexOf(PokemonHtml.WikiPokemonSuffix);
			bool pageProcessed = false;
			if (i != -1)
			{
				pageProcessed = true;
				await ShowInfoPokemon(Uri.UnescapeDataString(e.Url.Segments[^1].Remove(i)));
			}

			i = e.Url.Segments[^2].LastIndexOf(PokemonHtml.WikiPokemonSuffix);
			if (i != -1)
			{
				pageProcessed = true;
				await ShowInfoPokemon(Uri.UnescapeDataString(e.Url.Segments[^2].Remove(i)), e.Url.Segments[^1]);
			}

			i = e.Url.Segments[^1].LastIndexOf(MoveHtml.WikiMoveSuffix);
			if (i != -1)
			{
				pageProcessed = true;
				await ShowInfoMove(Uri.UnescapeDataString(e.Url.Segments[^1].Remove(i)));
			}

			i = e.Url.Segments[^1].LastIndexOf(AbilityHtml.WikiAbilitySuffix);
			if (i != -1)
			{
				pageProcessed = true;
				await ShowInfoAbility(Uri.UnescapeDataString(e.Url.Segments[^1].Remove(i)));
			}

			i = e.Url.Segments[^1].LastIndexOf(EggGroupHtml.WikiEggGroupSuffix);
			if (i != -1)
			{
				pageProcessed = true;
				await ShowInfoEggGroup(Uri.UnescapeDataString(e.Url.Segments[^1].Remove(i)));
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
			var content = await GetPageContentAsync("https://www.serebii.net/pokemon/nationalpokedex.shtml");
			if (content is null) return Array.Empty<string>();

			var doc = DocumentHtml.GetHtmlDocument(content);
			return doc.DocumentNode.SelectNodes(".//*[@class='dextable']//*[@class='fooinfo'][position()=3]/a").Select(n => WebUtility.HtmlDecode(n.InnerText)).Distinct().ToArray();
		}

		private async Task<string[]> GetListMove()
		{
			string content = await GetPageContentAsync("https://pokemondb.net/move/all");
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
			string content = await GetPageContentAsync("https://pokemondb.net/ability");
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

			var content = await GetPageContentAsync($"https://bulbapedia.bulbagarden.net/wiki/{Uri.EscapeDataString(pokemonName.Replace(' ', '_'))}{PokemonHtml.WikiPokemonSuffix}");
			if (content is null) return;

			string contentGenerationLearnset = null;
			if (!string.IsNullOrEmpty(generationLearnset))
			{
				contentGenerationLearnset = await GetPageContentAsync($"https://bulbapedia.bulbagarden.net/wiki/{Uri.EscapeDataString(pokemonName.Replace(' ', '_'))}{PokemonHtml.WikiPokemonSuffix}/{generationLearnset}");
				if (contentGenerationLearnset is null) return;
			}

			var doc = new PokemonHtml(pokemonName, content, contentGenerationLearnset, options.PokemonProfilePictures == OptionPokemonProfilePictures.Custom);

			ShowNewPage(doc.BuildNewPage());
		}

		private void ShowTypeEffectiveness()
		{
			ShowNewPage("<!DOCTYPE html><html style=\"height: 100%;\">"
				+ "<head><meta http-equiv=\"X-UA-Compatible\" content=\"IE=Edge\" /><meta charset=\"UTF-8\"><title>Type Effectiveness Chart</title></head>"
				+ "<body style=\"background-image: url(https://i.imgur.com/s0YOJfD.png); background-repeat: no-repeat; background-size: contain; background-color: #e7e7e7;\" /></body>"
				+ "</html>");
		}

		private async Task ShowInfoMove(string moveName)
		{
			if (string.IsNullOrWhiteSpace(moveName))
			{
				ShowNewPage("");
				return;
			}

			var content = await GetPageContentAsync($"https://bulbapedia.bulbagarden.net/wiki/{Uri.EscapeDataString(moveName.Replace(' ', '_'))}{MoveHtml.WikiMoveSuffix}");
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

			var content = await GetPageContentAsync($"https://bulbapedia.bulbagarden.net/wiki/{Uri.EscapeDataString(abilityName.Replace(' ', '_'))}{AbilityHtml.WikiAbilitySuffix}");
			if (content is null) return;

			var doc = new AbilityHtml(content);

			ShowNewPage(doc.BuildNewPage());
		}

		private async Task ShowInfoEggGroup(string eggGroupName)
		{
			if (string.IsNullOrWhiteSpace(eggGroupName))
			{
				ShowNewPage("");
				return;
			}

			var content = await GetPageContentAsync($"https://bulbapedia.bulbagarden.net/wiki/{Uri.EscapeDataString(eggGroupName.Replace(' ', '_'))}{EggGroupHtml.WikiEggGroupSuffix}");
			if (content is null) return;

			var doc = new EggGroupHtml(content);

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

		private void flpCriteria_DragEnter(object sender, DragEventArgs e)
		{
			if (e.Data.GetDataPresent(typeof(Criterion).FullName))
				e.Effect = DragDropEffects.Move;
			else
				e.Effect = DragDropEffects.None;
		}

		private void flpCriteria_DragDrop(object sender, DragEventArgs e)
		{
			if (e.Data.GetData(typeof(Criterion).FullName) is not Criterion criterion) return;

			var criteriaBounds = flpCriteria.Controls.OfType<Criterion>().Select((c, i) => (c.Bounds, Index: i));
			var clientPoint = flpCriteria.PointToClient(new Point(e.X, e.Y));
			var newIndex = criteriaBounds.OrderBy(b => clientPoint.Distance(b.Bounds)).First().Index;
			flpCriteria.Controls.SetChildIndex(criterion, newIndex);
		}

		delegate void AdvancedSearchDelegate();
		private void btnAdvancedSearch_Click(object sender, EventArgs e)
		{
			dgvResult.Rows.Clear();
			advancedSearch.RequestAsync(flpCriteria.Controls.OfType<Criterion>(), chkIncludeForms.Checked)
				.ContinueWith(t =>
					{
						if (t.IsFaulted)
						{
							MessageBox.Show("Error with the search");
							return;
						}

						var pokemons = t.Result;
						Invoke((AdvancedSearchDelegate)delegate
						{
							dgvResult.Rows.AddRange(pokemons.Select(p => p.ConvertRow()).ToArray());
							lblFound.Text = pokemons.Length + " found";
						});
					}, TaskScheduler.FromCurrentSynchronizationContext());
		}

		private void dgvResult_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
		{
			if (!OperatingSystem.IsWindows()) return;

			var grid = sender as DataGridView;
			var rowIdx = (e.RowIndex + 1).ToString();

			var centerFormat = new StringFormat()
			{
				// right alignment might actually make more sense for numbers
				Alignment = StringAlignment.Center,
				LineAlignment = StringAlignment.Center
			};

			var headerBounds = new Rectangle(e.RowBounds.Left, e.RowBounds.Top, grid.RowHeadersWidth, e.RowBounds.Height);
			e.Graphics.DrawString(rowIdx, grid.Font, SystemBrushes.ControlText, headerBounds, centerFormat);
		}

		private void btnOptions_Click(object sender, EventArgs e)
		{
			var form = new frmOptions(options);
			if (form.ShowDialog() != DialogResult.OK) return;

			options = form.Options;
		}
	}
}
