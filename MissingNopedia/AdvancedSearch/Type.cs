using System;
using System.Drawing;

namespace MissingNopedia.AdvancedSearch
{
	public struct Type
	{
		public enum TypeValue
		{
			Normal,
			Fire,
			Water,
			Electric,
			Grass,
			Ice,
			Fighting,
			Poison,
			Ground,
			Flying,
			Psychic,
			Bug,
			Rock,
			Ghost,
			Dragon,
			Dark,
			Steel,
			Fairy
		}

		public TypeValue Value { get; set; }

		public Type(TypeValue value)
		{
			Value = value;
		}

		public override string ToString()
		{
			return Value.ToString();
		}

		public override bool Equals(object obj)
		{
			return obj switch
			{
				Type => Value == ((Type)obj).Value,
				TypeValue => Value == (TypeValue)obj,
				_ => base.Equals(obj)
			};
		}

		public override int GetHashCode()
		{
			return Value.GetHashCode();
		}

		public Color BackColor()
		{
			return Value switch
			{
				TypeValue.Normal => Color.FromArgb(unchecked((int)0xFF8a8a59)),
				TypeValue.Fire => Color.FromArgb(unchecked((int)0xFFf08030)),
				TypeValue.Water => Color.FromArgb(unchecked((int)0xFF6890f0)),
				TypeValue.Electric => Color.FromArgb(unchecked((int)0xFFf8d030)),
				TypeValue.Grass => Color.FromArgb(unchecked((int)0xFF78c850)),
				TypeValue.Ice => Color.FromArgb(unchecked((int)0xFF98d8d8)),
				TypeValue.Fighting => Color.FromArgb(unchecked((int)0xFFc03028)),
				TypeValue.Poison => Color.FromArgb(unchecked((int)0xFFa040a0)),
				TypeValue.Ground => Color.FromArgb(unchecked((int)0xFFe0c068)),
				TypeValue.Flying => Color.FromArgb(unchecked((int)0xFFa890f0)),
				TypeValue.Psychic => Color.FromArgb(unchecked((int)0xFFf85888)),
				TypeValue.Bug => Color.FromArgb(unchecked((int)0xFFa8b820)),
				TypeValue.Rock => Color.FromArgb(unchecked((int)0xFFb8a038)),
				TypeValue.Ghost => Color.FromArgb(unchecked((int)0xFF705898)),
				TypeValue.Dragon => Color.FromArgb(unchecked((int)0xFF7038f8)),
				TypeValue.Dark => Color.FromArgb(unchecked((int)0xFF705848)),
				TypeValue.Steel => Color.FromArgb(unchecked((int)0xFFb8b8d0)),
				TypeValue.Fairy => Color.FromArgb(unchecked((int)0xFFe898e8)),
				_ => Color.White,
			};
		}

		public static Type Parse(string value)
		{
			return new Type((TypeValue)Enum.Parse(typeof(TypeValue), value, true));
		}

		public static bool operator ==(Type left, Type right)
		{
			return left.Equals(right);
		}

		public static bool operator !=(Type left, Type right)
		{
			return !(left == right);
		}
	}
}
