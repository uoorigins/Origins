//////////////////////////////////////////////
//
// Classic Children's Tales, Volume Two by Guilhem, Editor
//
// Created using Lokai's ReadBookFromTxt.cs
//
//////////////////////////////////////////////
using System;
using Server;

namespace Server.Items
{
	public class ClassicChildrensTalesVolumeTwo : BaseBook
	{
		public static readonly BookContent Content = new BookContent
			(
				"Classic Children's Tales, Volume Two", "Guilhem, Editor", 
				new BookPageInfo
				(
					"Clarke's Printery",
					"is Honored to",
					"Present Tales from",
					"Ages Past!",
					"    Guilhem the",
					"Scholar Shall End",
					"Each Volume with",
					"Staid Commentary."
				),
				new BookPageInfo
				(
					"THE RHYME",
					"Dance in the Star ",
					"   Chamber",
					"And Dance in the Pit",
					"And Eat of your ",
					"   Entrees",
					"In the Glass House ",
					"   you Sit"
				),
				new BookPageInfo
				(
					"COMMENTARY",
					"    A common ",
					"feeding rhyme for ",
					"little babies, 'tis ",
					"thought that this",
					"little ",
					"ditty is part of the ",
					"corpus of legendary "
				),
				new BookPageInfo
				(
					"tales regarding the ",
					"world before Sosaria ",
					"(see the wonderful ",
					"fables of Fabio the ",
					"Poor for fictionalized ",
					"versions of these ",
					"stories, also available",
					"from this same "
				),
				new BookPageInfo
				(
					"publisher).    ",
					"According to these ",
					"old tales, which ",
					"survive mostly in the ",
					"hills and remote ",
					"villages where Lord ",
					"British is as yet a ",
					"distant and mythical "
				),
				new BookPageInfo
				(
					"ruler, the gods of old ",
					"(a fanciful notion!) ",
					"met to discuss the ",
					"progress of creating ",
					"the world in mystical ",
					"rooms. A simple ",
					"analysis reveals these ",
					"rooms to be mere "
				),
				new BookPageInfo
				(
					"mythological ",
					"generalizations.",
					"    \"The Star ",
					"Chamber\" is clearly a ",
					"reference to the sky. ",
					"\"The Pit\" is certainly ",
					"an Underworld ",
					"analogous to the "
				),
				new BookPageInfo
				(
					"Snakehills of other ",
					"tales, and \"the Glass ",
					"House\" is no doubt the ",
					"vantage point from ",
					"which the gods ",
					"observed their ",
					"creation. All is simple",
					"when seen from this "
				),
				new BookPageInfo
				(
					"perspective, leaving ",
					"only the mysterious ",
					"reference to dinners. ",
					"Oddly enough, the ",
					"rhyme is universally ",
					"used only for ",
					"midnight feedings, ",
					"never during the "
				),
				new BookPageInfo
				(
					"day.",
					"",
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
		public ClassicChildrensTalesVolumeTwo() : base( Utility.Random( 0xFEF, 2 ), false )
		{
		}

		public ClassicChildrensTalesVolumeTwo( Serial serial ) : base( serial )
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