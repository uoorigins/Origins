using System;
using Server;
using Server.Misc;
using Server.Items;
using Server.Guilds;

namespace Server.Mobiles
{
	public abstract class BaseShieldGuard : BaseCreature
	{
		public BaseShieldGuard() : base( AIType.AI_Melee, FightMode.Aggressor, 14, 1, 0.8, 1.6 )
		{
			InitStats( 1000, 1000, 1000 );
			Title = "the guard";

			SpeechHue = Utility.RandomDyedHue();

			Hue = Utility.RandomSkinHue();

			if ( Female = Utility.RandomBool() )
			{
				Body = 0x191;
				Name = NameList.RandomName( "female" );

				AddItem( new FemalePlateChest() );
				AddItem( new PlateArms() );
				AddItem( new PlateLegs() );

                if (Shield is OrderShield)
                {
                    switch (Utility.Random(2))
                    {
                        case 0: AddItem(new Doublet(Utility.RandomBlueHue())); break;
                        case 1: AddItem(new BodySash(Utility.RandomBlueHue())); break;
                    }

                    switch (Utility.Random(2))
                    {
                        case 0: AddItem(new Skirt(Utility.RandomBlueHue())); break;
                        case 1: AddItem(new Kilt(Utility.RandomBlueHue())); break;
                    }
                }
                else
                {
                    switch (Utility.Random(2))
                    {
                        case 0: AddItem(new Doublet(Utility.RandomRedHue())); break;
                        case 1: AddItem(new BodySash(Utility.RandomRedHue())); break;
                    }

                    switch (Utility.Random(2))
                    {
                        case 0: AddItem(new Skirt(Utility.RandomRedHue())); break;
                        case 1: AddItem(new Kilt(Utility.RandomRedHue())); break;
                    }
                }
			}
			else
			{
				Body = 0x190;
				Name = NameList.RandomName( "male" );

				AddItem( new PlateChest() );
				AddItem( new PlateArms() );
				AddItem( new PlateLegs() );

                if (Shield is OrderShield)
                {
                    switch (Utility.Random(3))
                    {
                        case 0: AddItem(new Doublet(Utility.RandomBlueHue())); break;
                        case 1: AddItem(new Tunic(Utility.RandomBlueHue())); break;
                        case 2: AddItem(new BodySash(Utility.RandomBlueHue())); break;
                    }
                }
                else
                {
                    switch (Utility.Random(3))
                    {
                        case 0: AddItem(new Doublet(Utility.RandomRedHue())); break;
                        case 1: AddItem(new Tunic(Utility.RandomRedHue())); break;
                        case 2: AddItem(new BodySash(Utility.RandomRedHue())); break;
                    }
                }
			}

			Utility.AssignRandomHair( this );
			if( Utility.RandomBool() )
				Utility.AssignRandomFacialHair( this, HairHue );

			VikingSword weapon = new VikingSword();
			weapon.Movable = false;
			AddItem( weapon );

			BaseShield shield = Shield;
			shield.Movable = false;
			AddItem( shield );

			PackGold( 250, 500 );

			Skills[SkillName.Anatomy].Base = 120.0;
			Skills[SkillName.Tactics].Base = 120.0;
			Skills[SkillName.Swords].Base = 120.0;
			Skills[SkillName.MagicResist].Base = 120.0;
			Skills[SkillName.DetectHidden].Base = 100.0;
		}

		public abstract int Keyword{ get; }
		public abstract BaseShield Shield{ get; }
		public abstract int SignupNumber{ get; }
		public abstract GuildType Type{ get; }

		public override bool HandlesOnSpeech( Mobile from )
		{
			if ( from.InRange( this.Location, 2 ) )
				return true;

			return base.HandlesOnSpeech( from );
		}

		public override void OnSpeech( SpeechEventArgs e )
		{
            if (!e.Handled && e.HasKeyword(0x003B) && e.Mobile.InRange(this.Location, 2)) //hello
            {
                e.Handled = true;

                Mobile m = e.Mobile;

                if (Shield is OrderShield && (m.FindItemOnLayer(Layer.TwoHanded) is OrderShield || (m.Backpack != null && m.Backpack.FindItemByType(typeof(OrderShield)) != null)))
                {
                    switch (Utility.Random(10))
                    {
                        case 0: Say(true, "Greetings, fellow guard."); break;
                        case 1: Say(true, "In the name of our liege, greetings!"); break;
                        case 2: Say(true, "Greetings, my friend."); break;
                        case 3: Say(true, "Hail, my friend."); break;
                        case 4: Say(true, "Hail and well met!"); break;
                    };
                }
                else if (Shield is ChaosShield && (m.FindItemOnLayer(Layer.TwoHanded) is ChaosShield || (m.Backpack != null && m.Backpack.FindItemByType(typeof(ChaosShield)) != null)))
                {
                    switch (Utility.Random(10))
                    {
                        case 0: Say(true, "Greetings, fellow guard."); break;
                        case 1: Say(true, "In the name of our liege, greetings!"); break;
                        case 2: Say(true, "Greetings, my friend."); break;
                        case 3: Say(true, "Hail, my friend."); break;
                        case 4: Say(true, "Hail and well met!"); break;
                    };
                }
                else if (Shield is OrderShield && (m.FindItemOnLayer(Layer.TwoHanded) is ChaosShield || (m.Backpack != null && m.Backpack.FindItemByType(typeof(ChaosShield)) != null)))
                {
                        switch (Utility.Random(0, 7))
                        {
                            case 0: Say(true, "Stay away, lest our rivalry develop into something worse!"); break;
                            case 1: Say(true, "Thou'rt not of my brotherhood! Away with thee!"); break;
                            case 2: Say(true, "Whilst I grant respect to thy lord, I mislike thy emblem."); break;
                            case 3: Say(true, "Art thou here to harass me?"); break;
                            case 4: Say(true, "Tch tch... thou wearest the wrong emblem!"); break;
                            case 5: Say(true, "'Tis a pity that thou art in the wrong camp!"); break;
                            case 6: Say(true, "There is a rivalry between thy group and mine--be careful."); break;
                            case 7: Say(true, "Is not thy emblem a sign that thou art a member of our rival guards?"); break;
                        }
                }
                else if (Shield is ChaosShield && (m.FindItemOnLayer(Layer.TwoHanded) is OrderShield || (m.Backpack != null && m.Backpack.FindItemByType(typeof(OrderShield)) != null)))
                {
                    switch (Utility.Random(0, 7))
                    {
                        case 0: Say(true, "Stay away, lest our rivalry develop into something worse!"); break;
                        case 1: Say(true, "Thou'rt not of my brotherhood! Away with thee!"); break;
                        case 2: Say(true, "Whilst I grant respect to thy lord, I mislike thy emblem."); break;
                        case 3: Say(true, "Art thou here to harass me?"); break;
                        case 4: Say(true, "Tch tch... thou wearest the wrong emblem!"); break;
                        case 5: Say(true, "'Tis a pity that thou art in the wrong camp!"); break;
                        case 6: Say(true, "There is a rivalry between thy group and mine--be careful."); break;
                        case 7: Say(true, "Is not thy emblem a sign that thou art a member of our rival guards?"); break;
                    }
                }
                else if (m.Karma == 127)
                {
                    if (Shield is OrderShield)
                    {
                        switch (Utility.Random(5))
                        {
                            case 0: Say(true, "Thou hast the look of a likely candidate for joining Lord British's guards."); break;
                            case 1: Say(true, "Wouldst thou be interested in joining British's guard?"); break;
                            case 2: Say(true, "British's guard hath been looking for folk like thee."); break;
                            case 3: Say(true, "Thou'rt a good and honest person. Care to join Lord British's guard?"); break;
                            case 4: Say(true, "If thou art interested in joining Lord British's guard, a place can be found for thee."); break;
                        };


                        Say(true, "Say 'order shield' if thou art interested.");
                    }
                    else if (Shield is ChaosShield)
                    {
                        switch (Utility.Random(5))
                        {
                            case 0: Say(true, "Thou hast the look of a likely candidate for joining Lord Blackthorn's guards."); break;
                            case 1: Say(true, "Wouldst thou be interested in joining Blackthorn's guard?"); break;
                            case 2: Say(true, "Blackthorn's guard hath been looking for folk like thee."); break;
                            case 3: Say(true, "Thou'rt a good and honest person. Care to join Lord Blackthorn's guard?"); break;
                            case 4: Say(true, "If thou art interested in joining Lord Blackthorn's guard, a place can be found for thee."); break;
                        };

                        Say(true, "Say 'chaos shield' if thou art interested.");
                    }
                }
                else if (m.Karma < 127)
                {
                    if (Shield is OrderShield)
                    {
                        Say(true, "Wouldst thou be interested in joining British's guard?");
                        Say(true, "Say 'order shield' if thou art interested.");
                    }
                    else if (Shield is ChaosShield)
                    {
                        Say(true, "Wouldst thou be interested in joining Blackthorn's guard?");
                        Say(true, "Say 'chaos shield' if thou art interested.");
                    }
                }
            }

            if (!e.Handled && (e.HasKeyword(Keyword) || e.HasKeyword(0x0020)) && e.Mobile.InRange(this.Location, 2)) //virtue guard, order shield, chaos shield
			{
				e.Handled = true;

                Mobile from = e.Mobile;

                if (from.Karma < 127)
                {
                    switch(Utility.Random(0,6))
                    {
                        case 0: Say(true, "Thou art not worthy of being a member of our fraternity."); break;
                        case 1: Say(true, "The guards will not accept thee until thy reputation improves."); break;
                        case 2: Say(true, "Thou hast not the unblemished record we expect from our members."); break;
                        case 3: Say(true, "Thy record is not good enough to join the guards."); break;
                        case 4: Say(true, "Only those of utmost probity are accepted into the guards."); break;
                        case 5: Say(true, "Only the very best of citizens may join the guards."); break;
                        case 6: Say(true, "Thou dost not qualify for the virtue guards; thy record is not good enough."); break;
                    }
                    if (from.Karma < -39)
                        Say(true, "Do not dishonor us by asking again, scum.");
                    if (from.Karma > 109)
                        Say(true, " Thou'rt extremely close, however.");
                }
                else
                {
                    Container pack = from.Backpack;
                    BaseShield shield = Shield;
                    Item twoHanded = from.FindItemOnLayer(Layer.TwoHanded);

                    if ((pack != null && pack.FindItemByType(shield.GetType()) != null) || (twoHanded != null && shield.GetType().IsAssignableFrom(twoHanded.GetType())))
                    {
                        switch (Utility.Random(0, 4))
                        {
                            case 0: Say(true, "Yes, thou'rt a virtue guard."); break;
                            case 1: Say(true, "Hmm? Yes, I am one. So art thou."); break;
                            case 2: Say(true, "Yes, as thou knowest, it is a great thing to be one!"); break;
                            case 3: Say(true, "Isn't it wonderful being a virtue guard?"); break;
                            case 4: Say(true, "Why dost thou ask about virtue guards when thou art one?"); break;
                        }
                        shield.Delete();
                    }
                    else if (pack != null && (Shield is OrderShield && pack.FindItemByType(typeof(ChaosShield)) != null) || (twoHanded != null && (Shield is OrderShield && twoHanded is ChaosShield)))
                    {
                        switch (Utility.Random(0, 7))
                        {
                            case 0: Say(true, "Stay away, lest our rivalry develop into something worse!"); break;
                            case 1: Say(true, "Thou'rt not of my brotherhood! Away with thee!"); break;
                            case 2: Say(true, "Whilst I grant respect to thy lord, I mislike thy emblem."); break;
                            case 3: Say(true, "Art thou here to harass me?"); break;
                            case 4: Say(true, "Tch tch... thou wearest the wrong emblem!"); break;
                            case 5: Say(true, "'Tis a pity that thou art in the wrong camp!"); break;
                            case 6: Say(true, "There is a rivalry between thy group and mine--be careful."); break;
                            case 7: Say(true, "Is not thy emblem a sign that thou art a member of our rival guards?"); break;
                        }
                        shield.Delete();
                    }
                    else if (pack != null && (Shield is ChaosShield && pack.FindItemByType(typeof(OrderShield)) != null) || (twoHanded != null && (Shield is ChaosShield && twoHanded is OrderShield)))
                    {
                        switch (Utility.Random(0, 7))
                        {
                            case 0: Say(true, "Stay away, lest our rivalry develop into something worse!"); break;
                            case 1: Say(true, "Thou'rt not of my brotherhood! Away with thee!"); break;
                            case 2: Say(true, "Whilst I grant respect to thy lord, I mislike thy emblem."); break;
                            case 3: Say(true, "Art thou here to harass me?"); break;
                            case 4: Say(true, "Tch tch... thou wearest the wrong emblem!"); break;
                            case 5: Say(true, "'Tis a pity that thou art in the wrong camp!"); break;
                            case 6: Say(true, "There is a rivalry between thy group and mine--be careful."); break;
                            case 7: Say(true, "Is not thy emblem a sign that thou art a member of our rival guards?"); break;
                        }
                        shield.Delete();
                    }
                    else if (from.PlaceInBackpack(shield))
                    {
                        Say(true, "Thy shield is in thy backpack. Be sure thou dost not lose thy reputation, or else thou shalt lose thy life with it.");
                        from.AddToBackpack(shield);
                    }
                    else
                    {
                        from.SendAsciiMessage("Your backpack is too full."); // Your backpack is too full.
                        shield.Delete();
                    }
                }

				/*if ( g == null || g.Type != Type )
				{
					Say( SignupNumber );
				}
				else
				{
					Container pack = from.Backpack;
					BaseShield shield = Shield;
					Item twoHanded = from.FindItemOnLayer( Layer.TwoHanded );

					if ( (pack != null && pack.FindItemByType( shield.GetType() ) != null) || ( twoHanded != null && shield.GetType().IsAssignableFrom( twoHanded.GetType() ) ) )
					{
						Say( 1007110 ); // Why dost thou ask about virtue guards when thou art one?
						shield.Delete();
					}
					else if ( from.PlaceInBackpack( shield ) )
					{
						Say( Utility.Random( 1007101, 5 ) );
						Say( 1007139 ); // I see you are in need of our shield, Here you go.
						from.AddToBackpack( shield );
					}
					else
					{
						from.SendLocalizedMessage( 502868 ); // Your backpack is too full.
						shield.Delete();
					}
				}*/
			}

			base.OnSpeech( e );
		}

		public BaseShieldGuard( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}
}