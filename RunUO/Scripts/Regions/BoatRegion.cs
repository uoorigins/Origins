using System;
using Server;
using Server.Mobiles;
using Server.Items;
using Server.Multis;
using Server.Spells;
using Server.Spells.Sixth;
using Server.Guilds;
using Server.Gumps;
namespace Server.Regions
{
    public class BoatRegion : BaseRegion
    {
        public static readonly int BoatPriority = Region.DefaultPriority + 1;
        public override MusicName DefaultMusic { get { return MusicName.Sailing; } }

        private BaseBoat m_Boat;

        public BoatRegion(BaseBoat boat) : base(null, boat.Map, BoatPriority, GetArea(boat))
        {
            m_Boat = boat;
        }

        private static Rectangle3D[] GetArea(BaseBoat boat)
        {
            int x = boat.X;
            int y = boat.Y;
            int z = boat.Z;

            Rectangle2D[] houseArea;

            if (boat.Facing == Direction.North || boat.Facing == Direction.South)
                houseArea = boat.AreaNorth;
            else
                houseArea = boat.AreaEast;

            Rectangle3D[] area = new Rectangle3D[houseArea.Length];

            for (int i = 0; i < area.Length; i++)
            {
                Rectangle2D rect = houseArea[i];
                area[i] = Region.ConvertTo3D(new Rectangle2D(x + rect.Start.X, y + rect.Start.Y, rect.Width, rect.Height));
            }

            return area;
        }

        public override bool OnDecay(Item item)
        {
            if (BaseBoat.FindBoatAt(item.Location, item.Map) != null)
            {
                BaseBoat boat = BaseBoat.FindBoatAt(item.Location, item.Map);
                MultiComponentList mcl = boat.Components;
                Map map = boat.Map;

                IPooledEnumerable eable = map.GetObjectsInBounds(new Rectangle2D(boat.X + mcl.Min.X, boat.Y + mcl.Min.Y, mcl.Width, mcl.Height));
                foreach (object o in eable)
                {
                    if (o is Item && boat.Contains((Item)o))
                    {
                        if ((Item)o == item)
                        {
                            eable.Free();
                            return false;
                        }
                    }
                }
                eable.Free();
            }
            return base.OnDecay(item);
        }
    }
}
