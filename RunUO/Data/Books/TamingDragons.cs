//////////////////////////////////////////////
//
// Taming Dragons by Wyrd Beastmaster
//
// Created using Lokai's ReadBookFromTxt.cs
//
//////////////////////////////////////////////
using System;
using Server;

namespace Server.Items
{
	public class TamingDragons : BaseBook
	{
		public static readonly BookContent Content = new BookContent
			(
				"Taming Dragons", "Wyrd Beastmaster", 
				new BookPageInfo
				(
					"  I have not much to ",
					"tell about dragons. The",
					"sole time I approached ",
					"one with an eye ",
					"towards taming it, ",
					"my initial attempts at ",
					"calming it met with ",
					"failure. It fixed a "
				),
				new BookPageInfo
				(
					"massive beady eye ",
					"upon me, and began ",
					"its slithering ",
					"approach, intending no ",
					"doubt to insert me ",
					"into its maw and bear ",
					"down with its teeth.",
					"  However, as I was "
				),
				new BookPageInfo
				(
					"engaged in what ",
					"remains to this day ",
					"the most terrifying ",
					"combat of my life, ",
					"the dragon suddenly ",
					"whirled as if in a ",
					"panic, ran a short ",
					"distance, took off into"
				),
				new BookPageInfo
				(
					"the air, then ",
					"transformed into a ",
					"whirlwind. Lastly, it ",
					"exploded, showering ",
					"gouts of black blood ",
					"and heaving, stinking ",
					"flesh upon miles of ",
					"countryside. The "
				),
				new BookPageInfo
				(
					"fireball was massive, ",
					"enough to light a city,",
					"I should surmise.",
					"  I never did discover ",
					"the exact cause of ",
					"this strange behavior, ",
					"except to assume that ",
					"it was not typical for "
				),
				new BookPageInfo
				(
					"this reptilian species.",
					"My best guesses ",
					"revolve around a ",
					"magical fracture in ",
					"the nature of reality, ",
					"which is far too ",
					"esoteric a territory ",
					"for one of my limited "
				),
				new BookPageInfo
				(
					"scholarship.",
					"  Hence my basibasic ",
					"advice to those who ",
					"seek to tame a ",
					"dragon-be sure that ",
					"thou hast mastered ",
					"the twin skills of ",
					"taming animals, and "
				),
				new BookPageInfo
				(
					"running away very ",
					"very fast.",
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
		public TamingDragons() : base( Utility.Random( 0xFEF, 2 ), false )
		{
		}

		public TamingDragons( Serial serial ) : base( serial )
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