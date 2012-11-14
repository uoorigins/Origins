using System;
using Server;
using Server.Mobiles;

namespace Server
{
    public partial class BaseSpeech
    {
        public static string BritanniaLow(BaseCreature m_Mobile, SpeechEventArgs e)
        {
            Mobile from = e.Mobile;
            string response = null;
            string town = GetLocation(m_Mobile);
            string MyName = m_Mobile.Name;

            if (Insensitive.Speech(e.Speech, "hello") || Insensitive.Speech(e.Speech, "hi") || (Insensitive.Speech(e.Speech, "good") && Insensitive.Speech(e.Speech, "see") && Insensitive.Speech(e.Speech, "thee")))
            {
                if (m_Mobile.Attitude == AttitudeLevel.Wicked)
                {
                    switch (Utility.Random(3))
                    {
                        case 0: response = ("What dost thou want?"); break;
                        case 1: response = ("Uh huh?"); break;
                        case 2: response = ("Yeah, what?"); break;
                    }
                }
                else if (m_Mobile.Attitude == AttitudeLevel.Neutral)
                {
                    switch (Utility.Random(3))
                    {
                        case 0: response = ("Hello."); break;
                        case 1: response = ("Hi."); break;
                        case 2: response = ("Greetings."); break;
                    }
                }
                else if (m_Mobile.Attitude == AttitudeLevel.Goodhearted)
                {
                    switch (Utility.Random(3))
                    {
                        case 0: response = String.Format("Hello, {0}.", e.Mobile.Female ? "milady" : "milord"); break;
                        case 1: response = ("Hi."); break;
                        case 2: response = String.Format("Greetings, {0}.", e.Mobile.Female ? "milady" : "milord"); break;
                    }
                }
            }
            if ((Insensitive.Speech(e.Speech, "how") && Insensitive.Speech(e.Speech, "you")) || (Insensitive.Speech(e.Speech, "how") && Insensitive.Speech(e.Speech, "thou")))
            {
                if (m_Mobile.Attitude == AttitudeLevel.Wicked)
                {
                    switch (Utility.Random(3))
                    {
                        case 0: response = ("Horrible!"); break;
                        case 1: response = ("None of thy business!"); break;
                        case 2: response = ("Get away from me!"); break;
                    }
                }
                else if (m_Mobile.Attitude == AttitudeLevel.Neutral)
                {
                    switch (Utility.Random(3))
                    {
                        case 0: response = ("I'm alright."); break;
                        case 1: response = ("Fine."); break;
                        case 2: response = ("I'm good."); break;
                    }
                }
                else if (m_Mobile.Attitude == AttitudeLevel.Goodhearted)
                {
                    switch (Utility.Random(3))
                    {
                        case 0: response = String.Format("I'm good, {0}.  I hope thou'rt alright too.", e.Mobile.Female ? "m'lady" : "m'lord"); break;
                        case 1: response = ("Doin' great!"); break;
                        case 2: response = String.Format("Pretty good, $m'lord/m'lady$.", e.Mobile.Female ? "m'lady" : "m'lord"); break;
                    }
                }
            }
            if ((Insensitive.Speech(e.Speech, "where") && Insensitive.Speech(e.Speech, "thou") && Insensitive.Speech(e.Speech, "live")) || (Insensitive.Speech(e.Speech, "where") && Insensitive.Speech(e.Speech, "you") && Insensitive.Speech(e.Speech, "live")) || (Insensitive.Speech(e.Speech, "thou") && Insensitive.Speech(e.Speech, "live")) || (Insensitive.Speech(e.Speech, "you") && Insensitive.Speech(e.Speech, "live")) || Insensitive.Speech(e.Speech, "what city are you from") || Insensitive.Speech(e.Speech, "what town are you from") || (Insensitive.Speech(e.Speech, "where") && Insensitive.Speech(e.Speech, "you") && Insensitive.Speech(e.Speech, "from")) || (Insensitive.Speech(e.Speech, "where") && Insensitive.Speech(e.Speech, "thou") && Insensitive.Speech(e.Speech, "from")))
            {
                if (m_Mobile.Attitude == AttitudeLevel.Wicked)
                {
                    switch (Utility.Random(3))
                    {
                        case 0: response = ("I live here!"); break;
                        case 1: response = ("None of thy business!"); break;
                        case 2: response = ("I live in the bottom of the ocean!  I only come here during the day!"); break;
                    }
                }
                else if (m_Mobile.Attitude == AttitudeLevel.Neutral)
                {
                    switch (Utility.Random(5))
                    {
                        case 0: response = String.Format("I live here in {0}.", town); break;
                        case 1: response = String.Format("I live in {0}.", town); break;
                        case 2: response = String.Format("Here. In {0}.", town); break;
                        case 3: response = String.Format("Right here.  In {0}.", town); break;
                        case 4: response = "I live in the town that thou'rt standin' in."; break;
                    }
                }
                else if (m_Mobile.Attitude == AttitudeLevel.Goodhearted)
                {
                    switch (Utility.Random(4))
                    {
                        case 0: response = String.Format("I live here in {0}.", town); break;
                        case 1: response = String.Format("Here, {0}.", e.Mobile.Female ? "milady" : "milord"); break;
                        case 2: response = String.Format("Right here.  In {0}, {1}.", town, e.Mobile.Female ? "m'lady" : "m'lord"); break;
                        case 3: response = String.Format("I live in the town that thou'rt standin' in, {0}.", e.Mobile.Female ? "m'lady" : "m'lord"); break;
                    }
                }
            }
            if ((Insensitive.Speech(e.Speech, "where") && Insensitive.Speech(e.Speech, "am") && Insensitive.Speech(e.Speech, "i")) || Insensitive.Speech(e.Speech, "what town am I in") || (Insensitive.Speech(e.Speech, "what") && Insensitive.Speech(e.Speech, "town is this")))
            {
                switch (Utility.Random(3))
                {
                    case 0: response = String.Format("{0}.", town); break;
                    case 1: response = String.Format("Uhhh... {0}, I think.", town); break;
                    case 2: response = String.Format("Thou'rt in the town of {0}.", town); break;
                }
            }
            if ((Insensitive.Speech(e.Speech, "where") && Insensitive.Speech(e.Speech, "thou") && Insensitive.Speech(e.Speech, "work")) || (Insensitive.Speech(e.Speech, "where") && Insensitive.Speech(e.Speech, "you") && Insensitive.Speech(e.Speech, "work")))
            {
                if (m_Mobile.Attitude == AttitudeLevel.Wicked)
                {
                    switch (Utility.Random(3))
                    {
                        case 0: response = ("I work here, moron!"); break;
                        case 1: response = ("None of thy business!"); break;
                        case 2: response = ("I work out of a cave!"); break;
                    }
                }
                else if (m_Mobile.Attitude == AttitudeLevel.Neutral)
                {
                    switch (Utility.Random(3))
                    {
                        case 0: response = String.Format("I work here in {0}.", town); break;
                        case 1: response = String.Format("Here. In {0}.", town); break;
                        case 2: response = String.Format("I work in the town that thou art in.", town); break;
                    }
                }
                else if (m_Mobile.Attitude == AttitudeLevel.Goodhearted)
                {
                    switch (Utility.Random(3))
                    {
                        case 0: response = String.Format("I work here in {0}.", town); break;
                        case 1: response = String.Format("Here. In {0}, {1}.", town, e.Mobile.Female ? "m'lady" : "m'lord"); break;
                        case 2: response = String.Format("I work in this town, {0}.", e.Mobile.Female ? "m'lady" : "m'lord"); break;
                    }
                }
            }
            if (Insensitive.Speech(e.Speech, "thanks") || Insensitive.Speech(e.Speech, "thank you") || Insensitive.Speech(e.Speech, "thank thee") || Insensitive.Speech(e.Speech, "thank ye") || Insensitive.Speech(e.Speech, "appreciate"))
            {
                switch (Utility.Random(4))
                {
                    case 0: response = "Thou'rt welcome."; break;
                    case 1: response = "'Twas nothin'."; break;
                    case 2: response = "Sure, Thee's welcome."; break;
                    case 3: response = "No problem."; break;
                }
            }
            if (Insensitive.Speech(e.Speech, "bye") || Insensitive.Speech(e.Speech, "farewell") || (Insensitive.Speech(e.Speech, "fare") && Insensitive.Speech(e.Speech, "well")) || Insensitive.Speech(e.Speech, "chow") || Insensitive.Speech(e.Speech, "ciao") || (Insensitive.Speech(e.Speech, "see") && Insensitive.Speech(e.Speech, "ya")) || Insensitive.Speech(e.Speech, "seeya") || Insensitive.Speech(e.Speech, "see you") || Insensitive.Speech(e.Speech, "cya"))
            {
                switch (Utility.Random(3))
                {
                    case 0: response = "Goodbye."; break;
                    case 1: response = "Farewell."; break;
                    case 2: response = "Fare thee well."; break;
                }
            }
            if (Insensitive.Speech(e.Speech, "who are you") || Insensitive.Speech(e.Speech, "what's your name") || Insensitive.Speech(e.Speech, "what is your name") || Insensitive.Speech(e.Speech, "who art thou") || Insensitive.Speech(e.Speech, "who are ye") || (Insensitive.Speech(e.Speech, "who") && Insensitive.Speech(e.Speech, "you")) || Insensitive.Speech(e.Speech, "what are you called") || Insensitive.Speech(e.Speech, "what art thou called") || Insensitive.Speech(e.Speech, "what are ye called") || Insensitive.Speech(e.Speech, "know your name") || Insensitive.Speech(e.Speech, "know thy name") || Insensitive.Speech(e.Speech, "know yer name") || Insensitive.Speech(e.Speech, "what is thy name") || Insensitive.Speech(e.Speech, "what's thy name"))
            {
                switch (Utility.Random(3))
                {
                    case 0: response = String.Format("{0}.", MyName); break;
                    case 1: response = String.Format("I am {0}, {1}.", MyName, e.Mobile.Female ? "m'lady" : "m'lord"); break;
                    case 2: response = String.Format("Call me {0}.", MyName); break;
                }
            }
            if (Insensitive.Equals(e.Speech, "name"))
            {
                response = String.Format("My name, {0}?", e.Mobile.Female ? "m'lady" : "m'lord");
            }
            if ((Insensitive.Speech(e.Speech, "Is") && Insensitive.Speech(e.Speech, "name")) || Insensitive.Speech(e.Speech, "What's thy name"))
            {
                switch (Utility.Random(3))
                {
                    case 0: response = String.Format("I'm {0}.", MyName); break;
                    case 1: response = String.Format("My name's {0}.", MyName); break;
                    case 2: response = String.Format("I'm {0}.", MyName); break;
                }
            }
            if ((Insensitive.Speech(e.Speech, "make") && Insensitive.Speech(e.Speech, "money")) || (Insensitive.Speech(e.Speech, "earn") && Insensitive.Speech(e.Speech, "money")) || (Insensitive.Speech(e.Speech, "get") && Insensitive.Speech(e.Speech, "money")))
            {
                if (m_Mobile.Attitude == AttitudeLevel.Wicked)
                {
                    switch (Utility.Random(3))
                    {
                        case 0: response = ("If it's money thou'rt wantin', find thy own source!"); break;
                        case 1: response = ("I ain't tellin' thee nothin'. Thou would elbow in on my victim - er - customers."); break;
                        case 2: response = ("If thou'rt able, go huntin'. What thou hunts is thy own business, though."); break;
                    }
                }
                else if (m_Mobile.Attitude == AttitudeLevel.Neutral)
                {
                    switch (Utility.Random(6))
                    {
                        case 0: response = ("I can tell thee that in order to make some money thou would do best to practice thy skills in a shop somewhere. Some even have the tools and materials thou would need on hand."); break;
                        case 1: response = ("If thou'rt willing to work, thou could use the tools and stuff at some of the local shops and make some things that thou could sell."); break;
                        case 2: response = ("Thou could go chop up some trees for wood. Carpenters are usually lookin' for wood."); break;
                        case 3: response = ("Either work an honest living and make some things or don't. Ain't no matter to me."); break;
                        case 4: response = ("Thou can always hunt. Meat, hides, and feathers an' such can all be sold to shokeepers."); break;
                        case 5: response = ("There's ore to be mined, wood to be cut, armor to be made, and lotsa other things to do. Take thy pick."); break;
                    }
                }
                else if (m_Mobile.Attitude == AttitudeLevel.Goodhearted)
                {
                    switch (Utility.Random(4))
                    {
                        case 0: response = "Thou could practice thy skills at a shop, and then sell what thou art able to make. The shops should have tools and materials."; break;
                        case 1: response = "If thou kills some monsters, thou can usually find gold. They tend to keep any that they find."; break;
                        case 2: response = "Thou want money? Hunt for meat an' hides. Lotsa folks do it."; break;
                        case 3: response = "Get an axe and chop a tree, if thou want money from a carpenter. Or sell some furniture."; break;
                    }
                }
            }
            if (Insensitive.Speech(e.Speech, "camp"))
            {
                switch (Utility.Random(3))
                {
                    case 0: response = "Take some kindling with thee if thou'rt wantin' to camp. Oh, and a bedroll. Yeah, that too."; break;
                    case 1: response = "If thou gots a bedroll and some kindling, thou'rt set to camp."; break;
                    case 2: response = "I just take a bedroll when I'm goin' out in the woods. Kindlin' thou can get from the trees, usually."; break;
                }
            }
            if ((Insensitive.Speech(e.Speech, "how") && Insensitive.Speech(e.Speech, "quit")) || (Insensitive.Speech(e.Speech, "log") && Insensitive.Speech(e.Speech, "off")) || Insensitive.Speech(e.Speech, "logoff"))
            {
                switch (Utility.Random(2))
                {
                    case 0: response = "Find an Inn. Or make a camp. Either one will do thee just fine."; break;
                    case 1: response = "Thou can make camp, or check in to an Inn."; break;
                }
            }
            if (Insensitive.Speech(e.Speech, "bedroll"))
            {
                switch (Utility.Random(3))
                {
                    case 0: response = "Thou'rt needin' a bedroll? Find a provisioner."; break;
                    case 1: response = "Provisioners got bedrolls."; break;
                    case 2: response = "Find a provisioner. Then thou can buy a bedroll."; break;
                }
            }
            if (Insensitive.Speech(e.Speech, "kindling"))
            {
                switch (Utility.Random(3))
                {
                    case 0: response = "Chop up some wood. That'll get thee some kindling."; break;
                    case 1: response = "Uh... the provisioner's got some for sale, if thou wants to buy it."; break;
                    case 2: response = "Thou wants kindling? Go chop some wood. Or buy some from the provisioner. I don't care."; break;
                }
            }
            if (Insensitive.Speech(e.Speech, "cave") || Insensitive.Speech(e.Speech, "dungeon") || Insensitive.Speech(e.Speech, "destard") || Insensitive.Speech(e.Speech, "despise") || Insensitive.Speech(e.Speech, "shame") || Insensitive.Speech(e.Speech, "deceit") || Insensitive.Speech(e.Speech, "hythloth") || Insensitive.Speech(e.Speech, "wrong") || Insensitive.Speech(e.Speech, "covetous"))
            {
                switch (Utility.Random(4))
                {
                    case 0: response = "Dungeons got treasure. That and lotsa pain."; break;
                    case 1: response = "I don't know none of the names of the dungeons. An' I KNOW I couldn't find my way there!"; break;
                    case 2: response = "I'd go explore some of them places - caves and such - but I like my skin attached to my body."; break;
                    case 3: response = "Them dungeons got monsters. Dangerous places. I hear they can make thee rich, though."; break;
                }
            }
            if (Insensitive.Speech(e.Speech, "gold") || Insensitive.Speech(e.Speech, "treasure"))
            {
                switch (Utility.Random(3))
                {
                    case 0: response = "Find thy own gold! I ain't lettin' my secrets out!"; break;
                    case 1: response = "I ain't tellin' thee where I gets my gold! 'Sides, thou couldn't survive the dungeons without knowin' how."; break;
                    case 2: response = "Tell thee what... go kill a deamon. They got lotsa good stuff on 'em."; break;
                }
            }
            if (Insensitive.Speech(e.Speech, "Britannia"))
            {
                if (from.Karma <= -60)
                {
                    if (m_Mobile.Attitude == AttitudeLevel.Wicked)
                    {
                        switch (Utility.Random(3))
                        {
                            case 0: response = "Britannia? 'Tis the land thou'rt ruinin', blackguard!"; break;
                            case 1: response = "Surely thou knows the land thou'rt destroyin'!"; break;
                            case 2: response = "And what would thou like to know about Britannia? How thou can do more to make our lives worse?"; break;
                        }
                    }
                    else if (m_Mobile.Attitude == AttitudeLevel.Goodhearted)
                    {
                        switch (Utility.Random(3))
                        {
                            case 0: response = String.Format("Please, {0}, don't spoil our land no further. I beg thee!", e.Mobile.Female ? "lady" : "sir"); break;
                            case 1: response = "I can't help but worry that any news I give will only aid thy plans for destruction."; break;
                            case 2: response = String.Format("I would ask thee, {0}, to pillage our land no further.", e.Mobile.Female ? "m'lady" : "sir"); break;
                        }
                    }
                    else if (m_Mobile.Attitude == AttitudeLevel.Neutral)
                    {
                        switch (Utility.Random(3))
                        {
                            case 0: response = "Britannia is the very land thou'rt standin' on."; break;
                            case 1: response = "Britannia? Why, 'tis all around thee."; break;
                            case 2: response = "This very land is Britannia, as thou should know."; break;
                        }
                    }
                }
                else if (from.Karma >= 60)
                {
                    switch (Utility.Random(3))
                    {
                        case 0: response = "Britannia is this land thou helps make better."; break;
                        case 1: response = "Britannia? Why, 'tis the name that thanks thee daily for thy kindness."; break;
                        case 2: response = "How could thou not know of the lands made more prosperous by thy deeds?"; break;
                    }
                }
                else
                {
                    switch (Utility.Random(3))
                    {
                        case 0: response = "Britannia?  Why, 'tis the name Lord British has claimed for the lands of 'imself and 'is subjects"; break;
                        case 1: response = "This very land is Britannia, as thou must know."; break;
                        case 2: response = "Britannia? Why, 'tis all around thee."; break;
                    }
                }
            }
            if (Insensitive.Speech(e.Speech, "Buccaneer's Den") || Insensitive.Speech(e.Speech, "Buccaneers Den"))
            {
                switch (Utility.Random(3))
                {
                    case 0: response = "Find thy own way about."; break;
                    case 1: response = String.Format("If thou don't know, {0}, I can't help.", e.Mobile.Female ? "m'lady" : "m'lord"); break;
                    case 2: response = String.Format("I ain't good with directions, {0}.", e.Mobile.Female ? "m'lady" : "m'lord"); break;
                }
            }
            if (Insensitive.Speech(e.Speech, "Britain") || Insensitive.Speech(e.Speech, "capital"))
            {
                switch (Utility.Random(3))
                {
                    case 0: response = "Find thy own way around."; break;
                    case 1: response = String.Format("If thou don't know, {0}, I can't help.", e.Mobile.Female ? "m'lady" : "m'lord"); break;
                    case 2: response = String.Format("I ain't good with directions, {0}.", e.Mobile.Female ? "m'lady" : "m'lord"); break;
                }
            }
            if (Insensitive.Speech(e.Speech, "Cove"))
            {
                switch (Utility.Random(3))
                {
                    case 0: response = "Find thy own way about."; break;
                    case 1: response = String.Format("If thou don't know, {0}, I can't help.", e.Mobile.Female ? "m'lady" : "m'lord"); break;
                    case 2: response = String.Format("I ain't good with directions, {0}.", e.Mobile.Female ? "m'lady" : "m'lord"); break;
                }
            }
            if (Insensitive.Speech(e.Speech, "Jhelom"))
            {
                switch (Utility.Random(3))
                {
                    case 0: response = "Find thy own way about."; break;
                    case 1: response = String.Format("If thou don't know, {0}, I can't help.", e.Mobile.Female ? "m'lady" : "m'lord"); break;
                    case 2: response = String.Format("I ain't good with directions, {0}.", e.Mobile.Female ? "m'lady" : "m'lord"); break;
                }
            }
            if (Insensitive.Speech(e.Speech, "Magincia"))
            {
                switch (Utility.Random(3))
                {
                    case 0: response = "Find thy own way about."; break;
                    case 1: response = String.Format("If thou don't know, {0}, I can't help.", e.Mobile.Female ? "m'lady" : "m'lord"); break;
                    case 2: response = String.Format("I ain't good with directions, {0}.", e.Mobile.Female ? "m'lady" : "m'lord"); break;
                }
            }
            if (Insensitive.Speech(e.Speech, "Minoc"))
            {
                switch (Utility.Random(3))
                {
                    case 0: response = "Find thy own way about."; break;
                    case 1: response = String.Format("If thou don't know, {0}, I can't help.", e.Mobile.Female ? "m'lady" : "m'lord"); break;
                    case 2: response = String.Format("I ain't good with directions, {0}.", e.Mobile.Female ? "m'lady" : "m'lord"); break;
                }
            }
            if (Insensitive.Speech(e.Speech, "Serpent's Hold") || Insensitive.Speech(e.Speech, "Serpents Hold"))
            {
                switch (Utility.Random(3))
                {
                    case 0: response = "Find thy own way about."; break;
                    case 1: response = String.Format("If thou don't know, {0}, I can't help.", e.Mobile.Female ? "m'lady" : "m'lord"); break;
                    case 2: response = String.Format("I ain't good with directions, {0}.", e.Mobile.Female ? "m'lady" : "m'lord"); break;
                }
            }
            if (Insensitive.Speech(e.Speech, "Skara Brae"))
            {
                switch (Utility.Random(3))
                {
                    case 0: response = "Find thy own way about."; break;
                    case 1: response = String.Format("If thou don't know, {0}, I can't help.", e.Mobile.Female ? "m'lady" : "m'lord"); break;
                    case 2: response = String.Format("I ain't good with directions, {0}.", e.Mobile.Female ? "m'lady" : "m'lord"); break;
                }
            }
            if (Insensitive.Speech(e.Speech, "Trinsic"))
            {
                switch (Utility.Random(3))
                {
                    case 0: response = "Find thy own way about."; break;
                    case 1: response = String.Format("If thou don't know, {0}, I can't help.", e.Mobile.Female ? "m'lady" : "m'lord"); break;
                    case 2: response = String.Format("I ain't good with directions, {0}.", e.Mobile.Female ? "m'lady" : "m'lord"); break;
                }
            }
            if (Insensitive.Speech(e.Speech, "Vesper"))
            {
                switch (Utility.Random(3))
                {
                    case 0: response = "Find thy own way about."; break;
                    case 1: response = String.Format("If thou don't know, {0}, I can't help.", e.Mobile.Female ? "m'lady" : "m'lord"); break;
                    case 2: response = String.Format("I ain't good with directions, {0}.", e.Mobile.Female ? "m'lady" : "m'lord"); break;
                }
            }
            if (Insensitive.Speech(e.Speech, "Yew"))
            {
                switch (Utility.Random(3))
                {
                    case 0: response = "Find thy own way about."; break;
                    case 1: response = String.Format("If thou don't know, {0}, I can't help.", e.Mobile.Female ? "m'lady" : "m'lord"); break;
                    case 2: response = String.Format("I ain't good with directions, {0}.", e.Mobile.Female ? "m'lady" : "m'lord"); break;
                }
            }
            if (Insensitive.Speech(e.Speech, "Lord British") || Insensitive.Speech(e.Speech, "ruler") || Insensitive.Speech(e.Speech, "king"))
            {
                if (m_Mobile.Attitude == AttitudeLevel.Wicked)
                {
                    if (from.Karma <= -60)
                    {
                        switch (Utility.Random(3))
                        {
                            case 0: response = "I'd like to see the bloody fool's head on a spit, I would."; break;
                            case 1: response = "Dost thou want my opinion on Lord British? I find him a lout, I do. And I'd say it to him if I could."; break;
                            case 2: response = "Thou wouldn't find me cryin' over 'is spilled blood."; break;
                        }
                    }
                    else if (from.Karma >= 60)
                    {
                        switch (Utility.Random(3))
                        {
                            case 0: response = String.Format("Lord British? Best we don't speak of him, {0}.", e.Mobile.Female ? "m'lady" : "m'lord"); break;
                            case 1: response = String.Format("Thou wouldn't care for my opinion, {0}.", e.Mobile.Female ? "m'lady" : "m'lord"); break;
                            case 2: response = String.Format("Nah, {0}, I ain't tellin' my feelings 'bout our liege. Best to be quiet 'bout them things.", e.Mobile.Female ? "m'lady" : "m'lord"); break;
                        }
                    }
                    else
                    {
                        switch (Utility.Random(3))
                        {
                            case 0: response = "I don't know what makes him feel so lordly. 'Less, 'tis his gift for collectin' gold to fill his coffers."; break;
                            case 1: response = "I hope Lord British is pleased with all he's done for Britannia. I'll say I ain't."; break;
                            case 2: response = "Lord British? I suppose the taxes he takes to fill his coffers leave us enough to live on... almost."; break;
                        }
                    }
                }
                else if (m_Mobile.Attitude == AttitudeLevel.Neutral)
                {
                    switch (Utility.Random(3))
                    {
                        case 0: response = "Aye, now thou'rt speakin' of our king."; break;
                        case 1: response = "Lord British? I like 'im."; break;
                        case 2: response = "They say Lord British is fair and just. I got no argument with that."; break;
                    }
                }
                else if (m_Mobile.Attitude == AttitudeLevel.Goodhearted)
                {
                    switch (Utility.Random(3))
                    {
                        case 0: response = "Aye, Lord British works hard to make sure we're all happy."; break;
                        case 1: response = "Lord British is our king. He's a good man, I say."; break;
                        case 2: response = "Lord British? He's done right by Britannia, he has!"; break;
                    }
                }
            }
            if (Insensitive.Speech(e.Speech, "weather"))
            {
                response = "Ah, the weather... 'Tis an interesting thing, really. No matter what the season, no matter what enchantments are cast, our land is almost always blessed with clear and beautiful blue skies.";
            }
            if (Insensitive.Speech(e.Speech, "concerns") || Insensitive.Speech(e.Speech, "troubles"))
            {
                switch (Utility.Random(3))
                {
                    case 0: response = "Lotsa things annoy people, like taxation, invasion, and protection form creatures of the wild."; break;
                    case 1: response = "Any land sees hard times, an' it takes a wise ruler to lead his people through 'em."; break;
                    case 2: response = "Surely thou ain't understandin' -- life can't always be free of trouble."; break;
                }
            }
            if (Insensitive.Speech(e.Speech, "blackthorn"))
            {
                switch (Utility.Random(3))
                {
                    case 0: response = "I think Blackthorn wrote a book or somethin'. I, uh, ain't had the time to read it though."; break;
                    case 1: response = "Blackthorn an' British are still pals, I hear. Huh. Strange pair, that."; break;
                    case 2: response = "I don't think either one of 'em's better than the other. Blackthorn an' British, I mean."; break;
                }
            }
            if (Insensitive.Speech(e.Speech, "shamino"))
            {
                switch (Utility.Random(3))
                {
                    case 0: response = "Who?"; break;
                    case 1: response = "Where?"; break;
                    case 2: response = "What?"; break;
                }
            }
            if (Insensitive.Speech(e.Speech, "Iolo"))
            {
                switch (Utility.Random(3))
                {
                    case 0: response = "Who?"; break;
                    case 1: response = "Where?"; break;
                    case 2: response = "What?"; break;
                }
            }
            if (Insensitive.Speech(e.Speech, "Dupre"))
            {
                switch (Utility.Random(3))
                {
                    case 0: response = "Who?"; break;
                    case 1: response = "Where?"; break;
                    case 2: response = "What?"; break;
                }
            }
            if (Insensitive.Speech(e.Speech, "New Magincia"))
            {
                switch (Utility.Random(3))
                {
                    case 0: response = "New Magincia? Is there something wrong with the original?"; break;
                    case 1: response = String.Format("Forgive me, {0}, but I think thou'rt drunk.", e.Mobile.Female ? "m'lady" : "m'lord"); break;
                    case 2: response = String.Format("I ain't heard o' such a place, {0}. I s'pose it could be a colony or somethin'.", e.Mobile.Female ? "m'lady" : "m'lord"); break;
                }
            }
            if (Insensitive.Speech(e.Speech, "other lands") || Insensitive.Speech(e.Speech, "other realms") || Insensitive.Speech(e.Speech, "many realms") || Insensitive.Speech(e.Speech, "many lands") || Insensitive.Speech(e.Speech, "other realm") || Insensitive.Speech(e.Speech, "one realm of many?"))
            {
                switch (Utility.Random(3))
                {
                    case 0: response = "What's thou mean? There's only one land, and it's called Britannia."; break;
                    case 1: response = "What other?"; break;
                    case 2: response = "There ain't no other!"; break;
                }
            }
            if (Insensitive.Speech(e.Speech, "colony"))
            {
                switch (Utility.Random(3))
                {
                    case 0: response = String.Format("I ain't knowin' of none, {0}.", e.Mobile.Female ? "m'lady" : "m'lord"); break;
                    case 1: response = "Rumor is that Lord British wants to send idiots to settle unexplored areas."; break;
                    case 2: response = String.Format("I don't know what thou'rt speakin' 'bout, {0}.", e.Mobile.Female ? "m'lady" : "m'lord"); break;
                }
            }
            if (Insensitive.Speech(e.Speech, "virtue") || Insensitive.Speech(e.Speech, "virtues") || Insensitive.Speech(e.Speech, "shrines") || Insensitive.Speech(e.Speech, "truth") || Insensitive.Speech(e.Speech, "love") || Insensitive.Speech(e.Speech, "courage") || Insensitive.Speech(e.Speech, "spirituality") || Insensitive.Speech(e.Speech, "valor") || Insensitive.Speech(e.Speech, "honor") || Insensitive.Speech(e.Speech, "justice") || Insensitive.Speech(e.Speech, "sacrifice") || Insensitive.Speech(e.Speech, "honesty") || Insensitive.Speech(e.Speech, "humility") || Insensitive.Speech(e.Speech, "compassion"))
            {
                switch (Utility.Random(2))
                {
                    case 0: response = "Shrines to the virtues are all 'round our land. They're mighty powerful, I've heard."; break;
                    case 1: response = "Rest and health is found at the shrines. I heard they'll bring thee alive if thou'rt dead or some such non-sense."; break;
                }
            }
            if (Insensitive.Speech(e.Speech, "avatar"))
            {
                switch (Utility.Random(4))
                {
                    case 0: response = "Who?"; break;
                    case 1: response = "Sorry, Never heard of 'im."; break;
                    case 2: response = "I ain't never heard of this tar person."; break;
                    case 3: response = "I can't help thee."; break;
                }
            }
            if (Insensitive.Speech(e.Speech, "moongates"))
            {
                switch (Utility.Random(3))
                {
                    case 0: response = "The moongates?  What are they for?"; break;
                    case 1: response = "They help travellers. I think."; break;
                    case 2: response = String.Format("Them things are beyond me, {0}.", e.Mobile.Female ? "m'lady" : "m'lord"); break;
                }
            }
            if (Insensitive.Speech(e.Speech, "moons"))
            {
                response = "Britannia's moons are called Trammel and Felucca. They control the destinations of the moongates.";
            }
            return response;
        }
    }
}