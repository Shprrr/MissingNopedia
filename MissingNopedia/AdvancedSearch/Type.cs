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
			if (obj is Type)
				return Value == ((Type)obj).Value;
			if (obj is TypeValue)
				return Value == (TypeValue)obj;
			return base.Equals(obj);
		}

		public override int GetHashCode()
		{
			return Value.GetHashCode();
		}

		public Color BackColor()
		{
			switch (Value)
			{
				case TypeValue.Normal:
					return Color.FromArgb(unchecked((int)0xFF8a8a59));
				case TypeValue.Fire:
					return Color.FromArgb(unchecked((int)0xFFf08030));
				case TypeValue.Water:
					return Color.FromArgb(unchecked((int)0xFF6890f0));
				case TypeValue.Electric:
					return Color.FromArgb(unchecked((int)0xFFf8d030));
				case TypeValue.Grass:
					return Color.FromArgb(unchecked((int)0xFF78c850));
				case TypeValue.Ice:
					return Color.FromArgb(unchecked((int)0xFF98d8d8));
				case TypeValue.Fighting:
					return Color.FromArgb(unchecked((int)0xFFc03028));
				case TypeValue.Poison:
					return Color.FromArgb(unchecked((int)0xFFa040a0));
				case TypeValue.Ground:
					return Color.FromArgb(unchecked((int)0xFFe0c068));
				case TypeValue.Flying:
					return Color.FromArgb(unchecked((int)0xFFa890f0));
				case TypeValue.Psychic:
					return Color.FromArgb(unchecked((int)0xFFf85888));
				case TypeValue.Bug:
					return Color.FromArgb(unchecked((int)0xFFa8b820));
				case TypeValue.Rock:
					return Color.FromArgb(unchecked((int)0xFFb8a038));
				case TypeValue.Ghost:
					return Color.FromArgb(unchecked((int)0xFF705898));
				case TypeValue.Dragon:
					return Color.FromArgb(unchecked((int)0xFF7038f8));
				case TypeValue.Dark:
					return Color.FromArgb(unchecked((int)0xFF705848));
				case TypeValue.Steel:
					return Color.FromArgb(unchecked((int)0xFFb8b8d0));
				case TypeValue.Fairy:
					return Color.FromArgb(unchecked((int)0xFFe898e8));
			}
			return Color.White;
		}

		public static Type Parse(string value)
		{
			return new Type((TypeValue)Enum.Parse(typeof(TypeValue), value, true));
		}
	}
}
