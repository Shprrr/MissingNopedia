using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MissingNopedia.AdvancedSearch
{
	public class Pokemon
	{
		[JsonProperty("species"), JsonConverter(typeof(PokemonSpeciesConverter))]
		private readonly PokemonSpecies pokemonSpecies = new();
		[JsonProperty("forms")]
		private readonly PokemonForm[] pokemonForms = new PokemonForm[1];
		/// <summary>
		/// Do not use this directly.
		/// </summary>
		private PokemonForm pokemonForm;

		[JsonProperty("evolvesFrom")]
		private readonly PokemonEvolvesFrom[] pokemonEvolvesFrom = Array.Empty<PokemonEvolvesFrom>();

		private static readonly Ability AbilityNotSet = new("Not set", new());
		private Ability ability1 = AbilityNotSet;
		private Ability ability2 = AbilityNotSet;
		private Ability hiddenAbility = AbilityNotSet;

		/// <summary>
		/// Get additional data for the current form.
		/// </summary>
		/// <returns></returns>
		private PokemonForm GetPokemonForm()
		{
			pokemonForm ??= pokemonForms.Single(f => f.Id == Number);
			return pokemonForm;
		}

		[JsonProperty("number")]
		public int Number { get; private set; }
		public string Name => pokemonSpecies.Name;
		public string Form => GetPokemonForm().Form?.Name;
		public Type Type1 => GetPokemonForm().Types.Types[0];
		public Type? Type2 => GetPokemonForm().Types.Types.Length > 1 ? GetPokemonForm().Types.Types[1] : null;
		public Type[] Types
		{
			get
			{
				List<Type> listTypes = new() { Type1 };
				if (Type2.HasValue)
					listTypes.Add(Type2.Value);
				return listTypes.ToArray();
			}
		}

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

		public Ability Ability1
		{
			get
			{
				if (ability1 == AbilityNotSet)
				{
					var formAbility = GetPokemonForm().Abilities.SingleOrDefault(a => a.Slot == 1);
					ability1 = formAbility == null
						? null
						: (new(formAbility.Ability.Name, formAbility.Ability.Descriptions.ToDictionary(d => d.VersionGroupName, d => d.FlavorText)));
				}
				return ability1;
			}
		}
		public Ability Ability2
		{
			get
			{
				if (ability2 == AbilityNotSet)
				{
					var formAbility = GetPokemonForm().Abilities.SingleOrDefault(a => a.Slot == 2);
					ability2 = formAbility == null
						? null
						: (new(formAbility.Ability.Name, formAbility.Ability.Descriptions.ToDictionary(d => d.VersionGroupName, d => d.FlavorText)));
				}
				return ability2;
			}
		}
		public Ability HiddenAbility
		{
			get
			{
				if (hiddenAbility == AbilityNotSet)
				{
					var formAbility = GetPokemonForm().Abilities.SingleOrDefault(a => a.IsHidden);
					hiddenAbility = formAbility == null
						? null
						: (new(formAbility.Ability.Name, formAbility.Ability.Descriptions.ToDictionary(d => d.VersionGroupName, d => d.FlavorText)));
				}
				return hiddenAbility;
			}
		}
		public Ability[] Abilities
		{
			get
			{
				List<Ability> listAbilties = new();
				if (Ability1 != null)
					listAbilties.Add(Ability1);
				if (Ability2 != null)
					listAbilties.Add(Ability2);
				if (HiddenAbility != null)
					listAbilties.Add(HiddenAbility);
				return listAbilties.ToArray();
			}
		}

		public Type[] Weaknesses => GetPokemonForm().Types.Efficacies.Where(t => t.DamageMultiplier > 1).Select(t => t.DamageType).ToArray();
		public Type[] Resistances => GetPokemonForm().Types.Efficacies.Where(t => t.DamageMultiplier < 1).Select(t => t.DamageType).ToArray();
		public PokemonTypesEfficacy[] TypeEfficacies => GetPokemonForm().Types.Efficacies;

		[JsonIgnore]
		public string Species => pokemonSpecies.Species;
		public float HeightInMeters => GetPokemonForm().Height / 10f;
		public int HeightInFeet => (int)(GetPokemonForm().Height / 0.254 / 12);
		public int HeightInInches => (int)Math.Round(GetPokemonForm().Height / 0.254 % 12);
		public float WeightInKilograms => GetPokemonForm().Weight / 10f;
		public float WeightInPounds => GetPokemonForm().Weight / 4.5359237f;

		public string[] EffortValues => GetPokemonForm().BaseStats.Where(s => s.Effort > 0).Select(s => $"{s.Effort} {s.StatIdToString()}").ToArray();

		[JsonProperty("capture_rate")]
		public int CaptureRate { get; private set; }

		[JsonProperty("base_happiness")]
		public int? BaseHappiness { get; private set; }

		public int? BaseExperience => GetPokemonForm().BaseExperience;

		[JsonProperty("growthRate"), JsonConverter(typeof(PokemonNameConverter))]
		public string GrowthRate { get; private set; }

		[JsonProperty("eggGroups", ItemConverterType = typeof(PokemonEggGroupConverter))]
		public string[] EggGroups { get; private set; }

		[JsonProperty("hatch_counter")]
		public int? EggCycle { get; private set; }

		[JsonProperty("gender_rate"), JsonConverter(typeof(PokemonGenderRateConverter))]
		public GenderRate GenderRate { get; private set; }
		public float MaleGenderRate => GenderRate == GenderRate.Unknown ? 0 : 1 - FemaleGenderRate;
		public float FemaleGenderRate
		{
			get
			{
				var femaleRate = GenderRate switch
				{
					GenderRate.Unknown or GenderRate.Male => 0,
					GenderRate.Male7To1 => 30,
					GenderRate.Male3To1 => 62,
					GenderRate.Half => 126,
					GenderRate.Female3To1 => 190,
					GenderRate.Female7To1 => 224,
					GenderRate.Female => 253,
					_ => throw new NotImplementedException(),
				};

				return femaleRate / 253f;
			}
		}

		[JsonProperty("pokedexNumbers"), JsonConverter(typeof(PokedexNumbersConverter))]
		public Dictionary<string, int> PokedexNumbers { get; private set; }

		[JsonProperty("pokedexEntries"), JsonConverter(typeof(PokedexEntriesConverter))]
		public Dictionary<string, string> PokedexEntries { get; private set; }

		[JsonIgnore]
		public PokemonEvolution[] EvolvesFrom { get; private set; } = Array.Empty<PokemonEvolution>();
		[JsonIgnore]
		public PokemonEvolution[] EvolvesTo { get; private set; } = Array.Empty<PokemonEvolution>();

		[JsonConstructor]
		private Pokemon()
		{
		}

		private Pokemon(Pokemon pokemonOriginal, PokemonForm pokemonForm)
		{
			Number = pokemonForm.Id;
			pokemonSpecies = pokemonOriginal.pokemonSpecies;
			this.pokemonForm = pokemonForm;
			CaptureRate = pokemonOriginal.CaptureRate;
			BaseHappiness = pokemonOriginal.BaseHappiness;
			GrowthRate = pokemonOriginal.GrowthRate;
			EggGroups = pokemonOriginal.EggGroups;
			EggCycle = pokemonOriginal.EggCycle;
			GenderRate = pokemonOriginal.GenderRate;
			PokedexNumbers = pokemonOriginal.PokedexNumbers;
			PokedexEntries = pokemonOriginal.PokedexEntries;
			pokemonEvolvesFrom = pokemonOriginal.pokemonEvolvesFrom;
			EvolvesFrom = pokemonOriginal.EvolvesFrom;
			EvolvesTo = pokemonOriginal.EvolvesTo;
		}

		public override string ToString()
		{
			var s = Name;
			if (!string.IsNullOrEmpty(Form))
				s += " (" + Form + ")";
			return s;
		}

		public IEnumerable<Pokemon> GetForms() => pokemonForms.Select(f => new Pokemon(this, f));

		public void SetEvolutions(Pokemon[] pokemons)
		{
			EvolvesFrom = GetEvolvesFrom(pokemonEvolvesFrom.Join(pokemons, e => e.Specy.Number, p => p.Number, (e, p) => (p, e)));

			EvolvesTo = GetEvolvesFrom(pokemons.Where(p => p.pokemonEvolvesFrom.Any(e => e.Specy.Number == Number)).SelectMany(p => p.pokemonEvolvesFrom.Where(e => e.Specy.Number == Number), (p, pe) => (pokemon: p, evolvesFrom: pe)));

			static PokemonEvolution[] GetEvolvesFrom(IEnumerable<(Pokemon pokemon, PokemonEvolvesFrom evolvesFrom)> pokemonsEvolvesFrom)
			{
				List<PokemonEvolution> listEvolvesFrom = new();

				var evolvesFromToProcess = pokemonsEvolvesFrom.Where(pe => !pe.evolvesFrom.IsEmpty()).ToList();
				for (var i = 0; i < evolvesFromToProcess.Count; i++)
				{
					var pokemon = evolvesFromToProcess[i].pokemon;
					var evolvesFrom = evolvesFromToProcess[i].evolvesFrom;
					if (evolvesFrom.LocationId == 10) // if Magnetic Field
					{
						evolvesFromToProcess.RemoveAll(pe => pe.pokemon == pokemon && pe.evolvesFrom.LocationId.HasValue && pe.evolvesFrom.LocationId != 10);
						listEvolvesFrom.Add(new(pokemon, new PokemonEvolvesFrom()
						{
							Specy = evolvesFrom.Specy,
							EvolutionTrigger = evolvesFrom.EvolutionTrigger,
							MinLevel = evolvesFrom.MinLevel,
							MinHappiness = evolvesFrom.MinHappiness,
							TimeOfDay = evolvesFrom.TimeOfDay,
							RelativePhysicalStats = evolvesFrom.RelativePhysicalStats,
							MinBeauty = evolvesFrom.MinBeauty,
							Location = new() { Name = "Magnetic Field" },
							HeldItem = evolvesFrom.HeldItem,
							EvolutionItem = evolvesFrom.EvolutionItem
						}));
						continue;
					}

					if (evolvesFrom.Specy.Number == 737) // if Magnetic Field, but location unkown
					{
						listEvolvesFrom.Add(new(pokemon, new PokemonEvolvesFrom()
						{
							Specy = evolvesFrom.Specy,
							EvolutionTrigger = "level-up",
							Location = new() { Name = "Magnetic Field" }
						}));
					}

					if (evolvesFrom.LocationId == 8) // if Mossy Rock
					{
						evolvesFromToProcess.RemoveAll(pe => pe.pokemon == pokemon && pe.evolvesFrom.LocationId.HasValue && pe.evolvesFrom.LocationId != 8);
						listEvolvesFrom.Add(new(pokemon, new PokemonEvolvesFrom()
						{
							Specy = evolvesFrom.Specy,
							EvolutionTrigger = evolvesFrom.EvolutionTrigger,
							MinLevel = evolvesFrom.MinLevel,
							MinHappiness = evolvesFrom.MinHappiness,
							TimeOfDay = evolvesFrom.TimeOfDay,
							RelativePhysicalStats = evolvesFrom.RelativePhysicalStats,
							MinBeauty = evolvesFrom.MinBeauty,
							Location = new() { Name = "Mossy Rock" },
							HeldItem = evolvesFrom.HeldItem,
							EvolutionItem = evolvesFrom.EvolutionItem
						}));
						continue;
					}

					if (evolvesFrom.LocationId == 48) // if Icy Rock
					{
						evolvesFromToProcess.RemoveAll(pe => pe.pokemon == pokemon && pe.evolvesFrom.LocationId.HasValue && pe.evolvesFrom.LocationId != 48);
						listEvolvesFrom.Add(new(pokemon, new PokemonEvolvesFrom()
						{
							Specy = evolvesFrom.Specy,
							EvolutionTrigger = evolvesFrom.EvolutionTrigger,
							MinLevel = evolvesFrom.MinLevel,
							MinHappiness = evolvesFrom.MinHappiness,
							TimeOfDay = evolvesFrom.TimeOfDay,
							RelativePhysicalStats = evolvesFrom.RelativePhysicalStats,
							MinBeauty = evolvesFrom.MinBeauty,
							Location = new() { Name = "Icy Rock" },
							HeldItem = evolvesFrom.HeldItem,
							EvolutionItem = evolvesFrom.EvolutionItem
						}));
						continue;
					}

					if (evolvesFrom.Specy.Number == 265)
					{
						listEvolvesFrom.Add(new(pokemon, evolvesFrom, "random based on personality"));
						continue;
					}

					listEvolvesFrom.Add(new(pokemon, evolvesFrom));
				}

				return listEvolvesFrom.ToArray();
			}
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

			row.Cells.Add(new DataGridViewTextBoxCell
			{
				Value = Ability1?.Name,
				ToolTipText = Ability1?.Description.Last().Value
			});
			row.Cells.Add(new DataGridViewTextBoxCell
			{
				Value = Ability2?.Name,
				ToolTipText = Ability2?.Description.Last().Value
			});
			row.Cells.Add(new DataGridViewTextBoxCell
			{
				Value = HiddenAbility?.Name,
				ToolTipText = HiddenAbility?.Description.Last().Value
			});
			return row;
		}

		private class PokemonSpecies
		{
			public string Name { get; set; }
			[JsonProperty("genus")]
			public string Species { get; set; }
		}

		private class PokemonForm
		{
			public int Id { get; set; }
			public int Height { get; set; }
			public int Weight { get; set; }
			[JsonProperty("base_experience")]
			public int? BaseExperience { get; set; }
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

				public override string ToString() => string.Join(' ', Types);
			}

			public class PokemonStat
			{
				[JsonProperty("stat_id")]
				public int StatId { get; set; }
				[JsonProperty("base_stat")]
				public int BaseStat { get; set; }
				public int Effort { get; set; }

				public override string ToString() => BaseStat.ToString();

				public string StatIdToString() => StatId switch
				{
					1 => "HP",
					2 => "Attack",
					3 => "Defense",
					4 => "Special Attack",
					5 => "Special Defense",
					6 => "Speed",
					_ => ""
				};
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
					public PokemonAbilityDescription[] Descriptions { get; set; }

					public class PokemonAbilityDescription
					{
						[JsonProperty("versionGroup"), JsonConverter(typeof(PokemonNameConverter))]
						public string VersionGroupName { get; set; }
						[JsonProperty("flavor_text")]
						public string FlavorText { get; set; }
					}
				}

				public override string ToString() => Ability.Name;
			}
		}

		internal class PokemonEvolvesFrom
		{
			public PokemonSpecy Specy { get; set; }
			[JsonConverter(typeof(PokemonNameConverter))]
			public string EvolutionTrigger { get; set; }
			[JsonProperty("min_level")]
			public int? MinLevel { get; set; }
			[JsonProperty("min_happiness")]
			public int? MinHappiness { get; set; }
			[JsonProperty("time_of_day")]
			public string TimeOfDay { get; set; }
			[JsonProperty("relative_physical_stats")]
			public int? RelativePhysicalStats { get; set; }
			[JsonProperty("min_beauty")]
			public int? MinBeauty { get; set; }
			[JsonProperty("location_id")]
			public int? LocationId { get; set; }
			public PokemonEvolvesFromLocation Location { get; set; }
			public PokemonEvolvesFromName KnownMove { get; set; }
			[JsonProperty("party_species_id")]
			public int? PartySpeciesId { get; set; }
			[JsonProperty("gender_id")]
			public int? GenderId { get; set; }
			[JsonProperty("min_affection")]
			public int? MinAffection { get; set; }
			[JsonConverter(typeof(PokemonNameConverter))]
			public string KnownMoveType { get; set; }
			public PokemonEvolvesFromName HeldItem { get; set; }
			[JsonProperty("trade_species_id")]
			public int? TradeSpeciesId { get; set; }
			public PokemonEvolvesFromName EvolutionItem { get; set; }

			public bool IsEmpty() => EvolutionTrigger == "level-up" && !MinLevel.HasValue && !MinHappiness.HasValue
				&& string.IsNullOrEmpty(TimeOfDay) && !RelativePhysicalStats.HasValue && !MinBeauty.HasValue
				&& Location == null && KnownMove == null && !PartySpeciesId.HasValue && !GenderId.HasValue
				&& !MinAffection.HasValue && string.IsNullOrEmpty(KnownMoveType)
				&& HeldItem == null && !TradeSpeciesId.HasValue && EvolutionItem == null;

			public class PokemonSpecy
			{
				public int Number { get; set; }
			}

			public class PokemonEvolvesFromName
			{
				[JsonConverter(typeof(PokemonNameConverter))]
				public string Name { get; set; }
			}

			public class PokemonEvolvesFromLocation
			{
				[JsonConverter(typeof(PokemonNameConverter))]
				public string Name { get; set; }
				public PokemonEvolvesFromLocationRegion Region { get; set; }

				public class PokemonEvolvesFromLocationRegion
				{
					[JsonConverter(typeof(PokemonNameConverter))]
					public string Name { get; set; }
				}
			}
		}

		public class PokemonEvolution
		{
			public Pokemon Pokemon { get; set; }
			public Trigger EvolutionTrigger { get; set; }
			public int? MinLevel { get; set; }
			public int? MinHappiness { get; set; }
			public TimeDay? TimeOfDay { get; set; }
			public RelativeStats? RelativePhysicalStats { get; set; }
			public int? MinBeauty { get; set; }
			public string LocationName { get; set; }
			public string RegionName { get; set; }
			public string KnownMoveName { get; set; }
			public int? PartySpeciesId { get; set; }
			public Gender? EvolutionGender { get; set; }
			public int? MinAffection { get; set; }
			public string KnownMoveType { get; set; }
			public string HeldItemName { get; set; }
			public int? TradeSpeciesId { get; set; }
			public string EvolutionItemName { get; set; }
			public string SpecialEvolution { get; set; }

			internal PokemonEvolution(Pokemon pokemonFrom, PokemonEvolvesFrom evolvesFrom, string specialEvolution = null)
			{
				Pokemon = pokemonFrom;
				EvolutionTrigger = evolvesFrom.EvolutionTrigger switch
				{
					"level-up" => Trigger.LevelUp,
					"trade" => Trigger.Trade,
					"use-item" => Trigger.UseItem,
					"shed" => Trigger.Shed,
					"spin" => Trigger.Spin,
					"tower-of-darkness" => Trigger.TowerOfDarkness,
					"tower-of-waters" => Trigger.TowerOfWaters,
					"three-critical-hits" => Trigger.ThreeCriticalHits,
					"take-damage" => Trigger.TakeDamage,
					"other" => Trigger.Other,
					_ => Trigger.Other
				};
				MinLevel = evolvesFrom.MinLevel;
				MinHappiness = evolvesFrom.MinHappiness;
				TimeOfDay = evolvesFrom.TimeOfDay switch { "day" => TimeDay.Day, "night" => TimeDay.Night, "dusk" => TimeDay.Dusk, _ => null };
				RelativePhysicalStats = evolvesFrom.RelativePhysicalStats switch
				{
					0 => RelativeStats.AttackEqualsDefense,
					1 => RelativeStats.AttackGreaterDefense,
					-1 => RelativeStats.DefenseGreaterAttack,
					_ => null
				};
				MinBeauty = evolvesFrom.MinBeauty;
				LocationName = evolvesFrom.Location?.Name;
				RegionName = evolvesFrom.Location?.Region?.Name;
				KnownMoveName = evolvesFrom.KnownMove?.Name;
				PartySpeciesId = evolvesFrom.PartySpeciesId;
				EvolutionGender = evolvesFrom.GenderId switch
				{
					1 => Gender.Female,
					2 => Gender.Male,
					3 => Gender.Genderless,
					_ => null
				};
				MinAffection = evolvesFrom.MinAffection;
				KnownMoveType = evolvesFrom.KnownMoveType;
				HeldItemName = evolvesFrom.HeldItem?.Name;
				TradeSpeciesId = evolvesFrom.TradeSpeciesId;
				EvolutionItemName = evolvesFrom.EvolutionItem?.Name;
				SpecialEvolution = specialEvolution;
			}

			public enum Trigger
			{
				LevelUp,
				Trade,
				UseItem,
				Shed,
				Spin,
				TowerOfDarkness,
				TowerOfWaters,
				ThreeCriticalHits,
				TakeDamage,
				Other
			}

			public enum TimeDay
			{
				Day,
				Night,
				Dusk
			}

			public enum RelativeStats
			{
				AttackEqualsDefense,
				AttackGreaterDefense,
				DefenseGreaterAttack
			}

			public enum Gender
			{
				Female,
				Male,
				Genderless
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
				var o = serializer.Deserialize(reader);
				return GetName(o);
			}

			protected static string GetName(object o)
			{
				if (o is JObject jo)
					return jo["name"].ToString();
				else if (o is JArray ja)
				{
					PokemonName[] pokemonNames = ja.ToObject<PokemonName[]>();
					return pokemonNames.Length == 0 ? null : pokemonNames[0].Name;
				}
				return null;
			}

			public override void WriteJson(JsonWriter writer, [AllowNull] string value, JsonSerializer serializer) => throw new NotImplementedException();
		}

		private class PokemonEggGroupConverter : PokemonNameConverter
		{
			public override string ReadJson(JsonReader reader, System.Type objectType, [AllowNull] string existingValue, bool hasExistingValue, JsonSerializer serializer)
			{
				var o = serializer.Deserialize(reader) as JObject;
				return GetName(o.First.Children().Single());
			}
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

		private class PokemonSpeciesConverter : JsonConverter<PokemonSpecies>
		{
			public override PokemonSpecies ReadJson(JsonReader reader, System.Type objectType, [AllowNull] PokemonSpecies existingValue, bool hasExistingValue, JsonSerializer serializer)
			{
				return serializer.Deserialize<PokemonSpecies[]>(reader)[0];
			}

			public override void WriteJson(JsonWriter writer, [AllowNull] PokemonSpecies value, JsonSerializer serializer) => throw new NotImplementedException();
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
					Efficacies = pokemonTypes.SelectMany(t => t.Type.Efficacies)
						.GroupBy(t => t.DamageType, (d, ts) => new PokemonTypesEfficacy(d, ts.Aggregate(1f, (dm, t) => dm * t.DamageMultiplier))).ToArray()
				};
			}

			public override void WriteJson(JsonWriter writer, [AllowNull] PokemonForm.PokemonTypes value, JsonSerializer serializer) => throw new NotImplementedException();
		}

		private class PokemonGenderRateConverter : JsonConverter<GenderRate>
		{
			public override GenderRate ReadJson(JsonReader reader, System.Type objectType, [AllowNull] GenderRate existingValue, bool hasExistingValue, JsonSerializer serializer)
			{
				var genderRate = serializer.Deserialize<int>(reader);
				return genderRate switch
				{
					-1 => GenderRate.Unknown,
					0 => GenderRate.Male,
					1 => GenderRate.Male7To1,
					2 => GenderRate.Male3To1,
					4 => GenderRate.Half,
					6 => GenderRate.Female3To1,
					7 => GenderRate.Female7To1,
					8 => GenderRate.Female,
					_ => throw new NotImplementedException()
				};
			}

			public override void WriteJson(JsonWriter writer, [AllowNull] GenderRate value, JsonSerializer serializer) => throw new NotImplementedException();
		}

		private class PokedexNumbersConverter : JsonConverter<Dictionary<string, int>>
		{
			private class PokedexNumber
			{
				[JsonProperty("pokedexName"), JsonConverter(typeof(PokemonNameConverter))]
				public string PokedexName { get; set; }
				[JsonProperty("pokedex_number")]
				public int Number { get; set; }
			}

			public override Dictionary<string, int> ReadJson(JsonReader reader, System.Type objectType, [AllowNull] Dictionary<string, int> existingValue, bool hasExistingValue, JsonSerializer serializer)
			{
				return serializer.Deserialize<PokedexNumber[]>(reader).ToDictionary(pe => pe.PokedexName, pe => pe.Number);
			}

			public override void WriteJson(JsonWriter writer, [AllowNull] Dictionary<string, int> value, JsonSerializer serializer) => throw new NotImplementedException();
		}

		private class PokedexEntriesConverter : JsonConverter<Dictionary<string, string>>
		{
			private class PokedexEntry
			{
				[JsonProperty("version"), JsonConverter(typeof(PokemonNameConverter))]
				public string VersionName { get; set; }
				[JsonProperty("flavor_text")]
				public string FlavorText { get; set; }
			}

			public override Dictionary<string, string> ReadJson(JsonReader reader, System.Type objectType, [AllowNull] Dictionary<string, string> existingValue, bool hasExistingValue, JsonSerializer serializer)
			{
				return serializer.Deserialize<PokedexEntry[]>(reader).ToDictionary(pe => pe.VersionName, pe => pe.FlavorText);
			}

			public override void WriteJson(JsonWriter writer, [AllowNull] Dictionary<string, string> value, JsonSerializer serializer) => throw new NotImplementedException();
		}
	}
}
