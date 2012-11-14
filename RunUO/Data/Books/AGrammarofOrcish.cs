//////////////////////////////////////////////
//
// A Grammar of Orcish by Yorick o'Skara Brae
//
// Created using Lokai's ReadBookFromTxt.cs
//
//////////////////////////////////////////////
using System;
using Server;

namespace Server.Items
{
	public class AGrammarofOrcish : BaseBook
	{
		public static readonly BookContent Content = new BookContent
			(
				"A Grammar of Orcish", "Yorick o'Skara Brae", 
				new BookPageInfo
				(
					"This volume, and ",
					"others in the series, ",
					"are sponsored by ",
					"Lord Blackthorn,",
					"ever a supporter of ",
					"understanding the ",
					"other sentient races ",
					"of Britannia."
				),
				new BookPageInfo
				(
					"  The Orcish tongue ",
					"may fall unpleasingly ",
					"'pon the ear, yet it ",
					"has within it a ",
					"complex grammar oft ",
					"misunderstood by ",
					"those who merely ",
					"hear the few broken "
				),
				new BookPageInfo
				(
					"words of English ",
					"our orcish brothers ",
					"manage without ",
					"education.",
					"  These are the basic ",
					"rules of orcish:",
					"  Orcish has five ",
					"tenses: present, past, "
				),
				new BookPageInfo
				(
					"future imperfect, ",
					"present interjectional,",
					"and prehensile.",
					"  Examples: gugroflu, ",
					"gugrofloog, gugrobo, ",
					"gugroglu!, gugrogug.",
					"  All transitive verbs ",
					"in the prehensile "
				),
				new BookPageInfo
				(
					"tense end in \"ug.\"",
					"  Examples: ",
					"urgleighug, ",
					"biggugdaghgug, ",
					"curdakalmug.",
					"  All present ",
					"interjectional ",
					"conjugations start "
				),
				new BookPageInfo
				(
					"with the letter G ",
					"unless they contain ",
					"the third declensive ",
					"accent of the letter U.",
					"  Examples: ",
					"ghothudunglug, but not ",
					"azhbuugub.",
					"  The past tense can "
				),
				new BookPageInfo
				(
					"only refer to events ",
					"since the last meal, ",
					"but the prehensile ",
					"tense can refer to ",
					"any event within ",
					"reach.",
					"  The present tense ",
					"is conjugated like the "
				),
				new BookPageInfo
				(
					"future imperfect ",
					"tense, when the ",
					"interrogative mode is ",
					"used by pitching the ",
					"sound a quarter-tone ",
					"higher.",
					"  Orcish hath no ",
					"concept of person, as "
				),
				new BookPageInfo
				(
					"in first person, ",
					"third person, I, we, ",
					"etc.",
					"  Orcish grammar ",
					"relies upon the three ",
					"cardinal rules of ",
					"accretion, prefixing, ",
					"and agglutination, in "
				),
				new BookPageInfo
				(
					"addition to pitch. In ",
					"the former, phonemes ",
					"combine into larger ",
					"words which may ",
					"contain full phrasal ",
					"significance. In the ",
					"second, prefixing ",
					"specific phonetic "
				),
				new BookPageInfo
				(
					"sounds changes the ",
					"subject of the ",
					"sentence into object, ",
					"interrogative, ",
					"addressed individual, ",
					"or dinner.",
					"  Agglutination occurs ",
					"whenever four of "
				),
				new BookPageInfo
				(
					"the same letter are ",
					"present in a word, in ",
					"which case, any two ",
					"of them may be ",
					"removed or slurred.",
					"  Pitch changes the ",
					"phoneme value of ",
					"individual syllables, "
				),
				new BookPageInfo
				(
					"thus completely ",
					"altering what a word ",
					"may mean. The ",
					"classic example is ",
					"\"Aktgluthugrot ",
					"bigglogubuu ",
					"dargilgaglug ",
					"lublublub\" "
				),
				new BookPageInfo
				(
					"which can mean \"You ",
					"are such a pretty ",
					"girl,\" \"My mother ate ",
					"your primroses,\" or ",
					"\"Jellyfish nose paints ",
					"alms potato,\" ",
					"depending on pitch.",
					"  Orcish poetry often "
				),
				new BookPageInfo
				(
					"relies upon repeating ",
					"the same phrase in ",
					"multiple pitches, ",
					"even changing pitch ",
					"midword. None of ",
					"this great art is ",
					"translatable.",
					"  The orcish language "
				),
				new BookPageInfo
				(
					"uses the following ",
					"vowels: ab, ad, ag,",
					"akt, ",
					"at, augh, auh, azh, e, ",
					"i, o, oo, u, uu. The ",
					"vowel sound a is not ",
					"recognized as a vowel ",
					"and does not exist in "
				),
				new BookPageInfo
				(
					"their alphabet.",
					"The orcish alphabet ",
					"is best learned using ",
					"the classic rhyme ",
					"repeated at 23 ",
					"different pitches:",
					"(A translation of the ",
					"first pitch follows):"
				),
				new BookPageInfo
				(
					"Lugnog ghu blat ",
					"  suggaroglug,",
					"Gaghbuu dakdar ab ",
					"  highugbo,",
					"Gothnogbuim ad ",
					"  gilgubbugbuilug",
					"Bilgeaugh thurggulg ",
                    "  stuiggro!"
				),
				new BookPageInfo
				(
					"Eat food, the first ",
					"  letter is ab,",
					"Kill people, next ",
					"  letter is ad,",
					"I forget the rest",
					"But augh is in there ",
					"  somewhere!",
                    "      THE END"
				),
			);

		public override BookContent DefaultContent{ get{ return Content; } }

		[Constructable]
		public AGrammarofOrcish() : base( Utility.Random( 0xFEF, 2 ), false )
		{
		}

		public AGrammarofOrcish( Serial serial ) : base( serial )
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