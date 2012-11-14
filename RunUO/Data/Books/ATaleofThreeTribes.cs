//////////////////////////////////////////////
//
// A Tale of Three Tribes by Janet, Scribe
//
// Created using Lokai's ReadBookFromTxt.cs
//
//////////////////////////////////////////////
using System;
using Server;

namespace Server.Items
{
	public class ATaleofThreeTribes : BaseBook
	{
		public static readonly BookContent Content = new BookContent
			(
				"A Tale of Three Tribes", "Janet, Scribe", 
				new BookPageInfo
				(
					"  The dungeon known ",
					"as Despise is in fact ",
					"not a dungeon as ",
					"such, but rather a ",
					"large natural cave. ",
					"Inhospitable and ",
					"unfriendly to ",
					"visitors, it is filled "
				),
				new BookPageInfo
				(
					"with damp spots ",
					"where the deadly ",
					"Exploding Red Spotted ",
					"Toadstool grows in ",
					"abundance.",
					"  According to the ",
					"oldest of historical ",
					"texts, in days gone "
				),
				new BookPageInfo
				(
					"by the cave was once ",
					"the home of three ",
					"separate tribes who ",
					"had come to an ",
					"accommodation with ",
					"each other. Oddly ",
					"enough, the three ",
					"tribes were of "
				),
				new BookPageInfo
				(
					"dragons, lizard men, ",
					"and rat men. While ",
					"today few except ",
					"extremists associated ",
					"with Lord Blackthorn ",
					"regard these latter ",
					"two as being ",
					"intelligent beings, "
				),
				new BookPageInfo
				(
					"apparently they have ",
					"indeed fallen from a ",
					"more evolved state ",
					"over the years.",
					"  'Tis said that these ",
					"three races did dwell ",
					"in relative harmony ",
					"within the vast cave, "
				),
				new BookPageInfo
				(
					"building when they ",
					"required it, and ",
					"trading amongst ",
					"themselves if needed.",
					"  But over time, ",
					"something happened, ",
					"and they were forced ",
					"to withdraw from "
				),
				new BookPageInfo
				(
					"their society, until ",
					"today thou mayst ",
					"find individuals of ",
					"each species within ",
					"the dungeon, but ",
					"never again as a ",
					"civilization.",
					"  'Tis also said that "
				),
				new BookPageInfo
				(
					"someday the three ",
					"tribes may return to ",
					"Despise, to once again ",
					"inhabit it together.",
					"  Until then, nothing ",
					"remains as token of ",
					"this save an oddly ",
					"intelligent skeleton, "
				),
				new BookPageInfo
				(
					"magically enchanted, ",
					"that doth speak when ",
					"questions are asked, ",
					"and from whom I ",
					"obtained these tales ",
					"one day, when I was ",
					"pursued by evil ",
					"monsters and fled "
				),
				new BookPageInfo
				(
					"into his skeletal arms.",
					"  Fortunately, I ",
					"escaped and lived to ",
					"write it all down!",
					"",
					"",
					"",
					""
				)
			);

		public override BookContent DefaultContent{ get{ return Content; } }

		[Constructable]
		public ATaleofThreeTribes() : base( Utility.Random( 0xFEF, 2 ), false )
		{
		}

		public ATaleofThreeTribes( Serial serial ) : base( serial )
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