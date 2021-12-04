namespace MissingNopedia.AdvancedSearch.ExtendedMappings
{
	public class Ability
	{
		private readonly Mappings.Ability _Ability;
		private readonly Mappings.AbilityName _AbilityName;

		public int Id { get { return _Ability.Id; } set { _Ability.Id = value; _AbilityName.AbilityId = value; } }
		public string Name { get { return _AbilityName.Name; } set { _AbilityName.Name = value; } }

		public Ability(Mappings.Ability ability, Mappings.AbilityName abilityName)
		{
			_Ability = ability;
			_AbilityName = abilityName;
		}

		public override string ToString()
		{
			return Name;
		}
	}
}
