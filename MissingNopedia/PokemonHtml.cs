﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HtmlAgilityPack;
using MissingNopedia.AdvancedSearch;
using Type = MissingNopedia.AdvancedSearch.Type;

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
        .profile {
            max-width: 300px;
            float: right;
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
            background-color: #aa9
        }

        .type-fire {
            background-color: #f42;
        }

        .type-water {
            background-color: #39f
        }

        .type-electric {
            background-color: #fc3
        }

        .type-grass {
            background-color: #7c5
        }

        .type-ice {
            background-color: #6cf
        }

        .type-fighting {
            background-color: #b54
        }

        .type-poison {
            background-color: #a59
        }

        .type-ground {
            background-color: #db5
        }

        .type-flying {
            background-color: #89f
        }

        .type-psychic {
            background-color: #f59
        }

        .type-bug {
            background-color: #ab2
        }

        .type-rock {
            background-color: #ba6
        }

        .type-ghost {
            background-color: #66b
        }

        .type-dragon {
            background-color: #76e
        }

        .type-dark {
            background-color: #754
        }

        .type-steel {
            background-color: #aab
        }

        .type-fairy {
            background-color: #e9e
        }

        .type-curse {
            background-color: #698
        }

        .itype {
            color: #737373
        }

            .itype.normal {
                color: #797964
            }

            .itype.fire {
                color: #d52100;
            }

            .itype.water {
                color: #0080ff
            }

            .itype.electric {
                color: #c90
            }

            .itype.grass {
                color: #5cb737
            }

            .itype.ice {
                color: #0af
            }

            .itype.fighting {
                color: #a84d3d
            }

            .itype.poison {
                color: #88447a
            }

            .itype.ground {
                color: #bf9926
            }

            .itype.flying {
                color: #556dff
            }

            .itype.psychic {
                color: #ff227a
            }

            .itype.bug {
                color: #83901a
            }

            .itype.rock {
                color: #a59249
            }

            .itype.ghost {
                color: #5454b3
            }

            .itype.dragon {
                color: #4e38e9
            }

            .itype.dark {
                color: #573e31
            }

            .itype.steel {
                color: #8e8ea4
            }

            .itype.fairy {
                color: #e76de7
            }

        .igame.red, .igame.ruby, .igame.firered, .igame.y, .igame.omega-ruby, .igame.shield {
            color: #c03028
        }

        .igame.blue, .igame.sapphire, .igame.x, .igame.alpha-sapphire, .igame.sword {
            color: #5d81d6
        }

        .igame.yellow, .igame.lets-go-pikachu {
            color: #d6b11f
        }

        .igame.gold, .igame.heartgold {
            color: #ad9551
        }

        .igame.silver, .igame.platinum, .igame.soulsilver, .igame.white, .igame.white-2 {
            color: #9797ab
        }

        .igame.crystal {
            color: #87bfbf
        }

        .igame.leafgreen, .igame.legends-arceus {
            color: #65a843
        }

        .igame.emerald {
            color: #909e1b
        }

        .igame.diamond, .igame.brilliant-diamond {
            color: #8471bd
        }

        .igame.pearl, .igame.shining-pearl {
            color: #de4f7a
        }

        .igame.black, .igame.black-2 {
            color: #574438
        }

        .igame.sun, .igame.ultra-sun {
            color: #db8624
        }

        .igame.moon, .igame.ultra-moon {
            color: #7038f8
        }

        .igame.lets-go-eevee {
            color: #ac8639
        }

        .text-blue {
            color: #3273dc
        }

        .text-pink {
            color: #ff6bce
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
            <td></td>
        </tr>
    </tbody>
</table>"));
			List<Type> listTypes = new()
			{
				pokemonData.Type1
			};
			if (pokemonData.Type2.HasValue)
				listTypes.Add(pokemonData.Type2.Value);
			main.LastChild.SelectSingleNode("//tr[2]/td[1]")
				.InnerHtml = string.Join(" ", listTypes.Select(t => $"<span class=\"type-icon type-{t.ToString().ToLower()}\">{t}</span>"));

			return doc.DocumentNode.OuterHtml;
		}

		private string ImageName() => PokemonName.ToLower()
			.Replace("♀", "-f").Replace("♂", "-m").Replace("'", "").Replace(". ", "-").Replace(" ", "-").Replace(".", "").Replace(":", "");
	}
}
