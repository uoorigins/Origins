using System;
using Server;
using Server.Mobiles;

namespace Server
{
    public partial class BaseSpeech
    {
        public static string GetJob(BaseCreature m_Mobile)
        {
            BaseVendor m = m_Mobile as BaseVendor;

                if (m is Alchemist)
                    return "alchemist";
                else if (m is AnimalTrainer)
                    return "animal trainer";
                else if (m is Architect)
                    return "architect";
                else if (m is Armorer)
                    return "armorer";
                else if (m is Baker)
                    return "baker";
                else if (m is Bard || m is BardGuildmaster)
                    return "bard";
                else if (m is Barkeeper)
                    return "tavernkeeper";
                else if (m is Beekeeper)
                    return "beekeeper";
                else if (m is Blacksmith || m is BlacksmithGuildmaster)
                    return "blacksmith";
                else if (m is Bowyer)
                    return "bowyer";
                else if (m is Butcher)
                    return "butcher";
                else if (m is Carpenter)
                    return "carpenter";
                else if (m is Cobbler)
                    return "cobbler";
                else if (m is Cook)
                    return "cook";
                else if (m is Farmer)
                    return "farmer";
                else if (m is Fisherman || m is FisherGuildmaster)
                    return "fisherman";
                else if (m is Furtrader)
                    return "furtrader";
                else if (m is Glassblower)
                    return "glassblower";
                else if (m is Herbalist)
                    return "herbalist";
                else if (m is InnKeeper)
                    return "innkeeper";
                else if (m is Jeweler)
                    return "jeweler";
                else if (m is LeatherWorker)
                    return "leatherworker";
                else if (m is Mage || m is MageGuildmaster)
                    return "mage";
                else if (m is Mapmaker)
                    return "mapmaker";
                else if (m is Miller)
                    return "miller";
                else if (m is Miner || m is MinerGuildmaster)
                    return "miner";
                else if (m is Provisioner)
                    return "provisioner";
                else if (m is Rancher)
                    return "rancher";
                else if (m is Scribe)
                    return "scribe";
                else if (m is Shipwright)
                    return "shipwright";
                else if (m is Tailor || m is TailorGuildmaster)
                    return "tailor";
                else if (m is Tanner)
                    return "tanner";
                else if (m is TavernKeeper)
                    return "tavernkeeper";
                else if (m is Thief || m is ThiefGuildmaster)
                    return "theif";
                else if (m is Tinker || m is TinkerGuildmaster)
                    return "tinker";
                else if (m is Waiter)
                    return "waiter";
                else if (m is Weaponsmith)
                    return "weaponsmith";
                else if (m is Weaver)
                    return "weaver";
                else if (m is HealerGuildmaster)
                    return "healer";
                else if (m is MerchantGuildmaster)
                    return "merchant";
                else if (m is RangerGuildmaster)
                    return "ranger";
                else if (m is WarriorGuildmaster)
                    return "warrior";
                else if (m_Mobile is Actor)
                    return "actor";
                else if (m_Mobile is Artist)
                    return "artist";
                else if (m_Mobile is Banker)
                    return "banker";
                else if (m_Mobile is BrideGroom)
                    return m_Mobile.Female ? "bride" : "groom";
                else if (m_Mobile is EscortableMage)
                    return "mage";
                else if (m_Mobile is Gypsy)
                    return "gypsy";
                else if (m_Mobile is HarborMaster)
                    return "harbor master";
                else if (m_Mobile is Merchant)
                    return "merchant";
                else if (m_Mobile is Messenger)
                    return "runner";
                else if (m_Mobile is Minter)
                    return "minter";
                else if (m_Mobile is Noble)
                    return "noble";
                else if (m_Mobile is Peasant)
                    return "peasent";
                else if (m_Mobile is Sculptor)
                    return "sculptor";
                else if (m_Mobile is SeekerOfAdventure)
                    return "seeker of adventure";
                else if (m_Mobile is TownCrier)
                    return "town crier";
                else if (m_Mobile is BaseGuard || m_Mobile is SpawningGuard)
                    return "guard";
                else if (m_Mobile is Healer || m_Mobile is EvilHealer)
                    return "healer";
                else if (m_Mobile is WanderingHealer || m_Mobile is EvilWanderingHealer)
                    return "wandering healer";
                else
                    return "worker";
        }
    }
}