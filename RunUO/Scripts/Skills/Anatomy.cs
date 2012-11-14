using System;
using Server;
using Server.Mobiles;
using Server.Network;
using Server.Targeting;

namespace Server.SkillHandlers
{
	public class Anatomy
	{
		public static void Initialize()
		{
			SkillInfo.Table[(int)SkillName.Anatomy].Callback = new SkillUseCallback( OnUse );
		}

		public static TimeSpan OnUse( Mobile m )
		{
			m.Target = new Anatomy.InternalTarget();

            //m.SendAsciiMessage(m.Skills[SkillName.Anatomy].LastUsed.ToString());
           // m.Skills[SkillName.Anatomy].LastUsed = DateTime.Now;
           // m.SendAsciiMessage("Whom shall I examine?");
			//m.SendLocalizedMessage( 500321 ); // Whom shall I examine?

			return TimeSpan.FromSeconds( 10.0 );
		}

		private class InternalTarget : Target
		{
			public InternalTarget() :  base ( 8, false, TargetFlags.None )
			{
			}

			protected override void OnTarget( Mobile from, object targeted )
			{
                int OldHue = 0;

				if ( from == targeted )
				{
					from.LocalOverheadMessage( MessageType.Regular, 0x3B2, true, "You know yourself quite well enough already." ); // You know yourself quite well enough already.
				}
				else if ( targeted is TownCrier )
				{
                    ((TownCrier)targeted).SayTo(from, true, "This person looks fine to me, though he may have some news...");
					//((TownCrier)targeted).PrivateOverheadMessage( MessageType.Regular, 0x3B2, 500322, from.NetState ); // This person looks fine to me, though he may have some news...
				}
				else if ( targeted is BaseVendor && ((BaseVendor)targeted).IsInvulnerable )
                {
                    OldHue = ((BaseVendor)targeted).SpeechHue;
                    ((BaseVendor)targeted).SpeechHue = 0;
                    ((BaseVendor)targeted).SayTo(from, true, "That can not be inspected.");
					//((BaseVendor)targeted).PrivateOverheadMessage( MessageType.Regular, 0x3B2, 500326, from.NetState ); // That can not be inspected.
                    ((BaseVendor)targeted).SpeechHue = OldHue;
				}
				else if ( targeted is Mobile )
				{
					Mobile targ = (Mobile)targeted;

                    OldHue = targ.SpeechHue;
                    targ.SpeechHue = 0;

					int marginOfError = Math.Max( 0, 25 - (int)(from.Skills[SkillName.Anatomy].Value / 4) );

					int str = targ.Str + Utility.RandomMinMax( -marginOfError, +marginOfError );
					int dex = targ.Dex + Utility.RandomMinMax( -marginOfError, +marginOfError );
					int stm = ((targ.Stam * 100) / Math.Max( targ.StamMax, 1 )) + Utility.RandomMinMax( -marginOfError, +marginOfError );

					int strMod = str / 10;
					int dexMod = dex / 10;
					int stmMod = stm / 10;

					if ( strMod < 0 ) strMod = 0;
					else if ( strMod > 10 ) strMod = 10;

					if ( dexMod < 0 ) dexMod = 0;
					else if ( dexMod > 10 ) dexMod = 10;

					if ( stmMod > 10 ) stmMod = 10;
					else if ( stmMod < 0 ) stmMod = 0;

					if ( from.CheckTargetSkill( SkillName.Anatomy, targ, 0, 100 ) )
					{
                        switch (strMod)
                        {
                            case 0:
                                switch (dexMod)
                                {
                                    case 0: targ.SayTo(from, true, "That looks like they have trouble lifting small objects and like they barely manage to stay standing."); break;
                                    case 1: targ.SayTo(from, true, "That looks like they have trouble lifting small objects and very clumsy."); break;
                                    case 2: targ.SayTo(from, true, "That looks like they have trouble lifting small objects and somewhat uncoordinated."); break;
                                    case 3: targ.SayTo(from, true, "That looks like they have trouble lifting small objects and moderately dexterous."); break;
                                    case 4: targ.SayTo(from, true, "That looks like they have trouble lifting small objects and somewhat agile."); break;
                                    case 5: targ.SayTo(from, true, "That looks like they have trouble lifting small objects and very agile."); break;
                                    case 6: targ.SayTo(from, true, "That looks like they have trouble lifting small objects and extremely agile."); break;
                                    case 7: targ.SayTo(from, true, "That looks like they have trouble lifting small objects and extraordinarily agile."); break;
                                    case 8: targ.SayTo(from, true, "That looks like they have trouble lifting small objects and moves like quicksilver."); break;
                                    case 9: targ.SayTo(from, true, "That looks like they have trouble lifting small objects and faster than anything you have ever seen."); break;
                                    case 10: targ.SayTo(from, true, "That looks like they have trouble lifting small objects and superhumanly agile."); break;
                                } break;
                            case 1:
                                switch (dexMod)
                                {
                                    case 0: targ.SayTo(from, true, "That looks rather feeble and like they barely manage to stay standing."); break;
                                    case 1: targ.SayTo(from, true, "That looks rather feeble and very clumsy."); break;
                                    case 2: targ.SayTo(from, true, "That looks rather feeble and somewhat uncoordinated."); break;
                                    case 3: targ.SayTo(from, true, "That looks rather feeble and moderately dexterous."); break;
                                    case 4: targ.SayTo(from, true, "That looks rather feeble and somewhat agile."); break;
                                    case 5: targ.SayTo(from, true, "That looks rather feeble and very agile."); break;
                                    case 6: targ.SayTo(from, true, "That looks rather feeble and extremely agile."); break;
                                    case 7: targ.SayTo(from, true, "That looks rather feeble and extraordinarily agile."); break;
                                    case 8: targ.SayTo(from, true, "That looks rather feeble and moves like quicksilver."); break;
                                    case 9: targ.SayTo(from, true, "That looks rather feeble and faster than anything you have ever seen."); break;
                                    case 10: targ.SayTo(from, true, "That looks rather feeble and superhumanly agile."); break;
                                } break;
                            case 2:
                                switch (dexMod)
                                {
                                    case 0: targ.SayTo(from, true, "That looks somewhat weak and like they barely manage to stay standing."); break;
                                    case 1: targ.SayTo(from, true, "That looks somewhat weak and very clumsy."); break;
                                    case 2: targ.SayTo(from, true, "That looks somewhat weak and somewhat uncoordinated."); break;
                                    case 3: targ.SayTo(from, true, "That looks somewhat weak and moderately dexterous."); break;
                                    case 4: targ.SayTo(from, true, "That looks somewhat weak and somewhat agile."); break;
                                    case 5: targ.SayTo(from, true, "That looks somewhat weak and very agile."); break;
                                    case 6: targ.SayTo(from, true, "That looks somewhat weak and extremely agile."); break;
                                    case 7: targ.SayTo(from, true, "That looks somewhat weak and extraordinarily agile."); break;
                                    case 8: targ.SayTo(from, true, "That looks somewhat weak and moves like quicksilver."); break;
                                    case 9: targ.SayTo(from, true, "That looks somewhat weak and faster than anything you have ever seen."); break;
                                    case 10: targ.SayTo(from, true, "That looks somewhat weak and superhumanly agile."); break;
                                } break;
                            case 3:
                                switch (dexMod)
                                {
                                    case 0: targ.SayTo(from, true, "That looks to be of normal strength and like they barely manage to stay standing."); break;
                                    case 1: targ.SayTo(from, true, "That looks to be of normal strength and very clumsy."); break;
                                    case 2: targ.SayTo(from, true, "That looks to be of normal strength and somewhat uncoordinated."); break;
                                    case 3: targ.SayTo(from, true, "That looks to be of normal strength and moderately dexterous."); break;
                                    case 4: targ.SayTo(from, true, "That looks to be of normal strength and somewhat agile."); break;
                                    case 5: targ.SayTo(from, true, "That looks to be of normal strength and very agile."); break;
                                    case 6: targ.SayTo(from, true, "That looks to be of normal strength and extremely agile."); break;
                                    case 7: targ.SayTo(from, true, "That looks to be of normal strength and extraordinarily agile."); break;
                                    case 8: targ.SayTo(from, true, "That looks to be of normal strength and moves like quicksilver."); break;
                                    case 9: targ.SayTo(from, true, "That looks to be of normal strength and faster than anything you have ever seen."); break;
                                    case 10: targ.SayTo(from, true, "That looks to be of normal strength and superhumanly agile."); break;
                                } break;
                            case 4:
                                switch (dexMod)
                                {
                                    case 0: targ.SayTo(from, true, "That looks somewhat strong and like they barely manage to stay standing."); break;
                                    case 1: targ.SayTo(from, true, "That looks somewhat strong and very clumsy."); break;
                                    case 2: targ.SayTo(from, true, "That looks somewhat strong and somewhat uncoordinated."); break;
                                    case 3: targ.SayTo(from, true, "That looks somewhat strong and moderately dexterous."); break;
                                    case 4: targ.SayTo(from, true, "That looks somewhat strong and somewhat agile."); break;
                                    case 5: targ.SayTo(from, true, "That looks somewhat strong and very agile."); break;
                                    case 6: targ.SayTo(from, true, "That looks somewhat strong and extremely agile."); break;
                                    case 7: targ.SayTo(from, true, "That looks somewhat strong and extraordinarily agile."); break;
                                    case 8: targ.SayTo(from, true, "That looks somewhat strong and moves like quicksilver."); break;
                                    case 9: targ.SayTo(from, true, "That looks somewhat strong and faster than anything you have ever seen."); break;
                                    case 10: targ.SayTo(from, true, "That looks somewhat strong and superhumanly agile."); break;
                                } break;
                            case 5:
                                switch (dexMod)
                                {
                                    case 0: targ.SayTo(from, true, "That looks very strong and like they barely manage to stay standing."); break;
                                    case 1: targ.SayTo(from, true, "That looks very strong and very clumsy."); break;
                                    case 2: targ.SayTo(from, true, "That looks very strong and somewhat uncoordinated."); break;
                                    case 3: targ.SayTo(from, true, "That looks very strong and moderately dexterous."); break;
                                    case 4: targ.SayTo(from, true, "That looks very strong and somewhat agile."); break;
                                    case 5: targ.SayTo(from, true, "That looks very strong and very agile."); break;
                                    case 6: targ.SayTo(from, true, "That looks very strong and extremely agile."); break;
                                    case 7: targ.SayTo(from, true, "That looks very strong and extraordinarily agile."); break;
                                    case 8: targ.SayTo(from, true, "That looks very strong and moves like quicksilver."); break;
                                    case 9: targ.SayTo(from, true, "That looks very strong and faster than anything you have ever seen."); break;
                                    case 10: targ.SayTo(from, true, "That looks very strong and superhumanly agile."); break;
                                } break;
                            case 6:
                                switch (dexMod)
                                {
                                    case 0: targ.SayTo(from, true, "That looks extremely strong and like they barely manage to stay standing."); break;
                                    case 1: targ.SayTo(from, true, "That looks extremely strong and very clumsy."); break;
                                    case 2: targ.SayTo(from, true, "That looks extremely strong and somewhat uncoordinated."); break;
                                    case 3: targ.SayTo(from, true, "That looks extremely strong and moderately dexterous."); break;
                                    case 4: targ.SayTo(from, true, "That looks extremely strong and somewhat agile."); break;
                                    case 5: targ.SayTo(from, true, "That looks extremely strong and very agile."); break;
                                    case 6: targ.SayTo(from, true, "That looks extremely strong and extremely agile."); break;
                                    case 7: targ.SayTo(from, true, "That looks extremely strong and extraordinarily agile."); break;
                                    case 8: targ.SayTo(from, true, "That looks extremely strong and moves like quicksilver."); break;
                                    case 9: targ.SayTo(from, true, "That looks extremely strong and faster than anything you have ever seen."); break;
                                    case 10: targ.SayTo(from, true, "That looks extremely strong and superhumanly agile."); break;
                                } break;
                            case 7:
                                switch (dexMod)
                                {
                                    case 0: targ.SayTo(from, true, "That looks extraordinarily strong and like they barely manage to stay standing."); break;
                                    case 1: targ.SayTo(from, true, "That looks extraordinarily strong and very clumsy."); break;
                                    case 2: targ.SayTo(from, true, "That looks extraordinarily strong and somewhat uncoordinated."); break;
                                    case 3: targ.SayTo(from, true, "That looks extraordinarily strong and moderately dexterous."); break;
                                    case 4: targ.SayTo(from, true, "That looks extraordinarily strong and somewhat agile."); break;
                                    case 5: targ.SayTo(from, true, "That looks extraordinarily strong and very agile."); break;
                                    case 6: targ.SayTo(from, true, "That looks extraordinarily strong and extremely agile."); break;
                                    case 7: targ.SayTo(from, true, "That looks extraordinarily strong and extraordinarily agile."); break;
                                    case 8: targ.SayTo(from, true, "That looks extraordinarily strong and moves like quicksilver."); break;
                                    case 9: targ.SayTo(from, true, "That looks extraordinarily strong and faster than anything you have ever seen."); break;
                                    case 10: targ.SayTo(from, true, "That looks extraordinarily strong and superhumanly agile."); break;
                                } break;
                            case 8:
                                switch (dexMod)
                                {
                                    case 0: targ.SayTo(from, true, "That looks strong as an ox and like they barely manage to stay standing."); break;
                                    case 1: targ.SayTo(from, true, "That looks strong as an ox and very clumsy."); break;
                                    case 2: targ.SayTo(from, true, "That looks strong as an ox and somewhat uncoordinated."); break;
                                    case 3: targ.SayTo(from, true, "That looks strong as an ox and moderately dexterous."); break;
                                    case 4: targ.SayTo(from, true, "That looks strong as an ox and somewhat agile."); break;
                                    case 5: targ.SayTo(from, true, "That looks strong as an ox and very agile."); break;
                                    case 6: targ.SayTo(from, true, "That looks strong as an ox and extremely agile."); break;
                                    case 7: targ.SayTo(from, true, "That looks strong as an ox and extraordinarily agile."); break;
                                    case 8: targ.SayTo(from, true, "That looks strong as an ox and moves like quicksilver."); break;
                                    case 9: targ.SayTo(from, true, "That looks strong as an ox and faster than anything you have ever seen."); break;
                                    case 10: targ.SayTo(from, true, "That looks strong as an ox and superhumanly agile."); break;
                                } break;
                            case 9:
                                switch (dexMod)
                                {
                                    case 0: targ.SayTo(from, true, "That looks stronger than anything you have ever seen and like they barely manage to stay standing."); break;
                                    case 1: targ.SayTo(from, true, "That looks stronger than anything you have ever seen and very clumsy."); break;
                                    case 2: targ.SayTo(from, true, "That looks stronger than anything you have ever seen and somewhat uncoordinated."); break;
                                    case 3: targ.SayTo(from, true, "That looks stronger than anything you have ever seen and moderately dexterous."); break;
                                    case 4: targ.SayTo(from, true, "That looks stronger than anything you have ever seen and somewhat agile."); break;
                                    case 5: targ.SayTo(from, true, "That looks stronger than anything you have ever seen and very agile."); break;
                                    case 6: targ.SayTo(from, true, "That looks stronger than anything you have ever seen and extremely agile."); break;
                                    case 7: targ.SayTo(from, true, "That looks stronger than anything you have ever seen and extraordinarily agile."); break;
                                    case 8: targ.SayTo(from, true, "That looks stronger than anything you have ever seen and moves like quicksilver."); break;
                                    case 9: targ.SayTo(from, true, "That looks stronger than anything you have ever seen and faster than anything you have ever seen."); break;
                                    case 10: targ.SayTo(from, true, "That looks stronger than anything you have ever seen and superhumanly agile."); break;
                                } break;
                            case 10:
                                switch (dexMod)
                                {
                                    case 0: targ.SayTo(from, true, "That looks superhumanly strong and superhumanly agile."); break;
                                    case 1: targ.SayTo(from, true, "That looks superhumanly strong and superhumanly agile."); break;
                                    case 2: targ.SayTo(from, true, "That looks superhumanly strong and superhumanly agile."); break;
                                    case 3: targ.SayTo(from, true, "That looks superhumanly strong and superhumanly agile."); break;
                                    case 4: targ.SayTo(from, true, "That looks superhumanly strong and superhumanly agile."); break;
                                    case 5: targ.SayTo(from, true, "That looks superhumanly strong and superhumanly agile."); break;
                                    case 6: targ.SayTo(from, true, "That looks superhumanly strong and superhumanly agile."); break;
                                    case 7: targ.SayTo(from, true, "That looks superhumanly strong and superhumanly agile."); break;
                                    case 8: targ.SayTo(from, true, "That looks superhumanly strong and superhumanly agile."); break;
                                    case 9: targ.SayTo(from, true, "That looks superhumanly strong and superhumanly agile."); break;
                                    case 10: targ.SayTo(from, true, "That looks superhumanly strong and superhumanly agile."); break;
                                } break;

                        }
						//targ.PrivateOverheadMessage( MessageType.Regular, 0x3B2, 1038045 + (strMod * 11) + dexMod, from.NetState ); // That looks [strong] and [dexterous].

                        if (from.Skills[SkillName.Anatomy].Base >= 65.0)
                        {
                            switch(stmMod)
                            {
                                case 0: targ.SayTo(from, true, "This being is at zero percent endurance."); break;
                                case 1: targ.SayTo(from, true, "This being is at ten percent endurance."); break;
                                case 2: targ.SayTo(from, true, "This being is at twenty percent endurance."); break;
                                case 3: targ.SayTo(from, true, "This being is at thirty percent endurance."); break;
                                case 4: targ.SayTo(from, true, "This being is at forty percent endurance."); break;
                                case 5: targ.SayTo(from, true, "This being is at fifty percent endurance."); break;
                                case 6: targ.SayTo(from, true, "This being is at sixty percent endurance."); break;
                                case 7: targ.SayTo(from, true, "This being is at seventy percent endurance."); break;
                                case 8: targ.SayTo(from, true, "This being is at eighty percent endurance."); break;
                                case 9: targ.SayTo(from, true, "This being is at ninety percent endurance."); break;
                                case 10: targ.SayTo(from, true, "This being is at one-hundred percent endurance."); break;
                            }
                            //targ.PrivateOverheadMessage(MessageType.Regular, 0x3B2, 1038303 + stmMod, from.NetState); // That being is at [10,20,...] percent endurance.
                            targ.SpeechHue = OldHue;
                        }
					}
					else
					{
                        OldHue = targ.SpeechHue;
                        targ.SpeechHue = 0;
                        targ.SayTo(from, true, "You can not quite get a sense of their physical characteristics.");
                        targ.SpeechHue = OldHue;
						//targ.PrivateOverheadMessage( MessageType.Regular, 0x3B2, 1042666, from.NetState ); // You can not quite get a sense of their physical characteristics.
					}
				}
				else if ( targeted is Item )
				{
                    from.Send(new AsciiMessage(((Item)targeted).Serial, ((Item)targeted).ItemID, MessageType.Regular, 0, 3, "", "Only living things have anatomies!"));
					//((Item)targeted).SendLocalizedMessageTo( from, 500323, "" ); // Only living things have anatomies!
				}
			}
		}
	}
}