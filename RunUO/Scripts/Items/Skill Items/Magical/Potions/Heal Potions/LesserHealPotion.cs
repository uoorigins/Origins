using System;
using Server;
using Server.Network;

namespace Server.Items
{
	public class LesserHealPotion : BaseHealPotion
	{
		public override int MinHeal { get { return (Core.AOS ? 6 : 3); } }
		public override int MaxHeal { get { return (Core.AOS ? 8 : 10); } }
		public override double Delay{ get{ return (Core.AOS ? 3.0 : 10.0); } }

		[Constructable]
		public LesserHealPotion() : base( PotionEffect.HealLesser )
		{
		}

		public LesserHealPotion( Serial serial ) : base( serial )
		{
		}

        public override void OnSingleClick(Mobile from)
        {
            if (this.Name != null)
            {
                from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", this.Name));
            }
            else
            {
                from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", "a Lesser Heal potion"));
            }
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