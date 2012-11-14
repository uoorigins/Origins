using System;
using Server.Items;

namespace Server.Engines.Craft
{
	public class DefTailoring : CraftSystem
	{
		public override SkillName MainSkill
		{
			get	{ return SkillName.Tailoring; }
		}

		public override int GumpTitleNumber
		{
			get { return 1044005; } // <CENTER>TAILORING MENU</CENTER>
		}

		private static CraftSystem m_CraftSystem;

		public static CraftSystem CraftSystem
		{
			get
			{
				if ( m_CraftSystem == null )
					m_CraftSystem = new DefTailoring();

				return m_CraftSystem;
			}
		}

		public override CraftECA ECA{ get{ return CraftECA.ChanceMinusSixtyToFourtyFive; } }

		public override double GetChanceAtMin( CraftItem item )
		{
			return 0.0; // 50%
		}

		private DefTailoring() : base( 1, 1, 1.25 )// base( 1, 1, 4.5 )
		{
		}

		public override int CanCraft( Mobile from, BaseTool tool, Type itemType )
		{
			if( tool == null || tool.Deleted || tool.UsesRemaining < 0 )
				return 1044038; // You have worn out your tool!
			else if ( !BaseTool.CheckAccessible( tool, from ) )
				return 1044263; // The tool must be on your person to use.

			return 0;
		}

		public override void PlayCraftEffect( Mobile from )
		{
			from.PlaySound( 0x248 );
		}

		public override int PlayEndingEffect( Mobile from, bool failed, bool lostMaterial, bool toolBroken, int quality, bool makersMark, CraftItem item )
		{
			if ( toolBroken )
                from.SendAsciiMessage("You have worn out your tool!");

			if ( failed )
			{
                from.SendAsciiMessage("You failed to create the item, and some of your materials are lost.");
				if ( lostMaterial )
					return 1044043; // You failed to create the item, and some of your materials are lost.
				else
					return 1044157; // You failed to create the item, but no materials were lost.
			}
			else
			{
                from.SendAsciiMessage("You create the item and put it in your backpack.");
				if ( quality == 0 )
					return 502785; // You were barely able to make this item.  It's quality is below average.
				else if ( makersMark && quality == 2 )
					return 1044156; // You create an exceptional quality item and affix your maker's mark.
				else if ( quality == 2 )
					return 1044155; // You create an exceptional quality item.
				else				
					return 1044154; // You create the item.
			}
		}

		public override void InitCraftList()
		{
			int index = -1;

			#region Shirts
			AddCraft( typeof( Shirt ), 1015269, 1025399,0.0, 100.0, typeof( Cloth ), 1044286, 8, 1044287 );
            AddCraft( typeof( Cloak ), 1015269, 1025397, 0.0, 100.0, typeof(Cloth), 1044286, 14, 1044287);
			AddCraft( typeof( FancyShirt ), 1015269, 1027933, 0.0, 100.0, typeof( Cloth ), 1044286, 8, 1044287 );
            AddCraft( typeof( FancyDress ), 1015269, 1027935, 0.0, 100.0, typeof( Cloth ), 1044286, 12, 1044287 );
			AddCraft( typeof( PlainDress ), 1015269, 1027937, 0.0, 100.0, typeof( Cloth ), 1044286, 10, 1044287 );
			AddCraft( typeof( Robe ), 1015269, 1027939, 0.0, 100.0, typeof( Cloth ), 1044286, 16, 1044287 );
			#endregion

			#region Pants
			AddCraft( typeof( LongPants ), 1015279, 1025433, 0.0, 100.0, typeof( Cloth ), 1044286, 8, 1044287 );
			AddCraft( typeof( Kilt ), 1015279, 1025431, 0.0, 100.0, typeof( Cloth ), 1044286, 8, 1044287 );
			AddCraft( typeof( Skirt ), 1015279, 1025398, 0.0, 100.0, typeof( Cloth ), 1044286, 10, 1044287 );
			#endregion

			#region Misc
            AddCraft( typeof( SkullCap ), 1011375, 1025444, 0.0, 100.0, typeof(Cloth), 1044286, 2, 1044287);
            AddCraft( typeof( Bandana ), 1011375, 1025440, 0.0, 100.0, typeof(Cloth), 1044286, 2, 1044287);
			AddCraft( typeof( BodySash ), 1015283, 1025441, 4.1, 29.1, typeof( Cloth ), 1044286, 4, 1044287 );
			AddCraft( typeof( HalfApron ), 1015283, 1025435, 0.0, 100.0, typeof( Cloth ), 1044286, 6, 1044287 );
			AddCraft( typeof( FullApron ), 1015283, 1025437, 0.0, 100.0, typeof( Cloth ), 1044286, 10, 1044287 );
			#endregion

            #region Cloth
            AddCraft(typeof(BoltOfCloth), 1011375, 1025444, 0.0, 0.0, typeof(Cloth), 1044286, 50, 1044287);
            AddCraft(typeof(BoltOfCloth), 1011375, 1025440, 0.0, 0.0, typeof(Cloth), 1044286, 50, 1044287);
            AddCraft(typeof(BoltOfCloth), 1015283, 1025441, 0.0, 0.0, typeof(Cloth), 1044286, 50, 1044287);
            #endregion

			#region Footwear
            AddCraft( typeof( Boots ), 1015288, 1025899, 0.0, 100.0, typeof(Leather), 1044462, 8, 1044463);
			AddCraft( typeof( Sandals ), 1015288, 1025901, 0.0, 100.0, typeof( Leather ), 1044462, 4, 1044463 );
			AddCraft( typeof( Shoes ), 1015288, 1025904, 0.0, 100.0, typeof( Leather ), 1044462, 6, 1044463 );
			AddCraft( typeof( ThighBoots ), 1015288, 1025906, 0.0, 100.0, typeof( Leather ), 1044462, 10, 1044463 );
			#endregion

			#region Leather Armor

			index = AddCraft( typeof( LeatherGorget ), 1015293, 1025063, 0.0, 100.0, typeof( Leather ), 1044462, 4, 1044463 );
            ForceNonExceptional(index);
            index = AddCraft(typeof(LeatherGloves), 1015293, 1025062, 0.0, 100.0, typeof(Leather), 1044462, 3, 1044463);
            ForceNonExceptional(index);
            index = AddCraft(typeof(LeatherArms), 1015293, 1025061, 0.0, 100.0, typeof(Leather), 1044462, 8, 1044463);
            ForceNonExceptional(index);
            index = AddCraft(typeof(LeatherLegs), 1015293, 1025067, 0.0, 100.0, typeof(Leather), 1044462, 10, 1044463);
            ForceNonExceptional(index);
            index = AddCraft(typeof(LeatherChest), 1015293, 1025068, 0.0, 100.0, typeof(Leather), 1044462, 12, 1044463);
            ForceNonExceptional(index);
			#endregion

			#region Studded Armor
            index = AddCraft(typeof(StuddedGorget), 1015300, 1025078, 0.0, 100.0, typeof(Leather), 1044462, 6, 1044463);
            ForceNonExceptional(index);
            index = AddCraft(typeof(StuddedGloves), 1015300, 1025077, 0.0, 100.0, typeof(Leather), 1044462, 8, 1044463);
            ForceNonExceptional(index);
            index = AddCraft(typeof(StuddedArms), 1015300, 1025076, 0.0, 100.0, typeof(Leather), 1044462, 10, 1044463);
            ForceNonExceptional(index);
            index = AddCraft(typeof(StuddedLegs), 1015300, 1025082, 0.0, 100.0, typeof(Leather), 1044462, 12, 1044463);
            ForceNonExceptional(index);
            index = AddCraft(typeof(StuddedChest), 1015300, 1025083, 0.0, 100.0, typeof(Leather), 1044462, 14, 1044463);
            ForceNonExceptional(index);
			#endregion

			#region Female Armor
            index = AddCraft(typeof(LeatherShorts), 1015306, 1027168, 0.0, 100.0, typeof(Leather), 1044462, 8, 1044463);
            ForceNonExceptional(index);
            index = AddCraft(typeof(LeatherSkirt), 1015306, 1027176, 0.0, 100.0, typeof(Leather), 1044462, 6, 1044463);
            ForceNonExceptional(index);
            index = AddCraft(typeof(LeatherBustierArms), 1015306, 1027178, 0.0, 100.0, typeof(Leather), 1044462, 6, 1044463);
            ForceNonExceptional(index);
            index = AddCraft(typeof(StuddedBustierArms), 1015306, 1027180, 0.0, 100.0, typeof(Leather), 1044462, 8, 1044463);
            ForceNonExceptional(index);
            index = AddCraft(typeof(FemaleLeatherChest), 1015306, 1027174, 0.0, 100.0, typeof(Leather), 1044462, 8, 1044463);
            ForceNonExceptional(index);
            index = AddCraft(typeof(FemaleStuddedChest), 1015306, 1027170, 0.0, 100.0, typeof(Leather), 1044462, 10, 1044463);
            ForceNonExceptional(index);
			#endregion

			#region Bone Armor
			index = AddCraft( typeof( BoneHelm ), 1049149, 1025206, 85.0, 110.0, typeof( Leather ), 1044462, 4, 1044463 );
			AddRes( index, typeof( Bone ), 1049064, 2, 1049063 );
			
			index = AddCraft( typeof( BoneGloves ), 1049149, 1025205, 89.0, 114.0, typeof( Leather ), 1044462, 6, 1044463 );
			AddRes( index, typeof( Bone ), 1049064, 2, 1049063 );

			index = AddCraft( typeof( BoneArms ), 1049149, 1025203, 92.0, 117.0, typeof( Leather ), 1044462, 8, 1044463 );
			AddRes( index, typeof( Bone ), 1049064, 4, 1049063 );

			index = AddCraft( typeof( BoneLegs ), 1049149, 1025202, 95.0, 120.0, typeof( Leather ), 1044462, 10, 1044463 );
			AddRes( index, typeof( Bone ), 1049064, 6, 1049063 );
		
			index = AddCraft( typeof( BoneChest ), 1049149, 1025199, 96.0, 121.0, typeof( Leather ), 1044462, 12, 1044463 );
			AddRes( index, typeof( Bone ), 1049064, 10, 1049063 );
			#endregion

			// Set the overridable material
			SetSubRes( typeof( Leather ), 1049150 );

			// Add every material you want the player to be able to choose from
			// This will override the overridable material
			AddSubRes( typeof( Leather ),		1049150, 00.0, 1044462, 1049311 );
			AddSubRes( typeof( SpinedLeather ),	1049151, 65.0, 1044462, 1049311 );
			AddSubRes( typeof( HornedLeather ),	1049152, 80.0, 1044462, 1049311 );
			AddSubRes( typeof( BarbedLeather ),	1049153, 99.0, 1044462, 1049311 );

			MarkOption = true;
			Repair = Core.AOS;
			CanEnhance = Core.AOS;
		}
	}
}