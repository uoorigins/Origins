using System;
using System.Collections.Generic;
using System.Text;
using Server.Network;
using Server.Items;

namespace Server.Items
{
    public class MagicEightBall : Item
    {
        [Constructable]
        public MagicEightBall() : base(3630)
        {
            LootType = LootType.Blessed;
            Light = LightType.Circle150;
            Weight = 1.0;
        }

        public MagicEightBall(Serial serial) : base(serial)
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
                from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", "Looking into the crystal ball, thou doth see swirling mists in which words form, 'Ask and be answered.'"));
            }
        }

        public override void OnDoubleClick(Mobile from)
        {
            switch (Utility.RandomMinMax(0, 27))
            {
                default:
                case 0:
                    
                    from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", "Aye.")); break;
                case 1:
                    from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", "Nay.")); break;
                case 2:
                    from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", "Very doubtful.")); break;
                case 3:
                    from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", "Mine sources say nay.")); break;
                case 4:
                    from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", "Tis most certain.")); break;
                case 5:
                    from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", "Aye, verily.")); break;
                case 6:
                    from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", "*fizzles*")); break;
                case 7:
                    from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", "Count on it not")); break;
                case 8:
                    from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", "Concentrate and ask again.")); break;
                case 9:
                    from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", "Beyond the slightest doubt.")); break;
                case 10:
                    from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", "Unable to foresee.")); break;
                case 11:
                    from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", "Eat at the Blue Boar!")); break;
                case 12:
                    from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", "Why art thou asking me?")); break;
                case 13:
                    from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", "The very ground shall crumble before a thing.")); break;
                case 14:
                    from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", "'Twas not clear.")); break;
                case 15:
                    from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", "With virtue, it shall be.")); break;
                case 16:
                    from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", "*shimmers, crackles, and fades*")); break;
                case 17:
                    from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", "Only a fool would ask such.")); break;
                case 18:
                    from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", "If I say, 'yea', wilt thou leave?")); break;
                case 19:
                    from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", "A plague on tee if thou doth ask again.")); break;
                case 20:
                    from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", "Tis no suprise that I'd say aye'.")); break;
                case 21:
                    from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", "Truth is tough. It will not break.")); break;
                case 22:
                    from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", "Keep from me the divinity of Yes and No.")); break;
                case 23:
                    from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", "Who then is going to say Nay?")); break;
                case 24:
                    from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", "T'is no good arguing with the inevitable.")); break;
                case 25:
                    from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", "Mondain himself could not make me say yes!")); break;
                case 26:
                    from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", "Nothing left, nothing at all, nothing, nothing.")); break;
                case 27:
                    from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", "Best not to tell thee.")); break;
            }
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }
}
