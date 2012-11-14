using System;
using System.Collections.Generic;
using Server;
using Server.ContextMenus;
using Server.Gumps;
using Server.Items;
using Server.Network;
using Server.Targeting;

namespace Server.Mobiles
{
	public class RabbitHerder : BaseVendor
	{
		private List<SBInfo> m_SBInfos = new List<SBInfo>();
		protected override List<SBInfo> SBInfos{ get { return m_SBInfos; } }
        public override bool Unprovokable { get { return true; } }

		[Constructable]
		public RabbitHerder() : base( "the rabbit herder" )
		{
			SetSkill( SkillName.AnimalLore, 64.0, 100.0 );
			SetSkill( SkillName.AnimalTaming, 90.0, 100.0 );
			SetSkill( SkillName.Veterinary, 65.0, 88.0 );
		}

		public override void InitSBInfo()
		{
			//m_SBInfos.Add( new SBAnimalTrainer() );
		}

		public override Item ShoeType
		{
			get{ return RandomBoots(); }
		}

		public override int GetShoeHue()
		{
			return 0;
		}

		public override void InitOutfit()
		{
			base.InitOutfit();

			AddItem( (Item)new Pitchfork() );
		}

		private class StableTarget : Target
		{
            private RabbitHerder m_Trainer;

			public StableTarget( RabbitHerder trainer ) : base( 12, false, TargetFlags.None )
			{
				m_Trainer = trainer;
			}

			protected override void OnTarget( Mobile from, object targeted )
			{
                if (targeted is Bunny)
                {
                    m_Trainer.EndStable(from, (BaseCreature)targeted);
                    return;
                }
                else if (targeted == from)
                    m_Trainer.SayTo(from, true, "HA HA HA! Sorry, I am not an inn."); // HA HA HA! Sorry, I am not an inn.
                else
                    m_Trainer.SayTo(from, true, "You can't stable that!"); // You can't stable that!
			}
		}

		public void BeginClaimList( Mobile from )
		{
            return;
		}

		public void EndClaimList( Mobile from, BaseCreature pet )
		{
            return;
		}

		public void BeginStable( Mobile from )
		{
			if ( Deleted || !from.CheckAlive() )
				return;

				SayTo( from, true, "Which bunny wouldst thou like to stable here?");

				from.Target = new StableTarget( this );
		}

        private static int Convert(string value)
        {
            try
            {
                int number = Int32.Parse(value);
                return number;
            }
            catch (FormatException)
            {
                return 0;
            }
            catch (ArgumentNullException)
            {
                return 0;
            }
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
			else
			{
                Timer.DelayCall( TimeSpan.FromSeconds( 0.5 ), new TimerCallback( pet.Delete ) );
                from.Tag = (Convert(from.Tag) - 1).ToString();
                return;
			}
		}

		public void Claim( Mobile from )
		{
            return;
		}

		public override bool HandlesOnSpeech( Mobile from )
		{
			return true;
		}

		public override void OnSpeech( SpeechEventArgs e )
		{
			if ( !e.Handled && e.HasKeyword( 0x0008 ) )
			{
				e.Handled = true;
				BeginStable( e.Mobile );
			}
			else
			{
                if (!e.Handled && e.Mobile.InRange(this.Location, 2))
                {
                    if (Insensitive.Speech(e.Speech, "olin") || Insensitive.Speech(e.Speech, "enemy"))
                    {
                        this.Say(true, "Bah, that scoundral? Olin believes eradicating the bunnies is the only solution.");
                    }
                    else if (Insensitive.Speech(e.Speech, "easter"))
                    {
                        this.Say(true, "My favorite time of year. The only time the easter bunny makes an appearence. Quite a rare sight!");
                    }
                    else if (Insensitive.Speech(e.Speech, "bunny") || Insensitive.Speech(e.Speech, "rabbit") || Insensitive.Speech(e.Speech, "bunnies") || Insensitive.Speech(e.Speech, "rabbits"))
                    {
                        this.Say(true, (Utility.RandomBool() ? "I hear they -love- carrots!" : "They always seem to be a problem this time of year. Bring them to me and I will stable them free!"));
                    }
                    else if (Insensitive.Speech(e.Speech, "job") || (Insensitive.Speech(e.Speech, "who are you")))
                    {
                        this.Say(true, "Why, I am a rabbit herder. I make sure they live a healthy life, unlike Lord Olin.");
                    }
                    else if (Insensitive.Speech(e.Speech, "chest") || Insensitive.Speech(e.Speech, "box") || (Insensitive.Speech(e.Speech, "key")))
                    {
                        this.Say(true, "I don't go snooping in your things! I, and only myself has the key.");
                    }
                    else if (Insensitive.Speech(e.Speech, "journal"))
                    {
                        this.Say(true, "I don't go snooping in your things!");
                    }
                    else if (Insensitive.Contains(e.Speech, "document"))
                    {
                        this.Say(true, "Yes... How did you hear of this?");
                    }
                    else
                        base.OnSpeech(e);
                }
			}
		}

        public RabbitHerder(Serial serial) : base(serial)
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