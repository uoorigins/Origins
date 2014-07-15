using System;
using System.Collections.Generic;
using Server;
using Server.Multis;
using Server.Targeting;

namespace Server.Items
{
	[Flipable( 0x14F0, 0x14EF )]
	public abstract class BaseAddonDeed : Item
	{
		public abstract BaseAddon Addon{ get; }

		public BaseAddonDeed() : base( 0x14F0 )
		{
			Weight = 1.0;

			if ( !Core.AOS )
				LootType = LootType.Newbied;
		}

		public BaseAddonDeed( Serial serial ) : base( serial )
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

			if ( Weight == 0.0 )
				Weight = 1.0;
		}

		public override void OnDoubleClick( Mobile from )
		{
			if ( IsChildOf( from.Backpack ) )
				from.Target = new InternalTarget( this );
			else
                from.SendLocalizedMessage( "That must be in your pack for you to use it." ); // That must be in your pack for you to use it.
		}

		private class InternalTarget : Target
		{
			private BaseAddonDeed m_Deed;

			public InternalTarget( BaseAddonDeed deed ) : base( -1, true, TargetFlags.None )
			{
				m_Deed = deed;

				CheckLOS = false;
			}

			protected override void OnTarget( Mobile from, object targeted )
			{
				IPoint3D p = targeted as IPoint3D;
				Map map = from.Map;

				if ( p == null || map == null || m_Deed.Deleted )
					return;

				if ( m_Deed.IsChildOf( from.Backpack ) )
				{
					BaseAddon addon = m_Deed.Addon;

					Server.Spells.SpellHelper.GetSurfaceTop( ref p );

					BaseHouse house = null;

					AddonFitResult res = addon.CouldFit( p, map, from, ref house );

					if ( res == AddonFitResult.Valid )
						addon.MoveToWorld( new Point3D( p ), map );
					else if ( res == AddonFitResult.Blocked )
                        from.SendLocalizedMessage( "You cannot build that there." ); // You cannot build that there.
					else if ( res == AddonFitResult.NotInHouse )
                        from.SendLocalizedMessage( "You can only place this in a house that you own!" ); // You can only place this in a house that you own!
					else if ( res == AddonFitResult.DoorTooClose )
                        from.SendLocalizedMessage( "You cannot build near the door." ); // You cannot build near the door.
					else if ( res == AddonFitResult.NoWall )
                        from.SendLocalizedMessage( "This object needs to be mounted on something." ); // This object needs to be mounted on something.
					
					if ( res == AddonFitResult.Valid )
					{
						m_Deed.Delete();
						house.Addons.Add( addon );
					}
					else
					{
						addon.Delete();
					}
				}
				else
				{
                    from.SendLocalizedMessage( "That must be in your pack for you to use it." ); // That must be in your pack for you to use it.
				}
			}
		}
	}
}