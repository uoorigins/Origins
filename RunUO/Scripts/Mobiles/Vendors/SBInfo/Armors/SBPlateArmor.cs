using System;
using System.Collections.Generic;
using Server.Items;

namespace Server.Mobiles
{
	public class SBPlateArmor: SBInfo
	{
		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo();
		private IShopSellInfo m_SellInfo = new InternalSellInfo();

		public SBPlateArmor()
		{
		}

		public override IShopSellInfo SellInfo { get { return m_SellInfo; } }
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } }

		public class InternalBuyInfo : List<GenericBuyInfo>
		{
			public InternalBuyInfo()
			{
				Add( new GenericBuyInfo( typeof( PlateGorget ), 141, 9, 0x1413, 0 ) );
				Add( new GenericBuyInfo( typeof( PlateChest ), 272, 9, 0x1415, 0 ) );
				Add( new GenericBuyInfo( typeof( PlateLegs ), 217, 9, 0x1411, 0 ) );
				Add( new GenericBuyInfo( typeof( PlateArms ), 182, 9, 0x1410, 0 ) );
				Add( new GenericBuyInfo( typeof( PlateGloves ), 144, 9, 0x1414, 0 ) );

                switch (Utility.Random(6))
                {
                    case 0: Add(new GenericBuyInfo(typeof(PlateGorget), 141 * 2, 1, 0x1413, Utility.RandomMetalHue())); break;
                    case 1: Add(new GenericBuyInfo(typeof(PlateChest), 272 * 2, 1, 0x1415, Utility.RandomMetalHue())); break;
                    case 2: Add(new GenericBuyInfo(typeof(PlateLegs), 217 * 2, 1, 0x1411, Utility.RandomMetalHue())); break;
                    case 3: Add(new GenericBuyInfo(typeof(PlateArms), 182 * 2, 1, 0x1410, Utility.RandomMetalHue())); break;
                    case 4: Add(new GenericBuyInfo(typeof(PlateGloves), 144 * 2, 1, 0x1414, Utility.RandomMetalHue())); break;
                    case 5: Add(new GenericBuyInfo(typeof(PlateHelm), 170 * 2, 1, 0x1414, Utility.RandomMetalHue())); break;
                }

			}
		}

		public class InternalSellInfo : GenericSellInfo
		{
			public InternalSellInfo()
			{
                Add(typeof(PlateArms), 91);
                Add(typeof(PlateChest), 136);
                Add(typeof(PlateGloves), 72);
                Add(typeof(PlateGorget), 71);
                Add(typeof(PlateLegs), 109);

				Add( typeof( FemalePlateChest ), 136 );

			}
		}
	}
}
