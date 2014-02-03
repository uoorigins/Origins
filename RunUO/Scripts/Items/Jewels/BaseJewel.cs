using System;
using Server.Engines.Craft;
using Server.Spells;
using Server.Spells.Third;
using Server.Targeting;
using System.Text;
using System.Collections;
using Server.Network;
using Server.Spells.Sixth;
using System.Collections.Generic;

namespace Server.Items
{
    public enum JewelEffect
    {
        None,
        NightSight,
        Protection,
        Agility,
        Cunning,
        Strength,
        Invisibility,
        Teleportation,
        MagicReflection,
        Feeblemind,
        Clumsy,
        Weaken,
        Bless,
        Curse
    }

	public enum GemType
	{
		None,
		StarSapphire,
		Emerald,
		Sapphire,
		Ruby,
		Citrine,
		Amethyst,
		Tourmaline,
		Amber,
		Diamond
	}

	public abstract class BaseJewel : Item, ICraftable
	{
        public abstract string AsciiName { get; }
        public virtual bool AsciiPlural { get { return false; } }

        public virtual TimeSpan GetUseDelay { get { return TimeSpan.FromSeconds(4.0); } }

		private AosAttributes m_AosAttributes;
		private AosElementAttributes m_AosResistances;
		private AosSkillBonuses m_AosSkillBonuses;
		private CraftResource m_Resource;
		private GemType m_GemType;
        private JewelEffect m_JewelEffect;
        private int m_Charges;
        private bool m_Identified;
        private List<Mobile> m_IDList = new List<Mobile>();
        private static EffectTimer m_st;

        [CommandProperty(AccessLevel.GameMaster)]
        public JewelEffect Effect
        {
            get { return m_JewelEffect; }
            set { m_JewelEffect = value; InvalidateProperties(); }
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

        [CommandProperty(AccessLevel.GameMaster)]
        public int Charges
        {
            get { return m_Charges; }
            set { m_Charges = value; InvalidateProperties(); }
        }

		[CommandProperty( AccessLevel.GameMaster )]
		public AosAttributes Attributes
		{
			get{ return m_AosAttributes; }
			set{}
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public AosElementAttributes Resistances
		{
			get{ return m_AosResistances; }
			set{}
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public AosSkillBonuses SkillBonuses
		{
			get{ return m_AosSkillBonuses; }
			set{}
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public CraftResource Resource
		{
			get{ return m_Resource; }
			set{ m_Resource = value; Hue = CraftResources.GetHue( m_Resource ); }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public GemType GemType
		{
			get{ return m_GemType; }
			set{ m_GemType = value; InvalidateProperties(); }
		}

		public override int PhysicalResistance{ get{ return m_AosResistances.Physical; } }
		public override int FireResistance{ get{ return m_AosResistances.Fire; } }
		public override int ColdResistance{ get{ return m_AosResistances.Cold; } }
		public override int PoisonResistance{ get{ return m_AosResistances.Poison; } }
		public override int EnergyResistance{ get{ return m_AosResistances.Energy; } }
		public virtual int BaseGemTypeNumber{ get{ return 0; } }

        public string GetEffectString()
        {
            if ( Effect == JewelEffect.NightSight )
                return "night eyes";
            else if ( Effect == JewelEffect.Protection )
                return "protection";
            else if ( Effect == JewelEffect.Agility )
                return "agility";
            else if ( Effect == JewelEffect.Cunning )
                return "cunning";
            else if ( Effect == JewelEffect.Strength )
                return "strength";
            else if ( Effect == JewelEffect.Invisibility )
                return "invisibility";
            else if ( Effect == JewelEffect.MagicReflection )
                return "spell reflection";
            else if ( Effect == JewelEffect.Feeblemind )
                return "feeblemindedness";
            else if ( Effect == JewelEffect.Clumsy )
                return "clumsiness";
            else if ( Effect == JewelEffect.Weaken )
                return "weakness";
            else if ( Effect == JewelEffect.Bless )
                return "blessings";
            else if ( Effect == JewelEffect.Curse )
                return "evil";
            else if ( Effect == JewelEffect.Teleportation )
                return "teleportation";
            else
                return "";
        }

		public override int LabelNumber
		{
			get
			{
				if ( m_GemType == GemType.None )
					return base.LabelNumber;

				return BaseGemTypeNumber + (int)m_GemType - 1;
			}
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

		public virtual int ArtifactRarity{ get{ return 0; } }

		public BaseJewel( int itemID, Layer layer ) : this( itemID, layer, JewelEffect.None, 1, 5 )
		{
		}

        public BaseJewel(int itemID, Layer layer, JewelEffect effect, int minCharges, int maxCharges) : base(itemID)
		{
            m_AosAttributes = new AosAttributes( this );
			m_AosResistances = new AosElementAttributes( this );
			m_AosSkillBonuses = new AosSkillBonuses( this );
			m_Resource = CraftResource.Iron;
			m_GemType = GemType.None;
            m_IDList = new List<Mobile>();

			Layer = layer;
			Weight = 1.0;
			Effect = effect;
			Charges = Utility.RandomMinMax( minCharges, maxCharges );
            m_Identified = false;
		}

        private void Activate( object parent )
        {
            if ( Effect == JewelEffect.None || Charges <= 0 )
                return;

            if ( parent is Mobile )
            {
                Mobile from = (Mobile)parent;

                string modName = this.Serial.ToString();

                switch ( Effect )
                {
                    case JewelEffect.Strength:
                        from.AddStatMod( new StatMod( StatType.Str, modName + "Str", 10, TimeSpan.Zero ) );
                        from.CheckStatTimers();
                        from.FixedParticles( 0x375a, 10, 15, 5017, EffectLayer.Waist );
                        from.PlaySound( 0x1ee );
                        break;
                    case JewelEffect.Agility:
                        from.AddStatMod( new StatMod( StatType.Dex, modName + "Dex", 10, TimeSpan.Zero ) );
                        from.CheckStatTimers();
                        from.FixedParticles( 0x375a, 10, 15, 5010, EffectLayer.Waist );
                        from.PlaySound( 0x28e );
                        break;
                    case JewelEffect.Cunning:
                        from.AddStatMod( new StatMod( StatType.Int, modName + "Int", 10, TimeSpan.Zero ) );
                        from.CheckStatTimers();
                        from.FixedParticles( 0x375a, 10, 15, 5011, EffectLayer.Head );
                        from.PlaySound( 0x1eb );
                        break;
                    case JewelEffect.Bless:
                        from.AddStatMod( new StatMod( StatType.Str, modName + "BlessStr", 10, TimeSpan.Zero ) );
                        from.AddStatMod( new StatMod( StatType.Dex, modName + "BlessDex", 10, TimeSpan.Zero ) );
                        from.AddStatMod( new StatMod( StatType.Int, modName + "BlessInt", 10, TimeSpan.Zero ) );
                        from.CheckStatTimers();
                        from.FixedParticles( 0x373a, 10, 15, 5018, EffectLayer.Waist );
                        from.PlaySound( 0x1ea );
                        break;
                    case JewelEffect.Weaken:
                        from.AddStatMod( new StatMod( StatType.Str, modName + "LowerStr", -10, TimeSpan.Zero ) );
                        from.CheckStatTimers();
                        from.FixedParticles( 0x3779, 10, 15, 5009, EffectLayer.Waist );
                        from.PlaySound( 0x1e6 );
                        break;
                    case JewelEffect.Clumsy:
                        from.AddStatMod( new StatMod( StatType.Dex, modName + "LowerDex", -10, TimeSpan.Zero ) );
                        from.CheckStatTimers();
                        from.FixedParticles( 0x3779, 10, 15, 5002, EffectLayer.Head );
                        from.PlaySound( 0x1df );
                        break;
                    case JewelEffect.Feeblemind:
                        from.AddStatMod( new StatMod( StatType.Int, modName + "LowerInt", -10, TimeSpan.Zero ) );
                        from.CheckStatTimers();
                        from.FixedParticles( 0x3779, 10, 15, 5004, EffectLayer.Head );
                        from.PlaySound( 0x1e4 );
                        break;
                    case JewelEffect.Curse:
                        from.AddStatMod( new StatMod( StatType.Str, modName + "CurseStr", -10, TimeSpan.Zero ) );
                        from.AddStatMod( new StatMod( StatType.Dex, modName + "CurseDex", -10, TimeSpan.Zero ) );
                        from.AddStatMod( new StatMod( StatType.Int, modName + "CurseInt", -10, TimeSpan.Zero ) );
                        from.CheckStatTimers();
                        from.FixedParticles( 0x374a, 10, 15, 5028, EffectLayer.Waist );
                        from.PlaySound( 0x1ea );
                        break;
                    case JewelEffect.NightSight:
                        from.EndAction( typeof( LightCycle ) );
                        from.BeginAction( typeof( LightCycle ) );
                        from.LightLevel = (int)LightCycle.DungeonLevel;
                        from.FixedParticles( 0x376a, 9, 32, 5007, EffectLayer.Waist );
                        from.PlaySound( 0x1e3 );
                        break;
                    case JewelEffect.Invisibility:
                        Effects.SendLocationParticles( EffectItem.Create( new Point3D( from.X, from.Y, from.Z + 16 ), from.Map, EffectItem.DefaultDuration ), 0x376A, 10, 15, 5045 );
                        from.PlaySound( 0x203 );
                        from.Hidden = true;
                        break;
                    case JewelEffect.MagicReflection:
                        from.MagicDamageAbsorb = 15;
                        from.FixedParticles( 0x375a, 10, 15, 5037, EffectLayer.Waist );
                        from.PlaySound( 0x1e9 );
                        break;
                    case JewelEffect.Protection:
                        from.VirtualArmorMod += 10;
                        from.FixedParticles( 0x375a, 9, 20, 5016, EffectLayer.Waist );
                        from.PlaySound( 0x1ed );
                        break;
                }

                ConsumeCharge( from, this );

                if ( m_st != null )
                    m_st.Stop();

                m_st = new EffectTimer( from, this );
                m_st.Start();
            }
        }

        public void ConsumeCharge( object parent, BaseJewel item )
        {
            item.Charges--;
        }

        private class EffectTimer : Timer
        {
            private Mobile parent;
            private BaseJewel m_Item;

            public EffectTimer( Mobile m, BaseJewel item )
                : base( TimeSpan.FromSeconds( 60 ) )
            {
                parent = m;
                m_Item = item;
                Priority = TimerPriority.OneMinute;
            }

            protected override void OnTick()
            {
                Mobile from = (Mobile)parent;

                #region Magic Clothing
                if ( m_Item.Charges <= 0 )
                {
                    from.SendAsciiMessage( "This item is out of charges." ); // This item is out of charges.

                    switch ( m_Item.Effect )
                    {
                        case JewelEffect.Strength:
                            from.RemoveStatMod( m_Item.Serial.ToString() + "Str" );
                            from.CheckStatTimers();
                            break;
                        case JewelEffect.Agility:
                            from.RemoveStatMod( m_Item.Serial.ToString() + "Dex" );
                            from.CheckStatTimers();
                            break;
                        case JewelEffect.Cunning:
                            from.RemoveStatMod( m_Item.Serial.ToString() + "Int" );
                            from.CheckStatTimers();
                            break;
                        case JewelEffect.Bless:
                            from.RemoveStatMod( m_Item.Serial.ToString() + "BlessStr" );
                            from.RemoveStatMod( m_Item.Serial.ToString() + "BlessDex" );
                            from.RemoveStatMod( m_Item.Serial.ToString() + "BlessInt" );
                            from.CheckStatTimers();
                            break;
                        case JewelEffect.Weaken:
                            from.RemoveStatMod( m_Item.Serial.ToString() + "LowerStr" );
                            from.CheckStatTimers();
                            break;
                        case JewelEffect.Clumsy:
                            from.RemoveStatMod( m_Item.Serial.ToString() + "LowerDex" );
                            from.CheckStatTimers();
                            break;
                        case JewelEffect.Feeblemind:
                            from.RemoveStatMod( m_Item.Serial.ToString() + "LowerInt" );
                            from.CheckStatTimers();
                            break;
                        case JewelEffect.Curse:
                            from.RemoveStatMod( m_Item.Serial.ToString() + "CurseStr" );
                            from.RemoveStatMod( m_Item.Serial.ToString() + "CurseDex" );
                            from.RemoveStatMod( m_Item.Serial.ToString() + "CurseInt" );
                            from.CheckStatTimers();
                            break;
                        case JewelEffect.NightSight:
                            from.EndAction( typeof( LightCycle ) );
                            from.LightLevel = 0;
                            break;
                        case JewelEffect.Invisibility:
                            from.Hidden = false;
                            break;
                        case JewelEffect.MagicReflection:
                            break;
                        case JewelEffect.Protection:
                            if ( from.VirtualArmorMod >= 10 )
                                from.VirtualArmorMod -= 10;
                            else
                                from.VirtualArmorMod = 0;
                            break;
                    }
                    Stop();

                }
                else
                {
                    if ( m_Item.Effect == JewelEffect.Invisibility && parent.Hidden == false )
                        Stop();
                    else
                    {
                        m_Item.ConsumeCharge( from, m_Item );
                        m_st = new EffectTimer( from, m_Item );
                        m_st.Start();
                    }
                }
                #endregion
            }
        }

        public string GetBeginning( string text )
        {
            if ( text.Length < 1 )
                return "";

            char start = text[0];

            // a, e, i, o, u
            return ( start == 'a' || start == 'e' || start == 'i' || start == 'o' || start == 'u' ? "an" : "a" );
        }

        public override void OnSingleClick( Mobile from )
        {
            //Effect text
            string effectText = "";

            if ( Effect != JewelEffect.None )
            {
                effectText = String.Format( "of {0} ({1} charges)", GetEffectString(), Charges );
            }

            string displayText;

            if ( !IsInIDList( from ) && Effect != JewelEffect.None && from.AccessLevel == AccessLevel.Player )
            {
                displayText = String.Format( "magic {0}",  ( Name != null ? Name : AsciiName ) );
            }
            else
            {
                displayText = String.Format( "{0} {1}", ( Name != null ? Name : AsciiName ), effectText );
            }

            //Trim the ends
            displayText = displayText.Trim();

            //Remove double whitespaces
            while ( displayText.Contains( "  " ) )
            {
                displayText = displayText.Replace( "  ", " " );
            }

            if ( !AsciiPlural && Name == null )
            {
                displayText = String.Format( "{0} {1}", GetBeginning( displayText ), displayText );
            }

            from.Send( new AsciiMessage( Serial, ItemID, MessageType.Label, 0, 3, "", displayText ) );
        }

        public override void OnDoubleClick(Mobile from)
        {
            if (Effect != JewelEffect.None)
            {
                if (!from.CanBeginAction(typeof(BaseJewel)))
                    return;

                if (Parent == from)
                {
                    if (Charges > 0)
                    {
                        if (Effect == JewelEffect.Teleportation)
                            Cast(new TeleportSpell(from, this));
                    }
                    else
                        from.SendAsciiMessage("This item is out of charges."); // This item is out of charges.
                }
                else
                {
                    from.SendAsciiMessage("You must equip this item to use it."); // You must equip this item to use it.
                }
            }
            else
                base.OnDoubleClick(from);

        }

        public void ConsumeCharge(Mobile from)
        {
            --Charges;

            if (Charges == 0)
                from.SendAsciiMessage("This item is out of charges."); // This item is out of charges.

            ApplyDelayTo(from);
        }

        public virtual void ApplyDelayTo(Mobile from)
        {
            from.BeginAction(typeof(BaseJewel));
            Timer.DelayCall(GetUseDelay, new TimerStateCallback(ReleaseJewelLock_Callback), from);
        }

        public virtual void ReleaseJewelLock_Callback(object state)
        {
            ((Mobile)state).EndAction(typeof(BaseJewel));
        }

        public void Cast(Spell spell)
        {
            bool m = Movable;

            Movable = false;
            spell.Cast();
            Movable = m;
        }

		public override void OnAdded( object parent )
		{
			if ( Core.AOS && parent is Mobile )
			{
				Mobile from = (Mobile)parent;

				m_AosSkillBonuses.AddTo( from );

				int strBonus = m_AosAttributes.BonusStr;
				int dexBonus = m_AosAttributes.BonusDex;
				int intBonus = m_AosAttributes.BonusInt;

				if ( strBonus != 0 || dexBonus != 0 || intBonus != 0 )
				{
					string modName = this.Serial.ToString();

					if ( strBonus != 0 )
						from.AddStatMod( new StatMod( StatType.Str, modName + "Str", strBonus, TimeSpan.Zero ) );

					if ( dexBonus != 0 )
						from.AddStatMod( new StatMod( StatType.Dex, modName + "Dex", dexBonus, TimeSpan.Zero ) );

					if ( intBonus != 0 )
						from.AddStatMod( new StatMod( StatType.Int, modName + "Int", intBonus, TimeSpan.Zero ) );
				}

				from.CheckStatTimers();
			}

            //Magic Clothing
            Activate( parent );
		}

		public override void OnRemoved( object parent )
		{
			if ( Core.AOS && parent is Mobile )
			{
				Mobile from = (Mobile)parent;

				m_AosSkillBonuses.Remove();

				string modName = this.Serial.ToString();

				from.RemoveStatMod( modName + "Str" );
				from.RemoveStatMod( modName + "Dex" );
				from.RemoveStatMod( modName + "Int" );

				from.CheckStatTimers();
			}

            #region Magic Clothing
            if (m_JewelEffect == JewelEffect.None)
                return;

            if ( parent is Mobile )
            {
                Mobile from = (Mobile)parent;

                string modName = this.Serial.ToString();

                switch ( m_JewelEffect )
                {
                    case JewelEffect.Strength:
                        from.RemoveStatMod( modName + "Str" );
                        from.CheckStatTimers();
                        break;
                    case JewelEffect.Agility:
                        from.RemoveStatMod( modName + "Dex" );
                        from.CheckStatTimers();
                        break;
                    case JewelEffect.Cunning:
                        from.RemoveStatMod( modName + "Int" );
                        from.CheckStatTimers();
                        break;
                    case JewelEffect.Bless:
                        from.RemoveStatMod( modName + "BlessStr" );
                        from.RemoveStatMod( modName + "BlessDex" );
                        from.RemoveStatMod( modName + "BlessInt" );
                        from.CheckStatTimers();
                        break;
                    case JewelEffect.Weaken:
                        from.RemoveStatMod( modName + "LowerStr" );
                        from.CheckStatTimers();
                        break;
                    case JewelEffect.Clumsy:
                        from.RemoveStatMod( modName + "LowerDex" );
                        from.CheckStatTimers();
                        break;
                    case JewelEffect.Feeblemind:
                        from.RemoveStatMod( modName + "LowerInt" );
                        from.CheckStatTimers();
                        break;
                    case JewelEffect.Curse:
                        from.RemoveStatMod( modName + "CurseStr" );
                        from.RemoveStatMod( modName + "CurseDex" );
                        from.RemoveStatMod( modName + "CurseInt" );
                        from.CheckStatTimers();
                        break;
                    case JewelEffect.NightSight:
                        from.EndAction( typeof( LightCycle ) );
                        from.LightLevel = 0;
                        break;
                    case JewelEffect.Invisibility:
                        from.Hidden = false;
                        break;
                    case JewelEffect.MagicReflection:
                        break;
                    case JewelEffect.Protection:
                        if ( from.VirtualArmorMod >= 10 )
                            from.VirtualArmorMod -= 10;
                        else
                            from.VirtualArmorMod = 0;
                        break;
                }

                if ( m_st != null )
                    m_st.Stop();
            }
            #endregion
		}

		public BaseJewel( Serial serial ) : base( serial )
		{
		}

		public override void GetProperties( ObjectPropertyList list )
		{
			base.GetProperties( list );

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

			if ( (prop = m_AosAttributes.Luck) != 0 )
				list.Add( 1060436, prop.ToString() ); // luck ~1_val~

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
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 3 ); // version

            writer.Write((List<Mobile>)m_IDList, true);

			writer.WriteEncodedInt( (int) m_Resource );
			writer.WriteEncodedInt( (int) m_GemType );
            writer.Write((bool)m_Identified);
            writer.Write((int)m_Charges);
            writer.WriteEncodedInt((int)m_JewelEffect);



			m_AosAttributes.Serialize( writer );
			m_AosResistances.Serialize( writer );
			m_AosSkillBonuses.Serialize( writer );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			switch ( version )
			{
                case 3:
                    {
                        m_IDList = reader.ReadStrongMobileList();
                        goto case 2;
                    }
				case 2:
				{
					m_Resource = (CraftResource)reader.ReadEncodedInt();
					m_GemType = (GemType)reader.ReadEncodedInt();
                    m_Identified = reader.ReadBool();
                    m_Charges = reader.ReadInt();
                    m_JewelEffect = (JewelEffect)reader.ReadEncodedInt();


					goto case 1;
				}
				case 1:
				{
					m_AosAttributes = new AosAttributes( this, reader );
					m_AosResistances = new AosElementAttributes( this, reader );
					m_AosSkillBonuses = new AosSkillBonuses( this, reader );

					if ( Core.AOS && Parent is Mobile )
						m_AosSkillBonuses.AddTo( (Mobile)Parent );

					int strBonus = m_AosAttributes.BonusStr;
					int dexBonus = m_AosAttributes.BonusDex;
					int intBonus = m_AosAttributes.BonusInt;

					if ( Parent is Mobile && (strBonus != 0 || dexBonus != 0 || intBonus != 0) )
					{
						Mobile m = (Mobile)Parent;

						string modName = Serial.ToString();

						if ( strBonus != 0 )
							m.AddStatMod( new StatMod( StatType.Str, modName + "Str", strBonus, TimeSpan.Zero ) );

						if ( dexBonus != 0 )
							m.AddStatMod( new StatMod( StatType.Dex, modName + "Dex", dexBonus, TimeSpan.Zero ) );

						if ( intBonus != 0 )
							m.AddStatMod( new StatMod( StatType.Int, modName + "Int", intBonus, TimeSpan.Zero ) );
					}

					if ( Parent is Mobile )
						((Mobile)Parent).CheckStatTimers();

					break;
				}
				case 0:
				{
					m_AosAttributes = new AosAttributes( this );
					m_AosResistances = new AosElementAttributes( this );
					m_AosSkillBonuses = new AosSkillBonuses( this );

					break;
				}
			}

			if ( version < 2 )
			{
				m_Resource = CraftResource.Iron;
				m_GemType = GemType.None;
			}
		}
		#region ICraftable Members

		public int OnCraft( int quality, bool makersMark, Mobile from, CraftSystem craftSystem, Type typeRes, BaseTool tool, CraftItem craftItem, int resHue )
		{
			Type resourceType = typeRes;

			if ( resourceType == null )
				resourceType = craftItem.Ressources.GetAt( 0 ).ItemType;

			Resource = CraftResources.GetFromType( resourceType );

			CraftContext context = craftSystem.GetContext( from );

			if ( context != null && context.DoNotColor )
				Hue = 0;

			if ( 1 < craftItem.Ressources.Count )
			{
				resourceType = craftItem.Ressources.GetAt( 1 ).ItemType;

				if ( resourceType == typeof( StarSapphire ) )
					GemType = GemType.StarSapphire;
				else if ( resourceType == typeof( Emerald ) )
					GemType = GemType.Emerald;
				else if ( resourceType == typeof( Sapphire ) )
					GemType = GemType.Sapphire;
				else if ( resourceType == typeof( Ruby ) )
					GemType = GemType.Ruby;
				else if ( resourceType == typeof( Citrine ) )
					GemType = GemType.Citrine;
				else if ( resourceType == typeof( Amethyst ) )
					GemType = GemType.Amethyst;
				else if ( resourceType == typeof( Tourmaline ) )
					GemType = GemType.Tourmaline;
				else if ( resourceType == typeof( Amber ) )
					GemType = GemType.Amber;
				else if ( resourceType == typeof( Diamond ) )
					GemType = GemType.Diamond;
			}

			return 1;
		}

		#endregion
	}
}