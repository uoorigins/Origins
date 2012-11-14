using System;
using Server;
using Server.Mobiles;

namespace Server
{
    public partial class BaseSpeech
    {
        public static string FarewellHigh(BaseCreature m_Mobile, Mobile from)
        {
            string response = null;

            //Dastardly
            if (from.Karma <= -60)
            {
                if (m_Mobile.Attitude == AttitudeLevel.Wicked)
                {
                    switch (Utility.Random(3))
                    {
                        case 0: response = "Good riddance to thee."; break;
                        case 1: response = "I thank thee for leaving me my miserable life."; break;
                        case 2: response = "Thou didst not kill me! I thank thee!"; break;
                    }
                }
                else if (m_Mobile.Attitude == AttitudeLevel.Neutral || m_Mobile.Attitude == AttitudeLevel.Goodhearted)
                {
                    switch (Utility.Random(3))
                    {
                        case 0: response = "Good riddance to thee, scum."; break;
                        case 1: response = String.Format("I have survived an encounter with {0}!", from.Name); break;
                        case 2: response = String.Format("I thank thee for not slaying me, {0}.", from.Name); break;
                    }
                }
            }
            //Famous
            else if (from.Karma >= 60)
            {
                if (m_Mobile.Attitude == AttitudeLevel.Wicked)
                {
                    switch (Utility.Random(3))
                    {
                        case 0: response = "Finally! Be on thy way."; break;
                        case 1: response = "About time, for I have other tasks to attend to."; break;
                        case 2: response = "Ah, decided that thou hast wasted my time long enough?"; break;
                    }
                }
                else if (m_Mobile.Attitude == AttitudeLevel.Neutral || m_Mobile.Attitude == AttitudeLevel.Goodhearted)
                {
                    switch (Utility.Random(3))
                    {
                        case 0: response = String.Format("'Twas a pleasure to speak with thee, {0}.", from.Name); break;
                        case 1: response = String.Format("Fare thee well, {0}.", from.Name); break;
                        case 2: response = String.Format("Goodbye, {0}.", from.Name); break;
                    }
                }
            }
            //Annonymous
            else
            {
                if (m_Mobile.Attitude == AttitudeLevel.Wicked)
                {
                    switch (Utility.Random(3))
                    {
                        case 0: response = "Finally! Be on thy way."; break;
                        case 1: response = "About time, for I have other tasks to attend to."; break;
                        case 2: response = "Ah, decided that thou hast wasted my time long enough?"; break;
                    }
                }
                else if (m_Mobile.Attitude == AttitudeLevel.Neutral || m_Mobile.Attitude == AttitudeLevel.Goodhearted)
                {
                    switch (Utility.Random(3))
                    {
                        case 0: response = "Fare thee well."; break;
                        case 1: response = "Goodbye, stranger."; break;
                        case 2: response = "May the rest of thy day be pleasant. "; break;
                    }
                }
            }

            return response;
        }
    }
}