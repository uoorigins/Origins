using System;
using Server;
using Server.Misc;
using Server.Items;
using Server.Guilds;

namespace Server.Mobiles
{
	public class OrderGuard : BaseShieldGuard
	{
		public override int Keyword{ get{ return 0x21; } } // *order shield*
		public override BaseShield Shield{ get{ return new OrderShield(); } }
		public override int SignupNumber{ get{ return 1007141; } } // Sign up with a guild of order if thou art interested.
		public override GuildType Type{ get{ return GuildType.Order; } }
        public override bool ClickTitle { get { return false; } }

		public override bool BardImmune{ get{ return true; } }

        /*public static TimeSpan TalkDelay = TimeSpan.FromSeconds(120.0);
        public DateTime m_NextTalk;

        public override void OnMovement(Mobile m, Point3D oldLocation)
        {
            if (DateTime.Now >= m_NextTalk && InRange(m, 2) && !InRange(oldLocation, 2) && InLOS(m)) // check if its time to talk + Player in range.
            {
                m_NextTalk = DateTime.Now + TalkDelay; // set next talk time
                
                if (m.FindItemOnLayer(Layer.TwoHanded) is OrderShield || m.Backpack.FindItemByType(typeof(OrderShield)) != null)
                {
                }
                else if (m.Karma == 127)
                {
                    switch (Utility.Random(10))
                    {
                        case 0: Say(true, "Thou hast the look of a likely candidate for joining Lord British's guards."); Say(true, "Say 'order shield' if thou art interested."); break;
                        case 1: Say(true, "Wouldst thou be interested in joining British's guard?"); Say(true, "Say 'order shield' if thou art interested."); break;
                        case 2: Say(true, "British's guard hath been looking for folk like thee."); Say(true, "Say 'order shield' if thou art interested."); break;
                        case 3: Say(true, "Thou'rt a good and honest person. Care to join Lord British's guard?"); Say(true, "Say 'order shield' if thou art interested."); break;
                        case 4: Say(true, "If thou art interested in joining Lord British's guard, a place can be found for thee."); Say(true, "Say 'order shield' if thou art interested."); break;
                    };
                }

            }
        }*/

		[Constructable]
		public OrderGuard()
		{
		}

		public OrderGuard( Serial serial ) : base( serial )
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