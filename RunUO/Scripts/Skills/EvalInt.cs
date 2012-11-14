using System;
using Server;
using Server.Mobiles;
using Server.Network;
using Server.Targeting;

namespace Server.SkillHandlers
{
	public class EvalInt
	{
		public static void Initialize()
		{
			SkillInfo.Table[16].Callback = new SkillUseCallback( OnUse );
		}

		public static TimeSpan OnUse( Mobile m )
		{
			m.Target = new EvalInt.InternalTarget();

            m.SendAsciiMessage("What do you wish to evaluate?");
			//m.SendLocalizedMessage( 500906 ); // What do you wish to evaluate?

			return TimeSpan.FromSeconds( 10.0 );
		}

		private class InternalTarget : Target
		{
			public InternalTarget() :  base ( 8, false, TargetFlags.None )
			{
			}

			protected override void OnTarget( Mobile from, object targeted )
			{
				if ( from == targeted )
				{
					from.LocalOverheadMessage( MessageType.Regular, 0x3B2, true, "Hmm, that person looks really silly." ); // Hmm, that person looks really silly.
				}
				else if ( targeted is TownCrier )
				{
					((TownCrier)targeted).PrivateOverheadMessage( MessageType.Regular, 0x3B2, true, "He looks smart enough to remember the news.  Ask him about it.", from.NetState ); // He looks smart enough to remember the news.  Ask him about it.
				}
                /*else if ( targeted is BaseVendor) && ((BaseVendor)targeted).IsInvulnerable )
                {
                    ((BaseVendor)targeted).PrivateOverheadMessage( MessageType.Regular, 0x3B2, true, "That person could probably calculate the cost of what you buy from them.", from.NetState ); // That person could probably calculate the cost of what you buy from them.
                }*/
                else if ( targeted is Mobile )
				{
					Mobile targ = (Mobile)targeted;

					int marginOfError = Math.Max( 0, 20 - (int)(from.Skills[SkillName.EvalInt].Value / 5) );

					int intel = targ.Int + Utility.RandomMinMax( -marginOfError, +marginOfError );
					int mana = ((targ.Mana * 100) / Math.Max( targ.ManaMax, 1 )) + Utility.RandomMinMax( -marginOfError, +marginOfError );

					int intMod = intel / 10;
					int mnMod = mana / 10;

					if ( intMod > 10 ) intMod = 10;
					else if ( intMod < 0 ) intMod = 0;

					if ( mnMod > 10 ) mnMod = 10;
					else if ( mnMod < 0 ) mnMod = 0;

					int body;

					if ( targ.Body.IsHuman )
						body = targ.Female ? 11 : 0;
					else
						body = 22;

					if ( from.CheckTargetSkill( SkillName.EvalInt, targ, 0.0, 100.0 ) )
					{
                        switch (body / 11)
                        {
                            case 0:
                                switch (intMod)
                                {
                                    case 0: targ.SayTo(from, true, "He looks slightly less intelligent than a rock."); break;
                                    case 1: targ.SayTo(from, true, "He looks fairly stupid."); break;
                                    case 2: targ.SayTo(from, true, "He looks not the brightest."); break;
                                    case 3: targ.SayTo(from, true, "He looks about average."); break;
                                    case 4: targ.SayTo(from, true, "He looks moderately intelligent."); break;
                                    case 5: targ.SayTo(from, true, "He looks very intelligent."); break;
                                    case 6: targ.SayTo(from, true, "He looks extremely intelligent."); break;
                                    case 7: targ.SayTo(from, true, "He looks extraordinarily intelligent."); break;
                                    case 8: targ.SayTo(from, true, "He looks like a formidable intellect, well beyond even the extraordinary."); break;
                                    case 9: targ.SayTo(from, true, "He looks like a definite genius."); break;
                                    case 10: targ.SayTo(from, true, "He looks superhumanly intelligent in a manner you cannot comprehend."); break;
                                }break;
                            case 1:
                                switch (intMod)
                                {
                                    case 0: targ.SayTo(from, true, "She looks slightly less intelligent than a rock."); break;
                                    case 1: targ.SayTo(from, true, "She looks fairly stupid."); break;
                                    case 2: targ.SayTo(from, true, "She looks not tShe brightest."); break;
                                    case 3: targ.SayTo(from, true, "She looks about average."); break;
                                    case 4: targ.SayTo(from, true, "She looks moderately intelligent."); break;
                                    case 5: targ.SayTo(from, true, "She looks very intelligent."); break;
                                    case 6: targ.SayTo(from, true, "She looks extremely intelligent."); break;
                                    case 7: targ.SayTo(from, true, "She looks extraordinarily intelligent."); break;
                                    case 8: targ.SayTo(from, true, "She looks like a formidable intellect, well beyond even tShe extraordinary."); break;
                                    case 9: targ.SayTo(from, true, "She looks like a definite genius."); break;
                                    case 10: targ.SayTo(from, true, "She looks superhumanly intelligent in a manner you cannot compreShend."); break;
                                } break;
                            case 2:
                                switch (intMod)
                                {
                                    case 0: targ.SayTo(from, true, "It looks slightly less intelligent than a rock."); break;
                                    case 1: targ.SayTo(from, true, "It looks fairly stupid."); break;
                                    case 2: targ.SayTo(from, true, "It looks not tIt brightest."); break;
                                    case 3: targ.SayTo(from, true, "It looks about average."); break;
                                    case 4: targ.SayTo(from, true, "It looks moderately intelligent."); break;
                                    case 5: targ.SayTo(from, true, "It looks very intelligent."); break;
                                    case 6: targ.SayTo(from, true, "It looks extremely intelligent."); break;
                                    case 7: targ.SayTo(from, true, "It looks extraordinarily intelligent."); break;
                                    case 8: targ.SayTo(from, true, "It looks like a formidable intellect, well beyond even tIt extraordinary."); break;
                                    case 9: targ.SayTo(from, true, "It looks like a definite genius."); break;
                                    case 10: targ.SayTo(from, true, "It looks superhumanly intelligent in a manner you cannot compreItnd."); break;
                                } break;
                        }

						//targ.PrivateOverheadMessage( MessageType.Regular, 0x3B2, 1038169 + intMod + body, from.NetState ); // He/She/It looks [slighly less intelligent than a rock.]  [Of Average intellect] [etc...]

                        if (from.Skills[SkillName.EvalInt].Base >= 76.0)
                        {
                            switch (mnMod)
                            {
                                case 0: targ.SayTo(from, true, "This being is at zero percent mental strength."); break;
                                case 1: targ.SayTo(from, true, "This being is at ten percent mental strength."); break;
                                case 2: targ.SayTo(from, true, "This being is at twenty percent mental strength."); break;
                                case 3: targ.SayTo(from, true, "This being is at thirty percent mental strength."); break;
                                case 4: targ.SayTo(from, true, "This being is at forty percent mental strength."); break;
                                case 5: targ.SayTo(from, true, "This being is at fifty percent mental strength."); break;
                                case 6: targ.SayTo(from, true, "This being is at sixty percent mental strength."); break;
                                case 7: targ.SayTo(from, true, "This being is at seventy percent mental strength."); break;
                                case 8: targ.SayTo(from, true, "This being is at eighty percent mental strength."); break;
                                case 9: targ.SayTo(from, true, "This being is at ninety percent mental strength."); break;
                                case 10: targ.SayTo(from, true, "This being is at one-hundred percent mental strength."); break;
                            }
                           // targ.PrivateOverheadMessage(MessageType.Regular, 0x3B2, 1038202 + mnMod); // That being is at [10,20,...] percent mental strength.
                        }
					} 
					else 
					{
                        switch (body / 11)
                        {
                            case 0: targ.SayTo(from, true, "You cannot quite judge his mental abilities."); break;
                            case 1: targ.SayTo(from, true, "You cannot quite judge her mental abilities."); break;
                            case 2: targ.SayTo(from, true, "You cannot quite judge its mental abilities."); break;
                        }
						//targ.PrivateOverheadMessage( MessageType.Regular, 0x3B2, 1038166 + (body / 11), from.NetState ); // You cannot judge his/her/its mental abilities.
					}
				}
				else if ( targeted is Item )
				{
                    from.Send(new AsciiMessage(((Item)targeted).Serial, ((Item)targeted).ItemID, MessageType.Regular, 0, 3, "", "It looks smarter than a rock, but dumber than a piece of wood."));
					//((Item)targeted).SendLocalizedMessageTo( from, 500908, "" ); // It looks smarter than a rock, but dumber than a piece of wood.
				}
			}
		}
	}
}