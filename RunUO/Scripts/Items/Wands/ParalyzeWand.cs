using System;
using Server;
using Server.Spells.Fifth;
using Server.Targeting;
using Server.Network;

namespace Server.Items
{
	public class ParalyzeWand : BaseWand
	{
		[Constructable]
		public ParalyzeWand() : base( WandEffect.None, 0, 0 )
		{
		}

		public ParalyzeWand( Serial serial ) : base( serial )
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

		public override void OnWandUse( Mobile from )
		{
            Cast(new ParalyzeSpell(from, this));
		}
	}
}