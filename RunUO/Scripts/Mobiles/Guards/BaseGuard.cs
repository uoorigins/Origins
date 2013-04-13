using System;
using System.Collections;
using System.Collections.Generic;
using Server.Misc;
using Server.Items;
using Server.Mobiles;

namespace Server.Mobiles
{
	public abstract class BaseGuard : BaseCreature
	{
		public static void Spawn( Mobile caller, Mobile target )
		{
			Spawn( caller, target, 1, false );
		}

		public static void Spawn( Mobile caller, Mobile target, int amount, bool onlyAdditional )
		{
			if ( target == null || target.Deleted )
				return;

			foreach ( Mobile m in target.GetMobilesInRange( 15 ) )
			{
				if ( m is BaseGuard )
				{
					BaseGuard g = (BaseGuard)m;

					if ( g.Focus == null ) // idling
					{
						g.Focus = target;

						--amount;
					}
					else if ( g.Focus == target && !onlyAdditional )
					{
						--amount;
					}
				}
			}

            while (amount-- > 0)
                caller.Region.MakeGuard(target);
		}

        public BaseGuard(Mobile target) : base(AIType.AI_Berserk, FightMode.Closest, 14, 1, 0.1, 1)
		{
			if ( target != null )
			{
				Location = target.Location;
                X += Utility.RandomList(5, -5);
                Y += Utility.RandomList(5, -5);
                Z = Map.Tiles.GetLandTile(X, Y).Z;
				Map = target.Map;

				Effects.SendLocationParticles( EffectItem.Create( Location, Map, EffectItem.DefaultDuration ), 0x3728, 10, 10, 5023 );
			}
		}

		public BaseGuard( Serial serial ) : base( serial )
		{
		}

        public void GetSpeech()
        {
            if (Utility.Random(3) == 1)
            {
                switch (Utility.Random(15))
                {
                    case 0: Say(true, "Er... thanks."); break;
                    case 1: Say(true, "I really hope that wasn't intended as a bribe."); break;
                    case 2: Say(true, "How disgusting!  I'll dispose of this."); break;
                    case 3: Say(true, "Er... thanks."); break;
                    case 4: Say(true, "Er... thanks."); break;
                    case 5: Say(true, "If this were the head of a murderer, I would check for a bounty."); break;
                    case 6: Say(true, "I shall place this on my mantle!"); break;
                    case 7: Say(true, "This tasteth like chicken."); break;
                    case 8: Say(true, "This tasteth just like the juicy peach I just ate."); break;
                    case 9: Say(true, "Ahh!  That was the one piece I was missing!"); break;
                    case 10: Say(true, "Somehow, it reminds me of mother."); break;
                    case 11: Say(true, "It's a sign!  I can see Elvis in this!"); break;
                    case 12: Say(true, "Thanks, I was missing mine."); break;
                    case 13: Say(true, "I'll put this in the lost-and-found box."); break;
                    case 14: Say(true, "My family will eat well tonight!"); break;
                }
            }
            else
            {
                Say(true, "'Tis a decapitated head. How disgusting.");
            }
        }

        public class GiveBountyTimer : Timer
        {
            PlayerMobile pm;
            Mobile guard;
            Head head;
            Mobile from;

            public GiveBountyTimer(PlayerMobile b, Mobile g, Head h, Mobile f) : base(TimeSpan.FromSeconds(5))
            {
                pm = b;
                guard = g;
                head = h;
                from = f;
                Priority = TimerPriority.OneSecond;
            }

            protected override void OnTick()
            {
                if (pm.Bounty > 0)
                {
                    guard.Say(true, String.Format("The bounty on {0} was {1} gold, and has been credited to your account.", pm.Name, pm.Bounty));
                    pm.Bounty = 0;
                }
                /*else if (head.Killer != ((PlayerMobile)from))
                {
                    guard.Say(true, "I had heard that this scum was taken care of. But thou didst not do the deed, and thus shall not get the reward!");
                }*/
                else
                {
                    guard.Say(true, String.Format("There was no bounty on {0}.", pm.Name));
                }

                Stop();
                return;
            }
        }

        public override bool OnDragDrop( Mobile from, Item dropped )
        {
            if (dropped is Head)
            {
                Head ph = dropped as Head;

                Mobile p = World.FindMobile(ph.PlayerSerial);

                if (p != null && p is PlayerMobile && (ph.WhenKilled + TimeSpan.FromHours(24.0)) > DateTime.Now)
                {
                    PlayerMobile pm = p as PlayerMobile;
                    if (pm.Bounty > 0)
                    {
                        if (from.Karma < 1)
                        {
                            Say(true, "We only accept bounty hunting from honorable folk! Away with thee!");
                            return false;
                        }

                        Say(true, "Ah, a head!  Let me check to see if there is a bounty on this.");
                        if (Banker.Deposit(from, pm.Bounty))
                        {
                            Timer m_timer = new GiveBountyTimer(pm, this, ph, from);
                            m_timer.Start();
                            ph.Delete();
                            return true;
                        }
                        else
                        {
                            Say(true, String.Format("There is a bounty on {0}, but your bank box is full.", pm.Name));
                            return false;
                        }
                    }
                    else
                    {
                        if (from.Karma < 1)
                        {
                            Say(true, "We only accept bounty hunting from honorable folk! Away with thee!");
                            return false;
                        }
                        //GetSpeech();
                        Say(true, "Ah, a head!  Let me check to see if there is a bounty on this.");
                        Timer m_timer = new GiveBountyTimer(pm, this, ph, from);
                        m_timer.Start();
                        ph.Delete();
                        return true;
                    }
                }
                else
                {
                    GetSpeech();
                    ph.Delete();
                    return true;
                }
            }
            return base.OnDragDrop(from, dropped);
        }

		public override bool OnBeforeDeath()
		{
			Effects.SendLocationParticles( EffectItem.Create( Location, Map, EffectItem.DefaultDuration ), 0x3728, 10, 10, 2023 );

			PlaySound( 0x1FE );

			Delete();

			return false;
		}

		public abstract Mobile Focus{ get; set; }

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