using System;
using Server;
using Server.Mobiles;
using Server.Regions;

namespace Server
{
    public partial class BaseSpeech
    {
        public static string GetLocation(BaseCreature m_Mobile)
        {
            Region region = Region.Find(m_Mobile.Location, m_Mobile.Map);

            while (region != null)
            {
                BaseRegion br = region as BaseRegion;

                if (br != null && br.Name != null)
                    return br.Name;

                region = region.Parent;
            }

            return "the wilderness";
        }
    }
}