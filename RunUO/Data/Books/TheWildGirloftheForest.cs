//////////////////////////////////////////////
//
// The Wild Girl of the Forest by Horace, Trader
//
// Created using Lokai's ReadBookFromTxt.cs
//
//////////////////////////////////////////////
using System;
using Server;

namespace Server.Items
{
	public class TheWildGirloftheForest : BaseBook
	{
		public static readonly BookContent Content = new BookContent
			(
				"The Wild Girl of the Forest", "Horace, Trader", 
				new BookPageInfo
				(
					"    Her name was ",
					"Leyla, she said, and ",
					"her hair was braided ",
					"wild with creepers ",
					"and thorns. I ",
					"marveled that they ",
					"did not hurt her, but ",
					"when I asked, she but "
				),
				new BookPageInfo
				(
					"shrugged and let her ",
					"eyes roam once more ",
					"across the woods. ",
					"Though I had my ",
					"hands securely ",
					"fastened by her ",
					"ropes, I itched to ",
					"reach out and comb "
				),
				new BookPageInfo
				(
					"that unruly golden ",
					"mane, dirtied and ",
					"leaf-ridden.",
					"    Her provenance, ",
					"she told me over ",
					"nights illumined by ",
					"campfires, was once ",
					"the city of Trinsic. "
				),
				new BookPageInfo
				(
					"She claimed to have ",
					"been kidnapped and ",
					"raised by orcs, which ",
					"I judged an unlikely ",
					"tale, for all know orcs",
					"delight in eating the ",
					"meat of honest folk. ",
					"When I told her this, "
				),
				new BookPageInfo
				(
					"she laughed a fey ",
					"laugh, and gaily ",
					"admitted that honest ",
					"she was not, for oft ",
					"had she stolen folk ",
					"away from caravans ",
					"to loot their ",
					"possessions from an "
				),
				new BookPageInfo
				(
					"unconscious body!",
					"    At this, I began to",
					"fear for my life, and ",
					"her smile seemed full ",
					"of teeth sharper than ",
					"a human ought to ",
					"have, for the tale of ",
					"orcish raising had "
				),
				new BookPageInfo
				(
					"struck fear into the ",
					"marrow of my bones. ",
					"\"Wilt thou eat me?\" I ",
					"asked, a-tremble, ",
					"fearing the answer.",
					"    And she cocked ",
					"her head at me, like a ",
					"wild animal facing a "
				),
				new BookPageInfo
				(
					"word that it dost not ",
					"understand, and the ",
					"fixity in her eyes ",
					"was a glimpse into ",
					"the deeper reaches of ",
					"the Abyss. But she ",
					"finally grunted, and ",
					"said, \"Nay,\" in a "
				),
				new BookPageInfo
				(
					"voice that recalled to ",
					"me a child. \"Nay,\" ",
					"she said, \"for thou ",
					"dost remind me of a ",
					"boy I knew once, ",
					"when I was a girl ",
					"who played in a city ",
					"of great sandstone "
				),
				new BookPageInfo
				(
					"walls, before I was ",
					"taken. He had sandy ",
					"hair like thee, and I ",
					"dreamt as a child of ",
					"holding his hand and ",
					"sharing flavored ice. ",
					"His name was ",
					"Japheth.\""
				),
				new BookPageInfo
				(
					"    The next morning ",
					"she let me go, ",
					"stripped of my pouch ",
					"and clothes, and bade ",
					"me run through the ",
					"woods, and to fear ",
					"recapture, for surely ",
					"her heart would not "
				),
				new BookPageInfo
				(
					"soften again. 'Twas a ",
					"fearful run, and I ",
					"came to the road to ",
					"Yew with welts and ",
					"scratches run ",
					"rampant crost my ",
					"skin, but I did not see",
					"her again."
				),
				new BookPageInfo
				(
					"    Oft have I ",
					"wondered of the boy ",
					"named Japheth, and ",
					"whether he ",
					"remembers a girl who ",
					"lived in sandstone ",
					"walls. The only ",
					"Japheth I know is the "
				),
				new BookPageInfo
				(
					"Guildmaster of ",
					"Paladins who died ",
					"last year warring ",
					"amidst the orcs, and ",
					"though he had indeed ",
					"sandy hair, I cannot ",
					"picture him side by ",
					"side with a feral girl "
				),
				new BookPageInfo
				(
					"whose tongue has ",
					"tasted of human ",
					"flesh.",
					"    Yet the paths of ",
					"fate are strange ",
					"indeed, and I suppose ",
					"'tis possible that this",
					"paladin died "
				),
				new BookPageInfo
				(
					"defending his ",
					"remembered lady's ",
					"honor, unknowingly ",
					"struck down by the ",
					"orc that she called ",
					"father.",
					"",
					""
				)
			);

		public override BookContent DefaultContent{ get{ return Content; } }

		[Constructable]
		public TheWildGirloftheForest() : base( Utility.Random( 0xFEF, 2 ), false )
		{
		}

		public TheWildGirloftheForest( Serial serial ) : base( serial )
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