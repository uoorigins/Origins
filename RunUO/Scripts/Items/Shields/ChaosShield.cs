using System;
using Server;
using Server.Guilds;
using Server.Network;

namespace Server.Items
{
	public class ChaosShield : BaseShield
	{
		public override int BasePhysicalResistance{ get{ return 1; } }
		public override int BaseFireResistance{ get{ return 0; } }
		public override int BaseColdResistance{ get{ return 0; } }
		public override int BasePoisonResistance{ get{ return 0; } }
		public override int BaseEnergyResistance{ get{ return 0; } }

		public override int InitMinHits{ get{ return 100; } }
		public override int InitMaxHits{ get{ return 125; } }

		public override int AosStrReq{ get{ return 95; } }
        public override int OldStrReq { get { return 0; } }

		public override int ArmorBase{ get{ return 32; } }

		[Constructable]
		public ChaosShield() : base( 0x1BC3 )
		{
			if ( !Core.AOS )
				LootType = LootType.Newbied;

			Weight = 5.0;
		}

		public ChaosShield( Serial serial ) : base(serial)
		{
		}

        public override void OnSingleClick(Mobile from)
        {

            string durabilitylevel = GetDurabilityLevel();
            string protectionlevel = GetProtectionLevel();
            string beginning;

            if (durabilitylevel == "indestructible")
            {
                beginning = "an ";
            }
            else
            {
                beginning = "a ";
            }

            if (this.Name != null)
            {
                from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", this.Name));
            }
            else
            {
                if (this.Quality == ArmorQuality.Exceptional)
                {
                    if (this.Crafter != null)
                        from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", String.Format("an exceptional chaos shield (crafted by {0})", this.Crafter.Name)));
                    else
                        from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", "an exceptional chaos shield"));
                }
                else if (IsInIDList(from) == false && (this.ProtectionLevel != ArmorProtectionLevel.Regular || this.Durability != ArmorDurabilityLevel.Regular))
                {
                    from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", "a magic chaos shield"));
                }
                else if (IsInIDList(from) == true)
                {
                    if (this.Durability > ArmorDurabilityLevel.Regular && this.ProtectionLevel == ArmorProtectionLevel.Regular)
                    {
                        from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", beginning + durabilitylevel + " chaos shield"));
                    }
                    else if (this.ProtectionLevel > ArmorProtectionLevel.Regular && this.Durability == ArmorDurabilityLevel.Regular)
                    {
                        from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", "a chaos shield " + protectionlevel));
                    }
                    else if (this.ProtectionLevel > ArmorProtectionLevel.Regular && this.Durability > ArmorDurabilityLevel.Regular)
                    {
                        from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", beginning + durabilitylevel + " chaos shield " + protectionlevel));
                    }
                    else
                    {
                        from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", "a chaos shield"));
                    }
                }
                else
                {
                    from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", "a chaos shield"));
                }
            }
        }

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int)0 );//version
		}

        public override bool OnEquip(Mobile from)
        {
            return Validate(from) && base.OnEquip(from);
        }

        public override bool OnDroppedToWorld(Mobile from, Point3D p)
        {
            from.FixedEffect(0x3728, 10, 13);
            Delete();
            return false;
        }

        public override bool OnDroppedToMobile(Mobile from, Mobile target)
        {
            if ((target.Karma < 110 || target.Backpack.FindItemByType(typeof(OrderShield)) != null || target.FindItemOnLayer(Layer.TwoHanded) is OrderShield) && target.Player)
            {
                from.FixedEffect(0x3728, 10, 13);
                Delete();
                return false;
            }
            else
                return base.OnDroppedToMobile(from, target);
        }

        public override bool OnDroppedInto(Mobile from, Container target, Point3D p)
        {
            if (target == from.Backpack && from.Karma >= 110)
                return base.OnDroppedInto(from, target, p);
            else
            {
                from.FixedEffect(0x3728, 10, 13);
                Delete();
                return false;
            }
        }

        public override bool OnDroppedOnto(Mobile from, Item target)
        {
            if (target == from.Backpack && from.Karma >= 110)
                return base.OnDroppedOnto(from, target);
            else
            {
                from.FixedEffect(0x3728, 10, 13);
                Delete();
                return false;
            }
        }

		/*public override void OnSingleClick( Mobile from )
		{
			if ( Validate( Parent as Mobile ) )
				base.OnSingleClick( from );
		}*/

		public virtual bool Validate( Mobile m )
		{
			if ( m == null || !m.Player || m.AccessLevel != AccessLevel.Player || Core.AOS )
				return true;

            if (m.Karma < 110)
            {
                m.FixedEffect(0x3728, 10, 13);
                Delete();

                return false;
            }

			/*Guild g = m.Guild as Guild;

			if ( g == null || g.Type != GuildType.Chaos )
			{
				m.FixedEffect( 0x3728, 10, 13 );
				Delete();

				return false;
			}*/

			return true;
		}
	}
}