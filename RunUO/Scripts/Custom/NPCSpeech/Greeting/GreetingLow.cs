using System;
using Server;
using Server.Mobiles;

namespace Server
{
    public partial class BaseSpeech
    {
        public static string GreetingLow(BaseCreature m_Mobile, Mobile from)
        {
            string response = null;

            //Dastardly
            if (from.Karma <= -60)
            {
                if (m_Mobile.Attitude == AttitudeLevel.Wicked)
                {
                    switch (Utility.Random(5))
                    {
                        case 0: response = "I don't got the time for the likes of thee. Begone!"; break;
                        case 1: response = "Please leave this place now!"; break;
                        case 2: response = "Thou ain't welcome here."; break;
                        case 3: response = "What's thou want from me, dog?"; break;
                        case 4: response = "Off with thee!"; break;
                    }
                }
                else if (m_Mobile.Attitude == AttitudeLevel.Neutral)
                {
                    switch (Utility.Random(7))
                    {
                        case 0: response = "Might I help thee?"; break;
                        case 1: response = "Get gone from here."; break;
                        case 2: response = "I don't deal with thy kind."; break;
                        case 3: response = "Yes?"; break;
                        case 4: response = "What?"; break;
                        case 5: response = "Hello."; break;
                        case 6: response = "What's thou need?"; break;
                    }
                }
                else if (m_Mobile.Attitude == AttitudeLevel.Goodhearted)
                {
                    switch (Utility.Random(6))
                    {
                        case 0: response = "Good day."; break;
                        case 1: response = "How can I help thee?"; break;
                        case 2: response = "Yes?"; break;
                        case 3: response = "What's thou needin'?"; break;
                        case 4: response = "Hello."; break;
                        case 5: response = String.Format("What's thou need, {0}?", from.Female ? "milady" : "milord"); break;
                    }
                }
            }
            //Famous
            else if (from.Karma >= 60)
            {
                if (m_Mobile.Attitude == AttitudeLevel.Wicked)
                {
                    switch (Utility.Random(5))
                    {
                        case 0: response = "Greetings to thee."; break;
                        case 1: response = "How can I do for thee?"; break;
                        case 2: response = "Yes, oh perfumed one?"; break;
                        case 3: response = "What?"; break;
                        case 4: response = "What's thou want of my low self?"; break;
                    }
                }
                else if (m_Mobile.Attitude == AttitudeLevel.Neutral)
                {
                    switch (Utility.Random(6))
                    {
                        case 0: response = String.Format("Greetings.  What can I do for thee, {0}?", from.Female ? "milady" : "milord"); break;
                        case 1: response = String.Format("Good morrow {0}!  How can I help thee today?", from.Female ? "milady" : "milord"); break;
                        case 2: response = "Yes?  Did thou need somethin'?"; break;
                        case 3: response = "Hello!  How may I help thee?"; break;
                        case 4: response = "Hello!"; break;
                        case 5: response = "What can I do for thee?"; break;
                    }
                }
                else if (m_Mobile.Attitude == AttitudeLevel.Goodhearted)
                {
                    switch (Utility.Random(7))
                    {
                        case 0: response = String.Format("Greetings, good {0}!  What can I do for thee?", from.Female ? "lady" : "sir"); break;
                        case 1: response = String.Format("Good morrow {0}!  How can I help?", from.Female ? "milady" : "milord"); break;
                        case 2: response = "Yes?  What can I do for thee today?"; break;
                        case 3: response = "Yes?  Art thou requirin' my assistance?"; break;
                        case 4: response = "Hello, my friend!  How may I help thee?"; break;
                        case 5: response = "Well hello!"; break;
                        case 6: response = String.Format("What's thou need, good {0}?", from.Female ? "lady" : "sir"); break;
                    }
                }
            }
            //Annonymous
            else
            {
                if (m_Mobile.Attitude == AttitudeLevel.Wicked)
                {
                    switch (Utility.Random(5))
                    {
                        case 0: response = "Huh?"; break;
                        case 1: response = "Umm? Thou want somethin'?"; break;
                        case 2: response = "Yes? Kin I help thee?"; break;
                        case 3: response = "What?"; break;
                        case 4: response = "What's thou want?"; break;
                    }
                }
                else if (m_Mobile.Attitude == AttitudeLevel.Neutral)
                {
                    switch (Utility.Random(7))
                    {
                        case 0: response = "Greetings. What can I do for thee?"; break;
                        case 1: response = "Greetings. What can I help thee with?"; break;
                        case 2: response = "Good morrow!  How can I help thee?"; break;
                        case 3: response = "Hello! ?"; break;
                        case 4: response = "Hello, my friend!  How may I assist thee?"; break;
                        case 5: response = "Hello!"; break;
                        case 6: response = "What's thou need?"; break;
                    }
                }
                else if (m_Mobile.Attitude == AttitudeLevel.Goodhearted)
                {
                    switch (Utility.Random(6))
                    {
                        case 0: response = "Greetings! Is there somethin' I can do for thee?"; break;
                        case 1: response = "Good morrow! How can I help thee?"; break;
                        case 2: response = "Yes?"; break;
                        case 3: response = "Hello, my friend! How may I help thee?"; break;
                        case 4: response = "Well hello!"; break;
                        case 5: response = String.Format("What's thou need, {0}?", from.Female ? "milady" : "milord"); break;
                    }
                }
            }

            return response;
        }
    }
}