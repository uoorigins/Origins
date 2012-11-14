using System;
using Server.Network;
using Server.Items;
using Server.Targeting;
using Server.Mobiles;

namespace Server.Items
{
	[FlipableAttribute( 0xE81, 0xE82 )]
	public class ShepherdsCrook : BaseStaff
	{
        public override WeaponAbility PrimaryAbility { get { return WeaponAbility.CrushingBlow; } }
        public override WeaponAbility SecondaryAbility { get { return WeaponAbility.Disarm; } }

        public override int AosStrengthReq { get { return 20; } }
        public override int AosMinDamage { get { return 13; } }
        public override int AosMaxDamage { get { return 15; } }
        public override int AosSpeed { get { return 40; } }

        public override int OldStrengthReq { get { return 10; } }
        public override int OldMinDamage { get { return 3; } }
        public override int OldMaxDamage { get { return 12; } }
        public override int OldSpeed { get { return 30; } }

        public override int InitMinHits { get { return 31; } }
        public override int InitMaxHits { get { return 50; } }

		[Constructable]
		public ShepherdsCrook() : base( 0xE81 )
		{
			Weight = 4.0;
		}

		public ShepherdsCrook( Serial serial ) : base( serial )
		{
		}

        public override void OnSingleClick(Mobile from)
        {

            string durabilitylevel = GetDurabilityString();
            string accuracylevel = GetAccuracyString();
            string damagelevel = GetDamageString();
            string beginning;

            if ((durabilitylevel == "indestructible") || (accuracylevel == "accurate") || (accuracylevel == "eminently accurate") || (accuracylevel == "exceedingly accurate"))
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
                if (this.Quality == WeaponQuality.Exceptional)
                {
                    if (this.Crafter != null)
                        from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", String.Format("an exceptional shepherd's crook (crafted by {0})", this.Crafter.Name)));
                    else
                        from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", "an exceptional shepherd's crook"));
                }
                else if ((this.IsInIDList(from) == false) && ((this.DamageLevel != WeaponDamageLevel.Regular) || (Slayer == SlayerName.Silver) || (Effect != WeaponEffect.None) || (this.DurabilityLevel != WeaponDurabilityLevel.Regular) || (this.AccuracyLevel != WeaponAccuracyLevel.Regular)))
                {
                    from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", "a magic shepherd's crook"));
                }
                else if (IsInIDList(from) || from.AccessLevel >= AccessLevel.GameMaster)
                {
                    if ((this.DamageLevel > WeaponDamageLevel.Regular || Effect != WeaponEffect.None) && ((this.DurabilityLevel == WeaponDurabilityLevel.Regular) && (this.AccuracyLevel == WeaponAccuracyLevel.Regular)))
                    {
                        from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", (Slayer == SlayerName.Silver ? "a silver " : "a ") + "shepherd's crook " + damagelevel));
                    }
                    else if ((this.DurabilityLevel > WeaponDurabilityLevel.Regular) && ((this.DamageLevel == WeaponDamageLevel.Regular && Effect == WeaponEffect.None) && (this.AccuracyLevel == WeaponAccuracyLevel.Regular)))
                    {
                        from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", beginning + durabilitylevel + " shepherd's crook"));
                    }
                    else if ((this.AccuracyLevel > WeaponAccuracyLevel.Regular) && ((this.DamageLevel == WeaponDamageLevel.Regular && Effect == WeaponEffect.None) && (this.DurabilityLevel == WeaponDurabilityLevel.Regular)))
                    {
                        from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", beginning + accuracylevel + " shepherd's crook"));
                    }



                    else if (((this.DamageLevel > WeaponDamageLevel.Regular || Effect != WeaponEffect.None) && (this.DurabilityLevel > WeaponDurabilityLevel.Regular)) && (this.AccuracyLevel == WeaponAccuracyLevel.Regular))
                    {
                        from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", beginning + durabilitylevel + " shepherd's crook " + damagelevel));
                    }
                    else if ((this.DamageLevel > WeaponDamageLevel.Regular || Effect != WeaponEffect.None) && (this.AccuracyLevel > WeaponAccuracyLevel.Regular) && (this.DurabilityLevel == WeaponDurabilityLevel.Regular))
                    {
                        from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", beginning + accuracylevel + " shepherd's crook " + damagelevel));
                    }
                    else if ((this.DurabilityLevel > WeaponDurabilityLevel.Regular) && (this.AccuracyLevel > WeaponAccuracyLevel.Regular) && (this.DamageLevel == WeaponDamageLevel.Regular && Effect == WeaponEffect.None))
                    {
                        from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", beginning + durabilitylevel + ", " + accuracylevel + " shepherd's crook"));
                    }
                    else if ((this.DurabilityLevel > WeaponDurabilityLevel.Regular) && (this.AccuracyLevel > WeaponAccuracyLevel.Regular) && (this.DamageLevel > WeaponDamageLevel.Regular || Effect != WeaponEffect.None))
                    {
                        from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", beginning + durabilitylevel + ", " + accuracylevel + " shepherd's crook " + damagelevel));
                    }
                    else
                    {
                        from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", (Slayer == SlayerName.Silver ? "a silver " : "a ") + "shepherd's crook"));
                    }
                }
                else
                {
                    from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", "a shepherd's crook"));
                }
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

			if ( Weight == 2.0 )
				Weight = 4.0;
		}

		public override void OnDoubleClick( Mobile from )
		{
			from.SendAsciiMessage( "Target the animal you wish to herd." ); // Target the animal you wish to herd.
			from.Target = new HerdingTarget();
		}

		private class HerdingTarget : Target
		{
			public HerdingTarget() : base( 10, false, TargetFlags.None )
			{
			}

			protected override void OnTarget( Mobile from, object targ )
			{
				if ( targ is BaseCreature )
				{
					BaseCreature bc = (BaseCreature)targ;

					if ( bc.Body.IsAnimal )
					{
						if ( bc.Controlled )
						{
							bc.PrivateOverheadMessage( MessageType.Regular, 0x3B2, true, "That animal looks tame already.", from.NetState ); // That animal looks tame already.
						}
						else 
						{
							from.SendAsciiMessage( "Click where you wish the animal to go." ); // Click where you wish the animal to go.
							from.Target = new InternalTarget( bc );
						}
					}
					else
					{
						from.SendAsciiMessage( "That is not a herdable animal." ); // That is not a herdable animal.
					}
				}
				else
				{
					from.SendAsciiMessage( "You don't seem to be able to persuade that to move." ); // You don't seem to be able to persuade that to move.
				}
			}

			private class InternalTarget : Target
			{
				private BaseCreature m_Creature;

				public InternalTarget( BaseCreature c ) : base( 10, true, TargetFlags.None )
				{
					m_Creature = c;
				}

				protected override void OnTarget( Mobile from, object targ )
				{
					if ( targ is IPoint2D )
					{
						if ( from.CheckTargetSkill( SkillName.Herding, m_Creature, 0, 100 ) )
						{
							m_Creature.TargetLocation = new Point2D( (IPoint2D)targ );
							from.SendAsciiMessage( "The animal walks where it was instructed to." ); // The animal walks where it was instructed to.
						}
						else
						{
							from.SendAsciiMessage( "You don't seem to be able to persuade that to move." ); // You don't seem to be able to persuade that to move.
						}
					}
				}
			}
		}
	}
}
