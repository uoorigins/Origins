//////////////////////////////////////////////
//
// Dimensional Travel: A Monograph by Dryus Doost, Mage
//
// Created using Lokai's ReadBookFromTxt.cs
//
//////////////////////////////////////////////
using System;
using Server;

namespace Server.Items
{
	public class DimensionalTravelAMonograph : BaseBook
	{
		public static readonly BookContent Content = new BookContent
			(
				"Dimensional Travel: A Monograph", "Dryus Doost, Mage", 
				new BookPageInfo
				(
					"  'Tis beyond the ",
					"scope of this small ",
					"monograph to discuss ",
					"the details of ",
					"moongates, and the ",
					"manners in which ",
					"they distort the ",
					"fabric of reality in "
				),
				new BookPageInfo
				(
					"such a manner as to ",
					"permit the passage of ",
					"living flesh from ",
					"place to place, world",
					"to ",
					"world, or indeed from ",
					"dimension to ",
					"dimension."
				),
				new BookPageInfo
				(
					"  Instead, allow me to ",
					"bring thy attention, ",
					"Gentle Reader, to the ",
					"curious ",
					"characteristics that ",
					"are shared by ",
					"certain individuals ",
					"within our realm."
				),
				new BookPageInfo
				(
					"  Long has it been ",
					"known that the blue ",
					"moongate permits ",
					"travel from place to ",
					"place, and none have ",
					"trouble in taking this ",
					"path. Yet 'tis also ",
					"known, albeit only to a"
				),
				new BookPageInfo
				(
					"few, that certain ",
					"individuals are ",
					"unable to traverse the ",
					"black moongates that ",
					"permit travel from ",
					"one dimension to ",
					"another.",
					"  The noted mage and "
				),
				new BookPageInfo
				(
					"peer of our realm, ",
					"Lord Blackthorn, once ",
					"told me in ",
					"conversation that his ",
					"arcane research had ",
					"indicated that the ",
					"issue was one of ",
					"conservation of ether. "
				),
				new BookPageInfo
				(
					"To wit, given the ",
					"postulate that matter ",
					"within a given ",
					"dimension may be ",
					"but a cross-section ",
					"of ethereal matter ",
					"that exists in ",
					"multiple dimensions, "
				),
				new BookPageInfo
				(
					"it becomes obvious ",
					"that said ethereal ",
					"structure cannot ",
					"enter dimensions in ",
					"which it is already ",
					"present.",
					"  Imagine an ",
					"individual (and the "
				),
				new BookPageInfo
				(
					"Lord Blackthorn ",
					"hinted that he was ",
					"one such) who exists ",
					"already in some ",
					"form in multiple ",
					"dimensions; said ",
					"individual would not ",
					"be able to cross into "
				),
				new BookPageInfo
				(
					"another dimension ",
					"because HE IS ",
					"ALREADY THERE.",
					"  The implications of ",
					"this are staggering, ",
					"and merit further ",
					"study. 'Tis well ",
					"known by theorists in "
				),
				new BookPageInfo
				(
					"the field that ",
					"divisions in the ",
					"ethereal structure of ",
					"an individual are ",
					"already implicit at ",
					"the temporal level, as ",
					"causality forces ",
					"divisions upon the "
				),
				new BookPageInfo
				(
					"ether. This is the ",
					"basic operating ",
					"mechanism by which ",
					"white moongates ",
					"function, permitting ",
					"time travel.",
					"  As time travel is ",
					"not barred by the "
				),
				new BookPageInfo
				(
					"presence of an earlier ",
					"self (though ",
					"encountering said ",
					"earlier self can ",
					"prove arcanely ",
					"perilous), there must ",
					"be some rigidity to ",
					"the ethereal "
				),
				new BookPageInfo
				(
					"structure that bars ",
					"multiple instantiations",
					"of structures from ",
					"manifesting within ",
					"the same context.",
					"  If one regards time ",
					"and causal bifurcation ",
					"as a web, perhaps the "
				),
				new BookPageInfo
				(
					"appropriate analogy ",
					"for dimensional ",
					"matrices is that of a ",
					"crystalline structure, ",
					"with rigid linkages. ",
					"The only way in ",
					"which an individual ",
					"such as Lord "
				),
				new BookPageInfo
				(
					"Blackthorn, who ",
					"exists in multiple ",
					"dimensional matrices, ",
					"can cross worlds via ",
					"a black moongate, ",
					"would be for the ",
					"entire crystalline ",
					"structure of the "
				),
				new BookPageInfo
				(
					"dimension to ",
					"perfectly match the ",
					"ethereal resonance of ",
					"the destination ",
					"dimension.",
					"  The problem of ",
					"why certain ",
					"individuals are "
				),
				new BookPageInfo
				(
					"already replicated in ",
					"multiple crystalline ",
					"matrices is one that I ",
					"fail to provide any ",
					"schema for in these ",
					"poor theories. It is ",
					"my fondest hope that ",
					"someday someone "
				),
				new BookPageInfo
				(
					"shall conquer that ",
					"thorny problem and ",
					"enlighten the world.",
					"",
					"",
					"",
					"",
					""
				)
			);

		public override BookContent DefaultContent{ get{ return Content; } }

		[Constructable]
		public DimensionalTravelAMonograph() : base( Utility.Random( 0xFEF, 2 ), false )
		{
		}

		public DimensionalTravelAMonograph( Serial serial ) : base( serial )
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