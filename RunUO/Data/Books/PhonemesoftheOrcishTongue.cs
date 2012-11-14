//////////////////////////////////////////////
//
// Phonemes of the Orcish Tongue by Yorick o'Skara Brae
//
// Created using Lokai's ReadBookFromTxt.cs
//
//////////////////////////////////////////////
using System;
using Server;

namespace Server.Items
{
	public class PhonemesoftheOrcishTongue : BaseBook
	{
		public static readonly BookContent Content = new BookContent
			(
				"Phonemes of the Orcish Tongue", "Yorick o'Skara Brae", 
				new BookPageInfo
				(
					"ab, ad, ag, akt, alm, ",
					"at, augh, auh, azh, ",
					"ba, ba, bag, bar, baz, ",
					"bid, bilge, bo, bog,",
					"bog, ",
					"brui, bu, buad, bug, ",
					"bug, buil, buim, bum, ",
					"buo, buor, buu, ca, "
				),
				new BookPageInfo
				(
					"car, clog, cro, cuk, ",
					"cur, da, dagh, dagh, ",
					"dak, dar, deak, der, ",
					"dil, dit, dor, dre,",
					"dri, ",
					"dru, du, dud, duf, ",
					"dug, dug, duh, dun, ",
					"eag, eg, egg, eichel, "
				),
				new BookPageInfo
				(
					"ek, ep, ewk, faugh, ",
					"fid, flu, fog, foo, ",
					"foz, fruk, fu, fub, ",
					"fud, fun, fup, fur, ",
					"gaa, gag, gagh, gan, ",
					"gar, gh, gha, ghat, ",
					"ghed, ghig, gho, ghu, ",
					"gig, gil, gka, glu,"
				),
				new BookPageInfo
				(
					"glu, ",
					"glug, gna, gno, gnu, ",
					"gol, gom, goth, grunt, ",
					"grut, gu, gub, gub, ",
					"gug, gug, gugh, guk, ",
					"guk (with an umlaut),",
					"gulg, gur, gurt, ha, ",
					"hagh, hat, hig, hig, "
				),
				new BookPageInfo
				(
					"hok, hrak, hrol, hug, ",
					"i, ig, igg, igh, ign, ",
					"ihg, ikk, it, jak, jek,",
					"jja, ju, juk, ka, ka, ",
					"ke, kgh, kh, ki, klap, ",
					"klu, knod, knu, kod, ",
					"krug, kt, kug, lat,",
					"log, "
				),
				new BookPageInfo
				(
					"log, lub, lug, lug,",
					"luh, ",
					"ma, nag, nar, natz, ",
					"neg, neh, nog, nug, ",
					"nug, nuk, o, oag, ob, ",
					"og, ogh, oh, olm, om, ",
					"oo, oog, oth, pa, pig, ",
					"qo, qua, quil, rekk, "
				),
				new BookPageInfo
				(
					"rim, ro, rod, ru, rug, ",
					"rukk, rur, sag, sah, ",
					"sg, snarf, stu, thu, ",
					"thu, thu, thurg, tk, ",
					"tug, u, ud, ug, ugh, ",
					"ukk, ulg, urd, urg, ",
					"urgle, ut, zug.",
					""
				)
			);

		public override BookContent DefaultContent{ get{ return Content; } }

		[Constructable]
		public PhonemesoftheOrcishTongue() : base( Utility.Random( 0xFEF, 2 ), false )
		{
		}

		public PhonemesoftheOrcishTongue( Serial serial ) : base( serial )
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