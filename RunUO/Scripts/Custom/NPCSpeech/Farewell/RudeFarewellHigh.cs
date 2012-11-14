using System;
using Server;
using Server.Mobiles;

namespace Server
{
    public partial class BaseSpeech
    {
        public static string RudeFarewellHigh(BaseCreature m_Mobile, Mobile from)
        {
            string response = null;

            if (m_Mobile.Attitude == AttitudeLevel.Wicked)
            {
                switch (Utility.Random(3))
                {
                    case 0: response = "Fine. Have a nice day, scum."; break;
                    case 1: response = "Same to thee."; break;
                    case 2: response = "Oh, don't be so immature about it."; break;
                }
            }
            else if (m_Mobile.Attitude == AttitudeLevel.Neutral || m_Mobile.Attitude == AttitudeLevel.Goodhearted)
            {
                switch (Utility.Random(4))
                {
                    case 0: response = "Fine. Have a nice day."; break;
                    case 1: response = "Same to thee."; break;
                    case 2: response = "There is no need to be so rude about it."; break;
                    case 3: response = "Very well, if thou dost not wish to speak to me, kindly do not interrupt me again."; break;
                }
            }

            return response;
        }
    }
}