//////////////////////////////////////////////
//
// Beltran's Guide to Guilds by Beltran
//
// Created using Lokai's ReadBookFromTxt.cs
//
//////////////////////////////////////////////
using System;
using Server;

namespace Server.Items
{
	public class BeltransGuidetoGuilds : BaseBook
	{
		public static readonly BookContent Content = new BookContent
			(
				"Beltran's Guide to Guilds", "Beltran", 
				new BookPageInfo
				(
					"  This reference ",
					"work is intended ",
					"merely to serve as ",
					"resource for those ",
					"curious as to the full ",
					"range of trades and ",
					"societies extant in ",
					"Britannia and nearby "
				),
				new BookPageInfo
				(
					"nations. For each ",
					"trade or guild, their ",
					"blazon is given.",
					"",
					"",
					"",
					"",
					""
				),
				new BookPageInfo
				(
					"  Armourer's Guild. ",
					"Gold bar above black ",
					"bar.",
					"",
					"  Association of ",
					"Warriors. Blue cross ",
					"on a red field.",
					""
				),
				new BookPageInfo
				(
					"  Barters' Guild. ",
					"Green and white ",
					"stripes, diagonal.",
					"",
					"  Blacksmith's Guild. ",
					"Gold alongside black.",
					"",
					""
				),
				new BookPageInfo
				(
					" Federation of Rogues ",
					"and Beggars. Red ",
					"above black.",
					"",
					"  Fighters and ",
					"Footmen. Blue ",
					"horizontal bar on red ",
					"field."
				),
				new BookPageInfo
				(
					"  Guild of Archers. ",
					"A gold swath parting ",
					"red and blue.",
					"",
					"  Guild of ",
					"Armaments. Swath of ",
					"gold on black field, ",
					"gold accents."
				),
				new BookPageInfo
				(
					"  Guild of Assassins. ",
					"Black and red ",
					"quartered.",
					"",
					"  Guild of Barbers.  ",
					"Red and white ",
					"stripes.",
					""
				),
				new BookPageInfo
				(
					"  Guild of Cavalry and ",
					"Horse. Vertical blue ",
					"on a red field.",
					"",
					"  Guild of ",
					"Fishermen. Blue and ",
					"white, quartered.",
					""
				),
				new BookPageInfo
				(
					"  Guild of Mages. ",
					"Purple and blue, in a ",
					"crossed pennant ",
					"pattern.",
					"",
					"  Guild of ",
					"Provisioners. White ",
					"bar above green bar."
				),
				new BookPageInfo
				(
					" Guild of Sorcery. A ",
					"field divided ",
					"diagonally in blue and ",
					"purple.",
					"",
					"",
					"",
					""
				),
				new BookPageInfo
				(
					"  Healers Guild. Gold ",
					"swath dividing green ",
					"from purple, gold ",
					"accents.",
					"",
					"",
					"",
					""
				),
				new BookPageInfo
				(
					"  Lord British's ",
					"Healers of Virtue. ",
					"Golden ankh on dark ",
					"green.",
					"",
					"  Masters of Illusion. ",
					"Blue and purple ",
					"checkers."
				),
				new BookPageInfo
				(
					"  Merchants' Guild. ",
					"Gold coins on green ",
					"field.",
					"",
					"  Mining Cooperative. ",
					"A gold cross, ",
					"quartering blue and ",
					"black."
				),
				new BookPageInfo
				(
					"  Order of Engineers. ",
					"Purple, gold, and blue ",
					"vertical.",
					"",
					"  Sailors' Maritime ",
					"Association. A white ",
					"bar centered on a blue ",
					"field."
				),
				new BookPageInfo
				(
					"  Seamen's Chapter. ",
					"Blue and white in a ",
					"crossed pennant ",
					"pattern.",
					"",
					"",
					"",
					""
				),
				new BookPageInfo
				(
					"  Society of Cooks and ",
					"Chefs. White and red ",
					"diagonal fields ",
					"checker on green ",
					"field.",
					"",
					"",
					""
				),
				new BookPageInfo
				(
					"  Society of ",
					"Shipwrights. White ",
					"diagonal above blue.",
					"",
					"  Society of Thieves. ",
					"Black and red ",
					"diagonal stripes.",
					""
				),
				new BookPageInfo
				(
					"  Society of ",
					"Weaponsmakers. Gold ",
					"diagonal above black.",
					"",
					"  Tailor's Hall. Purple",
					"above gold above red.",
					"",
					" "
				),
				new BookPageInfo
				(
					" The Bardic ",
					"Collegium. Purple and ",
					"red checkers on gold ",
					"field.",
					"",
					"  Traders' Guild. ",
					"White bar centered ",
					"down green field."
				)
			);

		public override BookContent DefaultContent{ get{ return Content; } }

		[Constructable]
		public BeltransGuidetoGuilds() : base( Utility.Random( 0xFEF, 2 ), false )
		{
		}

		public BeltransGuidetoGuilds( Serial serial ) : base( serial )
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