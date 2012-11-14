using System;
using Server;
using Server.Items;
using Server.Targets;
using Server.Targeting;
using System.Collections.Generic;
using Server.Spells;
using Server.Spells.Fourth;
using Server.Spells.Third;
using Server.Spells.Second;
using Server.Spells.First;
using Server.Spells.Fifth;
using Server.Spells.Eighth;
using Server.Network;

namespace Server.Items
{
    public enum StationaryEffect
    {
        None,
        Restoration,
        Heal,
        Cure,
        Agility,
        Cunning,
        Strength,
        Protection,
        Bless,
        SummomCreature,
        GreaterHeal,
        Clumsy,
        Feeblemind,
        Weaken,
        Harm,
        ManaDrain,
        Paralyze,
        SummonAirElemental,
        SummonEarthElemental,
        SummonFireElemental,
        SummonWaterElemental,
        SummonDaemon,
        Curse
    }

    public abstract class BaseStationary : Item
    {
        public virtual TimeSpan GetUseDelay { get { return TimeSpan.FromSeconds(4.0); } }
        private StationaryEffect m_Effect;
        private int m_Charges;
        private List<Mobile> m_IDList = new List<Mobile>();
        public abstract string StringName { get; }

        [CommandProperty(AccessLevel.GameMaster)]
        public StationaryEffect Effect
        {
            get { return m_Effect; }
            set { m_Effect = value; InvalidateProperties(); }
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

        public BaseStationary(int itemID) : this(itemID, StationaryEffect.None)
        {
        }

        public BaseStationary(int itemID, StationaryEffect effect) : this(itemID, StationaryEffect.None, 0, 0)
        {
        }

        public BaseStationary(int itemID, StationaryEffect effect, int minCharges, int maxCharges) : base(itemID)
        {
            Effect = effect;
            Charges = Utility.RandomMinMax(minCharges, maxCharges);
            m_IDList = new List<Mobile>();
        }

        public BaseStationary(Serial serial) : base(serial)
        {
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
            from.BeginAction(typeof(BaseStationary));
            Timer.DelayCall(GetUseDelay, new TimerStateCallback(ReleaseStationaryLock_Callback), from);
        }

        public virtual void ReleaseStationaryLock_Callback(object state)
        {
            ((Mobile)state).EndAction(typeof(BaseStationary));
        }

        public void Cast(Spell spell)
        {
            bool m = Movable;

            Movable = false;
            spell.Cast();
            Movable = m;
        }

        public string GetMagicType()
        {
            switch ((int)Effect)
            {
                default:
                case 1: return ""; break;
                case 2: return "restoration"; break;
                case 3: return "healing"; break;
                case 4: return "curing"; break;
                case 5: return "agility"; break;
                case 6: return "cunning"; break;
                case 7: return "strength"; break;
                case 8: return "blessedness"; break;
                case 9: return "creature summoning"; break;
                case 10: return "great healing"; break;
                case 11: return "clumsiness"; break;
                case 12: return "feeblemindedness"; break;
                case 13: return "weakness"; break;
                case 14: return "harming"; break;
                case 15: return "mana draining"; break;
                case 16: return "paralyzation"; break;
                case 17: return "air elemental summoning"; break;
                case 18: return "earth elemental summoning"; break;
                case 19: return "fire elemental summoning"; break;
                case 20: return "water elemental summoning"; break;
                case 21: return "daemon summoning"; break;
                case 22: return "curses"; break;
            }
        }

        public override void OnSingleClick(Mobile from)
        {
            if (this.Name != null)
            {
                from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", this.Name));
            }
            else
            {
                if (Effect != StationaryEffect.None)
                {
                    if (IsInIDList(from))
                        from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", String.Format("a {0} of {1} ({2} charges)", StringName, GetMagicType(), Charges)));
                    else
                        from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", String.Format("a magic {0}", StringName)));
                }
                else
                    from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", String.Format("a {0}", StringName)));
            }
        }

        public override void OnDoubleClick(Mobile from)
        {
            if (Effect != StationaryEffect.None)
            {
                if (!from.CanBeginAction(typeof(BaseStationary)))
                    return;

                if (from.Backpack != null && Parent == from.Backpack)
                {
                    if (Charges > 0)
                        OnUse(from);
                    else
                        from.SendAsciiMessage("This item is out of charges."); // This item is out of charges.
                }
                else
                {
                    from.SendAsciiMessage("That must be in your pack for you to use it.");
                }
            }
            else
                base.OnDoubleClick(from);
        }

        public virtual void OnUse(Mobile from)
        {
            if (Effect == StationaryEffect.Clumsy)
                Cast(new ClumsySpell(from, this));
            else if (Effect == StationaryEffect.Paralyze)
                Cast(new ParalyzeSpell(from, this));
            else if (Effect == StationaryEffect.Feeblemind)
                Cast(new FeeblemindSpell(from, this));
            else if (Effect == StationaryEffect.Weaken)
                Cast(new WeakenSpell(from, this));
            else if (Effect == StationaryEffect.Harm)
                Cast(new HarmSpell(from, this));
            else if (Effect == StationaryEffect.Curse)
                Cast(new CurseSpell(from, this));
            else if (Effect == StationaryEffect.ManaDrain)
                Cast(new ManaDrainSpell(from, this));
            else if (Effect == StationaryEffect.Restoration)
            {
                from.Hits = (int)((double)from.Hits * 1.1);
                from.Stam = (int)((double)from.Stam * 1.1);
                from.Mana = (int)((double)from.Mana * 1.1);
            }
            else if (Effect == StationaryEffect.Heal)
                Cast(new HealSpell(from, this));
            else if (Effect == StationaryEffect.Cure)
                Cast(new CureSpell(from, this));
            else if (Effect == StationaryEffect.Agility)
                Cast(new AgilitySpell(from, this));
            else if (Effect == StationaryEffect.Cunning)
                Cast(new CunningSpell(from, this));
            else if (Effect == StationaryEffect.Strength)
                Cast(new StrengthSpell(from, this));
            else if (Effect == StationaryEffect.Protection)
                Cast(new ProtectionSpell(from, this));
            else if (Effect == StationaryEffect.Bless)
                Cast(new BlessSpell(from, this));
            else if (Effect == StationaryEffect.SummomCreature)
                Cast(new SummonCreatureSpell(from, this));
            else if (Effect == StationaryEffect.GreaterHeal)
                Cast(new GreaterHealSpell(from, this));
            else if (Effect == StationaryEffect.SummonAirElemental)
                Cast(new AirElementalSpell(from, this));
            else if (Effect == StationaryEffect.SummonEarthElemental)
                Cast(new EarthElementalSpell(from, this));
            else if (Effect == StationaryEffect.SummonFireElemental)
                Cast(new FireElementalSpell(from, this));
            else if (Effect == StationaryEffect.SummonWaterElemental)
                Cast(new WaterElementalSpell(from, this));
            else if (Effect == StationaryEffect.SummonDaemon)
                Cast(new SummonDaemonSpell(from, this));
            else if (Effect == StationaryEffect.Curse)
                Cast(new CurseSpell(from, this));

        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0); // version
            writer.Write((int)m_Effect);
            writer.Write((int)m_Charges);
            writer.Write((List<Mobile>)m_IDList, true);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
            m_Effect = (StationaryEffect)reader.ReadInt();
            m_Charges = reader.ReadInt();
            m_IDList = reader.ReadStrongMobileList();
        }
    }
}