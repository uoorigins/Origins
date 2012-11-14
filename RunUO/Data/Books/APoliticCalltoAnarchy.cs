//////////////////////////////////////////////
//
// A Politic Call to Anarchy by Lord Blackthorn
//
// Created using Lokai's ReadBookFromTxt.cs
//
//////////////////////////////////////////////
using System;
using Server;

namespace Server.Items
{
	public class APoliticCalltoAnarchy : BaseBook
	{
		public static readonly BookContent Content = new BookContent
			(
				"A Politic Call to Anarchy", "Lord Blackthorn", 
				new BookPageInfo
				(
					"  Let it never be ",
					"said that I have aught ",
					"as quarrel with my ",
					"liege Lord British, ",
					"for indeed we be of ",
					"the best of friends, ",
					"sharing amicable ",
					"games of chess 'pon a "
				),
				new BookPageInfo
				(
					"winter's night, and ",
					"talking at length into ",
					"the wee hours of the ",
					"issues that affect the ",
					"realm of Britannia.",
					"  Yet true friendship ",
					"doth not prevent true ",
					"philosophical "
				),
				new BookPageInfo
				(
					"disagreement either. ",
					"While I view with ",
					"approval my lord's ",
					"affection for his ",
					"carefully crafted ",
					"philosophy of the ",
					"Eight Virtues, ",
					"wherein moral "
				),
				new BookPageInfo
				(
					"behavior is ",
					"encouraged in the ",
					"populace, I view with ",
					"less approval the ",
					"expenditure of public ",
					"funds upon the ",
					"construction of ",
					"\"shrines\" to said "
				),
				new BookPageInfo
				(
					"ideals.",
					"  The issue is not one ",
					"of funds, however, ",
					"but a disagreement ",
					"most intellectual over ",
					"the proper way of ",
					"humankind in an ",
					"ethical sense. Surely "
				),
				new BookPageInfo
				(
					"freedom of decision ",
					"must be regarded as ",
					"paramount in any ",
					"such moral decision? ",
					"Though none fail to ",
					"censure the ",
					"murderer, a subtler ",
					"question arises when "
				),
				new BookPageInfo
				(
					"we ask if his ",
					"behavior would be ",
					"ethical if he were ",
					"forced to it.",
					"  I say to thee, the ",
					"reader, quite flatly, ",
					"that no ethical ",
					"system shall have "
				),
				new BookPageInfo
				(
					"sway over me unless ",
					"it convinceth me, for ",
					"that freely made ",
					"choice is to me the ",
					"sign that the system ",
					"hath validity.",
					"  Whereas the system ",
					"of \"Virtues\" that my "
				),
				new BookPageInfo
				(
					"liege espouses is ",
					"indeed a compilation ",
					"of commonly ",
					"approved virtues, I ",
					"approve of it. Where ",
					"it seeks to control the",
					"populace and restrict ",
					"their diversity and "
				),
				new BookPageInfo
				(
					"their range of ",
					"behaviors, I quarrel ",
					"with it. And thus do I ",
					"issue this politic call",
					"to anarchy, whilst ",
					"humbly begging ",
					"forgiveness of Lord ",
					"British for my "
				),
				new BookPageInfo
				(
					"impertinence:  ",
					"    Celebrate thy ",
					"differences. Take ",
					"thy actions according ",
					"to thy own lights. ",
					"Question from what ",
					"source a law, a rule, ",
					"a judge, and a virtue "
				),
				new BookPageInfo
				(
					"may arise. 'Twere ",
					"possible (though I ",
					"suggest it not ",
					"seriously) that a ",
					"daemon planted the ",
					"seed of these ",
					"\"Virtues\" in my Lord ",
					"British's mind; 'twere "
				),
				new BookPageInfo
				(
					"possible that the ",
					"Shrines were but a ",
					"plan to destroy this ",
					"world. Thou canst ",
					"not know unless thou ",
					"questioneth, doubteth, ",
					"and in the end, ",
					"unless thou relyest "
				),
				new BookPageInfo
				(
					"upon THYSELF and ",
					"thy judgement.  I ",
					"offer these words as ",
					"mere philosophical ",
					"musings for those ",
					"who seek ",
					"enlightenment, for ",
					"'tis the issue that "
				),
				new BookPageInfo
				(
					"hath occupied mine ",
					"interest and that of ",
					"Lord British for ",
					"some time now.",
					"",
					"",
					"",
					""
				)
			);

		public override BookContent DefaultContent{ get{ return Content; } }

		[Constructable]
		public APoliticCalltoAnarchy() : base( Utility.Random( 0xFEF, 2 ), false )
		{
		}

		public APoliticCalltoAnarchy( Serial serial ) : base( serial )
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