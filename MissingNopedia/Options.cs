namespace MissingNopedia
{
	public class Options
	{
		public OptionPokemonProfilePictures PokemonProfilePictures { get; set; } = OptionPokemonProfilePictures.Custom;
		public OptionGenerations DefaultGeneration { get; set; } = OptionGenerations.Latest;
	}

	public enum OptionPokemonProfilePictures
	{
		Official,
		Custom
	}

	public enum OptionGenerations
	{
		Generation1,
		Generation2,
		Generation3,
		Generation4,
		Generation5,
		Generation6,
		Generation7,
		Generation8,
		Latest = Generation8
	}
}
