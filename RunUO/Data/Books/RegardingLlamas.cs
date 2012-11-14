//////////////////////////////////////////////
//
// Regarding Llamas by Simon Sanmartine
//
// Created using Lokai's ReadBookFromTxt.cs
//
//////////////////////////////////////////////
using System;
using Server;

namespace Server.Items
{
	public class RegardingLlamas : BaseBook
	{
		public static readonly BookContent Content = new BookContent
			(
				"Regarding Llamas", "Simon Sanmartine", 
				new BookPageInfo
				(
					"  Llamas are curious ",
					"beasts, shaggy and ",
					"sought after for ",
					"their wool, yet of a ",
					"curiously arrogant ",
					"disposition reflected ",
					"in their eyes. They ",
					"live in mountainous "
				),
				new BookPageInfo
				(
					"areas, though who ",
					"may have first tamed ",
					"them is lost in the ",
					"mists of history.",
					"  'Tis a well-known ",
					"fact that llamas can ",
					"indeed be tamed, and ",
					"used as grazing "
				),
				new BookPageInfo
				(
					"animals, for their ",
					"meat, and of course ",
					"for their wool. Yet ",
					"'tis lesser known ",
					"that their ornery ",
					"disposition and ",
					"tendency to spit at ",
					"those they dislike "
				),
				new BookPageInfo
				(
					"makes them appealing ",
					"guard creatures as ",
					"well, though they ",
					"have little sound with ",
					"which to sound an ",
					"alarum.",
					"",
					""
				)
			);

		public override BookContent DefaultContent{ get{ return Content; } }

		[Constructable]
		public RegardingLlamas() : base( Utility.Random( 0xFEF, 2 ), false )
		{
		}

		public RegardingLlamas( Serial serial ) : base( serial )
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