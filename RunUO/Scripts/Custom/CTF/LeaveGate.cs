using System;
using Server;
using Server.Mobiles;

namespace Server.Items
{
	public class LeaveGameGate : ConfirmationMoongate
	{
		[Constructable]
		public LeaveGameGate() 
		{
			this.GumpWidth = 420;
			this.GumpHeight = 280;
			this.Dispellable = false;
			this.TitleNumber = 1060635;
			this.TitleColor = 30720;
			this.MessageColor = 0xFFC000;
			this.MessageString = "If you leave the game, you may not be able to rejoin.  Are you sure you wish to leave the game?";
		}

		public LeaveGameGate( Serial ser ) : base( ser )
		{
		}

		public override void UseGate( Mobile m )
		{
			CTFTeam team = CTFGame.FindTeamFor( m );
			if ( team != null )
				team.Game.LeaveGame( m );

			base.UseGate( m );
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}
}
