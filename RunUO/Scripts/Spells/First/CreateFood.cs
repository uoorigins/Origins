using System;
using Server.Items;
using Server.Targeting;

namespace Server.Spells.First
{
	public class CreateFoodSpell : MagerySpell
	{
		private static SpellInfo m_Info = new SpellInfo(
				"Create Food", "In Mani Ylem",
				224,
				9011,
				Reagent.Garlic,
				Reagent.Ginseng,
				Reagent.MandrakeRoot
			);

		public override SpellCircle Circle { get { return SpellCircle.First; } }

		public CreateFoodSpell( Mobile caster, Item scroll ) : base( caster, scroll, m_Info )
		{
		}

		private static FoodInfo[] m_Food = new FoodInfo[]
			{
				new FoodInfo( typeof( Grapes ), "a grape bunch" ),
				new FoodInfo( typeof( Ham ), "a ham" ),
				new FoodInfo( typeof( CheeseWedge ), "a wedge of cheese" ),
				new FoodInfo( typeof( Muffins ), "muffins" ),
				new FoodInfo( typeof( FishSteak ), "a fish steak" ),
				new FoodInfo( typeof( Ribs ), "cut of ribs" ),
				new FoodInfo( typeof( CookedBird ), "a cooked bird" ),
				new FoodInfo( typeof( Sausage ), "sausage" ),
				new FoodInfo( typeof( Apple ), "an apple" ),
				new FoodInfo( typeof( Peach ), "a peach" )
			};

		public override void OnCast()
		{
			if ( CheckSequence() )
			{
				FoodInfo foodInfo = m_Food[Utility.Random( m_Food.Length )];
				Item food = foodInfo.Create();

				if ( food != null )
                    Caster.Target = new InternalTarget(Caster, food);
			}

			FinishSequence();
		}

        private class InternalTarget : Target
        {
            private Item m_Food;
            private Mobile m_Caster;

            public InternalTarget(Mobile caster, Item food) : base(2, true, TargetFlags.None)
            {
                m_Caster = caster;
                m_Food = food;

                CheckLOS = true;
            }

            protected override void OnTarget(Mobile from, object o)
            {
                IPoint3D ip = o as IPoint3D;

                if (ip != null)
                {
                    if (ip is Item)
                        ip = ((Item)ip).GetWorldTop();

                    Point3D p = new Point3D(ip);

                    Map map = from.Map;
                    LandTile landTile = map.Tiles.GetLandTile(p.X, p.Y);

                    if ((TileData.LandTable[landTile.ID & 0x3FFF].Flags & TileFlag.Impassable) != 0)
                        from.SendAsciiMessage("You cannot create that there.");
                    else if (!map.CanFit(p.X, p.Y, p.Z, m_Food.ItemData.Height, false, false, true))
                        from.SendAsciiMessage("You cannot create that there.");
                    else
                    {
                        m_Food.MoveToWorld(p, m_Caster.Map);
                        m_Caster.FixedParticles( 0, 10, 5, 2003, EffectLayer.RightHand );
                        m_Caster.PlaySound( 0x1E2 );
                    }
                }
                else
                    from.SendAsciiMessage("You cannot create that there.");
            }
        }
	}

	public class FoodInfo
	{
		private Type m_Type;
		private string m_Name;

		public Type Type{ get{ return m_Type; } set{ m_Type = value; } }
		public string Name{ get{ return m_Name; } set{ m_Name = value; } }

		public FoodInfo( Type type, string name )
		{
			m_Type = type;
			m_Name = name;
		}

		public Item Create()
		{
			Item item;

			try
			{
				item = (Item)Activator.CreateInstance( m_Type );
			}
			catch
			{
				item = null;
			}

			return item;
		}
	}
}