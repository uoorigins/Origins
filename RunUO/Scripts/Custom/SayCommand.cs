using System;
using System.Reflection;
using Server.Items;
using Server.Targeting;
using Server.Network;

namespace Server.Commands
{
    public class Say
    {
        public static void Initialize()
        {
            CommandSystem.Register("Say", AccessLevel.GameMaster, new CommandEventHandler(Say_OnCommand));
            CommandSystem.Register("PlayMusic", AccessLevel.GameMaster, new CommandEventHandler(PlayMusic_OnCommand));
        }

        [Usage("PlayMusic [text]")]
        [Description("Makes a targeted object speak.")]
        private static void PlayMusic_OnCommand(CommandEventArgs e)
        {
            Mobile m = e.Mobile;
            string toSend = e.ArgString.Trim();

            if (toSend.Length > 0)
                m.Send(PlayMusic.GetInstance((MusicName)(int.Parse(toSend))));
        }

        [Usage("Say [text]")]
        [Description("Makes a targeted object speak.")]
        private static void Say_OnCommand(CommandEventArgs e)
        {
            string message = "";
            if (e.Length >= 1)
                message = e.GetString(0);
            e.Mobile.Target = new SayTarget(message);
            e.Mobile.SendAsciiMessage("What object do you want to make speak?");
        }

        private class SayTarget : Target
        {
            private string m_Message;

            public SayTarget(string message): base(15, false, TargetFlags.None)
            {
                m_Message = message;
            }

            protected override void OnTarget(Mobile from, object targ)
            {
                if (targ is Item)
                {
                    ((Item)targ).PublicOverheadMessage(Network.MessageType.Regular, 0x3B2, true, m_Message);
                    return;
                }
                else if (targ is Mobile)
                {
                    ((Mobile)targ).Say(true, m_Message);
                    return;
                }
                else
                {
                    from.SendAsciiMessage("You cannot get that to speak!");
                }
            }
        }
    }
}
