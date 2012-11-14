using System;
using System.Collections.Generic;
using System.Text;
using Server.Network;

namespace Server.Items
{
    public class GraveStoneOne : Item
    {
        int m_Type;

        [Constructable]
        public GraveStoneOne(int type) : base(6255)
        {
            Weight = 255.0;
            m_Type = type;
            Movable = false;

            switch (type)
            {
                case 1:
                    this.ItemID = 4461; break;
                case 2:
                    this.ItemID = 4461; break;
                case 3:
                    this.ItemID = 4461; break;
                case 4:
                    this.ItemID = 4461; break;
                case 11:
                    this.ItemID = 4470; break;
                case 12:
                    this.ItemID = 4470; break;
                case 13:
                    this.ItemID = 4470; break;
                case 14:
                    this.ItemID = 4470; break;
                case 21:
                    this.ItemID = 3797; break;
                case 22:
                    this.ItemID = 3797; break;
                case 23:
                    this.ItemID = 3797; break;
                case 24:
                    this.ItemID = 3797; break;
                case 25:
                    this.ItemID = 3797; break;
                case 31:
                    this.ItemID = 4476; break;
                case 32:
                    this.ItemID = 4476; break;
                case 33:
                    this.ItemID = 4476; break;
                case 34:
                    this.ItemID = 4476; break;
                case 41:
                    this.ItemID = 4471; break;
                case 42:
                    this.ItemID = 4471; break;
                case 43:
                    this.ItemID = 4471; break;
                case 44:
                    this.ItemID = 4471; break;
                case 51:
                    this.ItemID = 3799; break;
                case 52:
                    this.ItemID = 3799; break;
                case 53:
                    this.ItemID = 3799; break;
                case 54:
                    this.ItemID = 3799; break;
                case 61:
                    this.ItemID = 4466; break;
                case 62:
                    this.ItemID = 4466; break;
                case 63:
                    this.ItemID = 4466; break;
                case 64:
                    this.ItemID = 4466; break;
                case 65:
                    this.ItemID = 4466; break;
                case 66:
                    this.ItemID = 4466; break;
                case 71:
                    this.ItemID = 7955; break;
                case 72:
                    this.ItemID = 7955; break;
                case 73:
                    this.ItemID = 7955; break;
                case 74:
                    this.ItemID = 7955; break;
                case 75:
                    this.ItemID = 7847; break;
                case 76:
                    this.ItemID = 7847; break;
                case 77:
                    this.ItemID = 7847; break;
                case 78:
                    this.ItemID = 7847; break;
                case 79:
                    this.ItemID = 3688; break;
                case 80:
                    this.ItemID = 3676; break;
                case 81:
                    this.ItemID = 3679; break;
                case 82:
                    this.ItemID = 3682; break;
                default:
                case 0:
                    this.ItemID = 4461; break;
            }
        }

        public GraveStoneOne(Serial serial) : base(serial)
		{
		}

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0); // version
            writer.Write((int)m_Type);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
            m_Type = reader.ReadInt();
        }

        public override void OnSingleClick(Mobile from)
        {
            if (this.Name != null)
            {
                from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", this.Name));
            }
            else
            {
                switch (m_Type)
                {
                    case 1:
                        from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", "One: Seek ye signs of shattered stones! Seek ye gatherings of glimmers!")); break;
                    case 2:
                        from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", "Two: For alas and alack and lo! they fall from heavens to tempt all the sinners...")); break;
                    case 3:
                        from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", "Three: Complete the prophecy to find the secret of the days to come")); break;
                    case 4:
                        from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", "Four: For runes and crystals whisper rhymes to frighten everyone...")); break;
                    case 11:
                        from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", "One: Hast thou heard the dead shall walk?")); break;
                    case 12:
                        from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", "Two: 'Tis prophesied here that castles fall.")); break;
                    case 13:
                        from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", "Three: When crystals shatter and magic gathers")); break;
                    case 14:
                        from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", "Four: And spirits make their ghastly call.")); break;
                    case 21:
                        from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", "One: 'Pon this eve the aether flows and dances madly in the air")); break;
                    case 22:
                        from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", "Two: The spirits rise and walk again and slaughter in the moon's red glare")); break;
                    case 23:
                        from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", "Three: Beware, ye men of mortal flesh, lest ghosts seize all thy lifespan's time")); break;
                    case 24:
                        from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", "Four: For soon the night shall come and soon shall set the sun")); break;
                    case 25:
                        from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", "Five: And soon shall rise the bones to walk and seek the magic sign...")); break;
                    case 31:
                        from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", "One: Where bones of land rise broken from the earth")); break;
                    case 32:
                        from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", "Two: And where the wind whistles 'til the rock itself speaks,")); break;
                    case 33:
                        from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", "Three: By the sea, north of sandstone, south of swamp,")); break;
                    case 34:
                        from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", "Four: A magic sign doth rest 'pon the smallest of peaks.")); break;
                    case 41:
                        from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", "One: Some say a message floated in a corked and sunburnt bottle")); break;
                    case 42:
                        from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", "Two: And washed ashore where fisherfolk found it with a smile--")); break;
                    case 43:
                        from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", "Three: 'Til wounds did grow upon their flesh and disease did rot:")); break;
                    case 44:
                        from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", "Four: A magic sign must live lonely lost upon a deserted isle.")); break;
                    case 51:
                        from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", "One: Soon the world shall split, and lives will lose their luster.")); break;
                    case 52:
                        from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", "Two: The cadavers shall rise from their restless aching sleep!")); break;
                    case 53:
                        from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", "Three: Soon shall monsters walk with bones broke all asunder,")); break;
                    case 54:
                        from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", "Four: For a magic sign doth dwell in caverns dark and deep!")); break;
                    case 61:
                        from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", "One: Where swords were shattered and brothers fell")); break;
                    case 62:
                        from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", "Two: In battles well divided")); break;
                    case 63:
                        from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", "Three: Where naught can live save pride and sting")); break;
                    case 64:
                        from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", "Four: And dry is air in hiding")); break;
                    case 65:
                        from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", "Five: A magic sign is caught in sand and sun")); break;
                    case 66:
                        from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", "Six: Where blood lays idle")); break;
                    case 71:
                        from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", "One: Upon a day when snow doth fall")); break;
                    case 72:
                        from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", "Two: A gathering will form of noblemen")); break;
                    case 73:
                        from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", "Three: Among them some who quarrel still")); break;
                    case 74:
                        from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", "Four: Between free will and civil men")); break;
                    case 75:
                        from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", "Five: Whilst watched by mice and monsters both")); break;
                    case 76:
                        from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", "Six: A challenge shall be made")); break;
                    case 77:
                        from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", "Seven: That breaketh lances and severs growth")); break;
                    case 78:
                        from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", "Eight: And stains fair grass with hate")); break;
                    case 79:
                        from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", "Nine: Someday perhaps shall reconcile, (Wis)")); break;
                    case 80:
                        from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", "Ten: Two men whose hearts were once the same (Corp)")); break;
                    case 81:
                        from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", "Eleven: 'Til then the world shall tremble dire (Hur)")); break;
                    case 82:
                        from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", "Twelve: And none shall fix the blame... (Mani)")); break;
                    default:
                    case 0:
                        from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", "I am broken. Page a GM.")); break;
                }
            }
        }
    }
}
