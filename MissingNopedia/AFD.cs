using System.Collections.Generic;
using System.IO;
using System.Reflection;
using CsvHelper;

namespace MissingNopedia
{
	public class AFD
	{
		private static readonly Dictionary<string, string> pokemonAFDArt = new();

		static AFD()
		{
			using var resourceStream = Assembly.GetExecutingAssembly().GetManifestResourceStream("MissingNopedia.afd_data.csv");
			using StreamReader afdStream = new(resourceStream);
			using CsvReader csvReader = new(afdStream, System.Globalization.CultureInfo.InvariantCulture);

			bool headers = false;
			while (csvReader.Read())
			{
				if (!headers)
				{
					csvReader.ReadHeader();
					headers = true;
					continue;
				}

				pokemonAFDArt.Add(NormalizeName(csvReader["Pokemon"]), csvReader["Art"]);
			}
		}

		public static bool HasAFD(string name) => pokemonAFDArt.ContainsKey(NormalizeName(name));

		public static string GetImageUrl(string name) => pokemonAFDArt[NormalizeName(name)];

		/// <summary>
		/// Remove the bug with those strings that don't match.
		/// </summary>
		/// <param name="name"></param>
		/// <returns></returns>
		private static string NormalizeName(string name)
		{
			return name switch
			{
				"Nidoran♀️" => "NidoranF",
				"Nidoran♀" => "NidoranF",
				"Nidoran♂️" => "NidoranM",
				"Nidoran♂" => "NidoranM",
				_ => name,
			};
		}
	}
}
