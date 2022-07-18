using System;
using System.Collections.Generic;

namespace MissingNopedia.AdvancedSearch
{
	public class Ability
	{
		public string Name { get; private set; }
		public Dictionary<string, string> Description { get; } = new Dictionary<string, string>();

		public Ability(string name, Dictionary<string, string> description)
		{
			Name = name ?? throw new ArgumentNullException(nameof(name));
			Description = description ?? throw new ArgumentNullException(nameof(description));
		}

		public override string ToString() => Name;
	}
}
