using System;
using Server;
using Server.Mobiles;

namespace Server
{
    public partial class BaseSpeech
    {
        public static string DefaultMedium(BaseCreature m_Mobile, Mobile from)
        {
            string response = null;

            //Dastardly
            if (from.Karma <= -60)
            {
                if (m_Mobile.Attitude == AttitudeLevel.Wicked)
                {
                    switch (Utility.Random(3))
                    {
                        case 0: response = String.Format("I don't care if thou art a dangerous powerful {0}. Please leave me alone.", from.Female ? "woman" : "man"); break;
                        case 1: response = "All high and mighty, are we? Why dost thou not wander off, eh?"; break;
                        case 2: response = "Even if thou hast a bit of a reputation, I'll wager if I hit thee, thou wouldst bleed."; break;
                    }
                }
                else if (m_Mobile.Attitude == AttitudeLevel.Neutral)
                {
                    switch (Utility.Random(3))
                    {
                        case 0: response = "Do not hurt me! I can't help thee!"; break;
                        case 1: response = "I, ahh, can't help thee, with this at least, thou seest, I, ahh, oh, please... I can grovel, wouldst thou like to see me grovel?"; break;
                        case 2: response = "I beg thy pardon, but I do not understand."; break;
                    }
                }
                else if (m_Mobile.Attitude == AttitudeLevel.Goodhearted)
                {
                    switch (Utility.Random(3))
                    {
                        case 0: response = "Even if thou art a cruel and frightening person, thou wouldst not harm me for not knowing... wouldst thou?"; break;
                        case 1: response = "Why would I tell scum like thee, if I knew?"; break;
                        case 2: response = "I never thought I would meet a famous villain like thee!"; break;
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
                        case 0: response = String.Format("Goodness, a high and mighty {0} talking to ME. I suppose thou thinkest I have no work to do.", from.Female ? "lady" : "lord"); break;
                        case 1: response = String.Format("For a fancy {0}, thou makest little sense.", from.Female ? "lady" : "lord"); break;
                        case 2: response = "Even if thou art famous, talking to me will not make my day any better."; break;
                    }
                }
                else if (m_Mobile.Attitude == AttitudeLevel.Neutral)
                {
                    switch (Utility.Random(3))
                    {
                        case 0: response = "Thou art amazing, didst thou know that? A truly magnificent person."; break;
                        case 1: response = "Oh... I know nothing about that."; break;
                        case 2: response = String.Format("Begging thy pardon, {0}, but couldst thou explain that a bit better for me?", from.Female ? "ma'am" : "sir"); break;
                    }
                }
                else if (m_Mobile.Attitude == AttitudeLevel.Goodhearted)
                {
                    switch (Utility.Random(3))
                    {
                        case 0: response = "I could not let a person like thee go by unassisted... help me understand what 'tis thou seekest."; break;
                        case 1: response = "I know nothing of that. But thou art as wonderful as thy reputation has it."; break;
                        case 2: response = "I'm sorry that I cannot help thee. If only we had more like thee!"; break;
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
                        case 0: response = "Who art thou anyway?"; break;
                        case 1: response = "Go away."; break;
                        case 2: response = "Why am I always pestered by those with nothing better to do?"; break;
                    }
                }
                else if (m_Mobile.Attitude == AttitudeLevel.Neutral)
                {
                    switch (Utility.Random(3))
                    {
                        case 0: response = "Sorry, I don't know what thou talkest about."; break;
                        case 1: response = "Excuse me?"; break;
                        case 2: response = "What?"; break;
                    }
                }
                else if (m_Mobile.Attitude == AttitudeLevel.Goodhearted)
                {
                    switch (Utility.Random(3))
                    {
                        case 0: response = "I am very sorry, friend, but I don't understand thee."; break;
                        case 1: response = "I don't know what thou art talking about."; break;
                        case 2: response = "Excuse me, but what art thou saying?"; break;
                    }
                }
            }

            return response;
        }
    }
}