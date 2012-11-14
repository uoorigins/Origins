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
		public ParalyzeWand() : base( WandEffect.Paralyze, 10, 25 )
		{
		}

		public ParalyzeWand( Serial serial ) : base( serial )
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
                if (IsInIDList(from) || from.AccessLevel >= AccessLevel.GameMaster)
                {
                    from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", String.Format("a wand of ghoul's touch ({0} charges)", Charges)));
                }
                else
                {
                    from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", "a magic wand"));                   
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
		}

		public override void OnWandUse( Mobile from )
		{
            Cast(new ParalyzeSpell(from, this));
		}
	}
}