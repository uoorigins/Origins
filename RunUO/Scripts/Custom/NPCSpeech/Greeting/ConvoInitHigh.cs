using System;
using Server;
using Server.Mobiles;

namespace Server
{
    public partial class BaseSpeech
    {
        public static string ConvoInitHigh(BaseCreature m_Mobile, Mobile from)
        {
            string response = null;

            if (m_Mobile.Attitude == AttitudeLevel.Wicked)
            {
                switch (Utility.Random(3))
                {
                    case 0: response = "Prithee, be quiet."; break;
                    case 1: response = "Thou mayst wish to talk to me, but I do not wish to talk to thee."; break;
                    case 2: response = "What, thou dost wish to waste my time?"; break;
                }
            }

            //Dastardly or Famous
            if (from.Karma <= -60 || from.Karma >= 60)
            {            
                if (m_Mobile.Attitude == AttitudeLevel.Neutral || m_Mobile.Attitude == AttitudeLevel.Goodhearted)
                {
                    switch (Utility.Random(4))
                    {
                        case 0: response = String.Format("Thou wishest to speak with me, {0}?", from.Name); break;
                        case 1: response = String.Format("Thou hast mine attention, {0}.", from.Name); break;
                        case 2: response = String.Format("What is it thou wishest, {0}?", from.Name); break;
                        case 3: response = String.Format("I am listening to thee, {0}.", from.Name); break;
                    }
                }
            }
            //Annonymous
            else
            {
                if (m_Mobile.Attitude == AttitudeLevel.Neutral || m_Mobile.Attitude == AttitudeLevel.Goodhearted)
                {
                    switch (Utility.Random(4))
                    {
                        case 0: response = "Thou wishest to speak with me?"; break;
                        case 1: response = "Thou hast mine attention."; break;
                        case 2: response = "What is it thou wishest?"; break;
                        case 3: response = "I am listening to thee."; break;
                    }
                }
            }

            return response;
        }
    }
}