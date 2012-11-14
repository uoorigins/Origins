using System;
using Server;

//Fill in your MySQL database details here...

namespace Server.Accounting
{
	public class WAConfig
	{
		public const bool Enabled = false;

		public const bool UpdateOnWorldSave = true;
		public const bool UpdateOnWorldLoad = true;
	
		public const string
			DatabaseDriver		= "{MySQL ODBC 3.51 Driver}", //Shouldn't need changing
            DatabaseServer      = "", //Server IP of the database
            DatabaseName        = "", //Name of the database
			DatabaseTable		= "", //Name of the table storing accounts
			DatabaseUserID		= "", //Username for the database
			DatabasePassword	= ""; //Username password
	}
}