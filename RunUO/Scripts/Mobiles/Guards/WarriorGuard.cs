using System;
using System.Collections;
using Server.Misc;
using Server.Items;
using Server.Mobiles;
using Server.Targeting;

namespace Server.Mobiles
{
    public class SpawningGuard : WarriorGuard
    {
        public override bool ClickTitle { get { return false; } }

        [Constructable]
        public SpawningGuard() : base()
        {
        }

        public SpawningGuard(Serial serial) : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }

	public class WarriorGuard : BaseGuard
	{
        public override bool ClickTitle { get { return false; } }

		private Timer m_AttackTimer, m_IdleTimer;

		private Mobile m_Focus;

		[Constructable]
		public WarriorGuard() : this( null )
		{
		}

        public WarriorGuard(Mobile target) : base( target )
		{
			InitStats( 1000, 1000, 1000 );
			Title = "the guard";

            AI = AIType.AI_Berserk;
            RangeFight = 1;
            ActiveSpeed = 0.1;
            PassiveSpeed = 1;
            RangePerception = 14;
            FightMode = FightMode.Closest;

			SpeechHue = Utility.RandomDyedHue();

			Hue = Utility.RandomSkinHue();

			if ( Female = Utility.RandomBool() )
			{
				Body = 0x191;
				Name = NameList.RandomName( "female" );

				switch( Utility.Random( 2 ) )
				{
					case 0: AddItem( new LeatherSkirt() ); break;
					case 1: AddItem( new LeatherShorts() ); break;
				}

				switch( Utility.Random( 5 ) )
				{
					case 0: AddItem( new FemaleLeatherChest() ); break;
					case 1: AddItem( new FemaleStuddedChest() ); break;
					case 2: AddItem( new LeatherBustierArms() ); break;
					case 3: AddItem( new StuddedBustierArms() ); break;
					case 4: AddItem( new FemalePlateChest() ); break;
				}
                AddItem(new BodySash(Utility.RandomAllColors()));
			}
			else
			{
				Body = 0x190;
				Name = NameList.RandomName( "male" );

				AddItem( new PlateChest() );
				AddItem( new PlateArms() );
				AddItem( new PlateLegs() );

				switch( Utility.Random( 3 ) )
				{
					case 0: AddItem( new Doublet( Utility.RandomNondyedHue() ) ); break;
					case 1: AddItem( new Tunic( Utility.RandomNondyedHue() ) ); break;
					case 2: AddItem( new BodySash( Utility.RandomNondyedHue() ) ); break;
				}
			}
			Utility.AssignRandomHair( this );

			if( Utility.RandomBool() )
				Utility.AssignRandomFacialHair( this, HairHue );

			Halberd weapon = new Halberd();

			weapon.Movable = false;
			weapon.Crafter = this;
			weapon.Quality = WeaponQuality.Exceptional;

			AddItem( weapon );
     
			Container pack = new Backpack();

			pack.Movable = false;

			pack.DropItem( new Gold( 10, 25 ) );

			AddItem( pack );

			Skills[SkillName.Anatomy].Base = 120.0;
			Skills[SkillName.Tactics].Base = 250.0;
			Skills[SkillName.Swords].Base = 120.0;
			Skills[SkillName.MagicResist].Base = 120.0;
			Skills[SkillName.DetectHidden].Base = 100.0;

            this.NextCombatTime = DateTime.Now;// +TimeSpan.FromSeconds(0.5);
			this.Focus = target;
		}

        public override TimeSpan ReacquireDelay { get { return TimeSpan.Zero; } }

		public WarriorGuard( Serial serial ) : base( serial )
		{
		}

		public override bool OnBeforeDeath()
		{
			if ( m_Focus != null && m_Focus.Alive )
				new AvengeTimer( m_Focus ).Start(); // If a guard dies, three more guards will spawn

			return base.OnBeforeDeath();
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public override Mobile Focus
		{
			get
			{
				return m_Focus;
			}
			set
			{
				if ( Deleted )
					return;

				Mobile oldFocus = m_Focus;

				if ( oldFocus != value )
				{
					m_Focus = value;

					if ( value != null )
						this.AggressiveAction( value );

					Combatant = value;
                    FocusMob = value;

                    if (oldFocus != null && !oldFocus.Alive)
                    {
                        switch (Utility.Random(16))
                        {
                            case 0:
                                Say(true, "Ha! I knew that I could do it!"); break;
                            case 2:
                                Say(true, "Thou shouldst not have messed with me!"); break;
                            case 3:
                                Say(true, "Die, pathetic fool!"); break;
                            case 4:
                                Say(true, "Thou deservest to die!"); break;
                            case 5:
                                Say(true, "There, that taketh care of thee."); break;
                            case 6:
                                Say(true, "So perish those who challenge me!"); break;
                            case 7:
                                Say(true, "Thou shouldst not have fought me."); break;
                            case 8:
                                Say(true, "May thy soul rest in peace."); break;
                            case 9:
                                Say(true, "May thy shade wander the wilderness forever!"); break;
                            default:
                            case 10:
                                Say(true, "Have done with thee!"); break;
                        }
                    }

                    if (value != null)
                    {
                        Say(true, "Thou wilt regret thine actions, swine!");
                        DamageMax = value.Hits;
                        DamageMin = value.Hits;
                    }

					if ( m_AttackTimer != null )
					{
						m_AttackTimer.Stop();
						m_AttackTimer = null;
					}

					if ( m_IdleTimer != null )
					{
						m_IdleTimer.Stop();
						m_IdleTimer = null;
					}

					if ( m_Focus != null )
					{
						m_AttackTimer = new AttackTimer( this );
						m_AttackTimer.Start();
						((AttackTimer)m_AttackTimer).DoOnTick();
					}
					else
					{
						m_IdleTimer = new IdleTimer( this );
						m_IdleTimer.Start();
					}
				}
				else if ( m_Focus == null && m_IdleTimer == null )
				{
					m_IdleTimer = new IdleTimer( this );
					m_IdleTimer.Start();
				}
			}
		}

        public override bool OnMoveOver(Mobile m)
        {
            return true;
        }

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version

			writer.Write( m_Focus );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			switch ( version )
			{
				case 0:
				{
					m_Focus = reader.ReadMobile();

					if ( m_Focus != null )
					{
						m_AttackTimer = new AttackTimer( this );
						m_AttackTimer.Start();
					}
					else
					{
						m_IdleTimer = new IdleTimer( this );
						m_IdleTimer.Start();
					}

					break;
				}
			}
		}

		public override void OnAfterDelete()
		{
			if ( m_AttackTimer != null )
			{
				m_AttackTimer.Stop();
				m_AttackTimer = null;
			}

			if ( m_IdleTimer != null )
			{
				m_IdleTimer.Stop();
				m_IdleTimer = null;
			}

			base.OnAfterDelete();
		}

		private class AvengeTimer : Timer
		{
			private Mobile m_Focus;

			public AvengeTimer( Mobile focus ) : base( TimeSpan.FromSeconds( 2.5 ), TimeSpan.FromSeconds( 1.0 ), 3 )
			{
				m_Focus = focus;
			}

			protected override void OnTick()
			{
				BaseGuard.Spawn( m_Focus, m_Focus, 1, true );
			}
		}

		private class AttackTimer : Timer
		{
			private WarriorGuard m_Owner;

			public AttackTimer( WarriorGuard owner ) : base( TimeSpan.FromSeconds( 0.25 ), TimeSpan.FromSeconds( 0.1 ) )
			{
				m_Owner = owner;
			}

			public void DoOnTick()
			{
				OnTick();
			}

			protected override void OnTick()
			{
                if (m_Owner.Deleted || m_Owner == null)
				{
					Stop();
					return;
				}

				m_Owner.Criminal = false;
				m_Owner.Kills = 0;
				m_Owner.Stam = m_Owner.StamMax;

				Mobile target = m_Owner.Focus;

				if ( target != null && (target.Deleted || !target.Alive || !m_Owner.CanBeHarmful( target )) )	
				{
					m_Owner.Focus = null;
					Stop();
					return;
				}
				else if ( m_Owner.Weapon is Fists )
				{
					m_Owner.Kill();
					Stop();
					return;
				}

				if ( target != null && m_Owner.Combatant != target )
					m_Owner.Combatant = target;

				if ( target == null )
				{
					Stop();
				}
				/*else
				{// <instakill>
					TeleportTo( target );
					//target.BoltEffect( 0 );
                    BaseWeapon weapon = m_Owner.Weapon as BaseWeapon;
                    weapon.PlaySwingAnimation(m_Owner);
                    m_Owner.PlaySound(weapon.DefHitSound);


					if ( target is BaseCreature )
						((BaseCreature)target).NoKillAwards = true;

					target.Damage( target.HitsMax, m_Owner );
					target.Kill(); // just in case, maybe Damage is overriden on some shard

					if ( target.Corpse != null && !target.Player )
						target.Corpse.Delete();

					m_Owner.Focus = null;
					Stop();
				}// </instakill>*/
			    if ( !m_Owner.InRange( target, 20 ) )
				{
					m_Owner.Focus = null;
				}
				else if ( !m_Owner.InRange( target, 10 ) || !m_Owner.InLOS( target ) )
				{
                    if (!m_Owner.Move(m_Owner.GetDirectionTo(target) | Direction.Running))
                        TeleportTo(target);

					TeleportTo( target );
				}
				/*else if ( !m_Owner.InRange( target, 1 ) )
				{
					if ( !m_Owner.Move( m_Owner.GetDirectionTo( target ) | Direction.Running ) )
						TeleportTo( target );
				}*/
				/*else if ( !m_Owner.CanSee( target ) )
				{
					if ( !m_Owner.UseSkill( SkillName.DetectHidden ) && Utility.Random( 50 ) == 0 )
						m_Owner.Say( "Reveal!" );
				}*/
			}
     
			private void TeleportTo( Mobile target )
			{
				Point3D from = m_Owner.Location;
                Point3D to = m_Owner.Location;

                Direction dir = m_Owner.GetDirectionTo(target);
                if (dir == Direction.North)
                    to.Y -= 2;
                else if (dir == Direction.South)
                    to.Y += 2;
                else if (dir == Direction.East)
                    to.X += 2;
                else if (dir == Direction.West)
                    to.X -= 2;
                else if (dir == Direction.Up)
                {
                    to.Y -= 2;
                    to.X -= 2;
                }
                else if (dir == Direction.Down)
                {
                    to.Y += 2;
                    to.X += 2;
                }
                else if (dir == Direction.Right)
                {
                    to.Y -= 2;
                    to.X += 2;
                }
                else if (dir == Direction.Left)
                {
                    to.Y += 2;
                    to.X -= 2;
                }
                else
                    to = target.Location;

                if (m_Owner.InRange(target, 1))
                    to.Z = target.Location.Z;
                else
                    to.Z = m_Owner.Map.Tiles.GetLandTile(to.X, to.Y).Z;

				m_Owner.Location = to;

				Effects.SendLocationParticles( EffectItem.Create( from, m_Owner.Map, EffectItem.DefaultDuration ), 0x3728, 10, 10, 2023 );
				Effects.SendLocationParticles( EffectItem.Create(   to, m_Owner.Map, EffectItem.DefaultDuration ), 0x3728, 10, 10, 5023 );

				m_Owner.PlaySound( 0x1FE );
			}
		}

		private class IdleTimer : Timer
		{
			private WarriorGuard m_Owner;
			private int m_Stage;

			public IdleTimer( WarriorGuard owner ) : base( TimeSpan.FromSeconds( 2.0 ), TimeSpan.FromSeconds( 2.5 ) )
			{
				m_Owner = owner;
			}

			protected override void OnTick()
			{
				if ( m_Owner.Deleted || m_Owner is SpawningGuard )
				{
					Stop();
					return;
				}

				if ( (m_Stage++ % 4) == 0 || !m_Owner.Move( m_Owner.Direction ) )
					m_Owner.Direction = (Direction)Utility.Random( 8 );

				if ( m_Stage > 16 )
				{
					Effects.SendLocationParticles( EffectItem.Create( m_Owner.Location, m_Owner.Map, EffectItem.DefaultDuration ), 0x3728, 10, 10, 2023 );
					m_Owner.PlaySound( 0x1FE );

					m_Owner.Delete();
				}
			}
		}
	}
}