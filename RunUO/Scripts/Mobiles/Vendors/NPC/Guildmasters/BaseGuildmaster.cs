using System;
using System.Collections.Generic;
using Server;
using Server.Items;
using Server.Network;

namespace Server.Mobiles
{
	public abstract class BaseGuildmaster : BaseVendor
	{
		private List<SBInfo> m_SBInfos = new List<SBInfo>();
		protected override List<SBInfo> SBInfos{ get { return m_SBInfos; } }

		public override bool IsActiveVendor{ get{ return false; } }

		public override bool ClickTitle{ get{ return false; } }

		public virtual int JoinCost{ get{ return 500; } }

		public virtual TimeSpan JoinAge{ get{ return TimeSpan.FromDays( 0.0 ); } }
		public virtual TimeSpan JoinGameAge{ get{ return TimeSpan.FromDays( 2.0 ); } }
		public virtual TimeSpan QuitAge{ get{ return TimeSpan.FromDays( 7.0 ); } }
		public virtual TimeSpan QuitGameAge{ get{ return TimeSpan.FromDays( 4.0 ); } }

		public override void InitSBInfo()
		{
		}

		public virtual bool CheckCustomReqs( PlayerMobile pm )
		{
			return true;
		}

		public virtual void SayGuildTo( Mobile m )
		{
            switch ((int)NpcGuild)
            {
                case 0: SayTo(m, true, "I am the guildmaster of The Default Guild of Super heroic Non-Player Characters."); break;
                case 1: SayTo(m, true, "I am the guildmaster of The Guild of Arcane Arts."); break;
                case 2: SayTo(m, true, "I am the guildmaster of The Warrior's Guild."); break;
                case 3: SayTo(m, true, "I am the guildmaster of The Society of Thieves."); break;
                case 4: SayTo(m, true, "I am the guildmaster of the League of Rangers."); break;
                case 5: SayTo(m, true, "If this were the head of a murderer, I would check for a bounty."); break;
                case 6: SayTo(m, true, "I am the guildmaster of The Healer's Guild."); break;
                case 7: SayTo(m, true, "I am the guildmaster of The Mining Cooperative."); break;
                case 8: SayTo(m, true, "I am the guildmaster of The Merchant's Association."); break;
                case 9: SayTo(m, true, "I am the guildmaster of The Order of Engineers."); break;
                case 10: SayTo(m, true, "I am the guildmaster of The Society of Clothiers."); break;
                case 11: SayTo(m, true, "I am the guildmaster of The Maritime Guild."); break;
                case 12: SayTo(m, true, "I am the guildmaster of The Bardic Collegium."); break;
                case 13: SayTo(m, true, "I am the guildmaster of The Fellowship of Blacksmiths."); break;
            }
			//SayTo( m, 1008055 + (int)NpcGuild );
		}

		public virtual void SayWelcomeTo( Mobile m )
		{
			SayTo( m, true, "Welcome to the guild! Thou shalt find that fellow members shall grant thee lower prices in shops." ); // Welcome to the guild! Thou shalt find that fellow members shall grant thee lower prices in shops.
		}

		public virtual void SayPriceTo( Mobile m )
		{
            SayTo(m, true, "There is a fee in gold coins for joining the guild : {0}", JoinCost.ToString());
			//m.Send( new MessageLocalizedAffix( Serial, Body, MessageType.Regular, SpeechHue, 3, 1008052, Name, AffixType.Append, JoinCost.ToString(), "" ) );
		}

		public virtual bool WasNamed( string speech )
		{
			string name = this.Name;

			return ( name != null && Insensitive.StartsWith( speech, name ) );
		}

		public override bool HandlesOnSpeech( Mobile from )
		{
			if ( from.InRange( this.Location, 2 ) )
				return true;

			return base.HandlesOnSpeech( from );
		}

		public override void OnSpeech( SpeechEventArgs e )
		{
			Mobile from = e.Mobile;

            if (CheckHome())
            {
                e.Handled = true;
                return;
            }

			if ( !e.Handled && from is PlayerMobile && from.InRange( this.Location, 2 ) && WasNamed( e.Speech ) )
			{
				PlayerMobile pm = (PlayerMobile)from;

				if ( e.HasKeyword( 0x0004 ) ) // *join* | *member*
				{
					if ( pm.NpcGuild == this.NpcGuild )
                        SayTo(from, true, "Thou art already a member of our guild."); // Thou art already a member of our guild.
					else if ( pm.NpcGuild != NpcGuild.None )
						SayTo( from, true, "Thou must resign from thy other guild first." ); // Thou must resign from thy other guild first.
					else if ( pm.GameTime < JoinGameAge || (pm.CreationTime + JoinAge) > DateTime.Now )
						SayTo( from, "You are too young to join my guild..." ); // You are too young to join my guild...
					else if ( CheckCustomReqs( pm ) )
						SayPriceTo( from );

					e.Handled = true;
				}
				else if ( e.HasKeyword( 0x0005 ) ) // *resign* | *quit*
				{
					if ( pm.NpcGuild != this.NpcGuild )
					{
						SayTo( from, true, "Thou dost not belong to my guild!" ); // Thou dost not belong to my guild!
					}
					else if ( (pm.NpcGuildJoinTime + QuitAge) > DateTime.Now || (pm.NpcGuildGameTime + QuitGameAge) > pm.GameTime )
					{
						SayTo( from, true, "You just joined my guild! You must wait a week to resign." ); // You just joined my guild! You must wait a week to resign.
					}
					else
					{
						SayTo( from, true, "I accept thy resignation." ); // I accept thy resignation.
						pm.NpcGuild = NpcGuild.None;
					}

					e.Handled = true;
				}
			}

			base.OnSpeech( e );
		}

		public override bool OnGoldGiven( Mobile from, Gold dropped )
		{
			if ( from is PlayerMobile && dropped.Amount == JoinCost )
			{
				PlayerMobile pm = (PlayerMobile)from;

				if ( pm.NpcGuild == this.NpcGuild )
				{
					SayTo( from, true, "Thou art already a member of our guild." ); // Thou art already a member of our guild.
				}
				else if ( pm.NpcGuild != NpcGuild.None )
				{
					SayTo( from, true, "Thou must resign from thy other guild first." ); // Thou must resign from thy other guild first.
				}
				else if ( pm.GameTime < JoinGameAge || (pm.CreationTime + JoinAge) > DateTime.Now )
				{
					SayTo( from, true, "You are too young to join my guild..." ); // You are too young to join my guild...
				}
				else if ( CheckCustomReqs( pm ) )
				{
					SayWelcomeTo( from );

					pm.NpcGuild = this.NpcGuild;
					pm.NpcGuildJoinTime = DateTime.Now;
					pm.NpcGuildGameTime = pm.GameTime;

					dropped.Delete();
					return true;
				}

				return false;
			}

			return base.OnGoldGiven( from, dropped );
		}

		public BaseGuildmaster( string title ) : base( title )
		{
			Title = String.Format( "the {0} {1}", title, Female ? "guildmistress" : "guildmaster" );
		}

		public BaseGuildmaster( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}
}