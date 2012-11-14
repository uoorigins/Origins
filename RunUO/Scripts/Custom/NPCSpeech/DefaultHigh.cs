using System;
using Server;
using Server.Mobiles;

namespace Server
{
    public partial class BaseSpeech
    {
        public static string DefaultHigh(BaseCreature m_Mobile, Mobile from)
        {
            string response = null;

            //Dastardly
            if (from.Karma <= -60)
            {
                if (m_Mobile.Attitude == AttitudeLevel.Wicked)
                {
                    switch (Utility.Random(4))
                    {
                        case 0: response = String.Format("Begging thy {0} pardon, but kindly remove thyself from here. Thy blatherings annoy me.", from.Female ? "ladyship's" : "lordship's"); break;
                        case 1: response = "Even if thou hast a reputation, it says little about thy sense. Canst thou give me a reason why I shouldst not have thy legs broken?"; break;
                        case 2: response = "'Tis not my life full enough of inconveniences? Must I also deal with the awkward importunings of the likes of thee?"; break;
                        case 3: response = "It revolts me how petty lordlings and puffed-up peacocks disturb me with their callow chatter. Begone."; break;
                    }
                }
                else if (m_Mobile.Attitude == AttitudeLevel.Neutral)
                {
                    switch (Utility.Random(3))
                    {
                        case 0: response = "I beg thee, do not harm me. I cannot help thee."; break;
                        case 1: response = "I do not understand thee--I am sure it must be my fault, however."; break;
                        case 2: response = "Begging thy pardon, but I fail to understand what it is that thou wantest."; break;
                    }
                }
                else if (m_Mobile.Attitude == AttitudeLevel.Goodhearted)
                {
                    switch (Utility.Random(3))
                    {
                        case 0: response = "I know thy reputation, but surely thou wouldst not harm me for not knowing the answer?"; break;
                        case 1: response = "Even if I knew, would I tell such as thee, scoundrel?"; break;
                        case 2: response = String.Format("To think I have met thee, the infamous {0}.", from.Name); break;
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
                        case 0: response = "Ah, the high and mighty deign to speak with me. I feel honored that they soil their feet with the ground upon which I walk."; break;
                        case 1: response = "For one of the greats of this land, thou makest remarkably little sense."; break;
                        case 2: response = String.Format("Noble {0} of whatever it is thou art, know this: my life is a miserable cesspool, and right now the honor of thy asking me a question makes me merely the noblest toad there. It also maketh of thee the noblest leech.", from.Female ? "lady" : "lord"); break;
                    }
                }
                else if (m_Mobile.Attitude == AttitudeLevel.Neutral)
                {
                    switch (Utility.Random(3))
                    {
                        case 0: response = "Begging thy pardon, but in my awe at seeing thee actually here before me, I neglected to listen closely to what thou spoke."; break;
                        case 1: response = "Sadly, I must report that I know nothing of what thou speakest."; break;
                        case 2: response = "Help me to understand thee, for I know not what thou seekest."; break;
                    }
                }
                else if (m_Mobile.Attitude == AttitudeLevel.Goodhearted)
                {
                    switch (Utility.Random(3))
                    {
                        case 0: response = "'Twould be a pleasure to assist thee, but alas, I cannot."; break;
                        case 1: response = String.Format("Unfortunately, {0}, I know nothing of that.", from.Female ? "milady" : "sir"); break;
                        case 2: response = "I do not understand. Couldst thou clarify?"; break;
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
                        case 0: response = "I know not who thou art, but annoying is a right good term for thee."; break;
                        case 1: response = "To put it politely, given the miserable state of the world today, GO AWAY."; break;
                        case 2: response = "'Tis said that one of the burdens we of high intelligence bear is the pestering of you folk: the idle questioners of no consequence."; break;
                    }
                }
                else if (m_Mobile.Attitude == AttitudeLevel.Neutral)
                {
                    switch (Utility.Random(3))
                    {
                        case 0: response = "Forgive me, but I do not understand."; break;
                        case 1: response = "Excuse me? Forgive my lack of comprehension."; break;
                        case 2: response = "If I look perplexed, 'tis because I am."; break;
                    }
                }
                else if (m_Mobile.Attitude == AttitudeLevel.Goodhearted)
                {
                    switch (Utility.Random(3))
                    {
                        case 0: response = "Terribly sorry, friend, but I fear I do not understand what thou askest."; break;
                        case 1: response = "I fail to grasp thy meaning, friend."; break;
                        case 2: response = "My apologies, but thy meaning escapes me."; break;
                    }
                }
            }

            return response;
        }
    }
}