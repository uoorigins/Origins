using System;

namespace Server.Items
{
	public class LightSource : Item
	{
		[Constructable]
		public LightSource() : base( 0x1647 )
		{
			Layer = Layer.OuterLegs;
			Movable = false;
		}

        public override bool AllowEquipedCast( Mobile from )
        {
            return true;
        }

		public LightSource( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}
}