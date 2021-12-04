using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using System.Windows.Forms;

namespace MissingNopedia.AdvancedSearch
{
	public class Pokemon
	{
		private static readonly HttpClient client = new();

		public int Number { get; set; }
		public string Name { get; set; }
		public string Form { get; set; }
		public Type Type1 { get; set; }
		public Type? Type2 { get; set; }

		public int BaseHP { get; set; }
		public int BaseAttack { get; set; }
		public int BaseDefense { get; set; }
		public int BaseSpAttack { get; set; }
		public int BaseSpDefense { get; set; }
		public int BaseSpeed { get; set; }

		public int TotalStat { get { return BaseHP + BaseAttack + BaseDefense + BaseSpAttack + BaseSpDefense + BaseSpeed; } }
		public int PhysicalSweeper { get { return BaseAttack + BaseSpeed; } }
		public int SpecialSweeper { get { return BaseSpAttack + BaseSpeed; } }
		public int Wall { get { return BaseHP + BaseDefense + BaseSpDefense; } }
		public int PhysicalTank { get { return BaseAttack + BaseDefense; } }
		public int SpecialTank { get { return BaseSpAttack + BaseSpDefense; } }

		public string Ability1 { get; set; }
		public string Ability2 { get; set; }
		public string HiddenAbility { get; set; }

		public Pokemon(int number, string name)
		{
			Number = number;
			Name = name;
		}

		public override string ToString()
		{
			var s = Name;
			if (!string.IsNullOrEmpty(Form))
				s += " (" + Form + ")";
			return s;
		}

		public DataGridViewRow ConvertRow()
		{
			var row = new DataGridViewRow();
			row.Cells.Add(new DataGridViewTextBoxCell { Value = Number });
			row.Cells.Add(new DataGridViewTextBoxCell { Value = ToString() });

			row.Cells.Add(new DataGridViewTextBoxCell
			{
				Value = Type1,
				Style = new DataGridViewCellStyle { BackColor = Type1.BackColor() }
			});
			row.Cells.Add(new DataGridViewTextBoxCell
			{
				Value = Type2,
				Style = new DataGridViewCellStyle { BackColor = Type2.HasValue ? Type2.Value.BackColor() : Color.White }
			});

			row.Cells.Add(new DataGridViewTextBoxCell { Value = BaseHP });
			row.Cells.Add(new DataGridViewTextBoxCell { Value = BaseAttack });
			row.Cells.Add(new DataGridViewTextBoxCell { Value = BaseDefense });
			row.Cells.Add(new DataGridViewTextBoxCell { Value = BaseSpAttack });
			row.Cells.Add(new DataGridViewTextBoxCell { Value = BaseSpDefense });
			row.Cells.Add(new DataGridViewTextBoxCell { Value = BaseSpeed });
			row.Cells.Add(new DataGridViewTextBoxCell { Value = TotalStat });
			row.Cells.Add(new DataGridViewTextBoxCell { Value = PhysicalSweeper });
			row.Cells.Add(new DataGridViewTextBoxCell { Value = SpecialSweeper });
			row.Cells.Add(new DataGridViewTextBoxCell { Value = Wall });
			row.Cells.Add(new DataGridViewTextBoxCell { Value = PhysicalTank });
			row.Cells.Add(new DataGridViewTextBoxCell { Value = SpecialTank });

			row.Cells.Add(new DataGridViewTextBoxCell { Value = Ability1 });
			row.Cells.Add(new DataGridViewTextBoxCell { Value = Ability2 });
			row.Cells.Add(new DataGridViewTextBoxCell { Value = HiddenAbility });
			return row;
		}

		public static async Task<Pokemon[]> GetListPokemonWeb()
		{
			return null;
			var watch = new System.Diagnostics.Stopwatch(); watch.Start();
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

			var rows = doc.DocumentNode.SelectNodes("//*[@id='pokedex']/tbody/tr");
			var pokeList = new List<Pokemon>(rows.Count);
			var actionBlock = new ActionBlock<Pokemon>(p => p.GetDetailedInfo(), new ExecutionDataflowBlockOptions { MaxDegreeOfParallelism = rows.Count });
			foreach (var row in rows)
			{
				var types = row.SelectNodes(".//*[contains(@class, 'type-icon')]").Select(n => n.InnerText).ToArray();
				var nums = row.SelectNodes(".//*[@class='num']").Select(n => int.Parse(n.InnerText)).ToArray();
				var pkmn = new Pokemon(int.Parse(row.SelectSingleNode(".//*[@class='num cell-icon-string']").InnerText), row.SelectSingleNode(".//*[@class='ent-name']").InnerText)
				{
					Form = row.SelectSingleNode(".//*[@class='aside']")?.InnerText,

					Type1 = Type.Parse(types[0]),
					Type2 = types.Length == 2 ? Type.Parse(types[1]) : null,

					BaseHP = nums[0],
					BaseAttack = nums[1],
					BaseDefense = nums[2],
					BaseSpAttack = nums[3],
					BaseSpDefense = nums[4],
					BaseSpeed = nums[5]
				};

				actionBlock.Post(pkmn);

				pokeList.Add(pkmn);
			}

			actionBlock.Complete();
			await actionBlock.Completion;
			watch.Stop(); System.Diagnostics.Debug.Print("ListPokemon a pris " + watch.ElapsedMilliseconds + " ms.");
			return pokeList.ToArray();
		}

		private async Task GetDetailedInfo()
		{
			string content = "";
			HttpResponseMessage response;
			try
			{
				// Remove diacritics.
				string name = Encoding.UTF8.GetString(Encoding.GetEncoding("ISO-8859-8").GetBytes(Name.ToLower()));
				if (Name == "Nidoran♀")
					name = "nidoran-f";
				if (Name == "Nidoran♂")
					name = "nidoran-m";
				// Remove "'" and replace everything else with a "-".
				name = Regex.Replace(name.Replace("'", ""), @"\W+", "-").Trim('-');
				response = await client.GetAsync("http://pokemondb.net/pokedex/" + name);
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
			content = await response.Content.ReadAsStringAsync();

			var doc = DocumentHtml.GetHtmlDocument(content);

			var tables = doc.DocumentNode.SelectNodes("//*[@class='vitals-table']");
			//0=Summary
			var abilities = tables[0].SelectNodes(".//*[contains(@href, '/ability/')]");
			Ability1 = abilities[0].InnerText;
			if (abilities.Count > 1 && abilities[1].ParentNode.Name != "small")
				Ability2 = abilities[1].InnerText;
			if (abilities.Last().ParentNode.Name == "small")
				HiddenAbility = abilities.Last().InnerText;
			//1=Training
			//2=Breeding
			//3=Base Stats
			//4=Pokédex
			//5=Locations

			var typeDefenseValues = doc.DocumentNode.SelectNodes("//*[@class='type-table']//td").Select(n => WebUtility.HtmlDecode(n.InnerText)).ToArray();
		}

		public static Pokemon[] GetListPokemonDB()
		{
#if DEBUG
			DifferencesSunMoon.ExecuteRemoveConquest();
			DifferencesSunMoon.ExecuteDifferences();
			var watch = new System.Diagnostics.Stopwatch(); watch.Start();
#endif
			var pokeList = new List<Pokemon>();

			using var sql = new SQLite.SQLiteConnection("pokedex.sqlite");
			var pokemonsMap = Mappings.Pokemon.GetAllPokemons(sql);
			pokeList.Capacity = pokemonsMap.Count;
			var pokemonSpecies = Mappings.PokemonSpecie.GetAllPokemonSpecies(sql);
			var pokemonSpeciesNames = Mappings.PokemonSpecieName.GetAllPokemonSpeciesNames(sql);
			var pokemonForms = Mappings.PokemonForm.GetAllPokemonForms(sql);
			var pokemonFormNames = Mappings.PokemonFormName.GetAllPokemonFormNames(sql);
			var pokemonTypes = Mappings.PokemonType.GetAllPokemonTypes(sql);
			var pokemonStats = Mappings.PokemonStat.GetAllPokemonStats(sql);
			var pokemonAbilities = Mappings.PokemonAbility.GetAllPokemonAbilities(sql);
			var pokemons = pokemonsMap
				.Join(pokemonSpecies, p => p.SpeciesId, ps => ps.Id, (p, ps) => new { p, ps })
				.Join(pokemonSpeciesNames, p => p.ps.Id, psn => psn.PokemonSpeciesId, (p, psn) => new { p.p, p.ps, psn })
				.GroupJoin(pokemonForms, p => p.p.Id, pf => pf.PokemonId, (p, pf) => new { p.p, p.ps, p.psn, pf }).SelectMany(p => p.pf.DefaultIfEmpty(), (p, pf) => new { p.p, p.ps, p.psn, pf })
				.GroupJoin(pokemonFormNames, p => p.pf.Id, pfn => pfn.PokemonFormId, (p, pfn) => new { p.p, p.ps, p.psn, p.pf, pfn }).SelectMany(p => p.pfn.DefaultIfEmpty(), (p, pfn) => new { p.p, p.ps, p.psn, p.pf, pfn })
				.GroupJoin(pokemonTypes, p => p.p.Id, pt => pt.PokemonId, (p, pt) => new { p.p, p.ps, p.psn, p.pf, p.pfn, pt = pt.ToArray() })
				.GroupJoin(pokemonStats, p => p.p.Id, ps => ps.PokemonId, (p, ps) => new { p.p, p.ps, p.psn, p.pf, p.pfn, p.pt, pst = ps.ToArray() })
				.GroupJoin(pokemonAbilities, p => p.p.Id, pa => pa.PokemonId, (p, pa) => new ExtendedMappings.Pokemon(p.p, p.ps, p.psn, p.pf, p.pfn, p.pt, p.pst, pa.ToArray())).ToList();

			var types = Mappings.Type.GetAllTypes(sql);

			var stats = Mappings.Stat.GetAllStats(sql);
			var statHP = stats.Single(s => s.IsHP);
			var statAttack = stats.Single(s => s.IsAttack);
			var statDefense = stats.Single(s => s.IsDefense);
			var statSpecialAttack = stats.Single(s => s.IsSpecialAttack);
			var statSpecialDefense = stats.Single(s => s.IsSpecialDefense);
			var statSpeed = stats.Single(s => s.IsSpeed);

			var abilitiesMap = Mappings.Ability.GetAllAbilities(sql);
			var abilitieNames = Mappings.AbilityName.GetAllAbilityNames(sql);
			var abilities = abilitiesMap.Join(abilitieNames, a => a.Id, an => an.AbilityId, (a, an) => new ExtendedMappings.Ability(a, an));

			foreach (var pkmn in pokemons)
			{
				var pokemonType2 = pkmn.Types.SingleOrDefault(pt => pt.Slot == 2);
				var pokemon = new Pokemon(pkmn.SpeciesId, pkmn.Name)
				{
					Form = pkmn.FormName,

					Type1 = Type.Parse(types.Single(t => t.Id == pkmn.Types.Single(pt => pt.Slot == 1).TypeId).Identifier),
					Type2 = pokemonType2 != null ? Type.Parse(types.Single(t => t.Id == pokemonType2.TypeId).Identifier) : null,

					BaseHP = pkmn.Stats.Single(ps => ps.StatId == statHP.Id).BaseStat,
					BaseAttack = pkmn.Stats.Single(ps => ps.StatId == statAttack.Id).BaseStat,
					BaseDefense = pkmn.Stats.Single(ps => ps.StatId == statDefense.Id).BaseStat,
					BaseSpAttack = pkmn.Stats.Single(ps => ps.StatId == statSpecialAttack.Id).BaseStat,
					BaseSpDefense = pkmn.Stats.Single(ps => ps.StatId == statSpecialDefense.Id).BaseStat,
					BaseSpeed = pkmn.Stats.Single(ps => ps.StatId == statSpeed.Id).BaseStat,

					Ability1 = abilities.Single(a => a.Id == pkmn.Abilities.Single(pa => pa.Slot == 1).AbilityId).Name,
					Ability2 = abilities.SingleOrDefault(a => a.Id == pkmn.Abilities.SingleOrDefault(pa => pa.Slot == 2)?.AbilityId)?.Name,
					HiddenAbility = abilities.SingleOrDefault(a => a.Id == pkmn.Abilities.SingleOrDefault(pa => pa.IsHidden)?.AbilityId)?.Name
				};

				pokeList.Add(pokemon);
			}
#if DEBUG
			watch.Stop(); System.Diagnostics.Debug.Print("ListPokemonDB a pris " + watch.ElapsedMilliseconds + " ms.");
#endif

			return pokeList.ToArray();
		}
	}
}
