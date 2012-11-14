using System;
using Server;
using Server.Mobiles;

namespace Server
{
    public partial class BaseSpeech
    {
        public static string ReGreetingHigh(BaseCreature m_Mobile, Mobile from)
        {
            string response = null;

            if (m_Mobile.Attitude == AttitudeLevel.Wicked)
            {
                switch (Utility.Random(4))
                {
                    case 0: response = "I greeted thee. Now, GET ON WITH IT!"; break;
                    case 1: response = "Oh, go soak thy head. I heard thee."; break;
                    case 2: response = "HELLO! HELLO! I hear ye, foul vermin."; break;
                    case 3: response = "Hello, a dozen times; Hello! Gads."; break;
                }
            }

            //Dastardly or Famous
            if (from.Karma <= -60 || from.Karma >= 60)
            {
                if (m_Mobile.Attitude == AttitudeLevel.Neutral || m_Mobile.Attitude == AttitudeLevel.Goodhearted)
                {
                    switch (Utility.Random(4))
                    {
                        case 0: response = "How did thou become so notorious repeating thyself?"; break;
                        case 1: response = String.Format("Thou art fearsome and repetitive, {0}.", from.Name); break;
                        case 2: response = String.Format("{0}, repeating thyself gets thee nowhere.", from.Name); break;
                        case 3: response = "And to thee, hello and hello again."; break;
                    }
                }
            }
            //Annonymus
            else
            {
                if (m_Mobile.Attitude == AttitudeLevel.Neutral || m_Mobile.Attitude == AttitudeLevel.Goodhearted)
                {
                    switch (Utility.Random(4))
                    {
                        case 0: response = "Thou said that."; break;
                        case 1: response = String.Format("And hello again. How may I help thee, {0}?", from.Name); break;
                        case 2: response = "Is it me, or art thou repetitive?"; break;
                        case 3: response = "And hello to thee, again."; break;
                    }
                }
            }

            return response;
        }
    }
}