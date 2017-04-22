using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace MissingNopedia.AdvancedSearch.Criteria
{
	public partial class Criterion : UserControl
	{
		public enum Type
		{
			PokemonNumber,
			PokemonName,
			Type,
			HP,
			Attack,
			Defense,
			SpecialAttack,
			SpecialDefense,
			Speed,
			Total,
			PhysicalSweeper,
			SpecialSweeper,
			Wall,
			PhysicalTank,
			SpecialTank,
			AbilityName
		}

		public enum Operator
		{
			Equals,
			NotEquals,
			Like,
			NotLike,
			Lesser,
			LesserEqual,
			Greater,
			GreaterEqual
		}

		public Criterion()
		{
			InitializeComponent();

			var types = new[] { new { Display = "Pokemon Number", Value = Type.PokemonNumber }, new { Display = "Pokemon Name", Value = Type.PokemonName }, new { Display = "Type", Value = Type.Type }, new { Display = "HP Base Stat", Value = Type.HP }, new { Display = "Attack Base Stat", Value = Type.Attack }, new { Display = "Defense Base Stat", Value = Type.Defense }, new { Display = "Special Attack Base Stat", Value = Type.SpecialAttack }, new { Display = "Special Defense Base Stat", Value = Type.SpecialDefense }, new { Display = "Total Base Stat", Value = Type.Total }, new { Display = "Physical Sweeper Base Stat", Value = Type.PhysicalSweeper }, new { Display = "Special Sweeper Base Stat", Value = Type.SpecialSweeper }, new { Display = "Wall Base Stat", Value = Type.Wall }, new { Display = "Physical Tank Base Stat", Value = Type.PhysicalTank }, new { Display = "Special Tank Base Stat", Value = Type.SpecialTank }, new { Display = "Ability Name", Value = Type.AbilityName } };
			cboType.DataSource = types;
			cboType.DisplayMember = "Display";
			cboType.ValueMember = "Value";

			var operators = new[] { new { Display = "=", Value = Operator.Equals }, new { Display = "!=", Value = Operator.NotEquals }, new { Display = "like", Value = Operator.Like }, new { Display = "not like", Value = Operator.NotLike }, new { Display = "<", Value = Operator.Lesser }, new { Display = "<=", Value = Operator.LesserEqual }, new { Display = ">", Value = Operator.Greater }, new { Display = ">=", Value = Operator.GreaterEqual } };
			cboOperator.DataSource = operators;
			cboOperator.DisplayMember = "Display";
			cboOperator.ValueMember = "Value";
		}

		public Type TypeValue { get { return (Type)cboType.SelectedValue; } set { cboType.SelectedValue = value; } }

		public Operator OperatorValue { get { return (Operator)cboOperator.SelectedValue; } set { cboOperator.SelectedValue = value; } }

		public string Value { get { return txtValue.Text; } set { txtValue.Text = value; } }

		public Pokemon[] ApplyCriterion(Pokemon[] pokemons)
		{
			switch (TypeValue)
			{
				case Type.PokemonNumber:
					pokemons = pokemons.Where(p => CompareOperator(p.Number)).ToArray();
					break;
				case Type.PokemonName:
					pokemons = pokemons.Where(p => CompareOperator(p.Name)).ToArray();
					break;
				case Type.Type:
					pokemons = pokemons.Where(p => CompareOperator(p.Type1.ToString()) || CompareOperator(p.Type2.ToString())).ToArray();
					break;
				case Type.HP:
					pokemons = pokemons.Where(p => CompareOperator(p.BaseHP)).ToArray();
					break;
				case Type.Attack:
					pokemons = pokemons.Where(p => CompareOperator(p.BaseAttack)).ToArray();
					break;
				case Type.Defense:
					pokemons = pokemons.Where(p => CompareOperator(p.BaseDefense)).ToArray();
					break;
				case Type.SpecialAttack:
					pokemons = pokemons.Where(p => CompareOperator(p.BaseSpAttack)).ToArray();
					break;
				case Type.SpecialDefense:
					pokemons = pokemons.Where(p => CompareOperator(p.BaseSpDefense)).ToArray();
					break;
				case Type.Speed:
					pokemons = pokemons.Where(p => CompareOperator(p.BaseSpeed)).ToArray();
					break;
				case Type.Total:
					pokemons = pokemons.Where(p => CompareOperator(p.TotalStat)).ToArray();
					break;
				case Type.PhysicalSweeper:
					pokemons = pokemons.Where(p => CompareOperator(p.PhysicalSweeper)).ToArray();
					break;
				case Type.SpecialSweeper:
					pokemons = pokemons.Where(p => CompareOperator(p.SpecialSweeper)).ToArray();
					break;
				case Type.Wall:
					pokemons = pokemons.Where(p => CompareOperator(p.Wall)).ToArray();
					break;
				case Type.PhysicalTank:
					pokemons = pokemons.Where(p => CompareOperator(p.PhysicalTank)).ToArray();
					break;
				case Type.SpecialTank:
					pokemons = pokemons.Where(p => CompareOperator(p.SpecialTank)).ToArray();
					break;
				case Type.AbilityName:
					pokemons = pokemons.Where(p => CompareOperator(p.Ability1) || CompareOperator(p.Ability2) || CompareOperator(p.HiddenAbility)).ToArray();
					break;
			}
			return pokemons;
		}

		public bool CompareOperator(string valueToCompare)
		{
			switch (OperatorValue)
			{
				case Operator.Equals:
					return valueToCompare == Value;
				case Operator.NotEquals:
					return valueToCompare != Value;
				case Operator.Like:
					// Don't interfere if the user has specified an % at the start or the end.
					bool addAll = !Value.StartsWith("%") && !Value.EndsWith("%");
					string value = Value;
					// Encapsulates in a string to block the automatic addition of %.
					if (Value.StartsWith("\"") && Value.EndsWith("\""))
					{
						addAll = false;
						value = Value.Trim('"');
					}
					return valueToCompare.SqlLike((addAll ? "%" : "") + value + (addAll ? "%" : ""));
				case Operator.NotLike:
					// Don't interfere if the user has specified an % at the start or the end.
					addAll = !Value.StartsWith("%") && !Value.EndsWith("%");
					value = Value;
					// Encapsulates in a string to block the automatic addition of %.
					if (Value.StartsWith("\"") && Value.EndsWith("\""))
					{
						addAll = false;
						value = Value.Trim('"');
					}
					return !valueToCompare.SqlLike((addAll ? "%" : "") + value + (addAll ? "%" : ""));
				case Operator.Lesser:
					return valueToCompare.CompareTo(Value) < 0;
				case Operator.LesserEqual:
					return valueToCompare.CompareTo(Value) <= 0;
				case Operator.Greater:
					return valueToCompare.CompareTo(Value) > 0;
				case Operator.GreaterEqual:
					return valueToCompare.CompareTo(Value) >= 0;
			}
			return false;
		}

		public bool CompareOperator(int valueToCompare)
		{
			int value;
			int.TryParse(Value, out value);
			switch (OperatorValue)
			{
				case Operator.Equals:
					return valueToCompare == value;
				case Operator.NotEquals:
					return valueToCompare != value;
				case Operator.Like:
					// Don't interfere if the user has specified an % at the start or the end.
					bool addAll = !Value.StartsWith("%") && !Value.EndsWith("%");
					string valueString = Value;
					// Encapsulates in a string to block the automatic addition of %.
					if (Value.StartsWith("\"") && Value.EndsWith("\""))
					{
						addAll = false;
						valueString = Value.Trim('"');
					}
					return valueToCompare.ToString().SqlLike((addAll ? "%" : "") + valueString + (addAll ? "%" : ""));
				case Operator.NotLike:
					// Don't interfere if the user has specified an % at the start or the end.
					addAll = !Value.StartsWith("%") && !Value.EndsWith("%");
					valueString = Value;
					// Encapsulates in a string to block the automatic addition of %.
					if (Value.StartsWith("\"") && Value.EndsWith("\""))
					{
						addAll = false;
						valueString = Value.Trim('"');
					}
					return !valueToCompare.ToString().SqlLike((addAll ? "%" : "") + valueString + (addAll ? "%" : ""));
				case Operator.Lesser:
					return valueToCompare < value;
				case Operator.LesserEqual:
					return valueToCompare <= value;
				case Operator.Greater:
					return valueToCompare > value;
				case Operator.GreaterEqual:
					return valueToCompare >= value;
			}
			return false;
		}
	}
}
