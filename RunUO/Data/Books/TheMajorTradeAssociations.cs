//////////////////////////////////////////////
//
// The Major Trade Associations by Pieter of Vesper
//
// Created using Lokai's ReadBookFromTxt.cs
//
//////////////////////////////////////////////
using System;
using Server;

namespace Server.Items
{
	public class TheMajorTradeAssociations : BaseBook
	{
		public static readonly BookContent Content = new BookContent
			(
				"The Major Trade Associations", "Pieter of Vesper", 
				new BookPageInfo
				(
					"  There are ten ",
					"major trade ",
					"associations that ",
					"operate legitimately ",
					"in the lands of ",
					"Britannia and among ",
					"its trading partners. ",
					"Many of these "
				),
				new BookPageInfo
				(
					"guilds are divided ",
					"into local or specialty",
					"subguilds, who use ",
					"the same colors but ",
					"vary the heraldic ",
					"pattern.  There are ",
					"many lesser trade ",
					"associations that have "
				),
				new BookPageInfo
				(
					"closed membership, ",
					"and one can join them ",
					"only by invitation. ",
					"\"Beltran's Guide to ",
					"Guilds\" is the ",
					"definitive text on the ",
					"full range of guilds ",
					"and other "
				),
				new BookPageInfo
				(
					"associations in ",
					"Britannia, and I ",
					"heartily recommend ",
					"it.",
					"  In what follows I ",
					"have attempted to ",
					"bring together the ",
					"known information "
				),
				new BookPageInfo
				(
					"regarding these ",
					"guilds. I offer thee ",
					"the name, typical ",
					"membership, heraldic ",
					"colors, known ",
					"specialty ",
					"organizations within ",
					"the larger guild, and "
				),
				new BookPageInfo
				(
					"any known ",
					"affiliations to other ",
					"guilds, which often ",
					"occur because of ",
					"trade reasons.",
					"",
					"",
					""
				),
				new BookPageInfo
				(
					"League of Rangers",
					"--------------",
					"Members: ",
					"  rangers, bowyers, ",
					"  animal trainers",
					"Colors: Red, gold ",
					"  and blue",
					""
				),
				new BookPageInfo
				(
					"          The Guild of",
					"--------------- ",
					"Members: ",
					"  alchemists and ",
					"  wizards",
					"Colors: blue and purple",
					" ",
					""
				),
				new BookPageInfo
				(
					"Arcane Arts",
					"---------------",
					"Subguilds:",
					"  Illusionists, Mages, ",
					"  Wizards",
					"Affiliations:   ",
					"  Healer's Guild",
					""
				),
				new BookPageInfo
				(
					"        The Warrior's",
					"---------------",
					"Members:    ",
					"  mercenaries,  ",
					"  soldiery, ",
					"  guardsmen, ",
					"  weapons masters, ",
					"  paladins."
				),
				new BookPageInfo
				(
					"Guild",
					"--------------",
					"Colors: Blue and red",
					"Subguilds: ",
					"  Cavalry, Fighters, ",
					"  Warriors",
					"Affiliations: League ",
					"  of Rangers"
				),
				new BookPageInfo
				(
					"            Merchants' ",
					"---------------",
					"Members: ",
					"  innkeepers, ",
					"  taverners,jewelers, ",
					"  provisioners",
					"Colors: gold coins on a",
					"  green field for "
				),
				new BookPageInfo
				(
					"Association",
					"--------------",
					"  Merchants.  White ",
					"  & green for others.",
					"Subguilds: Barters, ",
					"  Provisioners, ",
					"  Traders, ",
					"  Merchants"
				),
				new BookPageInfo
				(
					"Guild of Healers",
					"---------------",
					"Members: healers",
					"Colors: Green, gold, ",
					"  and purple",
					"Affiliations: Guild of ",
					"  Arcane Arts",
					""
				),
				new BookPageInfo
				(
					"Mining Cooperative",
					"---------------",
					"Members: miners",
					"Colors: blue and black ",
					"  checkers, with a ",
					"  gold cross",
					"Affiliations: Order ",
					"  of Engineers"
				),
				new BookPageInfo
				(
					"Order of Engineers",
					"---------------",
					"Members: tinkers and ",
					"  engineers",
					"Colors: Blue, gold, and",
					"  purple vertical bars",
					"Affiliations: Mining ",
					"  Cooperative"
				),
				new BookPageInfo
				(
					"Society of Clothiers",
					"---------------",
					"Members: tailors and ",
					"  weavers",
					"Colors: Purple, gold, ",
					"  and red horizontal ",
					"  bars",
					""
				),
				new BookPageInfo
				(
					"             Maritime",
					"---------------",
					"Members: fishermen, ",
					"  sailors, mapmakers, ",
					"  shipwrights",
					"Colors: blue and white",
					"",
					""
				),
				new BookPageInfo
				(
					"Guild",
					"---------------",
					"Subguilds: ",
					"  Fishermen, Sailors, ",
					"  Shipwrights",
					"",
					"",
					""
				),
				new BookPageInfo
				(
					"Bardic Collegium",
					"---------------",
					"Members: bards, ",
					"  musicians, ",
					"  storytellers",
					"Colors: Purple, red ",
					"  and gold ",
					"  checkerboard"
				),
				new BookPageInfo
				(
					"  In addition to these ",
					"aboveboard guilds, ",
					"there is one other ",
					"covert organization ",
					"well known to exist, ",
					"whose membership ",
					"is likewise open to ",
					"those who seek to "
				),
				new BookPageInfo
				(
					"apply. In some places ",
					"where illegal",
					"activities ",
					"are condoned more ",
					"openly, they dare post ",
					"their sigils publicly.",
					"  No law-abiding ",
					"citizen would ever "
				),
				new BookPageInfo
				(
					"join a guild such as ",
					"this, of course! Yet ",
					"their existence must ",
					"be acknowledges of ",
					"the sake of ",
					"completeness.",
					"",
					""
				),
				new BookPageInfo
				(
					"            Society of",
					"--------------",
					"Members: ",
					"  beggars, cutpurses, ",
					"  assassins, ",
					"  and brigands",
					"Colors: red and black",
					""
				),
				new BookPageInfo
				(
					"Thieves",
					"---------------",
					"Subguilds: Rogues ",
					"  (beggars), ",
					"  Assassins, Thieves",
					"",
					"",
					""
				)
			);

		public override BookContent DefaultContent{ get{ return Content; } }

		[Constructable]
		public TheMajorTradeAssociations() : base( Utility.Random( 0xFEF, 2 ), false )
		{
		}

		public TheMajorTradeAssociations( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.WriteEncodedInt( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadEncodedInt();
		}
	}
}