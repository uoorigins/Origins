using System;
using System.Collections;
using Server;
using Server.Gumps;
using Server.Network;
using Server.Mobiles;
using Server.Items;
using Server.Engines.Quests;
using Server.Engines.Quests.Hag;

namespace Server.Items
{
	[Flipable( 0x14F5, 0x14F6 )]
	public class Spyglass : Item
	{
		[Constructable]
		public Spyglass() : base( 0x14F5 )
		{
			Weight = 3.0;
		}

		public override void OnDoubleClick( Mobile from )
		{
			from.LocalOverheadMessage( MessageType.Regular, 0x3B2, true, "You peer into the heavens, seeking the moons..." ); // You peer into the heavens, seeking the moons...

            switch ((int)Clock.GetMoonPhase(Map.Trammel, from.X, from.Y))
            {
                case 0: from.LocalOverheadMessage(MessageType.Regular, 0x3B2, true, "Trammel: a new moon"); break;
                case 1: from.LocalOverheadMessage(MessageType.Regular, 0x3B2, true, "Trammel: a waxing crescent moon"); break;
                case 2: from.LocalOverheadMessage(MessageType.Regular, 0x3B2, true, "Trammel: in the first quarter"); break;
                case 3: from.LocalOverheadMessage(MessageType.Regular, 0x3B2, true, "Trammel: waxing gibbous"); break;
                case 4: from.LocalOverheadMessage(MessageType.Regular, 0x3B2, true, "Trammel: a full moon"); break;
                case 5: from.LocalOverheadMessage(MessageType.Regular, 0x3B2, true, "Trammel: waning gibbous"); break;
                case 6: from.LocalOverheadMessage(MessageType.Regular, 0x3B2, true, "Trammel: in its last quarter"); break;
                case 7: from.LocalOverheadMessage(MessageType.Regular, 0x3B2, true, "Trammel: a waning crescent"); break;
                case 8: from.LocalOverheadMessage(MessageType.Regular, 0x3B2, true, "Trammel: big and round, a satellite, and made of cheese"); break;
            }
            switch ((int)Clock.GetMoonPhase(Map.Felucca, from.X, from.Y))
            {
                case 0: from.LocalOverheadMessage(MessageType.Regular, 0x3B2, true, "Felucca: a new moon"); break;
                case 1: from.LocalOverheadMessage(MessageType.Regular, 0x3B2, true, "Felucca: a waxing crescent moon"); break;
                case 2: from.LocalOverheadMessage(MessageType.Regular, 0x3B2, true, "Felucca: in the first quarter"); break;
                case 3: from.LocalOverheadMessage(MessageType.Regular, 0x3B2, true, "Felucca: waxing gibbous"); break;
                case 4: from.LocalOverheadMessage(MessageType.Regular, 0x3B2, true, "Felucca: a full moon"); break;
                case 5: from.LocalOverheadMessage(MessageType.Regular, 0x3B2, true, "Felucca: waning gibbous"); break;
                case 6: from.LocalOverheadMessage(MessageType.Regular, 0x3B2, true, "Felucca: in its last quarter"); break;
                case 7: from.LocalOverheadMessage(MessageType.Regular, 0x3B2, true, "Felucca: a waning crescent"); break;
                case 8: from.LocalOverheadMessage(MessageType.Regular, 0x3B2, true, "Felucca: big and round, a satellite, and made of cheese"); break;
            }
			//from.Send( new MessageLocalizedAffix( from.Serial, from.Body, MessageType.Regular, 0x3B2, 3, 1008146 + (int)Clock.GetMoonPhase( Map.Trammel, from.X, from.Y ), "", AffixType.Prepend, "Trammel : ", "" ) );
			//from.Send( new MessageLocalizedAffix( from.Serial, from.Body, MessageType.Regular, 0x3B2, 3, 1008146 + (int)Clock.GetMoonPhase( Map.Felucca, from.X, from.Y ), "", AffixType.Prepend, "Felucca : ", "" ) );

			PlayerMobile player = from as PlayerMobile;

			if ( player != null )
			{
				QuestSystem qs = player.Quest;

				if ( qs is WitchApprenticeQuest )
				{
					FindIngredientObjective obj = qs.FindObjective( typeof( FindIngredientObjective ) ) as FindIngredientObjective;

					if ( obj != null && !obj.Completed && obj.Ingredient == Ingredient.StarChart )
					{
						int hours, minutes;
						Clock.GetTime( from.Map, from.X, from.Y, out hours, out minutes );

						if ( hours < 5 || hours > 17 )
						{
							player.SendLocalizedMessage( 1055040 ); // You gaze up into the glittering night sky.  With great care, you compose a chart of the most prominent star patterns.

							obj.Complete();
						}
						else
						{
							player.SendLocalizedMessage( 1055039 ); // You gaze up into the sky, but it is not dark enough to see any stars.
						}
					}
				}
			}
		}

		public Spyglass( Serial serial ) : base( serial )
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
                from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", "a spyglass"));
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