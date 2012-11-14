using System;
using Server;
using Server.Mobiles;

namespace Server
{
    public partial class BaseSpeech
    {
        public static string ConvoInitLow(BaseCreature m_Mobile, Mobile from)
        {
            string response = null;

            if (m_Mobile.Attitude == AttitudeLevel.Wicked)
            {
                switch (Utility.Random(3))
                {
                    case 0: response = "Go 'way."; break;
                    case 1: response = "I ain't wantin' to talk to thee."; break;
                    case 2: response = "Thou'rt rude."; break;
                }
            }

            //Dastardly
            if (from.Karma <= -60)
            {            
                if (m_Mobile.Attitude == AttitudeLevel.Neutral || m_Mobile.Attitude == AttitudeLevel.Goodhearted)
                {
                    switch (Utility.Random(4))
                    {
                        case 0: response = "Don't hurt me."; break;
                        case 1: response = "What do thou want? I can't help."; break;
                        case 2: response = "Thou wants to talk to me? Umm..."; break;
                        case 3: response = "Thou'rt talkin' to me?"; break;
                    }
                }
            }
            //Famous
            else if (from.Karma >= 60)
            {
                if (m_Mobile.Attitude == AttitudeLevel.Neutral || m_Mobile.Attitude == AttitudeLevel.Goodhearted)
                {
                    switch (Utility.Random(4))
                    {
                        case 0: response = String.Format("Yes, {0}?", from.Name); break;
                        case 1: response = String.Format("Hmm? Oh! Tis thee, {0}!", from.Name); break;
                        case 2: response = "Can I help thee?"; break;
                        case 3: response = String.Format("{0}! Nice to see thee.", from.Name); break;
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
                        case 0: response = "Hmm?"; break;
                        case 1: response = "Aye?"; break;
                        case 2: response = "What's thee wantin'?"; break;
                        case 3: response = "I'm listenin'."; break;
                    }
                }
            }

            return response;
        }
    }
}