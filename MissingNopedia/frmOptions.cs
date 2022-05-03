using System.Windows.Forms;

namespace MissingNopedia
{
	public partial class frmOptions : Form
	{
		private readonly Options options = new();

		public frmOptions(Options options)
		{
			InitializeComponent();

			rbOfficialPictures.Checked = options.PokemonProfilePictures == OptionPokemonProfilePictures.Official;
			rbCustomPictures.Checked = options.PokemonProfilePictures == OptionPokemonProfilePictures.Custom;
		}

		public Options Options
		{
			get
			{
				if (rbOfficialPictures.Checked) options.PokemonProfilePictures = OptionPokemonProfilePictures.Official;
				if (rbCustomPictures.Checked) options.PokemonProfilePictures = OptionPokemonProfilePictures.Custom;
				return options;
			}
		}
	}
}
