using System;
using Server;
using Server.Mobiles;

namespace Server
{
    public partial class BaseSpeech
    {
        public static string BritanniaMedium(BaseCreature m_Mobile, SpeechEventArgs e)
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
                    case 0: response = String.Format("Thou hast stumbled into {0}.", town); break;
                    case 1: response = String.Format("{0}.", town); break;
                    case 2: response = String.Format("{0} is where thou dost find thyself.", town); break;
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
            if ((Insensitive.Speech(e.Speech, "make") && Insensitive.Speech(e.Speech, "money")) || (Insensitive.Speech(e.Speech, "earn") && Insensitive.Speech(e.Speech, "money")) || (Insensitive.Speech(e.Speech, "get") && Insensitive.Speech(e.Speech, "money")))
            {
                if (m_Mobile.Attitude == AttitudeLevel.Wicked)
                {
                    switch (Utility.Random(3))
                    {
                        case 0: response = ("If thou dost want money, I'd expect thee to just take it. That's what thy kind usually does."); break;
                        case 1: response = ("Money can be found where thou dost look for it. If thou art too lazy to work for it, then accept what thou art given."); break;
                        case 2: response = ("Thou can always beg. Of course I wouldn't beg in the wilderness. Beggars have a strange habit of disappearing out there."); break;
                    }
                }
                else if (m_Mobile.Attitude == AttitudeLevel.Neutral)
                {
                    switch (Utility.Random(8))
                    {
                        case 0: response = ("There are many ways to earn thy keep, here in Britannia. I would suggest working for thy money."); break;
                        case 1: response = ("Thou can craft weapons, armor, clothes, furniture... many things to do. Find a shop with the tools and set to work. The raw materials should be there as well."); break;
                        case 2: response = ("If thou dost look in some shops, thou might find the raw materials and tools necessary to make the things thou dost need. Or thou could seel those things thou ARE able to make and collect the money."); break;
                        case 3: response = ("Most people in Britannia earn money honestly, by using their skills to make things and sell them. "); break;
                        case 4: response = ("If thou dost look in some of the shops, there may be the necessary raw materials and tools to make thy own items."); break;
                        case 5: response = ("If thou art serious about earning money, then look to the shops. Inside thou can find the raw materials and tools necessary to make thy own items."); break;
                        case 6: response = ("Using an axe on some trees can yield wood that a carpenter may buy from thee. Also thou can use a pickaxe to mine for ore in rocks."); break;
                        case 7: response = ("Hunting seems to be a profitable living to many. The hides, meat, feathers, and pelts that thou dost obtain can all be sold to various merchants."); break;
                    }
                }
                else if (m_Mobile.Attitude == AttitudeLevel.Goodhearted)
                {
                    switch (Utility.Random(7))
                    {
                        case 0: response = "I would tell thee to look to the shopkeepers for thy money. If thou art able to craft the things that they sell, then they would more than likely buy them from thee."; break;
                        case 1: response = "The shopkeepers are usually nice enough to let thee use their leftover materials and tools. Try selling what thou dost make to others."; break;
                        case 2: response = "If thou dost find a shop with some materials and tools, then work on making thy own items. Then thou can sell them for money."; break;
                        case 3: response = "Thou could hunt for meat to sell to butchers, rid us of some of the detestable monsters that roam so freely, or even collect wood from trees to sell to carpenters. Of course thou would need an axe for that."; break;
                        case 4: response = "If thou art interested in earning money, thou can collect raw materials to sell or use. Many shopkeepers let people use their tools, and even sometimes their left-overs to craft their own items."; break;
                        case 5: response = "Hunting seems to do wonders for many bank accounts. Try selling the hides to the tanners and the meat to the butchers."; break;
                        case 6: response = "If thou catches a bird, Thou can sell it to a cook, and a bowyer may appreciate the feathers."; break;
                    }
                }
            }
            if (Insensitive.Speech(e.Speech, "camp"))
            {
                switch (Utility.Random(3))
                {
                    case 0: response = "When I think I may have to sleep out of doors, I make sure that I have my bedroll and some kindling with me."; break;
                    case 1: response = "I try to not be without a bedroll and some kindling when I think I may have to camp in the wilderness."; break;
                    case 2: response = "Make sure to keep kindling with thee if thou art thinking of camping. Sometimes 'tis a difficult thing to find. Of course, a bedroll is quite necessary also."; break;
                }
            }
            if ((Insensitive.Speech(e.Speech, "how") && Insensitive.Speech(e.Speech, "quit")) || (Insensitive.Speech(e.Speech, "log") && Insensitive.Speech(e.Speech, "off")) || Insensitive.Speech(e.Speech, "logoff"))
            {
                switch (Utility.Random(2))
                {
                    case 0: response = "If thou art in need of a rest from this world, make thyself a camp, or check in to an Inn."; break;
                    case 1: response = "Thou can either camp or check in to an Inn. Either way thou art likely to find the respite thou art looking for."; break;
                }
            }
            if (Insensitive.Speech(e.Speech, "bedroll"))
            {
                switch (Utility.Random(3))
                {
                    case 0: response = "A bedroll can be purchased from a provisioner's shop."; break;
                    case 1: response = "If thou art looking for camping equipment, look to a provisioner."; break;
                    case 2: response = "A provisioner should be able to supply thee with bedrolls."; break;
                }
            }
            if (Insensitive.Speech(e.Speech, "kindling"))
            {
                switch (Utility.Random(3))
                {
                    case 0: response = "All thou dost need is an axe and some wood to make kindling."; break;
                    case 1: response = "Any provisioner should sell som ekindling to adventurers like thee."; break;
                    case 2: response = "Thou can easily make thy own kindling with an axe and some wood. Or thou can purchase some from a provisioner."; break;
                }
            }
            if (Insensitive.Speech(e.Speech, "cave") || Insensitive.Speech(e.Speech, "dungeon") || Insensitive.Speech(e.Speech, "destard") || Insensitive.Speech(e.Speech, "despise") || Insensitive.Speech(e.Speech, "shame") || Insensitive.Speech(e.Speech, "deceit") || Insensitive.Speech(e.Speech, "hythloth") || Insensitive.Speech(e.Speech, "wrong") || Insensitive.Speech(e.Speech, "covetous"))
            {
                switch (Utility.Random(5))
                {
                    case 0: response = "The dugeons and caves under Britannia are rumored to have treasures and excitements in abundance. Though I don't know many who have returned unscathed from them."; break;
                    case 1: response = "If thou art able to fight thy way through, the dungeons beneath Britannia can make thee wealthy."; break;
                    case 2: response = "I seem to recall the names of a few of the dungeons... Despise, Deceit, Shame... I don't know any more."; break;
                    case 3: response = "Umm... I can recall two more of the dungeons, I think. Hythloth is one, and Wrong is the other."; break;
                    case 4: response = "The dungeons that people tend to forget about are Covetous and Destard. At least I usually do."; break;
                }
            }
            if (Insensitive.Speech(e.Speech, "gold") || Insensitive.Speech(e.Speech, "treasure"))
            {
                switch (Utility.Random(3))
                {
                    case 0: response = "Gold can be found on the bodies of the more dangerous monsters of Britannia."; break;
                    case 1: response = "Treasure abounds in the dungeons beneath Britannia. The creatures themselves sometimes have things on them, also."; break;
                    case 2: response = "Look to the dungeons or the monsters of Britannia if thou dost want gold and treasure."; break;
                }
            }
            if (Insensitive.Speech(e.Speech, "Britannia"))
            {
                if (from.Karma <= -60)
                {
                    switch (Utility.Random(3))
                    {
                        case 0: response = "Hast thou not performed enough cruelty to Lord British's lands?"; break;
                        case 1: response = String.Format("Please {0}, I will speak not of my feelings of my beloved homeland.", e.Mobile.Female ? "lady" : "sir"); break;
                        case 2: response = String.Format("Please forgive me, {0}, but I will tell thee nothing of Britannia, for I fear what thou wilt do further.", e.Mobile.Female ? "lady" : "sir"); break;
                    }
                }
                else if (from.Karma >= 60)
                {
                    switch (Utility.Random(3))
                    {
                        case 0: response = String.Format("Ah, {0}, surely thou dost know as much about the land as I. Why else wouldst thou do such good deeds for its people?", e.Mobile.Female ? "milady" : "milord"); break;
                        case 1: response = "What canst I tell one who has done so much? Nay, 'tis I who should be asking thee for advice."; break;
                        case 2: response = String.Format("Why, all of Lord British's realm is called Britannia. But fear not, $good sir/fair lady$, with deeds such as thine, thou wilt surely be honored by Lord British, himself, soon enough.", e.Mobile.Female ? "fair lady" : "good sir"); break;
                    }
                }
                else
                {
                    switch (Utility.Random(3))
                    {
                        case 0: response = "Britannia? Truly the lands of Lord British."; break;
                        case 1: response = "Good times or ill, I will always love my homeland,."; break;
                        case 2: response = "'Tis called Britannia in honor of our liege, Lord British."; break;
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
                    switch (Utility.Random(3))
                    {
                        case 0: response = "Aye, Lord British does the commanding in this realm. And we puppets are to jump when he so bids it."; break;
                        case 1: response = "Dost thou wish for me merely to repeat what surely other sheep have said before me? Well and good then -- Lord British is the finest leige I have ever been privileged to grovel before."; break;
                        case 2: response = String.Format("Now is not the time to talk about ruler, Good {0}.", e.Mobile.Female ? "Lady" : "Sir"); break;
                    }
                }
                else if (m_Mobile.Attitude == AttitudeLevel.Neutral)
                {
                    switch (Utility.Random(3))
                    {
                        case 0: response = "Lord British has always a fair and just ruler over this land."; break;
                        case 1: response = "I have no qualms about the way Lord British governs us."; break;
                        case 2: response = String.Format("Lord Britsh has done nothing to hurt my work as a {0}.", GetJob(m_Mobile)); break;
                    }
                }
                else if (m_Mobile.Attitude == AttitudeLevel.Goodhearted)
                {
                    switch (Utility.Random(3))
                    {
                        case 0: response = "A wiser, kinder ruler I have not heard of."; break;
                        case 1: response = "I think Lord British makes for a fine ruler."; break;
                        case 2: response = "We have a fine ruler in Lord British."; break;
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
                    case 0: response = "Blackthorn's written a book or two about himself. I haven't read them, myself, but I bet they say alot about the man."; break;
                    case 1: response = "I hear that Blackthorn and Lord British still play chess together. Kinda odd, considering their differences, I think."; break;
                    case 2: response = "Blackthorn believes in the freedom of choices. Even the WRONG choices that could lead to disaster."; break;
                }
            }
            if (Insensitive.Speech(e.Speech, "shamino"))
            {
                switch (Utility.Random(3))
                {
                    case 0: response = String.Format("Shamino? Nay, {0}, I cannot help thee.", e.Mobile.Female ? "fair lady" : "kind sir"); break;
                    case 1: response = String.Format("Shamino? I beg thee pardon, {0}, but the words mean nothing to me.", e.Mobile.Female ? "milady" : "milord"); break;
                    case 2: response = String.Format("I am sorry, {0}, I do not recognize the name.", e.Mobile.Female ? "milady" : "milord"); break;
                }
            }
            if (Insensitive.Speech(e.Speech, "Iolo"))
            {
                switch (Utility.Random(3))
                {
                    case 0: response = String.Format("Iolo? Nay, {0}, I cannot help thee?", e.Mobile.Female ? "fair lady" : "kind sir"); break;
                    case 1: response = String.Format("Iolo? I beg thee pardon, {0}, but the words mean nothing to me.", e.Mobile.Female ? "milady" : "milord"); break;
                    case 2: response = String.Format("I am sorry, {0}, I do not recognize the name.", e.Mobile.Female ? "milady" : "milord"); break;
                }
            }
            if (Insensitive.Speech(e.Speech, "Dupre"))
            {
                switch (Utility.Random(3))
                {
                    case 0: response = String.Format("Dupre? Nay, {0}, I cannot help thee?", e.Mobile.Female ? "fair lady" : "kind sir"); break;
                    case 1: response = String.Format("Dupre? I beg thee pardon, {0}, but the words mean nothing to me.", e.Mobile.Female ? "milady" : "milord"); break;
                    case 2: response = String.Format("I am sorry, {0}, I do not recognize the name.", e.Mobile.Female ? "milady" : "milord"); break;
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
            if (Insensitive.Speech(e.Speech, "other lands") || Insensitive.Speech(e.Speech, "other realms") || Insensitive.Speech(e.Speech, "many realms") || Insensitive.Speech(e.Speech, "many lands") || Insensitive.Speech(e.Speech, "other realm") || Insensitive.Speech(e.Speech, "one realm of many?"))
            {
                switch (Utility.Random(3))
                {
                    case 0: response = String.Format("My apologies, $milord/milady$, but I know of no other than Britannia.", e.Mobile.Female ? "milady" : "milord"); break;
                    case 1: response = String.Format("{0}, I do not understand. Realms other than Brittania? Surely thou dost make a jest.", e.Mobile.Female ? "Milady" : "Milord"); break;
                    case 2: response = String.Format("My apologies, {0}, but I know of no other than Britannia.", e.Mobile.Female ? "milady" : "milord"); break;
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
                    case 0: response = "Shrines to the virtues are spread around our land. 'Tis rumored that they will resurrect thee, if thou dost manage to get thyself killed."; break;
                    case 1: response = "Rest and health can be found at the shrines."; break;
                    case 2: response = "The power of resurrection is supposedly contained within the shrines. I know not whether 'tis true or no."; break;
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
                    case 0: response = "The moongates? They are doors that will lead thee around Britannia. The destinations of the moongates change in accordance to the phases of the two moons."; break;
                    case 1: response = "Thou shouldst learn to use the moongates if thou dost plan to travel far. Study the moons. The phases of the moons control the destinations of the moongates."; break;
                    case 2: response = "If thou dost use the moongates, then thou might not end up where thou had planned, unless thou hast learned how to use them correctly. The phases of the two moons are the key, thou knowest."; break;
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