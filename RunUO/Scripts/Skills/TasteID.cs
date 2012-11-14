using System;
using Server.Targeting;
using Server.Items;
using Server.Network;
using Server.Mobiles;

namespace Server.SkillHandlers
{
	public class TasteID
	{
		public static void Initialize()
		{
			SkillInfo.Table[(int)SkillName.TasteID].Callback = new SkillUseCallback( OnUse );
		}

		public static TimeSpan OnUse( Mobile m )
		{
			m.Target = new InternalTarget();

            m.SendAsciiMessage("What would you like to taste?");
			//m.SendLocalizedMessage( 502807 ); // What would you like to taste?

			return TimeSpan.FromSeconds( 10.0 );
		}

		[PlayerVendorTarget]
		private class InternalTarget : Target
		{
			public InternalTarget() :  base ( 2, false, TargetFlags.None )
			{
				AllowNonlocal = true;
			}

			protected override void OnTarget( Mobile from, object targeted )
			{
				if ( targeted is Mobile )
				{
                    from.SendAsciiMessage("You feel that such an action would be inappropriate.");
					//from.SendLocalizedMessage( 502816 ); // You feel that such an action would be inappropriate.
				}
				else if ( targeted is Food )
				{
					Food food = (Food) targeted;

					if ( from.CheckTargetSkill( SkillName.TasteID, food, 0, 100 ) )
					{
						if ( food.Poison != null )
						{
                            from.Send(new AsciiMessage(food.Serial, food.ItemID, MessageType.Regular, 0, 3, "", "It appears to have poison smeared on it."));
							//food.SendLocalizedMessageTo( from, 1038284 ); // It appears to have poison smeared on it.
						}
						else
						{
							// No poison on the food
                            from.Send(new AsciiMessage(food.Serial, food.ItemID, MessageType.Regular, 0, 3, "", "You detect nothing unusual about this substance."));
							//food.SendLocalizedMessageTo( from, 1010600 ); // You detect nothing unusual about this substance.
						}
					}
					else
					{
						// Skill check failed
                        from.Send(new AsciiMessage(food.Serial, food.ItemID, MessageType.Regular, 0, 3, "", "You cannot discern anything about this substance."));
						//food.SendLocalizedMessageTo( from, 502823 ); // You cannot discern anything about this substance.
					}
				}
				else if ( targeted is BasePotion )
				{
					BasePotion potion = (BasePotion) targeted;

                    from.Send(new AsciiMessage(potion.Serial, potion.ItemID, MessageType.Regular, 0, 3, "", "You already know what kind of potion that is."));
                    ((Item)targeted).OnSingleClick(from);
					//potion.SendLocalizedMessageTo( from, 502813 ); // You already know what kind of potion that is.
					//potion.SendLocalizedMessageTo( from, potion.LabelNumber );
				}
				else if ( targeted is PotionKeg )
				{
					PotionKeg keg = (PotionKeg) targeted;

					if ( keg.Held <= 0 )
					{
						keg.SendLocalizedMessageTo( from, 502228 ); // There is nothing in the keg to taste!
					}
					else
					{
						keg.SendLocalizedMessageTo( from, 502229 ); // You are already familiar with this keg's contents.
						keg.SendLocalizedMessageTo( from, keg.LabelNumber );
					}
				}
				else
				{
					// The target is not food or potion or potion keg.
					from.SendAsciiMessage( "That's not something you can taste." ); // That's not something you can taste.
				}
			}

			protected override void OnTargetOutOfRange( Mobile from, object targeted )
			{
				from.SendAsciiMessage( "You are too far away to taste that." ); // You are too far away to taste that.
			}
		}
	}
}