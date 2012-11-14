//////////////////////////////////////////////
//
// A Song of Samlethe by Sandra
//
// Created using Lokai's ReadBookFromTxt.cs
//
//////////////////////////////////////////////
using System;
using Server;

namespace Server.Items
{
	public class ASongofSamlethe : BaseBook
	{
		public static readonly BookContent Content = new BookContent
			(
				"A Song of Samlethe", "Sandra", 
				new BookPageInfo
				(
					"The first bear did ",
					"   swim by day,",
					"And it did sleep by ",
					"   night.",
					"It kept itself within ",
					"   its cave",
					"and ate by starry ",
					"   light."
				),
				new BookPageInfo
				(
					"The second bear it did ",
					"   cavort",
					"'Neath canopies of ",
					"   trees,",
					"And danced its odd ",
					"   bearish sort",
					"Of joy for all to see.",
					"The first bear, well, "
				),
				new BookPageInfo
				(
					"   'twas hunted,",
					"And today adorns a ",
					"   floor.",
					"Its ruggish face has ",
					"   been dented",
					"By footfalls and the ",
					"   door.",
					"The second bear did "
				),
				new BookPageInfo
				(
					"   step once",
					"Into a mushroom ring,",
					"And now does dance ",
					"   the dunce",
					"For wisps and ",
					"   unseen things.",
					"",
					"So do not dance, and "
				),
				new BookPageInfo
				(
					"   do not sleep,",
					"Or else be led astray!",
					"For bears all end up ",
					"   six feet deep",
					"At the end of ",
					"   Samlethe's day.",
					"",
					""
				)
			);

		public override BookContent DefaultContent{ get{ return Content; } }

		[Constructable]
		public ASongofSamlethe() : base( Utility.Random( 0xFEF, 2 ), false )
		{
		}

		public ASongofSamlethe( Serial serial ) : base( serial )
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