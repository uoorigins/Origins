//////////////////////////////////////////////
//
// Britannian Flora: a Casual Guide by Herbert the Lost
//
// Created using Lokai's ReadBookFromTxt.cs
//
//////////////////////////////////////////////
using System;
using Server;

namespace Server.Items
{
	public class BritannianFloraaCasualGuide : BaseBook
	{
		public static readonly BookContent Content = new BookContent
			(
				"Britannian Flora: a Casual Guide", "Herbert the Lost", 
				new BookPageInfo
				(
					"  Oft 'pon rambling ",
					"through the woods ",
					"avoiding bears have I ",
					"spotted some plant ",
					"whose like I have ",
					"never seen before, ",
					"and concluded that I ",
					"was a blithering idiot "
				),
				new BookPageInfo
				(
					"for failing to notice",
					"it ",
					"in the past. Equally ",
					"as oft have I ",
					"concluded that I was a ",
					"worse idiot for not ",
					"running faster from ",
					"the bear."
				),
				new BookPageInfo
				(
					"  While not all my ",
					"readers may share ",
					"my proclivities for ",
					"tree-climbing, it ",
					"occurred to me that ",
					"mayhap mine ",
					"information might ",
					"serve some humble "
				),
				new BookPageInfo
				(
					"purpose.",
					"  The two most ",
					"unique flowering ",
					"plants in the ",
					"Britannian ",
					"countryside are the ",
					"orfleur and the ",
					"whiteflower, also "
				),
				new BookPageInfo
				(
					"called white horns.",
					"  The orfleur is ",
					"notable for its ",
					"massive orange-red ",
					"blossoms, which ",
					"dwarf marigolds like ",
					"the sun dwarfs your ",
					"common fireball "
				),
				new BookPageInfo
				(
					"spell. The odor of ",
					"said blooms is best ",
					"described as ",
					"peppermint-apple, ",
					"with a dash of garlic. ",
					"'Tis a popular potted ",
					"plant despite, or ",
					"perhaps because of, "
				),
				new BookPageInfo
				(
					"its exotic nature.",
					"  Whiteflowers ",
					"exude a subtle ",
					"fragrance not unlike ",
					"that of freshly ",
					"shaven wood mixed ",
					"with cool lemon ice. ",
					"Their tall stands "
				),
				new BookPageInfo
				(
					"always droop with ",
					"the heavy weight of ",
					"the massive blooms, ",
					"oft as large as a ",
					"child's head.",
					"   The flowers are so ",
					"large that one may ",
					"scoop out the pollen in"
				),
				new BookPageInfo
				(
					"handfuls, and during ",
					"the spring season ",
					"many a prank hath ",
					"been played by idle ",
					"boys 'pon their ",
					"sisters by dumping ",
					"said pollen into their ",
					"clothing drawers, "
				),
				new BookPageInfo
				(
					"causing sneezes for ",
					"days.",
					"  The most ",
					"interesting native tree",
					"to Britannia is the ",
					"spider tree. The ",
					"reason for its naming ",
					"is obscure, but may "
				),
				new BookPageInfo
				(
					"have to do with the ",
					"twisted gray stalks ",
					"from which the ",
					"spherical canopy ",
					"sprouts. 'Tis ",
					"something of a ",
					"misnomer to term ",
					"these \"trunks\" as "
				),
				new BookPageInfo
				(
					"they are spindly and ",
					"flexible. Spider trees ",
					"provide a fresh, ",
					"piney smell to a room ",
					"and are therefore ",
					"often potted.",
					"  In jungle climes, ",
					"one finds the blade "
				),
				new BookPageInfo
				(
					"plant, whose sharp ",
					"leaves oft collect ",
					"water for the ",
					"thirsty traveler, yet ",
					"can draw blood ",
					"easily.",
					"  The deadliest plant, ",
					"if you can call a "
				),
				new BookPageInfo
				(
					"fungus plant, is the",
					"Exploding Red Spotted ",
					"Toadstool. No pattern ",
					"can be discerned to ",
					"its habitats save ",
					"malice, for merely ",
					"approaching results in ",
					"the cap exploding "
				),
				new BookPageInfo
				(
					"with powder, noxious ",
					"gas, and tiny painful ",
					"pellets flying in all ",
					"directions. ",
					"  Unfortunately, 'tis ",
					"impossible to tell it ",
					"apart from the ",
					"Ordinary Red Spotted "
				),
				new BookPageInfo
				(
					"Toadstool save ",
					"through ",
					"experimentation.  ",
					"  Truly odd among the ",
					"varied flora of ",
					"Britannia, however, ",
					"are those which bear ",
					"names clearly alien to "
				),
				new BookPageInfo
				(
					"our tongue. Among ",
					"these I name the ",
					"Tuscany pine (for I ",
					"have never seen a ",
					"region of this world ",
					"named Tuscany), the ",
					"o'hii tree, whose ",
					"very name sounds "
				),
				new BookPageInfo
				(
					"like some tropical ",
					"isle, and the welsh ",
					"poppy, which while ",
					"different from the ",
					"ordinary poppy in ",
					"color and appearance, ",
					"is prefaced with the ",
					"odd word \"welsh,\" "
				),
				new BookPageInfo
				(
					"which as far as I ",
					"know means to forgo ",
					"paying a debt.",
					"",
					"",
					"",
					"",
					""
				)
			);

		public override BookContent DefaultContent{ get{ return Content; } }

		[Constructable]
		public BritannianFloraaCasualGuide() : base( Utility.Random( 0xFEF, 2 ), false )
		{
		}

		public BritannianFloraaCasualGuide( Serial serial ) : base( serial )
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