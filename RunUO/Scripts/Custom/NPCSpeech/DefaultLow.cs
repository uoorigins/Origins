using System;
using Server;
using Server.Mobiles;

namespace Server
{
    public partial class BaseSpeech
    {
        public static string DefaultLow(BaseCreature m_Mobile, Mobile from)
        {
            string response = null;

            //Dastardly
            if (from.Karma <= -60)
            {
                if (m_Mobile.Attitude == AttitudeLevel.Wicked)
                {
                    switch (Utility.Random(3))
                    {
                        case 0: response = "Go 'way, big shot."; break;
                        case 1: response = "Thou wants me to break thy nose?"; break;
                        case 2: response = "Thou'rt stuck on thyself."; break;
                    }
                }
                else if (m_Mobile.Attitude == AttitudeLevel.Neutral)
                {
                    switch (Utility.Random(3))
                    {
                        case 0: response = "Don' hurt me!"; break;
                        case 1: response = "Sorry, sorry, I'm stupid, don't hit me."; break;
                        case 2: response = "Please, my old grandmother, she'd miss me if thou slit my throat!"; break;
                    }
                }
                else if (m_Mobile.Attitude == AttitudeLevel.Goodhearted)
                {
                    switch (Utility.Random(3))
                    {
                        case 0: response = "Thou'rt gonna kill me, huh? Well, I lived a good life."; break;
                        case 1: response = "I ain't talking to scum like thee."; break;
                        case 2: response = "Where's the horns? I thought thou had horns growin' out of thy head."; break;
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
                        case 0: response = "Are thou somebody famous?"; break;
                        case 1: response = "Huh?"; break;
                        case 2: response = "Um, yeah, whatever."; break;
                    }
                }
                else if (m_Mobile.Attitude == AttitudeLevel.Neutral)
                {
                    switch (Utility.Random(3))
                    {
                        case 0: response = "It 'ud be so great to help thee! I'd love to tell all abouts it! It's like this, it's...um... oh, consarn it."; break;
                        case 1: response = "Aw, why'd thou ask somethin' I ain't never known?"; break;
                        case 2: response = "How old are thee really? No, really, thou'rt so famous an' all..."; break;
                    }
                }
                else if (m_Mobile.Attitude == AttitudeLevel.Goodhearted)
                {
                    switch (Utility.Random(3))
                    {
                        case 0: response = "Why, someone oughter answer thee, thou bein' so nice an' all. Yes indeedy, someone out there somewheres must know. Just go on lookin', an' I know thou'lt find the answer."; break;
                        case 1: response = "Thee's the best, the best we got. I wisht I could help."; break;
                        case 2: response = "I wish I understood thee... but the greats talk a different language from the likes o' me."; break;
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
                        case 0: response = "Who art thou anyway? Besides annoying."; break;
                        case 1: response = "Life's lousy. Go away."; break;
                        case 2: response = "Go shovel stables or somethin'."; break;
                    }
                }
                else if (m_Mobile.Attitude == AttitudeLevel.Neutral)
                {
                    switch (Utility.Random(3))
                    {
                        case 0: response = "Huh?"; break;
                        case 1: response = "Um... um?"; break;
                        case 2: response = "I'd scratch my head, but I'm told it ain't polite."; break;
                    }
                }
                else if (m_Mobile.Attitude == AttitudeLevel.Goodhearted)
                {
                    switch (Utility.Random(3))
                    {
                        case 0: response = "Huh?"; break;
                        case 1: response = "Thou ain't talking sense there."; break;
                        case 2: response = "Sorry, I ain't gettin' thy drift."; break;
                    }
                }
            }

            return response;
        }
    }
}