using System;
using Server;
using Server.Mobiles;

namespace Server
{
    public partial class BaseSpeech
    {
        public static string BritainLow(BaseCreature m_Mobile, SpeechEventArgs e)
        {
            string response = null;

            if (Insensitive.Speech(e.Speech, "skeleton") || Insensitive.Speech(e.Speech, "graves") || Insensitive.Speech(e.Speech, "graveyard") || Insensitive.Speech(e.Speech, "crypt") || Insensitive.Speech(e.Speech, "undead") || Insensitive.Speech(e.Speech, "cemetery") || Insensitive.Speech(e.Speech, "mausoleum"))
            {
                switch (Utility.Random(3))
                {
                    case 0: response = ("I don' like undead."); break;
                    case 1: response = ("There's a cemetery way to the north."); break;
                    case 2: response = ("Lots of skeletons have come to life in the cemetery."); break;
                }
            }
            else if (Insensitive.Speech(e.Speech, "orc") || Insensitive.Speech(e.Speech, "camp"))
            {
                response = ("Orcs live in camps. They's dangerous, too.");
            }
            else if (Insensitive.Speech(e.Speech, "theif") || Insensitive.Speech(e.Speech, "thiev") || Insensitive.Speech(e.Speech, "steal"))
            {
                switch (Utility.Random(3))
                {
                    case 0: response = ("I dunno about thiefy types."); break;
                    case 1: response = ("I once had my pocket picked."); break;
                    case 2: response = ("Thou a thief? Don' steal nothin' from me!"); break;
                }
            }
            else if (Insensitive.Speech(e.Speech, "dummy") || Insensitive.Speech(e.Speech, "training dummy") || Insensitive.Speech(e.Speech, "dummies") || Insensitive.Speech(e.Speech, "training dummies"))
            {
                switch (Utility.Random(2))
                {
                    case 0: response = ("Ummm... I think the keep's got some dummies."); break;
                    case 1: response = ("Up North, near to one o' the inns there's some sword-fightin' dummies."); break;
                }
            }
            else if (Insensitive.Speech(e.Speech, "guild"))
            {
                response = ("There's so many, I can't keep track of 'em.");
            }
            else if (Insensitive.Speech(e.Speech, "bridge"))
            {
                response = ("Ah, there's lots of bridges!");
            }
            else if (Insensitive.Speech(e.Speech, "where is"))
            {
                response = ("'Fraid I can't help, I dunno where.");
            }
            else if (Insensitive.Speech(e.Speech, "tavern"))
            {
                switch (Utility.Random(2))
                {
                    case 0: response = ("I like taverns. Heh!"); break;
                    case 1: response = ("I don't like taverns. Bah! Always gettin' pawed there."); break;
                }
            }
            else if (Insensitive.Speech(e.Speech, "inn"))
            {
                response = ("Inns are fancy places for the rich to sleep.");
            }
            else if (Insensitive.Speech(e.Speech, "temple") || Insensitive.Speech(e.Speech, "death") || Insensitive.Speech(e.Speech, "ankh") || Insensitive.Contains(e.Speech, "resur"))
            {
                switch (Utility.Random(2))
                {
                    case 0: response = ("They say the ankh can bring folks back to life."); break;
                    case 1: response = ("The ankh be in the temple by the river."); break;
                }
            }
            else if (Insensitive.Speech(e.Speech, "bay") || Insensitive.Speech(e.Speech, "river") || Insensitive.Speech(e.Speech, "ocean") || Insensitive.Speech(e.Speech, "brittany") || Insensitive.Speech(e.Speech, "narrows") || Insensitive.Speech(e.Speech, "neck") || Insensitive.Speech(e.Speech, "moat"))
            {
                response = ("'Tis powerful wet, it is.");
            }
            else if (Insensitive.Speech(e.Speech, "where am i") || Insensitive.Speech(e.Speech, "lost"))
            {
                 response = ("Eh? Thou'rt right here.");
            }

            return response;
        }
    }
}