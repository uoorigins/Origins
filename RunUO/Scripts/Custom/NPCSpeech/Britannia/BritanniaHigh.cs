using System;
using Server;
using Server.Mobiles;

namespace Server
{
    public partial class BaseSpeech
    {
        public static string BritanniaHigh(BaseCreature m_Mobile, SpeechEventArgs e)
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
                        case 2: response = ("Get thee away from me!"); break;
                    }
                }
                else if (m_Mobile.Attitude == AttitudeLevel.Neutral)
                {
                    switch (Utility.Random(3))
                    {
                        case 0: response = ("I'm doing relatively well."); break;
                        case 1: response = ("Just fine."); break;
                        case 2: response = ("I am well."); break;
                    }
                }
                else if (m_Mobile.Attitude == AttitudeLevel.Goodhearted)
                {
                    switch (Utility.Random(3))
                    {
                        case 0: response = String.Format("I am well, {0}. I hope thou art the same.", e.Mobile.Female ? "milady" : "milord"); break;
                        case 1: response = ("Doing great!"); break;
                        case 2: response = String.Format("As well as I can be, {0}.", e.Mobile.Female ? "milady" : "milord"); break;
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
                        case 4: response = "I live in the town that thou art standing in."; break;
                    }
                }
                else if (m_Mobile.Attitude == AttitudeLevel.Goodhearted)
                {
                    switch (Utility.Random(4))
                    {
                        case 0: response = String.Format("I live here in {0}.", town); break;
                        case 1: response = String.Format("Here, {0}.", e.Mobile.Female ? "milady" : "milord"); break;
                        case 2: response = String.Format("Right here.  In {0}, {1}.", town, e.Mobile.Female ? "milady" : "milord"); break;
                        case 3: response = String.Format("I live in the town that thou art standing in, {0}.", e.Mobile.Female ? "milady" : "milord"); break;
                    }
                }
            }
            if ((Insensitive.Speech(e.Speech, "where") && Insensitive.Speech(e.Speech, "am") && Insensitive.Speech(e.Speech, "i")) || Insensitive.Speech(e.Speech, "what town am I in") || (Insensitive.Speech(e.Speech, "what") && Insensitive.Speech(e.Speech, "town is this")))
            {
                switch (Utility.Random(3))
                {
                    case 0: response = String.Format("Thou art in {0}.", town); break;
                    case 1: response = String.Format("Thou art a visitor of {0}.", town); break;
                    case 2: response = String.Format("If thou art lost, then know that thou art in {0}.", town); break;
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
                        case 2: response = ("I work out of a cave!  What dost thou think, imbecile!"); break;
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
                        case 1: response = String.Format("Here. In {0}, {1}.", town, e.Mobile.Female ? "milady" : "milord"); break;
                        case 2: response = String.Format("I work in this town, {0}.", e.Mobile.Female ? "milady" : "milord"); break;
                    }
                }
            }
            if (Insensitive.Speech(e.Speech, "thanks") || Insensitive.Speech(e.Speech, "thank you") || Insensitive.Speech(e.Speech, "thank thee") || Insensitive.Speech(e.Speech, "thank ye") || Insensitive.Speech(e.Speech, "appreciate"))
            {
                switch (Utility.Random(4))
                {
                    case 0: response = "Thou'rt welcome."; break;
                    case 1: response = "'Twas nothing."; break;
                    case 2: response = "Certainly, thou art welcome."; break;
                    case 3: response = "Not a problem."; break;
                }
            }
            if (Insensitive.Speech(e.Speech, "bye") || Insensitive.Speech(e.Speech, "farewell") || (Insensitive.Speech(e.Speech, "fare") && Insensitive.Speech(e.Speech, "well")) || Insensitive.Speech(e.Speech, "chow") || Insensitive.Speech(e.Speech, "ciao") || (Insensitive.Speech(e.Speech, "see") && Insensitive.Speech(e.Speech, "ya")) || Insensitive.Speech(e.Speech, "seeya") || Insensitive.Speech(e.Speech, "see you") || Insensitive.Speech(e.Speech, "cya"))
            {
                switch (Utility.Random(2))
                {
                    case 0: response = "Goodbye."; break;
                    case 1: response = "Farewell."; break;
                }
            }
            if (Insensitive.Speech(e.Speech, "who are you") || Insensitive.Speech(e.Speech, "what's your name") || Insensitive.Speech(e.Speech, "what is your name") || Insensitive.Speech(e.Speech, "who art thou") || Insensitive.Speech(e.Speech, "who are ye") || (Insensitive.Speech(e.Speech, "who") && Insensitive.Speech(e.Speech, "you")) || Insensitive.Speech(e.Speech, "what are you called") || Insensitive.Speech(e.Speech, "what art thou called") || Insensitive.Speech(e.Speech, "what are ye called") || Insensitive.Speech(e.Speech, "know your name") || Insensitive.Speech(e.Speech, "know thy name") || Insensitive.Speech(e.Speech, "know yer name") || Insensitive.Speech(e.Speech, "what is thy name") || Insensitive.Speech(e.Speech, "what's thy name"))
            {
                switch (Utility.Random(3))
                {
                    case 0: response = String.Format("My name is {0}.", MyName); break;
                    case 1: response = String.Format("I am {0}, {1}.", MyName, e.Mobile.Female ? "milady" : "milord"); break;
                    case 2: response = String.Format("Thou mayest call me {0}.", MyName); break;
                }
            }
            if ((Insensitive.Speech(e.Speech, "Is") && Insensitive.Speech(e.Speech, "name")) || Insensitive.Speech(e.Speech, "What's thy name"))
            {
                switch (Utility.Random(3))
                {
                    case 0: response = String.Format("I am {0}.", MyName); break;
                    case 1: response = String.Format("My name, {1}, is {0}.", MyName, e.Mobile.Female ? "madam" : "sir"); break;
                    case 2: response = String.Format("If thou art looking for {0} then thou hast found me.", MyName); break;
                }
            }
            if (Insensitive.Equals(e.Speech, "name"))
            {
                switch (Utility.Random(2))
                {
                    case 0: response = String.Format("If thou'rt asking for my name, {0}, please be more specific.", e.Mobile.Female ? "milady" : "milord"); break;
                    case 1: response = String.Format("Please be more specific, if thou'rt asking for my name, {0}.", e.Mobile.Female ? "milady" : "milord"); break;
                }
            }
            if ((Insensitive.Speech(e.Speech, "make") && Insensitive.Speech(e.Speech, "money")) || (Insensitive.Speech(e.Speech, "earn") && Insensitive.Speech(e.Speech, "money")) || (Insensitive.Speech(e.Speech, "get") && Insensitive.Speech(e.Speech, "money")))
            {
                if (m_Mobile.Attitude == AttitudeLevel.Wicked)
                {
                    switch (Utility.Random(4))
                    {
                        case 0: response = ("Oh, well there's stealing, begging, scavenging... many ways for one such as thee to find money."); break;
                        case 1: response = ("I'm sure that thou can find many varied ways to take money. Like stealing things and selling them back to their owners, killing people..."); break;
                        case 2: response = ("Just lift money from people whho have it. That's what thy type would do anyway."); break;
                        case 3: response = ("Thou can always beg for thy gold."); break;
                    }
                }
                else if (m_Mobile.Attitude == AttitudeLevel.Neutral)
                {
                    switch (Utility.Random(7))
                    {
                        case 0: response = ("If thou dost want to earn money, practice a skill. Make things and sell them to the public. Most shopkeepers will let thee use the tools and materials that they aren't using."); break;
                        case 1: response = ("If thou art able to hunt the wild animals, sometimes thou can sell the meat that thou dost carve from them to a butcher."); break;
                        case 2: response = ("If thou can get an axe, perhaps thou could cut some trees into useful boards for woodworkers. They may be willing to pay thee for tham."); break;
                        case 3: response = ("If thou dost happen upon some ore, go to the blacksmith's shop and try to craft some armor or weapons. I have heard that it just takes practice."); break;
                        case 4: response = ("I have heard that the great dungeons sometimes have great treasures."); break;
                        case 5: response = ("Tanners will buy hides and pelts that thou dost take from the animals thou dost hunt."); break;
                        case 6: response = ("Thou can sell feathers from birds to a bowyer. And a cook may purchase the bird itself from thee."); break;
                    }
                }
                else if (m_Mobile.Attitude == AttitudeLevel.Goodhearted)
                {
                    switch (Utility.Random(4))
                    {
                        case 0: response = "There are many different ways to make money in Britannia. Thou can sell raw materials to merchants, make and sell goods, sell the meat that thou dost find hunting... there are many ways."; break;
                        case 1: response = "The merchants will usually let people practice skills in their shops. All thou dost need is raw materials, and sometimes tools."; break;
                        case 2: response = "Some will stoop to stealing or killing to get their money, so be careful where thou dost walk. Make use of thy skills. Make something and sell it to a merchant."; break;
                        case 3: response = "Hunting seems to be a very profitable source of income. Most cooks will purchase fowl and fish from thee, butchers will pay thee for meat, and tanners for the pelts and hides."; break;
                    }
                }
            }
            if (Insensitive.Speech(e.Speech, "camp"))
            {
                switch (Utility.Random(3))
                {
                    case 0: response = "It's essential to have good kindling and a bedroll if thou dost want to camp."; break;
                    case 1: response = "I never leave the city without a bedroll on which to sleep and some kindling for a fire."; break;
                    case 2: response = "Kindling for a fire and a bedroll for thy comfort is all thou dost need to make camp."; break;
                }
            }
            if ((Insensitive.Speech(e.Speech, "how") && Insensitive.Speech(e.Speech, "quit")) || (Insensitive.Speech(e.Speech, "log") && Insensitive.Speech(e.Speech, "off")) || Insensitive.Speech(e.Speech, "logoff"))
            {
                switch (Utility.Random(2))
                {
                    case 0: response = "Making camp, if thou art in the wilds, and paying for a room in an Inn are the ways to give thyself a rest from this world."; break;
                    case 1: response = "If thou art wanting a respite from the rigors of Britannia, then thou can make a camp or check in to an Inn, whichever is the most convenient."; break;
                }
            }
            if (Insensitive.Speech(e.Speech, "bedroll"))
            {
                switch (Utility.Random(2))
                {
                    case 0: response = "Bedrolls can be purchased from a provisioner."; break;
                    case 1: response = "If thou dost need a bedroll, I think a provisioner might help thee."; break;
                    case 2: response = "I think thou can find a bedroll at a provisioner's shop, if that is what thou art looking for."; break;
                }
            }
            if (Insensitive.Speech(e.Speech, "kindling"))
            {
                switch (Utility.Random(2))
                {
                    case 0: response = "If thou dost use an axe on some wood, thou should get kindling from it. Otherwise, I think a provisioner will carry some for those wanting to camp in the wild."; break;
                    case 1: response = "Thou shouldst be able to get kindling from any wood thou finds. If thou has an axe, of course."; break;
                    case 2: response = "A provisioner can supply thee with kindling if thou dost lack the means to make thy own."; break;
                }
            }
            if (Insensitive.Speech(e.Speech, "cave") || Insensitive.Speech(e.Speech, "dungeon") || Insensitive.Speech(e.Speech, "destard") || Insensitive.Speech(e.Speech, "despise") || Insensitive.Speech(e.Speech, "shame") || Insensitive.Speech(e.Speech, "deceit") || Insensitive.Speech(e.Speech, "hythloth") || Insensitive.Speech(e.Speech, "wrong") || Insensitive.Speech(e.Speech, "covetous"))
            {
                switch (Utility.Random(5))
                {
                    case 0: response = "There are many as yet unmapped places beneath Britannia. 'Tis rumored to be riches and magic in them. And creatures that will rend the flesh from thy bones."; break;
                    case 1: response = "The places under Britannia - some call them 'dungeons' - are spposed to be rich with treasures and gold. They are also guarded by horrible creatures that want nothing more than to dine on humans."; break;
                    case 2: response = "I know of seven dungeons in Britannia. Covetous, Despise, Deceit, and... a, uh, few others."; break;
                    case 3: response = "Let's see... I remember the names of Shame, Wrong, Despise, and Hythloth. I've never owned a map of Britannia myself."; break;
                    case 4: response = "I think the dungeons that people tend to forget are Destard, Covetous, and Hythloth. I think."; break;
                }
            }
            if (Insensitive.Speech(e.Speech, "gold") || Insensitive.Speech(e.Speech, "treasure"))
            {
                switch (Utility.Random(3))
                {
                    case 0: response = "Thou can find a good deal of treasure under Britannia. Look to the caves and what thou call dungeons."; break;
                    case 1: response = "Some of the more dangerous creatures will carry gold that they've scavenged."; break;
                    case 2: response = "If thou art needing gold, either find it in the dungeons, or scavenge it from monsters thou have killed. Thou will find it either way, if thou dost survive."; break;
                }
            }
            if (Insensitive.Speech(e.Speech, "Britannia"))
            {
                if (from.Karma <= -60)
                {
                    switch (Utility.Random(3))
                    {
                        case 0: response = "What wouldst thou have me say about Britannia, save that $rogues/foul wenches$ such as thee do much to bespoil its name."; break;
                        case 1: response = String.Format("Britannia? I ask thee not to speak of this land while in my company, {0}!", e.Mobile.Female ? "witch" : "varlet"); break;
                        case 2: response = String.Format("{0}! Thou carest not for Britannia! Thus, I ask thee to remove the name of Lord British's realm from thy mind.", e.Mobile.Female ? "Foul wench" : "Scoundrel"); break;
                    }
                }
                else if (from.Karma >= 60)
                {
                    switch (Utility.Random(3))
                    {
                        case 0: response = "Ah, thou dost speak of fair Britannia. Lord British, himself, wouldst be proud of thine efforts to keep it peaceful. Yet, those who travel claim it is but one realm of many."; break;
                        case 1: response = "I cannot speak but fondly on my homeland, the very land thou dost seek to keep safe. Yet I would not forgo an opportunity to see other lands."; break;
                        case 2: response = "I have lived in Britannia all my life, and so I thank thee for thine efforts to improve the lives of all who live here."; break;
                    }
                }
                else
                {
                    switch (Utility.Random(3))
                    {
                        case 0: response = "Ah, fair Britannia. Those who travel claim it is but one realm of many."; break;
                        case 1: response = "I cannot speak but fondly on my homeland. Yet I would not forgo an opportunity to see other lands."; break;
                        case 2: response = "I have lived in Britannia all my life. I do love it so."; break;
                    }
                }
            }
            if (Insensitive.Speech(e.Speech, "Buccaneer's Den") || Insensitive.Speech(e.Speech, "Buccaneers Den"))
            {
                switch (Utility.Random(3))
                {
                    case 0: response = "Buccaneer's Den? Why, 'tis nothing more than a hiding place for pirates!"; break;
                    case 1: response = "'Tis where the scum of the realm go to lick their wounds."; break;
                    case 2: response = String.Format("I would not live in Buccaneer's Den if I were thee, {0}, for it is home to the most vile men and women to sail the seas.", e.Mobile.Female ? "milady" : "milord"); break;
                }
            }
            if (Insensitive.Speech(e.Speech, "Britain") || Insensitive.Speech(e.Speech, "capital"))
            {
                switch (Utility.Random(3))
                {
                    case 0: response = "Britain is the capital of all Britannia, land of Lord British."; break;
                    case 1: response = "Lord British, sovereign of all Britannia, resides in Britain."; break;
                    case 2: response = "Britain is home to Lord British, himself. 'Tis the capital of Britannia."; break;
                }
            }
            if (Insensitive.Speech(e.Speech, "Cove"))
            {
                switch (Utility.Random(3))
                {
                    case 0: response = "Cove? Thou shouldst find it North and East of Britain."; break;
                    case 1: response = "Cove is not much of a city. 'Tis more of a village, to be truthful."; break;
                    case 2: response = "Just Northeast of Britain is Cove."; break;
                }
            }
            if (Insensitive.Speech(e.Speech, "Jhelom"))
            {
                switch (Utility.Random(3))
                {
                    case 0: response = "Jhelom is an outpost for mercenaries and sellswords."; break;
                    case 1: response = "Jhelom? 'Tis a town of sword arms looking for work. Thou canst find it off the southwestern coast of mainland Britannia."; break;
                    case 2: response = "Some say Jhelom is as rough as Buccaneer's Den, with half the scoundrels and twice the bruises."; break;
                }
            }
            if (Insensitive.Speech(e.Speech, "Magincia"))
            {
                switch (Utility.Random(3))
                {
                    case 0: response = "Magincia is a city spanning nearly an entire island, outside of Britanny Bay."; break;
                    case 1: response = "Magincia? 'Tis a city of arrogance and pride, I think."; break;
                    case 2: response = String.Format("Quite a lovely place, {0}. And shouldst thou doubt my word, just ask someone who resides there.", e.Mobile.Female ? "milady" : "milord"); break;
                }
            }
            if (Insensitive.Speech(e.Speech, "Minoc"))
            {
                switch (Utility.Random(3))
                {
                    case 0: response = "Minoc is a mining town now."; break;
                    case 1: response = "Minoc? Why it was once home to the best artisans in the realm, but now that rich deposits of precious metals have been discovered in the mountains, 'tis full of miners."; break;
                    case 2: response = String.Format("Minoc lies on the northern coast of the mainland, {0}.", e.Mobile.Female ? "milady" : "milord"); break;
                }
            }
            if (Insensitive.Speech(e.Speech, "Serpent's Hold") || Insensitive.Speech(e.Speech, "Serpents Hold"))
            {
                switch (Utility.Random(3))
                {
                    case 0: response = "Serpent's Hold is where the best knights are trained. 'Tis located in the Cape of Heroes, off the southeastern edge of the mainland."; break;
                    case 1: response = "Serpent's Hold is the fortification Lord British granted to his loyal knights, those of the Order of the Silver Serpent."; break;
                    case 2: response = "Serpent's Hold? Art thou interested in becoming a member of Lord British's royal order of Knights -- the Order of the Silver Serpent? If such is true, then thou shouldst go to Serpent's Hold."; break;
                }
            }
            if (Insensitive.Speech(e.Speech, "Skara Brae"))
            {
                switch (Utility.Random(3))
                {
                    case 0: response = "Skara Brae is an island. Thou canst find it off the western coast of Britannia."; break;
                    case 1: response = "The most skilled trackers in the land learn their craft on the island of Skara Brae."; break;
                    case 2: response = "Many talented shipwrights live on Skara Brae, though the island is actually known for the many trackers who teach their skills there."; break;
                }
            }
            if (Insensitive.Speech(e.Speech, "Trinsic"))
            {
                switch (Utility.Random(3))
                {
                    case 0: response = "Though Serpent's Hold may be home to valiant knights, and Jhelom home to skilled mercenaries, Trinsic is where one shouldst go to find the most honorable warriors of the realm."; break;
                    case 1: response = "Though known as a haven for men and women of honor who wish to hone their martial skills, Trinsic also supports a large guild of architects and engineers."; break;
                    case 2: response = "Trinsic? Why, thou couldst find thy way there by merely following the south road from Britain."; break;
                }
            }
            if (Insensitive.Speech(e.Speech, "Vesper"))
            {
                switch (Utility.Random(3))
                {
                    case 0: response = "Vesper? 'Tis a city in northeastern Britannia. Ore from the mines of Minoc are sent down river to be unloaded in Vesper."; break;
                    case 1: response = String.Format("Much of the crafts forged in Minoc find their way to Vesper by way of the river, {0}.", e.Mobile.Female ? "milady" : "milord"); break;
                    case 2: response = "Some claim Vesper is merely an extension of Minoc, calling it nothing more than a large marketplace for artisans to sell their wares."; break;
                }
            }
            if (Insensitive.Speech(e.Speech, "Yew"))
            {
                switch (Utility.Random(3))
                {
                    case 0: response = "Yew is a small, peaceful community of farmers in northwestern Britannia."; break;
                    case 1: response = "The High Court of Britannia is in Yew. 'Tis there that important cases that concern all of Britannia are decided, many of which are determined by Lord British, himself."; break;
                    case 2: response = "They say farmers and criminals are all who go to Yew, $milord/milady$, save for those hoping to visit one of the two."; break;
                }
            }
            if (Insensitive.Speech(e.Speech, "Britanny Bay"))
            {
                switch (Utility.Random(3))
                {
                    case 0: response = "'Tis the bay that touches the city of Britain."; break;
                    case 1: response = "Britanny Bay is the body of water on the edge of Britannia's capital."; break;
                    case 2: response = "Britanny Bay? Why, 'tis the waters that border the ports of Britain."; break;
                }
            }
            if (Insensitive.Speech(e.Speech, "Order of the Silver Serpent") || Insensitive.Speech(e.Speech, "Knights"))
            {
                switch (Utility.Random(3))
                {
                    case 0: response = "'Tis the order of knights who served Lord British in the battle against Lord Robere."; break;
                    case 1: response = "When Lord Robere challenged Lord British for his share of the realm, Lord British's faithful knights defended the kingdom."; break;
                    case 2: response = "Only the bravest knights belong to the Order of the Silver Serpent. 'Twas they who defeated the forces of Lord Robere."; break;
                }
            }
            if (Insensitive.Speech(e.Speech, "Robere") || Insensitive.Speech(e.Speech, "Lord Robere"))
            {
                switch (Utility.Random(4))
                {
                    case 0: response = "History claims that Lord Robere was once an honorable man. But he was overcome with greed and sought to take the entire realm by force."; break;
                    case 1: response = "Years ago, Lord Robere made claims to the lands under Lord British's domain. Were it not for the stout knights in the Order of the Silver Serpent, Lord Robere could very well have conquered the entire realm."; break;
                    case 2: response = "Though a kind and generous man in his youth, Lord Robere thirsted for power in his later years, so legends say."; break;
                    case 3: response = "Had not the Order of the Silver Serpent been ready to fight for Lord British, this very land could have belonged to the ambitious conqueror."; break;
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
                            case 0: response = "Love him like thine own brother, they say.  Fine, say I, but must I be so unnkind to mine own flesh and blood?"; break;
                            case 1: response = "Yes, it seems Lord British doth possess a rare gift for leadership...and I wish he would give it back."; break;
                            case 2: response = String.Format("Wert thou not so masterful, {0}, I would compare THEE to our illustrious ruler... but only a fool would confuse a fine {1} such as thee with Britannia'a royalty.", e.Mobile.Female ? "Milady" : "Milord", e.Mobile.Female ? "soul" : "man"); break;
                        }
                    }
                    else if (from.Karma >= 60)
                    {
                        switch (Utility.Random(3))
                        {
                            case 0: response = "Ah, thou dost speak of fair Britannia. Lord British, himself, wouldst be proud of thine efforts to keep it peaceful. Yet, those who travel claim it is but one realm of many."; break;
                            case 1: response = "I cannot speak but fondly on my homeland, the very land thou dost seek to keep safe. Yet I would not forgo an opportunity to see other lands."; break;
                            case 2: response = "I have lived in Britannia all my life, and so I thank thee for thine efforts to improve the lives of all who live here."; break;
                        }
                    }
                    else
                    {
                        switch (Utility.Random(3))
                        {
                            case 0: response = "Hmmm...  yes, Lord British. That's a difficult one...."; break;
                            case 1: response = "I care not for his empty promises or hollow beliefs, for they brought me nothing vain hope."; break;
                            case 2: response = String.Format("Please accept mine apology, o' noble {0}, but I care little about sharing mine opinions on Lord Britsh with thee.", e.Mobile.Female ? "lady" : "lord"); break;
                        }
                    }
                }
                else if (m_Mobile.Attitude == AttitudeLevel.Neutral)
                {
                    switch (Utility.Random(3))
                    {
                        case 0: response = ("I have always find Lord British to be fair and just in his rule."); break;
                        case 1: response = ("From my liege I ask no more than a strong but gently, guiding hand. Thus far, Lord British has shown just that."); break;
                        case 2: response = ("Lord British governs as any wise ruler would -- with an even hand and a thoughtful eye."); break;
                    }
                }
                else if (m_Mobile.Attitude == AttitudeLevel.Goodhearted)
                {
                    if (from.Karma <= -60)
                    {
                        switch (Utility.Random(3))
                        {
                            case 0: response = "Though thou might never know such, Lord British has done much to make our land one of prosperity."; break;
                            case 1: response = "Fear not, troubled soul. The worries that thou might have about our land will soon be made to vanish by Lord British."; break;
                            case 2: response = String.Format("It may not be apparent to thee, {0}, but Lord British's guidance has made Britannia great.", e.Mobile.Female ? "Milady" : "Milord", e.Mobile.Female ? "soul" : "man"); break;
                        }
                    }
                    else if (from.Karma >= 60)
                    {
                        switch (Utility.Random(3))
                        {
                            case 0: response = String.Format("As thou must surely know, {0} {1}, Lord British is the reason our land has prospered so.", e.Mobile.Female ? "Milady" : "Milord", e.Mobile.Name); break;
                            case 1: response = "I have little fear that all will be well in Britannia."; break;
                            case 2: response = String.Format("As must be plainly apparent to thee, {0}, Lord British's guidance has made Britannia great.", e.Mobile.Female ? "Milady" : "Milord"); break;
                        }
                    }
                    else
                    {
                        switch (Utility.Random(3))
                        {
                            case 0: response = "Lord British is a kind and generous ruler."; break;
                            case 1: response = "I do not know of another who would rule as fairly in Lord British's stead."; break;
                            case 2: response = "In truth, Lord British does his best to address any concerns we seem to have."; break;
                        }
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
                    case 0: response = "Various issues surface from time to time, such as taxation, invasion, protection form creatures of the wild."; break;
                    case 1: response = "Any land experiences difficult times, but it takes a wise ruler to lead his people through them."; break;
                    case 2: response = "Surely thou dost understand -- life cannot always be free of trouble."; break;
                }
            }
            if (Insensitive.Speech(e.Speech, "blackthorn"))
            {
                switch (Utility.Random(3))
                {
                    case 0: response = "I've heard that Lord Blackthorn has written a couple of books describing his philosophies in-depth. If thou dost find these, I imagine thou wouldst be able to say better than I what Blackthorn is about."; break;
                    case 1: response = "Lord Blackthorn and Lord British are still friends, or so I've heard. They just envision different futures for Britannia."; break;
                    case 2: response = "Lord Blackthorn wishes the freedom of choice in ALL things extended to everyone. Some say that he'd even sees the Orcs and Lizardmen as equals to the humans in Britannia."; break;
                }
            }
            if (Insensitive.Speech(e.Speech, "shamino"))
            {
                switch (Utility.Random(3))
                {
                    case 0: response = "Lord Shamino? Ah, yes, Lord British's friend and ally, I believe."; break;
                    case 1: response = "The name is familiar to me... ah, yes -- I believe he is the infrequent guest of Lord British."; break;
                    case 2: response = String.Format("Not a name bandied about often, to be sure, kind {0}. I suspect thou'rt refering to the oft-time companion of Lord British, himself.", e.Mobile.Female ? "lady" : "sir"); break;
                }
            }
            if (Insensitive.Speech(e.Speech, "Iolo"))
            {
                switch (Utility.Random(3))
                {
                    case 0: response = "Iolo? Ah, yes, Lord British's friend and ally, I believe."; break;
                    case 1: response = "The name is familiar to me... ah, yes -- I believe he is the infrequent guest of Lord British."; break;
                    case 2: response = String.Format("Not a name bandied about often, to be sure, kind {0}. I suspect thou'rt refering to the oft-time companion of Lord British, himself.", e.Mobile.Female ? "lady" : "sir"); break;
                }
            }
            if (Insensitive.Speech(e.Speech, "Dupre"))
            {
                switch (Utility.Random(3))
                {
                    case 0: response = "Dupre? Ah, yes, Lord British's friend and ally, I believe. If thou wert to search for him, I wouldst most recommend a tavern."; break;
                    case 1: response = "The name is familiar to me... ah, yes -- I believe he is the infrequent guest of Lord British. Mayhaps thou wilt find him sampling the local spirits."; break;
                    case 2: response = String.Format("Not a name bandied about often, to be sure, kind {0}. I suspect thou'rt refering to the oft-time fighting companion of Lord British, himself.", e.Mobile.Female ? "lady" : "sir"); break;
                }
            }
            if (Insensitive.Speech(e.Speech, "New Magincia"))
            {
                switch (Utility.Random(3))
                {
                    case 0: response = "New Magincia? Is there something wrong with the original?"; break;
                    case 1: response = String.Format("Forgive me, {0}, but I think thou dost suffer the madness of the drink.", e.Mobile.Female ? "milady" : "milord"); break;
                    case 2: response = String.Format("I have not heard of such a place, {0}. I suppose it could be a colony of Britannians who have settled in a new territory.", e.Mobile.Female ? "milady" : "milord"); break;
                }
            }
            if (Insensitive.Speech(e.Speech, "colony"))
            {
                switch (Utility.Random(3))
                {
                    case 0: response = String.Format("I know not of any, {0}.", e.Mobile.Female ? "milady" : "milord"); break;
                    case 1: response = "Rumors abound that Lord British wishes to send adventurous and resourceful individuals to settle unexplored areas."; break;
                    case 2: response = String.Format("I know not of which thou doth speak, {0}.", e.Mobile.Female ? "milady" : "milord"); break;
                }
            }
            if (Insensitive.Speech(e.Speech, "virtue") || Insensitive.Speech(e.Speech, "virtues") || Insensitive.Speech(e.Speech, "shrines") || Insensitive.Speech(e.Speech, "truth") || Insensitive.Speech(e.Speech, "love") || Insensitive.Speech(e.Speech, "courage") || Insensitive.Speech(e.Speech, "spirituality") || Insensitive.Speech(e.Speech, "valor") || Insensitive.Speech(e.Speech, "honor") || Insensitive.Speech(e.Speech, "justice") || Insensitive.Speech(e.Speech, "sacrifice") || Insensitive.Speech(e.Speech, "honesty") || Insensitive.Speech(e.Speech, "humility") || Insensitive.Speech(e.Speech, "compassion"))
            {
                switch (Utility.Random(3))
                {
                    case 0: response = "Shrines to the virtues are spread around our land. I've heard that they can even resurrect the dead."; break;
                    case 1: response = "Rest and health can be found at the shrines."; break;
                    case 2: response = "The shrines are rumored to have the power to resurrect the dead."; break;
                }
            }
            if (Insensitive.Speech(e.Speech, "avatar"))
            {
                switch (Utility.Random(4))
                {
                    case 0: response = "Who?"; break;
                    case 1: response = "I'm sorry, I know not of whom thou doth speak."; break;
                    case 2: response = "I have never heard of this tar person."; break;
                    case 3: response = "I cannot help thee."; break;
                }
            }
            if (Insensitive.Speech(e.Speech, "moongates"))
            {
                switch (Utility.Random(3))
                {
                    case 0: response = "The moongates? They are... doors to different parts of Britannia. Except the destinations of the doors change with the phases of the two moons."; break;
                    case 1: response = "Thou shouldst learn to use the moongates if thou dost plan to travel far. The key is in the phases of the moons."; break;
                    case 2: response = "If thou dost use the moongates to travel, then thou might not end up where thou had planned, unless thou hast learned how to use them correctly."; break;
                }
            }
            if (Insensitive.Speech(e.Speech, "moons"))
            {
                response = "Our moons are called Trammel and Felucca. They control the moongates.";
            }
            return response;
        }
    }
}