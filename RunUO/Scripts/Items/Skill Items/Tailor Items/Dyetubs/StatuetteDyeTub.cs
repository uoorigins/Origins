using System;

namespace Server.Items
{
	public class StatuetteDyeTub : DyeTub, Engines.VeteranRewards.IRewardItem
	{
		public override bool AllowDyables{ get{ return false; } }
		public override bool AllowStatuettes{ get{ return true; } }
		public override string TargetMessage{ get{ return "Target the statuette to dye"; } } // Target the statuette to dye
		public override string FailMessage{ get{ return "You can only dye veteran reward statuettes with this tub."; } } // You can only dye veteran reward statuettes with this tub.
		public override int LabelNumber{ get{ return 1049741; } } // Reward Statuette Dye Tub
		public override CustomHuePicker CustomHuePicker{ get{ return CustomHuePicker.LeatherDyeTub; } }

		private bool m_IsRewardItem;

		[CommandProperty( AccessLevel.GameMaster )]
		public bool IsRewardItem
		{
			get{ return m_IsRewardItem; }
			set{ m_IsRewardItem = value; }
		}

		 
		public StatuetteDyeTub()
		{
			LootType = LootType.Blessed;
		}

		public override void OnDoubleClick( Mobile from )
		{
			if ( m_IsRewardItem && !Engines.VeteranRewards.RewardSystem.CheckIsUsableBy( from, this, null ) )
				return;

			base.OnDoubleClick( from );
		}

		public StatuetteDyeTub( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 1 ); // version

			writer.Write( (bool) m_IsRewardItem );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			switch ( version )
			{
				case 1:
				{
					m_IsRewardItem = reader.ReadBool();
					break;
				}
			}
		}
	}
}