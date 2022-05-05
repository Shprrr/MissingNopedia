using System.Collections.Generic;
using System.Windows.Forms;

namespace MissingNopedia
{
	public partial class frmOptions : Form
	{
		private readonly Options options = new();

		private static readonly KeyValuePair<string, OptionGenerations>[] generations = new KeyValuePair<string, OptionGenerations>[]
		{
			new("Latest", OptionGenerations.Latest),
			new("VII Sun & Moon", OptionGenerations.Generation7),
			new("VI X & Y", OptionGenerations.Generation6),
			new("V Black & White", OptionGenerations.Generation5),
			new("IV Diamond & Pearl", OptionGenerations.Generation4),
			new("III Ruby & Sapphire", OptionGenerations.Generation3),
			new("II Gold & Silver", OptionGenerations.Generation2),
			new("I Red & Green/Blue", OptionGenerations.Generation1)
		};

		public frmOptions(Options options)
		{
			InitializeComponent();

			rbOfficialPictures.Checked = options.PokemonProfilePictures == OptionPokemonProfilePictures.Official;
			rbCustomPictures.Checked = options.PokemonProfilePictures == OptionPokemonProfilePictures.Custom;
			cboDefaultGeneration.DataSource = generations;
			cboDefaultGeneration.DisplayMember = nameof(KeyValuePair<string, OptionGenerations>.Key);
			cboDefaultGeneration.ValueMember = nameof(KeyValuePair<string, OptionGenerations>.Value);
			cboDefaultGeneration.SelectedValue = options.DefaultGeneration;
		}

		public Options Options
		{
			get
			{
				if (rbOfficialPictures.Checked) options.PokemonProfilePictures = OptionPokemonProfilePictures.Official;
				if (rbCustomPictures.Checked) options.PokemonProfilePictures = OptionPokemonProfilePictures.Custom;
				options.DefaultGeneration = (OptionGenerations)cboDefaultGeneration.SelectedValue;
				return options;
			}
		}
	}
}
