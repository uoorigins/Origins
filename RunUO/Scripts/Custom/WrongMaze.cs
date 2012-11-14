using System;
using Server.Network;

namespace Server.Items
{
    public class WrongMazeWallEast : Item
    {
        [Constructable]
        public WrongMazeWallEast() : base(578)
        {
            Movable = false;
            Visible = false;
        }

        public override void OnSingleClick(Mobile from)
        {
            if (this.Name != null)
                from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", this.Name));
            else
                from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", "a dungeon wall"));
        }

        public override bool HandlesOnMovement { get { return true; } }

        public override void OnMovement(Mobile m, Point3D oldLocation)
        {
            IPooledEnumerable eable = GetMobilesInRange(2);

            foreach (object o in eable)
            {
                if (o is Mobile && ((Mobile)o).InRange(Location, 1))
                {
                    eable.Free();
                    Visible = true;
                    return;
                }
            }
            eable.Free();
            Visible = false;
        }

        public WrongMazeWallEast(Serial serial) : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }

    public class WrongMazeWallNorth : Item
    {
        [Constructable]
        public WrongMazeWallNorth() : base(577)
        {
            Movable = false;
            Visible = false;
        }

        public override void OnSingleClick(Mobile from)
        {
            if (this.Name != null)
                from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", this.Name));
            else
                from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", "a dungeon wall"));
        }

        public override bool HandlesOnMovement { get { return true; } }

        public override void OnMovement(Mobile m, Point3D oldLocation)
        {
            IPooledEnumerable eable = GetMobilesInRange(2);

            foreach (object o in eable)
            {
                if (o is Mobile && ((Mobile)o).InRange(Location, 1))
                {
                    eable.Free();
                    Visible = true;
                    return;
                }
            }
            eable.Free();
            Visible = false;
        }

        public WrongMazeWallNorth(Serial serial) : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }

    public class WrongMazeArchNorth1 : Item
    {
        [Constructable]
        public WrongMazeArchNorth1() : base(581)
        {
            Movable = false;
            Visible = false;
        }

        public override void OnSingleClick(Mobile from)
        {
            if (this.Name != null)
                from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", this.Name));
            else
                from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", "a dungeon wall"));
        }

        public override bool HandlesOnMovement { get { return true; } }

        public override void OnMovement(Mobile m, Point3D oldLocation)
        {
            IPooledEnumerable eable = GetMobilesInRange(2);

            foreach (object o in eable)
            {
                if (o is Mobile && ((Mobile)o).InRange(Location, 1))
                {
                    eable.Free();
                    Visible = true;
                    return;
                }
            }
            eable.Free();
            Visible = false;
        }

        public WrongMazeArchNorth1(Serial serial) : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }

    public class WrongMazeArchNorth2 : Item
    {
        [Constructable]
        public WrongMazeArchNorth2() : base(585)
        {
            Movable = false;
            Visible = false;
        }

        public override void OnSingleClick(Mobile from)
        {
            if (this.Name != null)
                from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", this.Name));
            else
                from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", "a dungeon wall"));
        }

        public override bool HandlesOnMovement { get { return true; } }

        public override void OnMovement(Mobile m, Point3D oldLocation)
        {
            IPooledEnumerable eable = GetMobilesInRange(2);

            foreach (object o in eable)
            {
                if (o is Mobile && ((Mobile)o).InRange(Location, 1))
                {
                    eable.Free();
                    Visible = true;
                    return;
                }
            }
            eable.Free();
            Visible = false;
        }

        public WrongMazeArchNorth2(Serial serial) : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }

    public class WrongMazeArchNorth3 : Item
    {
        [Constructable]
        public WrongMazeArchNorth3() : base(583)
        {
            Movable = false;
            Visible = false;
        }

        public override void OnSingleClick(Mobile from)
        {
            if (this.Name != null)
                from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", this.Name));
            else
                from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", "a dungeon wall"));
        }

        public override bool HandlesOnMovement { get { return true; } }

        public override void OnMovement(Mobile m, Point3D oldLocation)
        {
            IPooledEnumerable eable = GetMobilesInRange(2);

            foreach (object o in eable)
            {
                if (o is Mobile && ((Mobile)o).InRange(Location, 1))
                {
                    eable.Free();
                    Visible = true;
                    return;
                }
            }
            eable.Free();
            Visible = false;
        }

        public WrongMazeArchNorth3(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }

    public class WrongMazeArchEast1 : Item
    {
        [Constructable]
        public WrongMazeArchEast1()
            : base(582)
        {
            Movable = false;
            Visible = false;
        }

        public override void OnSingleClick(Mobile from)
        {
            if (this.Name != null)
                from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", this.Name));
            else
                from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", "a dungeon wall"));
        }

        public override bool HandlesOnMovement { get { return true; } }

        public override void OnMovement(Mobile m, Point3D oldLocation)
        {
            IPooledEnumerable eable = GetMobilesInRange(2);

            foreach (object o in eable)
            {
                if (o is Mobile && ((Mobile)o).InRange(Location, 1))
                {
                    eable.Free();
                    Visible = true;
                    return;
                }
            }
            eable.Free();
            Visible = false;
        }

        public WrongMazeArchEast1(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }

    public class WrongMazeArchEast2 : Item
    {
        [Constructable]
        public WrongMazeArchEast2()
            : base(586)
        {
            Movable = false;
            Visible = false;
        }

        public override void OnSingleClick(Mobile from)
        {
            if (this.Name != null)
                from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", this.Name));
            else
                from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", "a dungeon wall"));
        }

        public override bool HandlesOnMovement { get { return true; } }

        public override void OnMovement(Mobile m, Point3D oldLocation)
        {
            IPooledEnumerable eable = GetMobilesInRange(2);

            foreach (object o in eable)
            {
                if (o is Mobile && ((Mobile)o).InRange(Location, 1))
                {
                    eable.Free();
                    Visible = true;
                    return;
                }
            }
            eable.Free();
            Visible = false;
        }

        public WrongMazeArchEast2(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }

    public class WrongMazeArchEast3 : Item
    {
        [Constructable]
        public WrongMazeArchEast3()
            : base(584)
        {
            Movable = false;
            Visible = false;
        }

        public override void OnSingleClick(Mobile from)
        {
            if (this.Name != null)
                from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", this.Name));
            else
                from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", "a dungeon wall"));
        }

        public override bool HandlesOnMovement { get { return true; } }

        public override void OnMovement(Mobile m, Point3D oldLocation)
        {
            IPooledEnumerable eable = GetMobilesInRange(2);

            foreach (object o in eable)
            {
                if (o is Mobile && ((Mobile)o).InRange(Location, 1))
                {
                    eable.Free();
                    Visible = true;
                    return;
                }
            }
            eable.Free();
            Visible = false;
        }

        public WrongMazeArchEast3(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }
}