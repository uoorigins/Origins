using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Server.Commands;
using System.IO;

namespace Server.Misc
{
    public static class SpawnBarrels
    {
        private const bool m_Enabled = true;
        public static bool Enabled { get { return m_Enabled; } }

        public static void Initialize()
        {
            if ( Enabled )
            {
                Decorate.Generate( "Data/Decoration/Britannia/Barrels", Map.Felucca );
            }
        }
    }
}
