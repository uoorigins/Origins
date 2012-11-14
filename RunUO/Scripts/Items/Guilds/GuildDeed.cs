using System;
using Server.Network;
using Server.Prompts;
using Server.Guilds;
using Server.Multis;
using Server.Regions;

namespace Server.Items
{
	public class GuildDeed : Item
	{
		public override int LabelNumber{ get{ return 1041055; } } // a guild deed

		[Constructable]
		public GuildDeed() : base( 0x14F0 )
		{
			Weight = 1.0;
		}

		public GuildDeed( Serial serial ) : base( serial )
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
                from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", "a guild deed"));
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

			if ( Weight == 0.0 )
				Weight = 1.0;
		}

		public override void OnDoubleClick( Mobile from )
		{
			if( Guild.NewGuildSystem )
				return;

			if ( !IsChildOf( from.Backpack ) )
			{
				from.SendAsciiMessage( "That must be in your pack for you to use it." ); // That must be in your pack for you to use it.
			}
			else if ( from.Guild != null )
			{
				from.SendAsciiMessage( "You must resign from your current guild before founding another!" ); // You must resign from your current guild before founding another!
			}
			else
			{
				BaseHouse house = BaseHouse.FindHouseAt( from );
                BaseBoat boat = BaseBoat.FindBoatAt(from.Location, from.Map);

				if ( house == null && boat == null )
				{
                    from.SendAsciiMessage("You can only place a guildstone in a house or on a ship."); // You can only place a guildstone in a house.
				}
                else if ((house != null && (house.FindGuildstone() != null)) || (boat != null && (boat.FindGuildstone() != null)))
				{
                    from.SendAsciiMessage("Only one guildstone may reside in a given house or ship.");//Only one guildstone may reside in a given house.
				}
                else if ((house != null && (!Key.ContainsKey(from.Backpack, house.keyValue))) || (boat != null && !Key.ContainsKey(from.Backpack, boat.PPlank.KeyValue)))
				{
					from.SendAsciiMessage( "You can only place a guildstone in a house or ship you own!" ); // You can only place a guildstone in a house you own!
				}
				else
				{
					from.SendAsciiMessage( "Enter new guild name (40 characters max):" ); // Enter new guild name (40 characters max):
					from.Prompt = new InternalPrompt( this );
				}
			}
		}

		private class InternalPrompt : Prompt
		{
			private GuildDeed m_Deed;

			public InternalPrompt( GuildDeed deed )
			{
				m_Deed = deed;
			}

			public override void OnResponse( Mobile from, string text )
			{
				if ( m_Deed.Deleted )
					return;

				if ( !m_Deed.IsChildOf( from.Backpack ) )
				{
					from.SendAsciiMessage( "That must be in your pack for you to use it." ); // That must be in your pack for you to use it.
				}
				else if ( from.Guild != null )
				{
					from.SendAsciiMessage( "You must resign from your current guild before founding another!" ); // You must resign from your current guild before founding another!
				}
				else
				{
					BaseHouse house = BaseHouse.FindHouseAt( from );
                    BaseBoat boat = BaseBoat.FindBoatAt(from.Location, from.Map);

                    if (house == null && boat == null)
                    {
                        from.SendAsciiMessage("You can only place a guildstone in a house or on a ship."); // You can only place a guildstone in a house.
                    }
                    else if ((house != null && (house.FindGuildstone() != null)) || (boat != null && (boat.FindGuildstone() != null)))
                    {
                        from.SendAsciiMessage("Only one guildstone may reside in a given house or ship.");//Only one guildstone may reside in a given house.
                    }
                    else if ((house != null && (!Key.ContainsKey(from.Backpack, house.keyValue))) || (boat != null && !Key.ContainsKey(from.Backpack, boat.PPlank.KeyValue)))
                    {
                        from.SendAsciiMessage("You can only place a guildstone in a house or ship you own!"); // You can only place a guildstone in a house you own!
                    }
					else
					{
						m_Deed.Delete();

						if ( text.Length > 40 )
							text = text.Substring( 0, 40 );

						Guild guild = new Guild( from, text, text );

						from.Guild = guild;
						from.GuildTitle = "Guildmaster";

						Guildstone stone = new Guildstone( guild );

						stone.MoveToWorld( from.Location, from.Map );

						guild.Guildstone = stone;
					}
				}
			}

			public override void OnCancel( Mobile from )
			{
				from.SendAsciiMessage( "Placement of guildstone cancelled." ); // Placement of guildstone cancelled.
			}
		}
	}
}
