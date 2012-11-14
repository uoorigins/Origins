//////////////////////////////////////////////
//
// A Raised Fist's Progress by Willem Woidswuth
//
// Created using Lokai's ReadBookFromTxt.cs
//
//////////////////////////////////////////////
using System;
using Server;

namespace Server.Items
{
	public class ARaisedFistsProgress : BaseBook
	{
		public static readonly BookContent Content = new BookContent
			(
				"A Raised Fist's Progress", "Willem Woidswuth", 
				new BookPageInfo
				(
					"PREFACE",
					"---------------",
					"This classic epic of ",
					"Britannian literature ",
					"is one of those works ",
					"one must read before ",
					"they die. Hence ",
					"many citizens save "
				),
				new BookPageInfo
				(
					"it for life insurance.",
					"  While it is turgid ",
					"going indeed, it ",
					"offers a sample of ",
					"the formal poetic ",
					"styles prevalent two ",
					"hundred years ago, ",
					"when iambs roamed "
				),
				new BookPageInfo
				(
					"the land freely and ",
					"sonnets were not yet ",
					"extinct.",
					"  The \"garland of ",
					"sonnets\" form ",
					"consisteth of ",
					"fourteen sonnets, ",
					"where the last line "
				),
				new BookPageInfo
				(
					"of one sonnet ",
					"be the first line of ",
					"the next; and the last ",
					"line of the fourteenth ",
					"sonnet is the first ",
					"line of the first one. ",
					"Lastly, the \"crown\" ",
					"or fifteenth sonnet "
				),
				new BookPageInfo
				(
					"doth consist solely of ",
					"the first lines of the ",
					"other fourteen.",
					"  This is the only ",
					"known instance of ",
					"this form in ",
					"Britannian literature, ",
					"perhaps because it be "
				),
				new BookPageInfo
				(
					"too pedantic to suffer ",
					"to live.",
					"  Woidswuth lived ",
					"and died long ago ",
					"enough to be studied ",
					"in schools. Little is ",
					"known of his life, ",
					"save that he willed "
				),
				new BookPageInfo
				(
					"his second-best hat ",
					"to his cousin Javlin ",
					"Wiggle. A member of ",
					"the Romantic School, ",
					"he also dabbled in ",
					"painting, and ",
					"illustrated many of ",
					"his own books."
				),
				new BookPageInfo
				(
					"A RAISED FIST'S ",
					"PROGRESS",
					"",
					"I. I First Notice ",
					"Something Wrong ",
					"With Heaven",
					"",
					"The surest sign of "
				),
				new BookPageInfo
				(
					"  love is jealousy;",
					"Without a warning or ",
					"  compassion, with",
					"Little thought of ",
					"  consequence, it ",
					"  creeps,",
					"A slow and certain ",
					"  sleuth, along the "
				),
				new BookPageInfo
				(
					"  bliss-",
					"Ful wedding halls, ",
					"  around the Nuptial ",
					"  bed.",
					"It eyes your lover's ",
					"  friends, and ",
					"  ruptures old",
					"Acquaintanceships; it "
				),
				new BookPageInfo
				(
					"  plays the organ-led",
					"Last waltz, the ",
					"  dance that feels so ",
					"  empty cold.",
					"It is a sign, erected ",
					"  high, like grass",
					"Upon a grave, like ",
					"  flowers on a hill--"
				),
				new BookPageInfo
				(
					"Like tripping to fall ",
					"  right on your blind ",
					"  ass",
					"And landing eyes ",
					"  wide can give you ",
					"  the will",
					"To find the sparkles, ",
					"  glimmerings "
				),
				new BookPageInfo
				(
					"  bewitched",
					"In water in a ",
					"  storm-filled ",
					"  stagnant ditch.",
					"",
					"II. I Set Off On A ",
					"Quest To Find A ",
					"Purer Paradise Than "
				),
				new BookPageInfo
				(
					"Nature Offers Man",
					"",
					"In water in a ",
					"  storm-filled ",
					"  stagnant ditch",
					"I found the images of ",
					"  Camelot.",
					"So I set off, bade love"
				),
				new BookPageInfo
				(
					"  farewell, and ",
					"  wished",
					"My way ",
					"  along the questing ",
					"  path. A motley",
					"Figure I was; my ",
					"  friends said Stay! ",
					"  You've found"
				),
				new BookPageInfo
				(
					"A heaven here at ",
					"  home! But I saw ",
					"  much",
					"In that rainwater ",
					"  puddle: free and ",
					"  boundless",
					"Generosity, without ",
					"  the inching"
				),
				new BookPageInfo
				(
					"Flaws I saw in what ",
					"  was merely love.",
					"The path to find ",
					"  perfection clear to ",
					"  me,",
					"I undertook a classic ",
					"  quest, to solve",
					"Away the weeping "
				),
				new BookPageInfo
				(
					"  worm of jealousy.",
					"How could home seem ",
					"  filled with hope and ",
					"  freedom,",
					"Seeing the beauty of ",
					"  the earth so free?",
					"",
					"III. I Am Punished "
				),
				new BookPageInfo
				(
					"For My ",
					"Impertinence; It ",
					"Merely Makes Me ",
					"Strive",
					"",
					"",
					"",
					""
				)
			);

		public override BookContent DefaultContent{ get{ return Content; } }

		[Constructable]
		public ARaisedFistsProgress() : base( Utility.Random( 0xFEF, 2 ), false )
		{
		}

		public ARaisedFistsProgress( Serial serial ) : base( serial )
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