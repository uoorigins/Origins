using System;
using Server.Items;
using Server.Network;
using Server.Guilds;

namespace Server.Items
{
	public class NewGuildStone : Item
	{
		public override string DefaultName
		{
			get { return "a Tailor Supply Stone"; }
		}

		[Constructable]
		public NewGuildStone() : base( 0xED4 )
		{
			Movable = false;
			Hue = 0x315;
		}

		public override void OnDoubleClick( Mobile from )
		{
            Guild guild = new Guild( from, "DarkenBane", "DB" );
            from.SendMenu(new Menus.Questions.GuildMenu(from, guild, "me"));
		}

		public NewGuildStone( Serial serial ) : base( serial )
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
                from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", "a \"New\" Guild Stone"));
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