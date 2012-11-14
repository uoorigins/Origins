//////////////////////////////////////////////
//
// The Rankings of Trades by Lord Higginbotham
//
// Created using Lokai's ReadBookFromTxt.cs
//
//////////////////////////////////////////////
using System;
using Server;

namespace Server.Items
{
	public class TheRankingsofTrades : BaseBook
	{
		public static readonly BookContent Content = new BookContent
			(
				"The Rankings of Trades", "Lord Higginbotham", 
				new BookPageInfo
				(
					"  Whilst 'tis true ",
					"that within each ",
					"trade, one finds ",
					"differing titles ",
					"and accolades granted ",
					"to the members of a ",
					"given guild, ",
					"nonetheless for the "
				),
				new BookPageInfo
				(
					"betterment of trade ",
					"and understanding, ",
					"we must have a ",
					"commonality of ",
					"titling.",
					"  For those who may ",
					"find themselves ",
					"ignorant of the finer "
				),
				new BookPageInfo
				(
					"distinctions between a ",
					"three-knot member of ",
					"the Sailors' Maritime ",
					"Association and a ",
					"second thaumaturge, ",
					"this book shall serve ",
					"as a simple ",
					"introduction to the "
				),
				new BookPageInfo
				(
					"common cant used ",
					"when members of ",
					"differing guilds and ",
					"trade organizations ",
					"must trade with each ",
					"other and must ",
					"establish relative ",
					"credentials."
				),
				new BookPageInfo
				(
					"NEOPHYTE",
					"",
					"Has shown interest ",
					"in learning the craft ",
					"and some meager ",
					"talent.",
					"",
					""
				),
				new BookPageInfo
				(
					"NOVICE",
					"",
					"Is practicing basic ",
					"skills but has not been",
					"admitted to full ",
					"standing.",
					"",
					""
				),
				new BookPageInfo
				(
					"APPRENTICE",
					"",
					"A student of the ",
					"discipline.",
					"",
					"",
					"",
					""
				),
				new BookPageInfo
				(
					"JOURNEYMAN",
					"",
					"Warranted to practice ",
					"the discipline under ",
					"the eyes of a tutor.",
					"",
					"",
					""
				),
				new BookPageInfo
				(
					"EXPERT",
					"",
					"A full member of ",
					"the guild.",
					"",
					"",
					"",
					""
				),
				new BookPageInfo
				(
					"ADEPT",
					"",
					"A member of the ",
					"guild qualified to ",
					"teach others.",
					"",
					"",
					""
				),
				new BookPageInfo
				(
					"MASTER",
					"",
					"Acknowledged as ",
					"qualified to lead a ",
					"hall or business.",
					"",
					"",
					""
				),
				new BookPageInfo
				(
					"GRANDMASTER",
					"",
					"Rarely a permanent ",
					"title, granted in ",
					"common parlance to ",
					"those who have ",
					"shown extreme ",
					"mastery of their "
				),
				new BookPageInfo
				(
					"craft recently.",
					"",
					"",
					"",
					"",
					"",
					"",
					""
				)
			);

		public override BookContent DefaultContent{ get{ return Content; } }

		[Constructable]
		public TheRankingsofTrades() : base( Utility.Random( 0xFEF, 2 ), false )
		{
		}

		public TheRankingsofTrades( Serial serial ) : base( serial )
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