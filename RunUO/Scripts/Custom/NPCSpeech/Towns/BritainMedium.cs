using System;
using Server;
using Server.Mobiles;

namespace Server
{
    public partial class BaseSpeech
    {
        public static string BritainMedium(BaseCreature m_Mobile, SpeechEventArgs e)
        {
            string response = null;

            if (e.HasKeyword(0x0122)) // *where am i"
            {
                switch (Utility.Random(2))
                {
                    case 0: response = ("Why, thou'rt in the city of Britain, of course."); break;
                    case 1: response = (String.Format("Thou art in Britain, {0}.", e.Mobile.Female ? "milady" : "milord")); break;
                }
            }
            else if (e.HasKeyword(0x0125)) // *dummies*, *dummy*, *training dummies*, *training dummy*
            {
                switch (Utility.Random(3))
                {
                    case 0: response = ("The Cavalry Guild has training dummies. They can be found just north of The Wayfarer's Inn."); break;
                    case 1: response = ("Thy swordsmanship can be practiced at the old keep or at the Cavalry Guild."); break;
                    case 2: response = ("Just south of Castle Britain is the old keep. The Warrior's Guild now calls it home. Thou can find dummies there."); break;
                }
            }
            else if (Insensitive.Speech(e.Speech, "thief") || Insensitive.Speech(e.Speech, "thiev") || Insensitive.Speech(e.Speech, "steal"))
            {
                switch (Utility.Random(2))
                {
                    case 0: response = ("I know naught of thieves!"); break;
                    case 1: response = ("Indeed, there be thievery in Britain."); break;
                }
            }
            else if (Insensitive.Speech(e.Speech, "smith"))
            {
                switch (Utility.Random(2))
                {
                    case 0: response = ("Which sort of smith? Weaponsmith, blacksmith?"); break;
                    case 1: response = ("There are many sorts of smith."); break;
                }
            }
            else if (Insensitive.Speech(e.Speech, "blacksmith"))
            {
                response = ("There is a blacksmith beside the castle moat on the northern edge of town.");
            }
            else if (Insensitive.Speech(e.Speech, "weaponsmith"))
            {
                switch (Utility.Random(2))
                {
                    case 0: response = ("Thou mightest wish to check at the armourer's, or the castle."); break;
                    case 1: response = ("Thou canst buy weapons at the blacksmith's shop."); break;
                }
            }
            else if (Insensitive.Speech(e.Speech, "conservatory") || Insensitive.Speech(e.Speech, "bard") || Insensitive.Speech(e.Speech, "minstrel") || Insensitive.Speech(e.Speech, "musician") || Insensitive.Speech(e.Speech, "troubad"))
            {
                response = ("The center of the musical arts in Britain is the Bardic Conservatory on the north side of town.");
            }
            else if (Insensitive.Speech(e.Speech, "mage tower") || Insensitive.Speech(e.Speech, "mage guild") || Insensitive.Speech(e.Speech, "mage"))
            {
                switch (Utility.Random(2))
                {
                    case 0: response = ("The Mage Tower is the building with all the arches, made of grey stone, on the north side of the town."); break;
                    case 1: response = ("Ah, the Mage Guild is housed within a tower of many arches, with pools of water beside it."); break;
                }
            }
            else if (Insensitive.Speech(e.Speech, "mage's shop") || Insensitive.Speech(e.Speech, "magic") || Insensitive.Speech(e.Speech, "magic shop") || Insensitive.Speech(e.Speech, "mage shop"))
            {
                switch (Utility.Random(5))
                {
                    case 0: response = ("There are three or four merchants of the arcane in Britain."); break;
                    case 1: response = ("The magic shop The Sorcerer's Delight is also known as the Mage Tower. Thou can purchase magical items there."); break;
                    case 2: response = ("Ethral Goods Magic Shop has magical items for sale, and it boasts the only alchemist in Britain."); break;
                    case 3: response = ("The magic shop Incantations and Enchantments is situated by the park in eastern Britain."); break;
                    case 4: response = ("Sage Advice Magic Shop can sell thee some arcane items, if that is thy desire."); break;
                }
            }
            else if (Insensitive.Speech(e.Speech, "Sorcerer's Delight"))
            {
                response = ("Thou can purchase arcane goods at the Sorcerer's Delight - which some call the Mage Tower. It can be found in northern Britain, next to the Conservatory of Music.");
            }
            else if (Insensitive.Speech(e.Speech, "Ethral") || Insensitive.Speech(e.Speech, "Ethral Goods"))
            {
                response = ("Thou can find the Ethral Goods Magic Shop just south of the Main Gate. Next to the Premier Provisioner's shop.");
            }
            else if (Insensitive.Speech(e.Speech, "Incantations and Enchantments") || Insensitive.Speech(e.Speech, "Incantations"))
            {
                response = ("Incantations and Enchantments is in the middle of eastern Britain, on the south side of the park.");
            }
            else if (Insensitive.Speech(e.Speech, "Sage Advice"))
            {
                response = ("Thou can get to the Sage Advice Magic Shop on the east side of the river.");
            }
            else if (Insensitive.Speech(e.Speech, "stables"))
            {
                switch (Utility.Random(2))
                {
                    case 0: response = ("Aside from Lord British's stables, the only ones I can think of lie on the western bank of the river."); break;
                    case 1: response = ("Try the stables beside the Mage Tower."); break;
                }
            }
            else if (Insensitive.Speech(e.Speech, "vet"))
            {
                switch (Utility.Random(2))
                {
                    case 0: response = ("There is a doctor of animals whose shop lies beside The Bucking Horse Stables."); break;
                    case 1: response = ("The city's sole veterinarian lives by the river, by the Mage's Bridge."); break;
                }
            }
            else if (Insensitive.Speech(e.Speech, "theatre"))
            {
                response = ("Lord British supports the arts. There is a public theatre across from the Bardic Conservatory.");
            }
            else if (Insensitive.Speech(e.Speech, "woodworker"))
            {
                switch (Utility.Random(2))
                {
                    case 0: response = ("A goodly carpenter lives not far from the market square before the castle gate."); break;
                    case 1: response = ("There is a woodworker near the city library."); break;
                }
            }
            else if (Insensitive.Speech(e.Speech, "theatre"))
            {
                response = ("There are many guilds in this city. 'Tis the benefit of living in Lord British's capital.");
            }
            else if (Insensitive.Speech(e.Speech, "mining guild") || Insensitive.Speech(e.Speech, "miners"))
            {
                response = ("There is a Mining Guild beside the city library.");
            }
            else if (Insensitive.Speech(e.Speech, "baker") || Insensitive.Speech(e.Speech, "bread"))
            {
                response = ("There's a baker on the market square before the castle gate.");
            }
            else if (Insensitive.Speech(e.Speech, "tanner") || Insensitive.Speech(e.Speech, "leather"))
            {
                switch (Utility.Random(2))
                {
                    case 0: response = ("A tanner's shop exists by Poor Gate, 'gainst the old city wall."); break;
                    case 1: response = ("There is a leatherworker's shop facing the market square."); break;
                }
            }
            else if (Insensitive.Speech(e.Speech, "healer"))
            {
                switch (Utility.Random(2))
                {
                    case 0: response = ("There is a healer on the market square, near the Main Gate."); break;
                    case 1: response = ("If thou dost walk north through the main gates, thou will be facing the healer's shop."); break;
                }
            }
            else if (Insensitive.Speech(e.Speech, "bowyer") || Insensitive.Speech(e.Speech, "fletcher"))
            {
                response = ("Try the bowyer just East of the theatre.");
            }
            else if (Insensitive.Speech(e.Speech, "butcher"))
            {
                switch (Utility.Random(2))
                {
                    case 0: response = ("Britain's butcher has a shop on the waterfront, next to the custome house."); break;
                    case 1: response = ("The butcher can be found between the two docks, on the waterfront."); break;
                }
            }
            else if (Insensitive.Speech(e.Speech, "armour") || Insensitive.Speech(e.Speech, "armor"))
            {
                switch (Utility.Random(3))
                {
                    case 0: response = ("There is an armourer's shop beside the Mage Gate. Just South of the old city wall."); break;
                    case 1: response = ("Strength and Steel Armory is located in front of the Mage's Tower in Northern Britain."); break;
                    case 2: response = ("There's an Armorer way out to the East. Just North of the Lighthouse."); break;
                }
            }
            else if (Insensitive.Speech(e.Speech, "inn"))
            {
                switch (Utility.Random(4))
                {
                    case 0: response = ("If there be one thing Britain is not lacking, it be inns!"); break;
                    case 1: response = ("The Wayfarer's Inn is just to the East of the Mage's Bridge."); break;
                    case 2: response = ("An inn called Sweet Dreams lies next to the Main Gate, just East of the healer's shop."); break;
                    case 3: response = ("The North Side Inn is way up in North Britain, just past the Mage's Tower."); break;
                }
            }
            else if (Insensitive.Speech(e.Speech, "Wayfarer's Inn") || Insensitive.Speech(e.Speech, "Wayfarers Inn"))
            {
                switch (Utility.Random(2))
                {
                    case 0: response = ("'Tis South of the Cavalry Guild by the Mage's Gate."); break;
                    case 1: response = ("The Wayfarer's Inn is beside the Mage's Gate, on the East side of the river."); break;
                }
            }
            else if (Insensitive.Speech(e.Speech, "Sweet Dreams"))
            {
                switch (Utility.Random(2))
                {
                    case 0: response = ("The Sweet Dreams Inn is quite upscale!"); break;
                    case 1: response = ("Despite its name, the Sweet Dreams inn is quite good. It is near the Main Gate."); break;
                }
            }
            else if (Insensitive.Speech(e.Speech, "Northside"))
            {
                switch (Utility.Random(2))
                {
                    case 0: response = ("The Northside Inn is situated on the lake that surrounds Blackthorn's Castle."); break;
                    case 1: response = ("The Northside Inn is quite good. It is just North of the Music Conservatory."); break;
                }
            }
            else if (Insensitive.Speech(e.Speech, "alchemist"))
            {
                switch (Utility.Random(2))
                {
                    case 0: response = ("The alchemist shares space in the Ethral Goods Magic Shop."); break;
                    case 1: response = ("The only alchemist I know of is in the magic shop near the Main Gate."); break;
                }
            }
            else if (Insensitive.Speech(e.Speech, "weaponeer") || Insensitive.Speech(e.Speech, "weaponsmith"))
            {
                response = ("The armorer beside the Tinker's Guild can probably sell thee anything thou mightest need. Or look to the blacksmith.");
            }
            else if (Insensitive.Speech(e.Speech, "mechanician"))
            {
                response = ("The guildhall of the Mechanicians is right beside Poor Gate. Some do call them Tinkerers, if thou were wondering.");
            }
            else if (Insensitive.Speech(e.Speech, "artisan"))
            {
                response = ("There be a guildhall for the artisans of the town outside the old walls, south of the armorer's.");
            }
            else if (Insensitive.Speech(e.Speech, "provision"))
            {
                switch (Utility.Random(3))
                {
                    case 0: response = ("One can purchase provisions at any of a number of shops."); break;
                    case 1: response = ("There is a provisioner's directly before the Main Gate."); break;
                    case 2: response = ("A quite well-appointed provisioner's exists in the Eastern part of Britain."); break;
                }
            }
            else if (Insensitive.Speech(e.Speech, "jewel"))
            {
                response = "There is a jeweler's by the Artist's Guild.";
            }
            else if (Insensitive.Speech(e.Speech, "bank"))
            {
                response = ("The First Bank of Britain lies across the street from the Jeweler's, beside the moat.");
            }
            else if (Insensitive.Speech(e.Speech, "clothes") || (Insensitive.Speech(e.Speech, "clothiers")))
            {
                switch (Utility.Random(3))
                {
                    case 0: response = ("A goodly shop for clothing is next to Cypress Bridge, on the East side of the river."); break;
                    case 1: response = ("A good shop for clothing is next to the Blue Boar Tavern."); break;
                    case 2: response = ("Thou can purchase clothes on the east side of the river."); break;
                }
            }
            else if (Insensitive.Speech(e.Speech, "tavern"))
            {
                switch (Utility.Random(2))
                {
                    case 0: response = ("If thou seekest a place to drink, well, there are many choices!"); break;
                    case 1: response = ("There's the Salty Dog, The Unicorn's Horn, The Cat's Lair, and The Blue Boar taverns, here in Britain."); break;
                }
            }
            else if (Insensitive.Speech(e.Speech, "Salty Dog"))
            {
                response = ("The Salty Dog is a tavern overlooking the park, right next to The Wayfarer's Inn.");
            }
            else if (Insensitive.Speech(e.Speech, "Unicorn Horn"))
            {
                response = ("'Tis an expensive place, but the Unicorn Horn, overlooking the ocean on the east side, serves a decent ale.");
            }
            else if (Insensitive.Speech(e.Speech, "Blue Boar"))
            {
                response = ("The Blue Boar is just North of the library, on the river.");
            }
            else if (Insensitive.Speech(e.Speech, "Cat's Lair"))
            {
                response = ("The Cat's Lair is a bit disreputable, as it lies near the docks, but its beer is good.");
            }
            else if (Insensitive.Speech(e.Speech, "shipwright") || Insensitive.Speech(e.Speech, "oaken oar") || Insensitive.Speech(e.Speech, "boats") || Insensitive.Speech(e.Speech, "ships"))
            {
                switch (Utility.Random(2))
                {
                    case 0: response = ("The place to buy waterfaring vessels is the Oaken Oar."); break;
                    case 1: response = ("The Oaken Oar houses excellent shipwrights. It is on the waterfront by the mouth of the Narrow Neck."); break;
                }
            }
            else if (Insensitive.Speech(e.Speech, "cartographer") || Insensitive.Speech(e.Speech, "map"))
            {
                switch (Utility.Random(2))
                {
                    case 0: response = ("Cartographer's can usually be found sharing space with shipwrights."); break;
                    case 1: response = ("If there's not a mapmaker to be found at the Oaken Oar, then I can't help thee much."); break;
                }
            }
            else if (Insensitive.Speech(e.Speech, "customs") || Insensitive.Speech(e.Speech, "customs house"))
            {
                switch (Utility.Random(2))
                {
                    case 0: response = ("The Customs office checks imported goods. 'Tis on the waterfront."); break;
                    case 1: response = ("Smugglers from Buccaneer's Den have taken to going to Vesper since the Customs House opened on the waterfront."); break;
                }
            }
            else if (Insensitive.Speech(e.Speech, "train") || Insensitive.Speech(e.Speech, "weapons trainer"))
            {
                switch (Utility.Random(2))
                {
                    case 0: response = ("Thou canst be trained in weapons at the weapons trainer. But thou shouldst be warned that 'tis expensive. He caters to the nobility."); break;
                    case 1: response = ("The weapons trainer? Ah, that is on the north edge of town, on the eastern bank of the river. 'Tis called the Cavalry Guild."); break;
                }
            }
            else if (Insensitive.Speech(e.Speech, "lighthouse"))
            {
                switch (Utility.Random(2))
                {
                    case 0: response = ("'Tis being repaired, but it stands on the promontory o'erlooking Brittany Bay."); break;
                    case 1: response = ("The lighthouse is a most imposing sight, but 'twas badly damaged in a storm, and 'tis inoperable."); break;
                }
            }
            else if (Insensitive.Speech(e.Speech, "Lord British"))
            {
                switch (Utility.Random(2))
                {
                    case 0: response = ("He liveth here, in Britain, in his castle."); break;
                    case 1: response = ("Lord British doth reside in his castle, but the gates are always open."); break;
                }
            }
            else if (Insensitive.Speech(e.Speech, "LB"))
            {
                response = ("LB? Mayhap thou means Lord British ....");
            }
            else if (Insensitive.Speech(e.Speech, "castle"))
            {
                if (Insensitive.Speech(e.Speech, "blackthorn's castle"))
                {
                    switch (Utility.Random(2))
                    {
                        case 0: response = ("Blackthorn stays out of sight mostly, sequestered in his castle up North of Britain."); break;
                        case 1: response = ("Blackthorn's castle is on an island in the middle of a lake North of Britain."); break;
                    }
                }
                else
                {
                    switch (Utility.Random(3))
                    {
                        case 0: response = ("Lord British's castle is quite huge. A moat encircles it. Thou canst not miss it."); break;
                        case 1: response = ("Lord British's castle forms the western side of the city. Only farms lie beyond it."); break;
                        case 2: response = ("Blackthorn's castle is on an island in the middle of a lake North of Britain."); break;
                    }
                }
            }
            else if (Insensitive.Speech(e.Speech, "blackthorn"))
            {
                switch (Utility.Random(2))
                {
                    case 0: response = ("Blackthorn stays out of sight mostly, sequestered in his castle up North of Britain."); break;
                    case 1: response = ("Blackthorn's castle is on an island in the middle of a lake North of Britain."); break;
                }
            }
            else if (Insensitive.Speech(e.Speech, "old keep") || Insensitive.Speech(e.Speech, "fighter") || Insensitive.Speech(e.Speech, "warrior"))
            {
                switch (Utility.Random(3))
                {
                    case 0: response = ("The Warrior's Guild is housed in the Old Keep."); break;
                    case 1: response = ("The Old Keep, where the Warrior's Guild is headquartered, was once the castle hereabouts, but now we have Lord British's castle."); break;
                    case 2: response = ("'Tis crumbling into the Narrows, but the home of the Warrior's Guild is the Old Keep."); break;
                }
            }
            else if (Insensitive.Speech(e.Speech, "farms") || Insensitive.Speech(e.Speech, "farmers"))
            {
                response = ("The farms in this area are all in the countryside, to the west of Lord British's castle. Thou must cross the Narrows and follow the road there.");
            }
            else if (Insensitive.Speech(e.Speech, "river"))
            {
                switch (Utility.Random(2))
                {
                    case 0: response = ("Britain is bounded by two rivers. The Narrows is on the west and Brittany River on the east."); break;
                    case 1: response = ("When most people say the river, they mean Brittany River, which runs through the middle of the city."); break;
                }
            }
            else if (Insensitive.Speech(e.Speech, "narrows") || Insensitive.Speech(e.Speech, "narrows neck") || Insensitive.Speech(e.Speech, "neck"))
            {
                response = ("Narrows Neck is the name for the river mouth on the southwestern side of Britain.");
            }
            else if (Insensitive.Speech(e.Speech, "Brittany River"))
            {
                response = ("That's the river that runs down the middle of the city.");
            }
            else if (Insensitive.Speech(e.Speech, "gate"))
            {
                if (Insensitive.Speech(e.Speech, "River's Gate"))
                {
                    response = ("The Southernmost bridge is called River's Gate.");
                }
                else if (Insensitive.Speech(e.Speech, "poor gate"))
                {
                    switch (Utility.Random(2))
                    {
                        case 0: response = ("That is the name of the old gate beside the castle moat, for once the poor came in through there to go to market. It is quite narrow."); break;
                        case 1: response = ("Poor Gate is on the south wall, beside the Mechanicians' Guild."); break;
                    }
                }
                else if (Insensitive.Speech(e.Speech, "Main gate"))
                {
                    switch (Utility.Random(2))
                    {
                        case 0: response = ("The Main Gate is quite majestic! The space before it is paved with grey and red stones, and there be guard towers on either side."); break;
                        case 1: response = ("The Main Gate divides the central city from the waterfront."); break;
                    }
                }
                else
                    response = ("There are two gates in the old city walls. The Poor Gate, and the Main Gate are the names.");
            }
            else if (Insensitive.Speech(e.Speech, "city wall") || Insensitive.Speech(e.Speech, "wall"))
            {
                switch (Utility.Random(3))
                {
                    case 0: response = ("When Britain was but a young city, 'twas enclosed by a wall. But it has long since outgrown it."); break;
                    case 1: response = ("Thou canst see that the old city walls are of a different vintage stone than the castle proper. 'Twas built before the current castle buildings."); break;
                    case 2: response = ("The northern side of the old city wall hath disappeared completely. 'Tis said the Mage Tower was built from its fragments."); break;
                }
            }
            else if (Insensitive.Speech(e.Speech, "bridge"))
            {
                if (Insensitive.Speech(e.Speech, "Northern bridge") || Insensitive.Speech(e.Speech, "Great Northern") || Insensitive.Speech(e.Speech, "Great bridge"))
                {
                    switch (Utility.Random(2))
                    {
                        case 0: response = ("The Great Northern Bridge is so called because it's the Northern-most bridge in Britain."); break;
                        case 1: response = ("The Great Northern Bridge is built fairly high above the water."); break;
                    }
                }
                else if (Insensitive.Speech(e.Speech, "Cypress Bridge"))
                {
                    switch (Utility.Random(2))
                    {
                        case 0: response = ("Cypress Bridge is named for the trees which greet thee coming and going across it."); break;
                        case 1: response = ("The Cypress Bridge is surrounded on one side by a tailor's shop and on the other by a magic shop."); break;
                    }
                }
                else if (Insensitive.Speech(e.Speech, "Mage's Bridge"))
                {
                    switch (Utility.Random(3))
                    {
                        case 0: response = ("The bridge next to the Mage's tower is thus called the Mage's Bridge."); break;
                        case 1: response = ("Mage's Bridge was once a gate leading into the city, but the old wall has outlived its usefulness and most of it has been torn down."); break;
                        case 2: response = ("The Mage's Bridge is so named for the fact that the Mage's Tower almost overlooks it."); break;
                    }
                }
                else
                {
                    switch (Utility.Random(6))
                    {
                        case 0: response = ("There are many bridges across the Brittany. There's the Great Northern Bridge at the north-most end, the Mage's Bridge, then the one we call Virtue's Pass. South of that there's Cypress Bridge, and the River's Gate Bridge. And, of course, the Gung-Farmer's Bridge connects Britain with the farmlands to the West."); break;
                        case 1: response = ("The bridge next to the Mage's tower is thus called the Mage's Bridge."); break;
                        case 2: response = ("The Great Northern Bridge is so called because it's the Northern-most bridge in Britain."); break;
                        case 3: response = ("Cypress Bridge is named for the trees which greet thee coming and going across it."); break;
                        case 4: response = ("Virtue's Pass connects to Virtue's Path, which will take thee into Lord British's Castle."); break;
                        case 5: response = ("There is the Gung-Farmer's Bridge, which leadeth across Narrows Neck."); break;
                    }
                }
            }
            else if (Insensitive.Speech(e.Speech, "Great Northern"))
            {
                switch (Utility.Random(2))
                {
                    case 0: response = ("The Great Northern Bridge is so called because it's the Northern-most bridge in Britain."); break;
                    case 1: response = ("The Great Northern Bridge is built fairly high above the water."); break;
                }
            }
            else if (Insensitive.Speech(e.Speech, "Virtue's Pass"))
            {
                switch (Utility.Random(2))
                {
                    case 0: response = ("Virtue's Pass connects to Virtue's Path, which will take thee into Lord British's Castle."); break;
                    case 1: response = ("There used to be a gate at Virtue's Pass, it was torn down some time ago."); break;
                }
            }
            else if (Insensitive.Speech(e.Speech, "Gung-Farmer") || Insensitive.Speech(e.Speech, "Gung Farmer"))
            {
                response = ("The Gung-Farmer's Bridge connects all the farms with Britain proper.");
            }
            else if (Insensitive.Speech(e.Speech, "ankh") || Insensitive.Speech(e.Speech, "death") || Insensitive.Speech(e.Speech, "temple") || Insensitive.Contains(e.Speech, "resur"))
            {
                switch (Utility.Random(2))
                {
                    case 0: response = ("Thou mayst find the temple, where thou canst be resurrected, directly by Mage's Bridge."); break;
                    case 1: response = ("The ankh hath the power to restore life to the dead. 'Tis in the temple, by Mage's Bridge."); break;
                }
            }
            else if (Insensitive.Speech(e.Speech, "paws"))
            {
                response = ("Paws? Take a look at the feet of a dog!");
            }
            else if (Insensitive.Speech(e.Speech, "guardhouse") || Insensitive.Speech(e.Speech, "guard house"))
            {
                switch (Utility.Random(2))
                {
                    case 0: response = ("The two guardhouses are located near the Gung-Farmer's Bridge and next to the Sweet Dreams Inn."); break;
                    case 1: response = ("There is a Guardhouse right beside the Cat's Lair."); break;
                }
            }
            else if (Insensitive.Speech(e.Speech, "waterfront"))
            {
                response = ("The waterfront is what many call everything south of the old city wall. Properly speaking, it runneth along the river from River's Gate Bridge, down along the water to the Oaken Oar and the Narrows.");
            }
            else if (Insensitive.Speech(e.Speech, "moat"))
            {
                response = ("Long ago the Narrows were dug up to meet the moat, and now 'tis fed from the water of Brittany Bay.");
            }
            else if (Insensitive.Speech(e.Speech, "brittany bay"))
            {
                response = ("'Tis the bay on our southern coast.");
            }
            else if (Insensitive.Speech(e.Speech, "ocean"))
            {
                response = ("The ocean is to the south of Britain.");
            }
            else if (Insensitive.Speech(e.Speech, "undead") || Insensitive.Speech(e.Speech, "skeleton") || Insensitive.Speech(e.Speech, "graves") || Insensitive.Speech(e.Speech, "graveyard") || Insensitive.Speech(e.Speech, "crypt") || Insensitive.Speech(e.Speech, "cemetery") || Insensitive.Speech(e.Speech, "mausoleum"))
            {
                switch (Utility.Random(3))
                {
                    case 0: response = ("Ah, 'tis a terrible thing, but the graveyard far to the north has become infested with the living bones of the dead."); break;
                    case 1: response = ("We have had to abandon our mausoleums, for the cemetery north of here has been touched by magic and is now filled with undead skeletons."); break;
                }
            }

            return response;
        }
    }
}