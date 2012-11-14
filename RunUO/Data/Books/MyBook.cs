//////////////////////////////////////////////
//
// My Book by Sherry the Mouse
//
// Created using Lokai's ReadBookFromTxt.cs
//
//////////////////////////////////////////////
using System;
using Server;

namespace Server.Items
{
	public class MyBook : BaseBook
	{
		public static readonly BookContent Content = new BookContent
			(
				"My Book", "Sherry the Mouse", 
				new BookPageInfo
				(
					"  'Twas on a chill ",
					"night, when the moon ",
					"shone pasty-faced ",
					"above the horizon, ",
					"balanced on the ",
					"towers of Lord ",
					"British's castle, that ",
					"the events I am about "
				),
				new BookPageInfo
				(
					"to relate took place, ",
					"some years ago now. I ",
					"witnessed them all ",
					"from my tiny ",
					"mousehole.",
					"  Milords British and ",
					"Blackthorn are ",
					"accustomed to a game "
				),
				new BookPageInfo
				(
					"of chess 'pon an ",
					"evening, over which ",
					"they argue the issues ",
					"that affect the course ",
					"of the realm. Lord ",
					"Blackthorn was on ",
					"his way to Lord ",
					"British's chambers, "
				),
				new BookPageInfo
				(
					"and Lord British ",
					"stood by a window ",
					"casement, just having ",
					"finished setting the ",
					"pieces upon the board.",
					"  Suddenly the ",
					"shutters blew open, ",
					"and Lord British fell "
				),
				new BookPageInfo
				(
					"to the ground, one ",
					"hand shielding his ",
					"eyes. A chill wind ",
					"entered the room, ",
					"and it seemed a gash ",
					"was torn in the very ",
					"air. Through the gash ",
					"I could see stars and "
				),
				new BookPageInfo
				(
					"swirling clouds of ",
					"stellar dust, and a ",
					"coldness sucked all ",
					"the warmth from the ",
					"air. A terrible wind ",
					"tossed books and ",
					"blankets across the ",
					"room, and furniture "
				),
				new BookPageInfo
				(
					"toppled.",
					"  From within this ",
					"gash issued a great ",
					"voice, unlike any I ",
					"have ever heard. ",
					"And these are the ",
					"words it spoke (for I ",
					"memorized them "
				),
				new BookPageInfo
				(
					"most carefully):",
					"  \"Greetings, Lord ",
					"British. I am the ",
					"Time Lord, a being ",
					"from beyond your ",
					"dimension, as thou ",
					"art from a world ",
					"other than Sosaria. I "
				),
				new BookPageInfo
				(
					"am here to bring thee ",
					"warning. Dost thou ",
					"recall how long ago a ",
					"mysterious Stranger ",
					"came to Sosaria and ",
					"saved the world ",
					"from the evil wizard ",
					"Mondain? He "
				),
				new BookPageInfo
				(
					"shattered the Gem of ",
					"Immortality, within ",
					"which dwelled a ",
					"perfect likeness of ",
					"this world.\"",
					"  Lord British slowly ",
					"stood and faced the ",
					"hole in the air. \"I "
				),
				new BookPageInfo
				(
					"remember,\" he said. ",
					"\"Oft have I wished ",
					"that stranger would ",
					"return.\"",
					"  \"He hath returned,\" ",
					"spoke the voice. \"But ",
					"not to here. When the ",
					"Gem was shattered, a "
				),
				new BookPageInfo
				(
					"thousand shards were ",
					"scattered across the ",
					"dimensions, and in ",
					"each shard there is a ",
					"perfect likeness of ",
					"this world. And thou ",
					"dost live upon one ",
					"such shard, for thou "
				),
				new BookPageInfo
				(
					"art not of the true ",
					"world-thou art ",
					"merely a reflection.\"",
					"  Lord British looked ",
					"shaken by this, and I ",
					"did not know what to ",
					"think! Was I merely a ",
					"shadow of the real "
				),
				new BookPageInfo
				(
					"me, which lives still ",
					"somewhere else ",
					"across uncounted ",
					"universes?",
					"  \"My task is to heal ",
					"this shattered world, ",
					"Lord British,\" said ",
					"the voice. \"And I seek "
				),
				new BookPageInfo
				(
					"to enlist thee in my ",
					"cause. Be warned ",
					"that in this case, ",
					"healing carries with ",
					"it a terrible price.\"",
					"  Concern warred ",
					"with curiosity on my ",
					"liege's face, but ever "
				),
				new BookPageInfo
				(
					"one to shoulder a ",
					"burden, he ",
					"straightened and ",
					"faced the gash in the ",
					"air bravely. \"Name ",
					"thy price.\"",
					"  \"A shard of a ",
					"universe is a "
				),
				new BookPageInfo
				(
					"powerful thing, and a ",
					"universe shattered is ",
					"always in danger ",
					"from the powers of ",
					"darkness. Already ",
					"three shards were ",
					"turned to evil, and ",
					"sent to plague the "
				),
				new BookPageInfo
				(
					"original universe in ",
					"the form of ",
					"Shadowlords. Many ",
					"times have I brought ",
					"the Stranger back to ",
					"Britannia, to preserve ",
					"it from its own folly ",
					"or from outside "
				),
				new BookPageInfo
				(
					"dangers. Yet as long ",
					"as the world ",
					"remaineth in pieces, ",
					"it remaineth ",
					"vulnerable. We must ",
					"bring the shards into ",
					"harmony, so that they ",
					"resonate in such a "
				),
				new BookPageInfo
				(
					"manner that matches ",
					"the original universe. ",
					"Then the two ",
					"universes shall ",
					"merge, and be again ",
					"as one.\"",
					"  \"But if we are only ",
					"shadows...\" Lord "
				),
				new BookPageInfo
				(
					"British said ",
					"wonderingly.",
					"  The light from the ",
					"stars within the hole ",
					"seemed to dim. ",
					"\"Indeed, the ",
					"reflections shall ",
					"become one with the "
				),
				new BookPageInfo
				(
					"original. Thou wouldst ",
					"cease to be as thou ",
					"art, and become part ",
					"of the larger you. ",
					"Thou shalt not die; ",
					"however, uncounted ",
					"generations have ",
					"passed and borne "
				),
				new BookPageInfo
				(
					"children since that ",
					"day, and they have ",
					"no counterparts. ",
					"They would perish ",
					"utterly.\"  Lord ",
					"British sagged in ",
					"shock, realizing the ",
					"terrible price that "
				),
				new BookPageInfo
				(
					"would be paid to heal ",
					"the universe. \"All of ",
					"my people,\" he ",
					"breathed.",
					"  \"'Tis for ",
					"the greater good.\"",
					"  Lord British bowed ",
					"his head."
				),
				new BookPageInfo
				(
					" 'Twas then I saw ",
					"the movement by the ",
					"door, half-hid by the ",
					"heavy red curtains. ",
					"Lord Blackthorn ",
					"stood there, concealed ",
					"from the rest of the ",
					"room, his face "
				),
				new BookPageInfo
				(
					"white. How long had ",
					"he been listening? I ",
					"cannot say, yet I ",
					"suspect that he had ",
					"heard all that the ",
					"mysterious voice had ",
					"to say.",
					"  \"How then, shall I "
				),
				new BookPageInfo
				(
					"aid thee?\" Lord ",
					"British said, ",
					"weariness in his ",
					"voice.",
					"  \"Aid the nobilty ",
					"that resideth in the ",
					"human heart. Protect ",
					"the Virtues that so "
				),
				new BookPageInfo
				(
					"recently came to thee ",
					"in thought late at ",
					"night. They are the ",
					"Virtues of life, as ",
					"your counterpart ",
					"understands them to ",
					"be. For when thy ",
					"populace doth live and "
				),
				new BookPageInfo
				(
					"breathe these Virtues, ",
					"shall it match the ",
					"true Britannia, and ",
					"thy shard shall ",
					"rejoin with it.\"",
					"  The gash in the air ",
					"began to close, and ",
					"with it warmth stole "
				),
				new BookPageInfo
				(
					"back into the room.",
					"  \"I was going to ",
					"discuss my idea with ",
					"Blackthorn tonight,\" ",
					"Lord British ",
					"breathed. \"Have I no ",
					"thoughts that are my ",
					"own? Is my life but "
				),
				new BookPageInfo
				(
					"a reflection of ",
					"another me?\"",
					"  \"Nay,\" said the ",
					"voice, smaller ",
					"through the ",
					"diminished opening. ",
					"\"Say, rather, that ",
					"you are parallel, for "
				),
				new BookPageInfo
				(
					"there is no guarantee ",
					"that thou shalt ",
					"accomplish what I ",
					"have set thee to. I ",
					"speak tonight to a ",
					"thousand of thee, and ",
					"ask the same of all. ",
					"Perhaps not all shall "
				),
				new BookPageInfo
				(
					"seek to aid me.\" And ",
					"with that, the gash ",
					"closed, and the voice ",
					"was gone, leaving a ",
					"room that appeare ",
					"tossed by a mighty ",
					"storm.",
					"  \"Destroy the "
				),
				new BookPageInfo
				(
					"world to save the ",
					"universe,\" Lord ",
					"British said bitterly. ",
					"\"I do not wonder that ",
					"some may balk.\"",
					"  Lord Blackthorn ",
					"collected himself, and ",
					"strode into the room, "
				),
				new BookPageInfo
				(
					"a decent mimicry of ",
					"surprise on his face. ",
					"\"My liege! What has ",
					"happened here?\" he ",
					"exclaimed, feigning ",
					"dismay well. But not ",
					"well enough to fool ",
					"his old friend, "
				),
				new BookPageInfo
				(
					"whose eyes narrowed ",
					"at seeing him there.  ",
					"  \"How much didst ",
					"thou hear?\" demanded ",
					"Lord British.",
					"  \"Why, nothing,\" ",
					"managed Blackthorn, ",
					"his head ducked away "
				),
				new BookPageInfo
				(
					"from his friend, as ",
					"he bent to retrieve ",
					"the fallen chess ",
					"pieces. \"I merely ",
					"came for our game ",
					"of chess.\"",
					"  Together they ",
					"righted the "
				),
				new BookPageInfo
				(
					"table, and set the ",
					"pieces upon the black ",
					"and white squares. ",
					"\"Such simplicity to ",
					"the game, Blackthorn,\" ",
					"mused Lord British, ",
					"idly brushing one ",
					"finger against the "
				),
				new BookPageInfo
				(
					"board. \"Black and ",
					"white, each to its ",
					"own color, as if life ",
					"were so simple. ",
					"What think you?\"",
					"  Blackthorn sat ",
					"heavily on a hassock ",
					"beside the chess "
				),
				new BookPageInfo
				(
					"table. \"I think that ",
					"matters are never so ",
					"simple, my liege. And ",
					"that I would regret it ",
					"deeply if someone, ",
					"such as a friend, ",
					"saw it thus.\"",
					"  Lord British's eyes "
				),
				new BookPageInfo
				(
					"met his. \"Yet ",
					"sometimes one must ",
					"sacrifice a pawn to ",
					"save a king.\"",
					"  Lord Blackthorn ",
					"met his gaze ",
					"squarely. \"Even ",
					"pawns have lives and "
				),
				new BookPageInfo
				(
					"loves at home, my ",
					"lord.\" Then he ",
					"reached out for a ",
					"pawn, and firmly ",
					"moved it forward two ",
					"squares. \"Shall we ",
					"play a game?\" he ",
					"asked."
				),
				new BookPageInfo
				(
					"  The chess game ",
					"that night was a ",
					"draw, and they ",
					"played grimly.",
					"  And the next day, ",
					"Lord British ",
					"gathered the nobles to ",
					"proclaim the idea of "
				),
				new BookPageInfo
				(
					"a new system of ",
					"Virtues, and declared ",
					"that shrines should be ",
					"built across the land.",
					"  Lord Blackthorn ",
					"opposed it bitterly, ",
					"and many thought ",
					"him strange for doing "
				),
				new BookPageInfo
				(
					"so, for ever had he ",
					"been a noble and ",
					"upright man, and ",
					"ever had he and ",
					"Lord British been in ",
					"accord. Declaring that ",
					"he should start his ",
					"own shrine, he "
				),
				new BookPageInfo
				(
					"departed the castle ",
					"that day to live in a ",
					"tower in a lake on the ",
					"north side of the ",
					"city.",
					"  They are still the ",
					"best of friends, yet a ",
					"sadness hangs "
				),
				new BookPageInfo
				(
					"between them, as if ",
					"they were forced ",
					"into making choices ",
					"that appealed not to ",
					"them. And at night, ",
					"when I creep softly ",
					"from one corner of ",
					"my liege's "
				),
				new BookPageInfo
				(
					"bedchamber to ",
					"another, I sometimes ",
					"see him take a pawn ",
					"from his night table, ",
					"and hold it in his ",
					"hand, and quietly ",
					"weep.",
					"  But I am but a "
				),
				new BookPageInfo
				(
					"mouse, and none ",
					"hear me. This tale ",
					"goes unknown, save ",
					"for my writing ",
					"these enormous ",
					"letters with mine ",
					"ink-stained tiny ",
					"paws for thee to "
				),
				new BookPageInfo
				(
					"read, for I fear ",
					"indeed for our world ",
					"and for our people in ",
					"these perilous times.",
					"",
					"",
					"",
					""
				)
			);

		public override BookContent DefaultContent{ get{ return Content; } }

		[Constructable]
		public MyBook() : base( Utility.Random( 0xFEF, 2 ), false )
		{
		}

		public MyBook( Serial serial ) : base( serial )
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