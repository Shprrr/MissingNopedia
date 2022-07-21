using System;
using System.Linq;
using System.Threading.Tasks;
using HtmlAgilityPack;
using MissingNopedia.AdvancedSearch;

namespace MissingNopedia
{
	public class PokemonHtml : DocumentHtml
	{
		public const string WikiPokemonSuffix = "_(Pok%C3%A9mon)";

		private Pokemon pokemonData;

		public string PokemonName { get; }
		public string GenerationLearnset { get; }
		public bool IsCustomPictures { get; }

		public PokemonHtml(string pokemonName, string generationLearnset, bool isCustomPictures)
		{
			PokemonName = pokemonName;
			GenerationLearnset = generationLearnset;
			IsCustomPictures = isCustomPictures;
		}

		public override async Task LoadAsync()
		{
			pokemonData = Array.Find(await AdvancedSearch.AdvancedSearch.LoadAsync(), p => p.Name.Replace('’', '\'') == PokemonName);
		}

		public override string BuildNewPage()
		{
			if (pokemonData is null) return "";
			doc = ConstructNewPage(PokemonName);

			doc.DocumentNode.SelectSingleNode("//head").AppendChild(HtmlNode.CreateNode(@"<style>
        html {
            font-family: 'Trebuchet MS', 'Lucida Sans Unicode', 'Lucida Grande', 'Lucida Sans', Arial, sans-serif;
            scroll-behavior: smooth;
        }

        body {
            position: relative;
            margin-bottom: 17rem;
        }

        .profile {
            max-width: 300px;
            float: right;
        }

        .img-sprite {
            width: 128px;
            height: 128px;
        }

        .img-fixed {
            display: inline-block;
            max-width: none;
            vertical-align: middle;
        }

        .type-icon {
            display: inline-block;
            width: 66px;
            margin-bottom: 4px;
            background: #dbdbdb;
            border-radius: 4px;
            border: 1px solid rgba(0,0,0,.2);
            color: #fff;
            font-size: .75rem;
            font-weight: normal;
            line-height: 1.5rem;
            text-align: center;
            text-shadow: 1px 1px 2px rgba(0,0,0,.7);
            text-transform: uppercase;
        }

        .type-normal {
            background-color: #aa9;
        }

        .type-fire {
            background-color: #f42;
        }

        .type-water {
            background-color: #39f;
        }

        .type-electric {
            background-color: #fc3;
        }

        .type-grass {
            background-color: #7c5;
        }

        .type-ice {
            background-color: #6cf;
        }

        .type-fighting {
            background-color: #b54;
        }

        .type-poison {
            background-color: #a59;
        }

        .type-ground {
            background-color: #db5;
        }

        .type-flying {
            background-color: #89f;
        }

        .type-psychic {
            background-color: #f59;
        }

        .type-bug {
            background-color: #ab2;
        }

        .type-rock {
            background-color: #ba6;
        }

        .type-ghost {
            background-color: #66b;
        }

        .type-dragon {
            background-color: #76e;
        }

        .type-dark {
            background-color: #754;
        }

        .type-steel {
            background-color: #aab;
        }

        .type-fairy {
            background-color: #e9e;
        }

        .type-curse {
            background-color: #698;
        }

        .itype {
            color: #737373;
        }

            .itype.normal {
                color: #797964;
            }

            .itype.fire {
                color: #d52100;
            }

            .itype.water {
                color: #0080ff;
            }

            .itype.electric {
                color: #c90;
            }

            .itype.grass {
                color: #5cb737;
            }

            .itype.ice {
                color: #0af;
            }

            .itype.fighting {
                color: #a84d3d;
            }

            .itype.poison {
                color: #88447a;
            }

            .itype.ground {
                color: #bf9926;
            }

            .itype.flying {
                color: #556dff;
            }

            .itype.psychic {
                color: #ff227a;
            }

            .itype.bug {
                color: #83901a;
            }

            .itype.rock {
                color: #a59249;
            }

            .itype.ghost {
                color: #5454b3;
            }

            .itype.dragon {
                color: #4e38e9;
            }

            .itype.dark {
                color: #573e31;
            }

            .itype.steel {
                color: #8e8ea4;
            }

            .itype.fairy {
                color: #e76de7;
            }

        .igame {
            white-space: nowrap;
        }

            .igame.red, .igame.ruby, .igame.firered, .igame.y, .igame.omega-ruby, .igame.shield {
                color: #c03028;
            }

            .igame.blue, .igame.sapphire, .igame.x, .igame.alpha-sapphire, .igame.sword {
                color: #5d81d6;
            }

            .igame.yellow, .igame.lets-go-pikachu {
                color: #d6b11f;
            }

            .igame.gold, .igame.heartgold {
                color: #ad9551;
            }

            .igame.silver, .igame.platinum, .igame.soulsilver, .igame.white, .igame.white-2 {
                color: #9797ab;
            }

            .igame.crystal {
                color: #87bfbf;
            }

            .igame.leafgreen, .igame.legends-arceus {
                color: #65a843;
            }

            .igame.emerald {
                color: #909e1b;
            }

            .igame.diamond, .igame.brilliant-diamond {
                color: #8471bd;
            }

            .igame.pearl, .igame.shining-pearl {
                color: #de4f7a;
            }

            .igame.black, .igame.black-2 {
                color: #574438;
            }

            .igame.sun, .igame.ultra-sun {
                color: #db8624;
            }

            .igame.moon, .igame.ultra-moon {
                color: #7038f8;
            }

            .igame.lets-go-eevee {
                color: #ac8639;
            }

        .tag {
            display: inline-block;
            box-sizing: border-box;
            width: 125px;
            height: 30px;
            line-height: 24px;
            border-radius: 48px;
            background: #000000;
            border: 1px solid #000000;
            margin: 3px 0;
            padding: 3px;
            text-align: center;
        }

            .tag span:first-child {
                display: inline-block;
                color: #FFF;
                font-weight: bold;
            }

            .tag span:last-child {
                display: inline-block;
                width: 24px;
                border-radius: 48px;
                background: #FFF;
                float: right;
            }

            .tag.none span:last-child {
                display: none;
            }

            .tag.normal {
                background-color: #A8A878;
                border: 1px solid #6D6D4E;
            }

            .tag.fire {
                background: #F08030;
                border: 1px solid #9C531F;
            }

            .tag.water {
                background: #6890F0;
                border: 1px solid #445E9C;
            }

            .tag.electric {
                background: #F8D030;
                border: 1px solid #A1871F;
            }

            .tag.grass {
                background: #78C850;
                border: 1px solid #4E8234;
            }

            .tag.ice {
                background: #98D8D8;
                border: 1px solid #638D8D;
            }

            .tag.fighting {
                background-color: #C03028;
                border: 1px solid #7D1F1A;
            }

            .tag.poison {
                background: #A040A0;
                border: 1px solid #682A68;
            }

            .tag.ground {
                background: #E0C068;
                border: 1px solid #927D44;
            }

            .tag.flying {
                background: #A890F0;
                border: 1px solid #6D5E9C;
            }

            .tag.psychic {
                background: #F85888;
                border: 1px solid #A13959;
            }

            .tag.bug {
                background: #A8B820;
                border: 1px solid #6D7815;
            }

            .tag.rock {
                background: #B8A038;
                border: 1px solid #786824;
            }

            .tag.ghost {
                background: #705898;
                border: 1px solid #493963;
            }

            .tag.dragon {
                background: #7038F8;
                border: 1px solid #4924A1;
            }

            .tag.dark {
                background: #705848;
                border: 1px solid #49392F;
            }

            .tag.steel {
                background: #B8B8D0;
                border: 1px solid #787887;
            }

            .tag.fairy {
                background: #EE99AC;
                border: 1px solid #9B6470;
            }

        .infocard-list-evo {
            display: flex;
            justify-content: center;
            align-items: center;
        }

            .infocard-list-evo > .infocard {
                width: 150px;
                vertical-align: middle;
            }

            .infocard-list-evo > .infocard-arrow {
                width: 165px;
            }

        .infocard {
            display: inline-block;
            padding: 0 .75rem 2rem;
            line-height: 1.25;
            text-align: center;
        }

        .infocard-evo-split {
            display: flex;
            flex-direction: column;
            justify-content: center;
        }

            .infocard-evo-split > .infocard-list-evo {
                justify-content: flex-start;
            }

        .icon-arrow {
            display: block;
            font: normal 2.5rem/1 ""Arial Unicode MS"",""Trebuchet MS"",""Arial"",""Helvetica"",sans-serif;
        }

        .icon-arrow-n::before {
            content: ""↑︎"";
        }

        .icon-arrow-e::before {
            content: ""→︎"";
        }

        .icon-arrow-s::before {
            content: ""↓︎"";
        }

        .icon-arrow-w::before {
            content: ""←︎"";
        }

        .icon-arrow-ne::before {
            content: ""↗︎"";
        }

        .icon-arrow-se::before {
            content: ""↘︎"";
        }

        .icon-arrow-sw::before {
            content: ""↙︎"";
        }

        .icon-arrow-nw::before {
            content: ""↖︎"";
        }

        .text-blue {
            color: #3273dc;
        }

        .text-pink {
            color: #ff6bce;
        }

        .text-muted {
            color: #737373;
        }

        nav {
            position: absolute;
            box-sizing: border-box;
            top: 0;
            right: 11rem;
            bottom: 0;
        }

            nav ul {
                position: fixed;
                top: -17rem;
                transform: translateY(100vh);
                border: 1px solid gray;
                border-radius: 4px;
                background-color: white;
                opacity: 0.8;
                padding: 10px;
                padding-inline-start: 28px;
            }

        table {
            border-collapse: collapse;
            border-spacing: 0;
        }

        th, td {
            border-width: 1px 0;
            border-style: solid;
            border-color: #f0f0f0;
            padding: 4px 10px;
        }

        thead th {
            background-color: #ebebe5;
            white-space: nowrap;
        }

        tbody th:first-child {
            text-align: right;
        }

        table.colored tbody tr {
            margin: 3px;
            border-radius: 10px;
            display: block;
            background: #F5AC78;
        }

        table.colored th, table.colored td {
            border: 0;
        }

        table.sprites-table th {
            text-align: center;
        }

        table.sprites-table td {
            text-align: center;
        }

            table.sprites-table td div {
                display: inline-block;
                vertical-align: middle;
            }
    </style>"));

			var main = doc.GetElementbyId("main");

			main.AppendChild(HtmlNode.CreateNode($"<h1>{PokemonName}</h1>"));
			main.AppendChild(HtmlNode.CreateNode($"<img src=\"https://img.pokemondb.net/artwork/large/{ImageName()}.jpg\" class=\"profile\" />"));

			main.AppendChild(HtmlNode.CreateNode("<h2>Pokédex data</h2>"));
			main.AppendChild(HtmlNode.CreateNode(@$"<table>
    <tbody>
        <tr>
            <th>National №</th>
            <td><strong>{pokemonData.Number:D3}</strong></td>
        </tr>
        <tr>
            <th>Type</th>
            <td><small class=""text-muted"">None</small></td>
        </tr>
        <tr>
            <th>Species</th>
            <td>{pokemonData.Species}</td>
        </tr>
        <tr>
            <th>Height</th>
            <td>{pokemonData.HeightInMeters:F1}&nbsp;m ({pokemonData.HeightInFeet}′{pokemonData.HeightInInches:D2}″)</td>
        </tr>
        <tr>
            <th>Weight</th>
            <td>{pokemonData.WeightInKilograms:F1}&nbsp;kg ({pokemonData.WeightInPounds:F1}&nbsp;lbs)</td>
        </tr>
        <tr>
            <th>Abilities</th>
            <td><small class=""text-muted"">None</small></td>
        </tr>
        <tr>
            <th>Local №</th>
            <td><small class=""text-muted"">None</small></td>
        </tr>
    </tbody>
</table>"));
			var types = string.Join(" ", pokemonData.Types.Select(t => $"<span class=\"type-icon type-{t.ToString().ToLower()}\">{t}</span>"));
			if (!string.IsNullOrEmpty(types))
				main.LastChild.SelectSingleNode("//tr[2]/td[1]").InnerHtml = types;

			if (pokemonData.Ability1 != null)
			{
				var abilitiesCell = main.LastChild.SelectSingleNode("//tr[6]/td[1]");
				abilitiesCell.InnerHtml = $"<span class=\"text-muted\">1. <a href=\"/{pokemonData.Ability1.Name}{AbilityHtml.WikiAbilitySuffix}\" title=\"{pokemonData.Ability1.Description.Last().Value}\">{pokemonData.Ability1.Name}</a></span>";

				if (pokemonData.Ability2 != null)
					abilitiesCell.InnerHtml += $"<br><span class=\"text-muted\">2. <a href=\"/{pokemonData.Ability2.Name}{AbilityHtml.WikiAbilitySuffix}\" title=\"{pokemonData.Ability2.Description.Last().Value}\">{pokemonData.Ability2.Name}</a></span>";

				if (pokemonData.HiddenAbility != null)
					abilitiesCell.InnerHtml += $"<br><small class=\"text-muted\"><a href=\"/{pokemonData.HiddenAbility.Name}{AbilityHtml.WikiAbilitySuffix}\" title=\"{pokemonData.HiddenAbility.Description.Last().Value}\">{pokemonData.HiddenAbility.Name}</a> (hidden ability)</small>";
			}

			var localDexNumbers = string.Join("<br>", pokemonData.PokedexNumbers.Where(pn => pn.Key != "national").Select(pn => $"{pn.Value:D3} <small class=\"text-muted\">({ToPokedexName(pn.Key)})</small>"));
			if (!string.IsNullOrEmpty(localDexNumbers))
				main.LastChild.SelectSingleNode("//tr[7]/td[1]").InnerHtml = localDexNumbers;

			main.AppendChild(HtmlNode.CreateNode("<h2>Pokédex entries</h2>"));
			main.AppendChild(HtmlNode.CreateNode(@"<table>
    <tbody>
    </tbody>
</table>"));
			var tbody = main.LastChild.SelectSingleNode("//tbody");
			foreach (var pokedexEntry in pokemonData.PokedexEntries.GroupBy(pe => pe.Value, pe => pe.Key))
			{
				var verions = string.Join("<br>", pokedexEntry.Select(v => $"<span class=\"igame {v}\">{ToVersionName(v)}</span>"));
				tbody.AppendChild(HtmlNode.CreateNode(@$"<tr>
            <th>{verions}</th>
            <td>{pokedexEntry.Key}</td>
        </tr>"));
			}

			return doc.DocumentNode.OuterHtml;
		}

		private string ImageName() => PokemonName.ToLower()
			.Replace("♀", "-f").Replace("♂", "-m").Replace("'", "").Replace(". ", "-").Replace(" ", "-").Replace(".", "").Replace(":", "");

		private static string ToVersionName(string version)
		{
			return version switch
			{
				"red" => "Red",
				"blue" => "Blue",
				"yellow" => "Yellow",
				"gold" => "Gold",
				"silver" => "Silver",
				"crystal" => "Crystal",
				"ruby" => "Ruby",
				"sapphire" => "Sapphire",
				"emerald" => "Emerald",
				"firered" => "FireRed",
				"leafgreen" => "LeafGreen",
				"diamond" => "Diamond",
				"pearl" => "Pearl",
				"platinum" => "Platinum",
				"heartgold" => "HeartGold",
				"soulsilver" => "SoulSilver",
				"black" => "Black",
				"white" => "White",
				"black-2" => "Black 2",
				"white-2" => "White 2",
				"x" => "X",
				"y" => "Y",
				"omega-ruby" => "Omega Ruby",
				"alpha-sapphire" => "Alpha Sapphire",
				"sun" => "Sun",
				"moon" => "Moon",
				"ultra-sun" => "Ultra Sun",
				"ultra-moon" => "Ultra Moon",
				"lets-go-pikachu" => "Let's Go Pikachu",
				"lets-go-eevee" => "Let's Go Eevee",
				"sword" => "Sword",
				"shield" => "Shield",
				"brillant-diamond" => "Brillant Diamond",
				"shining-pearl" => "Shining Pearl",
				"legends-arceus" => "Legends: Arceus",
				_ => version
			};
		}

		private static string ToPokedexName(string pokedexName)
		{
			return pokedexName switch
			{
				"national" => "National",
				"kanto" => "Red/Blue/Yellow/FireRed/LeafGreen",
				"original-johto" => "Gold/Silver/Crystal",
				"hoenn" => "Ruby/Sapphire/Emerald",
				"original-sinnoh" => "Diamond/Pearl/Brilliant Diamond/Shining Pearl",
				"extended-sinnoh" => "Platinum",
				"updated-johto" => "HeartGold/SoulSilver",
				"original-unova" => "Black/White",
				"updated-unova" => "Black 2/White 2",
				"kalos-central" => "X/Y — Central Kalos",
				"kalos-coastal" => "X/Y — Coastal Kalos",
				"kalos-mountain" => "X/Y — Mountain Kalos",
				"updated-hoenn" => "Omega Ruby/Alpha Sapphire",
				"original-alola" => "Sun/Moon — Alola",
				"original-melemele" => "Sun/Moon — Melemele",
				"original-akala" => "Sun/Moon — Akala",
				"original-ulaula" => "Sun/Moon — Ula'ula",
				"original-poni" => "Sun/Moon — Poni",
				"updated-alola" => "Ultra Sun/Ultra Moon — Alola",
				"updated-melemele" => "Ultra Sun/Ultra Moon — Melemele",
				"updated-akala" => "Ultra Sun/Ultra Moon — Akala",
				"updated-ulaula" => "Ultra Sun/Ultra Moon — Ula'ula",
				"updated-poni" => "Ultra Sun/Ultra Moon — Poni",
				"letsgo-kanto" => "Let’s Go: Pikachu/Let’s Go: Eevee",
				"galar" => "Sword/Shield",
				"isle-of-armor" => "Isle of Armor",
				"crown-tundra" => "Crown Tundra",
				_ => ""
			};
		}
	}
}
