using System;
using Server;
using Server.Mobiles;

namespace Server
{
    public partial class BaseSpeech
    {
        public static string ReGreetingMedium(BaseCreature m_Mobile, Mobile from)
        {
            string response = null;

            if (m_Mobile.Attitude == AttitudeLevel.Wicked)
            {
                switch (Utility.Random(3))
                {
                    case 0: response = String.Format("Was that thee, or a parrot, {0}?", from.Name); break;
                    case 1: response = String.Format("Get to thy point, {0}!",from.Name); break;
                    case 2: response = "Lose thy greetings and acquire some meaning."; break;
                }
            }

            //Dastardly
            if (from.Karma <= -60)
            {
                if (m_Mobile.Attitude == AttitudeLevel.Neutral || m_Mobile.Attitude == AttitudeLevel.Goodhearted)
                {
                    switch (Utility.Random(4))
                    {
                        case 0: response = "I greet thee a thousand times."; break;
                        case 1: response = String.Format("Yes, {0}, hello. I heard thee.", from.Female ? "Lady" : "Sir"); break;
                        case 2: response = String.Format("Thou'rt a repetitive {0} to talk to.", from.Female ? "woman" : "man"); break;
                        case 3: response = "Uh, right. Art thou having me on?"; break;
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
                        case 0: response = String.Format("I'd think a renown {0}'d too busy for repeatin' thyself.", from.Female ? "lady" : "rogue"); break;
                        case 1: response = "Yes, I heard thou."; break;
                        case 2: response = String.Format("So famous, yet so addled. Hello, {0}.", from.Name); break;
                        case 3: response = String.Format("I am listening to thee, {0}. But hello anyway.", from.Name); break;
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
                        case 0: response = String.Format("Hello again. My, what a friendly {0}.", from.Female ? "lady" : "fellow"); break;
                        case 1: response = "Hee-hee. Hello. Thou art a rascal."; break;
                        case 2: response = "What is it thou wishest?"; break;
                        case 3: response = "Yes, goodtidings to thee as well."; break;
                    }
                }
            }

            return response;
        }
    }
}