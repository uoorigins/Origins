using System;
using Server;

namespace Server.Items
{
	public class RandomWand
	{
		public static BaseWand CreateWand()
		{
			return CreateRandomWand();
		}

		public static BaseWand CreateRandomWand( )
		{
			switch ( Utility.Random( 11 ) )
			{
				default:
				case  0: return new ClumsyWand();
				case  1: return new FeebleWand();
				case  2: return new FireballWand();
				case  3: return new HealWand();
				case  4: return new HarmWand();
				case  5: return new GreaterHealWand();
				case  6: return new IDWand();
				case  7: return new LightningWand();
				case  8: return new MagicArrowWand();
				case  9: return new ManaDrainWand();
				case 10: return new WeaknessWand();
			}
		}

        public static Type RandomWandType()
        {
            int rand = Utility.Random( 300 );
            if ( 40 > rand )
            {
                return typeof( IDWand );
            }
            else if ( 80 > rand )
            {
                return typeof( HealWand );
            }
            else if ( 100 > rand )
            {
                return typeof( GreaterHealWand );
            }
            else if ( 133 > rand )
            {
                return typeof( ClumsyWand );
            }
            else if ( 166 > rand )
            {
                return typeof(FeebleWand);
            }
            else if ( 200 > rand )
            {
                return typeof(WeaknessWand);
            }
            else if ( 230 > rand )
            {
                return typeof(MagicArrowWand);
            }
            else if ( 255 > rand )
            {
                return typeof(HarmWand);
            }
            else if ( 275 > rand )
            {
                return typeof(FireballWand);
            }
            else if ( 290 > rand )
            {
                return typeof(LightningWand);
            }
            else
            {
                return typeof(ManaDrainWand);
            }
        }
	}
}