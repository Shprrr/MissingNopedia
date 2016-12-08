using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MissingNopedia.AdvancedSearch
{
	public class Pokemon
	{
		private static HttpClient client = new HttpClient();

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
			return row;
		}

		public static async Task<Pokemon[]> GetListPokemonDB()
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

			var rows = doc.DocumentNode.SelectNodes("//*[@id='pokedex']/tbody/tr");
			var pokeList = new List<Pokemon>(rows.Count);
			foreach (var row in rows)
			{
				var pkmn = new Pokemon(int.Parse(row.SelectSingleNode(".//*[@class='num cell-icon-string']").InnerText), row.SelectSingleNode(".//*[@class='ent-name']").InnerText);

				pkmn.Form = row.SelectSingleNode(".//*[@class='aside']")?.InnerText;

				var types = row.SelectNodes(".//*[contains(@class, 'type-icon')]").Select(n => n.InnerText).ToArray();
				pkmn.Type1 = Type.Parse(types[0]);
				if (types.Length == 2)
					pkmn.Type2 = Type.Parse(types[1]);

				var nums = row.SelectNodes(".//*[@class='num']").Select(n => int.Parse(n.InnerText)).ToArray();
				pkmn.BaseHP = nums[0];
				pkmn.BaseAttack = nums[1];
				pkmn.BaseDefense = nums[2];
				pkmn.BaseSpAttack = nums[3];
				pkmn.BaseSpDefense = nums[4];
				pkmn.BaseSpeed = nums[5];

				pokeList.Add(pkmn);
			}

			return pokeList.ToArray();
		}
	}
}
