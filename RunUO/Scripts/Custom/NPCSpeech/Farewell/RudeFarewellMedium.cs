using System;
using Server;
using Server.Mobiles;

namespace Server
{
    public partial class BaseSpeech
    {
        public static string RudeFarewellMedium(BaseCreature m_Mobile, Mobile from)
        {
            string response = null;

            if (m_Mobile.Attitude == AttitudeLevel.Wicked)
            {
                switch (Utility.Random(3))
                {
                    case 0: response = "Fine. Have a nice day. "; break;
                    case 1: response = "Same to thee."; break;
                    case 2: response = "Thou'rt very rude."; break;
                }
            }
            else if (m_Mobile.Attitude == AttitudeLevel.Neutral || m_Mobile.Attitude == AttitudeLevel.Goodhearted)
            {
                switch (Utility.Random(3))
                {
                    case 0: response = "Fine. Have a nice day."; break;
                    case 1: response = "Same to thee."; break;
                    case 2: response = "There is no need to be so rude about it."; break;
                }
            }

            return response;
        }
    }
}