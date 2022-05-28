using System;
using System.Linq;
using System.Windows.Forms;

namespace MissingNopedia.AdvancedSearch.Criteria
{
	public partial class Criterion : UserControl
	{
		public enum Conjonction
		{
			And,
			Or
		}

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
			AbilityName,
			Weakness,
			Resistance
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

			cboConjonction.SelectedIndex = 0;

			var types = new[] { new { Display = "Pokemon Number", Value = Type.PokemonNumber }, new { Display = "Pokemon Name", Value = Type.PokemonName }, new { Display = "Type", Value = Type.Type }, new { Display = "HP Base Stat", Value = Type.HP }, new { Display = "Attack Base Stat", Value = Type.Attack }, new { Display = "Defense Base Stat", Value = Type.Defense }, new { Display = "Special Attack Base Stat", Value = Type.SpecialAttack }, new { Display = "Special Defense Base Stat", Value = Type.SpecialDefense }, new { Display = "Total Base Stat", Value = Type.Total }, new { Display = "Physical Sweeper Base Stat", Value = Type.PhysicalSweeper }, new { Display = "Special Sweeper Base Stat", Value = Type.SpecialSweeper }, new { Display = "Wall Base Stat", Value = Type.Wall }, new { Display = "Physical Tank Base Stat", Value = Type.PhysicalTank }, new { Display = "Special Tank Base Stat", Value = Type.SpecialTank }, new { Display = "Ability Name", Value = Type.AbilityName }, new { Display = "Weakness to Type", Value = Type.Weakness }, new { Display = "Resistance to Type", Value = Type.Resistance } };
			cboType.DataSource = types;
			cboType.DisplayMember = "Display";
			cboType.ValueMember = "Value";

			var operators = new[] { new { Display = "=", Value = Operator.Equals }, new { Display = "!=", Value = Operator.NotEquals }, new { Display = "like", Value = Operator.Like }, new { Display = "not like", Value = Operator.NotLike }, new { Display = "<", Value = Operator.Lesser }, new { Display = "<=", Value = Operator.LesserEqual }, new { Display = ">", Value = Operator.Greater }, new { Display = ">=", Value = Operator.GreaterEqual } };
			cboOperator.DataSource = operators;
			cboOperator.DisplayMember = "Display";
			cboOperator.ValueMember = "Value";
		}

		private void Criterion_ParentChanged(object sender, EventArgs e)
		{
			if (Parent == null) return;

			var parentIndex = Parent.Controls.GetChildIndex(this);
			cboConjonction.Visible = parentIndex != 0;
		}

		private void picHandle_MouseDown(object sender, MouseEventArgs e)
		{
			DoDragDrop(this, DragDropEffects.Move);
		}

		public Conjonction? ConjonctionValue
		{
			get
			{
				if (!cboConjonction.Visible) return null;
				return cboConjonction.SelectedIndex == 1 ? Conjonction.Or : Conjonction.And;
			}

			set => cboConjonction.SelectedIndex = value == Conjonction.Or ? 1 : 0;
		}

		public Type TypeValue { get => (Type)cboType.SelectedValue; set => cboType.SelectedValue = value; }

		public Operator OperatorValue { get => (Operator)cboOperator.SelectedValue; set => cboOperator.SelectedValue = value; }

		public string Value { get => txtValue.Text; set => txtValue.Text = value; }

		public Func<Pokemon, bool> ApplyCriterion(Func<Pokemon, bool> filter)
		{
			var criterion = (Pokemon p) => filter(p);
			switch (TypeValue)
			{
				case Type.PokemonNumber:
					criterion = (Pokemon p) => CompareOperator(p.Number);
					break;
				case Type.PokemonName:
					criterion = (Pokemon p) => CompareOperator(p.Name);
					break;
				case Type.Type:
					criterion = (Pokemon p) => CompareOperator(p.Type1.ToString()) || p.Type2 != null && CompareOperator(p.Type2.ToString());
					break;
				case Type.HP:
					criterion = (Pokemon p) => CompareOperator(p.BaseHP);
					break;
				case Type.Attack:
					criterion = (Pokemon p) => CompareOperator(p.BaseAttack);
					break;
				case Type.Defense:
					criterion = (Pokemon p) => CompareOperator(p.BaseDefense);
					break;
				case Type.SpecialAttack:
					criterion = (Pokemon p) => CompareOperator(p.BaseSpAttack);
					break;
				case Type.SpecialDefense:
					criterion = (Pokemon p) => CompareOperator(p.BaseSpDefense);
					break;
				case Type.Speed:
					criterion = (Pokemon p) => CompareOperator(p.BaseSpeed);
					break;
				case Type.Total:
					criterion = (Pokemon p) => CompareOperator(p.TotalStat);
					break;
				case Type.PhysicalSweeper:
					criterion = (Pokemon p) => CompareOperator(p.PhysicalSweeper);
					break;
				case Type.SpecialSweeper:
					criterion = (Pokemon p) => CompareOperator(p.SpecialSweeper);
					break;
				case Type.Wall:
					criterion = (Pokemon p) => CompareOperator(p.Wall);
					break;
				case Type.PhysicalTank:
					criterion = (Pokemon p) => CompareOperator(p.PhysicalTank);
					break;
				case Type.SpecialTank:
					criterion = (Pokemon p) => CompareOperator(p.SpecialTank);
					break;
				case Type.AbilityName:
					criterion = (Pokemon p) => CompareOperator(p.Ability1) || p.Ability2 != null && CompareOperator(p.Ability2) || p.HiddenAbility != null && CompareOperator(p.HiddenAbility);
					break;
				case Type.Weakness:
					criterion = (Pokemon p) => p.Weaknesses.Select(t => t.ToString()).Any(CompareOperator);
					break;
				case Type.Resistance:
					criterion = (Pokemon p) => p.Resistances.Select(t => t.ToString()).Any(CompareOperator);
					break;
			}

			return ConjonctionValue switch
			{
				Conjonction.And => (p) => filter(p) && criterion(p),
				Conjonction.Or => (p) => filter(p) || criterion(p),
				_ => (p) => criterion(p)
			};
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
			if (!int.TryParse(Value, out var value)) return false;
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
