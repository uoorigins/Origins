using System;
using Server;
using Server.Mobiles;

namespace Server
{
    public partial class BaseSpeech
    {
        public static string FarewellMedium(BaseCreature m_Mobile, Mobile from)
        {
            string response = null;

            //Dastardly
            if (from.Karma <= -60)
            {
                if (m_Mobile.Attitude == AttitudeLevel.Wicked)
                {
                    switch (Utility.Random(3))
                    {
                        case 0: response = "Good riddance."; break;
                        case 1: response = String.Format("Look, everyone, {0} did not kill me!", from.Name); break;
                        case 2: response = "Thou didst not kill me?!"; break;
                    }
                }
                else if (m_Mobile.Attitude == AttitudeLevel.Neutral || m_Mobile.Attitude == AttitudeLevel.Goodhearted)
                {
                    switch (Utility.Random(3))
                    {
                        case 0: response = "Good riddance!"; break;
                        case 1: response = String.Format("I spoke with {0} and I lived...!", from.Name); break;
                        case 2: response = "Goodbye."; break;
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
                        case 0: response = "Finally! Go away."; break;
                        case 1: response = "Thank thee for ending thy interruption."; break;
                        case 2: response = "Goodbye."; break;
                    }
                }
                else if (m_Mobile.Attitude == AttitudeLevel.Neutral || m_Mobile.Attitude == AttitudeLevel.Goodhearted)
                {
                    switch (Utility.Random(3))
                    {
                        case 0: response = String.Format("An honor to speak with thee, {0}.", from.Name); break;
                        case 1: response = String.Format("Farewell, {0}.", from.Name); break;
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
                        case 0: response = "Finally! Go away."; break;
                        case 1: response = "Thank thee for ending thy interruption."; break;
                        case 2: response = "Goodbye."; break;
                    }
                }
                else if (m_Mobile.Attitude == AttitudeLevel.Neutral || m_Mobile.Attitude == AttitudeLevel.Goodhearted)
                {
                    switch (Utility.Random(3))
                    {
                        case 0: response = "Farewell."; break;
                        case 1: response = "Goodbye."; break;
                        case 2: response = "Good day, then."; break;
                    }
                }
            }

            return response;
        }
    }
}