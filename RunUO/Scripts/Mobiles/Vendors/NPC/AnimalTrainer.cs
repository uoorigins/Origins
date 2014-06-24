using System;
using System.Collections.Generic;
using Server;
using Server.ContextMenus;
using Server.Gumps;
using Server.Items;
using Server.Network;
using Server.Targeting;
using Server.Menus.Questions;

namespace Server.Mobiles
{
	public class AnimalTrainer : BaseVendor
	{
		private List<SBInfo> m_SBInfos = new List<SBInfo>();
		protected override List<SBInfo> SBInfos{ get { return m_SBInfos; } }

		[Constructable]
		public AnimalTrainer() : base( "the animal trainer" )
		{
			SetSkill( SkillName.AnimalLore, 64.0, 100.0 );
			SetSkill( SkillName.AnimalTaming, 90.0, 100.0 );
			SetSkill( SkillName.Veterinary, 65.0, 88.0 );
		}

		public override void InitSBInfo()
		{
			m_SBInfos.Add( new SBAnimalTrainer() );
		}

		public override Item ShoeType
		{
            get { return RandomBoots(0); }
		}

		public override int GetShoeHue()
		{
			return 0;
		}

		public override void InitOutfit()
		{
			base.InitOutfit();

			AddItem( (Item)new QuarterStaff() );
		}

		private class StableEntry : ContextMenuEntry
		{
			private AnimalTrainer m_Trainer;
			private Mobile m_From;

			public StableEntry( AnimalTrainer trainer, Mobile from ) : base( 6126, 12 )
			{
				m_Trainer = trainer;
				m_From = from;
			}

			public override void OnClick()
			{
				m_Trainer.BeginStable( m_From );
			}
		}

        private class ClaimListMenu : QuestionMenu
        {
            private AnimalTrainer m_Trainer;
            private Mobile m_From;
            private List<BaseCreature> m_List;

            public ClaimListMenu( AnimalTrainer trainer, Mobile from, List<BaseCreature> list )
                : base( "Select a pet to retrieve from the stables:", null )
            {
                m_Trainer = trainer;
                m_From = from;
                m_List = list;

                List<String> answersList = new List<String>();

                for ( int i = 0; i < list.Count; ++i )
                {
                    BaseCreature pet = list[i];

                    if ( pet == null || pet.Deleted )
                        continue;

                    answersList.Add( pet.Name );
                }

                Answers = answersList.ToArray();
            }

            public override void OnResponse( NetState state, int index )
            {
                if ( index >= 0 && index < m_List.Count )
                    m_Trainer.EndClaimList( m_From, m_List[index] );
            }
        }

		private class ClaimListGump : Gump
		{
			private AnimalTrainer m_Trainer;
			private Mobile m_From;
			private List<BaseCreature> m_List;

			public ClaimListGump( AnimalTrainer trainer, Mobile from, List<BaseCreature> list ) : base( 50, 50 )
			{
				m_Trainer = trainer;
				m_From = from;
				m_List = list;

				from.CloseGump( typeof( ClaimListGump ) );

				AddPage( 0 );

				AddBackground( 0, 0, 325, 50 + (list.Count * 20), 9250 );
				AddAlphaRegion( 5, 5, 315, 40 + (list.Count * 20) );

				AddHtml( 15, 15, 275, 20, "<BASEFONT COLOR=#FFFFFF>Select a pet to retrieve from the stables:</BASEFONT>", false, false );

				for ( int i = 0; i < list.Count; ++i )
				{
					BaseCreature pet = list[i];

					if ( pet == null || pet.Deleted )
						continue;

					AddButton( 15, 39 + (i * 20), 10006, 10006, i + 1, GumpButtonType.Reply, 0 );
					AddHtml( 32, 35 + (i * 20), 275, 18, String.Format( "<BASEFONT COLOR=#C0C0EE>{0}</BASEFONT>", pet.Name ), false, false );
				}
			}

			public override void OnResponse( NetState sender, RelayInfo info )
			{
				int index = info.ButtonID - 1;

				if ( index >= 0 && index < m_List.Count )
					m_Trainer.EndClaimList( m_From, m_List[index] );
			}
		}

		private class ClaimAllEntry : ContextMenuEntry
		{
			private AnimalTrainer m_Trainer;
			private Mobile m_From;

			public ClaimAllEntry( AnimalTrainer trainer, Mobile from ) : base( 6127, 12 )
			{
				m_Trainer = trainer;
				m_From = from;
			}

			public override void OnClick()
			{
				m_Trainer.Claim( m_From );
			}
		}

		public override void AddCustomContextEntries( Mobile from, List<ContextMenuEntry> list )
		{
			if ( from.Alive )
			{
				list.Add( new StableEntry( this, from ) );

				if ( from.Stabled.Count > 0 )
					list.Add( new ClaimAllEntry( this, from ) );
			}

			base.AddCustomContextEntries( from, list );
		}

		public static int GetMaxStabled( Mobile from )
		{
			double taming = from.Skills[SkillName.AnimalTaming].Value;
			double anlore = from.Skills[SkillName.AnimalLore].Value;
			double vetern = from.Skills[SkillName.Veterinary].Value;
			double sklsum = taming + anlore + vetern;

			int max;

			if ( sklsum >= 240.0 )
				max = 5;
			else if ( sklsum >= 200.0 )
				max = 4;
			else if ( sklsum >= 160.0 )
				max = 3;
			else
				max = 2;

			if ( taming >= 100.0 )
				max += (int)((taming - 90.0) / 10);

			if ( anlore >= 100.0 )
				max += (int)((anlore - 90.0) / 10);

			if ( vetern >= 100.0 )
				max += (int)((vetern - 90.0) / 10);

			return max;
		}

		private class StableTarget : Target
		{
			private AnimalTrainer m_Trainer;

			public StableTarget( AnimalTrainer trainer ) : base( 12, false, TargetFlags.None )
			{
				m_Trainer = trainer;
			}

			protected override void OnTarget( Mobile from, object targeted )
			{
				if ( targeted is BaseCreature )
					m_Trainer.EndStable( from, (BaseCreature)targeted );
				else if ( targeted == from )
					m_Trainer.SayTo( from, true, "HA HA HA! Sorry, I am not an inn." ); // HA HA HA! Sorry, I am not an inn.
				else
					m_Trainer.SayTo( from, true, "You can't stable that!" ); // You can't stable that!
			}
		}

		public void BeginClaimList( Mobile from )
		{
			if ( Deleted || !from.CheckAlive() )
				return;

			List<BaseCreature> list = new List<BaseCreature>();

			for ( int i = 0; i < from.Stabled.Count; ++i )
			{
				BaseCreature pet = from.Stabled[i] as BaseCreature;

				if ( pet == null || pet.Deleted )
				{
					pet.IsStabled = false;
					from.Stabled.RemoveAt( i );
					--i;
					continue;
				}

				list.Add( pet );
			}

			if ( list.Count > 0 )
				from.SendMenu( new ClaimListMenu( this, from, list ) );
			else
				SayTo( from, true, "But I have no animals stabled with me at the moment!" ); // But I have no animals stabled with me at the moment!
		}

		public void EndClaimList( Mobile from, BaseCreature pet )
		{
			if ( pet == null || pet.Deleted || from.Map != this.Map || !from.InRange( this, 14 ) || !from.Stabled.Contains( pet ) || !from.CheckAlive() )
				return;

			if ( (from.Followers + pet.ControlSlots) <= from.FollowersMax )
			{
				pet.SetControlMaster( from );

				if ( pet.Summoned )
					pet.SummonMaster = from;

				pet.ControlTarget = from;
				pet.ControlOrder = OrderType.Follow;

				pet.MoveToWorld( from.Location, from.Map );

				pet.IsStabled = false;
				from.Stabled.Remove( pet );

                SayTo(from, true, "Here you go... and good day to you!"); // Here you go... and good day to you!
			}
			else
			{
				SayTo( from, true, String.Format("{0} remained in the stables because you have too many followers.", pet.Name )); // ~1_NAME~ remained in the stables because you have too many followers.
			}
		}

		public void BeginStable( Mobile from )
		{
			if ( Deleted || !from.CheckAlive() )
				return;

			/*if ( from.Stabled.Count >= GetMaxStabled( from ) )
			{
				SayTo( from, true, "You have too many pets in the stables!" ); // You have too many pets in the stables!
			}
			else
			{*/
				SayTo( from, true, "I charge 30 gold per pet for a real week's stable time. I will withdraw it from thy bank account. Which animal wouldst thou like to stable here?"); /* I charge 30 gold per pet for a real week's stable time.
										 * I will withdraw it from thy bank account.
										 * Which animal wouldst thou like to stable here?
										 */

				from.Target = new StableTarget( this );
			//}
		}

		public void EndStable( Mobile from, BaseCreature pet )
		{
			if ( Deleted || !from.CheckAlive() )
				return;

			if ( !pet.Controlled || pet.ControlMaster != from )
			{
				SayTo( from, true, "You do not own that pet!" ); // You do not own that pet!
			}
			else if ( pet.IsDeadPet )
			{
				SayTo( from, true, "Living pets only, please." ); // Living pets only, please.
			}
			else if ( pet.Summoned )
			{
				SayTo( from, true, "I can not stable summoned creatures." ); // I can not stable summoned creatures.
			}
			else if ( pet.Body.IsHuman )
			{
				SayTo( from, true, "HA HA HA! Sorry, I am not an inn." ); // HA HA HA! Sorry, I am not an inn.
			}
			else if ( (pet is PackLlama || pet is PackHorse || pet is Beetle) && (pet.Backpack != null && pet.Backpack.Items.Count > 0) )
			{
				SayTo( from, true, "You need to unload your pet." ); // You need to unload your pet.
			}
			else if ( pet.Combatant != null && pet.InRange( pet.Combatant, 12 ) && pet.Map == pet.Combatant.Map )
			{
				SayTo( from, true, "I'm sorry.  Your pet seems to be busy." ); // I'm sorry.  Your pet seems to be busy.
			}
			/*else if ( from.Stabled.Count >= GetMaxStabled( from ) )
			{
				SayTo( from, true, "You have too many pets in the stables!" ); // You have too many pets in the stables!
			}*/
			else
			{
				Container bank = from.FindBankNoCreate();

				if ( bank != null && bank.ConsumeTotal( typeof( Gold ), 30 ) )
				{
					pet.ControlTarget = null;
					pet.ControlOrder = OrderType.Stay;
					pet.Internalize();

					pet.SetControlMaster( null );
					pet.SummonMaster = null;

					pet.IsStabled = true;

					if ( Core.SE )	
						pet.Loyalty = BaseCreature.MaxLoyalty; // Wonderfully happy

					from.Stabled.Add( pet );

					SayTo( from, true, "Very well, thy pet is stabled. Thou mayst recover it by saying 'claim' to me. In one real world week, I shall sell it off if it is not claimed!" ); // Very well, thy pet is stabled. Thou mayst recover it by saying 'claim' to me. In one real world week, I shall sell it off if it is not claimed!
				}
				else
				{
					SayTo( from, true, "But thou hast not the funds in thy bank account!" ); // But thou hast not the funds in thy bank account!
				}
			}
		}

        public void Claim( Mobile from )
        {
            Claim( from, null );
        }

		public void Claim( Mobile from, String petName )
		{
			if ( Deleted || !from.CheckAlive() )
				return;

			bool claimed = false;
			int stabled = 0;

            bool claimByName = ( petName != null );

			for ( int i = 0; i < from.Stabled.Count; ++i )
			{
				BaseCreature pet = from.Stabled[i] as BaseCreature;

				if ( pet == null || pet.Deleted )
				{
					pet.IsStabled = false;
					from.Stabled.RemoveAt( i );
					--i;
					continue;
				}

				++stabled;

                if ( claimByName && !Insensitive.Equals( pet.Name, petName ) )
                    continue;

				if ( (from.Followers + pet.ControlSlots) <= from.FollowersMax )
				{
					pet.SetControlMaster( from );

					if ( pet.Summoned )
						pet.SummonMaster = from;

					pet.ControlTarget = from;
					pet.ControlOrder = OrderType.Follow;

					pet.MoveToWorld( from.Location, from.Map );

					pet.IsStabled = false;

					if ( Core.SE )
						pet.Loyalty = BaseCreature.MaxLoyalty; // Wonderfully Happy

					from.Stabled.RemoveAt( i );
					--i;

					claimed = true;
				}
				else
				{
					SayTo( from, 1049612, pet.Name ); // ~1_NAME~ remained in the stables because you have too many followers.
				}
			}

			if ( claimed )
                SayTo(from, true, "Here you go... and good day to you!"); // Here you go... and good day to you!
			else if ( stabled == 0 )
                SayTo(from, true, "But I have no animals stabled with me at the moment!"); // But I have no animals stabled with me at the moment!
            else if ( claimByName )
                BeginClaimList( from );
        }

		public override bool HandlesOnSpeech( Mobile from )
		{
			return true;
		}

		public override void OnSpeech( SpeechEventArgs e )
		{
            if (CheckHome())
            {
                e.Handled = true;
                return;
            }

            if ( !e.Handled && e.HasKeyword( 0x0008 ) ) // *stable*
			{
				e.Handled = true;
				BeginStable( e.Mobile );
			}
            else if ( !e.Handled && e.HasKeyword( 0x0009 ) ) // *claim*
            {
                e.Handled = true;

                int index = e.Speech.IndexOf( ' ' );

                if ( index != -1 )
                    Claim( e.Mobile, e.Speech.Substring( index ).Trim() );
                else
                    Claim( e.Mobile );
            }
			else
			{
				base.OnSpeech( e );
			}
		}

		public AnimalTrainer( Serial serial ) : base( serial )
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