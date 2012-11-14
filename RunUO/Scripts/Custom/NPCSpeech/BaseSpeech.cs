using System;
using Server;
using Server.Misc;
using Server.Targeting;
using Server.Items;
using Server.Network;
using Server.Mobiles;
using Server.Regions;

namespace Server
{
    public partial class BaseSpeech
    {
        public static bool CheckSpeech(BaseCreature m_Mobile, SpeechEventArgs e)
        {
            string response = null;
            Region reg = Region.Find(m_Mobile.Location, m_Mobile.Map);

            //Check Job

            //Check Region
            if (reg.Name == "Britain")
            {
                if (m_Mobile.Sophistication == SophisticationLevel.High)
                    response = BritainHigh(m_Mobile, e);
                else if (m_Mobile.Sophistication == SophisticationLevel.Medium)
                    response = BritainMedium(m_Mobile, e);
                else if (m_Mobile.Sophistication == SophisticationLevel.Low)
                    response = BritainLow(m_Mobile, e);
            }

            //Check World
            if (m_Mobile.Sophistication == SophisticationLevel.High)
                response = BritanniaHigh(m_Mobile, e);
            else if (m_Mobile.Sophistication == SophisticationLevel.Medium)
                response = BritanniaMedium(m_Mobile, e);
            else if (m_Mobile.Sophistication == SophisticationLevel.Low)
                response = BritanniaLow(m_Mobile, e);

            if (response != null)
                return true;
            else
                return false;
        }

        public static void GetSpeech(BaseCreature m_Mobile, SpeechEventArgs e)
        {
            string response = null;
            Region reg = Region.Find(m_Mobile.Location, m_Mobile.Map);

            //Check Job
            //TODO

            //Check Region
            if (reg.Name == "Britain")
            {
                if (m_Mobile.Sophistication == SophisticationLevel.High)
                    response = BritainHigh(m_Mobile, e);
                else if (m_Mobile.Sophistication == SophisticationLevel.Medium)
                    response = BritainMedium(m_Mobile, e);
                else if (m_Mobile.Sophistication == SophisticationLevel.Low)
                    response = BritainLow(m_Mobile, e);
            }

            //Check World
            if (response == null)
            {
                if (m_Mobile.Sophistication == SophisticationLevel.High)
                    response = BritanniaHigh(m_Mobile, e);
                else if (m_Mobile.Sophistication == SophisticationLevel.Medium)
                    response = BritanniaMedium(m_Mobile, e);
                else if (m_Mobile.Sophistication == SophisticationLevel.Low)
                    response = BritanniaLow(m_Mobile, e);
            }

            //No answer found
            if (response == null)
            {
                if (m_Mobile.Sophistication == SophisticationLevel.High)
                    response = DefaultHigh(m_Mobile, e.Mobile);
                else if (m_Mobile.Sophistication == SophisticationLevel.Medium)
                    response = DefaultMedium(m_Mobile, e.Mobile);
                else if (m_Mobile.Sophistication == SophisticationLevel.Low)
                    response = DefaultLow(m_Mobile, e.Mobile);
            }

            m_Mobile.Say(true, response);
        }
    }
}