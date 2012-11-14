//////////////////////////////////////////////
//
// Classic Children's Tales, Volume One by Guilhem, Ed.
//
// Created using Lokai's ReadBookFromTxt.cs
//
//////////////////////////////////////////////
using System;
using Server;

namespace Server.Items
{
	public class ClassicChildrensTalesVolumeOne : BaseBook
	{
		public static readonly BookContent Content = new BookContent
			(
				"Classic Children's Tales, Volume One", "Guilhem, Ed.", 
				new BookPageInfo
				(
					"    Clarke's Printery ",
					"is Honored to  ",
					"Present Tales from ",
					"Ages Past!",
					"    Guilhem the ",
					"Scholar Shall End ",
					"EachVolume with ",
					"Staid Commentary."
				),
				new BookPageInfo
				(
					"   THE RHYME",
					"In the Wind where",
					"  the Balance",
					"Is Whispered in",
					"  Hallways",
					"In the Wind where ",
					"  the Magic",
					"Flows All through"
				),
				new BookPageInfo
				(
					"  the Night",
					"There live Mages ",
					"  and Mages",
					"With Robes made of",
					"  Whole Days",
					"Reading Books full of",
					"  Doings  ",
					"Printed on Light"
				),
				new BookPageInfo
				(
					"In the Wind where ",
					"  the Lovers",
					"Are Crossed under",
					"  Shadows",
					"Where they Meet and",
					"  are Parted",
					"By the Orders of",
					"  Fate"
				),
				new BookPageInfo
				(
					"The Girl becomes ",
					"  Tree",
					"And thus becomes",
					"  Widow",
					"The Boy becomes",
					"  Earth",
					"And Wanders Till",
					"  Late"
				),
				new BookPageInfo
				(
					"In the Wind are the",
					"  Monsters",
					"First Born First",
					"  Created",
					"When Chanting and",
					"  Ether",
					"Mix Meddling and",
					"  Nigh"
				),
				new BookPageInfo
				(
					"Fear going to Wind",
					"Fear Finding its",
					"  Plaitings",
					"Go Not to the",
					"  Snakehills",
					"Lest You Care To",
					"  Die",
					"      THE END"
				),
				new BookPageInfo
				(
					"   COMMENTARY",
					"  The meaning of this ",
					"verse has oft been ",
					"discussed in halls of ",
					"scholarly sorts.",
					"  Perhaps it is ",
					"but the remnant of a ",
					"longer ballad once "
				),
				new BookPageInfo
				(
					"extant, for there are ",
					"internal indications ",
					"that it once told a ",
					"longer story about ",
					"ill-fated lovers, and a",
					"magical experiment ",
					"gone awry. However, ",
					"poetic license and the "
				),
				new BookPageInfo
				(
					"folk process has ",
					"distorted the words ",
					"until now the locale of",
					"the tale is no more ",
					"than \"in the wind,\" ",
					"which while it serves ",
					"a pleasingly ",
					"metaphorical purpose, "
				),
				new BookPageInfo
				(
					"fails to inform the ",
					"listener as to any ",
					"real locale!",
					"  Another possibility ",
					"is that this is some ",
					"form of creation ",
					"myth explaining the ",
					"genesis of the "
				),
				new BookPageInfo
				(
					"various humanoid ",
					"creatures that roam ",
					"the lands of ",
					"Britannia. It does not ",
					"take a stretch of the ",
					"imagination to name ",
					"the middle verse's ",
					"\"girl becomes tree\" as "
				),
				new BookPageInfo
				(
					"a possible explanation ",
					"for the reaper, for  ",
					"in the area ",
					"surrounding Minoc, ",
					"reapers are oft ",
					"referred to among  ",
					"the lumberjacking ",
					"community as "
				),
				new BookPageInfo
				(
					"\"widowmakers.\"",
					"  The verse seems to ",
					"imply a long ago ",
					"creator, and uses the ",
					"antique magickal ",
					"terminology of ",
					"\"plaiting strands of ",
					"ether\" that is so often"
				),
				new BookPageInfo
				(
					"found in ancient ",
					"texts. In addition, ",
					"the reference to ",
					"\"snakehills\" may ",
					"well be regarded ",
					"as a reference to an ",
					"actual location, such ",
					"as perhaps a local "
				),
				new BookPageInfo
				(
					"term for the ",
					"Serpent's Spine.",
					"  A commoner ",
					"interpretation is that ",
					"like many nusery ",
					"rhymes, it is a ",
					"simple explanation ",
					"for death, wherein "
				),
				new BookPageInfo
				(
					"the wind snatches up ",
					"boys and girls when ",
					"they sleep in order to ",
					"keep the balance of ",
					"the world. Notable ",
					"tales have been ",
					"written for children ",
					"of adventures in \"the "
				),
				new BookPageInfo
				(
					"Snakehills,\" which ",
					"are presumed to be an ",
					"Afterworld whence ",
					"the spirit liveth on. A",
					"grim lullabye, to be ",
					"sure, but no worse ",
					"than \"lest I die before",
					"I wake\" surely."
				),
				new BookPageInfo
				(
					"  In either case, 'tis ",
					"an old favorite, ",
					"herein printed for ",
					"the first time for ",
					"thy enjoyment and ",
					"perusal!",
					"- GUILHEM ",
					"THE SCHOLAR."
				)
			);

		public override BookContent DefaultContent{ get{ return Content; } }

		[Constructable]
		public ClassicChildrensTalesVolumeOne() : base( Utility.Random( 0xFEF, 2 ), false )
		{
		}

		public ClassicChildrensTalesVolumeOne( Serial serial ) : base( serial )
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