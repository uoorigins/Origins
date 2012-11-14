//////////////////////////////////////////////
//
// The Bold Stranger by Fabio the Poor
//
// Created using Lokai's ReadBookFromTxt.cs
//
//////////////////////////////////////////////
using System;
using Server;

namespace Server.Items
{
	public class TheBoldStranger : BaseBook
	{
		public static readonly BookContent Content = new BookContent
			(
				"The Bold Stranger", "Fabio the Poor", 
				new BookPageInfo
				(
					"    In a time before ",
					"time, the Gods that ",
					"Be assembled a group ",
					"of artisans, ",
					"craftsmen and lore ",
					"masters (for, yes, ",
					"even in those days, ",
					"art existed) to create "
				),
				new BookPageInfo
				(
					"the world of Sosaria. ",
					"To this group, the ",
					"gods gave a tiny ",
					"world, Rytabul, in ",
					"which to test their ",
					"works, to see if they ",
					"were of the quality ",
					"desired for the true "
				),
				new BookPageInfo
				(
					"world in which they ",
					"would be placed. And ",
					"though the gods were ",
					"tight fisted with ",
					"their gold, this small ",
					"crew worked hard ",
					"and long, and were ",
					"happy in their tasks."
				),
				new BookPageInfo
				(
					"    A small corner of ",
					"Rytabul had been ",
					"claimed by the artisan ",
					"Selrahc the Slow. ",
					"Though he was not ",
					"the fastest of the ",
					"assembled workers, ",
					"the gods smiled upon "
				),
				new BookPageInfo
				(
					"his work, even ",
					"presenting him with ",
					"a mystic talisman ",
					"proclaiming his work ",
					"the best among the ",
					"newer artisans. And ",
					"so Selrahc went about ",
					"his business, creating "
				),
				new BookPageInfo
				(
					"hundreds of designs ",
					"which would one day ",
					"add color and variety ",
					"to Sosaria.",
					"    One day a ",
					"stranger appeared to ",
					"Selrahc. His chest ",
					"was bare and he wore "
				),
				new BookPageInfo
				(
					"trousers of the ",
					"brightest green, and ",
					"wherever he went, ",
					"plants grew in his ",
					"footsteps. This ",
					"caused Selrahc no end ",
					"of trouble, the ",
					"stranger always "
				),
				new BookPageInfo
				(
					"looking over his ",
					"shoulder, and the ",
					"plants sprouting in ",
					"places Selrahc ",
					"required to ply his ",
					"art. And so Selrahc ",
					"approached the ",
					"stranger and bade "
				),
				new BookPageInfo
				(
					"him speak. But this ",
					"man in green ",
					"remained silent. ",
					"Selrahc pleaded with ",
					"the stranger to give ",
					"his name, and would ",
					"he please leave ",
					"Selrahc to his work. "
				),
				new BookPageInfo
				(
					"But this mysterious ",
					"stranger remained ",
					"mute.",
					"    This angered ",
					"Selrahc mightily. Who ",
					"was this silent man, ",
					"interfering with ",
					"tasks the gods "
				),
				new BookPageInfo
				(
					"themselves had ",
					"entrusted to Selrahc? ",
					"In an attempt to ",
					"embarrass this ",
					"interloper, Selrahc ",
					"stole his green ",
					"trousers, leaving him ",
					"naked and open to "
				),
				new BookPageInfo
				(
					"comments about his ",
					"very manhood, and ",
					"still the stranger ",
					"would not speak, ",
					"would not leave this ",
					"tiny corner of ",
					"Rytabul.",
					"    Vexed to his very "
				),
				new BookPageInfo
				(
					"limits, Selrahc took ",
					"his war axe and ",
					"smote the silent one ",
					"mightily, again and ",
					"again, until the silent",
					"stranger ran away, ",
					"having never said a ",
					"word, and never "
				),
				new BookPageInfo
				(
					"showed himself in ",
					"Rytabul again.",
					"    Thus endeth the ",
					"tale of the bold ",
					"stranger.",
					"",
					"",
					""
				)
			);

		public override BookContent DefaultContent{ get{ return Content; } }

		[Constructable]
		public TheBoldStranger() : base( Utility.Random( 0xFEF, 2 ), false )
		{
		}

		public TheBoldStranger( Serial serial ) : base( serial )
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