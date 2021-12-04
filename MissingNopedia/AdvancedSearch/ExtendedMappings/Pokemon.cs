using System;

namespace MissingNopedia.AdvancedSearch.ExtendedMappings
{
	public class Pokemon
	{
		private readonly Mappings.Pokemon _Pokemon;
		[System.Diagnostics.CodeAnalysis.SuppressMessage("CodeQuality", "IDE0052:Supprimer les membres privés non lus", Justification = "<En attente>")]
		private readonly Mappings.PokemonSpecie _PokemonSpecie;
		private readonly Mappings.PokemonSpecieName _PokemonSpecieName;
		private readonly Mappings.PokemonForm _PokemonForm;
		private readonly Mappings.PokemonFormName _PokemonFormName;

		public int Id
		{
			get { return _Pokemon.Id; }
			set
			{
				_Pokemon.Id = value;
				_PokemonForm.PokemonId = value;
				Array.ForEach(Types, pt => pt.PokemonId = value);
				Array.ForEach(Stats, ps => ps.PokemonId = value);
				Array.ForEach(Abilities, pa => pa.PokemonId = value);
			}
		}
		public string Name { get { return _PokemonSpecieName.Name; } set { _PokemonSpecieName.Name = value; } }
		public string FormName { get { return _PokemonFormName?.FormName; } set { if (_PokemonFormName != null) _PokemonFormName.FormName = value; } }
		public int SpeciesId { get { return _Pokemon.SpeciesId; } set { _Pokemon.SpeciesId = value; _PokemonSpecieName.PokemonSpeciesId = value; } }
		public Mappings.PokemonType[] Types { get; }
		public Mappings.PokemonStat[] Stats { get; }
		public Mappings.PokemonAbility[] Abilities { get; }

		public Pokemon(Mappings.Pokemon pokemon, Mappings.PokemonSpecie pokemonSpecie, Mappings.PokemonSpecieName pokemonSpecieName
			, Mappings.PokemonForm pokemonForm, Mappings.PokemonFormName pokemonFormName, Mappings.PokemonType[] pokemonTypes, Mappings.PokemonStat[] pokemonStats, Mappings.PokemonAbility[] pokemonAbilities)
		{
			_Pokemon = pokemon;
			_PokemonSpecie = pokemonSpecie;
			_PokemonSpecieName = pokemonSpecieName;
			_PokemonForm = pokemonForm;
			_PokemonFormName = pokemonFormName;
			Types = pokemonTypes;
			Stats = pokemonStats;
			Abilities = pokemonAbilities;
		}

		public override string ToString()
		{
			return Name;
		}
	}
}
