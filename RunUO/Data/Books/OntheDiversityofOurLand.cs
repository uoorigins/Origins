//////////////////////////////////////////////
//
// On the Diversity of Our Land by Lord Blackthorn
//
// Created using Lokai's ReadBookFromTxt.cs
//
//////////////////////////////////////////////
using System;
using Server;

namespace Server.Items
{
	public class OntheDiversityofOurLand : BaseBook
	{
		public static readonly BookContent Content = new BookContent
			(
				"On the Diversity of Our Land", "Lord Blackthorn", 
				new BookPageInfo
				(
					"  While I deplore the ",
					"depredations of the ",
					"misguided and ",
					"belligerent races with ",
					"which we share our ",
					"fair Britannia, and ",
					"alongside the populace,",
					"do mourn the "
				),
				new BookPageInfo
				(
					"needless deaths that ",
					"their raids cause, I ",
					"cannot countenance ",
					"the policy of ",
					"wholesale slaughter of ",
					"these races that ",
					"seems to be the habit ",
					"of our soldierly "
				),
				new BookPageInfo
				(
					"element.  Can we not ",
					"regard the ratmen, ",
					"lizard men, and orcs ",
					"are fellow intelligent ",
					"beings with whom ",
					"we share a planet? ",
					"Why must we slay ",
					"them on sight, rather "
				),
				new BookPageInfo
				(
					"than attempt to engage ",
					"them in dialogue? ",
					"There is no policy of ",
					"shooting at wisps ",
					"when they grace us ",
					"with their presence ",
					"(not that an arrow ",
					"could do much to "
				),
				new BookPageInfo
				(
					"pierce them!).",
					"  To view these ",
					"creatures as vermin ",
					"denies their obvious ",
					"intelligence, and we ",
					"cannot underestimate ",
					"the repercussions ",
					"that their slaughter "
				),
				new BookPageInfo
				(
					"may have. If we ",
					"regard the slaying of ",
					"fellow humans as a ",
					"crime, so must we ",
					"regard the killing of ",
					"an orc.",
					"  At the same time, ",
					"should a lizardman "
				),
				new BookPageInfo
				(
					"slay a human, should ",
					"we not forgive their ",
					"ignorance and ",
					"foolishness? Let us ",
					"not surrender the ",
					"high moral ground by ",
					"descending to ",
					"bestiality."
				),
				new BookPageInfo
				(
					"  Now, I say not that ",
					"we should fail to ",
					"defend ourselves in ",
					"case of attack, for ",
					"even amongst humans ",
					"we see war, we see ",
					"famine, and we see ",
					"assault (though we "
				),
				new BookPageInfo
				(
					"owe a debt of ",
					"gratitude to our Lord ",
					"British for ",
					"preserving us from ",
					"the worst of these!). ",
					"However, incursions ",
					"such as the recent ",
					"tragedy which cost "
				),
				new BookPageInfo
				(
					"us the life of ",
					"Japheth, Guildmaster ",
					"of Trinsic's Paladins, ",
					"are folly.",
					"  I had met Japheth, ",
					"and like all paladins, ",
					"he burned with an ",
					"inner fire. Yet "
				),
				new BookPageInfo
				(
					"though I had the ",
					"utmost respect for ",
					"him, none could deny ",
					"the hatred that ",
					"flashed in his eyes at ",
					"the mere mention of ",
					"orcs. And thus he ",
					"carried his battle to "
				),
				new BookPageInfo
				(
					"the orc camps, and ",
					"died there, unable to ",
					"rise above his own ",
					"childhood experiences ",
					"depicted in his book, ",
					"\"The Burning of ",
					"Trinsic.\" 'Tis a ",
					"shame that even our "
				),
				new BookPageInfo
				(
					"mightiest men fall ",
					"prey to this ",
					"ignorance!",
					"  Are there not ",
					"legends of orcs ",
					"adopting human ",
					"children to raise as ",
					"their own? Tales of "
				),
				new BookPageInfo
				(
					"complex societies built",
					"underground by races ",
					"we regard as bestial?",
					"  Let us not repeat ",
					"the mistake of ",
					"Japheth of the ",
					"Paladins, and let us ",
					"cease to persecute the "
				),
				new BookPageInfo
				(
					"nonhuman races, ",
					"before we discover ",
					"that we are harming ",
					"ourselves in the ",
					"process.",
					"",
					"",
					""
				)
			);

		public override BookContent DefaultContent{ get{ return Content; } }

		[Constructable]
		public OntheDiversityofOurLand() : base( Utility.Random( 0xFEF, 2 ), false )
		{
		}

		public OntheDiversityofOurLand( Serial serial ) : base( serial )
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