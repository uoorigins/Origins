//////////////////////////////////////////////
//
// A Primer on Arms by Martin
//
// Created using Lokai's ReadBookFromTxt.cs
//
//////////////////////////////////////////////
using System;
using Server;

namespace Server.Items
{
	public class APrimeronArms : BaseBook
	{
		public static readonly BookContent Content = new BookContent
			(
				"A Primer on Arms", "Martin", 
				new BookPageInfo
				(
					"  About the Author",
					"--------------",
					"Martin was for",
					"most of his life the ",
					"Weaponsmaster of ",
					"Serpent's Hold, until ",
					"his death at 92, of ",
					"an overgrown bunion."
				),
				new BookPageInfo
				(
					"    These are the ",
					"basic elements to ",
					"consider in assessing ",
					"a weapon, of which ",
					"all warriors who ",
					"regard themselves as ",
					"more than mere ",
					"mercenaries should be "
				),
				new BookPageInfo
				(
					"aware.",
					"    First and most ",
					"obvious is the amount ",
					"of damage that the ",
					"weapon may do ",
					"against unprotected ",
					"flesh. While 'tis this ",
					"which first attracts "
				),
				new BookPageInfo
				(
					"the attention of the ",
					"novice, 'tis a deadly ",
					"mistake to regard it ",
					"as the sole value of a ",
					"weapon. While it may ",
					"prove devastating ",
					"indeed as a means of ",
					"causing damage, a "
				),
				new BookPageInfo
				(
					"weapon must also ",
					"serve as stout shield ",
					"when engaged in ",
					"combat.",
					"    Hence the second ",
					"issue to which to pay ",
					"attention is the ",
					"amount of protection "
				),
				new BookPageInfo
				(
					"that a weapon may ",
					"offer. Pay close ",
					"attention to the guard ",
					"on it, if it be a",
					"blade, ",
					"or the stoutness of ",
					"its wood if it is a",
					"pole "
				),
				new BookPageInfo
				(
					"arm.",
					"    Oft related to this",
					"is the weight of the ",
					"weapon, for a heavy ",
					"weapon is more ",
					"difficult to maneuver ",
					"to block with, though ",
					"it may do more "
				),
				new BookPageInfo
				(
					"damage to thy ",
					"opponent.",
					"    If a weapon is too ",
					"heavy for the wielder ",
					"to move it freely, ",
					"they should choose ",
					"another and not ",
					"attempt to prove their "
				),
				new BookPageInfo
				(
					"prowess by the size ",
					"of their sword.",
					"    The reach of a ",
					"weapon both increases ",
					"its defensive ability, ",
					"and renders it more ",
					"useful in open spaces ",
					"as it allows attack "
				),
				new BookPageInfo
				(
					"against the opponent ",
					"without the need to ",
					"close. But be aware of ",
					"the limitations of thy ",
					"weapon! For a ",
					"weapon with great ",
					"reach may be useless ",
					"in close quarters, for "
				),
				new BookPageInfo
				(
					"lack of space to ",
					"maneuver it. Should ",
					"that dagger-wielding ",
					"enemy close on thee ",
					"and thy halberd, 'tis ",
					"best to flee.",
					"",
					"    Lastly, a factor "
				),
				new BookPageInfo
				(
					"that must always be ",
					"considered is the ",
					"condition of the ",
					"weapon. It might be a ",
					"wondrous magical ",
					"blade of surpassing ",
					"sharpness and it may ",
					"leap to block blows "
				),
				new BookPageInfo
				(
					"with a mind of its ",
					"own. It also might be ",
					"of such flimsy ",
					"construction, or ",
					"damaged to such an ",
					"extent, that the first ",
					"time it clangs against ",
					"steel, 'twill  shatter "
				),
				new BookPageInfo
				(
					"into useless shards.",
					"    Seek ye a good ",
					"blacksmith should thy ",
					"weapon become ",
					"damaged, but be ",
					"aware that their ",
					"ministrations may ",
					"simply make the "
				),
				new BookPageInfo
				(
					"matter worse.",
					"    While mages of ",
					"some ability oft create",
					"magical weapons ",
					"which enhance skill, ",
					"are preternaturally ",
					"sharp, or incinerate ",
					"the enemy as they "
				),
				new BookPageInfo
				(
					"fall, to my mind the ",
					"greatest gift that they",
					"can grant a stout ",
					"sword is to make it ",
					"resistant to damage, ",
					"for thy own skill can ",
					"make up the ",
					"difference. Except "
				),
				new BookPageInfo
				(
					"for the fireball, but ",
					"if the corpse is ",
					"charred, then so will ",
					"be the possessions, ",
					"which maketh looting ",
					"difficult!",
					"",
					""
				)
			);

		public override BookContent DefaultContent{ get{ return Content; } }

		[Constructable]
		public APrimeronArms() : base( Utility.Random( 0xFEF, 2 ), false )
		{
		}

		public APrimeronArms( Serial serial ) : base( serial )
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