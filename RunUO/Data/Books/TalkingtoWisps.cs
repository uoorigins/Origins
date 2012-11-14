//////////////////////////////////////////////
//
// Talking to Wisps by Heigel of Moonglow
//
// Created using Lokai's ReadBookFromTxt.cs
//
//////////////////////////////////////////////
using System;
using Server;

namespace Server.Items
{
	public class TalkingtoWisps : BaseBook
	{
		public static readonly BookContent Content = new BookContent
			(
				"Talking to Wisps", "Heigel of Moonglow", 
				new BookPageInfo
				(
					"This volume was ",
					"sponsored by ",
					"donations from Lord ",
					"Blackthorn, ever a ",
					"supporter of ",
					"understanding the ",
					"other sentient races ",
					"of Britannia."
				),
				new BookPageInfo
				(
					"  Wisps are the most ",
					"intelligent of the ",
					"nonhuman races ",
					"inhabiting Britannia. ",
					"'Tis claimed by the ",
					"great sages that ",
					"someday we shall be ",
					"able to converse with "
				),
				new BookPageInfo
				(
					"them openly in our ",
					"native tongue -- ",
					"indeed, we must",
					"hope that wisps ",
					"learn our language, ",
					"for it is not possible ",
					"for humans to ",
					"pronounce wispish!"
				),
				new BookPageInfo
				(
					"  The wispish ",
					"language seems to ",
					"only contain one ",
					"vowel, the letter Y. ",
					"However, the letters ",
					"W, C, M, and L seem ",
					"to be treated ",
					"grammatically as "
				),
				new BookPageInfo
				(
					"vowels, and in ",
					"addition every letter ",
					"is followed by what ",
					"sounds to the human ",
					"ear like a glottal stop.",
					"It is possible that the",
					"glottal stop is ",
                    "considered a vowel "
				),
				new BookPageInfo
				(
					"as well.",
					"  Wisps do make use ",
					"of what sound to us ",
					"like pitch and ",
					"emphasis shifts ",
					"similar to ",
					"exclamations and ",
                    "questions."
				),
				new BookPageInfo
				(
					"  The average word ",
					"in wispish seems to ",
					"consist of three ",
					"phonemes and three ",
					"glottal stops, plus ",
					"possibly a pitch ",
					"shift. It often ",
                    "sounds "
				),
				new BookPageInfo
				(
					"burning or crackling. ",
					"Some have speculated ",
					"that what we are ",
					"analyzing is in fact ",
					"nothing more than the ",
					"very air crackling ",
					"near the wisp's glow, ",
                    "and not language, but "
				),
				new BookPageInfo
				(
					"this is of course ",
					"unlikely.",
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
		public TalkingtoWisps() : base( Utility.Random( 0xFEF, 2 ), false )
		{
		}

		public TalkingtoWisps( Serial serial ) : base( serial )
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