using System;
using Server;
using Server.Mobiles;

namespace Server
{
    public partial class BaseSpeech
    {
        public static string RudeFarewellLow(BaseCreature m_Mobile, Mobile from)
        {
            string response = null;

            if (m_Mobile.Attitude == AttitudeLevel.Wicked)
            {
                switch (Utility.Random(3))
                {
                    case 0: response = "Fine."; break;
                    case 1: response = "Same to ye."; break;
                    case 2: response = "Ye's rude."; break;
                }
            }
            else if (m_Mobile.Attitude == AttitudeLevel.Neutral || m_Mobile.Attitude == AttitudeLevel.Goodhearted)
            {
                switch (Utility.Random(3))
                {
                    case 0: response = "Fine. "; break;
                    case 1: response = "Same to ye."; break;
                    case 2: response = "Aye, whatever."; break;
                }
            }

            return response;
        }
    }
}