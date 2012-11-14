using System;
using System.Data.Odbc;
using System.Security;
using System.Security.Cryptography;
using System.Net;
using System.Collections;
using System.Xml;
using System.Text;
using Server;
using Server.Misc;
using Server.Network;
using Server.Commands;

namespace Server.Accounting
{
	public class WebAccounting
	{

		public enum Status
		{
			Void = 0,
			Pending = 1,
			Active = 2,
			PWChanged = 3,
			EmailChanged = 4,
			Delete = 5
		}

		private static int QueryCount = 0; //Offset 0 values.

		public static bool UpdateOnWorldSave = true;
		public static bool UpdateOnWorldLoad = true;

		private static string
			DatabaseDriver = WAConfig.DatabaseDriver,
			DatabaseServer = WAConfig.DatabaseServer,
			DatabaseName = WAConfig.DatabaseName,
			DatabaseTable = WAConfig.DatabaseTable,
			DatabaseUserID = WAConfig.DatabaseUserID,
			DatabasePassword = WAConfig.DatabasePassword;

		static string ConnectionString = string.Format( "DRIVER={0};SERVER={1};DATABASE={2};UID={3};PASSWORD={4};",
			DatabaseDriver, DatabaseServer, DatabaseName, DatabaseUserID, DatabasePassword );

		static bool Synchronizing = false;

		public static void Initialize( )
		{
			SynchronizeDatabase( );
			CommandSystem.Register( "AccSync", AccessLevel.Administrator, new CommandEventHandler( Sync_OnCommand ) );

			if( UpdateOnWorldLoad )
			{
				EventSink.WorldLoad += new WorldLoadEventHandler( OnLoaded );
			}

			if( UpdateOnWorldSave )
			{
				EventSink.WorldSave += new WorldSaveEventHandler( OnSaved );
			}
			else
			{
				Timer.DelayCall( TimeSpan.FromMinutes( 10.0 ), TimeSpan.FromMinutes( 10.0 ), new TimerCallback( SynchronizeDatabase ) );
			}
		}

		public static void OnSaved( WorldSaveEventArgs e )
		{
			if( Synchronizing )
				return;

			SynchronizeDatabase( );
		}

		public static void OnLoaded( )
		{
			if( Synchronizing )
				return;

			SynchronizeDatabase( );
		}

		[Usage( "AccSync" )]
		[Description( "Synchronizes the Accounts Database" )]
		public static void Sync_OnCommand( CommandEventArgs e )
		{
			if( Synchronizing )
				return;

			Mobile from = e.Mobile;

			SynchronizeDatabase( );
			from.SendMessage( "Done Synchronizing Database!" );
		}

		public static void CreateAccountsFromDB( )
		{
			//Console.WriteLine( "Getting New Accounts..." );
			try
			{
				ArrayList ToCreateFromDB = new ArrayList( );
				OdbcConnection Connection = new OdbcConnection( ConnectionString );

				Connection.Open( );
				OdbcCommand Command = Connection.CreateCommand( );

				Command.CommandText = string.Format( "SELECT name,password,email FROM {0} WHERE state='{1}'", DatabaseTable, ( int )Status.Pending );
				OdbcDataReader reader = Command.ExecuteReader( );

				QueryCount += 1;

				while( reader.Read( ) )
				{
					string username = reader.GetString( 0 );
					string password = reader.GetString( 1 );
					string email = reader.GetString( 2 );

					if( Accounts.GetAccount( username ) == null )
						ToCreateFromDB.Add( Accounts.AddAccount( username, password, email ) );
				}
				reader.Close( );

				//Console.WriteLine( "Updating Database..." );
				foreach( Account a in ToCreateFromDB )
				{
					int ALevel = 0;

					if( a.AccessLevel == AccessLevel.Player )
					{
						ALevel = 1;
					}
					else if( a.AccessLevel == AccessLevel.Counselor )
					{
						ALevel = 2;
					}
					else if( a.AccessLevel == AccessLevel.GameMaster )
					{
						ALevel = 3;
					}
					else if( a.AccessLevel == AccessLevel.Seer )
					{
						ALevel = 4;
					}
					else if( a.AccessLevel == AccessLevel.Administrator )
					{
						ALevel = 6;
					}

					QueryCount += 1;

					Command.CommandText = string.Format( "UPDATE {0} SET email='{1}',password='{2}',state='{3}',access='{4}' WHERE name='{5}'", DatabaseTable, a.Email, a.CryptPassword, ( int )Status.Active, ALevel, a.Username );
					Command.ExecuteNonQuery( );
				}

				Connection.Close( );

				Console.WriteLine( "[{0} In-Game Accounts Created] ", ToCreateFromDB.Count );
			}
			catch( Exception e )
			{
				Console.WriteLine( "[In-Game Account Create] Error..." );
				Console.WriteLine( e );
			}
		}


		public static void CreateAccountsFromUO( )
		{
			//Console.WriteLine( "Exporting New Accounts..." );
			try
			{
				ArrayList ToCreateFromUO = new ArrayList( );
				OdbcConnection Connection = new OdbcConnection( ConnectionString );

				Connection.Open( );
				OdbcCommand Command = Connection.CreateCommand( );

				Command.CommandText = string.Format( "SELECT name FROM {0}", DatabaseTable );
				OdbcDataReader reader = Command.ExecuteReader( );

				QueryCount += 1;

				while( reader.Read( ) )
				{
					string username = reader.GetString( 0 );

					Account toCheck = Accounts.GetAccount( username ) as Account;

					if( toCheck == null )
						ToCreateFromUO.Add( toCheck );
				}
				reader.Close( );

				//Console.WriteLine( "Updating Database..." );
				foreach( Account a in ToCreateFromUO )
				{
					int ALevel = 0;

					if( a.AccessLevel == AccessLevel.Player )
					{
						ALevel = 1;
					}
					else if( a.AccessLevel == AccessLevel.Counselor )
					{
						ALevel = 2;
					}
					else if( a.AccessLevel == AccessLevel.GameMaster )
					{
						ALevel = 3;
					}
					else if( a.AccessLevel == AccessLevel.Seer )
					{
						ALevel = 4;
					}
					else if( a.AccessLevel == AccessLevel.Administrator )
					{
						ALevel = 6;
					}

					PasswordProtection PWMode = AccountHandler.ProtectPasswords;
					string Password = "";

					switch( PWMode )
					{
						case PasswordProtection.None: { Password = a.PlainPassword; } break;
						case PasswordProtection.Crypt: { Password = a.CryptPassword; } break;
						default: { Password = a.NewCryptPassword; } break;
					}

					QueryCount += 1;

					OdbcCommand InsertCommand = Connection.CreateCommand( );

					InsertCommand.CommandText = string.Format( "INSERT INTO {0} (name,password,email,access,timestamp,state) VALUES( '{1}', '{2}', '{3}', '{4}', '{5}', '{6}')", DatabaseTable, a.Username, Password, a.Email, ALevel, ToUnixTimestamp( a.Created ), ( int )Status.Active );
					InsertCommand.ExecuteNonQuery( );
				}

				Connection.Close( );

				Console.WriteLine( "[{0} Database Accounts Added] ", ToCreateFromUO.Count );
			}
			catch( Exception e )
			{
				Console.WriteLine( "[Database Account Create] Error..." );
				Console.WriteLine( e );
			}
		}

		public static void UpdateUOPasswords( )
		{
			//Console.WriteLine( "Getting New Passwords..." );
			try
			{
				ArrayList ToUpdatePWFromDB = new ArrayList( );
				OdbcConnection Connection = new OdbcConnection( ConnectionString );

				Connection.Open( );
				OdbcCommand Command = Connection.CreateCommand( );

				Command.CommandText = string.Format( "SELECT name,password FROM {0} WHERE state='{1}'", DatabaseTable, ( int )Status.PWChanged );
				OdbcDataReader reader = Command.ExecuteReader( );

				QueryCount += 1;

				while( reader.Read( ) )
				{
					string username = reader.GetString( 0 );
					string password = reader.GetString( 1 );

					Account AtoUpdate = Accounts.GetAccount( username ) as Account;

					if( AtoUpdate != null )
					{
						PasswordProtection PWMode = AccountHandler.ProtectPasswords;
						string Password = "";

						switch( PWMode )
						{
							case PasswordProtection.None: { Password = AtoUpdate.PlainPassword; } break;
							case PasswordProtection.Crypt: { Password = AtoUpdate.CryptPassword; } break;
							default: { Password = AtoUpdate.NewCryptPassword; } break;
						}

						if( Password == null || Password == "" || Password != password )
						{
							AtoUpdate.SetPassword( password );
							ToUpdatePWFromDB.Add( AtoUpdate );
						}
					}
				}
				reader.Close( );

				//Console.WriteLine( "Updating Database..." );
				foreach( Account a in ToUpdatePWFromDB )
				{
					PasswordProtection PWModeU = AccountHandler.ProtectPasswords;
					string PasswordU = "";

					switch( PWModeU )
					{
						case PasswordProtection.None: { PasswordU = a.PlainPassword; } break;
						case PasswordProtection.Crypt: { PasswordU = a.CryptPassword; } break;
						default: { PasswordU = a.NewCryptPassword; } break;
					}

					QueryCount += 1;

					Command.CommandText = string.Format( "UPDATE {0} SET state='{1}',password='{2}' WHERE name='{3}'", DatabaseTable, ( int )Status.Active, PasswordU, a.Username );
					Command.ExecuteNonQuery( );
				}

				Connection.Close( );

				Console.WriteLine( "[{0} In-game Passwords Changed] ", ToUpdatePWFromDB.Count );
			}
			catch( System.Exception e )
			{
				Console.WriteLine( "[In-Game Password Change] Error..." );
				Console.WriteLine( e );
			}
		}

		public static void UpdateDBPasswords( )
		{
			//Console.WriteLine( "Exporting New Passwords..." );
			try
			{
				ArrayList ToUpdatePWFromUO = new ArrayList( );
				OdbcConnection Connection = new OdbcConnection( ConnectionString );

				Connection.Open( );
				OdbcCommand Command = Connection.CreateCommand( );

				Command.CommandText = string.Format( "SELECT name,password FROM {0} WHERE state='{1}'", DatabaseTable, ( int )Status.Active );
				OdbcDataReader reader = Command.ExecuteReader( );

				QueryCount += 1;

				while( reader.Read( ) )
				{
					string username = reader.GetString( 0 );
					string password = reader.GetString( 1 );

					Account AtoUpdate = Accounts.GetAccount( username ) as Account;

					if( AtoUpdate != null )
					{
						PasswordProtection PWMode = AccountHandler.ProtectPasswords;
						string Password = "";

						switch( PWMode )
						{
							case PasswordProtection.None: { Password = AtoUpdate.PlainPassword; } break;
							case PasswordProtection.Crypt: { Password = AtoUpdate.CryptPassword; } break;
							default: { Password = AtoUpdate.NewCryptPassword; } break;
						}

						if( Password == null || Password == "" || Password != password )
						{
							ToUpdatePWFromUO.Add( AtoUpdate );
						}
					}
				}
				reader.Close( );

				//Console.WriteLine( "Updating Database..." );
				foreach( Account a in ToUpdatePWFromUO )
				{
					PasswordProtection PWModeU = AccountHandler.ProtectPasswords;
					string PasswordU = "";

					switch( PWModeU )
					{
						case PasswordProtection.None: { PasswordU = a.PlainPassword; } break;
						case PasswordProtection.Crypt: { PasswordU = a.CryptPassword; } break;
						default: { PasswordU = a.NewCryptPassword; } break;
					}

					QueryCount += 1;

					Command.CommandText = string.Format( "UPDATE {0} SET state='{1}',password='{2}' WHERE name='{3}'", DatabaseTable, ( int )Status.Active, PasswordU, a.Username );
					Command.ExecuteNonQuery( );
				}

				Connection.Close( );

				Console.WriteLine( "[{0} Database Passwords Changed] ", ToUpdatePWFromUO.Count );
			}
			catch( Exception e )
			{
				Console.WriteLine( "[Database Password Change] Error..." );
				Console.WriteLine( e );
			}
		}



		public static void UpdateUOEmails( )
		{
			//Console.WriteLine( "Getting New Emails..." );
			try
			{
				ArrayList ToUpdateEmailFromDB = new ArrayList( );
				OdbcConnection Connection = new OdbcConnection( ConnectionString );

				Connection.Open( );
				OdbcCommand Command = Connection.CreateCommand( );

				Command.CommandText = string.Format( "SELECT name,email FROM {0} WHERE state='{1}'", DatabaseTable, ( int )Status.EmailChanged );
				OdbcDataReader reader = Command.ExecuteReader( );

				QueryCount += 1;

				while( reader.Read( ) )
				{
					string username = reader.GetString( 0 );
					string email = reader.GetString( 1 );

					Account AtoUpdate = Accounts.GetAccount( username ) as Account;

					if( AtoUpdate != null && ( AtoUpdate.Email == null || AtoUpdate.Email == "" || AtoUpdate.Email != email ) )
					{
						AtoUpdate.Email = email;
						ToUpdateEmailFromDB.Add( AtoUpdate );
					}
				}
				reader.Close( );

				//Console.WriteLine( "Updating Database..." );
				foreach( Account a in ToUpdateEmailFromDB )
				{
					QueryCount += 1;

					Command.CommandText = string.Format( "UPDATE {0} SET state='{1}',email='{2}' WHERE name='{3}'", DatabaseTable, ( int )Status.Active, a.Email, a.Username );
					Command.ExecuteNonQuery( );
				}

				Connection.Close( );

				Console.WriteLine( "[{0} In-Game Emails Changed] ", ToUpdateEmailFromDB.Count );
			}
			catch( System.Exception e )
			{
				Console.WriteLine( "[In-Game Email Change] Error..." );
				Console.WriteLine( e );
			}
		}

		public static void UpdateDBEmails( )
		{
			//Console.WriteLine( "Exporting New Emails..." );
			try
			{
				ArrayList ToUpdateEmailFromUO = new ArrayList( );
				OdbcConnection Connection = new OdbcConnection( ConnectionString );

				Connection.Open( );
				OdbcCommand Command = Connection.CreateCommand( );

				Command.CommandText = string.Format( "SELECT name,email FROM {0} WHERE state='{1}'", DatabaseTable, ( int )Status.Active );
				OdbcDataReader reader = Command.ExecuteReader( );

				QueryCount += 1;

				while( reader.Read( ) )
				{
					string username = reader.GetString( 0 );
					string email = reader.GetString( 1 );

					Account AtoUpdate = Accounts.GetAccount( username ) as Account;

					if( AtoUpdate != null && ( AtoUpdate.Email == null || AtoUpdate.Email == "" || AtoUpdate.Email != email ) )
						ToUpdateEmailFromUO.Add( AtoUpdate );
				}
				reader.Close( );

				//Console.WriteLine( "Updating Database..." );
				foreach( Account a in ToUpdateEmailFromUO )
				{
					QueryCount += 1;

					Command.CommandText = string.Format( "UPDATE {0} SET state='{1}',email='{2}' WHERE name='{3}'", DatabaseTable, ( int )Status.Active, a.Email, a.Username );
					Command.ExecuteNonQuery( );
				}

				Connection.Close( );

				Console.WriteLine( "[{0} Database Emails Changed] ", ToUpdateEmailFromUO.Count );
			}
			catch( Exception e )
			{
				Console.WriteLine( "[Database Email Change] Error..." );
				Console.WriteLine( e );
			}
		}

		public static void SynchronizeDatabase( )
		{
			if( Synchronizing || !WAConfig.Enabled )
				return;

			Synchronizing = true;

			Console.WriteLine( "Accounting System..." );

			CreateAccountsFromDB( );
			CreateAccountsFromUO( );

			UpdateUOEmails( );
			UpdateDBEmails( );

			UpdateUOPasswords( );
			UpdateDBPasswords( );

			Console.WriteLine( string.Format( "[Executed {0} Database Queries]", QueryCount.ToString( ) ) );

			QueryCount = 0;

			World.Save( );

			Synchronizing = false;
		}

		static double ToUnixTimestamp( DateTime date )
		{
			DateTime origin = new DateTime( 1970, 1, 1, 0, 0, 0, 0 );

			TimeSpan diff = date - origin;

			return Math.Floor( diff.TotalSeconds );
		}


	}
}