using System;

namespace MissingNopedia.AdvancedSearch.ExtendedMappings
{
	public class Pokemon
	{
		private Mappings.Pokemon _Pokemon;
		private Mappings.PokemonSpecie _PokemonSpecie;
		private Mappings.PokemonSpecieName _PokemonSpecieName;
		private Mappings.PokemonForm _PokemonForm;
		private Mappings.PokemonFormName _PokemonFormName;
		private Mappings.PokemonType[] _PokemonTypes;
		private Mappings.PokemonStat[] _PokemonStats;
		private Mappings.PokemonAbility[] _PokemonAbilities;

		public int Id
		{
			get { return _Pokemon.Id; }
			set
			{
				_Pokemon.Id = value;
				_PokemonForm.PokemonId = value;
				Array.ForEach(_PokemonTypes, pt => pt.PokemonId = value);
				Array.ForEach(_PokemonStats, ps => ps.PokemonId = value);
				Array.ForEach(_PokemonAbilities, pa => pa.PokemonId = value);
			}
		}
		public string Name { get { return _PokemonSpecieName.Name; } set { _PokemonSpecieName.Name = value; } }
		public string FormName { get { return _PokemonFormName?.FormName; } set { if (_PokemonFormName != null) _PokemonFormName.FormName = value; } }
		public int SpeciesId { get { return _Pokemon.SpeciesId; } set { _Pokemon.SpeciesId = value; _PokemonSpecieName.PokemonSpeciesId = value; } }
		public Mappings.PokemonType[] Types { get { return _PokemonTypes; } }
		public Mappings.PokemonStat[] Stats { get { return _PokemonStats; } }
		public Mappings.PokemonAbility[] Abilities { get { return _PokemonAbilities; } }

		public Pokemon(Mappings.Pokemon pokemon, Mappings.PokemonSpecie pokemonSpecie, Mappings.PokemonSpecieName pokemonSpecieName
			, Mappings.PokemonForm pokemonForm, Mappings.PokemonFormName pokemonFormName, Mappings.PokemonType[] pokemonTypes, Mappings.PokemonStat[] pokemonStats, Mappings.PokemonAbility[] pokemonAbilities)
		{
			_Pokemon = pokemon;
			_PokemonSpecie = pokemonSpecie;
			_PokemonSpecieName = pokemonSpecieName;
			_PokemonForm = pokemonForm;
			_PokemonFormName = pokemonFormName;
			_PokemonTypes = pokemonTypes;
			_PokemonStats = pokemonStats;
			_PokemonAbilities = pokemonAbilities;
		}

		public override string ToString()
		{
			return Name;
		}
	}
}
