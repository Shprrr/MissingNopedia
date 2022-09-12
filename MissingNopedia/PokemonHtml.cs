using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HtmlAgilityPack;
using MissingNopedia.AdvancedSearch;

namespace MissingNopedia
{
	public class PokemonHtml : DocumentHtml
	{
		public const string WikiPokemonSuffix = "_(Pok%C3%A9mon)";

		private const int MaxIv = 31;
		private const int MaxEv = 254;
		private const int EggCycleSteps = 256;
		private Pokemon pokemonData;
		private int minimumHP;
		private int maximumHP;
		private int minimumAttack;
		private int maximumAttack;
		private int minimumDefense;
		private int maximumDefense;
		private int minimumSpAttack;
		private int maximumSpAttack;
		private int minimumSpDefense;
		private int maximumSpDefense;
		private int minimumSpeed;
		private int maximumSpeed;

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
			Pokemon[] pokemons = await AdvancedSearch.AdvancedSearch.LoadAsync();
			pokemonData = Array.Find(pokemons, p => p.Name.Replace('’', '\'') == PokemonName);
			if (pokemonData is null) return;
			Pokemon[] pokemonsWithForms = pokemons.SelectMany(p => p.GetForms()).ToArray();
			minimumHP = pokemonsWithForms.Min(p => p.BaseHP);
			maximumHP = pokemonsWithForms.Max(p => p.BaseHP);
			minimumAttack = pokemonsWithForms.Min(p => p.BaseAttack);
			maximumAttack = pokemonsWithForms.Max(p => p.BaseAttack);
			minimumDefense = pokemonsWithForms.Min(p => p.BaseDefense);
			maximumDefense = pokemonsWithForms.Max(p => p.BaseDefense);
			minimumSpAttack = pokemonsWithForms.Min(p => p.BaseSpAttack);
			maximumSpAttack = pokemonsWithForms.Max(p => p.BaseSpAttack);
			minimumSpDefense = pokemonsWithForms.Min(p => p.BaseSpDefense);
			maximumSpDefense = pokemonsWithForms.Max(p => p.BaseSpDefense);
			minimumSpeed = pokemonsWithForms.Min(p => p.BaseSpeed);
			maximumSpeed = pokemonsWithForms.Max(p => p.BaseSpeed);
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

        .cell-barchart {
            width: 100%;
            min-width: 150px
        }

        .barchart-bar {
            height: .75rem;
            border-radius: 4px;
            background-color: #a3a3a3;
            border: 1px solid #737373;
            border-color: rgba(0,0,0,.15)
        }

        .barchart-rank-1 {
            background-color: #f34444
        }

        .barchart-rank-2 {
            background-color: #ff7f0f
        }

        .barchart-rank-3 {
            background-color: #ffdd57
        }

        .barchart-rank-4 {
            background-color: #a0e515
        }

        .barchart-rank-5 {
            background-color: #23cd5e
        }

        .barchart-rank-6 {
            background-color: #00c2b8
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

        table.colored {
            width: 100%;
            border: 3px solid;
        }

            table.colored tbody tr {
                margin: 3px;
                border-radius: 10px;
                display: block;
            }

            table.colored th, table.colored td {
                border: 0;
            }

            table.colored.normal1 {
                background: #A8A878;
            }

            table.colored.normal2 {
                border-color: #6D6D4E;
            }

            table.colored.normal1 tbody tr {
                background: #C6C6A7;
            }

            table.colored.fire1 {
                background: #F08030;
            }

            table.colored.fire2 {
                border-color: #9C531F;
            }

            table.colored.fire1 tbody tr {
                background: #F5AC78;
            }

            table.colored.water1 {
                background: #6890F0;
            }

            table.colored.water2 {
                border-color: #445E9C;
            }

            table.colored.water1 tbody tr {
                background: #9DB7F5;
            }

            table.colored.electric1 {
                background: #F8D030;
            }

            table.colored.electric2 {
                border-color: #A1871F;
            }

            table.colored.electric1 tbody tr {
                background: #FAE078;
            }

            table.colored.grass1 {
                background: #78C850;
            }

            table.colored.grass2 {
                border-color: #4E8234;
            }

            table.colored.grass1 tbody tr {
                background: #A7DB8D;
            }

            table.colored.ice1 {
                background: #98D8D8;
            }

            table.colored.ice2 {
                border-color: #638D8D;
            }

            table.colored.ice1 tbody tr {
                background: #BCE6E6;
            }

            table.colored.fighting1 {
                background: #C03028;
            }

            table.colored.fighting2 {
                border-color: #7D1F1A;
            }

            table.colored.fighting1 tbody tr {
                background: #D67873;
            }

            table.colored.poison1 {
                background: #A040A0;
            }

            table.colored.poison2 {
                border-color: #682A68;
            }

            table.colored.poison1 tbody tr {
                background: #C183C1;
            }

            table.colored.ground1 {
                background: #E0C068;
            }

            table.colored.ground2 {
                border-color: #927D44;
            }

            table.colored.ground1 tbody tr {
                background: #EBD69D;
            }

            table.colored.flying1 {
                background: #A890F0;
            }

            table.colored.flying2 {
                border-color: #6D5E9C;
            }

            table.colored.flying1 tbody tr {
                background: #C6B7F5;
            }

            table.colored.psychic1 {
                background: #F85888;
            }

            table.colored.psychic2 {
                border-color: #A13959;
            }

            table.colored.psychic1 tbody tr {
                background: #FA92B2;
            }

            table.colored.bug1 {
                background: #A8B820;
            }

            table.colored.bug2 {
                border-color: #6D7815;
            }

            table.colored.bug1 tbody tr {
                background: #C6D16E;
            }

            table.colored.rock1 {
                background: #B8A038;
            }

            table.colored.rock2 {
                border-color: #786824;
            }

            table.colored.rock1 tbody tr {
                background: #D1C17D;
            }

            table.colored.ghost1 {
                background: #705898;
            }

            table.colored.ghost2 {
                border-color: #493963;
            }

            table.colored.ghost1 tbody tr {
                background: #A292BC;
            }

            table.colored.dragon1 {
                background: #7038F8;
            }

            table.colored.dragon2 {
                border-color: #4924A1;
            }

            table.colored.dragon1 tbody tr {
                background: #A27DFA;
            }

            table.colored.dark1 {
                background: #705848;
            }

            table.colored.dark2 {
                border-color: #49392F;
            }

            table.colored.dark1 tbody tr {
                background: #A29288;
            }

            table.colored.steel1 {
                background: #B8B8D0;
            }

            table.colored.steel2 {
                border-color: #787887;
            }

            table.colored.steel1 tbody tr {
                background: #D1D1E0;
            }

            table.colored.fairy1 {
                background: #EE99AC;
            }

            table.colored.fairy2 {
                border-color: #9B6470;
            }

            table.colored.fairy1 tbody tr {
                background: #F4BDC9;
            }

            table.colored.curse1 {
                background: #68A090;
            }

            table.colored.curse2 {
                border-color: #44685E;
            }

            table.colored.curse1 tbody tr {
                background: #9DC1B7;
            }

        table.sprites-table th, table.sprites-table td {
            text-align: center;
        }

            table.sprites-table td div {
                display: inline-block;
                vertical-align: middle;
            }
    </style>"));

			var main = doc.GetElementbyId("main");

			main.AppendChild(HtmlNode.CreateNode($"<h1>{PokemonName}</h1>"));

			main.AppendChild(HtmlNode.CreateNode("<div style=\"float: right;\"></div>"));
			main.LastChild.AppendChild(HtmlNode.CreateNode($"<img src=\"https://img.pokemondb.net/artwork/large/{ImageName()}.jpg\" class=\"profile\" />"));
			AddTraining(main.LastChild);
			AddBreeding(main.LastChild);

			AddPokedexData(main);

			AddBaseStats(main);

			AddTypeDefenses(main);

			AddEvolutionChart(main);

			AddPokedexEntries(main);

			return doc.DocumentNode.OuterHtml;
		}

		private string ImageName() => ImageName(PokemonName);
		private static string ImageName(string pokemonName) => pokemonName.ToLower()
			.Replace("♀", "-f").Replace("♂", "-m").Replace("'", "").Replace(". ", "-").Replace(" ", "-").Replace(".", "").Replace(":", "");

		private void AddPokedexData(HtmlNode parentNode)
		{
			parentNode.AppendChild(HtmlNode.CreateNode("<h2 id=\"pokedexData\">Pokédex data</h2>"));
			parentNode.AppendChild(HtmlNode.CreateNode(@$"<table>
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
				parentNode.LastChild.SelectSingleNode("//tr[2]/td[1]").InnerHtml = types;

			if (pokemonData.Ability1 != null)
			{
				var abilitiesCell = parentNode.LastChild.SelectSingleNode("//tr[6]/td[1]");
				abilitiesCell.InnerHtml = $"<span class=\"text-muted\">1. <a href=\"/{pokemonData.Ability1.Name}{AbilityHtml.WikiAbilitySuffix}\" title=\"{pokemonData.Ability1.Description.Last().Value}\">{pokemonData.Ability1.Name}</a></span>";

				if (pokemonData.Ability2 != null)
					abilitiesCell.InnerHtml += $"<br><span class=\"text-muted\">2. <a href=\"/{pokemonData.Ability2.Name}{AbilityHtml.WikiAbilitySuffix}\" title=\"{pokemonData.Ability2.Description.Last().Value}\">{pokemonData.Ability2.Name}</a></span>";

				if (pokemonData.HiddenAbility != null)
					abilitiesCell.InnerHtml += $"<br><small class=\"text-muted\"><a href=\"/{pokemonData.HiddenAbility.Name}{AbilityHtml.WikiAbilitySuffix}\" title=\"{pokemonData.HiddenAbility.Description.Last().Value}\">{pokemonData.HiddenAbility.Name}</a> (hidden ability)</small>";
			}

			var localDexNumbers = string.Join("<br>", pokemonData.PokedexNumbers.Where(pn => pn.Key != "national").Select(pn => $"{pn.Value:D3} <small class=\"text-muted\">({ToPokedexName(pn.Key)})</small>"));
			if (!string.IsNullOrEmpty(localDexNumbers))
				parentNode.LastChild.SelectSingleNode("//tr[7]/td[1]").InnerHtml = localDexNumbers;
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
				"hisui" => "Legends: Arceus",
				_ => ""
			};
		}

		private void AddTraining(HtmlNode parentNode)
		{
			parentNode.AppendChild(HtmlNode.CreateNode("<h2>Training</h2>"));
			parentNode.AppendChild(HtmlNode.CreateNode(@$"<table>
    <tbody>
        <tr>
            <th>EV yield</th>
            <td>{string.Join(", ", pokemonData.EffortValues)}</td>
        </tr>
        <tr>
            <th>Catch rate</th>
            <td>{pokemonData.CaptureRate} <small class=""text-muted"">({CaptureRateWithPokeballFullHP(pokemonData.Number, pokemonData.CaptureRate):F3}% with PokéBall, full HP)</small></td>
        </tr>
        <tr>
            <th>Base Friendship</th>
            <td>—</td>
        </tr>
        <tr>
            <th>Base Exp.</th>
            <td>{pokemonData.BaseExperience?.ToString() ?? "—"}</td>
        </tr>
        <tr>
            <th>Growth Rate</th>
            <td>{ToGrowthRateName(pokemonData.GrowthRate)}</td>
        </tr>
    </tbody>
</table>"));

			var baseFriendshipCell = parentNode.LastChild.SelectSingleNode("//tr[3]/td[1]");
			if (pokemonData.BaseHappiness.HasValue)
				baseFriendshipCell.InnerHtml = $"{pokemonData.BaseHappiness} <small class=\"text-muted\">({(pokemonData.BaseHappiness > 70 ? "higher than " : pokemonData.BaseHappiness < 50 ? "lower than " : "")}normal)</small>";
		}

		/// <summary>
		/// Source : https://www.dragonflycave.com/calculators/gen-viii-catch-rate
		/// </summary>
		/// <param name="pokemonNumber"></param>
		/// <param name="captureRate"></param>
		/// <returns></returns>
		private static double CaptureRateWithPokeballFullHP(int pokemonNumber, int captureRate)
		{
			var rate = captureRate / 3d;
			if (IsUltraBeast(pokemonNumber))
				rate *= 410d / 4096;

			var wobble = Math.Floor(RoundToNearest4096th(65536 / RoundToNearest4096th(Math.Pow(RoundToNearest4096th(255 / rate), 3d / 16))));
			var wobbleChance = wobble / 65536;
			if (wobbleChance > 1) wobbleChance = 1;

			return Math.Pow(wobbleChance, 4) * 100;
		}
		private static double RoundToNearest4096th(double a) => Math.Round(a * 4096) / 4096;
		private static bool IsUltraBeast(int pokemonNumber) => pokemonNumber >= 793 && pokemonNumber <= 799 || pokemonNumber >= 803 && pokemonNumber <= 806;
		private static string ToGrowthRateName(string growthRate)
		{
			return growthRate switch
			{
				"slow" => "Slow",
				"medium" => "Medium Fast",
				"fast" => "Fast",
				"medium-slow" => "Medium Slow",
				"slow-then-very-fast" => "Erratic",
				"fast-then-very-slow" => "Fluctuating",
				_ => ""
			};
		}

		private void AddBreeding(HtmlNode parentNode)
		{
			parentNode.AppendChild(HtmlNode.CreateNode("<h2>Breeding</h2>"));
			parentNode.AppendChild(HtmlNode.CreateNode(@$"<table>
    <tbody>
        <tr>
            <th>Egg Groups</th>
            <td><small class=""text-muted"">None</small></td>
        </tr>
        <tr>
            <th>Gender</th>
            <td>{AddGenderRate()}</td>
        </tr>
        <tr>
            <th>Egg cycles</th>
            <td><small class=""text-muted"">None</small></td>
        </tr>
    </tbody>
</table>"));

			var eggGroups = string.Join(", ", pokemonData.EggGroups.Select(e => $"<a href=\"/{e}{EggGroupHtml.WikiEggGroupSuffix}\">{ToEggGroupeName(e)}</a>"));
			if (!string.IsNullOrEmpty(eggGroups))
				parentNode.LastChild.SelectSingleNode("//tr[1]/td[1]").InnerHtml = eggGroups;

			if (pokemonData.EggCycle.HasValue)
				parentNode.LastChild.SelectSingleNode("//tr[3]/td[1]").InnerHtml = $"{pokemonData.EggCycle} <small class=\"text-muted\">({(pokemonData.EggCycle - 1) * EggCycleSteps:N0}–{pokemonData.EggCycle * EggCycleSteps:N0} steps)</small>";
		}

		private static string ToEggGroupeName(string eggGroup)
		{
			return eggGroup switch
			{
				"monster" => "Monster",
				"water1" => "Water 1",
				"bug" => "Bug",
				"flying" => "Flying",
				"ground" => "Field",
				"fairy" => "Fairy",
				"plant" => "Grass",
				"humanshape" => "Human-Like",
				"water3" => "Water 3",
				"mineral" => "Mineral",
				"indeterminate" => "Amorphous",
				"water2" => "Water 2",
				"ditto" => "Ditto",
				"dragon" => "Dragon",
				"no-eggs" => "Undiscovered",
				_ => ""
			};
		}
		private string AddGenderRate()
		{
			return pokemonData.GenderRate switch
			{
				GenderRate.Unknown => "Genderless",
				GenderRate.Male or GenderRate.Male7To1 or GenderRate.Male3To1 or GenderRate.Half or GenderRate.Female3To1 or GenderRate.Female7To1 or GenderRate.Female => $"<span class=\"text-blue\">{pokemonData.MaleGenderRate:0.##%} male</span>, <span class=\"text-pink\">{pokemonData.FemaleGenderRate:0.##%} female</span>",
				_ => throw new NotImplementedException()
			};
		}

		private void AddBaseStats(HtmlNode parentNode)
		{
			parentNode.AppendChild(HtmlNode.CreateNode("<h2 id=\"baseStats\">Base stats</h2>"));
			parentNode.AppendChild(HtmlNode.CreateNode(FormattableString.Invariant(@$"<table>
    <thead>
        <tr><td colspan=""3"" rowspan=""2""></td><th colspan=""2"">At Lv. 50</th><th colspan=""2"">At Lv. 100</th></tr>
        <tr>
            <th>Min</th>
            <th>Max</th>
            <th>Min</th>
            <th>Max</th>
        </tr>
    </thead>
    <tbody>
        <tr>
            <th>HP</th>
            <td>{pokemonData.BaseHP}</td>
            <td class=""cell-barchart"">
                <div style=""width:{CalculateWidth(pokemonData.BaseHP, minimumHP, maximumHP)}%;"" class=""barchart-bar barchart-rank-{CalculateRank(pokemonData.BaseHP, minimumHP, maximumHP)}""></div>
            </td>
            <td>{CalculateHPStat(pokemonData.BaseHP, 50, 0, 0)}</td>
            <td>{CalculateHPStat(pokemonData.BaseHP, 50, MaxIv, MaxEv)}</td>
            <td>{CalculateHPStat(pokemonData.BaseHP, 100, 0, 0)}</td>
            <td>{CalculateHPStat(pokemonData.BaseHP, 100, MaxIv, MaxEv)}</td>
        </tr>
        <tr>
            <th>Attack</th>
            <td>{pokemonData.BaseAttack}</td>
            <td class=""cell-barchart"">
                <div style=""width:{CalculateWidth(pokemonData.BaseAttack, minimumAttack, maximumAttack)}%;"" class=""barchart-bar barchart-rank-{CalculateRank(pokemonData.BaseAttack, minimumAttack, maximumAttack)}""></div>
            </td>
            <td>{CalculateOtherStat(pokemonData.BaseAttack, 50, 0, 0, NatureBoosting.Hindering)}</td>
            <td>{CalculateOtherStat(pokemonData.BaseAttack, 50, MaxIv, MaxEv, NatureBoosting.Beneficial)}</td>
            <td>{CalculateOtherStat(pokemonData.BaseAttack, 100, 0, 0, NatureBoosting.Hindering)}</td>
            <td>{CalculateOtherStat(pokemonData.BaseAttack, 100, MaxIv, MaxEv, NatureBoosting.Beneficial)}</td>
        </tr>
        <tr>
            <th>Defense</th>
            <td>{pokemonData.BaseDefense}</td>
            <td class=""cell-barchart"">
                <div style=""width:{CalculateWidth(pokemonData.BaseDefense, minimumDefense, maximumDefense)}%;"" class=""barchart-bar barchart-rank-{CalculateRank(pokemonData.BaseDefense, minimumDefense, maximumDefense)}""></div>
            </td>
            <td>{CalculateOtherStat(pokemonData.BaseDefense, 50, 0, 0, NatureBoosting.Hindering)}</td>
            <td>{CalculateOtherStat(pokemonData.BaseDefense, 50, MaxIv, MaxEv, NatureBoosting.Beneficial)}</td>
            <td>{CalculateOtherStat(pokemonData.BaseDefense, 100, 0, 0, NatureBoosting.Hindering)}</td>
            <td>{CalculateOtherStat(pokemonData.BaseDefense, 100, MaxIv, MaxEv, NatureBoosting.Beneficial)}</td>
        </tr>
        <tr>
            <th>Sp. Atk</th>
            <td>{pokemonData.BaseSpAttack}</td>
            <td class=""cell-barchart"">
                <div style=""width:{CalculateWidth(pokemonData.BaseSpAttack, minimumSpAttack, maximumSpAttack)}%;"" class=""barchart-bar barchart-rank-{CalculateRank(pokemonData.BaseSpAttack, minimumSpAttack, maximumSpAttack)}""></div>
            </td>
            <td>{CalculateOtherStat(pokemonData.BaseSpAttack, 50, 0, 0, NatureBoosting.Hindering)}</td>
            <td>{CalculateOtherStat(pokemonData.BaseSpAttack, 50, MaxIv, MaxEv, NatureBoosting.Beneficial)}</td>
            <td>{CalculateOtherStat(pokemonData.BaseSpAttack, 100, 0, 0, NatureBoosting.Hindering)}</td>
            <td>{CalculateOtherStat(pokemonData.BaseSpAttack, 100, MaxIv, MaxEv, NatureBoosting.Beneficial)}</td>
        </tr>
        <tr>
            <th>Sp. Def</th>
            <td>{pokemonData.BaseSpDefense}</td>
            <td class=""cell-barchart"">
                <div style=""width:{CalculateWidth(pokemonData.BaseSpDefense, minimumSpDefense, maximumSpDefense)}%;"" class=""barchart-bar barchart-rank-{CalculateRank(pokemonData.BaseSpDefense, minimumSpDefense, maximumSpDefense)}""></div>
            </td>
            <td>{CalculateOtherStat(pokemonData.BaseSpDefense, 50, 0, 0, NatureBoosting.Hindering)}</td>
            <td>{CalculateOtherStat(pokemonData.BaseSpDefense, 50, MaxIv, MaxEv, NatureBoosting.Beneficial)}</td>
            <td>{CalculateOtherStat(pokemonData.BaseSpDefense, 100, 0, 0, NatureBoosting.Hindering)}</td>
            <td>{CalculateOtherStat(pokemonData.BaseSpDefense, 100, MaxIv, MaxEv, NatureBoosting.Beneficial)}</td>
        </tr>
        <tr>
            <th>Speed</th>
            <td>{pokemonData.BaseSpeed}</td>
            <td class=""cell-barchart"">
                <div style=""width:{CalculateWidth(pokemonData.BaseSpeed, minimumSpeed, maximumSpeed)}%;"" class=""barchart-bar barchart-rank-{CalculateRank(pokemonData.BaseSpeed, minimumSpeed, maximumSpeed)}""></div>
            </td>
            <td>{CalculateOtherStat(pokemonData.BaseSpeed, 50, 0, 0, NatureBoosting.Hindering)}</td>
            <td>{CalculateOtherStat(pokemonData.BaseSpeed, 50, MaxIv, MaxEv, NatureBoosting.Beneficial)}</td>
            <td>{CalculateOtherStat(pokemonData.BaseSpeed, 100, 0, 0, NatureBoosting.Hindering)}</td>
            <td>{CalculateOtherStat(pokemonData.BaseSpeed, 100, MaxIv, MaxEv, NatureBoosting.Beneficial)}</td>
        </tr>
    </tbody>
    <tfoot>
        <tr>
            <th rowspan=""2"">Total</th>
            <td rowspan=""2""><b>{pokemonData.BaseHP + pokemonData.BaseAttack + pokemonData.BaseDefense + pokemonData.BaseSpAttack + pokemonData.BaseSpDefense + pokemonData.BaseSpeed}</b></td>
            <th colspan=""5""></th>
        </tr>
    </tfoot>
</table>")));
			parentNode.AppendChild(HtmlNode.CreateNode("<p class=\"text-muted\">Maximum values are based on a beneficial nature, 252 EVs, 31 IVs; minimum values are based on a hindering nature, 0 EVs, 0 IVs.</p>"));
		}

		private static float CalculateWidth(int stat, int minimum, int maximum) => 100f * (stat - minimum) / (maximum - minimum);
		private static int CalculateRank(int stat, int minimum, int maximum) => (int)(6f * (stat - minimum) / (maximum - minimum + 1) + 1);
		private static int CalculateHPStat(int baseStat, int level, int iv, int ev)
		{
			if (baseStat == 1) return 1;
			return (int)((2 * baseStat + iv + (int)(ev / 4d)) * level / 100d) + level + 10;
		}
		private static int CalculateOtherStat(int baseStat, int level, int iv, int ev, NatureBoosting natureBoosting)
		{
			return (int)(((int)((2 * baseStat + iv + (int)(ev / 4d)) * level / 100d) + 5) * NatureMultiplier(natureBoosting));
		}
		private enum NatureBoosting
		{
			Neutral,
			Hindering,
			Beneficial
		}
		private static float NatureMultiplier(NatureBoosting natureBoosting) => natureBoosting switch
		{
			NatureBoosting.Beneficial => 1.1f,
			NatureBoosting.Hindering => 0.9f,
			_ => 1
		};

		private void AddTypeDefenses(HtmlNode parentNode)
		{
			parentNode.AppendChild(HtmlNode.CreateNode("<h2 id=\"typeDefenses\">Type defenses</h2>"));
			parentNode.AppendChild(HtmlNode.CreateNode(@$"<table class=""colored {pokemonData.Type1.ToString().ToLower()}1 {pokemonData.Types.Last().ToString().ToLower()}2"">
    <thead>
        <tr valign=""top"">
            <td>
                Under normal battle conditions in Generation VIII, this Pokémon is:
            </td>
        </tr>
    </thead>
    <tbody>
        <tr>
            <th style=""width: 100px;"">
                Damaged<br>normally by:
            </th>
            <td>
                <span class=""tag none"">
                    <span>None</span>
                    <span></span>
                </span>
            </td>
        </tr>
        <tr>
            <th style=""width:100px;"">
                Weak to:
            </th>
            <td>
                <span class=""tag none"">
                    <span>None</span>
                    <span></span>
                </span>
            </td>
        </tr>
        <tr>
            <th style=""width:100px;"">
                Immune to:
            </th>
            <td>
                <span class=""tag none"">
                    <span>None</span>
                    <span></span>
                </span>
            </td>
        </tr>
        <tr>
            <th style=""width:100px;"">
                Resistant to:
            </th>
            <td>
                <span class=""tag none"">
                    <span>None</span>
                    <span></span>
                </span>
            </td>
        </tr>
    </tbody>
</table>"));

			HtmlNode typeListNode;
			if (pokemonData.TypeEfficacies.Any(t => t.DamageMultiplier == 1))
			{
				typeListNode = parentNode.LastChild.SelectSingleNode("//tbody/tr[1]/td[1]");
				typeListNode.InnerHtml = string.Join(' ', pokemonData.TypeEfficacies.Where(t => t.DamageMultiplier == 1).Select(t => @$"<span class=""tag {t.DamageType.ToString().ToLower()}"">
                    <span>{t.DamageType}</span>
                    <span>1×</span>
                </span>"));
			}

			if (pokemonData.TypeEfficacies.Any(t => t.DamageMultiplier > 1))
			{
				typeListNode = parentNode.LastChild.SelectSingleNode("//tbody/tr[2]/td[1]");
				typeListNode.InnerHtml = string.Join(' ', pokemonData.TypeEfficacies.Where(t => t.DamageMultiplier > 1).OrderByDescending(t => t.DamageMultiplier).Select(t => @$"<span class=""tag {t.DamageType.ToString().ToLower()}"">
                    <span>{t.DamageType}</span>
                    <span>{t.DamageMultiplier}×</span>
                </span>"));
			}

			if (pokemonData.TypeEfficacies.Any(t => t.DamageMultiplier == 0))
			{
				typeListNode = parentNode.LastChild.SelectSingleNode("//tbody/tr[3]/td[1]");
				typeListNode.InnerHtml = string.Join(' ', pokemonData.TypeEfficacies.Where(t => t.DamageMultiplier == 0).Select(t => @$"<span class=""tag {t.DamageType.ToString().ToLower()}"">
                    <span>{t.DamageType}</span>
                    <span>0×</span>
                </span>"));
			}

			if (pokemonData.TypeEfficacies.Any(t => t.DamageMultiplier < 1 && t.DamageMultiplier > 0))
			{
				typeListNode = parentNode.LastChild.SelectSingleNode("//tbody/tr[4]/td[1]");
				typeListNode.InnerHtml = string.Join(' ', pokemonData.TypeEfficacies.Where(t => t.DamageMultiplier < 1 && t.DamageMultiplier > 0).OrderBy(t => t.DamageMultiplier).Select(t => @$"<span class=""tag {t.DamageType.ToString().ToLower()}"">
                    <span>{t.DamageType}</span>
                    <span>{t.DamageMultiplier switch { 0.5f => "½", 0.25f => "¼", _ => t.DamageMultiplier.ToString() }}×</span>
                </span>"));
			}
		}

		private void AddEvolutionChart(HtmlNode parentNode)
		{
			parentNode.AppendChild(HtmlNode.CreateNode("<h2 id=\"evolutionChart\">Evolution chart</h2>"));
			parentNode.AppendChild(HtmlNode.CreateNode(@"<div class=""infocard-list-evo"">
</div>"));

			var evolutions = parentNode.LastChild;
			AddEvolvesFrom(evolutions, pokemonData);
			AddEvolutionCard(evolutions, pokemonData);
			AddEvolvesTo(evolutions, pokemonData);

			void AddEvolvesFrom(HtmlNode evolutions, Pokemon pokemonEvolution)
			{
				// We don't show the exact value so the first one is enough.
				foreach (var evolution in pokemonEvolution.EvolvesFrom.Where((e, i) => !e.MinBeauty.HasValue || i == 0))
				{
					AddEvolvesFrom(evolutions, evolution.Pokemon);
					AddEvolutionCard(evolutions, evolution.Pokemon);
					AddEvolutionMethod(evolutions, evolution);
				}
			}

			void AddEvolvesTo(HtmlNode evolutions, Pokemon pokemonEvolution)
			{
				// We don't show the exact value so the first one is enough.
				foreach (var evolution in pokemonEvolution.EvolvesTo.Where((e, i) => !e.MinBeauty.HasValue || i == 0))
				{
					AddEvolutionMethod(evolutions, evolution);
					AddEvolutionCard(evolutions, evolution.Pokemon);
					AddEvolvesTo(evolutions, evolution.Pokemon);
				}
			}
		}

		private static void AddEvolutionCard(HtmlNode evolutions, Pokemon pokemonEvolution)
		{
			evolutions.AppendChild(HtmlNode.CreateNode(@$"<div class=""infocard"">
        <a href=""/{pokemonEvolution.Name}{WikiPokemonSuffix}""><img class=""img-fixed img-sprite"" src=""https://img.pokemondb.net/sprites/home/normal/{ImageName(pokemonEvolution.Name)}.png"" alt=""{pokemonEvolution.Name} sprite""></a>
        <span class=""text-muted"">
            <small>#{pokemonEvolution.Number:D3}</small><br>
            <a class=""ent-name"" href=""/{pokemonEvolution.Name}{WikiPokemonSuffix}"">{pokemonEvolution.Name}</a><br>
            <small>None</small>
        </span>
    </div>"));

			var types = string.Join(" · ", pokemonEvolution.Types.Select(t => $"<span class=\"itype {t.ToString().ToLower()}\">{t}</span>"));
			if (!string.IsNullOrEmpty(types))
				evolutions.LastChild.SelectSingleNode("//small[2]").InnerHtml = types;
		}

		private static void AddEvolutionMethod(HtmlNode evolutions, Pokemon.PokemonEvolution evolution)
		{
			List<string> texts = new();
			if (evolution.MinLevel.HasValue)
				texts.Add($"Level {evolution.MinLevel}");
			if (evolution.MinHappiness.HasValue)
				texts.Add("high Friendship");
			if (evolution.TimeOfDay.HasValue)
				texts.Add(evolution.TimeOfDay switch
				{
					Pokemon.PokemonEvolution.TimeDay.Day => "Daytime",
					Pokemon.PokemonEvolution.TimeDay.Night => "Nighttime",
					Pokemon.PokemonEvolution.TimeDay.Dusk => "Dusk 5-6PM",
					_ => throw new NotImplementedException()
				});
			if (evolution.RelativePhysicalStats.HasValue)
				texts.Add(evolution.RelativePhysicalStats switch
				{
					Pokemon.PokemonEvolution.RelativeStats.AttackEqualsDefense => "Attack = Defense",
					Pokemon.PokemonEvolution.RelativeStats.AttackGreaterDefense => "Attack > Defense",
					Pokemon.PokemonEvolution.RelativeStats.DefenseGreaterAttack => "Attack < Defense",
					_ => throw new NotImplementedException()
				});
			if (evolution.MinBeauty.HasValue)
				texts.Add("level up with max Beauty");
			if (!string.IsNullOrEmpty(evolution.LocationName))
				if (evolution.LocationName.Contains("Rock"))
					texts.Add($"level up near a {evolution.LocationName}");
				else if (string.IsNullOrEmpty(evolution.RegionName))
					texts.Add($"level up in a {evolution.LocationName} area");
				else
					texts.Add($"level up at {evolution.LocationName}, {evolution.RegionName}");
			if (evolution.EvolutionTrigger == Pokemon.PokemonEvolution.Trigger.LevelUp && !string.IsNullOrEmpty(evolution.HeldItemName))
				texts.Add($"hold {evolution.HeldItemName}");
			if (evolution.EvolutionTrigger == Pokemon.PokemonEvolution.Trigger.Trade)
				if (string.IsNullOrEmpty(evolution.HeldItemName))
					texts.Add("Trade");
				else
					texts.Add($"trade holding {evolution.HeldItemName}");
			if (evolution.EvolutionTrigger == Pokemon.PokemonEvolution.Trigger.UseItem)
				texts.Add($"use {evolution.EvolutionItemName}");
			if (evolution.EvolutionTrigger == Pokemon.PokemonEvolution.Trigger.Shed)
				texts.Add("while evolving, empty spot in party, Pokéball in bag");


			string text = "";
			if (texts.Count > 0)
				text = $"<small>({string.Join(", ", texts)})</small>";

			evolutions.AppendChild(HtmlNode.CreateNode($"<span class=\"infocard infocard-arrow\"><i class=\"icon-arrow icon-arrow-e\"></i>{text}</span>"));
		}

		private void AddPokedexEntries(HtmlNode parentNode)
		{
			parentNode.AppendChild(HtmlNode.CreateNode("<h2 id=\"pokedexEntries\">Pokédex entries</h2>"));
			parentNode.AppendChild(HtmlNode.CreateNode(@"<table>
    <tbody>
    </tbody>
</table>"));

			var tbody = parentNode.LastChild.SelectSingleNode("//tbody");
			foreach (var pokedexEntry in pokemonData.PokedexEntries.GroupBy(pe => pe.Value, pe => pe.Key))
			{
				var verions = string.Join("<br>", pokedexEntry.Select(v => $"<span class=\"igame {v}\">{ToVersionName(v)}</span>"));
				tbody.AppendChild(HtmlNode.CreateNode(@$"<tr>
            <th>{verions}</th>
            <td>{pokedexEntry.Key}</td>
        </tr>"));
			}
		}

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
	}
}
