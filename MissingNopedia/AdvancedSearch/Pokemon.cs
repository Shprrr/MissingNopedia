using System;
using System.Collections.Generic;
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
		public string Form => GetPokemonForm().Form?.Name;
		public Type Type1 => GetPokemonForm().Types.Types[0];
		public Type? Type2 => GetPokemonForm().Types.Types.Length > 1 ? GetPokemonForm().Types.Types[1] : null;

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
					ability1 = GetPokemonForm().Abilities.SingleOrDefault(a => a.Slot == 1)?.Ability.Name;
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

		public Type[] Weaknesses => GetPokemonForm().Types.Efficacies.Where(t => t.DamageMultiplier > 1).Select(t => t.DamageType).ToArray();
		public Type[] Resistances => GetPokemonForm().Types.Efficacies.Where(t => t.DamageMultiplier < 1).Select(t => t.DamageType).ToArray();

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

		public IEnumerable<Pokemon> GetForms() => pokemonForms.Select(f => new Pokemon(f.Id, Name) { pokemonForm = f });

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
				ToolTipText = Type2?.ToString(),
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
			row.Cells.Add(new DataGridViewTextBoxCell
			{
				Value = Ability2,
				ToolTipText = Ability2?.ToString()
			});
			row.Cells.Add(new DataGridViewTextBoxCell
			{
				Value = HiddenAbility,
				ToolTipText = HiddenAbility?.ToString()
			});
			return row;
		}

		private class PokemonForm
		{
			public int Id { get; set; }
			[JsonConverter(typeof(PokemonFormConverter))]
			public PokemonFormForm Form { get; set; }
			[JsonConverter(typeof(PokemonTypesConverter))]
			public PokemonTypes Types { get; set; }
			public PokemonStat[] BaseStats { get; set; }
			public PokemonAbility[] Abilities { get; set; }

			public class PokemonFormForm
			{
				[JsonProperty("form_order")]
				public int FormOrder { get; set; }
				[JsonProperty("form_name")]
				public string FormName { get; set; }
				[JsonProperty("is_default")]
				public bool IsDefault { get; set; }
				[JsonProperty("is_battle_only")]
				public bool IsBattleOnly { get; set; }
				[JsonProperty("is_mega")]
				public bool IsMega { get; set; }
				[JsonConverter(typeof(PokemonNameConverter))]
				public string Name { get; set; }

				public override string ToString() => FormName;
			}

			public class PokemonTypes
			{
				public Type[] Types { get; set; }
				public PokemonTypesEfficacy[] Efficacies { get; set; }

				public class PokemonTypesEfficacy
				{
					public Type DamageType { get; set; }
					public float DamageMultiplier { get; set; }
					public override string ToString() => $"{DamageType} > {DamageMultiplier}";
				}
				public override string ToString() => string.Join(' ', Types);
			}

			public class PokemonStat
			{
				[JsonProperty("stat_id")]
				public int StatId { get; set; }
				[JsonProperty("base_stat")]
				public int BaseStat { get; set; }

				public override string ToString() => BaseStat.ToString();
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

				public override string ToString() => Ability.Name;
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
				PokemonName[] pokemonNames = serializer.Deserialize<PokemonName[]>(reader);
				return pokemonNames.Length == 0 ? null : pokemonNames[0].Name;
			}

			public override void WriteJson(JsonWriter writer, [AllowNull] string value, JsonSerializer serializer) => throw new NotImplementedException();
		}

		private class PokemonTypeConverter : JsonConverter<Type>
		{
			private class TypeName
			{
				public string Name { get; set; }
			}

			public override Type ReadJson(JsonReader reader, System.Type objectType, [AllowNull] Type existingValue, bool hasExistingValue, JsonSerializer serializer)
			{
				if (reader.ValueType == typeof(string))
					return new Type(Enum.Parse<Type.TypeValue>(serializer.Deserialize<string>(reader), true));

				return new Type(Enum.Parse<Type.TypeValue>(serializer.Deserialize<TypeName>(reader).Name, true));
			}

			public override void WriteJson(JsonWriter writer, [AllowNull] Type value, JsonSerializer serializer) => throw new NotImplementedException();
		}

		private class PokemonFormConverter : JsonConverter<PokemonForm.PokemonFormForm>
		{
			public override PokemonForm.PokemonFormForm ReadJson(JsonReader reader, System.Type objectType, [AllowNull] PokemonForm.PokemonFormForm existingValue, bool hasExistingValue, JsonSerializer serializer)
			{
				return serializer.Deserialize<PokemonForm.PokemonFormForm[]>(reader).FirstOrDefault();
			}

			public override void WriteJson(JsonWriter writer, [AllowNull] PokemonForm.PokemonFormForm value, JsonSerializer serializer) => throw new NotImplementedException();
		}

		private class PokemonTypesConverter : JsonConverter<PokemonForm.PokemonTypes>
		{
			private class PokemonTypes
			{
				public class PokemonType
				{
					public class PokemonTypeEfficacy
					{
						[JsonProperty("damage_type"), JsonConverter(typeof(PokemonTypeConverter))]
						public Type DamageType { get; set; }
						[JsonProperty("target_type"), JsonConverter(typeof(PokemonTypeConverter))]
						public Type TargetType { get; set; }
						[JsonProperty("damage_factor")]
						public int DamageFactor { get; set; }
						public float DamageMultiplier => DamageFactor / 100f;
					}
					[JsonProperty("name"), JsonConverter(typeof(PokemonTypeConverter))]
					public Type Type { get; set; }
					public PokemonTypeEfficacy[] Efficacies { get; set; }
				}
				public PokemonType Type { get; set; }
			}

			public override PokemonForm.PokemonTypes ReadJson(JsonReader reader, System.Type objectType, [AllowNull] PokemonForm.PokemonTypes existingValue, bool hasExistingValue, JsonSerializer serializer)
			{
				PokemonTypes[] pokemonTypes = serializer.Deserialize<PokemonTypes[]>(reader);
				return new PokemonForm.PokemonTypes
				{
					Types = pokemonTypes.Select(t => t.Type.Type).ToArray(),
					Efficacies = pokemonTypes.SelectMany(t => t.Type.Efficacies).GroupBy(t => t.DamageType, (d, ts) => new PokemonForm.PokemonTypes.PokemonTypesEfficacy { DamageType = d, DamageMultiplier = ts.Aggregate(1f, (dm, t) => dm * t.DamageMultiplier) }).ToArray()
				};
			}

			public override void WriteJson(JsonWriter writer, [AllowNull] PokemonForm.PokemonTypes value, JsonSerializer serializer) => throw new NotImplementedException();
		}
	}
}
