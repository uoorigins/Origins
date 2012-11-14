using System;

namespace Server.Items
{
	/// <summary>
	/// Available on some crossbows, this special move allows archers to fire while on the move.
	/// This shot is somewhat less accurate than normal, but the ability to fire while running is a clear advantage.
	/// </summary>
	public class MovingShot : WeaponAbility
	{
		public MovingShot()
		{
		}

		public override int BaseMana{ get{ return 15; } }
		public override int AccuracyScalar{ get{ return -25; } }



		public override bool ValidatesDuringHit { get { return false; } }

	}
}