using System;
using Server;
using Server.Items;
using Server.Mobiles;

namespace Server.Targeting
{
    [PlayerVendorTarget]
	public class WandTarget : Target
	{
		private BaseWand m_Item;

		public WandTarget( BaseWand item ) : base( 6, false, TargetFlags.None )
		{
            AllowNonlocal = true;
			m_Item = item;
		}

		private static int GetOffset( Mobile caster )
		{
			return 5 + (int)(caster.Skills[SkillName.Magery].Value * 0.02 );
		}

		protected override void OnTarget( Mobile from, object targeted )
		{
			m_Item.DoWandTarget( from, targeted );
		}
	}

    [PlayerVendorTarget]
    public class StaffTarget : Target
    {
        private BaseStaff m_Item;

        public StaffTarget(BaseStaff item) : base(6, false, TargetFlags.None)
        {
            AllowNonlocal = true;
            m_Item = item;
        }

        private static int GetOffset(Mobile caster)
        {
            return 5 + (int)(caster.Skills[SkillName.Magery].Value * 0.02);
        }

        protected override void OnTarget(Mobile from, object targeted)
        {
            m_Item.DoWandTarget(from, targeted);
        }
    }
}