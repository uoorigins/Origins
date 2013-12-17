using System;
using Server;
using Server.Items;

namespace Server.Items
{
	public class AnimateDeadScroll : SpellScroll
	{
		 
		public AnimateDeadScroll() : this( 1 )
		{
		}

		 
		public AnimateDeadScroll( int amount ) : base( 100, 0x2260, amount )
		{
		}

		public AnimateDeadScroll( Serial serial ) : base( serial )
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