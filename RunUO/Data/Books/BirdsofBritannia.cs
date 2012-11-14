//////////////////////////////////////////////
//
// Birds of Britannia by Thomas th' Heathen
//
// Created using Lokai's ReadBookFromTxt.cs
//
//////////////////////////////////////////////
using System;
using Server;

namespace Server.Items
{
	public class BirdsofBritannia : BaseBook
	{
		public static readonly BookContent Content = new BookContent
			(
				"Birds of Britannia", "Thomas th' Heathen", 
				new BookPageInfo
				(
					"    The WREN is a ",
					"tiny insect-eating ",
					"bird with a loud voice.",
					" The cheerful trills ",
					"of Wrens are ",
					"extraordinarily ",
					"varied and melodious.",
					""
				),
				new BookPageInfo
				(
					"    The SWALLOW ",
					"is easily recognized ",
					"by its forked tail.  ",
					"Swallows catch ",
					"insects in flight, and ",
					"have squeaky, ",
					"twittering songs.",
					""
				),
				new BookPageInfo
				(
					"    The WARBLER ",
					"is an exceptional ",
					"singer, whose ",
					"extensive songs ",
					"combine the best ",
					"qualities of Wrens ",
					"and Swallows.",
					""
				),
				new BookPageInfo
				(
					"    The ",
					"NUTHATCH climbs ",
					"down trees head ",
					"first, searching for ",
					"insects in the bark.  ",
					"It sings a repetitive ",
					"series of notes with a ",
					"nasal tone quality."
				),
				new BookPageInfo
				(
					"    The agile ",
					"CHICKADEE has a ",
					"buzzy ",
					"\"chick-a-dee-dee\" ",
					"call, from which its ",
					"name is derived.  Its ",
					"song is a series of ",
					"whistled notes."
				),
				new BookPageInfo
				(
					"    The THRUSH is a ",
					"brown bird with a ",
					"spotted breast, which ",
					"eats worms and ",
					"snails, and has a ",
					"beautiful singing ",
					"voice.  Thrushes use ",
					"a stone as an anvil to "
				),
				new BookPageInfo
				(
					"smash the shells of ",
					"snails.",
					"",
					"",
					"",
					"",
					"",
					""
				),
				new BookPageInfo
				(
					"    The little ",
					"NIGHTINGALE is ",
					"also known for its ",
					"beautiful song, ",
					"which it sings even ",
					"at night.",
					"",
					""
				),
				new BookPageInfo
				(
					"    The STARLING ",
					"is a small dark bird ",
					"with a yellow bill and ",
					"a squeaky, ",
					"high-pitched song.  ",
					"Starlings can mimic ",
					"the sounds of other ",
					"birds."
				),
				new BookPageInfo
				(
					"    The SKYLARK ",
					"sings a series of ",
					"high-pitched ",
					"melodious trills in ",
					"flight.",
					"",
					"",
					""
				),
				new BookPageInfo
				(
					"    The FINCH is a ",
					"small seed-eating ",
					"bird with a conical ",
					"beak and a musical, ",
					"warbling song.",
					"",
					"",
					""
				),
				new BookPageInfo
				(
					"    The CROSSBILL ",
					"is a kind of Finch ",
					"with a strange ",
					"crossed bill, which it ",
					"uses to extract seeds ",
					"from pine cones.    ",
					"",
					""
				),
				new BookPageInfo
				(
					"The CANARY is a ",
					"kind of Finch that is ",
					"often kept as a pet.  ",
					"Miners would often ",
					"take Canaries ",
					"underground with ",
					"them, to warn them ",
					"of the presence of "
				),
				new BookPageInfo
				(
					"hazardous vapors in ",
					"the air.",
					"",
					"",
					"",
					"",
					"",
					""
				),
				new BookPageInfo
				(
					"    The SPARROW ",
					"weaves a nest of ",
					"grass, and has an ",
					"unmusical chirp for a ",
					"voice.",
					"",
					"",
					""
				),
				new BookPageInfo
				(
					"    The TOWHEE is a ",
					"kind of Sparrow that ",
					"continually reminds ",
					"listeners to drink ",
					"their tea.",
					"",
					"",
					""
				),
				new BookPageInfo
				(
					"    The SHRIKE is a ",
					"gray bird with a ",
					"hooked bill.  Shrikes ",
					"have the habit of ",
					"impaling their prey ",
					"on thorns.",
					"",
					""
				),
				new BookPageInfo
				(
					"    The ",
					"WOODPECKER has a ",
					"pointed beak that is ",
					"suitable for pecking at",
					"wood to get at the ",
					"insects inside.",
					"",
					""
				),
				new BookPageInfo
				(
					"    The ",
					"KINGFISHER dives ",
					"for fish, which it ",
					"catches with its long, ",
					"pointed beak.",
					"",
					"",
					""
				),
				new BookPageInfo
				(
					"    The TERN ",
					"migrates over great ",
					"distances, from one ",
					"end of Britannia to ",
					"the other each year.  ",
					"Terns dive from the ",
					"air to catch fish.",
					""
				),
				new BookPageInfo
				(
					"    The PLOVER is a ",
					"bird that distracts ",
					"predators by ",
					"pretending to have a ",
					"broken wing.",
					"",
					"",
					""
				),
				new BookPageInfo
				(
					"    The LAPWING is ",
					"a kind of Plover that ",
					"has a long black crest.",
					"",
					"",
					"",
					"",
					""
				),
				new BookPageInfo
				(
					"    The HAWK is a ",
					"predator that feeds on ",
					"small birds, mice, ",
					"squirrels, and other ",
					"small animals.  Small ",
					"hawks are known as ",
					"Kites.",
					""
				),
				new BookPageInfo
				(
					"    The DOVE is a ",
					"seed-eating bird with ",
					"a peaceful reputation. ",
					" Doves have a ",
					"low-pitched cooing ",
					"song.",
					"",
					""
				),
				new BookPageInfo
				(
					"    The PARROT is a ",
					"brightly colored bird ",
					"with a hooked bill, ",
					"favored as a ",
					"companion by pirates. ",
					" Parrots can be ",
					"taught to imitate the ",
					"human voice."
				),
				new BookPageInfo
				(
					"    The CUCKOO is a ",
					"devious bird that lays ",
					"eggs in the nests of ",
					"Warblers and other ",
					"small birds.  Cuckoos ",
					"have the uncanny ",
					"ability to keep track ",
					"of time, singing once "
				),
				new BookPageInfo
				(
					"at the beginning of ",
					"each hour.",
					"",
					"",
					"",
					"",
					"",
					""
				),
				new BookPageInfo
				(
					"    The ",
					"ROADRUNNER is ",
					"an unusual bird with ",
					"a long tail, which ",
					"runs swiftly along ",
					"the ground hunting ",
					"for lizards and ",
					"snakes."
				),
				new BookPageInfo
				(
					"    The SWIFT is a ",
					"very agile bird that ",
					"spends nearly its ",
					"entire life in the air.",
					"With their mouths ",
					"wide open, Swifts ",
					"capture insects in ",
					"mid-flight."
				),
				new BookPageInfo
				(
					"    The ",
					"HUMMINGBIRD is a ",
					"cross between a ",
					"Swift and a Fairy.  ",
					"These tiny, brightly ",
					"colored birds hover ",
					"magically near ",
					"flowers, and live on "
				),
				new BookPageInfo
				(
					"the nectar they ",
					"provide.",
					"",
					"",
					"",
					"",
					"",
					""
				),
				new BookPageInfo
				(
					"    The OWL is a ",
					"reputedly wise bird ",
					"that is active at",
					"night, ",
					"unlike most birds.  ",
					"Owls have excellent ",
					"night vision and ",
					"low-pitched hooting "
				),
				new BookPageInfo
				(
					"calls.  Their wings ",
					"are silent in flight.",
					"",
					"",
					"",
					"",
					"",
					""
				),
				new BookPageInfo
				(
					"",
					"    The ",
					"GOATSUCKER is a ",
					"strange owl-like bird ",
					"that is thought to live",
					"on the milk of goats.  ",
					"These mysterious ",
					"birds make jarring "
				),
				new BookPageInfo
				(
					"sounds at night, for ",
					"which reason they ",
					"are also called ",
					"Nightjars.",
					"",
					"",
					"",
					""
				),
				new BookPageInfo
				(
					"",
					"    The DUCK is a ",
					"bird that swims more ",
					"often than it flies, ",
					"and has a nasal voice ",
					"that is described as a ",
					"\"quack\".",
					""
				),
				new BookPageInfo
				(
					"    The SWAN is a ",
					"kind of long-necked ",
					"Duck that is all white.",
					" Swans are usually ",
					"voiceless, but they ",
					"are said to have an ",
					"extraordinarily ",
					"beautiful song."
				),
				new BookPageInfo
				(
					"",
					"",
					"",
					"      THE END",
					"""",
					"""",
					"""",
					""""
				)
			);

		public override BookContent DefaultContent{ get{ return Content; } }

		[Constructable]
		public BirdsofBritannia() : base( Utility.Random( 0xFEF, 2 ), false )
		{
		}

		public BirdsofBritannia( Serial serial ) : base( serial )
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