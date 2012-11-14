using System;
using Server.Items;
using Server;
using Server.Misc;
using Server.Targeting;
using Server.Network;

namespace Server.Mobiles
{
	public class HarborMaster : BaseCreature
	{
		public override bool CanTeach { get { return false; } }
        public virtual bool IsInvulnerable { get { return false; } }

		[Constructable]
		public HarborMaster()
			: base( AIType.AI_Animal, FightMode.None, 10, 1, 0.2, 0.4 )
		{
			InitStats( 31, 41, 51 );

			SetSkill( SkillName.Mining, 36, 68 );


			SpeechHue = Utility.RandomDyedHue();
			Hue = Utility.RandomSkinHue();
			Blessed = true;


			if( this.Female = Utility.RandomBool() )
			{
				this.Body = 0x191;
				this.Name = NameList.RandomName( "female" );
				Title = "the Harbor Mistress";
			}
			else
			{
				this.Body = 0x190;
				this.Name = NameList.RandomName( "male" );
				Title = "the Harbor Master";
			}
			AddItem( PlainShirt( Utility.RandomAllColors() ) );
			AddItem( RandomBoots() );
			AddItem( PlainPants( Utility.RandomAllColors() ) );
			AddItem( new QuarterStaff() );

			Utility.AssignRandomHair( this );

			Container pack = new Backpack();

			pack.Movable = false;

			AddItem( pack );
		}

        public override void GenerateLoot()
        {
            AddLoot(LootPack.Poor);
            AddLootPouch(LootPack.PoorPouch);
            AddLoot(LootPack.PoorPile);
        }

		public override bool ClickTitle { get { return false; } }


		public HarborMaster( Serial serial ) : base( serial )
		{
		}

        public override bool HandlesOnSpeech( Mobile from )
		{
			if ( from.InRange( this.Location, 4 ) )
				return true;

			return base.HandlesOnSpeech( from );  
        }

        public override void OnSpeech(SpeechEventArgs e)
        {
            if (CheckHome())
            {
                e.Handled = true;
                return;
            }

            if (!e.Handled && e.HasKeyword(0x000A) && e.Mobile.InRange(this.Location, 4))
            {
                e.Handled = true;
                Say(true, "I am a harbormaster.  I dock ships for a fee.");
            }

            if (!e.Handled && e.HasKeyword(0x0009) && e.Mobile.InRange(this.Location, 4))
            {

                e.Handled = true;
                Say(true, "If you already gave me a ship, just use your claim ticket as you would any other deed.");
            }

            if (!e.Handled && e.HasKeyword(0x000B) && e.Mobile.InRange(this.Location, 4))
            {
                e.Handled = true;
                Mobile m = e.Mobile;
                Say(true, "I charge 25 gold for docking thy ship.  What ship do you want to dock?");
                m.Target = new InternalTarget(this);
            }
        }

        private class InternalTarget : Target
        {
            private Mobile m_Master;

            public InternalTarget(Mobile master) : base(20, false, TargetFlags.None)
            {
                m_Master = master;
            }

            protected override void OnTarget(Mobile from, object target)
            {
                if (target is TillerMan)
                {
                    if (!m_Master.InRange(((TillerMan)target).Location, 20))
                    {
                        from.SendAsciiMessage("That is too far away.");
                        return;
                    }

                    ((TillerMan)target).Boat.BeginDryDock(from, m_Master);
                }
                else
                {
                    from.SendAsciiMessage("That is not a ship!");
                }
            }
        }

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int)0 ); // version 
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}
}
