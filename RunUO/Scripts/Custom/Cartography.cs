using System;
using Server;
using Server.Misc;
using Server.Targeting;
using Server.Items;
using Server.Network;
using Server.Menus.ItemLists;

namespace Server.SkillHandlers
{
    public class Cartography
    {
        public static void Initialize()
        {
            SkillInfo.Table[(int)SkillName.Cartography].Callback = new SkillUseCallback(OnUse);
        }

        public static TimeSpan OnUse(Mobile m)
        {
            m.RevealingAction();

            m.Target = new InternalTarget();
            m.RevealingAction();

            m.SendAsciiMessage("Select the map upon which to draw.");

            return TimeSpan.FromSeconds(10.0);
        }

        private class InternalTarget : Target
        {

            public InternalTarget() : base(1, false, TargetFlags.None)
            {
            }

            protected override void OnTarget(Mobile from, object target)
            {
                if (target is BlankMap)
                {
                    Item item = (Item)target;
                    BaseTool tool = new MapmakersPen();

                    if (item.RootParent != from)
                        from.SendAsciiMessage("That must be in your pack for you to use it.");
                    else
                        from.SendMenu(new CartographyMenu(from, CartographyMenu.Main(from), "Main", tool));

                    if (tool != null)
                        tool.Delete();
                }
                else if (target is WorldMap || target is LocalMap || target is CityMap || target is SeaChart)
                    //from.Send(new AsciiMessage(((Item)target).Serial, ((Item)target).ItemID, MessageType.Regular, 0, 3, "", "You cannot overwrite this carefully hand-drawn map!"));
                    from.SendAsciiMessage("You cannot overwrite this carefully hand-drawn map!");
                else
                {
                    from.SendAsciiMessage("This is not a map.");
                    /*if (target is Item)
                        from.Send(new AsciiMessage(((Item)target).Serial, ((Item)target).ItemID, MessageType.Regular, 0, 3, "", "This is not a map."));*/
                }
                    

            }
        }
    }
}