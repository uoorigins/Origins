using System;
using Server;
using Server.Mobiles;

namespace Server
{
    public partial class BaseSpeech
    {
        public static string ReGreetingLow(BaseCreature m_Mobile, Mobile from)
        {
            string response = null;

            if (m_Mobile.Attitude == AttitudeLevel.Wicked)
            {
                switch (Utility.Random(3))
                {
                    case 0: response = "Uh what? Hello. Hm. Could thou repeat that?"; break;
                    case 1: response = String.Format("Er... Did thee say, 'hello,' {0}?", from.Name); break;
                    case 2: response = "Uh. What?"; break;
                }
            }

            //Dastardly
            if (from.Karma <= -60)
            {
                if (m_Mobile.Attitude == AttitudeLevel.Neutral || m_Mobile.Attitude == AttitudeLevel.Goodhearted)
                {
                    switch (Utility.Random(4))
                    {
                        case 0: response = "Say 'hello' again and I pour slop on thy armor."; break;
                        case 1: response = "WHAT?!?!"; break;
                        case 2: response = "I'm not worthy, but hello anyways."; break;
                        case 3: response = "Are ye talkin' to me? Ye must be, I see no others afoot."; break;
                    }
                }
            }
            //Famous
            else if (from.Karma >= 60)
            {
                if (m_Mobile.Attitude == AttitudeLevel.Neutral || m_Mobile.Attitude == AttitudeLevel.Goodhearted)
                {
                    switch (Utility.Random(3))
                    {
                        case 0: response = String.Format("Hello, {0}. Again, that is.", from.Name); break;
                        case 1: response = "What? I'm confused now. Hello, I guess."; break;
                        case 2: response = String.Format("I'm not smart. {0} is smart. Hello {0}.", from.Name); break;
                    }
                }
            }
            //Annonymous
            else
            {
                if (m_Mobile.Attitude == AttitudeLevel.Neutral || m_Mobile.Attitude == AttitudeLevel.Goodhearted)
                {
                    switch (Utility.Random(3))
                    {
                        case 0: response = "Canst thou say that again?"; break;
                        case 1: response = "I hear thee. Do I say 'hello' now?"; break;
                        case 2: response = "Thou art broing."; break;
                    }
                }
            }

            return response;
        }
    }
}