using System;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace MissingNopedia.AdvancedSearch
{
	public class Pokemon
	{
		[JsonProperty("forms")]
		private readonly PokemonForm[] pokemonForms = new PokemonForm[1];
		/// <summary>
		/// Do not use this directly.
		/// </summary>
		private PokemonForm pokemonForm;
		private string ability1 = "";
		private string ability2 = "";
		private string hiddenAbility = "";

		/// <summary>
		/// Get additional data for the current form.
		/// </summary>
		/// <returns></returns>
		private PokemonForm GetPokemonForm()
		{
			if (pokemonForm == null)
				pokemonForm = pokemonForms.Single(f => f.Id == Number);
			return pokemonForm;
		}

		public int Number { get; set; }
		[JsonConverter(typeof(PokemonNameConverter))]
		public string Name { get; set; }
		public string Form { get; set; }
		public Type Type1 => GetPokemonForm().Types[0];
		public Type? Type2 => GetPokemonForm().Types.Length > 1 ? GetPokemonForm().Types[1] : null;

		public int BaseHP => GetPokemonForm().BaseStats[0].BaseStat;
		public int BaseAttack => GetPokemonForm().BaseStats[1].BaseStat;
		public int BaseDefense => GetPokemonForm().BaseStats[2].BaseStat;
		public int BaseSpAttack => GetPokemonForm().BaseStats[3].BaseStat;
		public int BaseSpDefense => GetPokemonForm().BaseStats[4].BaseStat;
		public int BaseSpeed => GetPokemonForm().BaseStats[5].BaseStat;

		public int TotalStat { get { return BaseHP + BaseAttack + BaseDefense + BaseSpAttack + BaseSpDefense + BaseSpeed; } }
		public int PhysicalSweeper { get { return BaseAttack + BaseSpeed; } }
		public int SpecialSweeper { get { return BaseSpAttack + BaseSpeed; } }
		public int Wall { get { return BaseHP + BaseDefense + BaseSpDefense; } }
		public int PhysicalTank { get { return BaseAttack + BaseDefense; } }
		public int SpecialTank { get { return BaseSpAttack + BaseSpDefense; } }

		public string Ability1
		{
			get
			{
				if (ability1 == "")
					ability1 = GetPokemonForm().Abilities.Single(a => a.Slot == 1).Ability.Name;
				return ability1;
			}
		}
		public string Ability2
		{
			get
			{
				if (ability2 == "")
					ability2 = GetPokemonForm().Abilities.SingleOrDefault(a => a.Slot == 2)?.Ability.Name;
				return ability2;
			}
		}
		public string HiddenAbility
		{
			get
			{
				if (hiddenAbility == "")
					hiddenAbility = GetPokemonForm().Abilities.SingleOrDefault(a => a.IsHidden)?.Ability.Name;
				return hiddenAbility;
			}
		}

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
				Style = new DataGridViewCellStyle { BackColor = Type2.HasValue ? Type2.Value.BackColor() : Color.Empty }
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

		private class PokemonForm
		{
			public int Id { get; set; }
			[JsonConverter(typeof(PokemonTypesConverter))]
			public Type[] Types { get; set; }
			public PokemonStat[] BaseStats { get; set; }
			public PokemonAbility[] Abilities { get; set; }

			public class PokemonStat
			{
				[JsonProperty("stat_id")]
				public int StatId { get; set; }
				[JsonProperty("base_stat")]
				public int BaseStat { get; set; }
			}

			public class PokemonAbility
			{
				public int Slot { get; set; }
				[JsonProperty("is_hidden")]
				public bool IsHidden { get; set; }
				public PokemonAbilityAbility Ability { get; set; }

				public class PokemonAbilityAbility
				{
					[JsonConverter(typeof(PokemonNameConverter))]
					public string Name { get; set; }
				}
			}
		}

		private class PokemonNameConverter : JsonConverter<string>
		{
			private class PokemonName
			{
				public string Name { get; set; }
			}

			public override string ReadJson(JsonReader reader, System.Type objectType, [AllowNull] string existingValue, bool hasExistingValue, JsonSerializer serializer)
			{
				return serializer.Deserialize<PokemonName[]>(reader)[0].Name;
			}

			public override void WriteJson(JsonWriter writer, [AllowNull] string value, JsonSerializer serializer) => throw new NotImplementedException();
		}

		private class PokemonTypesConverter : JsonConverter<Type[]>
		{
			private class PokemonTypes
			{
				public class PokemonType
				{
					public string Name { get; set; }
				}
				public PokemonType Type { get; set; }
			}

			public override Type[] ReadJson(JsonReader reader, System.Type objectType, [AllowNull] Type[] existingValue, bool hasExistingValue, JsonSerializer serializer)
			{
				return serializer.Deserialize<PokemonTypes[]>(reader).Select(t => new Type(Enum.Parse<Type.TypeValue>(t.Type.Name, true))).ToArray();
			}

			public override void WriteJson(JsonWriter writer, [AllowNull] Type[] value, JsonSerializer serializer) => throw new NotImplementedException();
		}
	}
}
