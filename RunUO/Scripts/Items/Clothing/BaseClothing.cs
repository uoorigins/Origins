using System;
using System.Collections.Generic;
using Server;
using Server.Engines.Craft;
using Server.Factions;
using Server.Network;
using Server.Spells;
using Server.Spells.First;
using Server.Spells.Second;
using Server.Spells.Sixth;
using Server.Spells.Third;
using Server.Spells.Fifth;
using Server.Spells.Fourth;

namespace Server.Items
{
	public enum ClothingQuality
	{
		Low,
		Regular,
		Exceptional
	}

    public enum ClothEffect
    {
        None,
        NightSight,
        Protection,
        Agility,
        Cunning,
        Strength,
        Invisibility,
        MagicReflection,
        Feeblemind,
        Clumsy,
        Weaken,
        Bless,
        Curse
    }

	public interface IArcaneEquip
	{
		bool IsArcane{ get; }
		int CurArcaneCharges{ get; set; }
		int MaxArcaneCharges{ get; set; }
	}

	public abstract class BaseClothing : Item, IDyable, IScissorable, IFactionItem, ICraftable, IWearableDurability
	{
		#region Factions
		private FactionItem m_FactionState;

		public FactionItem FactionItemState
		{
			get{ return m_FactionState; }
			set
			{
				m_FactionState = value;

				if ( m_FactionState == null )
					Hue = 0;

				LootType = ( m_FactionState == null ? LootType.Regular : LootType.Blessed );
			}
		}
		#endregion

		private int m_MaxHitPoints;
		private int m_HitPoints;
		private Mobile m_Crafter;
		private ClothingQuality m_Quality;
		private bool m_PlayerConstructed;
		protected CraftResource m_Resource;
		private int m_StrReq = -1;
        private ClothEffect m_ClothEffect;
        private int m_Charges;
        private bool m_Identified;
        private List<Mobile> m_IDList = new List<Mobile>();
        private static EffectTimer m_st;

		private AosAttributes m_AosAttributes;
		private AosArmorAttributes m_AosClothingAttributes;
		private AosSkillBonuses m_AosSkillBonuses;
		private AosElementAttributes m_AosResistances;

		[CommandProperty( AccessLevel.GameMaster )]
		public int MaxHitPoints
		{
			get{ return m_MaxHitPoints; }
			set{ m_MaxHitPoints = value; InvalidateProperties(); }
		}

        public string GetEffectString()
        {
            if (Effect == ClothEffect.NightSight)
                return "of night eyes";
            else if (Effect == ClothEffect.Protection)
                return "of protection";
            else if (Effect == ClothEffect.Agility)
                return "of agility";
            else if (Effect == ClothEffect.Cunning)
                return "of cunning";
            else if (Effect == ClothEffect.Strength)
                return "of strength";
            else if (Effect == ClothEffect.Invisibility)
                return "of invisibility";
            else if (Effect == ClothEffect.MagicReflection)
                return "of spell reflection";
            else if (Effect == ClothEffect.Feeblemind)
                return "of feeblemindedness";
            else if (Effect == ClothEffect.Clumsy)
                return "of clumsiness";
            else if (Effect == ClothEffect.Weaken)
                return "of weakness";
            else if (Effect == ClothEffect.Bless)
                return "of blessings";
            else if (Effect == ClothEffect.Curse)
                return "of evil";
            else
                return "";
        }

		[CommandProperty( AccessLevel.GameMaster )]
		public int HitPoints
		{
			get 
			{
				return m_HitPoints;
			}
			set 
			{
				if ( value != m_HitPoints && MaxHitPoints > 0 )
				{
					m_HitPoints = value;

					if ( m_HitPoints < 0 )
						Delete();
					else if ( m_HitPoints > MaxHitPoints )
						m_HitPoints = MaxHitPoints;

					InvalidateProperties();
				}
			}
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public Mobile Crafter
		{
			get{ return m_Crafter; }
			set{ m_Crafter = value; InvalidateProperties(); }
		}

        [CommandProperty(AccessLevel.GameMaster)]
        public int Charges
        {
            get { return m_Charges; }
            set { m_Charges = value; InvalidateProperties(); }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public ClothEffect Effect
        {
            get { return m_ClothEffect; }
            set { m_ClothEffect = value; InvalidateProperties(); }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public bool Identified
        {
            get { return false; }
            set { m_Identified = false; InvalidateProperties(); }
        }

        public List<Mobile> IDList
        {
            get { return m_IDList; }
            set { m_IDList = value; InvalidateProperties(); }
        }

		[CommandProperty( AccessLevel.GameMaster )]
		public int StrRequirement
		{
			get{ return ( m_StrReq == -1 ? (Core.AOS ? AosStrReq : OldStrReq) : m_StrReq ); }
			set{ m_StrReq = value; InvalidateProperties(); }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public ClothingQuality Quality
		{
			get{ return m_Quality; }
			set{ m_Quality = value; InvalidateProperties(); }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public bool PlayerConstructed
		{
			get{ return m_PlayerConstructed; }
			set{ m_PlayerConstructed = value; }
		}

		public virtual CraftResource DefaultResource{ get{ return CraftResource.None; } }

		[CommandProperty( AccessLevel.GameMaster )]
		public CraftResource Resource
		{
			get{ return m_Resource; }
			set{ m_Resource = value; Hue = CraftResources.GetHue( m_Resource ); InvalidateProperties(); }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public AosAttributes Attributes
		{
			get{ return m_AosAttributes; }
			set{}
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public AosArmorAttributes ClothingAttributes
		{
			get{ return m_AosClothingAttributes; }
			set{}
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public AosSkillBonuses SkillBonuses
		{
			get{ return m_AosSkillBonuses; }
			set{}
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public AosElementAttributes Resistances
		{
			get{ return m_AosResistances; }
			set{}
		}

		public virtual int BasePhysicalResistance{ get{ return 0; } }
		public virtual int BaseFireResistance{ get{ return 0; } }
		public virtual int BaseColdResistance{ get{ return 0; } }
		public virtual int BasePoisonResistance{ get{ return 0; } }
		public virtual int BaseEnergyResistance{ get{ return 0; } }

		public override int PhysicalResistance{ get{ return BasePhysicalResistance + m_AosResistances.Physical; } }
		public override int FireResistance{ get{ return BaseFireResistance + m_AosResistances.Fire; } }
		public override int ColdResistance{ get{ return BaseColdResistance + m_AosResistances.Cold; } }
		public override int PoisonResistance{ get{ return BasePoisonResistance + m_AosResistances.Poison; } }
		public override int EnergyResistance{ get{ return BaseEnergyResistance + m_AosResistances.Energy; } }

		public virtual int ArtifactRarity{ get{ return 0; } }

		public virtual int BaseStrBonus{ get{ return 0; } }
		public virtual int BaseDexBonus{ get{ return 0; } }
		public virtual int BaseIntBonus { get { return 0; } }

		public override bool AllowSecureTrade( Mobile from, Mobile to, Mobile newOwner, bool accepted )
		{
			if ( !Ethics.Ethic.CheckTrade( from, to, newOwner, this ) )
				return false;

			return base.AllowSecureTrade( from, to, newOwner, accepted );
		}

		public virtual Race RequiredRace { get { return null; } }

		public override bool CanEquip( Mobile from )
		{
			if ( !Ethics.Ethic.CheckEquip( from, this ) )
				return false;

			if( from.AccessLevel < AccessLevel.GameMaster )
			{
				if( RequiredRace != null && from.Race != RequiredRace )
				{
					if( RequiredRace == Race.Elf )
						from.SendLocalizedMessage( 1072203 ); // Only Elves may use this.
					else
						from.SendMessage( "Only {0} may use this.", RequiredRace.PluralName );

					return false;
				}
				else if( !AllowMaleWearer && !from.Female )
				{
					if( AllowFemaleWearer )
						from.SendLocalizedMessage( 1010388 ); // Only females can wear this.
					else
						from.SendMessage( "You may not wear this." );

					return false;
				}
				else if( !AllowFemaleWearer && from.Female )
				{
					if( AllowMaleWearer )
						from.SendLocalizedMessage( 1063343 ); // Only males can wear this.
					else
						from.SendMessage( "You may not wear this." );

					return false;
				}
				else
				{
					int strBonus = ComputeStatBonus( StatType.Str );
					int strReq = ComputeStatReq( StatType.Str );

					if( from.Str < strReq || (from.Str + strBonus) < 1 )
					{
						from.SendLocalizedMessage( 500213 ); // You are not strong enough to equip that.
						return false;
					}
				}
			}

			return base.CanEquip( from );
		}

		public virtual int AosStrReq{ get{ return 10; } }
		public virtual int OldStrReq{ get{ return 0; } }

		public virtual int InitMinHits{ get{ return 0; } }
		public virtual int InitMaxHits{ get{ return 0; } }

		public virtual bool AllowMaleWearer{ get{ return true; } }
		public virtual bool AllowFemaleWearer{ get{ return true; } }
		public virtual bool CanBeBlessed{ get{ return true; } }

		public int ComputeStatReq( StatType type )
		{
			int v;

			//if ( type == StatType.Str )
				v = StrRequirement;

			return AOS.Scale( v, 100 - GetLowerStatReq() );
		}

		public int ComputeStatBonus( StatType type )
		{
			if ( type == StatType.Str )
				return BaseStrBonus + Attributes.BonusStr;
			else if ( type == StatType.Dex )
				return BaseDexBonus + Attributes.BonusDex;
			else
				return BaseIntBonus + Attributes.BonusInt;
		}

        public bool IsInIDList(Mobile from)
        {
            if (from.AccessLevel > AccessLevel.Player)
                return true;

            foreach (Mobile m in m_IDList)
            {
                if (from == m)
                    return true;
            }

            return false;
        }

        public void AddToIDList(Mobile from)
        {
            if (!IsInIDList(from))
                m_IDList.Add(from);
        }

        public void RemoveFromIDList(Mobile from)
        {
            foreach (Mobile m in m_IDList)
            {
                if (from == m)
                {
                    m_IDList.Remove(m);
                    break;
                }
            }
        }

		public virtual void AddStatBonuses( Mobile parent )
		{
			if ( parent == null )
				return;

			int strBonus = ComputeStatBonus( StatType.Str );
			int dexBonus = ComputeStatBonus( StatType.Dex );
			int intBonus = ComputeStatBonus( StatType.Int );

			if ( strBonus == 0 && dexBonus == 0 && intBonus == 0 )
				return;

			string modName = this.Serial.ToString();

			if ( strBonus != 0 )
				parent.AddStatMod( new StatMod( StatType.Str, modName + "Str", strBonus, TimeSpan.Zero ) );

			if ( dexBonus != 0 )
				parent.AddStatMod( new StatMod( StatType.Dex, modName + "Dex", dexBonus, TimeSpan.Zero ) );

			if ( intBonus != 0 )
				parent.AddStatMod( new StatMod( StatType.Int, modName + "Int", intBonus, TimeSpan.Zero ) );
		}

		public static void ValidateMobile( Mobile m )
		{
			for ( int i = m.Items.Count - 1; i >= 0; --i )
			{
				if ( i >= m.Items.Count )
					continue;

				Item item = m.Items[i];

				if ( item is BaseClothing )
				{
					BaseClothing clothing = (BaseClothing)item;

					if( clothing.RequiredRace != null && m.Race != clothing.RequiredRace )
					{
						if( clothing.RequiredRace == Race.Elf )
							m.SendLocalizedMessage( 1072203 ); // Only Elves may use this.
						else
							m.SendMessage( "Only {0} may use this.", clothing.RequiredRace.PluralName );

						m.AddToBackpack( clothing );
					}
					else if ( !clothing.AllowMaleWearer && !m.Female && m.AccessLevel < AccessLevel.GameMaster )
					{
						if ( clothing.AllowFemaleWearer )
							m.SendLocalizedMessage( 1010388 ); // Only females can wear this.
						else
							m.SendMessage( "You may not wear this." );

						m.AddToBackpack( clothing );
					}
					else if ( !clothing.AllowFemaleWearer && m.Female && m.AccessLevel < AccessLevel.GameMaster )
					{
						if ( clothing.AllowMaleWearer )
							m.SendLocalizedMessage( 1063343 ); // Only males can wear this.
						else
							m.SendMessage( "You may not wear this." );

						m.AddToBackpack( clothing );
					}
				}
			}
		}

		public int GetLowerStatReq()
		{
			if ( !Core.AOS )
				return 0;

			return m_AosClothingAttributes.LowerStatReq;
		}

        /* MAGIC CLOTHING */
        public void ConsumeCharge(object parent, BaseClothing item)
        {
            item.Charges--;
        }

        private void Activate(object parent)
        {
            if (Effect == ClothEffect.None || Charges <= 0)
                return;

            if (parent is Mobile)
            {
                Mobile from = (Mobile)parent;

                string modName = this.Serial.ToString();

                switch (Effect)
                {
                    case ClothEffect.Strength:
                        from.AddStatMod(new StatMod(StatType.Str, modName + "Str", 10, TimeSpan.Zero));
                        from.CheckStatTimers();
                        from.FixedParticles(0x375a, 10, 15, 5017, EffectLayer.Waist);
                        from.PlaySound(0x1ee);
                        break;
                    case ClothEffect.Agility:
                        from.AddStatMod(new StatMod(StatType.Dex, modName + "Dex", 10, TimeSpan.Zero));
                        from.CheckStatTimers();
                        from.FixedParticles(0x375a, 10, 15, 5010, EffectLayer.Waist);
                        from.PlaySound(0x28e);
                        break;
                    case ClothEffect.Cunning:
                        from.AddStatMod(new StatMod(StatType.Int, modName + "Int", 10, TimeSpan.Zero));
                        from.CheckStatTimers();
                        from.FixedParticles(0x375a, 10, 15, 5011, EffectLayer.Head);
                        from.PlaySound(0x1eb);
                        break;
                    case ClothEffect.Bless:
                        from.AddStatMod(new StatMod(StatType.Str, modName + "BlessStr", 10, TimeSpan.Zero));
                        from.AddStatMod(new StatMod(StatType.Dex, modName + "BlessDex", 10, TimeSpan.Zero));
                        from.AddStatMod(new StatMod(StatType.Int, modName + "BlessInt", 10, TimeSpan.Zero));
                        from.CheckStatTimers();
                        from.FixedParticles(0x373a, 10, 15, 5018, EffectLayer.Waist);
                        from.PlaySound(0x1ea);
                        break;
                    case ClothEffect.Weaken:
                        from.AddStatMod(new StatMod(StatType.Str, modName + "LowerStr", -10, TimeSpan.Zero));
                        from.CheckStatTimers();
                        from.FixedParticles(0x3779, 10, 15, 5009, EffectLayer.Waist);
                        from.PlaySound(0x1e6);
                        break;
                    case ClothEffect.Clumsy:
                        from.AddStatMod(new StatMod(StatType.Dex, modName + "LowerDex", -10, TimeSpan.Zero));
                        from.CheckStatTimers();
                        from.FixedParticles(0x3779, 10, 15, 5002, EffectLayer.Head);
                        from.PlaySound(0x1df);
                        break;
                    case ClothEffect.Feeblemind:
                        from.AddStatMod(new StatMod(StatType.Int, modName + "LowerInt", -10, TimeSpan.Zero));
                        from.CheckStatTimers();
                        from.FixedParticles(0x3779, 10, 15, 5004, EffectLayer.Head);
                        from.PlaySound(0x1e4);
                        break;
                    case ClothEffect.Curse:
                        from.AddStatMod(new StatMod(StatType.Str, modName + "CurseStr", -10, TimeSpan.Zero));
                        from.AddStatMod(new StatMod(StatType.Dex, modName + "CurseDex", -10, TimeSpan.Zero));
                        from.AddStatMod(new StatMod(StatType.Int, modName + "CurseInt", -10, TimeSpan.Zero));
                        from.CheckStatTimers();
                        from.FixedParticles(0x374a, 10, 15, 5028, EffectLayer.Waist);
                        from.PlaySound(0x1ea);
                        break;
                    case ClothEffect.NightSight:
                        from.EndAction(typeof(LightCycle));
                        from.BeginAction(typeof(LightCycle));
                        from.LightLevel = (int)LightCycle.DungeonLevel;
                        from.FixedParticles(0x376a, 9, 32, 5007, EffectLayer.Waist);
                        from.PlaySound(0x1e3);
                        break;
                    case ClothEffect.Invisibility:
                        Effects.SendLocationParticles(EffectItem.Create(new Point3D(from.X, from.Y, from.Z + 16), from.Map, EffectItem.DefaultDuration), 0x376A, 10, 15, 5045);
                        from.PlaySound( 0x203 );
                        from.Hidden = true;
                        break;
                    case ClothEffect.MagicReflection:
                        from.MagicDamageAbsorb = 15;
                        from.FixedParticles(0x375a, 10, 15, 5037, EffectLayer.Waist);
                        from.PlaySound(0x1e9);
                        break;
                    case ClothEffect.Protection:
                        from.VirtualArmorMod += 10;
                        from.FixedParticles(0x375a, 9, 20, 5016, EffectLayer.Waist);
                        from.PlaySound(0x1ed);
                        break;
                }

                ConsumeCharge(from, this);

                if (m_st != null)
                    m_st.Stop();

                m_st = new EffectTimer(from, this);
                m_st.Start();
            }
        }

		public override void OnAdded( object parent )
		{
			Mobile mob = parent as Mobile;

			if ( mob != null )
			{
				if ( Core.AOS )
					m_AosSkillBonuses.AddTo( mob );

				AddStatBonuses( mob );
				mob.CheckStatTimers();
			}

			base.OnAdded( parent );

            //Magic Clothing
            Activate(parent);
		}

		public override void OnRemoved( object parent )
		{
			Mobile mob = parent as Mobile;

			if ( mob != null )
			{
				if ( Core.AOS )
					m_AosSkillBonuses.Remove();

				string modName = this.Serial.ToString();

				mob.RemoveStatMod( modName + "Str" );
				mob.RemoveStatMod( modName + "Dex" );
				mob.RemoveStatMod( modName + "Int" );

				mob.CheckStatTimers();
			}

			base.OnRemoved( parent );

            #region Magic Clothing
            if (m_ClothEffect == ClothEffect.None)
                return;

            if (parent is Mobile)
            {
                Mobile from = (Mobile)parent;

                string modName = this.Serial.ToString();

                switch (m_ClothEffect)
                {
                    case ClothEffect.Strength:
                        from.RemoveStatMod(modName + "Str");
                        from.CheckStatTimers();
                        break;
                    case ClothEffect.Agility:
                        from.RemoveStatMod(modName + "Dex");
                        from.CheckStatTimers();
                        break;
                    case ClothEffect.Cunning:
                        from.RemoveStatMod(modName + "Int");
                        from.CheckStatTimers();
                        break;
                    case ClothEffect.Bless:
                        from.RemoveStatMod(modName + "BlessStr");
                        from.RemoveStatMod(modName + "BlessDex");
                        from.RemoveStatMod(modName + "BlessInt");
                        from.CheckStatTimers();
                        break;
                    case ClothEffect.Weaken:
                        from.RemoveStatMod(modName + "LowerStr");
                        from.CheckStatTimers();
                        break;
                    case ClothEffect.Clumsy:
                        from.RemoveStatMod(modName + "LowerDex");
                        from.CheckStatTimers();
                        break;
                    case ClothEffect.Feeblemind:
                        from.RemoveStatMod(modName + "LowerInt");
                        from.CheckStatTimers();
                        break;
                    case ClothEffect.Curse:
                        from.RemoveStatMod(modName + "CurseStr");
                        from.RemoveStatMod(modName + "CurseDex");
                        from.RemoveStatMod(modName + "CurseInt");
                        from.CheckStatTimers();
                        break;
                    case ClothEffect.NightSight:
                        from.EndAction(typeof(LightCycle));
                        from.LightLevel = 0;
                        break;
                    case ClothEffect.Invisibility:
                        from.Hidden = false;
                        break;
                    case ClothEffect.MagicReflection:
                        break;
                    case ClothEffect.Protection:
                        if (from.VirtualArmorMod >= 10)
                            from.VirtualArmorMod -= 10;
                        else
                            from.VirtualArmorMod = 0;
                        break;
                }

                if (m_st != null)
                    m_st.Stop();
            #endregion
            }
		}

        private class EffectTimer : Timer
        {
            private Mobile parent;
            private BaseClothing m_Item;

            public EffectTimer(Mobile m, BaseClothing item) : base(TimeSpan.FromSeconds(60))
            {
                parent = m;
                m_Item = item;
                Priority = TimerPriority.OneMinute;
            }

            protected override void OnTick()
            {
                Mobile from = (Mobile)parent;

                #region Magic Clothing
                if (m_Item.Charges <= 0)
                {
                    from.SendAsciiMessage("This item is out of charges."); // This item is out of charges.

                    switch (m_Item.Effect)
                    {
                        case ClothEffect.Strength:
                            from.RemoveStatMod(m_Item.Serial.ToString() + "Str");
                            from.CheckStatTimers();
                            break;
                        case ClothEffect.Agility:
                            from.RemoveStatMod(m_Item.Serial.ToString() + "Dex");
                            from.CheckStatTimers();
                            break;
                        case ClothEffect.Cunning:
                            from.RemoveStatMod(m_Item.Serial.ToString() + "Int");
                            from.CheckStatTimers();
                            break;
                        case ClothEffect.Bless:
                            from.RemoveStatMod(m_Item.Serial.ToString() + "BlessStr");
                            from.RemoveStatMod(m_Item.Serial.ToString() + "BlessDex");
                            from.RemoveStatMod(m_Item.Serial.ToString() + "BlessInt");
                            from.CheckStatTimers();
                            break;
                        case ClothEffect.Weaken:
                            from.RemoveStatMod(m_Item.Serial.ToString() + "LowerStr");
                            from.CheckStatTimers();
                            break;
                        case ClothEffect.Clumsy:
                            from.RemoveStatMod(m_Item.Serial.ToString() + "LowerDex");
                            from.CheckStatTimers();
                            break;
                        case ClothEffect.Feeblemind:
                            from.RemoveStatMod(m_Item.Serial.ToString() + "LowerInt");
                            from.CheckStatTimers();
                            break;
                        case ClothEffect.Curse:
                            from.RemoveStatMod(m_Item.Serial.ToString() + "CurseStr");
                            from.RemoveStatMod(m_Item.Serial.ToString() + "CurseDex");
                            from.RemoveStatMod(m_Item.Serial.ToString() + "CurseInt");
                            from.CheckStatTimers();
                            break;
                        case ClothEffect.NightSight:
                            from.EndAction(typeof(LightCycle));
                            from.LightLevel = 0;
                            break;
                        case ClothEffect.Invisibility:
                            from.Hidden = false;
                            break;
                        case ClothEffect.MagicReflection:
                            break;
                        case ClothEffect.Protection:
                            if (from.VirtualArmorMod >= 10)
                                from.VirtualArmorMod -= 10;
                            else
                                from.VirtualArmorMod = 0;
                            break;
                    }
                    Stop();
                
                }
                else
                {
                    if (m_Item.Effect == ClothEffect.Invisibility && parent.Hidden == false)
                        Stop();
                    else
                    {
                        m_Item.ConsumeCharge(from, m_Item);
                        m_st = new EffectTimer(from, m_Item);
                        m_st.Start();
                    }
                }
                #endregion
            }
        }
        /* END MAGIC CLOTHING */

		public virtual int OnHit( BaseWeapon weapon, int damageTaken )
		{
			int Absorbed = Utility.RandomMinMax( 1, 4 );

			damageTaken -= Absorbed;

			if ( damageTaken < 0 ) 
				damageTaken = 0;

			if ( 25 > Utility.Random( 100 ) ) // 25% chance to lower durability
			{
				if ( Core.AOS && m_AosClothingAttributes.SelfRepair > Utility.Random( 10 ) )
				{
					HitPoints += 2;
				}
				else
				{
					int wear = Utility.Random( 2 );

					if ( wear > 0 && m_MaxHitPoints > 0 )
					{
						if ( m_HitPoints >= wear )
						{
							HitPoints -= wear;
							wear = 0;
						}
						else
						{
							wear -= HitPoints;
							HitPoints = 0;
						}

						if ( wear > 0 )
						{
							if ( m_MaxHitPoints > wear )
							{
								MaxHitPoints -= wear;

								if ( Parent is Mobile )
									((Mobile)Parent).LocalOverheadMessage( MessageType.Regular, 0x3B2, 1061121 ); // Your equipment is severely damaged.
							}
							else
							{
								Delete();
							}
						}
					}
				}
			}

			return damageTaken;
		}

		public BaseClothing( int itemID, Layer layer ) : this( itemID, layer, 0)
		{
		}

        public BaseClothing(int itemID, Layer layer, int hue) : this(itemID, layer, hue, ClothEffect.None, 0)
        {
        }

        public BaseClothing( int itemID, Layer layer, int hue, ClothEffect effect, int charges ) : base( itemID )
		{
			Layer = layer;
			Hue = hue;

			m_Resource = DefaultResource;
			m_Quality = ClothingQuality.Regular;
            m_ClothEffect = effect;
            m_Charges = charges;
            m_Identified = false;
            m_IDList = new List<Mobile>();

			m_HitPoints = m_MaxHitPoints = Utility.RandomMinMax( InitMinHits, InitMaxHits );

			m_AosAttributes = new AosAttributes( this );
			m_AosClothingAttributes = new AosArmorAttributes( this );
			m_AosSkillBonuses = new AosSkillBonuses( this );
			m_AosResistances = new AosElementAttributes( this );
		}

		public BaseClothing( Serial serial ) : base( serial )
		{
		}

		public override bool AllowEquipedCast( Mobile from )
		{
			if ( base.AllowEquipedCast( from ) )
				return true;

			return ( m_AosAttributes.SpellChanneling != 0 );
		}

		public override bool CheckPropertyConfliction( Mobile m )
		{
			if ( base.CheckPropertyConfliction( m ) )
				return true;

			if ( Layer == Layer.Pants )
				return ( m.FindItemOnLayer( Layer.InnerLegs ) != null );

			if ( Layer == Layer.Shirt )
				return ( m.FindItemOnLayer( Layer.InnerTorso ) != null );

			return false;
		}

		private string GetNameString()
		{
			string name = this.Name;

			if ( name == null )
				name = String.Format( "#{0}", LabelNumber );

			return name;
		}

		public override void AddNameProperty( ObjectPropertyList list )
		{
			int oreType;

			switch ( m_Resource )
			{
				case CraftResource.DullCopper:		oreType = 1053108; break; // dull copper
				case CraftResource.ShadowIron:		oreType = 1053107; break; // shadow iron
				case CraftResource.Copper:			oreType = 1053106; break; // copper
				case CraftResource.Bronze:			oreType = 1053105; break; // bronze
				case CraftResource.Gold:			oreType = 1053104; break; // golden
				case CraftResource.Agapite:			oreType = 1053103; break; // agapite
				case CraftResource.Verite:			oreType = 1053102; break; // verite
				case CraftResource.Valorite:		oreType = 1053101; break; // valorite
				case CraftResource.SpinedLeather:	oreType = 1061118; break; // spined
				case CraftResource.HornedLeather:	oreType = 1061117; break; // horned
				case CraftResource.BarbedLeather:	oreType = 1061116; break; // barbed
				case CraftResource.RedScales:		oreType = 1060814; break; // red
				case CraftResource.YellowScales:	oreType = 1060818; break; // yellow
				case CraftResource.BlackScales:		oreType = 1060820; break; // black
				case CraftResource.GreenScales:		oreType = 1060819; break; // green
				case CraftResource.WhiteScales:		oreType = 1060821; break; // white
				case CraftResource.BlueScales:		oreType = 1060815; break; // blue
				default: oreType = 0; break;
			}

			if ( oreType != 0 )
				list.Add( 1053099, "#{0}\t{1}", oreType, GetNameString() ); // ~1_oretype~ ~2_armortype~
			else if ( Name == null )
				list.Add( LabelNumber );
			else
				list.Add( Name );
		}

		public override void GetProperties( ObjectPropertyList list )
		{
			base.GetProperties( list );

			if ( m_Crafter != null )
				list.Add( 1050043, m_Crafter.Name ); // crafted by ~1_NAME~

			#region Factions
			if ( m_FactionState != null )
				list.Add( 1041350 ); // faction item
			#endregion

			if ( m_Quality == ClothingQuality.Exceptional )
				list.Add( 1060636 ); // exceptional

			if( RequiredRace == Race.Elf )
				list.Add( 1075086 ); // Elves Only

			if ( m_AosSkillBonuses != null )
				m_AosSkillBonuses.GetProperties( list );

			int prop;

			if ( (prop = ArtifactRarity) > 0 )
				list.Add( 1061078, prop.ToString() ); // artifact rarity ~1_val~

			if ( (prop = m_AosAttributes.WeaponDamage) != 0 )
				list.Add( 1060401, prop.ToString() ); // damage increase ~1_val~%

			if ( (prop = m_AosAttributes.DefendChance) != 0 )
				list.Add( 1060408, prop.ToString() ); // defense chance increase ~1_val~%

			if ( (prop = m_AosAttributes.BonusDex) != 0 )
				list.Add( 1060409, prop.ToString() ); // dexterity bonus ~1_val~

			if ( (prop = m_AosAttributes.EnhancePotions) != 0 )
				list.Add( 1060411, prop.ToString() ); // enhance potions ~1_val~%

			if ( (prop = m_AosAttributes.CastRecovery) != 0 )
				list.Add( 1060412, prop.ToString() ); // faster cast recovery ~1_val~

			if ( (prop = m_AosAttributes.CastSpeed) != 0 )
				list.Add( 1060413, prop.ToString() ); // faster casting ~1_val~

			if ( (prop = m_AosAttributes.AttackChance) != 0 )
				list.Add( 1060415, prop.ToString() ); // hit chance increase ~1_val~%

			if ( (prop = m_AosAttributes.BonusHits) != 0 )
				list.Add( 1060431, prop.ToString() ); // hit point increase ~1_val~

			if ( (prop = m_AosAttributes.BonusInt) != 0 )
				list.Add( 1060432, prop.ToString() ); // intelligence bonus ~1_val~

			if ( (prop = m_AosAttributes.LowerManaCost) != 0 )
				list.Add( 1060433, prop.ToString() ); // lower mana cost ~1_val~%

			if ( (prop = m_AosAttributes.LowerRegCost) != 0 )
				list.Add( 1060434, prop.ToString() ); // lower reagent cost ~1_val~%

			if ( (prop = m_AosClothingAttributes.LowerStatReq) != 0 )
				list.Add( 1060435, prop.ToString() ); // lower requirements ~1_val~%

			if ( (prop = m_AosAttributes.Luck) != 0 )
				list.Add( 1060436, prop.ToString() ); // luck ~1_val~

			if ( (prop = m_AosClothingAttributes.MageArmor) != 0 )
				list.Add( 1060437 ); // mage armor

			if ( (prop = m_AosAttributes.BonusMana) != 0 )
				list.Add( 1060439, prop.ToString() ); // mana increase ~1_val~

			if ( (prop = m_AosAttributes.RegenMana) != 0 )
				list.Add( 1060440, prop.ToString() ); // mana regeneration ~1_val~

			if ( (prop = m_AosAttributes.NightSight) != 0 )
				list.Add( 1060441 ); // night sight

			if ( (prop = m_AosAttributes.ReflectPhysical) != 0 )
				list.Add( 1060442, prop.ToString() ); // reflect physical damage ~1_val~%

			if ( (prop = m_AosAttributes.RegenStam) != 0 )
				list.Add( 1060443, prop.ToString() ); // stamina regeneration ~1_val~

			if ( (prop = m_AosAttributes.RegenHits) != 0 )
				list.Add( 1060444, prop.ToString() ); // hit point regeneration ~1_val~

			if ( (prop = m_AosClothingAttributes.SelfRepair) != 0 )
				list.Add( 1060450, prop.ToString() ); // self repair ~1_val~

			if ( (prop = m_AosAttributes.SpellChanneling) != 0 )
				list.Add( 1060482 ); // spell channeling

			if ( (prop = m_AosAttributes.SpellDamage) != 0 )
				list.Add( 1060483, prop.ToString() ); // spell damage increase ~1_val~%

			if ( (prop = m_AosAttributes.BonusStam) != 0 )
				list.Add( 1060484, prop.ToString() ); // stamina increase ~1_val~

			if ( (prop = m_AosAttributes.BonusStr) != 0 )
				list.Add( 1060485, prop.ToString() ); // strength bonus ~1_val~

			if ( (prop = m_AosAttributes.WeaponSpeed) != 0 )
				list.Add( 1060486, prop.ToString() ); // swing speed increase ~1_val~%

			base.AddResistanceProperties( list );

			if ( (prop = m_AosClothingAttributes.DurabilityBonus) > 0 )
				list.Add( 1060410, prop.ToString() ); // durability ~1_val~%

			if ( (prop = ComputeStatReq( StatType.Str )) > 0 )
				list.Add( 1061170, prop.ToString() ); // strength requirement ~1_val~

			if ( m_HitPoints >= 0 && m_MaxHitPoints > 0 )
				list.Add( 1060639, "{0}\t{1}", m_HitPoints, m_MaxHitPoints ); // durability ~1_val~ / ~2_val~
		}

		public override void OnSingleClick( Mobile from )
		{
			List<EquipInfoAttribute> attrs = new List<EquipInfoAttribute>();

			AddEquipInfoAttributes( from, attrs );

			int number;

			if ( Name == null )
			{
				number = LabelNumber;
			}
			else
			{
				this.LabelTo( from, Name );
				number = 1041000;
			}

			if ( attrs.Count == 0 && Crafter == null && Name != null )
				return;

			EquipmentInfo eqInfo = new EquipmentInfo( number, m_Crafter, false, attrs.ToArray() );

			from.Send( new DisplayEquipmentInfo( this, eqInfo ) );
		}

		public virtual void AddEquipInfoAttributes( Mobile from, List<EquipInfoAttribute> attrs )
		{
			if ( DisplayLootType )
			{
				if ( LootType == LootType.Blessed )
					attrs.Add( new EquipInfoAttribute( 1038021 ) ); // blessed
				else if ( LootType == LootType.Cursed )
					attrs.Add( new EquipInfoAttribute( 1049643 ) ); // cursed
			}

			#region Factions
			if ( m_FactionState != null )
				attrs.Add( new EquipInfoAttribute( 1041350 ) ); // faction item
			#endregion

			if ( m_Quality == ClothingQuality.Exceptional )
				attrs.Add( new EquipInfoAttribute( 1018305 - (int)m_Quality ) );
		}

		#region Serialization
		private static void SetSaveFlag( ref SaveFlag flags, SaveFlag toSet, bool setIf )
		{
			if ( setIf )
				flags |= toSet;
		}

		private static bool GetSaveFlag( SaveFlag flags, SaveFlag toGet )
		{
			return ( (flags & toGet) != 0 );
		}

		[Flags]
		private enum SaveFlag
		{
			None				= 0x00000000,
			Resource			= 0x00000001,
			Attributes			= 0x00000002,
			ClothingAttributes	= 0x00000004,
			SkillBonuses		= 0x00000008,
			Resistances			= 0x00000010,
			MaxHitPoints		= 0x00000020,
			HitPoints			= 0x00000040,
			PlayerConstructed	= 0x00000080,
			Crafter				= 0x00000100,
			Quality				= 0x00000200,
			StrReq				= 0x00000400
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 6 ); // version

            writer.Write((List<Mobile>)m_IDList, true);

			SaveFlag flags = SaveFlag.None;

			SetSaveFlag( ref flags, SaveFlag.Resource,			m_Resource != DefaultResource );
			SetSaveFlag( ref flags, SaveFlag.Attributes,		!m_AosAttributes.IsEmpty );
			SetSaveFlag( ref flags, SaveFlag.ClothingAttributes,!m_AosClothingAttributes.IsEmpty );
			SetSaveFlag( ref flags, SaveFlag.SkillBonuses,		!m_AosSkillBonuses.IsEmpty );
			SetSaveFlag( ref flags, SaveFlag.Resistances,		!m_AosResistances.IsEmpty );
			SetSaveFlag( ref flags, SaveFlag.MaxHitPoints,		m_MaxHitPoints != 0 );
			SetSaveFlag( ref flags, SaveFlag.HitPoints,			m_HitPoints != 0 );
			SetSaveFlag( ref flags, SaveFlag.PlayerConstructed,	m_PlayerConstructed != false );
			SetSaveFlag( ref flags, SaveFlag.Crafter,			m_Crafter != null );
			SetSaveFlag( ref flags, SaveFlag.Quality,			m_Quality != ClothingQuality.Regular );
			SetSaveFlag( ref flags, SaveFlag.StrReq,			m_StrReq != -1 );

            writer.Write((bool)m_Identified);
            writer.Write((int)m_Charges);
            writer.WriteEncodedInt((int)m_ClothEffect);

			writer.WriteEncodedInt( (int) flags );

			if ( GetSaveFlag( flags, SaveFlag.Resource ) )
				writer.WriteEncodedInt( (int) m_Resource );

			if ( GetSaveFlag( flags, SaveFlag.Attributes ) )
				m_AosAttributes.Serialize( writer );

			if ( GetSaveFlag( flags, SaveFlag.ClothingAttributes ) )
				m_AosClothingAttributes.Serialize( writer );

			if ( GetSaveFlag( flags, SaveFlag.SkillBonuses ) )
				m_AosSkillBonuses.Serialize( writer );

			if ( GetSaveFlag( flags, SaveFlag.Resistances ) )
				m_AosResistances.Serialize( writer );

			if ( GetSaveFlag( flags, SaveFlag.MaxHitPoints ) )
				writer.WriteEncodedInt( (int) m_MaxHitPoints );

			if ( GetSaveFlag( flags, SaveFlag.HitPoints ) )
				writer.WriteEncodedInt( (int) m_HitPoints );

			if ( GetSaveFlag( flags, SaveFlag.Crafter ) )
				writer.Write( (Mobile) m_Crafter );

			if ( GetSaveFlag( flags, SaveFlag.Quality ) )
				writer.WriteEncodedInt( (int) m_Quality );

			if ( GetSaveFlag( flags, SaveFlag.StrReq ) )
				writer.WriteEncodedInt( (int) m_StrReq );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			switch ( version )
			{
                case 6:
                {
                        m_IDList = reader.ReadStrongMobileList();
                        goto case 5;
                }
				case 5:
				{
                    m_Identified = reader.ReadBool();
                    m_Charges = reader.ReadInt();
                    m_ClothEffect = (ClothEffect)reader.ReadEncodedInt();

					SaveFlag flags = (SaveFlag)reader.ReadEncodedInt();

					if ( GetSaveFlag( flags, SaveFlag.Resource ) )
						m_Resource = (CraftResource)reader.ReadEncodedInt();
					else
						m_Resource = DefaultResource;

					if ( GetSaveFlag( flags, SaveFlag.Attributes ) )
						m_AosAttributes = new AosAttributes( this, reader );
					else
						m_AosAttributes = new AosAttributes( this );

					if ( GetSaveFlag( flags, SaveFlag.ClothingAttributes ) )
						m_AosClothingAttributes = new AosArmorAttributes( this, reader );
					else
						m_AosClothingAttributes = new AosArmorAttributes( this );

					if ( GetSaveFlag( flags, SaveFlag.SkillBonuses ) )
						m_AosSkillBonuses = new AosSkillBonuses( this, reader );
					else
						m_AosSkillBonuses = new AosSkillBonuses( this );

					if ( GetSaveFlag( flags, SaveFlag.Resistances ) )
						m_AosResistances = new AosElementAttributes( this, reader );
					else
						m_AosResistances = new AosElementAttributes( this );

					if ( GetSaveFlag( flags, SaveFlag.MaxHitPoints ) )
						m_MaxHitPoints = reader.ReadEncodedInt();

					if ( GetSaveFlag( flags, SaveFlag.HitPoints ) )
						m_HitPoints = reader.ReadEncodedInt();

					if ( GetSaveFlag( flags, SaveFlag.Crafter ) )
						m_Crafter = reader.ReadMobile();

					if ( GetSaveFlag( flags, SaveFlag.Quality ) )
						m_Quality = (ClothingQuality)reader.ReadEncodedInt();
					else
						m_Quality = ClothingQuality.Regular;

					if ( GetSaveFlag( flags, SaveFlag.StrReq ) )
						m_StrReq = reader.ReadEncodedInt();
					else
						m_StrReq = -1;

					if ( GetSaveFlag( flags, SaveFlag.PlayerConstructed ) )
						m_PlayerConstructed = true;

					break;
				}
				case 4:
				{
					m_Resource = (CraftResource)reader.ReadInt();

					goto case 3;
				}
				case 3:
				{
					m_AosAttributes = new AosAttributes( this, reader );
					m_AosClothingAttributes = new AosArmorAttributes( this, reader );
					m_AosSkillBonuses = new AosSkillBonuses( this, reader );
					m_AosResistances = new AosElementAttributes( this, reader );

					goto case 2;
				}
				case 2:
				{
					m_PlayerConstructed = reader.ReadBool();
					goto case 1;
				}
				case 1:
				{
					m_Crafter = reader.ReadMobile();
					m_Quality = (ClothingQuality)reader.ReadInt();
					break;
				}
				case 0:
				{
					m_Crafter = null;
					m_Quality = ClothingQuality.Regular;
					break;
				}
			}

			if ( version < 2 )
				m_PlayerConstructed = true; // we don't know, so, assume it's crafted

			if ( version < 3 )
			{
				m_AosAttributes = new AosAttributes( this );
				m_AosClothingAttributes = new AosArmorAttributes( this );
				m_AosSkillBonuses = new AosSkillBonuses( this );
				m_AosResistances = new AosElementAttributes( this );
			}

			if ( version < 4 )
				m_Resource = DefaultResource;

			if ( m_MaxHitPoints == 0 && m_HitPoints == 0 )
				m_HitPoints = m_MaxHitPoints = Utility.RandomMinMax( InitMinHits, InitMaxHits );

			Mobile parent = Parent as Mobile;

			if ( parent != null )
			{
				if ( Core.AOS )
					m_AosSkillBonuses.AddTo( parent );

				AddStatBonuses( parent );
				parent.CheckStatTimers();
			}
		}
		#endregion

		public virtual bool Dye( Mobile from, DyeTub sender )
		{
			if ( Deleted )
				return false;
			else if ( RootParent is Mobile && from != RootParent )
				return false;

			Hue = sender.DyedHue;

			return true;
		}

		public bool Scissor( Mobile from, Scissors scissors )
		{
			if ( !IsChildOf( from.Backpack ) )
			{
				from.SendAsciiMessage( "Items you wish to cut must be in your backpack." ); // Items you wish to cut must be in your backpack.
				return false;
			}

            from.SendAsciiMessage("Scissors can not be used on that to produce anything."); // Scissors can not be used on that to produce anything.
            return false;

			/*if ( Ethics.Ethic.IsImbued( this ) )
			{
				from.SendAsciiMessage( "Scissors can not be used on that to produce anything." ); // Scissors can not be used on that to produce anything.
				return false;
			}

			CraftSystem system = DefTailoring.CraftSystem;

			CraftItem item = system.CraftItems.SearchFor( GetType() );

			if ( item != null && item.Ressources.Count == 1 && item.Ressources.GetAt( 0 ).Amount >= 2 )
			{
				try
				{
					Type resourceType = null;

					CraftResourceInfo info = CraftResources.GetInfo( m_Resource );

					if ( info != null && info.ResourceTypes.Length > 0 )
						resourceType = info.ResourceTypes[0];

					if ( resourceType == null )
						resourceType = item.Ressources.GetAt( 0 ).ItemType;

					Item res = (Item)Activator.CreateInstance( resourceType );

					ScissorHelper( from, res, m_PlayerConstructed ? (item.Ressources.GetAt( 0 ).Amount / 2) : 1 );

					res.LootType = LootType.Regular;

					return true;
				}
				catch
				{
				}
			}

			from.SendAsciiMessage( "Scissors can not be used on that to produce anything." ); // Scissors can not be used on that to produce anything.
			return false;*/
		}

		public void DistributeBonuses( int amount )
		{
			for ( int i = 0; i < amount; ++i )
			{
				switch ( Utility.Random( 5 ) )
				{
					case 0: ++m_AosResistances.Physical; break;
					case 1: ++m_AosResistances.Fire; break;
					case 2: ++m_AosResistances.Cold; break;
					case 3: ++m_AosResistances.Poison; break;
					case 4: ++m_AosResistances.Energy; break;
				}
			}

			InvalidateProperties();
		}

		#region ICraftable Members

		public virtual int OnCraft( int quality, bool makersMark, Mobile from, CraftSystem craftSystem, Type typeRes, BaseTool tool, CraftItem craftItem, int resHue )
		{
			Quality = (ClothingQuality)quality;

			if ( makersMark )
				Crafter = from;

			if ( DefaultResource != CraftResource.None )
			{
				Type resourceType = typeRes;

				if ( resourceType == null )
					resourceType = craftItem.Ressources.GetAt( 0 ).ItemType;

				Resource = CraftResources.GetFromType( resourceType );
			}
			else
			{
				Hue = resHue;
			}

			PlayerConstructed = true;

			CraftContext context = craftSystem.GetContext( from );

			if ( context != null && context.DoNotColor )
				Hue = 0;

			return quality;
		}

		#endregion
	}
}