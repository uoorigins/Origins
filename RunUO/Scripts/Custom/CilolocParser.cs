using System;
using System.Collections.Generic;
using System.IO;
using Server;
using Server.Items;

namespace Server.Commands
{
    public class ClilocParser
    {
        public static void Initialize()
        {
            CommandSystem.Register("Cliloc", AccessLevel.Administrator, new CommandEventHandler(Cliloc_OnCommand));
        }

        [Usage("Cliloc")]
        [Description("Converts Cliloc to Ascii")]
        public static void Cliloc_OnCommand(CommandEventArgs c)
        {
            string toSend = c.ArgString.Trim();

            if (toSend.Length > 0)
            {
                c.Mobile.Say(Parse(toSend));
            }
        }

        public static String Parse(String toSend)
        {
            string cfg = Path.Combine(Core.BaseDirectory, "Data/cliloc.cfg");

            if (File.Exists(cfg))
            {
                List<String> list = new List<String>();

                using (StreamReader ip = new StreamReader(cfg))
                {
                    string line;
                    //int number = Int32.Parse(toSend);
                    bool found = false;

                    while ((line = ip.ReadLine()) != null)
                    {
                        if (line.Equals("*********************") || line.Equals(""))
                            continue;

                        string[] split = line.Split(' ');

                        if (split[0].Equals("ID:"))
                        {
                            if (split[1].Equals(toSend))
                            {
                                found = true;
                                continue;
                            }
                        }

                        if (found)
                        {
                            return line;
                        }

                        /*SignEntry e = new SignEntry(
                            line.Substring( split[0].Length + 1 + split[1].Length + 1 + split[2].Length + 1 + split[3].Length + 1 + split[4].Length + 1 ),
                            new Point3D( Utility.ToInt32( split[2] ), Utility.ToInt32( split[3] ), Utility.ToInt32( split[4] ) ),
                            Utility.ToInt32( split[1] ), Utility.ToInt32( split[0] ) );

                        list.Add( e );*/
                    }
                }
                return "";
            }
            else
            {
                return "";
            }
        }
    }
}