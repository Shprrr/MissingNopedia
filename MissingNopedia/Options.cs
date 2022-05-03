namespace MissingNopedia
{
	public class Options
	{
		public OptionPokemonProfilePictures PokemonProfilePictures { get; set; } = OptionPokemonProfilePictures.Custom;
	}

	public enum OptionPokemonProfilePictures
	{
		Official,
		Custom
	}
}
